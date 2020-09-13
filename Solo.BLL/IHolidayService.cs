using System;
using System.Collections.Generic;
using System.Text;

namespace Solo.BLL
{
    public interface IHolidayService
    {
        List<DateTime> GetWorkDayList(DateTime endDt, int dayCount);
    }
}
