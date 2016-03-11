using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CogniStudyWeb
{
    public class DateUtils 
    {
        public static int getDayBlockNum(DateTime date) {
            int day = date.DayOfYear; //0 - 364 (or 365 in leap year)
            int theYear = date.Year;
            for(int year = theYear; year > 2016; year--) {
                day += (year % 4 == 1) ? 366 : 365;
            }
            return day;
        }

        public static int getCurrentDayBlockNum() {
            return getDayBlockNum(DateTime.Today);
        }

        public static int getTridayBlockNum(DateTime date) {
            return getDayBlockNum(date) / 3;
        }

        public static int getCurrentTridayBlockNum() {
            return getTridayBlockNum(DateTime.Today);
        }

        public static int getMonthBlockNum(DateTime date)
        {
            int month = date.Month - 1; //0 - 11
            int yearsSince2016 = date.Year - 2016;
            return month + yearsSince2016*12;
        }

        public static int getCurrentMonthBlockNum() {
            return getMonthBlockNum(DateTime.Today);
        }

        public enum BlockType {
            DAY, TRIDAY, MONTH
        }

        private static String blockNumsToString(int[] blockNums) {
            return "Day: " + blockNums[0] + " | Triday: " + blockNums[1] + " | Month: " + blockNums[2];
        }
    }
}