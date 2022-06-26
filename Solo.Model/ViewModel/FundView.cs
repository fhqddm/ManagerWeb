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
        public double R2year { get; set; }
        public double R3year { get; set; }
        public double R5year { get; set; }
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
        public double Score { get; set; }
        public double Score3y { get; set; }
        public float FundScore { get; set; }
        public float ManagerScore { get; set; }
        //public float R2015 { get; set; }
        //public float R2016 { get; set; }
        //public float R2017 { get; set; }
        //public float R2018 { get; set; }
        //public float R2019 { get; set; }
        //public float R2020 { get; set; }
        //public float R2021 { get; set; }
        public float A2015 { get; set; }
        public float A2016 { get; set; }
        public float A2017 { get; set; }
        public float A2018 { get; set; }
        public float A2019 { get; set; }
        public float A2020 { get; set; }
        public float A2021 { get; set; }
        public float A2022 { get; set; }
        public Nullable<double> D_Top { get; set; }
        public Nullable<double> D_ExpM2 { get; set; }
        public Nullable<double> D_ExpM1 { get; set; }
        public Nullable<double> D_ExpD10 { get; set; }
        public Nullable<double> D_ExpD5 { get; set; }
        public Nullable<double> D_Exp1 { get; set; }
        public Nullable<double> D_Exp2 { get; set; }
        public Nullable<double> D_Exp3 { get; set; }
        public Nullable<double> D_A60 { get; set; }
        public Nullable<double> D_Max10D { get; set; }
        public Nullable<double> D_Max5D { get; set; }
        public double LinearM1 { get; set; }
        public double LinearM2 { get; set; }
        public Nullable<double> Linear1 { get; set; }
        public Nullable<double> Linear2 { get; set; }
        public Nullable<double> Linear3 { get; set; }
        public Nullable<double> Linear5 { get; set; }
        public Nullable<int> Rank_Id { get; set; }
        public Nullable<int> BestReturn_Id { get; set; }
        public DateTime FundUpdateTime { get; set; }
        public int UserId { get; set; }
        public string Strategy { get; set; }
        public string FundType { get; set; }
        public string FundManagerName { get; set; }
        public string FundManagerNo { get; set; }
        public double Top10Rate { get; set; }
        public double Volatility { get; set; }
        public string StockNos { get; set; }
        public string StockNames { get; set; }
        public string StockPositions { get; set; }
        public string PEs { get; set; }
        public string Belongs { get; set; }
        public double Sharp { get; set; }
        public double Sharp2 { get; set; }
        public double Sharp3 { get; set; }
        public double Maxretra { get; set; }
        public double Maxretra5 { get; set; }
        public double Stddev { get; set; }
        public double Stddev2 { get; set; }
        public double Stddev3 { get; set; }
        public double AvgPE { get; set; }
        public double AvgPB { get; set; }
        public double alpha { get; set; }
        public string IndustryJson { get; set; }
        public string Turnovers { get; set; }
        public string StockR1days { get; set; }
        public string StockIndustrys { get; set; }
        public int MorningStar { get; set; }
        public double PoolRate { get; set; }

    }
}
