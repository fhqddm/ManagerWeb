using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Solo.Common;
using Solo.Model;
using Solo.Model.QueryModel;
using Solo.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using MySqlHelper = Solo.Common.MySqlHelper;
using System.Linq;
using AutoMapper;
using Newtonsoft.Json;
using System.Threading;

namespace Solo.BLL
{
    public class FundInfoMySqlService : IFundInfoService
    {

        public List<FundView> GetFundInfosByQuery(FundQuery fundQuery)
        {

            //using (MyContext context = new MyContext())
            //{
            //    var fundInfos = context.FundInfos;
            //    string lastFundNo = "";
            //    foreach (var fundInfo in fundInfos)
            //    {
            //        lastFundNo = fundInfo.FundNo;
            //    }
            //}

            List<FundView> list = new List<FundView>();
            //RedisHelper redisHelper = new RedisHelper(AppConfigurtaionServices.Configuration.GetConnectionString("RedisConnection"));
            RedisConn readConn = new RedisConn(true);
            
            var redisResult = readConn.GetRedisData<List<FundView>>("FundViews");
            if (fundQuery.resetRedis == false && redisResult == null)
            {
                int count = 0;
                while (count <5 && redisResult == null)
                {
                    count++;
                    Console.WriteLine("Can not get redis FundViews");
                    Thread.Sleep(250);
                    redisResult = readConn.GetRedisData<List<FundView>>("FundViews");
                   
                    
                }

            }
            if (redisResult != null && redisResult.Count >= 710 && !fundQuery.resetRedis)
            {
                if (fundQuery.UserId > 0)
                {
                    var redisMyfunds = readConn.GetRedisData<List<MyFund>>("MyFunds").Where(x=>x.UserId==fundQuery.UserId).ToList();

                    foreach (var myfund in redisMyfunds)
                    {
                        redisResult.Where(x => x.FundNo == myfund.FundNo).SingleOrDefault().UserId = myfund.UserId;
                        redisResult.Where(x => x.FundNo == myfund.FundNo).SingleOrDefault().status = myfund.Status;
                        redisResult.Where(x => x.FundNo == myfund.FundNo).SingleOrDefault().Investment = myfund.Investment;
                        //redisResult.Where(x => x.FundNo == myfund.FundNo).SingleOrDefault().ReturnRate = Math.Round(Convert.ToDouble(myfund.ReturnRate * 100), 2);
                        double unitvalue = redisResult.Where(x => x.FundNo == myfund.FundNo).SingleOrDefault().NetValue;
                        double cost = myfund.Cost;
                        double totalshares = myfund.HoldShares;
                        double totalbonus = myfund.TotalBonus;

                        if (totalshares != 0 && cost != 0)
                        {
                            redisResult.Where(x => x.FundNo == myfund.FundNo).SingleOrDefault().ReturnRate = Math.Round((((totalshares * unitvalue + totalbonus) / (cost * totalshares)) - 1) * 100, 2);
                        }
                        else
                        {
                            redisResult.Where(x => x.FundNo == myfund.FundNo).SingleOrDefault().ReturnRate = 0;
                        }
                        
                    }
                    if (fundQuery.Status > 0)
                    {
                        redisResult = redisResult.Where(x => x.UserId == fundQuery.UserId).ToList();

                        if (fundQuery.Status == 1 || fundQuery.Status == 2)
                        {
                            redisResult = redisResult.Where(x => x.status == fundQuery.Status).ToList();
                        }
                        else if (fundQuery.Status == 3)
                        {
                            redisResult = redisResult.Where(x => x.status > 0).ToList();
                        }
                    }                    
                }
                if (!string.IsNullOrEmpty(fundQuery.FundNo))
                {
                    redisResult = redisResult.Where(x => x.FundNo.Contains(fundQuery.FundNo)).ToList();
                }
                if (!string.IsNullOrEmpty(fundQuery.Strategy))
                {
                    redisResult = redisResult.Where(x => x.Strategy.Contains(fundQuery.Strategy)).ToList();
                }
                if (!string.IsNullOrEmpty(fundQuery.MainIndustry))
                {
                    redisResult = redisResult.Where(x => GetIndustryPosition(x.IndustryJson,fundQuery.MainIndustry)>15).ToList();
                }
                if (fundQuery.isnew)
                {
                    redisResult = redisResult.Where(x => DateHelper.getFormatDateTime(DateTime.Now) == DateHelper.getFormatDateTime(x.FundUpdateTime)).ToList();
                }
                if (!string.IsNullOrEmpty(fundQuery.FundName))
                {
                    redisResult = redisResult.Where(x => x.FundName.Contains(fundQuery.FundName)).ToList();
                }
                if (fundQuery.MinStockRate != null)
                {
                    redisResult = redisResult.Where(x => x.StockRate >= fundQuery.MinStockRate).ToList();
                }
                if (fundQuery.MaxStockRate != null)
                {
                    redisResult = redisResult.Where(x => x.StockRate <= fundQuery.MaxStockRate).ToList();
                }
                if (fundQuery.MinOrgHoldRate != null)
                {
                    redisResult = redisResult.Where(x => x.OrgHoldRate >= fundQuery.MinOrgHoldRate).ToList();
                }
                if (fundQuery.MaxOrgHoldRate != null)
                {
                    redisResult = redisResult.Where(x => x.OrgHoldRate <= fundQuery.MaxOrgHoldRate).ToList();
                }
                if (fundQuery.MinRank != null)
                {
                    redisResult = redisResult.Where(x => x.Rank_Id >= fundQuery.MinRank).ToList();
                }
                if (fundQuery.MinRank != null)
                {
                    redisResult = redisResult.Where(x => x.Rank_Id <= fundQuery.MaxRank).ToList();
                }
                if (fundQuery.bondstock == 1)
                {
                    //redisResult = redisResult.Where(x => x.StockRate <= 30 && x.FundName.Contains("债")).ToList();
                    redisResult = redisResult.Where(x => x.FundType == "bond").ToList();
                }
                else if (fundQuery.bondstock == 2)
                {
                    redisResult = redisResult.Where(x => x.FundType == "stock" || x.FundType == "index" || x.FundType == "qdii").ToList();
                    //redisResult = redisResult.Where(x => (x.StockRate > 30 || x.FundName.Contains("指数") || x.FundName.Contains("ETF")) && !x.FundName.Contains("债")).ToList();
                }
                if (fundQuery.MinTotalScale != null)
                {
                    redisResult = redisResult.Where(x => x.TotalScale >= fundQuery.MinTotalScale).ToList();
                }
                if (fundQuery.MaxTotalScale != null)
                {
                    redisResult = redisResult.Where(x => x.TotalScale <= fundQuery.MaxTotalScale).ToList();
                }
                if (fundQuery.MinAvgPE != null)
                {
                    redisResult = redisResult.Where(x => x.AvgPE > fundQuery.MinAvgPE).ToList();
                }
                if (fundQuery.MaxAvgPE != null)
                {
                    redisResult = redisResult.Where(x => x.AvgPE <= fundQuery.MaxAvgPE).ToList();
                }
                if (fundQuery.MinAvgPB != null)
                {
                    redisResult = redisResult.Where(x => x.AvgPB >= fundQuery.MinAvgPB).ToList();
                }
                if (fundQuery.MaxTop10Rate != null)
                {
                    redisResult = redisResult.Where(x => x.Top10Rate <= fundQuery.MaxTop10Rate).ToList();
                }
                if (fundQuery.MinTop10Rate != null)
                {
                    redisResult = redisResult.Where(x => x.Top10Rate >= fundQuery.MinTop10Rate).ToList();
                }
                readConn.Close();
                return redisResult;
            }
            //不能从redis中获得数据，直接返回空
            else if (!fundQuery.resetRedis)
            {
                return null;
            }

            RedisConn writeConn = new RedisConn(false);

            //string sql = "select *,(IFNULL(BuyRate, 0)+IFNULL(ManagerFee, 0)+IFNULL(CustodyFee, 0)+IFNULL(SaleFee, 0)) as TotalFee from FundInfos fi left join MyScore ms on fi.FundNo=ms.FundNo left join ManagerInfos mi on fi.FundManagerNo=mi.FundManagerNo ";
            string sql = "select fi.FundNo,FundName,BuyRate,TotalScale,R1day,R1week,R1month,R3month,R6month,fi.R1year,fi.R2year,fi.R3year,fi.R5year,DutyDate,OrgHoldRate,NetValue,StockRate,FundUpdateTime,fi.FundScore,fi.ManagerScore,Strategy,FundType,A2015,A2016,A2017,A2018,A2019,A2020,A2021,A2022,ms.BestReturn_Id,Score,Score3y,Rank_Id,fi.FundManagerNo,StockNos,StockPositions,StockNames,Maxretra,fi.Maxretra5,fi.Sharp,fi.Stddev,FundType,Turnovers,MorningStar,fi.Sharp2,fi.Sharp3,fi.Stddev2,fi.Stddev3,OrgHold,AvgScale,fi.Volatility,FundManagerName,(IFNULL(BuyRate, 0)+IFNULL(ManagerFee, 0)+IFNULL(CustodyFee, 0)+IFNULL(SaleFee, 0)) as TotalFee from FundInfos fi left join MyScore ms on fi.FundNo=ms.FundNo left join ManagerInfos mi on fi.FundManagerNo=mi.FundManagerNo ";
            sql = sql + SetMyFundString(fundQuery)+ SetFundString(fundQuery)+" order by R1day desc";
            float outSy = 0;
            int outMf = 0;
            MySqlParameter[] pars = null;
            try
            {
                DataTable da = MySqlHelper.ExecuteTable(sql, pars);
                if (da.Rows.Count > 0)
                {
                    var stocks = new List<StockInfo>();
                    double hs300_r1year = 0;
                    using (MyContext context = new MyContext())
                    {
                        stocks = context.StockInfos.ToList();
                        hs300_r1year = Convert.ToDouble(context.FundInfos.Where(x => x.FundNo == "333333").SingleOrDefault().R1year);
                    }

                    for (int i = 0; i < da.Rows.Count; i++)
                    {
                        FundView fv = new FundView();
                        fv.FundNo = da.Rows[i]["FundNo"].ToString();
                        fv.FundName = da.Rows[i]["FundName"].ToString();
                        fv.StockNos = da.Rows[i]["StockNos"].ToString();
                        fv.StockNames = da.Rows[i]["StockNames"].ToString();
                        fv.StockPositions = da.Rows[i]["StockPositions"].ToString();
                        fv.FundManagerName = da.Rows[i]["FundManagerName"].ToString();
                        var pArr = fv.StockPositions.Split(',');
                        fv.Top10Rate = 0;
                        foreach (var item in pArr)
                        {
                            double temp = 0;
                            if (double.TryParse(item, out temp))
                            {
                                fv.Top10Rate = fv.Top10Rate + temp;
                            };                           
                        }
                        fv.Top10Rate = Math.Round(fv.Top10Rate, 2);

                        fv.FundManagerNo = da.Rows[i]["FundManagerNo"].ToString();
                        fv.Strategy = da.Rows[i]["Strategy"].ToString();
                        fv.FundType = da.Rows[i]["FundType"].ToString();
                        fv.Turnovers = da.Rows[i]["Turnovers"].ToString();
                        if (float.TryParse(da.Rows[i]["TotalFee"].ToString(), out outSy))
                        {
                            float temp = outSy;
                            fv.TotalFee = (float)Math.Round(temp, 2);
                        }
                        if (float.TryParse(da.Rows[i]["TotalScale"].ToString(), out outSy))
                        {
                            fv.TotalScale = (float)da.Rows[i]["TotalScale"];
                        }
                        if (float.TryParse(da.Rows[i]["OrgHoldRate"].ToString(), out outSy))
                        {
                            float temp = (float)da.Rows[i]["OrgHoldRate"];
                            fv.OrgHoldRate = (float)Math.Round(temp, 2);
                        }
                        if (float.TryParse(da.Rows[i]["R1day"].ToString(), out outSy))
                        {
                            fv.R1day = (float)da.Rows[i]["R1day"];
                        }
                        if (float.TryParse(da.Rows[i]["R1week"].ToString(), out outSy))
                        {
                            fv.R1week = (float)da.Rows[i]["R1week"];
                        }
                        if (float.TryParse(da.Rows[i]["R1month"].ToString(), out outSy))
                        {
                            double viewrr = Math.Round(Convert.ToDouble(da.Rows[i]["R1month"]), 2);
                            fv.R1month = viewrr;
                            //fv.R1month = (float)da.Rows[i]["R1month"];
                        }
                        if (float.TryParse(da.Rows[i]["R3month"].ToString(), out outSy))
                        {
                            double viewrr = Math.Round(Convert.ToDouble(da.Rows[i]["R3month"]), 2);
                            fv.R3month = viewrr;
                            //fv.R3month = (float)da.Rows[i]["R3month"];
                        }
                        if (float.TryParse(da.Rows[i]["R6month"].ToString(), out outSy))
                        {
                            double viewrr = Math.Round(Convert.ToDouble(da.Rows[i]["R6month"]), 2);
                            fv.R6month = viewrr;
                            //fv.R6month = (float)da.Rows[i]["R6month"];
                        }
                        if (float.TryParse(da.Rows[i]["R1year"].ToString(), out outSy))
                        {
                            double viewrr = Math.Round(Convert.ToDouble(da.Rows[i]["R1year"]), 2);
                            fv.R1year = viewrr;
                            //fv.R1year = (float)da.Rows[i]["R1year"];
                        }
                        if (float.TryParse(da.Rows[i]["R2year"].ToString(), out outSy))
                        {
                            fv.R2year = Math.Round(Convert.ToDouble(da.Rows[i]["R2year"]), 2);
                        }
                        if (float.TryParse(da.Rows[i]["R3year"].ToString(), out outSy))
                        {
                            fv.R3year = Math.Round(Convert.ToDouble(da.Rows[i]["R3year"]), 2);
                        }
                        if (float.TryParse(da.Rows[i]["R5year"].ToString(), out outSy))
                        {
                            fv.R5year = Math.Round(Convert.ToDouble(da.Rows[i]["R5year"]), 2);
                        }
                        //if (float.TryParse(da.Rows[i]["Over7dFee"].ToString(), out outSy))
                        //{
                        //    fv.Over7dFee = (float)da.Rows[i]["Over7dFee"];
                        //}
                        //if (float.TryParse(da.Rows[i]["Over30dFee"].ToString(), out outSy))
                        //{
                        //    fv.Over30dFee = (float)da.Rows[i]["Over30dFee"];
                        //}
                        //if (float.TryParse(da.Rows[i]["Over1yFee"].ToString(), out outSy))
                        //{
                        //    fv.Over1yFee = (float)da.Rows[i]["Over1yFee"];
                        //}
                        //if (float.TryParse(da.Rows[i]["Over2yFee"].ToString(), out outSy))
                        //{
                        //    fv.Over2yFee = (float)da.Rows[i]["Over2yFee"];
                        //}
                        if (int.TryParse(da.Rows[i]["Rank_Id"].ToString(), out outMf))
                        {
                            fv.Rank_Id = (int)da.Rows[i]["Rank_Id"];
                        }
                        else
                        {
                            fv.Rank_Id = 9999;
                        }
                        if (int.TryParse(da.Rows[i]["MorningStar"].ToString(), out outMf))
                        {
                            fv.MorningStar = (int)da.Rows[i]["MorningStar"];
                        }
                        if (int.TryParse(da.Rows[i]["BestReturn_Id"].ToString(), out outMf))
                        {
                            fv.BestReturn_Id = (int)da.Rows[i]["BestReturn_Id"];
                        }
                        if (float.TryParse(da.Rows[i]["StockRate"].ToString(), out outSy))
                        {
                            fv.StockRate = (float)da.Rows[i]["StockRate"];
                        }
                        if (float.TryParse(da.Rows[i]["Score"].ToString(), out outSy))
                        {
                            fv.Score = Math.Round((float)da.Rows[i]["Score"],2);
                        }
                        if (float.TryParse(da.Rows[i]["Score3y"].ToString(), out outSy))
                        {
                            fv.Score3y = Math.Round((float)da.Rows[i]["Score3y"], 2);
                        }
                        if (float.TryParse(da.Rows[i]["FundScore"].ToString(), out outSy))
                        {
                            fv.FundScore = (float)da.Rows[i]["FundScore"];
                        }
                        if (float.TryParse(da.Rows[i]["ManagerScore"].ToString(), out outSy))
                        {
                            fv.ManagerScore = (float)da.Rows[i]["ManagerScore"];
                        }
                        if (fundQuery.UserId>0)
                        {
                            if (int.TryParse(da.Rows[i]["Investment"].ToString(), out outMf))
                            {
                                fv.Investment = (int)da.Rows[i]["Investment"];
                            }
                            if (float.TryParse(da.Rows[i]["ReturnRate"].ToString(), out outSy))
                            {
                                double viewrr = Math.Round(Convert.ToDouble(da.Rows[i]["ReturnRate"]) * 100, 2);
                                fv.ReturnRate = viewrr;
                            }
                        }
                        if (float.TryParse(da.Rows[i]["NetValue"].ToString(), out outSy))
                        {
                            fv.NetValue = (float)da.Rows[i]["NetValue"];
                        }
                        if (float.TryParse(da.Rows[i]["Volatility"].ToString(), out outSy))
                        {
                            double viewrr = Math.Round(Convert.ToDouble(da.Rows[i]["Volatility"]), 2);
                            fv.Volatility = viewrr;
                        }
                        if (float.TryParse(da.Rows[i]["Sharp"].ToString(), out outSy))
                        {
                            double viewrr = Math.Round(Convert.ToDouble(da.Rows[i]["Sharp"]), 2);
                            fv.Sharp = viewrr;
                        }
                        if (float.TryParse(da.Rows[i]["Sharp2"].ToString(), out outSy))
                        {
                            double viewrr = Math.Round(Convert.ToDouble(da.Rows[i]["Sharp2"]), 2);
                            fv.Sharp2 = viewrr;
                        }
                        if (float.TryParse(da.Rows[i]["Sharp3"].ToString(), out outSy))
                        {
                            double viewrr = Math.Round(Convert.ToDouble(da.Rows[i]["Sharp3"]), 2);
                            fv.Sharp3 = viewrr;
                        }
                        if (float.TryParse(da.Rows[i]["Maxretra"].ToString(), out outSy))
                        {
                            double viewrr = Math.Round(Convert.ToDouble(da.Rows[i]["Maxretra"]), 2);
                            fv.Maxretra = viewrr;
                        }
                        if (float.TryParse(da.Rows[i]["Maxretra5"].ToString(), out outSy))
                        {
                            fv.Maxretra5 = Math.Round(Convert.ToDouble(da.Rows[i]["Maxretra5"]), 2);
                        }
                        if (float.TryParse(da.Rows[i]["Stddev"].ToString(), out outSy))
                        {
                            double viewrr = Math.Round(Convert.ToDouble(da.Rows[i]["Stddev"]), 2);
                            fv.Stddev = viewrr;
                        }
                        if (float.TryParse(da.Rows[i]["Stddev2"].ToString(), out outSy))
                        {
                            double viewrr = Math.Round(Convert.ToDouble(da.Rows[i]["Stddev2"]), 2);
                            fv.Stddev2 = viewrr;
                        }
                        if (float.TryParse(da.Rows[i]["Stddev3"].ToString(), out outSy))
                        {
                            double viewrr = Math.Round(Convert.ToDouble(da.Rows[i]["Stddev3"]), 2);
                            fv.Stddev3 = viewrr;
                        }
                        //if (float.TryParse(da.Rows[i]["R2015"].ToString(), out outSy))
                        //{
                        //    fv.R2015 = (float)da.Rows[i]["R2015"];
                        //}
                        //if (float.TryParse(da.Rows[i]["R2016"].ToString(), out outSy))
                        //{
                        //    fv.R2016 = (float)da.Rows[i]["R2016"];
                        //}
                        //if (float.TryParse(da.Rows[i]["R2017"].ToString(), out outSy))
                        //{
                        //    fv.R2017 = (float)da.Rows[i]["R2017"];
                        //}
                        //if (float.TryParse(da.Rows[i]["R2018"].ToString(), out outSy))
                        //{
                        //    fv.R2018 = (float)da.Rows[i]["R2018"];
                        //}
                        //if (float.TryParse(da.Rows[i]["R2019"].ToString(), out outSy))
                        //{
                        //    fv.R2019 = (float)da.Rows[i]["R2019"];
                        //}
                        //if (float.TryParse(da.Rows[i]["R2020"].ToString(), out outSy))
                        //{
                        //    fv.R2020 = (float)da.Rows[i]["R2020"];
                        //}
                        //if (float.TryParse(da.Rows[i]["R2021"].ToString(), out outSy))
                        //{
                        //    fv.R2021 = (float)da.Rows[i]["R2021"];
                        //}
                        if (float.TryParse(da.Rows[i]["A2015"].ToString(), out outSy))
                        {
                            fv.A2015 = (float)da.Rows[i]["A2015"];
                        }
                        if (float.TryParse(da.Rows[i]["A2016"].ToString(), out outSy))
                        {
                            fv.A2016 = (float)da.Rows[i]["A2016"];
                        }
                        if (float.TryParse(da.Rows[i]["A2017"].ToString(), out outSy))
                        {
                            fv.A2017 = (float)da.Rows[i]["A2017"];
                        }
                        if (float.TryParse(da.Rows[i]["A2018"].ToString(), out outSy))
                        {
                            fv.A2018 = (float)da.Rows[i]["A2018"];
                        }
                        if (float.TryParse(da.Rows[i]["A2019"].ToString(), out outSy))
                        {
                            fv.A2019 = (float)da.Rows[i]["A2019"];
                        }
                        if (float.TryParse(da.Rows[i]["A2020"].ToString(), out outSy))
                        {
                            fv.A2020 = (float)da.Rows[i]["A2020"];
                        }
                        if (float.TryParse(da.Rows[i]["A2021"].ToString(), out outSy))
                        {
                            fv.A2021 = (float)da.Rows[i]["A2021"];
                        }
                        if (float.TryParse(da.Rows[i]["A2022"].ToString(), out outSy))
                        {
                            fv.A2022 = (float)da.Rows[i]["A2022"];
                        }
                        fv.DutyDate = Convert.ToDateTime(da.Rows[i]["DutyDate"]);
                        fv.FundUpdateTime = Convert.ToDateTime(da.Rows[i]["FundUpdateTime"]);
                        //int userId = 0;
                        //int.TryParse(da.Rows[i]["UserId"].ToString(),out userId);
                        //fv.UserId = userId;


                        var stocknos = fv.StockNos.Split(',');
                        var positions = fv.StockPositions.Split(',');
                        double AvgPE = 0;
                        double AvgPB = 0;
                        string StockR1days = "";
                        string StockIndustrys = "";
                        string PEs = "";
                        string Belongs = "";
                        Dictionary<string, double> industry_dic = new Dictionary<string, double>();

                        if (!fv.FundName.Contains("债"))
                        {                          
                            for (int j = 0; j < stocknos.Length; j++)
                            {
                                var stock = stocks.Where(x => x.StockNo == stocknos[j]).SingleOrDefault();
                                if (stock != null &&  stock.P_B_Ratio > 0 && stocknos.Count() == positions.Count())
                                {
                                    double position = 0;
                                    double.TryParse(positions[j], out position);
                                    if (!industry_dic.ContainsKey(stock.IndustryName))
                                    {                                       
                                        industry_dic.Add(stock.IndustryName, position);
                                    }
                                    else
                                    {
                                        industry_dic[stock.IndustryName] = Math.Round(industry_dic[stock.IndustryName] + Convert.ToDouble(positions[j]), 2);
                                    }

                                    if (fv.Top10Rate>0)
                                    {
                                        if (stock.P_E_Ratio > 0)
                                        {
                                            AvgPE = AvgPE + stock.P_E_Ratio * position / fv.Top10Rate;
                                        }
                                        else
                                        {
                                            AvgPE = AvgPE + 200 * position / fv.Top10Rate;
                                        }

                                        AvgPB = AvgPB + stock.P_B_Ratio * position / fv.Top10Rate;
                                    }
                                                                                                    
                                }
                                if (stock != null)
                                {
                                    if (StockR1days != "")
                                    {
                                        StockR1days = StockR1days + ",";
                                        StockIndustrys = StockIndustrys + ",";
                                        PEs = PEs + ",";
                                        Belongs = Belongs + ",";
                                    }
                                    StockR1days = StockR1days + stock.R1day.ToString();
                                    StockIndustrys = StockIndustrys + stock.IndustryName.ToString();
                                    PEs = PEs + stock.P_E_Ratio.ToString();
                                    Belongs = Belongs + GetBelong(stock);
                                }
                            }
                            fv.StockR1days = StockR1days;
                            fv.StockIndustrys = StockIndustrys;
                            fv.PEs = PEs;
                            fv.Belongs = Belongs;
                            industry_dic = industry_dic.OrderByDescending(o => o.Value).ToDictionary(p => p.Key, o => o.Value);
                            fv.IndustryJson = JsonConvert.SerializeObject(industry_dic);
                            AvgPE = Math.Round(AvgPE, 1);
                            AvgPB = Math.Round(AvgPB, 1);
                        }

                        fv.AvgPB = AvgPB;
                        fv.AvgPE = AvgPE;


                        fv.alpha = Math.Round(fv.R1year - 4 - hs300_r1year, 2);
                        



                        if (fundQuery.HasAnalysis != null)
                        {
                            if (Convert.ToBoolean(fundQuery.HasAnalysis))
                            {
                                double netValue = 0;
                                //double totalred = 0;
                                var netValueStr = da.Rows[i]["NetValue"].ToString();
                                //var totalUnitMoneyStr = da.Rows[i]["TotalUnitMoney"].ToString();
                                //if (double.TryParse(totalUnitMoneyStr, out totalred))
                                //{

                                //}
                                if (double.TryParse(netValueStr, out netValue))
                                {
                                    //fv.ReturnRate = Math.Round(((netValue / fv.Cost) - 1) * 100, 2);
                                }


                                DateTime updateTime;

                                if (DateTime.TryParse(da.Rows[i]["FundUpdateTime"].ToString(), out updateTime))
                                {

                                }

                                //Stopwatch watch = new Stopwatch();
                                //watch.Start();
                                var result = readConn.GetRedisData<LineReturn>(fv.FundNo);
                                if (result == null || updateTime > result.updatetime)
                                {
                                    result = LinearRegression.GetTrendEquation(fv.FundNo);
                                    if (result != null)
                                    {
                                        result.updatetime = updateTime;
                                        
                                        writeConn.SetRedisData(fv.FundNo, result);
                                    }
                                }

                                if (result != null)
                                {

                                    fv.D_Top = Math.Round(100 * (result.topValue - netValue) / result.topValue, 2);
                                    if (fv.D_Top < 0)
                                    {
                                        fv.D_Top = 0;
                                    }
                                    //av.D_Exp1 =Math.Round((netValue-(result.begins[3]) * result.points[0].X - result.points[0].Y) * 10000, 2);
                                    fv.D_Exp1 = Math.Round(100 * (netValue - (result.begins[4] - 1) * result.points[0].X - result.points[0].Y) / netValue, 2);
                                    fv.D_Exp2 = Math.Round(100 * (netValue - (result.begins[4] - 1) * result.points[1].X - result.points[1].Y) / netValue, 2);
                                    fv.D_Exp3 = Math.Round(100 * (netValue - (result.begins[4] - 1) * result.points[2].X - result.points[2].Y) / netValue, 2);
                                    fv.D_ExpM2 = Math.Round(100 * (netValue - (result.begins[4] - 1) * result.points[4].X - result.points[4].Y) / netValue, 2);
                                    fv.D_ExpM1 = Math.Round(100 * (netValue - (result.begins[4] - 1) * result.points[5].X - result.points[5].Y) / netValue, 2);
                                    fv.D_ExpD10 = Math.Round(100 * (netValue - (result.begins[4] - 1) * result.points[6].X - result.points[6].Y) / netValue, 2);
                                    fv.D_ExpD5 = Math.Round(100 * (netValue - (result.begins[4] - 1) * result.points[7].X - result.points[7].Y) / netValue, 2);

                                    double sumNet = 0;
                                    List<double> list5 = new List<double>();
                                    List<double> list10 = new List<double>();

                                    if (result.dataSet.Rows.Count > 60)
                                    {
                                        for (int k = 1; k <= 60; k++)
                                        {
                                            if (k <= 10)
                                            {
                                                if (k <= 5)
                                                {
                                                    list5.Add((double)result.dataSet.Rows[result.dataSet.Rows.Count - k][1]);
                                                }
                                                list10.Add((double)result.dataSet.Rows[result.dataSet.Rows.Count - k][1]);
                                            }
                                            sumNet = sumNet + (double)result.dataSet.Rows[result.dataSet.Rows.Count - k][1];
                                            //Console.WriteLine(result.dataSet.Rows[result.dataSet.Rows.Count - k][0]);
                                        }

                                        fv.D_A60 = Math.Round(100 * (netValue - sumNet / 60) / netValue, 2);
                                        fv.D_Max10D = Math.Round(100 * (netValue - list10.Max()) / netValue, 2);
                                        fv.D_Max5D = Math.Round(100 * (netValue - list5.Max()) / netValue, 2);
                                    }





                                    //double normalized = ((result.dataSet.Rows.Count - 1225) * result.points[3].X + result.points[3].Y);
                                    double normalized = 1;
                                    if (result.dataSet.Rows.Count >= 1225)
                                    {
                                        normalized = ((result.dataSet.Rows.Count - 1225) * result.points[3].X + result.points[3].Y);
                                    }
                                    else if (result.dataSet.Rows.Count < 1225 && result.dataSet.Rows.Count >= 735)
                                    {
                                        normalized = ((result.dataSet.Rows.Count - 735) * result.points[2].X + result.points[2].Y);
                                    }
                                    else if (result.dataSet.Rows.Count < 735 && result.dataSet.Rows.Count >= 490)
                                    {
                                        normalized = ((result.dataSet.Rows.Count - 490) * result.points[1].X + result.points[1].Y);
                                    }
                                    else if (result.dataSet.Rows.Count < 490 && result.dataSet.Rows.Count >= 245)
                                    {
                                        normalized = ((result.dataSet.Rows.Count - 245) * result.points[0].X + result.points[0].Y);
                                    }
                                    else if (result.dataSet.Rows.Count < 245)
                                    {
                                        normalized = result.points[0].Y;
                                    }

                                    if (result.dataSet.Rows.Count - 245 >= 0)
                                    {
                                        fv.Linear1 = Math.Round(result.points[0].X * 10000 / normalized, 2);
                                    }
                                    if (result.dataSet.Rows.Count - 490 >= 0)
                                    {
                                        fv.Linear2 = Math.Round(result.points[1].X * 10000 / normalized, 2);
                                    }
                                    if (result.dataSet.Rows.Count - 735 >= 0)
                                    {
                                        fv.Linear3 = Math.Round(result.points[2].X * 10000 / normalized, 2);
                                    }
                                    if (result.dataSet.Rows.Count - 1225 >= 0)
                                    {
                                        fv.Linear5 = Math.Round(result.points[3].X * 10000 / normalized, 2);
                                    }
                                    if (result.dataSet.Rows.Count - 30 >= 0)
                                    {
                                        fv.LinearM1 = Math.Round(result.points[5].X * 10000 / normalized, 2);
                                    }
                                    if (result.dataSet.Rows.Count - 60 >= 0)
                                    {
                                        fv.LinearM2 = Math.Round(result.points[4].X * 10000 / normalized, 2);
                                    }

                                }
                                else
                                {
                                    fv.D_Top = 0;
                                    fv.D_Exp1 = 0;
                                    fv.D_Exp2 = 0;
                                    fv.D_Exp3 = 0;
                                }
                            }

                        }

                        list.Add(fv);
                    }

                }

                writeConn.SetRedisData("FundViews", list);
                readConn.Close();
                writeConn.Close();


                //if (list.Count > 710)
                //{
                //    redisHelper.SetRedisData("FundViews", list);
                //}
                return list;
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public string SetFundString(FundQuery fundQuery)
        {
            string queryStr = fundQuery== null?"":"where ";
            //MyFundService myFundService = new MyFundService();
            queryStr += SetFundMainQueryStr(fundQuery);
            #region oldmethod
            //switch (fundQuery.own)
            //{
            //    case OWN.Empty:
            //        queryStr += SetFundMainQueryStr(fundQuery);
            //        break;
            //    case OWN.OnlyHolds:                   
            //        queryStr += SetSelsStr(myFundService.GetMyFundNos(1),true);
            //        break;
            //    case OWN.OnlyWaits:
            //        queryStr += SetSelsStr(myFundService.GetMyFundNos(2),true);
            //        break;
            //    case OWN.OnlyHoldWaits:
            //        queryStr += SetSelsStr(myFundService.GetMyFundNos(3),true);
            //        break;
            //    case OWN.Holds:
            //        queryStr += SetFundMainQueryStr(fundQuery);
            //        queryStr += SetSelsStr(myFundService.GetMyFundNos(1),false);
            //        queryStr += " mf.UserId="+ fundQuery.UserId+" and";
            //        break;
            //    case OWN.Waits:
            //        queryStr += SetFundMainQueryStr(fundQuery);
            //        queryStr += SetSelsStr(myFundService.GetMyFundNos(2), false);
            //        queryStr += " mf.UserId=" + fundQuery.UserId + " and";
            //        break;
            //    case OWN.HoldWaits:
            //        queryStr += SetFundMainQueryStr(fundQuery);
            //        queryStr += SetSelsStr(myFundService.GetMyFundNos(3), false);
            //        queryStr += " mf.UserId=" + fundQuery.UserId + " and";
            //        break;
            //}
            #endregion
            queryStr += " 1=1 ";
            return queryStr;

        }

        private string SetMyFundString(FundQuery fundQuery)
        {
            if (fundQuery.UserId>0)
            {
                return $"left join (select * from MyFunds where UserId={fundQuery.UserId}) mf on fi.FundNo=mf.FundNo ";
            }
            return "";
        }
        private string SetSelsStr(string sels,bool isOnly)
        {
            string ownStr = isOnly?"":"(";
            string[] strHolds = sels.Split(',');
            for (int i = 0; i < strHolds.Length; i++)
            {
                ownStr += $" fi.FundNo={strHolds[i]}";
                if (i < strHolds.Length - 1)
                {
                    ownStr += " or";
                }
                else
                {
                    if (!isOnly)
                    {
                        ownStr += ")";
                    }
                    ownStr += " and";
                }
            }
            return ownStr;
        }

        private string SetFundMainQueryStr(FundQuery fundQuery)
        {
            string queryStr = "";
            #region QueryCode
            if (!string.IsNullOrEmpty(fundQuery.FundNo))
            {
                queryStr += "fi.FundNo like '%" + fundQuery.FundNo + "%' and ";
            }
            if (!string.IsNullOrEmpty(fundQuery.FundName))
            {
                queryStr += "FundName like '%" + fundQuery.FundName + "%' and ";
            }
            if (fundQuery.MaxTotalFee != null)
            {
                queryStr += "(IFNULL(BuyRate, 0)+IFNULL(ManagerFee, 0)+IFNULL(CustodyFee, 0)+IFNULL(SaleFee, 0))<=" + fundQuery.MaxTotalFee + " and ";
            }
            if (fundQuery.MinTotalFee != null)
            {
                queryStr += "(IFNULL(BuyRate, 0)+IFNULL(ManagerFee, 0)+IFNULL(CustodyFee, 0)+IFNULL(SaleFee, 0))>=" + fundQuery.MinTotalFee + " and ";
            }
            if (fundQuery.MaxTotalScale != null)
            {
                queryStr += $"TotalScale<={fundQuery.MaxTotalScale} and ";
            }
            if (fundQuery.MinTotalScale != null)
            {
                queryStr += $"TotalScale>={fundQuery.MinTotalScale} and ";
            }
            if (fundQuery.MaxOrgHoldRate != null)
            {
                queryStr += $"OrgHoldRate<={fundQuery.MaxOrgHoldRate} and ";
            }
            if (fundQuery.MinOrgHoldRate != null)
            {
                queryStr += $"OrgHoldRate>={fundQuery.MinOrgHoldRate} and ";
            }
            if (fundQuery.MaxR1week != null)
            {
                queryStr += $"R1week<={fundQuery.MaxR1week} and ";
            }
            if (fundQuery.MinR1week != null)
            {
                queryStr += $"R1week>={fundQuery.MinR1week} and ";
            }
            if (fundQuery.MaxR1month != null)
            {
                queryStr += $"R1month<={fundQuery.MaxR1month} and ";
            }
            if (fundQuery.MinR1month != null)
            {
                queryStr += $"R1month>={fundQuery.MinR1month} and ";
            }
            if (fundQuery.MaxR3month != null)
            {
                queryStr += $"R3month<={fundQuery.MaxR3month} and ";
            }
            if (fundQuery.MinR3month != null)
            {
                queryStr += $"R3month>={fundQuery.MinR3month} and ";
            }
            if (fundQuery.MaxR6month != null)
            {
                queryStr += $"R6month<={fundQuery.MaxR6month} and ";
            }
            if (fundQuery.MinR6month != null)
            {
                queryStr += $"R6month>={fundQuery.MinR6month} and ";
            }
            if (fundQuery.MaxR1year != null)
            {
                queryStr += $"fi.R1year<={fundQuery.MaxR1year} and ";
            }
            if (fundQuery.MinR1year != null)
            {
                queryStr += $"fi.R1year>={fundQuery.MinR1year} and ";
            }
            if (fundQuery.MaxR2year != null)
            {
                queryStr += $"fi.R2year<={fundQuery.MaxR2year} and ";
            }
            if (fundQuery.MinR2year != null)
            {
                queryStr += $"fi.R2year>={fundQuery.MinR2year} and ";
            }
            if (fundQuery.MaxR3year != null)
            {
                queryStr += $"fi.R3year<={fundQuery.MaxR3year} and ";
            }
            if (fundQuery.MinR3year != null)
            {
                queryStr += $"fi.R3year>={fundQuery.MinR3year} and ";
            }
            if (fundQuery.MaxR5year != null)
            {
                queryStr += $"fi.R5year<={fundQuery.MaxR5year} and ";
            }
            if (fundQuery.MinR5year != null)
            {
                queryStr += $"fi.R5year>={fundQuery.MinR5year} and ";
            }
            if (fundQuery.MaxOver7dFee != null)
            {
                queryStr += $"Over7dFee<={fundQuery.MaxOver7dFee} and ";
            }
            if (fundQuery.MinOver7dFee != null)
            {
                queryStr += $"Over7dFee>={fundQuery.MinOver7dFee} and ";
            }
            if (fundQuery.MaxOver30dFee != null)
            {
                queryStr += $"Over30dFee<={fundQuery.MaxOver30dFee} and ";
            }
            if (fundQuery.MinOver30dFee != null)
            {
                queryStr += $"Over30dFee>={fundQuery.MinOver30dFee} and ";
            }
            if (fundQuery.MaxOver1yFee != null)
            {
                queryStr += $"Over1yFee<={fundQuery.MaxOver1yFee} and ";
            }
            if (fundQuery.MinOver1yFee != null)
            {
                queryStr += $"Over1yFee>={fundQuery.MinOver1yFee} and ";
            }
            if (fundQuery.MaxOver2yFee != null)
            {
                queryStr += $"Over2yFee<={fundQuery.MaxOver2yFee} and ";
            }
            if (fundQuery.MinOver2yFee != null)
            {
                queryStr += $"Over2yFee>={fundQuery.MinOver2yFee} and ";
            }
            if (fundQuery.MaxStockRate != null)
            {
                queryStr += $"StockRate<={fundQuery.MaxStockRate} and ";
            }
            if (fundQuery.MinStockRate != null)
            {
                queryStr += $"StockRate>={fundQuery.MinStockRate} and ";
            }
            if (fundQuery.MaxR2_1y != null)
            {
                queryStr += $"(R2year-R1year)<={fundQuery.MaxR2_1y} and ";
            }
            if (fundQuery.MinR2_1y != null)
            {
                queryStr += $"(R2year-R1year)>={fundQuery.MinR2_1y} and ";
            }
            if (fundQuery.MaxR3_2y != null)
            {
                queryStr += $"(R3year-R2year)<={fundQuery.MaxR3_2y} and ";
            }
            if (fundQuery.MinR3_2y != null)
            {
                queryStr += $"(R3year-R2year)>={fundQuery.MinR3_2y} and ";
            }
            if (fundQuery.MaxR5_3y != null)
            {
                queryStr += $"(R5year-R3year)<={fundQuery.MaxR5_3y} and ";
            }
            if (fundQuery.MinR5_3y != null)
            {
                queryStr += $"(R5year-R3year)>={fundQuery.MinR5_3y} and ";
            }
            if (fundQuery.MaxDutyDate != null)
            {
                string formatDate = fundQuery.MaxDutyDate.ToString("yyyy-MM-dd");
                //Console.WriteLine(formatDate);
                queryStr += $"DutyDate<='{formatDate}' and ";
            }
            if (fundQuery.MinDutyDate != null)
            {
                string formatDate = fundQuery.MinDutyDate.ToString("yyyy-MM-dd");
                //Console.WriteLine(formatDate);
                queryStr += $"DutyDate>='{formatDate}' and ";
            }
            if (fundQuery.MinRank != null)
            {
                queryStr += $"Rank_Id>={fundQuery.MinRank} and ";
            }
            if (fundQuery.MaxRank != null)
            {
                queryStr += $"Rank_Id<={fundQuery.MaxRank} and ";
            }
            if (fundQuery.isnew)
            {
                string formatDate = DateTime.Now.ToString("yyyy-MM-dd");
                queryStr += $"FundUpdateTime>='{formatDate}' and ";
            }
            if (fundQuery.UserId>0)
            {
                if (fundQuery.Status == 1 || fundQuery.Status == 2)
                {
                    queryStr += $"mf.Status={fundQuery.Status} and ";
                }
                else if(fundQuery.Status == 3)
                {
                    queryStr += $"mf.Status>0 and ";
                }
            }
            if (fundQuery.bondstock == 1)
            {
                queryStr += "FundType='bond' and ";
            }
            else if (fundQuery.bondstock == 2)
            {
                queryStr += "FundType!='bond' and FundType is not NULL and ";
            }
            if (!string.IsNullOrEmpty(fundQuery.Strategy))
            {
                string[] strategy = fundQuery.Strategy.Split(',');
                foreach (var item in strategy)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        queryStr += "Strategy like '%" + item + "%' and ";
                    }
                }
            }
            #endregion
            return queryStr;
        }


