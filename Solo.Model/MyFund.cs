using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Solo.Model.ViewModel
{
    public class MyFund
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FundNo { get; set; }
        public int Status { get; set; }//Status=1 持有 Status=2 观望
        public int Investment { get; set; }
        public Nullable<double> InNetValue { get; set; }     
        public string Strategy { get; set; }
        public double Estimate { get; set; }
        public int ValuationId { get; set; }


    }

    //public enum Strategy
    //{
    //    Conservative = 1 // 逐年大于5，
    //}
}
