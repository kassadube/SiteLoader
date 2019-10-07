﻿using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SiteLoaderLib.Model
{
    public class SenarioManager
    {
        public List<SenarioInfo> _senarios;
        const string API_URL = "https://pointerqa-web02.pointerbi.com/fleetcore.api";
        const string FLEET_URL = "https://pointerqa-web02.pointerbi.com/fleet";
        HttpManager _httpManager;
        int senarioId = 1;
        public SenarioManager()
        {
            _senarios = new List<SenarioInfo>();
            _httpManager = new HttpManager();
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
            HttpResultValue  t = await _httpManager.PostResult(model);
            s.User.ResultValue = t;
            if (t.IsSuccess)
            {
                Log.Verbose(t.Value);
                JObject obj = JObject.Parse(t.Value);
                s.User.Token = obj.First.First.Value<string>();
            }
            else
            {
                Log.Error(t.Ex, $"{t.sId}");
            }
            
            return t;
        }

        public List<SenarioInfo> GetSenarios()
        {
            return _senarios;
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
            return _senarios.FindAll(x => x.User.ResultValue != null).Count;
        }
        public int GetOpenSenarions()
        {
            return _senarios.FindAll(x => x.User.ResultValue == null).Count;
        }
        public int GetFailedSenarions()
        {
            return _senarios.FindAll(x => x.User.ResultValue != null && !x.User.ResultValue.IsSuccess).Count;
        }
    }

    public class SenarioInfo
    {
        string _baseUrl;
        public int Id { get; private set; }
        public SenarioInfo(string url, int id)
        {
            _baseUrl = url;
            Id = id;
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
        public Task<HttpResultValue> CreateLoginTask(HttpManager _httpManager)
        {
            HttpRequestModel m = GetLoginRequest();
            var t = new Task<HttpResultValue>(() => _httpManager.PostResult(m).Result);
            return t;

            
        }
    }
}
