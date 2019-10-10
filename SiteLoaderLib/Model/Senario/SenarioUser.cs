using System;
using System.Collections.Generic;
using System.Text;

namespace SiteLoaderLib.Model
{
    public class SenarioUserLogin
    {
        public bool Started { get; set; } = false;
        public bool finished { get; set; } = false;
        public string UserName { get; set; }
        public string Password { get; set; }
        public int LangId { get; set; }
        public string Token { get; set; }

        public HttpResultValue ResultValue { get; set; }
    }
   
}
