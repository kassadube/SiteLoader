using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SiteLoaderLib
{
    public static class HttpHelper
    {
        public static async Task<HttpResultValue> GetTokenResult(this HttpHandler handler, string url, string userName, string password, int langId = 1)
        {
            HttpResultValue value = new HttpResultValue();            
            try
            {               
                HttpContent content = new StringContent($"{{\"username\":\"{userName}\",\"password\":\"{password}\",\"langId\":{langId}}}", Encoding.UTF8, "application/json");               
                var res = await handler.PostResult(url, null, content);
                JObject obj = JObject.Parse(res.Value);
                value.Value = obj.First.First.Value<string>();
                value.Timed = res.Timed;
                value.HttpCode = res.HttpCode;
                value.StartTime = res.StartTime;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                value.Ex = e;
            }
            return value;
        }

        public static Task<HttpResultValue> GetTokenResultas(this HttpHandler handler, string url, string userName, string password, int langId = 1)
        {
           
            
            HttpContent content = new StringContent($"{{\"username\":\"{userName}\",\"password\":\"{password}\",\"langId\":{langId}}}", Encoding.UTF8, "application/json");
            var res = handler.PostResultas(url, null, content);
            return res;
               
            
        }
    }
}

