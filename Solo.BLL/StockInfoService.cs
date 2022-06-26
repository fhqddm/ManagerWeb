using AutoMapper;
using Microsoft.Extensions.Configuration;
using Solo.Common;
using Solo.Model;
using Solo.Model.QueryModel;
using Solo.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solo.BLL
{
    public class StockInfoService : IStockInfoService
    {


        public List<StockView> GetStockInfos(StockQuery query)
        {
            using (MyContext myContext = new MyContext())
            {


                RedisConn readConn = new RedisConn(true);
                var stockViews = readConn.GetRedisData<List<StockView>>("StockViews");

                if ((stockViews == null || stockViews.Count == 0) || query.resetRedis)
                {
                    stockViews = new List<StockView>();
                    var stockInfos = myContext.StockInfos.ToList();
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<StockInfo, StockView>());
                    var mapper = config.CreateMapper();
                    foreach (var stockInfo in stockInfos)
                    {
                        var stockView = mapper.Map<StockView>(stockInfo);
                        stockView.CurrentMoney = 0;
                        stockView.MyFundNames = "";
                        stockViews.Add(stockView);
                    }
                    RedisConn writeConn = new RedisConn(false);
                    writeConn.SetRedisData("StockViews", stockViews);
                    writeConn.Close();
                }

                if (query != null)
                {
                    if (!string.IsNullOrEmpty(query.StockNos))
                    {
                        stockViews = stockViews.Where(x => query.StockNos.Contains(x.StockNo)).ToList();
                    }
                    if (!string.IsNullOrEmpty(query.StockNo))
                    {
                        stockViews = stockViews.Where(x => x.StockNo.Contains(query.StockNo)).ToList();
                    }
                    if (!string.IsNullOrEmpty(query.StockName))
                    {
                        stockViews = stockViews.Where(x => x.StockName.Contains(query.StockName)).ToList();
                    }
                    if (!string.IsNullOrEmpty(query.IndustryName))
                    {
                        stockViews = stockViews.Where(x => x.IndustryName.Contains(query.IndustryName)).ToList();
                    }
                    if(query.MinFundTop10HoldRate != null)
                    {
                        stockViews = stockViews.Where(x => x.FundTop10HoldRate>query.MinFundTop10HoldRate).ToList();
                    }
                    if (!string.IsNullOrEmpty(query.Belong))
                    {
                        if (query.Belong == "HS300")
                        {
                            stockViews = stockViews.Where(x => x.HS300 > 0).ToList();
                        }
                        else if (query.Belong == "ZZ500")
                        {
                            stockViews = stockViews.Where(x => x.ZZ500 > 0).ToList();
                        }
                        else if (query.Belong == "ZZ1000")
                        {
                            stockViews = stockViews.Where(x => x.ZZ1000 > 0).ToList();
                        }
                        else if (query.Belong == "SZ50")
                        {
                            stockViews = stockViews.Where(x => x.SZ50 > 0).ToList();
                        }
                        else if (query.Belong == "HK")
                        {
                            stockViews = stockViews.Where(x => x.StockNo.Split('.')[0]=="116").ToList();
                        }
                        else if (query.Belong == "Other")
                        {
                            stockViews = stockViews.Where(x => x.SZ50 == 0 && x.HS300 == 0 && x.ZZ1000 == 0 && x.ZZ500 == 0 && x.StockNo.Split('.')[0] != "116").ToList();
                        }
                    }
                }
                readConn.Close();
                return stockViews;
            }
        }


    }
}
