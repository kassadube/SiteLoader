using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SiteLoaderLib.Model
{
    public class SenarioManager
    {
        public List<SenarioInfo> _senarios;
        // string API_URL = "https://pointerqa-web02.pointerbi.com/fleetcore.api";
     //   const string FLEET_URL = "https://pointerqa-web02.pointerbi.com/fleet";
     //   string API_URL = "http://pointerqa-web03.northeurope.cloudapp.azure.com/fleetcore.api";
     //   const string FLEET_URL = "http://pointerqa-web03.northeurope.cloudapp.azure.com/fleet";
          string API_URL = "http://webqa-staging02.pointerbi.com/fleetcore.api";
        const string FLEET_URL = "http://webqa-staging02.pointerbi.com/fleet";

        const string SiteDefinitionURL = "/api/site/definition";
        const string FleetVehiclesURL = "/api/fleetview/vehicles?isUpdate=1&vehicles=1&alertCount=0";
        const string AlertCountURL = "/api/fleetview/vehicles?isUpdate=1&vehicles=0&alertCount=1";
        const string Notification2URL = "/api/notification?dateFilterId=2&MaxId=0";
        const string Notification1URL = "/api/notification?dateFilterId=1&MaxId=0";
        const string PolygonsURL = "/api/account/polygon";
        const string LayersURL = "/api/account/layer";
        const string AssetsURL = "/api/fleetview/assets";
        const string VehiclesURL = "/api/fleetview/vehicles";
        const string ColumnsURL = "/api/fleetview/columns";
        const string DriverForAssignmentURL = "/api/driver/driversforassignment";


        HttpManager _httpManager;
        int senarioId = 1;
        public SenarioManager()
        {
            _senarios = new List<SenarioInfo>();
            _httpManager = new HttpManager();
            
        }
        public void SetApiUrl(string url)
        {
            API_URL = url;
        }

        public void AddUser(string userName, string password, int langId = 1, int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                _senarios.Add(new SenarioInfo(API_URL, senarioId++)
                {
                    User = new SenarioUserLogin()
                    {
                        UserName = userName,
                        Password = password,
                        LangId = langId
                    }

                });
            }
           
        }
        public List<Task<HttpResultValue>> DoLogin()
        {
            List<Task<HttpResultValue>> tasks = new List<Task<HttpResultValue>>();
            foreach (var item in _senarios)
            {
                tasks.Add(LoginSenarion(item));
            }
            return tasks;
        }

        public async Task<HttpResultValue> LoginSenarion(SenarioInfo s)
        {
            HttpRequestModel model = s.GetLoginRequest();
            s.User.Started = true;
            HttpResultValue  t = await _httpManager.PostResult(model);
            s.User.ResultValue = t;
            if (t.IsSuccess)
            {
                Log.Verbose(t.Value);
                JObject obj = JObject.Parse(t.Value);
                s.User.Token = obj.First.First.Value<string>();
                s.User.finished = true;
                Log.Debug($"success url {model.Url} sid {t.sId}");
            }
            else
            {
                Log.Error(t.Ex, $"{t.sId}");
                s.User.finished = true;
            }
           
            return t;
        }
        public async Task<HttpResultValue> RunSenarioItems(SenarioInfo s)
        {
            SenarioItemInfo item = s.GetNextItem();
            item.Started = true;
            HttpResultValue t = await (item.Httpmethod == HttpMethod.Get ? _httpManager.GetResult(item.RequestModel) : _httpManager.PostResult(item.RequestModel));
            item.ResultValue = t;
            item.finished = true;
            if (t.IsSuccess)
            {
               // Log.Verbose(t.Value);
              //  JObject obj = JObject.Parse(t.Value);
              //  s.User.Token = obj.First.First.Value<string>();
             //   s.User.finished = true;
                Log.Debug($"success url {item.RequestModel.Url} sid {t.sId} timed = {t.Timed}");
            }
            else
            {
                Log.Error(t.Ex, $"{t.sId} url {item.RequestModel.Url}");
              //  s.User.finished = true;
            }

            return t;
        }

        public List<SenarioInfo> GetSenarios(bool newOnes = false)
        {
            if (newOnes)
            {
                return _senarios.FindAll(x => !x.User.Started);
            }
            return _senarios;
        }
        public int GetRequestCount()
        {
            return _senarios.Sum(x => x.ItemsCount);
        }

        public double? GetAvarage()
        {
            //int loginCount _senarios.Where(x => x.User.finished && x.User.ResultValue != null).Average(x => x.User.ResultValue.Timed);
            List<SenarioItemInfo> items = new List<SenarioItemInfo>();
            foreach (var item in _senarios.Select(x => x.Items))
            {
                items.AddRange(item);                
            }
            var it = items.Where(s => s.ResultValue != null && s.finished);
            return it.Count()== 0? 0: it.Average(x => x.ResultValue.Timed);
        }
        public int GetInQueue()
        {
            List<SenarioItemInfo> items = new List<SenarioItemInfo>();
            foreach (var item in _senarios.Select(x => x.Items))
            {
                items.AddRange(item);
            }
            return items.Where(s => s.ResultValue != null && s.Started && !s.finished).Count();

        }
        public void RemoveAll()
        {
            senarioId = 1;
            _senarios.Clear();
        }
        public List<Task<HttpResultValue>> CreateLoginTasks()
        {
            List<Task<HttpResultValue>> tasks = new List<Task<HttpResultValue>>();
            foreach (var item in _senarios)
            {
                tasks.Add(item.CreateLoginTask(_httpManager));
            }
            return tasks;
        }

        public int GetFinishedSenarions()
        {
            return _senarios.FindAll(x => x.User.finished).Count;
        }
        public int GetStartedSenarions()
        {
            return _senarios.FindAll(x => x.User.Started).Count;
        }
        public int GetOpenSenarions()
        {
            return _senarios.FindAll(x => x.User.ResultValue == null).Count;
        }
        public int GetFailedSenarions()
        {
            return _senarios.FindAll(x => x.User.ResultValue != null && x.User.finished && !x.User.ResultValue.IsSuccess).Count;
        }
    }

    public class SenarioInfo
    {
        string _baseUrl;
        List<SenarioItemInfo> _items;
        int currentItem = 0;
        public int ItemsCount { get { return _items.Count; } }
        public int Id { get; private set; }
        public SenarioInfo(string url, int id)
        {
            _baseUrl = url;
            Id = id;
            _items = new List<SenarioItemInfo>();
             _items.Add(new SenarioItemInfo($"{_baseUrl}{URLS.SiteDefinitionURL}", HttpMethod.Get));
             _items.Add(new SenarioItemInfo($"{_baseUrl}{URLS.EventSound0URL}", HttpMethod.Get));
             _items.Add(new SenarioItemInfo($"{_baseUrl}{URLS.EventSound1URL}", HttpMethod.Get));
             _items.Add(new SenarioItemInfo($"{_baseUrl}{URLS.EventSound2URL}", HttpMethod.Get));
             _items.Add(new SenarioItemInfo($"{_baseUrl}{URLS.AssetsURL}", HttpMethod.Get));
             _items.Add(new SenarioItemInfo($"{_baseUrl}{URLS.ColumnsURL}", HttpMethod.Get));
             _items.Add(new SenarioItemInfo($"{_baseUrl}{URLS.VehiclesURL}", HttpMethod.Get));
             _items.Add(new SenarioItemInfo($"{_baseUrl}{URLS.DriverForAssignmentURL}", HttpMethod.Get));
             _items.Add(new SenarioItemInfo($"{_baseUrl}{URLS.AddinsURL}", HttpMethod.Get));
             _items.Add(new SenarioItemInfo($"{_baseUrl}{URLS.FleetVehiclesURL}", HttpMethod.Get));
             _items.Add(new SenarioItemInfo($"{_baseUrl}{URLS.AlertCountURL}", HttpMethod.Get));
             _items.Add(new SenarioItemInfo($"{_baseUrl}{URLS.AdminLayersURL}", HttpMethod.Get));
            _items.Add(new SenarioItemInfo($"{_baseUrl}{URLS.providersURL}", HttpMethod.Get));
            _items.Add(new SenarioItemInfo($"{_baseUrl}{URLS.PolygonsURL}", HttpMethod.Post));
            _items.Add(new SenarioItemInfo($"{_baseUrl}{URLS.LayersURL}", HttpMethod.Post));


            /*
             _items.Add(new SenarioItemInfo($"{_baseUrl}{URLS.SiteDefinitionURL}", HttpMethod.Get));
             _items.Add(new SenarioItemInfo($"{_baseUrl}{URLS.AssetsURL}", HttpMethod.Get));
             _items.Add(new SenarioItemInfo($"{_baseUrl}{URLS.AlertCountURL}", HttpMethod.Get));

             _items.Add(new SenarioItemInfo($"{_baseUrl}{URLS.DriverForAssignmentURL}", HttpMethod.Get));
             */
        }
        public SenarioUserLogin User { get; set; }

        public HttpRequestModel GetLoginRequest()
        {
            HttpRequestModel model = new HttpRequestModel()
            {
                Url = $"{_baseUrl}/token",
                ContentString = $"{{\"username\":\"{User.UserName}\",\"password\":\"{User.Password}\",\"langId\":{User.LangId.ToString()}}}",
                SId = Id
            };
            return model;
        }

        public SenarioItemInfo GetNextItem()
        {
            if (currentItem > _items.Count - 1)
                currentItem = 0;
            SenarioItemInfo item = _items[currentItem];
            currentItem++;

            HttpRequestModel model = new HttpRequestModel()
            {
                Url = item.Url,
                Token = this.User.Token,
                ContentString = $"{{\"WithOutPoints\":\"{1}\"}}",
                SId = Id,
                IId = 3334
            };
            item.RequestModel = model;
            item.Started = false;
            item.finished = false;
            return item;
        }

        public Task<HttpResultValue> CreateLoginTask(HttpManager _httpManager)
        {
            HttpRequestModel m = GetLoginRequest();
            var t = new Task<HttpResultValue>(() => _httpManager.PostResult(m).Result);
            return t;

            
        }
        public List<SenarioItemInfo> Items { get { return _items; } }
    }

    public class SenarioItemInfo : SenarioBase
    {

        HttpMethod method;
        public SenarioItemInfo(string url, HttpMethod httpMethod , string postContent ="")
        {
            Url = url;
            Httpmethod = httpMethod;
            PostContent = postContent;
        }
        public string Url { get; set; }
        public HttpMethod Httpmethod { get; set; }
        public HttpRequestModel RequestModel { get; set; }
        public string PostContent { get; set; }        
    }

    public class SenarioSummery
    {
        public int All { get; set; }
        public int Failed { get; set; }
        public int Succeeded { get; set; }
        public double? Average { get; set; }
        public int InQueue { get; set; }

    }

    public class URLS
    {
       // public static string API_URL = "https://pointerqa-web02.pointerbi.com/fleetcore.api";
       // public static string FLEET_URL = "https://pointerqa-web02.pointerbi.com/fleet";
          const string API_URL = "http://13.74.47.169/fleetcore.api";
         const string FLEET_URL = "http://13.74.47.169/fleet";

        public static string SiteDefinitionURL = "/api/site/definition";
        public static string FleetVehiclesURL = "/api/fleetview/vehicles?isUpdate=1&vehicles=1&alertCount=0";
        public static string AlertCountURL = "/api/fleetview/vehicles?isUpdate=1&vehicles=0&alertCount=1";
        public static string Notification2URL = "/api/notification?dateFilterId=2&MaxId=0";
        public static string Notification1URL = "/api/notification?dateFilterId=1&MaxId=0";
        public static string PolygonsURL = "/api/account/polygon";
        public static string LayersURL = "/api/account/layer";
        public static string AssetsURL = "/api/fleetview/assets";
        public static string VehiclesURL = "/api/fleetview/vehicles";
        public static string ColumnsURL = "/api/fleetview/columns";
        public static string DriverForAssignmentURL = "/api/driver/driversforassignment";
        public static string EventSound0URL = "/api/downloads/eventSound/0";
        public static string EventSound1URL = "/api/downloads/eventSound/1";
        public static string EventSound2URL = "/api/downloads/eventSound/2";
        public static string AddinsURL = "/api/fleetview/addins";
        public static string AdminLayersURL = "/api/account/adminlayer";
        public static string providersURL = "/api/account/providers";
    }
}
