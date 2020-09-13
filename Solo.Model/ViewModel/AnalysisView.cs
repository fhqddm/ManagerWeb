using System;
using System.Collections.Generic;
using System.Text;

namespace Solo.Model.ViewModel
{
    public class AnalysisView
    {
        public string FundNo { get; set; }
        public string FundName { get; set; }
        public Nullable<float> StockRate { get; set; }
        public float TotalFee { get; set; }
        public Nullable<float> R1day { get; set; }
        public Nullable<float> R1week { get; set; }
        public Nullable<float> R1month { get; set; }
        public Nullable<float> R1m_1w { get; set; }
        public Nullable<float> R3_1m { get; set; }
        public Nullable<float> R6_3m { get; set; }
        public Nullable<float> R12_6m { get; set; }
        public Nullable<float> R1year { get; set; }
        public Nullable<float> R2_1y { get; set; }
        public Nullable<float> R3_2y { get; set; }
        public Nullable<float> R5_3y { get; set; }
        public Nullable<double> D_Top { get; set; }
        public Nullable<double> D_ExpM2 { get; set; }
        public Nullable<double> D_ExpM1 { get; set; }
        public Nullable<double> D_ExpD10 { get; set; }
        public Nullable<double> D_ExpD5 { get; set; }
        public Nullable<double> D_Exp1 { get; set; }
        public Nullable<double> D_Exp2 { get; set; }
        public Nullable<double> D_Exp3 { get; set; }
        public Nullable<double> Linear1 { get; set; }
        public Nullable<double> Linear2 { get; set; }
        public Nullable<double> Linear3 { get; set; }
        public Nullable<double> Linear5 { get; set; }
        public Nullable<int> Bear_Id { get; set; }
        public Nullable<int> Rank_Id { get; set; }
        public Nullable<int> ManagerScore_Id { get; set; }
        public Nullable<int> FundScore_Id { get; set; }
        public Nullable<int> BestReturn_Id { get; set; }     
        public Nullable<double> HoldIncrease {get;set;}
        public Nullable<double> Investment { get; set; }
        public Nullable<double> ReturnRate { get; set; }
        public Nullable<float> FundScore { get; set; }
        public Nullable<float> ManagerScore { get; set; }
        public int status { get; set; }

    }
}
