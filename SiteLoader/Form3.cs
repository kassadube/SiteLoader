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
        bool startPeriodic = false;
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
            manager.SetApiUrl(txtApiUrl.Text);
           var T =  await Start_async();
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

        public async Task<HttpResultValue[]> Start_async()
        {
            int.TryParse(txtReqCount.Text, out int count);
            int perUser = count / 3;
            manager.AddUser("vladi", "Aa1111", count: perUser + (count - (perUser*4)));
            manager.AddUser("vita", "Aa111111", count: perUser);
            manager.AddUser("daniel", "Aa1111", count: perUser);
            manager.AddUser("pisr1", "Aa111111", count: perUser);
            //  await RunDownloadParallelAsync();
            var t = await RunDownloadParallelAsyncTest();
            return t;
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            startPeriodic = true;
            manager.SetApiUrl(txtApiUrl.Text);
            int.TryParse(txtRedoEvery.Text, out int count);
            while (startPeriodic)
            {

                var t = await Start_async();
                WaitNSeconds(count);
                SenarioSummery s = GetSummery();
                txtSummary.Text = $"ALL = {s.All} Failed = {s.Failed} succeed = {s.Succeeded} Average = {s.Average} InQueue = {s.InQueue}";

            }
        }

        private async Task RunDownloadParallelAsync()
        {
            manager.SetApiUrl(txtApiUrl.Text);
            var tasks =  manager.DoLogin();            
            var results = await Task.WhenAll(tasks);
            foreach (var item in results)
            {
                ReportWebsiteInfo(item, null);
            }
        }



        private async Task<HttpResultValue[]> RunDownloadParallelAsyncTest()
        {
            List<Task<HttpResultValue>> tasks = new List<Task<HttpResultValue>>();
            var s = manager.GetSenarios(true);
            foreach (var item in s)
            {
                var t = manager.LoginSenarion(item);
                t.ContinueWith((m) => ReportWebsiteInfo(m.Result, item), TaskScheduler.FromCurrentSynchronizationContext());
                for (int i = 0; i < item.ItemsCount; i++)
                {
                    t.ContinueWith((m) => 
                    manager.RunSenarioItems(item)
                    .ContinueWith((r) => ReportWebsiteInfo(r.Result, item), TaskScheduler.FromCurrentSynchronizationContext())
                    ,
                    TaskScheduler.FromCurrentSynchronizationContext());
                }
                /*
                    .ContinueWith((m) => ReportWebsiteInfo( m.Result,item), TaskScheduler.FromCurrentSynchronizationContext())
                    .ContinueWith((m) => manager.RunSenarioItems(item), TaskScheduler.FromCurrentSynchronizationContext())
                    .ContinueWith((m) => manager.RunSenarioItems(item), TaskScheduler.FromCurrentSynchronizationContext())
                    .ContinueWith((m) => manager.RunSenarioItems(item), TaskScheduler.FromCurrentSynchronizationContext())
                    .ContinueWith((m) => manager.RunSenarioItems(item), TaskScheduler.FromCurrentSynchronizationContext())
                    .ContinueWith((m) => manager.RunSenarioItems(item), TaskScheduler.FromCurrentSynchronizationContext());
                    */
                tasks.Add(t);

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
        private void DoSenario(object obj, SenarioInfo s)
        {
            manager.RunSenarioItems(s);
        }
        private void ReportWebsiteInfo(HttpResultValue data, SenarioInfo s)
        {
            
            textBox1.AppendText($"{data.IsSuccess} downloaded: {data.sId} sid timed: {data.Timed} url: {data.Url}{Environment.NewLine}");
            Log.Debug(($"{data.IsSuccess} downloaded: {data.sId} sid timed: {data.Timed}  url: {data.Url}" ));           

        }

        private  SenarioSummery GetSummery()
        {
            SenarioSummery s = new SenarioSummery()
            {
                All = manager.GetRequestCount(), //manager.GetSenarios().Count,
                Failed = manager.GetFailedSenarions()

            };
            s.Succeeded = s.All - s.Failed;
            s.Average = manager.GetAvarage();// manager.GetSenarios().Where(x => x.User.finished && x.User.ResultValue != null).Average(x => x.User.ResultValue.Timed);
            s.InQueue = manager.GetInQueue();
            return s;
        }
        private void BtnClear_Click(object sender, EventArgs e)
        {
            Log.Debug("CLEAR");
            manager.RemoveAll();
            textBox1.Clear();
            
            StopRunning = true;

        }

        private void BtnStopPeriodic_Click(object sender, EventArgs e)
        {
            Log.Debug("STOP PERIODIC");
            startPeriodic = false;
            SenarioSummery s  = GetSummery();
            txtSummary.Text = $"ALL = {s.All} Failed = {s.Failed} succeed = {s.Succeeded} Average = {s.Average} InQueue = {s.InQueue}";

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

        private void txtApiUrl_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
