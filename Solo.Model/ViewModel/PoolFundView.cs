using System;
using System.Collections.Generic;
using System.Text;

namespace Solo.Model.ViewModel
{
    public class PoolFundView
    {
        public string FundNo { get; set; }
        public string FundName { get; set; }
        public double Score { get; set; }
        public Nullable<int> Rank_Id { get; set; }
        public string Strategy { get; set; }
        public string StockNos { get; set; }
        public string StockNames { get; set; }
        public string StockPositions { get; set; }
        public int UserId { get; set; }
    }
}