        public List<StrategyView> GetStrategyByQuery(StrategyQuery strategyQuery)
        {
            List<StrategyView> stList = new List<StrategyView>();
            //RedisHelper redisHelper = new RedisHelper(AppConfigurtaionServices.Configuration.GetConnectionString("RedisConnection"));

            RedisConn readConn = new RedisConn(true);

            //var redisResult = redisHelper.GetRedisData<List<StrategyView>>("StrategyViews");
            //if (redisResult != null && redisResult.Count > 0)
            //{
            //    if (!string.IsNullOrEmpty(strategyQuery.Strategy))
            //    {
            //        redisResult = redisResult.Where(x => x.Strategy.Contains(strategyQuery.Strategy)).ToList();
            //    }
            //    if (strategyQuery.isnew)
            //    {
            //        redisResult = redisResult.Where(x => DateHelper.getFormatDateTime(DateTime.Now) == DateHelper.getFormatDateTime(x.FundUpdateTime)).ToList();
            //    }
            //    if (!string.IsNullOrEmpty(strategyQuery.FundName))
            //    {
            //        redisResult = redisResult.Where(x => x.FundName.Contains(strategyQuery.FundName)).ToList();
            //    }
            //    if (strategyQuery.bondstock == 1)
            //    {
            //        redisResult = redisResult.Where(x => x.StockRate <= 30 && !x.FundName.Contains("ETF") && !x.FundName.Contains("指数")).ToList();
            //    }
            //    else if (strategyQuery.bondstock == 2)
            //    {
            //        redisResult = redisResult.Where(x => x.StockRate >= 30 || (x.FundName.Contains("ETF") || x.FundName.Contains("指数"))).ToList();
            //    }
            //    if (strategyQuery.status == 1 || strategyQuery.status == 2)
            //    {
            //        redisResult = redisResult.Where(x => x.status == strategyQuery.status).ToList();
            //    }
            //    else if (strategyQuery.status == 3)
            //    {
            //        redisResult = redisResult.Where(x => x.status > 0).ToList();
            //    }
            //    return redisResult;
            //}

            string sql = "select fi.FundNo,FundName,Strategy,FundType,StockRate,Investment,HoldShares,TotalBonus,Cost,ReturnRate,R1day,R1week,R1month,fi.FundScore,fi.ManagerScore,DutyDate,NetValue,TotalScale," +
                         "Estimate,ValuationScore,Maxretra,fi.Maxretra5,FundUpdateTime,StockNos,StockNames,StockPositions,Rank_Id,mf.Status from FundInfos fi right join MyFunds mf on fi.FundNo = mf.FundNo left join MyScore ms on " +
                         "fi.FundNo=ms.FundNo left join Valuations vt on ValuationId=vt.Id";
            sql = sql + SetStrategyStr(strategyQuery) + " order by R1day desc";
            MySqlParameter[] pars = null;
            DataTable da = MySqlHelper.ExecuteTable(sql, pars);
            float outSy = 0;
            int outMf = 0;
            try
            {
                if (da.Rows.Count > 0)
                {
                    for (int i = 0; i < da.Rows.Count; i++)
                    {
                        StrategyView sv = new StrategyView();
                        sv.FundNo = da.Rows[i]["FundNo"].ToString();
                        sv.FundName = da.Rows[i]["FundName"].ToString();
                        sv.Strategy = da.Rows[i]["Strategy"].ToString();
                        sv.StockNos = da.Rows[i]["StockNos"].ToString();
                        sv.StockNames = da.Rows[i]["StockNames"].ToString();
                        sv.StockPositions = da.Rows[i]["StockPositions"].ToString();
                        if (float.TryParse(da.Rows[i]["R1day"].ToString(), out outSy))
                        {
                            float temp = (float)da.Rows[i]["R1day"];
                            sv.R1day = (float)Math.Round(temp, 2);
                        }
                        if (float.TryParse(da.Rows[i]["R1week"].ToString(), out outSy))
                        {
                            float temp = (float)da.Rows[i]["R1week"];
                            sv.R1week = (float)Math.Round(temp, 2);
                        }
                        if (float.TryParse(da.Rows[i]["R1month"].ToString(), out outSy))
                        {
                            sv.R1month = Math.Round(outSy, 2);
                        }
                        if (int.TryParse(da.Rows[i]["Investment"].ToString(), out outMf))
                        {
                            sv.Investment = (int)da.Rows[i]["Investment"];
                        }
                        if (float.TryParse(da.Rows[i]["TotalScale"].ToString(), out outSy))
                        {
                            sv.TotalScale = Math.Round(outSy, 1);
                        }
                        if (int.TryParse(da.Rows[i]["Rank_Id"].ToString(), out outMf))
                        {
                            sv.Rank_Id = (int)da.Rows[i]["Rank_Id"];
                        }
                        else
                        {
                            sv.Rank_Id = 9999;
                        }
                        if (float.TryParse(da.Rows[i]["StockRate"].ToString(), out outSy))
                        {
                            sv.StockRate = (float)da.Rows[i]["StockRate"];
                        }
                        //if (float.TryParse(da.Rows[i]["ReturnRate"].ToString(), out outSy))
                        //{
                        //    double viewrr = Math.Round(Convert.ToDouble(da.Rows[i]["ReturnRate"]) * 100, 2);
                        //    sv.ReturnRate = viewrr;
                        //}
                        if (float.TryParse(da.Rows[i]["FundScore"].ToString(), out outSy))
                        {
                            sv.FundScore = (float)da.Rows[i]["FundScore"];
                        }
                        if (float.TryParse(da.Rows[i]["ManagerScore"].ToString(), out outSy))
                        {
                            sv.ManagerScore = (float)da.Rows[i]["ManagerScore"];
                        }
                        //if (float.TryParse(da.Rows[i]["Maxretra"].ToString(), out outSy))
                        //{
                        //    sv.Maxretra = (float)da.Rows[i]["Maxretra"];
                        //}
                        if (float.TryParse(da.Rows[i]["Maxretra"].ToString(), out outSy))
                        {
                            sv.Maxretra = Math.Round(Convert.ToDouble(da.Rows[i]["Maxretra"]), 2);
                        }
                        if (float.TryParse(da.Rows[i]["Maxretra5"].ToString(), out outSy))
                        {
                            sv.Maxretra5 = Math.Round(Convert.ToDouble(da.Rows[i]["Maxretra5"]), 2);
                        }
                        if (float.TryParse(da.Rows[i]["Estimate"].ToString(), out outSy))
                        {
                            sv.Estimate = (double)da.Rows[i]["Estimate"];
                        }
                        if (float.TryParse(da.Rows[i]["Cost"].ToString(), out outSy))
                        {
                            sv.Cost = (double)da.Rows[i]["Cost"];
                        }
                        if (float.TryParse(da.Rows[i]["NetValue"].ToString(), out outSy))
                        {
                            double viewrr = Math.Round(Convert.ToDouble(da.Rows[i]["NetValue"]), 4);
                            sv.UnitValue = viewrr;
                            //sv.UnitValue = (double)da.Rows[i]["NetValue"];
                        }
                        if (float.TryParse(da.Rows[i]["TotalBonus"].ToString(), out outSy))
                        {
                            sv.TotalBonus = (double)da.Rows[i]["TotalBonus"];
                        }
                        if (float.TryParse(da.Rows[i]["HoldShares"].ToString(), out outSy))
                        {
                            sv.HoldShares = (double)da.Rows[i]["HoldShares"];
                        }
                        if (float.TryParse(da.Rows[i]["ValuationScore"].ToString(), out outSy))
                        {
                            sv.ValuationScore = (double)da.Rows[i]["ValuationScore"];
                        }
                        if (int.TryParse(da.Rows[i]["Status"].ToString(), out outMf))
                        {
                            sv.status = (int)da.Rows[i]["Status"];
                        }
                        
                        try
                        {
                            sv.DutyDate = Convert.ToDateTime(da.Rows[i]["DutyDate"]);
                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine(ex);
                        }
                        sv.FundUpdateTime = Convert.ToDateTime(da.Rows[i]["FundUpdateTime"]);

                        

                        if (sv.Investment!=null)
                        {
                            //sv.CurrentHold = (int)(sv.Investment * (1 + sv.ReturnRate / 100));
                            sv.CurrentHold = Convert.ToInt32(sv.HoldShares * sv.UnitValue);
                        }
                        

                        double netValue = 0;
                        //double totalred = 0;
                        var netValueStr = da.Rows[i]["NetValue"].ToString();
                        //var totalUnitMoneyStr = da.Rows[i]["TotalUnitMoney"].ToString();
                        if (!double.TryParse(netValueStr, out netValue))
                        {
                            
                        }

                        double totalshares = 0;
                        double totalbonus = 0;
                        if (float.TryParse(da.Rows[i]["HoldShares"].ToString(), out outSy))
                        {
                            totalshares = Math.Round(Convert.ToDouble(da.Rows[i]["HoldShares"]), 4);
                        }
                        if (float.TryParse(da.Rows[i]["TotalBonus"].ToString(), out outSy))
                        {
                            totalbonus = Math.Round(Convert.ToDouble(da.Rows[i]["TotalBonus"]), 4);
                        }


                        if (sv.Cost != 0 && totalshares != 0)
                        {
                            sv.ReturnRate = Math.Round((((totalshares * sv.UnitValue + totalbonus) / (sv.Cost * totalshares)) - 1) * 100, 2);
                        }
                        else
                        {
                            sv.ReturnRate = 0;
                        }

                        //if (!double.TryParse(totalUnitMoneyStr, out totalred))
                        //{

                        //}

                        if (sv.FundUpdateTime < DateHelper.getFormatDateTime(DateTime.Now))
                        {
                            netValue = netValue * (1 + (sv.Estimate/100));
                        }

                        DateTime updateTime;

                        if (!DateTime.TryParse(da.Rows[i]["FundUpdateTime"].ToString(), out updateTime))
                        {

                        }


                        var result = readConn.GetRedisData<LineReturn>(sv.FundNo);
                        if (result == null || updateTime > result.updatetime)
                        {
                            result = LinearRegression.GetTrendEquation(sv.FundNo);
                            if (result != null)
                            {
                                result.updatetime = updateTime;
                                RedisConn writeConn = new RedisConn(false);

                                writeConn.SetRedisData(sv.FundNo, result);
                                writeConn.Close();
                            }
                        }


                        if (result != null)
                        {
                            sv.D_Top = Math.Round(100 * (result.topValue - netValue) / result.topValue, 2);
                            //sv.D_Top = Math.Round(100 * (result.topValue - netValue) / (result.topValue - totalred), 2);
                            if (sv.D_Top < 0)
                            {
                                sv.D_Top = 0;
                            }
                            //av.D_Exp1 =Math.Round((netValue-(result.begins[3]) * result.points[0].X - result.points[0].Y) * 10000, 2);
                            sv.D_Exp1 = Math.Round(100 * (netValue - (result.begins[4]-1) * result.points[0].X - result.points[0].Y) / netValue, 2);
                            sv.D_Exp2 = Math.Round(100 * (netValue - (result.begins[4]-1) * result.points[1].X - result.points[1].Y) / netValue, 2);
                            sv.D_Exp3 = Math.Round(100 * (netValue - (result.begins[4]-1) * result.points[2].X - result.points[2].Y) / netValue, 2);
                            sv.D_ExpM2 = Math.Round(100 * (netValue - (result.begins[4] - 1) * result.points[4].X - result.points[4].Y) / netValue, 2);
                            sv.D_ExpM1 = Math.Round(100 * (netValue - (result.begins[4] - 1) * result.points[5].X - result.points[5].Y) / netValue, 2);
                            sv.D_ExpD10 = Math.Round(100 * (netValue - (result.begins[4] - 1) * result.points[6].X - result.points[6].Y) / netValue, 2);
                            sv.D_ExpD5 = Math.Round(100 * (netValue - (result.begins[4] - 1) * result.points[7].X - result.points[7].Y) / netValue, 2);

                            double sumNet = 0;

                            List<double> list5 = new List<double>();
                            List<double> list10 = new List<double>();

                            if (result.dataSet.Rows.Count > 60)
                            {
                                for (int k = 1; k <= 60; k++)
                                {
                                    if (k <= 10)
                                    {
                                        if (k <= 5)
                                        {
                                            list5.Add((double)result.dataSet.Rows[result.dataSet.Rows.Count - k][1]);
                                        }
                                        list10.Add((double)result.dataSet.Rows[result.dataSet.Rows.Count - k][1]);
                                    }
                                    sumNet = sumNet + (double)result.dataSet.Rows[result.dataSet.Rows.Count - k][1];
                                    //Console.WriteLine(result.dataSet.Rows[result.dataSet.Rows.Count - k][0]);
                                }
                                sv.D_A60 = Math.Round(100 * (netValue - (sumNet / 60)) / netValue, 2);

                                sv.D_Max10D = Math.Round(100 * (netValue - list10.Max()) / netValue, 2);
                                sv.D_Max5D = Math.Round(100 * (netValue - list5.Max()) / netValue, 2);
                            }

                            



                            double normalized = 1;
                            if (result.dataSet.Rows.Count >= 1225)
                            {
                                normalized = ((result.dataSet.Rows.Count - 1225) * result.points[3].X + result.points[3].Y);
                            }
                            else if (result.dataSet.Rows.Count < 1225 && result.dataSet.Rows.Count >= 735)
                            {
                                normalized = ((result.dataSet.Rows.Count - 735) * result.points[2].X + result.points[2].Y);
                            }
                            else if (result.dataSet.Rows.Count < 735 && result.dataSet.Rows.Count >= 490)
                            {
                                normalized = ((result.dataSet.Rows.Count - 490) * result.points[1].X + result.points[1].Y);
                            }
                            else if (result.dataSet.Rows.Count < 490 && result.dataSet.Rows.Count >= 245)
                            {
                                normalized = ((result.dataSet.Rows.Count - 245) * result.points[0].X + result.points[0].Y);
                            }
                            else if (result.dataSet.Rows.Count < 245)
                            {
                                normalized = result.points[0].Y;
                            }

                            if (result.dataSet.Rows.Count - 30 >= 0)
                            {
                                sv.LinearM1 = Math.Round(result.points[5].X * 10000 / normalized, 2);
                            }
                            if (result.dataSet.Rows.Count - 60 >= 0)
                            {
                                sv.LinearM2 = Math.Round(result.points[4].X * 10000 / normalized, 2);//error
                            }
                        }
                        else
                        {
                            sv.D_Top = 0;
                            sv.D_Exp1 = 0;
                            sv.D_Exp2 = 0;
                            sv.D_Exp3 = 0;
                            sv.D_ExpM2 = 0;
                        }
                        stList.Add(sv);
                    }

                }
                readConn.Close();
                return stList;
            }
            catch (Exception ex)
            {
                readConn.Close();
                throw;
            }
        }

