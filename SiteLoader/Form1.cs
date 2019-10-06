using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiteLoader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Normal_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            var watch = Stopwatch.StartNew();
            RunDownloadSync();
            watch.Stop();
            var elspms = watch.ElapsedMilliseconds;

            textBox1.AppendText($"Total Execution {elspms}");

        }

        private async void Async_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            var watch = Stopwatch.StartNew();
            await RunDownloadAsync();
            watch.Stop();
            var elspms = watch.ElapsedMilliseconds;

            textBox1.AppendText($"Total Execution {elspms}");
        }

        private async void ParallelAsync_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            var watch = Stopwatch.StartNew();
            await RunDownloadParallelAsync();
            watch.Stop();
            var elspms = watch.ElapsedMilliseconds;

            textBox1.AppendText($"Total Execution {elspms}");
        }

        private void RunDownloadSync()
        {
            List<string> list = PrepareData();
            foreach (var site in list)
            {
                WebsiteDataModel model = DownloadSite(site);
                ReportWebsiteInfo(model);
            }
        }

        private async Task RunDownloadAsync()
        {
            List<string> list = PrepareData();
            foreach (var site in list)
            {
                WebsiteDataModel model = await Task.Run(() => DownloadSite(site));
                ReportWebsiteInfo(model);
            }            
        }

        private async Task RunDownloadParallelAsync()
        {
            List<string> list = PrepareData();
            List<Task<WebsiteDataModel>> tasks = new List<Task<WebsiteDataModel>>();
            foreach (var site in list)
            {
                tasks.Add(DownloadSiteAsync(site));              
            }
            var results = await Task.WhenAll(tasks);
            
            foreach (var item in results)
            {
                ReportWebsiteInfo(item);
            }
        }
        /*
        private void RunDownloadParallelAsync()
        {
            List<string> list = PrepareData();
            List<Task<WebsiteDataModel>> tasks = new List<Task<WebsiteDataModel>>();
            foreach (var site in list)
            {
                DownloadSiteAsync(site).ContinueWith((m) => ReportWebsiteInfo(m.Result), TaskScheduler.FromCurrentSynchronizationContext());
                // tasks.Add());               
            }
            // var results = await Task.WhenAll(tasks);

            //foreach (var item in results)
            //{
            //    ReportWebsiteInfo(item);
            //}
        }
        */
        private WebsiteDataModel DownloadSite(string url)
        {
            WebsiteDataModel res = new WebsiteDataModel();

            WebClient client = new WebClient();
            res.WebsiteUrl = url;
            res.WebsiteData = client.DownloadString(url);
            //Thread.Sleep(1000);

            return res;
        }

        private async Task<WebsiteDataModel> DownloadSiteAsync(string url)
        {
            WebsiteDataModel res = new WebsiteDataModel();

            WebClient client = new WebClient();
            res.WebsiteUrl = url;
            res.WebsiteData = await client.DownloadStringTaskAsync(url);
            //Thread.Sleep(1000);

            return res;
        }
        private List<string> PrepareData()
        {
            List<string> list = new List<string>();
            list.Add("https://www.msn.com");
            list.Add("https://www.google.com");
            list.Add("https://www.cnn.com");
            list.Add("https://www.microsoft.com");
            list.Add("https://www.yahoo.com");
            list.Add("https://www.foxnews.com");
            
            return list;
        }

        private void ReportWebsiteInfo(WebsiteDataModel data)
        {
            textBox1.AppendText($"{data.WebsiteUrl} downloaded: {data.WebsiteData.Length} charecters.{Environment.NewLine}");
        }

        
    }
}
