using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Solo.Model
{
    public class FundDailyIDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FundNo { get; set; }
        public DateTime DataTime { get; set; }
        public Nullable<float> NetValue {get;set;}
        public Nullable<float> UnitValue { get; set; }
        public Nullable<float> PureNet { get; set; }
        public Nullable<float> EquityReturn { get; set; }
        public Nullable<float> UnitMoney { get; set; }
        public Nullable<float> Seperate { get; set; }
        public Nullable<float> MA5 { get; set; }
        public Nullable<float> MA10 { get; set; }
        public Nullable<float> Trend10 { get; set; }
        public Nullable<float> Slope10 { get; set; }
        public Nullable<float> MA20 { get; set; }
        public Nullable<float> Slope20 { get; set; }
        public Nullable<float> Trend20 { get; set; }
        public Nullable<float> MA60 { get; set; }
        public Nullable<float> Trend60 { get; set; }
        public Nullable<float> Slope60 { get; set; }

    }
}
