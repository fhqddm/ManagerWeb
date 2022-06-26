using System;
using System.Collections.Generic;
using System.Text;

namespace Solo.Model.QueryModel
{
    public class StockQuery
    {
        public string StockNos { get; set; }
        public string StockNo { get; set; }
        public string StockName { get; set; }
        public string IndustryName { get; set; }
        public bool resetRedis { get; set; }
        public int UserId { get; set; }
        public int HoldWait { get; set; }
        public Nullable<double> MinFundTop10HoldRate { get; set; }
        public string Belong { get; set; }
    }
}
