using StockAnalyzer.Assist;
using StockAnalyzer.DataModel;
using StockAnalyzer.DataSource;
using StockAnalyzer.DataSource.TushareData;
using StockAnalyzer.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    class QuarterCostPerfFilter : CostPerfFilter
    {
        public QuarterCostPerfFilter(string targetYear, string targetSeason, double ratio) : base(targetYear, targetSeason, ratio)
        {
        }

        protected override double getSrcValue(string stockID)
        {
            double srcVal = getMaxQuarterCostRefValueBefore(stockID, m_targetYear, m_targetSeason);
            return srcVal;
        }

        public static double getMaxQuarterCostRefValueBefore(string stockID, string year, string season)
        {
            int endYear = int.Parse(year);
            int maxYear = 0;// endYear;
            int maxQuarter = 0;// int.Parse(season);
            double maxVal = 0.0;
            for (int i = m_startYear; i < endYear; i++)
            {
                string yr = i.ToString();
                for (int j = 1; j <= 4; j++)
                {
                    string qt = j.ToString();
                    double val = calcCostRefValueForQuarter(stockID, yr, qt);
                    if (val > maxVal)
                    {
                        maxVal = val;
                        maxYear = i;
                        maxQuarter = j;
                    }
                }
            }

            int endQuarter = int.Parse(season);
            for (int i = 1; i < endQuarter; i++)
            {
                string qt = i.ToString();
                double val = calcCostRefValueForQuarter(stockID, year, qt);
                if (val > maxVal)
                {
                    maxVal = val;
                    maxYear = endYear;
                    maxQuarter = i;
                }
            }

            Logger.log("Best single quarter cost for " + stockID + " before " + year + "Q" + season + ": " + maxYear + "Q" + maxQuarter);
            return maxVal;
        }

        public static double calcCostRefValueForQuarter(string stockID, string year, string season)
        {
            double costRefVal = 0.0;
            StockReportData rd = StockDBVisitor.getInstance().getStockReportData(stockID, year, season);
            int qt = int.Parse(season);
            
            if (rd == null)
            {
                return 0.0;
            }

            double eps = rd.eps;
            if (qt > 1)
            {
                int prevQuarter = qt - 1;
                StockReportData prevRd = StockDBVisitor.getInstance().getStockReportData(stockID, year, prevQuarter.ToString());
                if (prevRd != null)
                {
                    eps -= prevRd.eps;
                }
            }

            string str = StockDataCollector.queryMonthlyKLineData(stockID);
            List<StockKLine> mk = StockDataConvertor.parseKLineArray(str);
            string targetMonth = convertMonthBySeason(season);
            StockKLine kl = StockDataUtil.getMonthKLineByYearMonth(mk, year, targetMonth);
            int quarter = int.Parse(season);
            if (kl != null)
            {
                costRefVal = eps / kl.latestPrice;
            }

            return costRefVal;
        }
    }
}
