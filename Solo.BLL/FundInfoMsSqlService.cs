using Microsoft.Extensions.Configuration;
using Solo.Common;
using Solo.Model;
using Solo.Model.QueryModel;
using Solo.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace Solo.BLL
{
    public class FundInfoMsSqlService : IFundInfoService
    {
        public int FundPositionUpdate(MasterPosition masterPosition)
        {
            throw new NotImplementedException();
        }

        public List<DailyRateView> GetDailyRate()
        {
            throw new NotImplementedException();
        }

        public List<FundView> GetFundInfosByQuery(FundQuery fundQuery)
        {
            throw new NotImplementedException();
        }

        public List<FundTip> GetFundNameTips(string fundstr)
        {
            throw new NotImplementedException();
        }

        public List<FundPositionView> GetFundPositionViews(int userId)
        {
            throw new NotImplementedException();
        }

        public List<FundPureTip> GetFundPureTips(string fundstr)
        {
            throw new NotImplementedException();
        }

        public object GetProcessPercent()
        {
            throw new NotImplementedException();
        }

        public List<StrategyView> GetStrategyByQuery(StrategyQuery strategyQuery)
        {
            throw new NotImplementedException();
        }

        public StrategyTitle GetStrategyTitle()
        {
            throw new NotImplementedException();
        }
    }
}
