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
        public double HoldShares { get; set; }
        public double Cost { get; set; }
        public double TotalBonus { get; set; }
        public Nullable<float> ReturnRate { get; set; }
        public int UserId { get; set; }


    }
}
