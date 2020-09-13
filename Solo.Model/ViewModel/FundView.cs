using System;
using System.Collections.Generic;
using System.Text;

namespace Solo.Model.ViewModel
{
    public class FundView
    {
        public string FundNo { get; set; }
        public string FundName { get; set; }
        public float TotalFee { get; set; }
        public float TotalScale { get; set; }
        public float OrgHoldRate { get; set; }
        public float R1day { get; set; }
        public float R1week { get; set; }
        public double R1month { get; set; }
        public double R3month { get; set; }
        public double R6month { get; set; }
        public double R1year { get; set; }
        public float R2year { get; set; }
        public float R3year { get; set; }
        public float R5year { get; set; }
        public float Over7dFee { get; set; }
        public float Over30dFee { get; set; }
        public float Over1yFee { get; set; }
        public float Over2yFee { get; set; }
        public DateTime DutyDate { get; set; }
        public int status { get; set; }
        public int Investment { get; set; }
        public float NetValue { get; set; }
        public float StockRate { get; set; }
        public double ReturnRate { get; set; }
        public float FundScore { get; set; }
        public float ManagerScore { get; set; }
        public Nullable<int> Rank_Id { get; set; }
        public DateTime FundUpdateTime { get; set; }
    }
}
