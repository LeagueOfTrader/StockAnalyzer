using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.Util
{
    public class DateUtil
    {
        public static int compareDate(string date1, string date2) {
            if (!isDateValid(date1) || !isDateValid(date2)) {
                return 0;
            }

            int y1 = getDateYear(date1);
            int y2 = getDateYear(date2);
            if (y1 > y2){
                return 1;
            }
            else if (y1 < y2) {
                return -1;
            }

            int m1 = getDateMonth(date1);
            int m2 = getDateMonth(date2);
            if (m1 > m2)
            {
                return 1;
            }
            else if (m1 < m2)
            {
                return -1;
            }

            int d1 = getDateDay(date1);
            int d2 = getDateDay(date2);
            if (d1 > d2)
            {
                return 1;
            }
            else if (d1 < d2)
            {
                return -1;
            }

            return 0;
        }

        public static bool isDateValid(string date) {
            if (date.Length != 8) {
                return false;
            }

            return true;
        }

        public static int getDateYear(string date) {
            return getDateValue(date, 0, 4);
        }

        public static int getDateMonth(string date) {
            return getDateValue(date, 4, 2);
        }

        public static int getDateDay(string date) {
            return getDateValue(date, 6, 2);
        }

        private static int getDateValue(string date, int start, int len){
            string str = date.Substring(start, len);
            return int.Parse(str);
        }

        public static int getShortDateYear(string date)
        {
            int year = getDateValue(date, 0, 2);
            if(year > 90)
            {
                year = 1900 + year;
            }
            else
            {
                year = 2000 + year;
            }

            return year;
        }

        public static int getShortDateMonth(string date)
        {
            return getDateValue(date, 2, 2);
        }

        public static int getShortDateDay(string date)
        {
            return getDateValue(date, 4, 2);
        }

        public static bool matchYearMonth(string date, string year, string month)
        {
            int sy = getShortDateYear(date);
            int sm = getShortDateMonth(date);
            int ty = int.Parse(year);
            int tm = int.Parse(month);

            return (sy == ty && sm == tm);
        }

        public static bool matchDate(string srcDate, string targetDate)
        {
            int sy = getDateYear(srcDate);
            int sm = getDateMonth(srcDate);
            int sd = getDateDay(srcDate);
            int ty = 0;
            int tm = 0;
            int td = 0;
            if(targetDate.Length > 6)
            {
                ty = getDateYear(targetDate);
                tm = getDateMonth(targetDate);
                td = getDateDay(targetDate);
            }
            else
            {
                ty = getShortDateYear(targetDate);
                tm = getShortDateMonth(targetDate);
                td = getShortDateDay(targetDate);
            }

            return (sy == ty && sm == tm && sd == td);
        }

        public static void getNextQuarter(string srcYear, string srcQuarter, out string targetYear, out string targetQuarter)
        {
            if (srcQuarter == "4")
            {
                int ty = int.Parse(srcYear) + 1;
                targetYear = ty.ToString();
                targetQuarter = "1";
                return;
            }

            int tq = int.Parse(srcQuarter) + 1;
            targetYear = srcYear;
            targetQuarter = tq.ToString();
        }

        public static void getPrevQuarter(string srcYear, string srcQuarter, out string targetYear, out string targetQuarter)
        {
            if (srcQuarter == "1")
            {
                int ty = int.Parse(srcYear) - 1;
                targetYear = ty.ToString();
                targetQuarter = "4";
                return;
            }

            int tq = int.Parse(srcQuarter) - 1;
            targetYear = srcYear;
            targetQuarter = tq.ToString();
        }

    }
}
