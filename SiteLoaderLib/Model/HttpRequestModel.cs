using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SiteLoaderLib
{
    public class HttpRequestModel
    {
        public string Url { get; set; }
        public string Token { get; set; }
        public HttpContent Content { get; set; }
        public string ContentString { get; set; }
        //senarioId
        public int SId { get; set; }
        public int IId { get; set; }
    }
}
