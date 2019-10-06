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
        const int REQ_COUNT = 1000;
        public Form2()
        {
            InitializeComponent();
        }

        private void Normalbrn_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            var watch = Stopwatch.StartNew();
            List<HttpRequestModel> list = PrepareData(REQ_COUNT);
            RunDownloadSync(list);
            watch.Stop();
            var elspms = watch.ElapsedMilliseconds;

            textBox1.AppendText($"Total Execution time: {elspms}, requests: {list.Count}");
        }

        private async void Async_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            var watch = Stopwatch.StartNew();
            List<HttpRequestModel> list = PrepareData(REQ_COUNT);
            await RunDownloadAsync(list);
            watch.Stop();
            var elspms = watch.ElapsedMilliseconds;

            textBox1.AppendText($"Total Execution time: {elspms}, requests: {list.Count}");
        }

        private async void ParallelAsync_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            var watch = Stopwatch.StartNew();
            List<HttpRequestModel> list = PrepareData(REQ_COUNT);
            await RunDownloadParallelAsync(list);
            watch.Stop();
            var elspms = watch.ElapsedMilliseconds;

            textBox1.AppendText($"Total Execution time: {elspms}, requests: {list.Count}");
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
                tasks.Add(Task.Run(() => DownloadSite(site)));
            }
            var results = await Task.WhenAll(tasks);

            foreach (var item in results)
            {
                ReportWebsiteInfo(item);
            }
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
            HttpClient client = new HttpClient();
            var watch = Stopwatch.StartNew();
            HttpResponseMessage response = await client.PostAsync(request.Url, request.Content).ConfigureAwait(false);
            res.WebsiteUrl = request.Url;
            res.WebsiteData = await response.Content.ReadAsStringAsync();
            watch.Stop();
            res.Timed = watch.ElapsedMilliseconds;
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
            textBox1.AppendText($"{data.WebsiteUrl} downloaded: {data.WebsiteData.Length} charecters timed: {data.Timed}{Environment.NewLine}");
        }

        
    }
}
