using System;
using System.Collections.Generic;
using System.Text;

namespace Solo.Model.ViewModel
{
    public class PoolStockView
    {
        public string StockNo { get; set; }
        public string StockName { get; set; }
        public string IndustryName { get; set; }
        public double FundTop10HoldRate { get; set; }
        public double P_E_Ratio { get; set; } //市盈率 price/earnings ratios 
        public double Price { get; set; }
        public double CurrentMoney { get; set; }
        
    }
}
