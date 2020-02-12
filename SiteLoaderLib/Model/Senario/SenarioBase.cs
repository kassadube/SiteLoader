using System;
using System.Collections.Generic;
using System.Text;

namespace SiteLoaderLib.Model
{
    public class SenarioBase
    {
        public bool Started { get; set; } = false;
        public bool finished { get; set; } = false;
        public string Token { get; set; }
        public HttpResultValue ResultValue { get; set; }
    }
   
}
