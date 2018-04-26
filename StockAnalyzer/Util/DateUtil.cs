using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.Util
{
    class DateUtil
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
    }
}
