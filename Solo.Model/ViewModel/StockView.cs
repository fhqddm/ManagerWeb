using System;
using System.Collections.Generic;
using System.Text;

namespace Solo.Model.ViewModel
{
    public class StockView
    {
        public int Id { get; set; }
        public string StockNo { get; set; }
        public string StockName { get; set; }
        public double Market { get; set; } //0.深圳证券交易所  1.上海证券交易所
        public double P_E_Ratio { get; set; } //市盈率 price/earnings ratios 
        public double MarketCap { get; set; } // 总市值
        public double P_B_Ratio { get; set; }    //市净率 Price to book ratio
        public double ROE { get; set; } //股本回报率
        public double NetRate { get; set; }//净利率
        public double GrossProfit { get; set; }//毛利润
        public double NetMargin { get; set; }//净利润
        public double NetAssets { get; set; }//净资产
        public string IndustryId { get; set; }
        public string IndustryName { get; set; }
        public double FundTop10HoldRate { get; set; }
        public double R1day { get; set; }
        public double Price { get; set; }
        public DateTime StockUpdateTime { get; set; }
        public double CurrentMoney { get; set; }
        public string MyFundNames { get; set; }
        public double SZ50 { get; set; }
        public double HS300 { get; set; }
        public double ZZ500 { get; set; }
        public double ZZ1000 { get; set; }
    }
}
