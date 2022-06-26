using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Solo.Model
{
    public class ManagerInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FundManagerNo { get; set; }
        public string FundManagerName { get; set; }
        public string FundCompanyNo { get; set; }
        public string FundCompanyName { get; set; }
        public string ManagerFundNos { get; set; }
        public string ManagerFundNames { get; set; }
        public int DutyTime { get; set; }
        public string MainFundNo { get; set; }
        public string MainFundName { get; set; }
        public double ManagerScale { get; set; }
        public double BestReturn { get; set; }
    }
}
