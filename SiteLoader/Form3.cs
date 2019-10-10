using Serilog;
using Serilog.Formatting.Compact;
using SiteLoaderLib;
using SiteLoaderLib.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiteLoader
{
    public partial class Form3 : Form
    {
        SenarioManager manager;
        Stopwatch _watch;
        bool StopRunning = true;
        public Form3()
        {
            InitializeComponent();
            manager = new SenarioManager();
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();
            Log.Debug("Starting");
            txtReqCount.Text = "10";
        }

        private async void ParallelAsync_Click(object sender, EventArgs e)
        {
            StopRunning = false;
            Log.Debug("START");
            textBox1.Clear();
            _watch = Stopwatch.StartNew();
            int.TryParse(txtReqCount.Text, out int count);
            manager.AddUser("vladi", "Aa1111", count: count);
            //  await RunDownloadParallelAsync();
          var T =  await RunDownloadParallelAsyncTest();
            while (!StopRunning)
            {
                WaitNSeconds(1);
                lblTimeFromStart.Text = (_watch.ElapsedMilliseconds / 1000).ToString();
                var fCount = manager.GetFinishedSenarions();
                label1.Text = fCount.ToString();
                if(manager.GetFinishedSenarions() == manager.GetSenarios().Count)
                {
                    _watch.Stop();
                    var elspms = _watch.ElapsedMilliseconds / (decimal)1000;
                    textBox1.AppendText($"Total Execution time: {elspms}, requests: {txtReqCount.Text}, failed: {manager.GetFailedSenarions()}");
                    Log.Debug(($"Total Execution time: {elspms}, requests: {txtReqCount.Text}, failed: {manager.GetFailedSenarions()}"));
                    StopRunning = true;
                }
            }
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

        private async Task<HttpResultValue[]> RunDownloadParallelAsyncTest()
        {
            List<Task<HttpResultValue>> tasks = new List<Task<HttpResultValue>>();
            foreach (var item in manager.GetSenarios())
            {
                manager.LoginSenarion(item).ContinueWith((m) => ReportWebsiteInfo(m.Result), TaskScheduler.FromCurrentSynchronizationContext());
               // DownloadSiteAsync(site).ContinueWith((m) => ReportWebsiteInfo(m.Result), TaskScheduler.FromCurrentSynchronizationContext());
            }
            var results = await Task.WhenAll(tasks);
            return results;
           /* _watch.Stop();
            var elspms = _watch.ElapsedMilliseconds / (decimal)1000;
            textBox1.AppendText($"Total Execution time: {elspms}, requests: {txtReqCount.Text}, failed: {manager.GetFailedSenarions()}");
            Log.Debug(($"Total Execution time: {elspms}, requests: {txtReqCount.Text}, failed: {manager.GetFailedSenarions()}"));
            */
        }

        private void ReportWebsiteInfo(HttpResultValue data)
        {
            
            textBox1.AppendText($"{data.IsSuccess} downloaded: {data.sId} charecters timed: {data.Timed}{Environment.NewLine}");
            Log.Debug(($"{data.IsSuccess} downloaded: {data.sId} charecters timed: {data.Timed}"));
            /*if (manager.GetOpenSenarions() == 0)
            {
                _watch.Stop();
                var elspms = _watch.ElapsedMilliseconds/(decimal)1000;
                textBox1.AppendText($"Total Execution time: {elspms}, requests: {txtReqCount.Text}, failed: {manager.GetFailedSenarions()}");
                Log.Debug(($"Total Execution time: {elspms}, requests: {txtReqCount.Text}, failed: {manager.GetFailedSenarions()}"));
            }*/

        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            Log.Debug("CLEAR");
            manager.RemoveAll();
            textBox1.Clear();
            StopRunning = true;

        }

        private void WaitNSeconds(int seconds)
        {
            if (seconds < 1) return;
            DateTime _desired = DateTime.Now.AddSeconds(seconds);
            while (DateTime.Now < _desired)
            {
                Thread.Sleep(1);
                System.Windows.Forms.Application.DoEvents();
            }
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }
    }
}
