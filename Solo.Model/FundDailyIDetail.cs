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
        public Nullable<float> EquityReturn { get; set; }
        public Nullable<float> UnitMoney { get; set; }

    }
}
