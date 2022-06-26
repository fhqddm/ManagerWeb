using System;
using System.Collections.Generic;
using System.Text;

namespace Solo.Model.QueryModel
{
    public class FundQuery
    {
        public string FundNo { get; set; }
        public string FundName { get; set; }
        public Nullable<bool> HasAnalysis { get; set; }
        //public Nullable<float> MinBuyRate { get; set; }
        //public Nullable<float> MaxBuyRate { get; set; }
        public Nullable<float> MinTotalScale { get; set; }
        public Nullable<float> MaxTotalScale { get; set; }
        public Nullable<float> MaxOrgHoldRate { get; set; }
        public Nullable<float> MinOrgHoldRate { get; set; }
        public Nullable<float> MaxAvgPE { get; set; }
        public Nullable<float> MinAvgPE { get; set; }
        public Nullable<float> MaxAvgPB { get; set; }
        public Nullable<float> MinAvgPB { get; set; }
        public Nullable<float> MaxTop10Rate { get; set; }
        public Nullable<float> MinTop10Rate { get; set; }      
        public string MainIndustry { get; set; }
        public Nullable<float> MaxR1week { get; set; }
        public Nullable<float> MinR1week { get; set; }
        public Nullable<float> MaxR1month { get; set; }
        public Nullable<float> MinR1month { get; set; }
        public Nullable<float> MaxR3month { get; set; }
        public Nullable<float> MinR3month { get; set; }
        public Nullable<float> MaxR6month { get; set; }
        public Nullable<float> MinR6month { get; set; }
        public Nullable<float> MaxR1year { get; set; }
        public Nullable<float> MinR1year { get; set; }
        public Nullable<float> MaxR2year { get; set; }
        public Nullable<float> MinR2year { get; set; }
        public Nullable<float> MaxR3year { get; set; }
        public Nullable<float> MinR3year { get; set; }
        public Nullable<float> MaxR5year { get; set; }
        public Nullable<float> MinR5year { get; set; }
        public Nullable<float> MaxTotalFee { get; set; }
        public Nullable<float> MinTotalFee { get; set; }
        public Nullable<float> MaxOver7dFee { get; set; }
        public Nullable<float> MinOver7dFee { get; set; }
        public Nullable<float> MaxOver30dFee { get; set; }
        public Nullable<float> MinOver30dFee { get; set; }
        public Nullable<float> MaxOver1yFee { get; set; }
        public Nullable<float> MinOver1yFee { get; set; }
        public Nullable<float> MaxOver2yFee { get; set; }
        public Nullable<float> MinOver2yFee { get; set; }
        public Nullable<float> MaxStockRate { get; set; }
        public Nullable<float> MinStockRate { get; set; }
        public DateTime MaxDutyDate { get; set; }
        public DateTime MinDutyDate { get; set; }
        public Nullable<float> MaxR2_1y { get; set; }
        public Nullable<float> MinR2_1y { get; set; }
        public Nullable<float> MaxR3_2y { get; set; }
        public Nullable<float> MinR3_2y { get; set; }
        public Nullable<float> MaxR5_3y { get; set; }
        public Nullable<float> MinR5_3y { get; set; }
        public Nullable<int> MaxRank { get; set; }
        public Nullable<int> MinRank { get; set; }
        public int Status { get; set; }
        public bool isnew { get; set; }
        public int bondstock { get; set; } // 1.Bond 2.Stock 3.All
        public string Strategy { get; set; }
        public int UserId { get; set; }
        public bool resetRedis { get; set; }
    }

    public enum OWN { 
        Empty=1,
        OnlyHolds=2,
        OnlyWaits=3,
        OnlyHoldWaits=4,
        Holds=5,
        Waits=6,
        HoldWaits=7

    }
}