        private string SetStrategyStr(StrategyQuery strategyQuery)
        {
            string queryStr = strategyQuery == null ? "" : " where ";
            //if (!string.IsNullOrEmpty(strategyQuery.FundName))
            //{
            //        queryStr += "FundName like '%" + strategyQuery.FundName + "%' and ";
            //}
            if (!string.IsNullOrEmpty(strategyQuery.FundName))
            {
                string[] fundNames = strategyQuery.FundName.Split(',');
                if (fundNames.Length>1)
                {
                    queryStr += "(";
                    for (int i = 0; i < fundNames.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(fundNames[i]))
                        {
                            queryStr += "FundName like '%" + fundNames[i] + "%'";
                        }
                        if (i == fundNames.Length - 1)
                        {
                            queryStr += ") and ";
                        }
                        else
                        {
                            queryStr += " or ";
                        }
                    }
                }
                else
                {
                    queryStr += "FundName like '%" + fundNames[0] + "%' and ";
                }
                
                //foreach (var item in fundNames)
                //{
                //    if (!string.IsNullOrEmpty(item))
                //    {
                //        queryStr += "FundName like '%" + item + "%' or ";
                //    }
                //}
            }
            if (!string.IsNullOrEmpty(strategyQuery.Strategy))
            {
                string[] strategy = strategyQuery.Strategy.Split(',');
                foreach (var item in strategy)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        queryStr += "Strategy like '%" + item + "%' and ";
                    }                   
                }
            }
            if (strategyQuery.isnew)
            {
                string formatDate = DateTime.Now.ToString("yyyy-MM-dd");
                queryStr += $"FundUpdateTime>='{formatDate}' and ";
            }
            if (strategyQuery.bondstock == 1)
            {
                //queryStr += "StockRate<50 and FundName not like '%ETF%' and FundName not like '%指数%' and ";
                queryStr += "FundType='bond' and ";
            }
            else if (strategyQuery.bondstock == 2)
            {
                //queryStr += "(StockRate>=50 or (FundName like '%ETF%' or FundName like '%指数%')) and ";
                queryStr += "FundType!='bond' and FundType is not NULL and ";
            }
            if (strategyQuery.status<=2)
            {
                queryStr += "status=" + strategyQuery.status.ToString()+" and ";
            }
            queryStr += "UserId=" + strategyQuery.UserId.ToString()+" ";           
            return queryStr;
        }

        public StrategyTitle GetStrategyTitle()
        {
            //StrategyTitle strategyTitle = new StrategyTitle();
            //RedisHelper redisHelper = new RedisHelper(AppConfigurtaionServices.Configuration.GetConnectionString("RedisConnection"));
            //string timestr = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            //string todaystr = DateTime.Now.ToString("yyyy-MM-dd");
            //string sql = "";
            //MySqlParameter[] pars = null;
            //DataTable da;

            //var result = redisHelper.GetRedisData<StrategyTitle>(todaystr+"-title");

            //if (result != null && result.AR1day!=0 && result.CR1day!=0 && result.SR1day!=0)
            //{
            //    strategyTitle.AR1day = result.AR1day;
            //    strategyTitle.CR1day = result.CR1day;
            //    strategyTitle.SR1day = result.SR1day;
            //}
            //else
            //{
            //    using (MyContext myContext = new MyContext())
            //    {
            //        var fundInfos = myContext.FundInfos.Where(x => x.FundNo=="444444" || x.FundNo == "555555" || x.FundNo == "666666").ToList();
            //        strategyTitle.AR1day = Math.Round((double)fundInfos.Single(x => x.FundNo == "444444").R1day,2);
            //        strategyTitle.CR1day = Math.Round((double)fundInfos.Single(x => x.FundNo == "555555").R1day,2);
            //        strategyTitle.SR1day = Math.Round((double)fundInfos.Single(x => x.FundNo == "666666").R1day,2);
            //    }
            //}


            //if (result != null && result.XYGRate !=0 && result.KJRate!=0 && result.MyStockRate!=0 && result.XYGReturn!=0 && result.RankReturn!=0 && result.KJReturn!=0 && result.MyReturn!=0 && result.SumStock!=0)
            //{
            //    strategyTitle.XYGRate = result.XYGRate;
            //    strategyTitle.KJRate = result.KJRate;
            //    strategyTitle.MyStockRate = result.MyStockRate;
            //    strategyTitle.XYGReturn = result.XYGReturn;
            //    strategyTitle.KJReturn = result.KJReturn;
            //    strategyTitle.MyStockReturn = result.MyStockReturn;
            //    strategyTitle.MyBondReturn = result.MyBondReturn;
            //    strategyTitle.MyReturn = result.MyReturn;
            //    strategyTitle.RankReturn = result.RankReturn;
            //    strategyTitle.SumStock = result.SumStock;
            //}
            //else
            //{
            //    //sql = "select sum(Investment*StockRate/100) from FundInfos fi right join MyFunds mf on fi.FundNo=mf.FundNo where StockRate<1 and fi.FundName not like '%ETF%';";
            //    //da = MySqlHelper.ExecuteTable(sql, pars);
            //    //double stock1 = Convert.ToDouble(da.Rows[0][0].ToString());

            //    //sql = "select sum(Investment*StockRate/100) from FundInfos fi right join MyFunds mf on fi.FundNo=mf.FundNo where 1<StockRate and StockRate<50 and fi.FundName not like '%ETF%';";
            //    //da = MySqlHelper.ExecuteTable(sql, pars);
            //    //double stock2 = Convert.ToDouble(da.Rows[0][0].ToString());

            //    sql = "select sum(Investment*StockRate/100) from FundInfos fi right join MyFunds mf on fi.FundNo=mf.FundNo where 0<StockRate and StockRate<30 and fi.FundName not like '%ETF%';";
            //    da = MySqlHelper.ExecuteTable(sql, pars);
            //    double stock3 = Convert.ToDouble(da.Rows[0][0].ToString());

            //    sql = "select sum(Investment) from FundInfos fi right join MyFunds mf on fi.FundNo=mf.FundNo where 30<=StockRate or fi.FundName like '%ETF%';";
            //    da = MySqlHelper.ExecuteTable(sql, pars);
            //    double stock4 = Convert.ToDouble(da.Rows[0][0].ToString());

            //    double sumstock = stock3 + stock4;

            //    strategyTitle.SumStock = sumstock;

            //    sql = "select sum(Investment) from MyFunds where Strategy like '%XYG%'";
            //    da = MySqlHelper.ExecuteTable(sql, pars);
            //    double XYGStock = Convert.ToDouble(da.Rows[0][0].ToString());
            //    strategyTitle.XYGRate = Math.Round(XYGStock * 100 / sumstock, 2);

            //    sql = "select sum(Investment) from MyFunds where Strategy like '%KJ%'";
            //    da = MySqlHelper.ExecuteTable(sql, pars);
            //    double KJStock = Convert.ToDouble(da.Rows[0][0].ToString());
            //    strategyTitle.KJRate = Math.Round(KJStock * 100 / sumstock, 2);

            //    sql = "select sum(Investment) from MyFunds";
            //    da = MySqlHelper.ExecuteTable(sql, pars);
            //    double totalinvestment = Convert.ToDouble(da.Rows[0][0].ToString());
            //    strategyTitle.MyStockRate = Math.Round(sumstock*100/totalinvestment, 2);

            //    sql = "select sum(Investment*ReturnRate) from FundInfos fi right join MyFunds mf on fi.FundNo=mf.FundNo where Strategy like '%XYG%'";
            //    da = MySqlHelper.ExecuteTable(sql, pars);
            //    double xygreturn = Convert.ToDouble(da.Rows[0][0].ToString());
            //    strategyTitle.XYGReturn = Math.Round(xygreturn*100/XYGStock, 2);

            //    sql = "select sum(Investment*ReturnRate) from FundInfos fi right join MyFunds mf on fi.FundNo=mf.FundNo where Strategy like '%KJ%'";
            //    da = MySqlHelper.ExecuteTable(sql, pars);
            //    double kjreturn = Convert.ToDouble(da.Rows[0][0].ToString());
            //    strategyTitle.KJReturn = Math.Round(kjreturn*100/ KJStock, 2);

            //    sql = "select sum(Investment*ReturnRate) from FundInfos fi right join MyFunds mf on fi.FundNo=mf.FundNo where StockRate>=30 or FundName like '%ETF%'";
            //    da = MySqlHelper.ExecuteTable(sql, pars);
            //    double mystockreturn = Convert.ToDouble(da.Rows[0][0].ToString());
            //    strategyTitle.MyStockReturn = Math.Round(mystockreturn * 100/sumstock, 2);

            //    sql = "select sum(Investment*ReturnRate) from FundInfos fi right join MyFunds mf on fi.FundNo=mf.FundNo where StockRate<30 and FundName not like '%ETF%'";
            //    da = MySqlHelper.ExecuteTable(sql, pars);
            //    double mybondreturn = Convert.ToDouble(da.Rows[0][0].ToString());
            //    sql = "select sum(Investment) from MyFunds where status=1";
            //    da = MySqlHelper.ExecuteTable(sql, pars);
            //    double total = Convert.ToDouble(da.Rows[0][0].ToString());
            //    strategyTitle.MyBondReturn = Math.Round(mybondreturn * 100 / (total- sumstock), 2);

            //    sql = "select sum(Investment) from MyFunds where Strategy like '%Rank%'";
            //    da = MySqlHelper.ExecuteTable(sql, pars);
            //    double rankStock = Convert.ToDouble(da.Rows[0][0].ToString());
            //    sql = "select sum(Investment*ReturnRate) from FundInfos fi right join MyFunds mf on fi.FundNo=mf.FundNo where Strategy like '%Rank%'";
            //    da = MySqlHelper.ExecuteTable(sql, pars);
            //    double rankReturn = Convert.ToDouble(da.Rows[0][0].ToString());
            //    strategyTitle.RankReturn = Math.Round(rankReturn * 100 / rankStock, 2);

            //    strategyTitle.MyReturn = Math.Round((mybondreturn + mystockreturn)*100/total,2);
            //}

            //redisHelper.SetRedisData<StrategyTitle>(todaystr+"-title",strategyTitle);
            //redisHelper.Close();
            //return strategyTitle;

            return null;
        }
        public object GetProcessPercent()
        {
            //List<string> uf = new List<string>();
            try
            {
                string todaystr = DateTime.Now.ToString("yyyy-MM-dd");

                string sql = "select count(FundNo) from MyFunds where status=1 and FundNo !='000345'";
                MySqlParameter[] pars = null;
                DataTable da = MySqlHelper.ExecuteTable(sql, pars);
                double mytotal = Convert.ToDouble(da.Rows[0][0].ToString());
                //for (int i=0;i<da.Rows.Count; i++)
                //{
                //    uf.Add(da.Rows[i][0].ToString());
                //}

                sql = "select count(mf.FundNo) from FundDailyIDetails fd left join MyFunds mf on fd.FundNo=mf.FundNo where fd.DataTime='" + todaystr + " 00:00:00' and mf.status=1 and mf.FundNo !='000345' and fd.FundNo!='444444'";               
                da = MySqlHelper.ExecuteTable(sql, pars);
                double myvaluefinish = Convert.ToDouble(da.Rows[0][0].ToString());
                //for (int i = 0; i < da.Rows.Count; i++)
                //{
                //    uf.Remove(da.Rows[i][0].ToString());
                //}

                sql = "select count(FundNo) from FundInfos where BuyRate>-1";
                da = MySqlHelper.ExecuteTable(sql, pars);
                double total = Convert.ToDouble(da.Rows[0][0].ToString());

                sql = "select count(FundNo) from FundInfos where and BuyRate>-1 and FundUpdateTime='" + todaystr + "'";
                da = MySqlHelper.ExecuteTable(sql, pars);
                double infofiinsh = Convert.ToDouble(da.Rows[0][0].ToString());

                sql = "select count(FundNo) from FundDailyIDetails where DataTime='" + todaystr + " 00:00:00' and FundNo!='444444' and FundNo!='555555' and FundNo!='666666' and FundNo!='000345'";
                da = MySqlHelper.ExecuteTable(sql, pars);
                double valuefinish = Convert.ToDouble(da.Rows[0][0].ToString());

                var result = new
                {
                    processmy = Math.Round(myvaluefinish * 100 / mytotal, 2),
                    processinfo = Math.Round(infofiinsh * 100 / total, 2),
                    processvalue = Math.Round(valuefinish * 100 / total, 2)
                };
                return result;
            }
            catch (Exception ex)
            {

                return new { processmy = 0, process = 0 };
            }
            
        }

        public List<FundTip> GetFundNameTips(string fundstr)
        {

            using (MyContext myContext = new MyContext())
            {
                //RedisHelper redisHelper = new RedisHelper(AppConfigurtaionServices.Configuration.GetConnectionString("RedisConnection"));
                RedisConn readConn = new RedisConn(true);
                var fundtips = readConn.GetRedisData<List<FundTip>>("FundTips");
                if (fundtips == null || fundstr == "reset")
                {
                    RedisConn writeConn = new RedisConn(false);
                    fundtips = new List<FundTip>();
                    string sql = "select FundNo,FundName,StockNos,StockPositions from FundInfos where FundType!='bond' and FundType is not NULL and BuyRate>-1";
                    MySqlParameter[] pars = null;
                    DataTable da = MySqlHelper.ExecuteTable(sql, pars);
                    //var fundInfos = myContext.FundInfos.Where(x => x.BuyRate > -1 && x.FundType != "bond" && x.FundType != null);//.ToList();
                    for (int i = 0; i < da.Rows.Count; i++)
                    {
                        try
                        {
                            fundtips.Add(new FundTip()
                            {
                                FundNo = da.Rows[i]["FundNo"].ToString(),
                                FundName = da.Rows[i]["FundName"].ToString(),
                                StockNos = da.Rows[i]["StockNos"].ToString(),
                                StockPositions = da.Rows[i]["StockPositions"].ToString()
                            });
                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine(ex.Message);
                        }
                        
                    }
                    writeConn.SetRedisData("FundTips", fundtips);
                    writeConn.Close();
                }

                //if (MyUtils.HasChinese(fundstr))
                //{
                //     res = myContext.FundInfos.Where(x => x.FundName.Contains(fundstr)).ToList();
                //}
                //else
                //{
                //     res = myContext.FundInfos.Where(x => x.FundNo.Contains(fundstr)).ToList();
                //}
                readConn.Close();
               
                return fundtips;
            }
            
        }
        public List<FundPureTip> GetFundPureTips(string fundstr)
        {

            using (MyContext myContext = new MyContext())
            {
                //RedisHelper redisHelper = new RedisHelper(AppConfigurtaionServices.Configuration.GetConnectionString("RedisConnection"));
                RedisConn readConn = new RedisConn(true);
                var fundPureTips = readConn.GetRedisData<List<FundPureTip>>("FundPureTips");
                if (fundPureTips == null || fundstr == "reset")
                {
                    fundPureTips = new List<FundPureTip>();
                    //var fundInfos = myContext.FundInfos.Where(x => x.BuyRate > -1).ToList();
                    string sql = "select FundNo,FundName from FundInfos where BuyRate>-1";
                    MySqlParameter[] pars = null;
                    DataTable da = MySqlHelper.ExecuteTable(sql, pars);
                    for (int i = 0; i < da.Rows.Count; i++)
                    {
                        fundPureTips.Add(new FundPureTip()
                        {
                            FundNo = da.Rows[i]["FundNo"].ToString(),
                            FundName = da.Rows[i]["FundName"].ToString()
                        });
                    }
                    RedisConn writeConn = new RedisConn(false);
                    writeConn.SetRedisData("FundPureTips", fundPureTips);
                    writeConn.Close();
                }

                //if (MyUtils.HasChinese(fundstr))
                //{
                //     res = myContext.FundInfos.Where(x => x.FundName.Contains(fundstr)).ToList();
                //}
                //else
                //{
                //     res = myContext.FundInfos.Where(x => x.FundNo.Contains(fundstr)).ToList();
                //}
                readConn.Close();
                return fundPureTips;

            }
        }

        public List<FundPositionView> GetFundPositionViews(int userId)
        {
            using (MyContext myContext = new MyContext())
            {
                List<FundPositionView> fundPositionViews = new List<FundPositionView>();
                var masterPositions =  myContext.MasterPositions.ToList();
                var config = new MapperConfiguration(cfg => cfg.CreateMap<MasterPosition, FundPositionView>());
                var mapper = config.CreateMapper();
                foreach (var masterPosition in masterPositions)
                {
                    fundPositionViews.Add(mapper.Map<FundPositionView>(masterPosition));
                }
                return fundPositionViews;
            }
            
        }

        public int FundPositionUpdate(MasterPosition masterPosition)
        {
            using (MyContext myContext = new MyContext())
            {
                var position = myContext.MasterPositions.Where(x => x.FundNo == masterPosition.FundNo).SingleOrDefault();
                position.KJ_Position = masterPosition.KJ_Position;
                position.XYG_Position = masterPosition.XYG_Position;
                position.ALEX_Position = masterPosition.ALEX_Position;
                return myContext.SaveChanges();
            }
        }

        public List<DailyRateView> GetDailyRate()
        {
            using (MyContext myContext = new MyContext())
            {
                var daliyRates = new List<DailyRateView>();
                DateTime nowdate = DateHelper.getFormatDateTime(DateTime.Now);
                var all = myContext.DailyRates.OrderBy(x=>x.DateTime).ToList();
                var config = new MapperConfiguration(cfg => cfg.CreateMap<DailyRate, DailyRateView>());
                var mapper = config.CreateMapper();
                if (nowdate.DayOfWeek!=DayOfWeek.Saturday && nowdate.DayOfWeek != DayOfWeek.Sunday )
                {
                    var view = mapper.Map<DailyRateView>(all.Where(x => x.DateTime == nowdate).FirstOrDefault());
                    view.Tag = nowdate.ToString("MM-dd");
                    daliyRates.Add(view);

                }

                DailyRateView dailyRate = new DailyRateView() { 
                    My = 1,XYG=1,KJ=1,Alex_My=1,Alex =1 ,HS300=1,CYB=1,Tag="近一周"
                };

                var funds = myContext.FundInfos.Where(x => x.BuyRate <= -2).ToList();
                for (int i = all.Count()-5; i < all.Count(); i++)
                {
                    dailyRate.My = dailyRate.My * (1 + (all[i].My / 100));
                }
                dailyRate.My = Math.Round((dailyRate.My - 1) * 100, 2);
                dailyRate.XYG = Math.Round((double)funds.Where(x => x.FundNo == "12582474").SingleOrDefault().R1week, 2);
                dailyRate.KJ = Math.Round((double)funds.Where(x => x.FundNo == "10917305").SingleOrDefault().R1week, 2);
                dailyRate.Alex = Math.Round((double)funds.Where(x => x.FundNo == "10017235").SingleOrDefault().R1week, 2);
                dailyRate.HS300 = Math.Round((double)funds.Where(x => x.FundNo == "333333").SingleOrDefault().R1week, 2);
                dailyRate.CYB = Math.Round((double)funds.Where(x => x.FundNo == "555555").SingleOrDefault().R1week, 2);
                daliyRates.Add(dailyRate);

                dailyRate = new DailyRateView()
                {
                    My = 1,
                    XYG = 1,
                    KJ = 1,
                    Alex_My = 1,
                    Alex = 1,
                    HS300 = 1,
                    CYB = 1,
                    Tag = "近一月"
                };
                for (int i = all.Count() - 22; i < all.Count(); i++)
                {
                    dailyRate.My = dailyRate.My * (1 + (all[i].My / 100));
                }
                dailyRate.My = Math.Round((dailyRate.My - 1) * 100, 2);
                dailyRate.XYG = Math.Round((double)funds.Where(x => x.FundNo == "12582474").SingleOrDefault().R1month,2);
                dailyRate.KJ = Math.Round((double)funds.Where(x => x.FundNo == "10917305").SingleOrDefault().R1month,2);
                dailyRate.Alex = Math.Round((double)funds.Where(x => x.FundNo == "10017235").SingleOrDefault().R1month,2);
                dailyRate.HS300 = Math.Round((double)funds.Where(x => x.FundNo == "333333").SingleOrDefault().R1month,2);
                dailyRate.CYB = Math.Round((double)funds.Where(x => x.FundNo == "555555").SingleOrDefault().R1month,2);
                daliyRates.Add(dailyRate);

                dailyRate = new DailyRateView()
                {
                    My = 1,
                    XYG = 1,
                    KJ = 1,
                    Alex_My = 1,
                    Alex = 1,
                    HS300 = 1,
                    CYB = 1,
                    Tag = "近三月"
                };
                for (int i = all.Count() - 63; i < all.Count(); i++)
                {
                    dailyRate.My = dailyRate.My * (1 + (all[i].My / 100));
                }
                dailyRate.My = Math.Round((dailyRate.My - 1) * 100, 2);
                dailyRate.XYG = Math.Round((double)funds.Where(x => x.FundNo == "12582474").SingleOrDefault().R3month, 2);
                dailyRate.KJ = Math.Round((double)funds.Where(x => x.FundNo == "10917305").SingleOrDefault().R3month, 2);
                dailyRate.Alex = Math.Round((double)funds.Where(x => x.FundNo == "10017235").SingleOrDefault().R3month, 2);
                dailyRate.HS300 = Math.Round((double)funds.Where(x => x.FundNo == "333333").SingleOrDefault().R3month, 2);
                dailyRate.CYB = Math.Round((double)funds.Where(x => x.FundNo == "555555").SingleOrDefault().R3month, 2);
                daliyRates.Add(dailyRate);

                dailyRate = new DailyRateView()
                {
                    My = 1,
                    XYG = 1,
                    KJ = 1,
                    Alex_My = 1,
                    Alex = 1,
                    HS300 = 1,
                    CYB = 1,
                    Tag = "近六月"
                };
                for (int i = all.Count() - 122; i < all.Count(); i++)
                {
                    dailyRate.My = dailyRate.My * (1 + (all[i].My / 100));
                }
                dailyRate.My = Math.Round((dailyRate.My - 1) * 100, 2);
                dailyRate.XYG = Math.Round((double)funds.Where(x => x.FundNo == "12582474").SingleOrDefault().R6month, 2);
                dailyRate.KJ = Math.Round((double)funds.Where(x => x.FundNo == "10917305").SingleOrDefault().R6month, 2);
                dailyRate.Alex = Math.Round((double)funds.Where(x => x.FundNo == "10017235").SingleOrDefault().R6month, 2);
                dailyRate.HS300 = Math.Round((double)funds.Where(x => x.FundNo == "333333").SingleOrDefault().R6month, 2);
                dailyRate.CYB = Math.Round((double)funds.Where(x => x.FundNo == "555555").SingleOrDefault().R6month, 2);
                daliyRates.Add(dailyRate);

                var all_2022 = all.Where(x => x.DateTime >= new DateTime(2022, 1, 1, 0, 0, 0)).ToList();
                dailyRate = new DailyRateView()
                {
                    My = 1,
                    XYG = 1,
                    KJ = 1,
                    Alex_My = 1,
                    Alex = 1,
                    HS300 = 1,
                    CYB = 1,
                    Tag = "今年以来"
                };
                for (int i = all.Count() - all_2022.Count(); i < all.Count(); i++)
                {
                    dailyRate.My = dailyRate.My * (1 + (all[i].My / 100));
                }
                dailyRate.My = Math.Round((dailyRate.My - 1) * 100, 2);
                dailyRate.XYG = Math.Round((double)funds.Where(x => x.FundNo == "12582474").SingleOrDefault().A2022, 2);
                dailyRate.KJ = Math.Round((double)funds.Where(x => x.FundNo == "10917305").SingleOrDefault().A2022, 2);
                dailyRate.Alex = Math.Round((double)funds.Where(x => x.FundNo == "10017235").SingleOrDefault().A2022, 2);
                dailyRate.HS300 = Math.Round((double)funds.Where(x => x.FundNo == "333333").SingleOrDefault().A2022, 2);
                dailyRate.CYB = Math.Round((double)funds.Where(x => x.FundNo == "555555").SingleOrDefault().A2022, 2);
                daliyRates.Add(dailyRate);

                //for (int i = all.Count()-5; i < all.Count(); i++)
                //{
                //    dailyRate.My = dailyRate.My * (1 + (all[i].My / 100));
                //    dailyRate.XYG = dailyRate.XYG * (1 + (all[i].XYG / 100));
                //    dailyRate.KJ = dailyRate.KJ * (1 + (all[i].KJ / 100));
                //    dailyRate.Alex_My = dailyRate.Alex_My * (1 + (all[i].Alex_My / 100));
                //    dailyRate.Alex = dailyRate.Alex * (1 + (all[i].Alex / 100));
                //    dailyRate.HS300 = dailyRate.HS300 * (1 + (all[i].HS300 / 100));
                //    dailyRate.CYB = dailyRate.CYB * (1 + (all[i].CYB / 100));
                //}
                //dailyRate.My = Math.Round((dailyRate.My - 1) * 100, 2);
                //dailyRate.XYG = Math.Round((dailyRate.XYG - 1) * 100, 2);
                //dailyRate.KJ = Math.Round((dailyRate.KJ - 1) * 100, 2);
                //dailyRate.Alex_My = Math.Round((dailyRate.Alex_My - 1) * 100, 2);
                //dailyRate.Alex = Math.Round((dailyRate.Alex - 1) * 100, 2);
                //dailyRate.HS300 = Math.Round((dailyRate.HS300 - 1) * 100, 2);
                //dailyRate.CYB = Math.Round((dailyRate.CYB - 1) * 100, 2);

                //daliyRates.Add(dailyRate);


                //dailyRate = new DailyRateView() { 
                //    My = 1,XYG=1,KJ=1,Alex_My=1,Alex =1 ,HS300=1,CYB=1,Tag="近一月"
                //};

                //for (int i = all.Count() - 22; i < all.Count(); i++)
                //{
                //    dailyRate.My = dailyRate.My * (1 + (all[i].My / 100));
                //    dailyRate.XYG = dailyRate.XYG * (1 + (all[i].XYG / 100));
                //    dailyRate.KJ = dailyRate.KJ * (1 + (all[i].KJ / 100));
                //    dailyRate.Alex_My = dailyRate.Alex_My * (1 + (all[i].Alex_My / 100));
                //    dailyRate.Alex = dailyRate.Alex * (1 + (all[i].Alex / 100));
                //    dailyRate.HS300 = dailyRate.HS300 * (1 + (all[i].HS300 / 100));
                //    dailyRate.CYB = dailyRate.CYB * (1 + (all[i].CYB / 100));
                //}              

                //dailyRate.My = Math.Round((dailyRate.My-1)*100, 2);
                //dailyRate.XYG = Math.Round((dailyRate.XYG - 1) * 100, 2);
                //dailyRate.KJ = Math.Round((dailyRate.KJ - 1) * 100, 2);
                //dailyRate.Alex_My = Math.Round((dailyRate.Alex_My - 1) * 100, 2);
                //dailyRate.Alex = Math.Round((dailyRate.Alex - 1) * 100, 2);
                //dailyRate.HS300 = Math.Round((dailyRate.HS300 - 1) * 100, 2);
                //dailyRate.CYB = Math.Round((dailyRate.CYB - 1) * 100, 2);

                //daliyRates.Add(dailyRate);
                return daliyRates;
            }
        }

        public double GetIndustryPosition(string ipstr, string industryName)
        {
            if (!string.IsNullOrEmpty(ipstr))
            {
                var dict = JsonConvert.DeserializeObject<Dictionary<string, double>>(ipstr);
                if (dict.ContainsKey(industryName))
                {
                    return dict[industryName];
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
            
        }

        public string GetBelong(StockInfo stock)
        {
            if (stock.SZ50 > 0)
            {
                return "上证50";
            }
            else if (stock.HS300>0)
            {
                return "沪深300";
            }
            else if (stock.ZZ500 > 0)
            {
                return "中证500";
            }
            else if (stock.ZZ1000 > 0)
            {
                return "中证1000";
            }
            else if (stock.StockNo.Split('.')[0] == "116")
            {
                return "港股";
            }
            else
            {
                return "小盘";
            }
        }
    }
}
