using SiteLoaderLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace SiteLoaderConsoleApp
{
    class Program
    {
        const string URL = "https://pointerqa-web02.pointerbi.com/fleetcore.api";
        const string URL_FLEET = "https://pointerqa-web02.pointerbi.com/fleet";

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

        static HttpHandler handler;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            handler = new HttpHandler();
            Console.WriteLine($"Before {DateTime.Now}");

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            List <Task<HttpResultValue>>  list = GetTokens(1000);
            stopWatch.Stop();
            Console.WriteLine($"after {DateTime.Now} -- {stopWatch.ElapsedMilliseconds}");
            stopWatch.Restart();
            foreach (var item in list)
            {
                HttpResultValue t =  item.GetAwaiter().GetResult();                
               // PrintResult(t, "TOKEN");
            }
            stopWatch.Stop();
            Console.WriteLine($"after {DateTime.Now} -- {stopWatch.ElapsedMilliseconds}");
            //Task.WaitAll()
            Console.ReadKey();
        }

        static List<Task<HttpResultValue>> GetTokens(int count = 10)
        {
            List<Task<HttpResultValue>> list = new List<Task<HttpResultValue>>();
            for (int i = 0; i < count; i++)
            {
                list.Add(GetToken("vladi", "Aa1111"));
                list.Add(GetToken("vita", "Aa1111"));
                list.Add(GetToken("daniel", "Aa1111"));
            }
            
           
            return list;
        }
        static Task<HttpResultValue> GetToken(string userName, string password)
        {
            Task<HttpResultValue> t = handler.GetTokenResult($"{URL}/token", userName, password);
            return t;
        }

        static void PrintResult(HttpResultValue res, string callName)
        {
            Console.WriteLine($"{callName} {DateTime.Now} status={res.HttpCode} contentLength={res.Value.Length} start={res.StartTime.TimeOfDay} sec={res.Timed}");
        }
    }
}
