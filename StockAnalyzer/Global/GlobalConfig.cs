using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StockAnalyzer.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.Global
{
    class GlobalConfig : Singleton<GlobalConfig>
    {
        public string curYear = "2018";
        public string curMonth = "5";
        public string curQuarter = "1";
        public string defaultRefYear = "2013";

        public void init()
        {
            string globalCfgFile = "Data/GlobalConfig.txt";
            StreamReader sr = new StreamReader(globalCfgFile);
            string content = sr.ReadToEnd();
            if(content != null)
            {
                JObject jo = (JObject)JsonConvert.DeserializeObject(content);
                curYear = jo["year"].ToString();
                curMonth = jo["month"].ToString();
                curQuarter = jo["quarter"].ToString();
                defaultRefYear = jo["defaultRefYear"].ToString();
            }
        }
    }
}
