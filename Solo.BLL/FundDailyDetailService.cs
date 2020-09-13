using Solo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solo.BLL
{
    public class FundDailyDetailService : IFundDailyDetailService
    {
        public int AddDailyDetail(FundDailyIDetail fundDailyIDetail)
        {
            using (MyContext myContext = new MyContext())
            {
                myContext.FundDailyIDetails.Add(fundDailyIDetail);
                return myContext.SaveChanges();
            }
        }

        public int GetFinishuUpdate(string FundNo)
        {
            using (MyContext myContext = new MyContext())
            {
                return myContext.FundDailyIDetails.Where(x => x.FundNo == FundNo && x.UnitValue > 0).ToList().Count;
            }
        }
    }
}
