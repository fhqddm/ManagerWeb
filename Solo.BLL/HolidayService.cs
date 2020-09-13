using Solo.Common;
using Solo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solo.BLL
{
    public class HolidayService : IHolidayService
    {
        public List<DateTime> GetWorkDayList(DateTime endDt,int dayCount)
        {
            List<Holiday> workList = new List<Holiday>();
            List<DateTime> listDate = new List<DateTime>();
            using (MyContext myContext = new MyContext())
            {

                workList = myContext.Holidays.Where(x => x.HolidayType == 0 && (x.Year<endDt.Year || 
                           x.Month<endDt.Month || x.Day<=endDt.Day)).OrderBy(a => a.Year).ThenBy(a => a.Month).ThenBy(a => a.Day)
                           .ToList(); 
            }
            List<DateTime> dtList = new List<DateTime>();
            int startIndex = 0;
            if (workList.Count-dayCount>0)
            {
                startIndex = workList.Count - dayCount;
            }

            for (int i= startIndex; i<workList.Count; i++)
            {
                dtList.Add(new DateTime(workList[i].Year, workList[i].Month, workList[i].Day, 0, 0, 0));
            }
            //if (dtList[dtList.Count-1] == endDt)
            //{

            //}
            return dtList;
            
        }
    }
}
