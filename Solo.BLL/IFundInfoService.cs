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

        List<AnalysisView> GetAnalysisByQuery(AnalysisQuery analysisQuery);

        List<StrategyView> GetStrategyByQuery(StrategyQuery strategyQuery);

        StrategyTitle GetStrategyTitle();

        object GetProcessPercent();
    }
}
