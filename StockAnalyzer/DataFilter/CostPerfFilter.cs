﻿using StockAnalyzer.Assist;
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
    class CostPerfFilter : StockFilter
    {
        protected string m_targetYear = "2018";
        protected string m_targetSeason = "1";
        protected double m_ratio = 0.0;

        protected const int m_startYear = 2007;

        public CostPerfFilter(string targetYear, string targetSeason, double ratio)
        {
            m_targetYear = targetYear;
            m_targetSeason = targetSeason;
            m_ratio = ratio;
        }

        public override bool filterMethod(string stockID)
        {
            double srcVal = getSrcValue(stockID);
            double targetVal = calcCurCostRefValue(stockID, m_targetYear, m_targetSeason);

            if(srcVal < double.Epsilon)
            {
                return false;
            }

            double chg = (targetVal - srcVal) / srcVal;
            if(chg > m_ratio)
            {
                return true;
            }

            return false;
        }

        protected virtual double getSrcValue(string stockID)
        {
            double srcVal = getMaxCostRefValueBefore(stockID, m_targetYear, m_targetSeason);
            return srcVal;
        }

        public static double getMaxCostRefValueBefore(string stockID, string year, string season)
        {
            int endYear = int.Parse(year);
            int maxYear = 0;// endYear;
            int maxQuarter = 0;// int.Parse(season);
            double maxVal = 0.0;
            for(int i = m_startYear; i < endYear; i++)
            {
                string yr = i.ToString();
                for(int j = 1; j <= 4; j++)
                {
                    string qt = j.ToString();
                    double val = calcCostRefValue(stockID, yr, qt);
                    if(val > maxVal)
                    {
                        maxVal = val;
                        maxYear = i;
                        maxQuarter = j;
                    }
                }
            }

            int endQuarter = int.Parse(season);
            for(int i = 1; i < endQuarter; i++)
            {
                string qt = i.ToString();
                double val = calcCostRefValue(stockID, year, qt);
                if (val > maxVal)
                {
                    maxVal = val;
                    maxYear = endYear;
                    maxQuarter = i;
                }
            }

            Logger.log("Best forecast annual cost for " + stockID + " before " + year + "Q" + season + ": " + maxYear + "Q" + maxQuarter);

            return maxVal;
        }

        public static double calcCostRefValue(string stockID, string year, string season)
        {
            double costRefVal = 0.0;
            StockReportData rd = StockDBVisitor.getInstance().getStockReportData(stockID, year, season);
            
            string str = StockDataCollector.queryMonthlyKLineData(stockID);
            List<StockKLine> mk = StockDataConvertor.parseKLineArray(str);
            string targetMonth = convertMonthBySeason(season);
            StockKLine kl = StockDataUtil.getMonthKLineByYearMonth(mk, year, targetMonth);
            int quarter = int.Parse(season);
            if(rd != null && kl != null)
            {
                costRefVal = (rd.eps / kl.latestPrice) / quarter * 4;
            }

            return costRefVal;
        }

        public static double calcCurCostRefValue(string stockID, string year, string season)
        {
            double costRefVal = 0.0;
            StockReportData rd = StockDBVisitor.getInstance().getStockReportData(stockID, year, season);

            string str = StockDataCollector.queryMarketData(stockID);
            StockMarketData md = StockDataConvertor.parseMarketData(str);
            int quarter = int.Parse(season);
            if (rd != null && md != null)
            {
                costRefVal = (rd.eps / md.latestPrice) / quarter * 4;
            }

            return costRefVal;
        }

        protected static string convertMonthBySeason(string season)
        {
            string month = "12";
            if(season == "1")
            {
                month = "3";
            }
            else if(season == "2")
            {
                month = "6";
            }
            else if(season == "3")
            {
                month = "9";
            }

            return month;
        }
    }
}
