using StockAnalyzer.DataSource.TushareData;
using StockAnalyzer.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    class ForecastFilter : StockFilter
    {
        protected string m_targetYear = "2018";
        protected string m_targetSeason = "1";
        protected StockPerformanceForecastType m_forecastType = StockPerformanceForecastType.PFT_Profit;

        public ForecastFilter(string year, string season, StockPerformanceForecastType type)
        {
            m_targetYear = year;
            m_targetSeason = season;
            m_forecastType = type;
        }

        public override bool filterMethod(string stockID)
        {
            StockPerformanceForecastType pft = StockPerformanceForecastType.PFT_Alert;
            if(getStockForecastType(stockID, m_targetYear, m_targetSeason, out pft))
            {
                if(pft >= m_forecastType)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool getStockForecastType(string stockID, string year, string season, out StockPerformanceForecastType type)
        {
            string ty = null;
            string tq = null;
            DateUtil.getNextQuarter(year, season, out ty, out tq);

            type = StockPerformanceForecastType.PFT_Alert;

            StockForecastData data = StockDBVisitor.getInstance().getStockForecastData(stockID, ty, tq);
            if(data == null)
            {
                return false;
            }

            type = data.type;
            return true;
        }
    }
}
