using Solo.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solo.BLL
{
    public interface IFundDailyDetailService
    {
        int AddDailyDetail(FundDailyIDetail fundDailyIDetail);

        int GetFinishuUpdate(string FundNo);
    }
}
