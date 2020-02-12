using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SiteLoaderLib
{
    public class HttpManager
    {       

       
        public async Task<HttpResultValue> GetResult(string url, string token = null)
        {
            HttpClient Client = new HttpClient();
            HttpResultValue value = new HttpResultValue();
            if (!string.IsNullOrEmpty(token))
                Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            Stopwatch stopWatch = new Stopwatch();
            try
            {
                stopWatch.Start();
                HttpResponseMessage response = await Client.GetAsync(url);
                value.HttpCode = (int)response.StatusCode;                
                value.Value = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                value.Ex = e;
            }
            finally
            {
                stopWatch.Stop();
                value.Timed = stopWatch.ElapsedMilliseconds;
            }           
            return value;
        }

        public Task GetResultas(string url, string token = null)
        {
            HttpClient Client = new HttpClient();
            HttpResultValue value = new HttpResultValue();
            if (!string.IsNullOrEmpty(token))
                Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
             try
            {
               
                value.ResTask  =  Client.GetAsync(url);
            }
            catch (HttpRequestException e)
            {

                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                value.Ex = e;
            }
           
            return Task.FromResult(value);
        }

        public async Task<HttpResultValue> PostResult(string url, string token, string contentText = "")
        {
            HttpContent content = new StringContent(contentText, Encoding.UTF8, "application/json");
            return await PostResult(url, token, content);
        }
        public async Task<HttpResultValue> PostResult(string url, string token, HttpContent content = null)
        {
            HttpClient Client = new HttpClient();
            HttpResultValue value = new HttpResultValue();           
            if (!string.IsNullOrEmpty(token))
                Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            Stopwatch stopWatch = new Stopwatch();
            try
            {                
                stopWatch.Start();
                HttpResponseMessage response = await Client.PostAsync(url, content).ConfigureAwait(false);
                value.HttpCode = (int)response.StatusCode;
                value.Value = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                value.Ex = e;
            }
            finally
            {
                stopWatch.Stop();
                value.Timed = stopWatch.ElapsedMilliseconds;
            }
            return value;
        }

        public Task<HttpResultValue> PostResultas(string url, string token, HttpContent content = null)
        {
            HttpClient Client = new HttpClient();
            HttpResultValue value = new HttpResultValue();
            if (!string.IsNullOrEmpty(token))
                Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            try
            {
                value.ResTask = Client.PostAsync(url, content);                
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                value.Ex = e;
            }            
            return Task.FromResult(value);
        }

        public async Task<HttpResultValue> PostResult(HttpRequestModel request)
        {
            HttpResultValue res = new HttpResultValue();
            var watch = Stopwatch.StartNew();
            try
            {
                using (HttpClient Client = new HttpClient())
                {
                    Client.Timeout = TimeSpan.FromMinutes(100);
                    if (!string.IsNullOrEmpty(request.Token))
                        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", request.Token);
                    //HttpResponseMessage response = await Client.PostAsync(request.Url, request.Content).ConfigureAwait(false);
                    HttpResponseMessage response = await Client.PostAsync(request.Url, new StringContent(request.ContentString, Encoding.UTF8, "application/json"));
                    res.HttpCode = System.Convert.ToInt32( Enum.Parse(typeof(System.Net.HttpStatusCode), response.StatusCode.ToString()));
                    res.Value = await response.Content.ReadAsStringAsync();
                   // Log.Debug($"success url {request.Url} sid {request.SId}");
                }
            }
            catch (Exception ex)
            {
                res.Ex = ex;
                Log.Error(ex,$"URL {request.Url}");
            }
            finally
            {               
                watch.Stop();
                res.Timed = watch.ElapsedMilliseconds;
                res.sId = request.SId;
                res.Url = request.Url;
            }
            return res;
        }

        public async Task<HttpResultValue> GetResult(HttpRequestModel request)
        {
            HttpResultValue res = new HttpResultValue();
            var watch = Stopwatch.StartNew();
            try
            {
                using (HttpClient Client = new HttpClient())
                {
                    
                    Client.Timeout = TimeSpan.FromMinutes(100);
                    if (!string.IsNullOrEmpty(request.Token))
                        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", request.Token);
                    HttpResponseMessage response = await Client.GetAsync(request.Url);
                   
                    res.HttpCode = System.Convert.ToInt32(Enum.Parse(typeof(System.Net.HttpStatusCode), response.StatusCode.ToString()));
                    res.Value = await response.Content.ReadAsStringAsync();
                    //Log.Debug($"success url {request.Url} sid {request.SId}");
                }
            }
            catch (Exception ex)
            {
                res.Ex = ex;
                Log.Error(ex, $"URL {request.Url}");
            }
            finally
            {
                watch.Stop();
                res.Timed = watch.ElapsedMilliseconds;
                res.sId = request.SId;
                res.Url = request.Url;

            }
            return res;
        }

    }
}
