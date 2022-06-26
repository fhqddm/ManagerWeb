using System;
using System.Collections.Generic;
using System.Text;

namespace Solo.BLL
{
    public interface IHolidayService
    {
        List<DateTime> GetWorkDayList(DateTime endDt, int dayCount);

        bool IsHoliday(DateTime dt, bool resetRedis = false);
    }
}
