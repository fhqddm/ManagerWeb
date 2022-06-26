using System;
using System.Collections.Generic;
using System.Text;

namespace Solo.Model.QueryModel
{
    public class AnalysisQuery
    {
        public string FundNo { get; set; }
        public string FundName { get; set; }
        public Nullable<float> MaxStockRate { get; set; }
        public Nullable<float> MinStockRate { get; set; }
        public Nullable<float> MaxTotalFee { get; set; }
        public Nullable<float> MinTotalFee { get; set; }
        public Nullable<float> MaxR1week { get; set; }
        public Nullable<float> MinR1week { get; set; }
        public Nullable<float> MaxR1m_1w { get; set; }
        public Nullable<float> MinR1m_1w { get; set; }
        public Nullable<float> MaxR3_1m { get; set; }
        public Nullable<float> MinR3_1m { get; set; }
        public Nullable<float> MaxR6_3m { get; set; }
        public Nullable<float> MinR6_3m { get; set; }
        public Nullable<float> MaxR12_6m { get; set; }
        public Nullable<float> MinR12_6m { get; set; }
        public Nullable<float> MaxR1year { get; set; }
        public Nullable<float> MinR1year { get; set; }
        public Nullable<float> MaxR2_1y { get; set; }
        public Nullable<float> MinR2_1y { get; set; }
        public Nullable<float> MaxR3_2y { get; set; }
        public Nullable<float> MinR3_2y { get; set; }
        public Nullable<float> MaxR5_3y { get; set; }
        public Nullable<float> MinR5_3y { get; set; }
        public Nullable<float> MaxD_Top { get; set; }
        public Nullable<float> MinD_Top { get; set; }
        public Nullable<float> MinD_Exp1 { get; set; }
        public Nullable<float> MaxD_Exp1 { get; set; }
        public Nullable<float> MinD_Exp2 { get; set; }
        public Nullable<float> MaxD_Exp2 { get; set; }
        public Nullable<float> MinD_Exp3 { get; set; }
        public Nullable<float> MaxD_Exp3 { get; set; }
        public Nullable<float> MinLinear5 { get; set; }
        public Nullable<float> MaxLinear5 { get; set; }
        public Nullable<int> MaxRank_Id { get; set; }
        public Nullable<int> MinRank_Id { get; set; }
        public Nullable<int> MaxBear_Id { get; set; }
        public Nullable<int> MinBear_Id { get; set; }
        public Nullable<int> MaxBestReturn_Id { get; set; }
        public Nullable<int> MinBestReturn_Id { get; set; }
        public Nullable<int> MaxFundScore_Id { get; set; }
        public Nullable<int> MinFundScore_Id { get; set; }
        public Nullable<int> MaxManagerScore_Id { get; set; }
        public Nullable<int> MinManagerScore_Id { get; set; }
        public int status { get; set; } //
        public OWN own { get; set; }
        public bool isnew { get; set; }
        public int bondstock { get; set; } // 1.Bond 2.Stock 3.All
        

    }
}
