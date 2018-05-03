using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.Util
{
    class CSVFileUtil
    {
        public static List<List<string>> readCSV(string filepath, char splitter)
        {
            List<List<string>> result = new List<List<string>>();
            StreamReader sr = new StreamReader(filepath, Encoding.UTF8);
            string strLine = "";
            string[] arrLine = null;

            while ((strLine = sr.ReadLine()) != null)
            {
                List<string> lineContent = new List<string>();                
                arrLine = strLine.Split(splitter);
                for (int i = 0; i < arrLine.Length; i++)
                {
                    lineContent.Add(arrLine[i]);
                }
                result.Add(lineContent);                
            }

            sr.Close();
            return result;
        }
    }
}
