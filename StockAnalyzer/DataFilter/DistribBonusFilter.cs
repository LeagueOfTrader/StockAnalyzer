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
    public class DistribBonusFilter : StockFilter
    {
        protected string m_targetYear = "2018";
        protected string m_targetSeason = "1";
        private double m_ratio = 0.0;

        public DistribBonusFilter(string targetYear, string targetSeason, double ratio)
        {
            m_targetYear = targetYear;
            m_targetSeason = targetSeason;
            m_ratio = ratio;
        }

        public override bool filterMethod(string stockID)
        {
            double curVal = getLatestBonusRefValueInOneYear(stockID, m_targetYear, m_targetSeason);
            int maxYear = 0, maxQuarter = 0;
            double histVal = getBestBonusRefValueBefore(stockID, m_targetYear, m_targetSeason, out maxYear, out maxQuarter);

            if (histVal < double.Epsilon)
            {
                if (curVal > 0.0)
                {
                    return true;
                }
                else if(m_ratio < double.Epsilon)
                {
                    return true;
                }

                return false;
            }

            double chg = (curVal - histVal) / histVal;
            return chg > m_ratio;
        }

        public static double getBestBonusRefValueBefore(string stockID, string year, string season, out int maxYear, out int maxQuarter)
        {
            int endYear = int.Parse(year);
            maxYear = 0;// endYear;
            maxQuarter = 0;// int.Parse(season);
            double maxVal = 0.0;
            for (int i = m_startYear; i < endYear; i++)
            {
                string yr = i.ToString();
                for (int j = 1; j <= 4; j++)
                {
                    string qt = j.ToString();
                    double val = calcBonusRefValue(stockID, yr, qt);
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
                double val = calcBonusRefValue(stockID, year, qt);
                if (val > maxVal)
                {
                    maxVal = val;
                    maxYear = endYear;
                    maxQuarter = i;
                }
            }

            Logger.log("Best bonus distrib for " + stockID + " before " + year + "Q" + season + ": " + maxYear + "Q" + maxQuarter);

            return maxVal;
        }

        public static double calcBonusRefValue(string stockID, string year, string season)
        {
            double bonus = 0;
            if(getDistribBonus(stockID, year, season, out bonus))
            {
                List<StockKLine> mk = StockDataCenter.getInstance().getMonthKLine(stockID);
                if(mk == null)
                {
                    return 0.0;
                }
                string targetMonth = convertMonthBySeason(season);
                StockKLine kl = StockDataUtil.getMonthKLineByYearMonth(mk, year, targetMonth);

                return bonus / kl.latestPrice;
            }

            return 0.0;
        }

        public static bool getDistribBonus(string stockID, string year, string season, out double bonus)
        {
            bonus = 0.0;
            StockDistribData data = StockDBVisitor.getInstance().getDistribData(stockID, year, season);
            if(data.bonus == 0)
            {
                return false;
            }

            double factor = 1.0;
            if(data.deliver > 0)
            {
                factor *= ((double)data.deliver + 10.0) / 10.0;
            }
            else if(data.transfer > 0)
            {
                factor *= ((double)data.transfer + 10.0) / 10.0;
            }

            bonus = data.bonus * factor;
            return true;
        }

        public static double getLatestBonusRefValueInOneYear(string stockID, string year, string season)
        {
            int curYear = int.Parse(year);
            int curSeason = int.Parse(season);

            for(int i = 1; i <= curSeason; i++)
            {
                string qt = i.ToString();
                double ret = 0;
                if (getDistribBonus(stockID, year, qt, out ret))
                {
                    return ret;
                }
            }
            if (curSeason < 4)
            {
                int lastYear = curYear - 1;
                for (int i = 4; i > curSeason ; i++)
                {
                    string yr = lastYear.ToString();
                    string qt = i.ToString();
                    double ret = 0;
                    if(getDistribBonus(stockID, yr, qt, out ret))
                    {
                        return ret;
                    }
                }
            }

            return 0.0;
        }
    }
}
