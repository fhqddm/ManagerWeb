using System;
using System.Collections.Generic;
using System.Text;

namespace Solo.Model.ViewModel
{
    public class StrategyView
    {
        public string FundNo { get; set; }
        public string FundName { get; set; }
        public string Strategy { get; set; }
        public Nullable<double> Investment { get; set; }
        public int CurrentHold { get; set; }
        public Nullable<double> TotalScale { get; set; }
        public Nullable<double> ReturnRate { get; set; }
        public Nullable<double> HoldRate { get; set; }
        public Nullable<float> R1day { get; set; }
        public Nullable<float> R1week { get; set; }
        public double R1month { get; set; }
        public Nullable<double> D_ExpD10 { get; set; }
        public Nullable<double> D_ExpD5 { get; set; }
        public Nullable<double> D_ExpM1 { get; set; }
        public Nullable<double> D_ExpM2 { get; set; }
        public Nullable<double> D_Exp1 { get; set; }
        public Nullable<double> D_Exp2 { get; set; }
        public Nullable<double> D_Exp3 { get; set; }
        public Nullable<double> D_Top { get; set; }
        public Nullable<double> D_A60 { get; set; }
        public Nullable<double> D_Max10D { get; set; }
        public Nullable<double> D_Max5D { get; set; }
        public Nullable<float> FundScore { get; set; }
        public Nullable<float> ManagerScore { get; set; }
        public double Maxretra { get; set; }
        public double Maxretra5 { get; set; }
        public DateTime DutyDate { get; set; }
        public Nullable<int> Rank_Id{get;set;}
        public double Estimate { get; set; }

        public double ValuationScore { get; set; }
        public double  Recommend { get; set; }

        public double LinearM1 { get; set; }
        public double LinearM2 { get; set; }
        public DateTime FundUpdateTime { get; set; }

        public int RecentAddMoney { get; set; }

        public double StockRate { get; set; }
        public double UnitValue { get; set; }
        public double Cost { get; set; }
        public double TotalBonus { get; set; }
        public double HoldShares { get; set; }

        public int status { get; set; }
        public string StockNos { get; set; }
        public string StockNames { get; set; }
        public string StockPositions { get; set; }
    }
}
