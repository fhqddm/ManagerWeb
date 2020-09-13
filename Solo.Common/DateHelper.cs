using System;
using System.Collections.Generic;
using System.Text;

namespace Solo.Common
{
    public class DateHelper
    {
        public static int getDayCountByTime(DateTime dt)
        {
            if (dt.Month == 1 || dt.Month == 3 || dt.Month == 5 || dt.Month == 7 || dt.Month == 8 || dt.Month == 10 || dt.Month == 12)
            {
                return 31;
            }
            else if (dt.Month == 2)
            {
                if (dt.Year % 4 == 0)
                {
                    return 29;
                }
                else
                {
                    return 28;
                }
            }
            else
            {
                return 30;
            }
        }

        public static DateTime getFormatDateTime(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
        }

        public static DateTime getFormatdpValue(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0);
        }

        public static DateTime getThisSunday(DateTime dt)
        {
            int weekNum = (int)dt.DayOfWeek;
            return dt.AddDays(-weekNum);
        }
    }
}
