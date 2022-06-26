using Solo.Model;
using Solo.Model.QueryModel;
using Solo.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solo.BLL
{
    public interface IFundInfoService
    {
        List<FundView> GetFundInfosByQuery(FundQuery fundQuery);

        List<StrategyView> GetStrategyByQuery(StrategyQuery strategyQuery);

        StrategyTitle GetStrategyTitle();

        List<FundTip> GetFundNameTips(string fundstr);
        List<FundPureTip> GetFundPureTips(string fundstr);

        List<FundPositionView> GetFundPositionViews(int userId);

        int FundPositionUpdate(MasterPosition masterPosition);
   
        object GetProcessPercent();
        List<DailyRateView> GetDailyRate();
    }
}
