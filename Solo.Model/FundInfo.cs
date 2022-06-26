using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Solo.Model
{
    public class FundInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FundNo { get; set; }
        public string FundName { get; set; }
        public string FundType { get; set; }
        public Nullable<float> BuyRate { get; set; }
        public Nullable<float> TotalScale { get; set; }
        public Nullable<float> OrgHoldRate { get; set; }
        public Nullable<float> NetValue { get; set; }
        public Nullable<float> StockRate { get; set; }
        public Nullable<float> R1day { get; set; }
        public Nullable<float> R1week { get; set; }
        public Nullable<float> R1month {get;set;}
        public Nullable<float> R3month { get; set; }
        public Nullable<float> R6month { get; set; }
        public Nullable<float> R1year { get; set; }
        public Nullable<float> R2year { get; set; }
        public Nullable<float> R3year { get; set; }
        public Nullable<float> R5year { get; set; }
        public Nullable<float> R2015 { get; set; }
        public Nullable<float> R2016 { get; set; }
        public Nullable<float> R2017 { get; set; }
        public Nullable<float> R2018 { get; set; }
        public Nullable<float> R2019 { get; set; }
        public Nullable<float> R2020 { get; set; }
        public Nullable<float> R2021 { get; set; }
        public Nullable<float> R2022 { get; set; }
        public Nullable<float> A2015 { get; set; }
        public Nullable<float> A2016 { get; set; }
        public Nullable<float> A2017 { get; set; }
        public Nullable<float> A2018 { get; set; }
        public Nullable<float> A2019 { get; set; }
        public Nullable<float> A2020 { get; set; }
        public Nullable<float> A2021 { get; set; }
        public Nullable<float> A2022 { get; set; }
        public Nullable<float> ManagerFee { get; set; }
        public Nullable<float> CustodyFee { get; set; }
        public Nullable<float> SaleFee { get; set; }
        public Nullable<float> Over7dFee { get; set; }
        public Nullable<float> Over30dFee { get; set; }
        public Nullable<float> Over1yFee { get; set; }
        public Nullable<float> Over2yFee { get; set; }
        public Nullable<float> FundScore { get; set; }
        public Nullable<float> ManagerScore { get; set; }
        public double TotalUnitMoney { get; set; }
        public Nullable<DateTime> DutyDate { get; set; }
        public Nullable<DateTime> FundUpdateTime { get; set; }
        public string Strategy { get; set; }
        public Nullable<double> Estimate { get; set; }
        public Nullable<int> ValuationId { get; set; }
        public string FundManagerNo { get; set; }
        public string StockNos { get; set; }
        public string StockNames { get; set; }
        public string Combines { get; set; }
        public Nullable<float> Volatility { get; set; }
        public Nullable<float> Sharp { get; set; }
        public Nullable<float> Sharp2 { get; set; }
        public Nullable<float> Sharp3 { get; set; }
        public Nullable<float> Maxretra { get; set; }
        public Nullable<float> Maxretra5 { get; set; }
        public Nullable<float> Stddev { get; set; }
        public Nullable<float> Stddev2 { get; set; }
        public Nullable<float> Stddev3 { get; set; }
        public string StockPositions { get; set; }
        public string Turnovers { get; set; }
        public Nullable<int> MorningStar { get; set; }

    }
}
