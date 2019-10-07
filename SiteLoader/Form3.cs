using SiteLoaderLib;
using SiteLoaderLib.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiteLoader
{
    public partial class Form3 : Form
    {
        SenarioManager manager;
        public Form3()
        {
            InitializeComponent();
            manager = new SenarioManager();
        }

        private async void ParallelAsync_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            manager.AddUser("vladi", "Aa1111", count: 50);
            //  await RunDownloadParallelAsync();
            await RunDownloadParallelAsyncTest();
        }

        private async Task RunDownloadParallelAsync()
        {
            var tasks =  manager.DoLogin();            
            var results = await Task.WhenAll(tasks);
            foreach (var item in results)
            {
                ReportWebsiteInfo(item);
            }
        }

        private async Task RunDownloadParallelAsyncTest()
        {
            List<Task<HttpResultValue>> tasks = new List<Task<HttpResultValue>>();
            foreach (var item in manager.GetSenarios())
            {
                manager.LoginSenarion(item).ContinueWith((m) => ReportWebsiteInfo(m.Result), TaskScheduler.FromCurrentSynchronizationContext());
               // DownloadSiteAsync(site).ContinueWith((m) => ReportWebsiteInfo(m.Result), TaskScheduler.FromCurrentSynchronizationContext());
            }
            var results = await Task.WhenAll(tasks);
        }

        private void ReportWebsiteInfo(HttpResultValue data)
        {
           // var count = int.Parse(label1.Text) - 1;
          //  label1.Text = count.ToString();
            textBox1.AppendText($"{data.IsSuccess} downloaded: {data.sId} charecters timed: {data.Timed}{Environment.NewLine}");
            
        }
    }
}
