using System;
using System.Collections.Generic;
using System.Text;

namespace SiteLoaderLib.Model
{
    public class SenarioUserLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int LangId { get; set; }
        public string Token { get; set; }
    }
   
}
