using FluentAssertions;
using SiteLoaderLib;
using SiteLoaderLib.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace XSiteLoaderTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            SenarioManager manager = new SenarioManager();
            manager.AddUser("vladi", "Aa1111");
            List<SenarioInfo> sList = manager.GetSenarios();
            sList.Count.Should().Be(1);

        }
        [Fact]
        public void Add5Senarios_TEST()
        {
            SenarioManager manager = new SenarioManager();
            manager.AddUser("vladi", "Aa1111",count:5);
            List<SenarioInfo> sList = manager.GetSenarios();
            sList.Count.Should().Be(5);
            for (int i = 0; i < 5; i++)
            {
                sList[i].Id.Should().Be(i + 1);
            }

        }

        [Fact]
        public void GetSenarionRequestMode_TEST()
        {
            SenarioManager manager = new SenarioManager();
            manager.AddUser("vladi", "Aa1111");
            List<SenarioInfo> sList = manager.GetSenarios();
            HttpRequestModel m = sList[0].GetLoginRequest();
            m.ContentString.Should().Be("{\"username\":\"vladi\",\"password\":\"Aa1111\",\"langId\":1}");
        }

        [Fact]
        public void DoLogin_TEST()
        {
            SenarioManager manager = new SenarioManager();
            manager.AddUser("vladi", "Aa1111",count:5);
           Task t =  Task.Run(() => manager.DoLogin());
            t.Wait();
        }
        [Fact]
        public void CreateLoginTask_TEST()
        {
            SenarioManager manager = new SenarioManager();
            manager.AddUser("vladi", "Aa1111");
            Task t = manager.GetSenarios()[0].CreateLoginTask(new HttpManager());
            t.Start(TaskScheduler.Default);
            Task.WaitAll(t);
            t.Should().NotBeNull();
        }
    }
}
