using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SiteLoaderLib
{
    public class HttpHandler
    {
        

        public HttpHandler()
        {
            Client = new HttpClient();
        }

        public HttpClient Client { get; set; }
        public  async Task<HttpResultValue> GetResult(string url, string token = null)
        {
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

        public  Task<HttpResultValue> PostResultas(string url, string token, HttpContent content = null)
        {
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

    }
}
