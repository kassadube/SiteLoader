using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SiteLoaderLib
{
    public class HttpResultValue
    {
        public HttpResultValue()
        {
            StartTime = DateTime.Now;
        }
        public int HttpCode { get; set; }
        public string Value { get; set; }
        public Exception Ex { get; set; }
        public long Timed { get; set; }
        public bool IsSuccess { get { return HttpCode == 200; } }
        public DateTime StartTime { get; set; }
        public Task<HttpResponseMessage>  ResTask { get; set; }
    }
}
