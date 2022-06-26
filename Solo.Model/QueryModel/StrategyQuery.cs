using System;
using System.Collections.Generic;
using System.Text;

namespace Solo.Model.QueryModel
{
    public class StrategyQuery
    {
        public string FundName { get; set; }
        public string Strategy { get; set; }

        public int status { get; set; }

        public bool isnew { get; set; }

        public int bondstock { get; set; } // 1.Bond 2.Stock 3.All
        public int UserId { get; set; }
    }
}
