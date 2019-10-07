using SiteLoaderLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiteLoader
{
    public partial class Form2 : Form
    {
        const string URL = "https://pointerqa-web02.pointerbi.com/fleetcore.api";
        const int REQ_COUNT = 5;
        Stopwatch _watch;
        public Form2()
        {
            InitializeComponent();
            txtReqCount.Text = REQ_COUNT.ToString();
            label1.Text =  REQ_COUNT.ToString();
        }

        private void Normalbrn_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            _watch = Stopwatch.StartNew();
            List<HttpRequestModel> list = PrepareData(GetRequestCount());
            label1.Text = list.Count.ToString();
            RunDownloadSync(list);
            
        }

        private async void Async_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            var watch = Stopwatch.StartNew();
            List<HttpRequestModel> list = PrepareData(GetRequestCount());
            label1.Text = list.Count.ToString();
            await RunDownloadAsync(list);
            watch.Stop();
            var elspms = watch.ElapsedMilliseconds;

            textBox1.AppendText($"Total Execution time: {elspms}, requests: {list.Count}");
        }

        private async void ParallelAsync_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            
             _watch = Stopwatch.StartNew();
            List<HttpRequestModel> list = PrepareData(GetRequestCount());
            label1.Text = list.Count.ToString();
            await RunDownloadParallelAsyncTest(list);
          
        }


        private void RunDownloadSync(List<HttpRequestModel> list)
        {
            
            foreach (var site in list)
            {
                WebsiteDataModel model = DownloadSite(site);
                ReportWebsiteInfo(model);
            }
        }

        private async Task RunDownloadAsync(List<HttpRequestModel> list)
        {            
            foreach (var site in list)
            {
                WebsiteDataModel model = await Task.Run(() => DownloadSite(site));
                ReportWebsiteInfo(model);
            }
        }

        private async Task RunDownloadParallelAsync(List<HttpRequestModel> list)
        {
            
            List<Task<WebsiteDataModel>> tasks = new List<Task<WebsiteDataModel>>();
            foreach (var site in list)
            {
                tasks.Add(DownloadSiteAsync(site));
               // tasks.Add(Task.Run(() => DownloadSite(site)));
            }
            var results = await Task.WhenAll(tasks);

            foreach (var item in results)
            {
                ReportWebsiteInfo(item);
            }
        }
        private async Task RunDownloadParallelAsyncTest(List<HttpRequestModel> list)
        {
            
            List<Task<WebsiteDataModel>> tasks = new List<Task<WebsiteDataModel>>();
            foreach (var site in list)
            {
                DownloadSiteAsync(site).ContinueWith((m) => ReportWebsiteInfo(m.Result), TaskScheduler.FromCurrentSynchronizationContext());
            }
            var results = await Task.WhenAll(tasks);
           // textBox1.AppendText($"Total Execution time: , requests: {list.Count}");

        }

        private WebsiteDataModel DownloadSite(HttpRequestModel request)
        {
            var watch = Stopwatch.StartNew();
            WebsiteDataModel res = new WebsiteDataModel();
            WebClient wClient = new WebClient();
            wClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            HttpClient client = new HttpClient();
            res.WebsiteUrl = request.Url;
            string data = request.ContentString;// request.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            res.WebsiteData = wClient.UploadString(request.Url,"POST", data);
            watch.Stop();
            res.Timed = watch.ElapsedMilliseconds;
            return res;
        }
        
        
        private async Task<WebsiteDataModel> DownloadSiteAsync(HttpRequestModel request)
        {
            WebsiteDataModel res = new WebsiteDataModel();
            var watch = Stopwatch.StartNew();
            try
            {
                using (HttpClient Client = new HttpClient())
                {
                    Client.Timeout = TimeSpan.FromMinutes(100);
                    
                    //HttpResponseMessage response = await Client.PostAsync(request.Url, request.Content).ConfigureAwait(false);
                    HttpResponseMessage response = await Client.PostAsync(request.Url, new StringContent(request.ContentString, Encoding.UTF8, "application/json"));
                   
                    res.WebsiteData = await response.Content.ReadAsStringAsync();
                    
                }
            }
            catch(Exception ex)
            {
                res.WebsiteData = ex.Message;
            }
            finally
            {
                res.WebsiteUrl = request.Url;
                watch.Stop();
                res.Timed = watch.ElapsedMilliseconds;
            }
            return res;
        }

        private List<HttpRequestModel> PrepareData(int count =10)
        {
            string url = $"{URL}/token";
            HttpContent vladicontent = new StringContent("{\"username\":\"vladi\",\"password\":\"Aa1111\",\"langId\":1}", Encoding.UTF8, "application/json");
            string vladiStringContent = "{\"username\":\"vladi\",\"password\":\"Aa1111\",\"langId\":1}";
            HttpContent vitacontent = new StringContent("{\"username\":\"vita\",\"password\":\"Aa1111\",\"langId\":1}", Encoding.UTF8, "application/json");
            string vitaStringContent = "{\"username\":\"vita\",\"password\":\"Aa1111\",\"langId\":1}";
            List<HttpRequestModel> list = new List<HttpRequestModel>();
            for (int i = 0; i < count; i++)
            {
                list.Add(new HttpRequestModel() { Content = vladicontent, ContentString = vladiStringContent, Url = url });
                list.Add(new HttpRequestModel() { Content = vitacontent, ContentString = vitaStringContent, Url = url });
            }
            

            return list;
        }

        private void ReportWebsiteInfo(WebsiteDataModel data)
        {
            var count = int.Parse(label1.Text) - 1;
            label1.Text = count.ToString();
            textBox1.AppendText($"{data.WebsiteUrl} downloaded: {data.WebsiteData.Length} charecters timed: {data.Timed}{Environment.NewLine}");
            if(count == 0)
            {
                _watch.Stop();
                var elspms = _watch.ElapsedMilliseconds;

                textBox1.AppendText($"Total Execution time: {elspms}, requests: {txtReqCount.Text}");
            }
        }

        private int GetRequestCount()
        {
            int.TryParse(txtReqCount.Text, out int res);
            if (res == 0)
                return REQ_COUNT;
            else
                return res;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            f.Show();
        }
    }
}
