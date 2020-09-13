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

namespace Solo.BLL
{
    public class FundInfoMySqlService : IFundInfoService
    {

        public List<FundView> GetFundInfosByQuery(FundQuery fundQuery)
        {
            List<FundView> list = new List<FundView>();
            string sql = "select *,(IFNULL(BuyRate, 0)+IFNULL(ManagerFee, 0)+IFNULL(CustodyFee, 0)+IFNULL(SaleFee, 0)) as TotalFee,mf.Status,mf.Investment from FundInfos fi left join MyFunds mf on fi.FundNo = mf.FundNo left join MyScore ms on fi.FundNo=ms.FundNo ";
            sql = sql + SetFundString(fundQuery)+" order by R1day desc";
            float outSy = 0;
            int outMf = 0;
            MySqlParameter[] pars = null;
            try
            {
                DataTable da = MySqlHelper.ExecuteTable(sql, pars);
                if (da.Rows.Count > 0)
                {
                    for (int i = 0; i < da.Rows.Count; i++)
                    {
                        FundView fv = new FundView();
                        fv.FundNo = da.Rows[i]["FundNo"].ToString();
                        fv.FundName = da.Rows[i]["FundName"].ToString();
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
                            fv.R2year = (float)da.Rows[i]["R2year"];
                        }
                        if (float.TryParse(da.Rows[i]["R3year"].ToString(), out outSy))
                        {
                            fv.R3year = (float)da.Rows[i]["R3year"];
                        }
                        if (float.TryParse(da.Rows[i]["R5year"].ToString(), out outSy))
                        {
                            fv.R5year = (float)da.Rows[i]["R5year"];
                        }
                        if (float.TryParse(da.Rows[i]["Over7dFee"].ToString(), out outSy))
                        {
                            fv.Over7dFee = (float)da.Rows[i]["Over7dFee"];
                        }
                        if (float.TryParse(da.Rows[i]["Over30dFee"].ToString(), out outSy))
                        {
                            fv.Over30dFee = (float)da.Rows[i]["Over30dFee"];
                        }
                        if (float.TryParse(da.Rows[i]["Over1yFee"].ToString(), out outSy))
                        {
                            fv.Over1yFee = (float)da.Rows[i]["Over1yFee"];
                        }
                        if (float.TryParse(da.Rows[i]["Over2yFee"].ToString(), out outSy))
                        {
                            fv.Over2yFee = (float)da.Rows[i]["Over2yFee"];
                        }
                        if (int.TryParse(da.Rows[i]["status"].ToString(), out outMf))
                        {
                            fv.status = (int)da.Rows[i]["status"];
                        }
                        if (int.TryParse(da.Rows[i]["Investment"].ToString(), out outMf))
                        {
                            fv.Investment = (int)da.Rows[i]["Investment"];
                        }
                        if (int.TryParse(da.Rows[i]["Rank_Id"].ToString(), out outMf))
                        {
                            fv.Rank_Id = (int)da.Rows[i]["Rank_Id"];
                        }
                        if (float.TryParse(da.Rows[i]["StockRate"].ToString(), out outSy))
                        {
                            fv.StockRate = (float)da.Rows[i]["StockRate"];
                        }
                        if (float.TryParse(da.Rows[i]["FundScore"].ToString(), out outSy))
                        {
                            fv.FundScore = (float)da.Rows[i]["FundScore"];
                        }
                        if (float.TryParse(da.Rows[i]["ManagerScore"].ToString(), out outSy))
                        {
                            fv.ManagerScore = (float)da.Rows[i]["ManagerScore"];
                        }
                        if (float.TryParse(da.Rows[i]["ReturnRate"].ToString(), out outSy))
                        {
                            double viewrr = Math.Round(Convert.ToDouble(da.Rows[i]["ReturnRate"]) * 100, 2);
                            fv.ReturnRate = viewrr;
                        }
                        fv.DutyDate = Convert.ToDateTime(da.Rows[i]["DutyDate"]);
                        fv.FundUpdateTime = Convert.ToDateTime(da.Rows[i]["FundUpdateTime"]);

                        list.Add(fv);
                    }

                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public string SetFundString(FundQuery fundQuery)
        {
            string queryStr = fundQuery== null?"":"where ";
            MyFundService myFundService = new MyFundService();
            switch (fundQuery.own)
            {
                case OWN.Empty:
                    queryStr += SetFundMainQueryStr(fundQuery);
                    break;
                case OWN.OnlyHolds:                   
                    queryStr += SetSelsStr(myFundService.GetMyFundNos(1),true);
                    break;
                case OWN.OnlyWaits:
                    queryStr += SetSelsStr(myFundService.GetMyFundNos(2),true);
                    break;
                case OWN.OnlyHoldWaits:
                    queryStr += SetSelsStr(myFundService.GetMyFundNos(3),true);
                    break;
                case OWN.Holds:
                    queryStr += SetFundMainQueryStr(fundQuery);
                    queryStr += SetSelsStr(myFundService.GetMyFundNos(1),false);
                    break;
                case OWN.Waits:
                    queryStr += SetFundMainQueryStr(fundQuery);
                    queryStr += SetSelsStr(myFundService.GetMyFundNos(2), false);
                    break;
                case OWN.HoldWaits:
                    queryStr += SetFundMainQueryStr(fundQuery);
                    queryStr += SetSelsStr(myFundService.GetMyFundNos(3), false);
                    break;
            }
            queryStr += " 1=1 ";
            return queryStr;

        }
        public string SetSelsStr(string sels,bool isOnly)
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

        public string SetFundMainQueryStr(FundQuery fundQuery)
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
            if (fundQuery.bondstock == 1)
            {
                queryStr += "StockRate<50 and FundName not like '%ETF%' and FundName not like '%指数%' and ";
            }
            else if (fundQuery.bondstock == 2)
            {
                queryStr += "(StockRate>=50 or (FundName like '%ETF%' or FundName like '%指数%')) and ";
            }
            #endregion
            return queryStr;
        }

        public string SetAnalysisMainQueryStr(AnalysisQuery analysisQuery)
        {
            string queryStr = "";
            if (!string.IsNullOrEmpty(analysisQuery.FundNo))
            {
                queryStr += "fi.FundNo like '%" + analysisQuery.FundNo + "%' and ";
            }
            if (!string.IsNullOrEmpty(analysisQuery.FundName))
            {
                queryStr += "FundName like '%" + analysisQuery.FundName + "%' and ";
            }
            if (analysisQuery.MaxStockRate != null)
            {
                queryStr += $"StockRate<={analysisQuery.MaxStockRate} and ";
            }
            if (analysisQuery.MinStockRate != null)
            {
                queryStr += $"StockRate>={analysisQuery.MinStockRate} and ";
            }
            if (analysisQuery.MaxTotalFee != null)
            {
                queryStr += "(IFNULL(BuyRate, 0)+IFNULL(ManagerFee, 0)+IFNULL(CustodyFee, 0)+IFNULL(SaleFee, 0))<=" + analysisQuery.MaxTotalFee + " and ";
            }
            if (analysisQuery.MinTotalFee != null)
            {
                queryStr += "(IFNULL(BuyRate, 0)+IFNULL(ManagerFee, 0)+IFNULL(CustodyFee, 0)+IFNULL(SaleFee, 0))>=" + analysisQuery.MinTotalFee + " and ";
            }
            if (analysisQuery.MaxR1week != null)
            {
                queryStr += $"R1week<={analysisQuery.MaxR1week} and ";
            }
            if (analysisQuery.MinR1week != null)
            {
                queryStr += $"R1week>={analysisQuery.MinR1week} and ";
            }
            if (analysisQuery.MaxR1m_1w != null)
            {
                queryStr += $"(R1month-R1week)<={analysisQuery.MaxR1m_1w} and ";
            }
            if (analysisQuery.MinR1m_1w != null)
            {
                queryStr += $"(R1month-R1week)>={analysisQuery.MinR1m_1w} and ";
            }
            if (analysisQuery.MaxR3_1m != null)
            {
                queryStr += $"(R3month-R1month)<={analysisQuery.MaxR3_1m} and ";
            }
            if (analysisQuery.MinR3_1m != null)
            {
                queryStr += $"(R3month-R1month)>={analysisQuery.MinR3_1m} and ";
            }
            if (analysisQuery.MaxR6_3m != null)
            {
                queryStr += $"(R6month-R3month)<={analysisQuery.MaxR6_3m} and ";
            }
            if (analysisQuery.MinR6_3m != null)
            {
                queryStr += $"(R6month-R3month)>={analysisQuery.MinR6_3m} and ";
            }
            if (analysisQuery.MaxR12_6m != null)
            {
                queryStr += $"(R1year-R6month)<={analysisQuery.MaxR12_6m} and ";
            }
            if (analysisQuery.MinR12_6m != null)
            {
                queryStr += $"(R1year-R6month)>={analysisQuery.MinR12_6m} and ";
            }
            if (analysisQuery.MaxR1year != null)
            {
                queryStr += $"R1year<={analysisQuery.MaxR1year} and ";
            }
            if (analysisQuery.MinR1year != null)
            {
                queryStr += $"R1year>={analysisQuery.MinR1year} and ";
            }
            if (analysisQuery.MaxR2_1y != null)
            {
                queryStr += $"(R2year-R1year)<={analysisQuery.MaxR2_1y} and ";
            }
            if (analysisQuery.MinR2_1y != null)
            {
                queryStr += $"(R2year-R1year)>={analysisQuery.MinR2_1y} and ";
            }
            if (analysisQuery.MaxR3_2y != null)
            {
                queryStr += $"(R3year-R2year)<={analysisQuery.MaxR3_2y} and ";
            }
            if (analysisQuery.MinR3_2y != null)
            {
                queryStr += $"(R3year-R2year)>={analysisQuery.MinR3_2y} and ";
            }
            if (analysisQuery.MaxR5_3y != null)
            {
                queryStr += $"(R5year-R3year)<={analysisQuery.MaxR5_3y} and ";
            }
            if (analysisQuery.MinR5_3y != null)
            {
                queryStr += $"(R5year-R3year)>={analysisQuery.MinR5_3y} and ";
            }
            if (analysisQuery.MaxRank_Id != null)
            {
                queryStr += $"Rank_Id<={analysisQuery.MaxRank_Id} and ";
            }
            if (analysisQuery.MinRank_Id != null)
            {
                queryStr += $"Rank_Id>={analysisQuery.MinRank_Id} and ";
            }
            if (analysisQuery.isnew)
            {
                string formatDate = DateTime.Now.ToString("yyyy-MM-dd");
                queryStr += $"FundUpdateTime>='{formatDate}' and ";
            }
            if (analysisQuery.bondstock == 1)
            {
                queryStr += "StockRate<50 and FundName not like '%ETF%' and FundName not like '%指数%' and ";
            }
            else if (analysisQuery.bondstock == 2)
            {
                queryStr += "(StockRate>=50 or (FundName like '%ETF%' or FundName like '%指数%')) and ";
            }
            return queryStr;
        }

        public string SetAnalysisStr(AnalysisQuery analysisQuery)
        {
            string queryStr = analysisQuery == null ? "" : "where ";
            MyFundService myFundService = new MyFundService();
            switch (analysisQuery.own)
            {
                case OWN.Empty:
                    queryStr += SetAnalysisMainQueryStr(analysisQuery);
                    break;
                case OWN.OnlyHolds:
                    queryStr += SetSelsStr(myFundService.GetMyFundNos(1), true);
                    break;
                case OWN.OnlyWaits:
                    queryStr += SetSelsStr(myFundService.GetMyFundNos(2), true);
                    break;
                case OWN.OnlyHoldWaits:
                    queryStr += SetSelsStr(myFundService.GetMyFundNos(3), true);
                    break;
                case OWN.Holds:
                    queryStr += SetAnalysisMainQueryStr(analysisQuery);
                    queryStr += SetSelsStr(myFundService.GetMyFundNos(1), false);
                    break;
                case OWN.Waits:
                    queryStr += SetAnalysisMainQueryStr(analysisQuery);
                    queryStr += SetSelsStr(myFundService.GetMyFundNos(2), false);
                    break;
                case OWN.HoldWaits:
                    queryStr += SetAnalysisMainQueryStr(analysisQuery);
                    queryStr += SetSelsStr(myFundService.GetMyFundNos(3), false);
                    break;
            }
            queryStr += " BuyRate>-1 ";

            #region QueryCode

            //if (fundQuery.MaxR1month != null)
            //{
            //    queryStr += $"R1month<={fundQuery.MaxR1month} and ";
            //}
            //if (fundQuery.MinR1month != null)
            //{
            //    queryStr += $"R1month>={fundQuery.MinR1month} and ";
            //}

            #endregion
            return queryStr;

        }

        public List<AnalysisView> GetAnalysisByQuery(AnalysisQuery analysisQuery)
        {
            List<AnalysisView> avList = new List<AnalysisView>();
            RedisHelper redisHelper = new RedisHelper(AppConfigurtaionServices.Configuration.GetConnectionString("RedisConnection"));
            string sql = "select fi.FundUpdateTime,fi.ReturnRate,mf.Investment,mf.status,fi.FundNo,FundName,NetValue,StockRate,R1day,R1week,R1month,fi.FundScore,fi.ManagerScore,ms.Rank_Id,ms.Bear_Id,ms.BestReturn_Id,ms.FundScore_Id,ms.ManagerScore_Id from FundInfos fi left join MyFunds mf on fi.FundNo = mf.FundNo left join MyScore ms on fi.FundNo=ms.FundNo ";
            sql = sql + SetAnalysisStr(analysisQuery) + " order by R1week desc";
            float outSy = 0;
            int outMf = 0;
            MySqlParameter[] pars = null;
            DataTable da = MySqlHelper.ExecuteTable(sql, pars);
            try
            {
                if (da.Rows.Count > 0)
                {
                    for (int i = 0; i < da.Rows.Count; i++)
                    {
                        AnalysisView av = new AnalysisView();
                        av.FundNo = da.Rows[i]["FundNo"].ToString();
                        av.FundName = da.Rows[i]["FundName"].ToString();
                        if (float.TryParse(da.Rows[i]["StockRate"].ToString(), out outSy))
                        {
                            float temp = (float)da.Rows[i]["StockRate"];
                            av.StockRate = (float)Math.Round(temp, 2);
                        }
                        if (float.TryParse(da.Rows[i]["R1day"].ToString(), out outSy))
                        {
                            float temp = (float)da.Rows[i]["R1day"];
                            av.R1day = (float)Math.Round(temp, 2);
                        }
                        if (float.TryParse(da.Rows[i]["R1week"].ToString(), out outSy))
                        {
                            float temp = (float)da.Rows[i]["R1week"];
                            av.R1week = (float)Math.Round(temp, 2);
                        }
                        if (float.TryParse(da.Rows[i]["R1month"].ToString(), out outSy))
                        {
                            float temp = (float)da.Rows[i]["R1month"];
                            av.R1month = (float)Math.Round(temp, 2);
                        }
                        if (int.TryParse(da.Rows[i]["Bear_Id"].ToString(), out outMf))
                        {
                            av.Bear_Id = (int)da.Rows[i]["Bear_Id"];
                        }
                        if (int.TryParse(da.Rows[i]["Rank_Id"].ToString(), out outMf))
                        {
                            av.Rank_Id = (int)da.Rows[i]["Rank_Id"];
                        }
                        if (int.TryParse(da.Rows[i]["FundScore_Id"].ToString(), out outMf))
                        {
                            av.FundScore_Id = (int)da.Rows[i]["FundScore_Id"];
                        }
                        if (int.TryParse(da.Rows[i]["ManagerScore_Id"].ToString(), out outMf))
                        {
                            av.ManagerScore_Id = (int)da.Rows[i]["ManagerScore_Id"];
                        }
                        if (int.TryParse(da.Rows[i]["BestReturn_Id"].ToString(), out outMf))
                        {
                            av.BestReturn_Id = (int)da.Rows[i]["BestReturn_Id"];

                        }
                        //if (float.TryParse(da.Rows[i]["R1m_1w"].ToString(), out outSy))
                        //{
                        //    float temp = outSy;
                        //    av.R1m_1w = (float)Math.Round(temp, 2);
                        //}
                        //if (float.TryParse(da.Rows[i]["R3_1m"].ToString(), out outSy))
                        //{
                        //    float temp = outSy;
                        //    av.R3_1m = (float)Math.Round(temp, 2);
                        //}
                        //if (float.TryParse(da.Rows[i]["R6_3m"].ToString(), out outSy))
                        //{
                        //    float temp = outSy;
                        //    av.R6_3m = (float)Math.Round(temp, 2);
                        //}
                        //if (float.TryParse(da.Rows[i]["R12_6m"].ToString(), out outSy))
                        //{
                        //    float temp = outSy;
                        //    av.R12_6m = (float)Math.Round(temp, 2);
                        //}
                        //if (float.TryParse(da.Rows[i]["R1year"].ToString(), out outSy))
                        //{
                        //    float temp = outSy;
                        //    av.R1year = (float)Math.Round(temp, 2);
                        //}
                        //if (float.TryParse(da.Rows[i]["R2_1y"].ToString(), out outSy))
                        //{
                        //    double temp = outSy;
                        //    av.R2_1y = (float)Math.Round(temp, 2);
                        //}
                        //if (float.TryParse(da.Rows[i]["R3_2y"].ToString(), out outSy))
                        //{
                        //    double temp = outSy;
                        //    av.R3_2y = (float)Math.Round(temp, 2);
                        //}
                        //if (float.TryParse(da.Rows[i]["R5_3y"].ToString(), out outSy))
                        //{
                        //    double temp = outSy;
                        //    av.R5_3y = (float)Math.Round(temp, 2);
                        //}
                        if (int.TryParse(da.Rows[i]["status"].ToString(), out outMf))
                        {
                            av.status = (int)da.Rows[i]["status"];
                        }
                        if (int.TryParse(da.Rows[i]["Investment"].ToString(), out outMf))
                        {
                            av.Investment = (int)da.Rows[i]["Investment"];
                        }
                        if (float.TryParse(da.Rows[i]["ReturnRate"].ToString(), out outSy))
                        {
                            double viewrr = Math.Round(Convert.ToDouble(da.Rows[i]["ReturnRate"]) * 100, 2);
                            av.ReturnRate = viewrr;
                        }
                        if (float.TryParse(da.Rows[i]["FundScore"].ToString(), out outSy))
                        {
                            av.FundScore = (float)da.Rows[i]["FundScore"];
                        }
                        if (float.TryParse(da.Rows[i]["ManagerScore"].ToString(), out outSy))
                        {
                            av.ManagerScore = (float)da.Rows[i]["ManagerScore"];
                        }

                        double netValue = 0;
                        //double inNetValue = 0;
                        var netValueStr = da.Rows[i]["NetValue"].ToString();
                        if (double.TryParse(netValueStr, out netValue))
                        {
                            //if (double.TryParse(da.Rows[i]["InNetValue"].ToString(), out inNetValue))
                            //{
                            //    if (netValue!=0 && inNetValue !=0)
                            //    {
                            //        av.HoldIncrease = Math.Round((netValue - inNetValue)/ netValue, 5);
                            //    }
                            //    else
                            //    {
                            //        av.HoldIncrease = null;
                            //    }
                            //}
                            //else
                            //{
                            //    av.HoldIncrease = null;
                            //}
                        }



                        DateTime updateTime;

                        if (DateTime.TryParse(da.Rows[i]["FundUpdateTime"].ToString(), out updateTime))
                        {

                        }
                        else
                        {

                        }
                        
                        //Stopwatch watch = new Stopwatch();
                        //watch.Start();
                        var result = redisHelper.GetRedisData<LineReturn>(av.FundNo);
                        if (result == null || updateTime>result.updatetime)
                        {
                            result = LinearRegression.GetTrendEquation(av.FundNo);
                            if (result!=null)
                            {
                                result.updatetime = updateTime;
                                redisHelper.SetRedisData(av.FundNo, result);
                            }                          
                        }
                                                                       
                        //double time = watch.Elapsed.TotalSeconds;
                        //Console.WriteLine(time);
                        //watch.Stop();

                        if (result != null)
                        {
                            av.D_Top = Math.Round(100*(result.topValue - netValue)/result.topValue,2);
                            if (av.D_Top < 0)
                            {
                                av.D_Top = 0;
                            }
                            //av.D_Exp1 =Math.Round((netValue-(result.begins[3]) * result.points[0].X - result.points[0].Y) * 10000, 2);
                            av.D_Exp1 = Math.Round(100*(netValue - (result.begins[4]-1) * result.points[0].X - result.points[0].Y)/netValue, 2);
                            av.D_Exp2 = Math.Round(100*(netValue-(result.begins[4]-1) * result.points[1].X - result.points[1].Y)/netValue, 2);
                            av.D_Exp3 = Math.Round(100*(netValue-(result.begins[4]-1) * result.points[2].X - result.points[2].Y)/netValue, 2);
                            av.D_ExpM2 = Math.Round(100 * (netValue - (result.begins[4]-1) * result.points[4].X - result.points[4].Y) / netValue, 2);
                            av.D_ExpM1 = Math.Round(100 * (netValue - (result.begins[4]-1) * result.points[5].X - result.points[5].Y) / netValue, 2);
                            av.D_ExpD10 = Math.Round(100 * (netValue - (result.begins[4] - 1) * result.points[6].X - result.points[6].Y) / netValue, 2);
                            av.D_ExpD5 = Math.Round(100 * (netValue - (result.begins[4] - 1) * result.points[7].X - result.points[7].Y) / netValue, 2);

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

                            if (result.dataSet.Rows.Count - 245 >=0)
                            {
                                av.Linear1 = Math.Round(result.points[0].X * 10000 / normalized, 2);
                            }
                            if (result.dataSet.Rows.Count - 490 >= 0)
                            {
                                av.Linear2 = Math.Round(result.points[1].X * 10000 / normalized, 2);
                            }
                            if (result.dataSet.Rows.Count - 735 >= 0)
                            {
                                av.Linear3 = Math.Round(result.points[2].X * 10000 / normalized, 2);
                            }
                            if (result.dataSet.Rows.Count - 1225 >= 0)
                            {
                                av.Linear5 = Math.Round(result.points[3].X * 10000 / normalized, 2);
                            }
                            
                        }
                        else
                        {
                            av.D_Top = 0;
                            av.D_Exp1 =0;
                            av.D_Exp2 =0;
                            av.D_Exp3 =0;
                        }
                        avList.Add(av);
                    }
                }
                return avList;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(analysisQuery.FundNo);
                //return null;
                throw ex;
            }
            
        }

        public List<StrategyView> GetStrategyByQuery(StrategyQuery strategyQuery)
        {
            List<StrategyView> stList = new List<StrategyView>();
            RedisHelper redisHelper = new RedisHelper(AppConfigurtaionServices.Configuration.GetConnectionString("RedisConnection"));
            string sql = "select fi.FundNo,FundName,Strategy,Investment,ReturnRate,R1day,R1week,R1month,fi.FundScore,fi.ManagerScore,DutyDate,NetValue,TotalScale," +
                         "mf.Estimate,vt.ValuationScore,FundUpdateTime,Rank_Id from FundInfos fi right join MyFunds mf on fi.FundNo = mf.FundNo left join MyScore ms on " +
                         "fi.FundNo=ms.FundNo left join Valuations vt on mf.ValuationId=vt.Id";
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
                        if (float.TryParse(da.Rows[i]["ReturnRate"].ToString(), out outSy))
                        {
                            double viewrr = Math.Round(Convert.ToDouble(da.Rows[i]["ReturnRate"]) * 100, 2);
                            sv.ReturnRate = viewrr;
                        }
                        if (float.TryParse(da.Rows[i]["FundScore"].ToString(), out outSy))
                        {
                            sv.FundScore = (float)da.Rows[i]["FundScore"];
                        }
                        if (float.TryParse(da.Rows[i]["ManagerScore"].ToString(), out outSy))
                        {
                            sv.ManagerScore = (float)da.Rows[i]["ManagerScore"];
                        }
                        if (float.TryParse(da.Rows[i]["Estimate"].ToString(), out outSy))
                        {
                            sv.Estimate = (double)da.Rows[i]["Estimate"];
                        }
                        if (float.TryParse(da.Rows[i]["ValuationScore"].ToString(), out outSy))
                        {
                            sv.ValuationScore = (double)da.Rows[i]["ValuationScore"];
                        }

                        try
                        {
                            sv.DutyDate = Convert.ToDateTime(da.Rows[i]["DutyDate"]);
                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine(ex);
                        }
                        

                        double netValue = 0;
                        var netValueStr = da.Rows[i]["NetValue"].ToString();
                        if (!double.TryParse(netValueStr, out netValue))
                        { 
                        
                        }



                        DateTime updateTime;

                        if (!DateTime.TryParse(da.Rows[i]["FundUpdateTime"].ToString(), out updateTime))
                        {

                        }



                        var result = redisHelper.GetRedisData<LineReturn>(sv.FundNo);
                        if (result == null || updateTime > result.updatetime)
                        {
                            result = LinearRegression.GetTrendEquation(sv.FundNo);
                            if (result != null)
                            {
                                result.updatetime = updateTime;
                                redisHelper.SetRedisData(sv.FundNo, result);
                            }
                        }


                        if (result != null)
                        {
                            sv.D_Top = Math.Round(100 * (result.topValue - netValue) / result.topValue, 2);
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
                            //sv.Linear1 = Math.Round(result.points[0].X * 10000 * 250 / 365, 2);
                            //sv.Linear2 = Math.Round(result.points[1].X * 10000 * 250 / 365, 2);
                            //sv.Linear3 = Math.Round(result.points[2].X * 10000 * 250 / 365, 2);
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
                return stList;
            }
            catch (Exception ex)
            {

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
                queryStr += "StockRate<50 and FundName not like '%ETF%' and FundName not like '%指数%' and ";
            }
            else if (strategyQuery.bondstock == 2)
            {
                queryStr += "(StockRate>=50 or (FundName like '%ETF%' or FundName like '%指数%')) and ";
            }
            if (strategyQuery.status<=2)
            {
                queryStr += "status=" + strategyQuery.status.ToString();
            }
            else
            {
                queryStr += "1=1";
            }           
            return queryStr;
        }

        public StrategyTitle GetStrategyTitle()
        {
            StrategyTitle strategyTitle = new StrategyTitle();
            RedisHelper redisHelper = new RedisHelper(AppConfigurtaionServices.Configuration.GetConnectionString("RedisConnection"));
            string timestr = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            string todaystr = DateTime.Now.ToString("yyyy-MM-dd");
            string sql = "";
            MySqlParameter[] pars = null;
            DataTable da;

            var result = redisHelper.GetRedisData<StrategyTitle>(todaystr+"-title");

            if (result != null && result.AR1day!=0 && result.AR7day!= 0 && result.AR1month!=0)
            {
                strategyTitle.AR1day = result.AR1day;
                strategyTitle.AR7day = result.AR7day;
                strategyTitle.AR1month = result.AR1month;
            }
            else
            {
                sql = "select * from FundInfos where FundNo='444444' and FundUpdateTime='" + todaystr + "'";               
                da = MySqlHelper.ExecuteTable(sql, pars);
                if (da.Rows.Count > 0)
                {
                    strategyTitle.AR1day = Math.Round(Convert.ToDouble(da.Rows[0]["R1day"]), 2);
                    strategyTitle.AR7day = Math.Round(Convert.ToDouble(da.Rows[0]["R1week"]), 2);
                    strategyTitle.AR1month = Math.Round(Convert.ToDouble(da.Rows[0]["R1month"]), 2);                    
                }
                else
                {
                    sql = "select NetValue,DataTime from FundDailyIDetails where FundNo='444444' and DataTime>='" + timestr + "' order by DataTime desc";
                    da = MySqlHelper.ExecuteTable(sql, pars);
                    if (da.Rows.Count > 0)
                    {
                        double recentNetvalue = Convert.ToDouble(da.Rows[0]["NetValue"]);
                        double r1dNetValue = Convert.ToDouble(da.Rows[1]["NetValue"]);
                        double r7dNetValue = Convert.ToDouble(da.Rows[5]["NetValue"]);
                        double r1mNetValue = Convert.ToDouble(da.Rows[da.Rows.Count - 1]["NetValue"]);
                        

                        strategyTitle.AR1day = Math.Round((recentNetvalue - r1dNetValue) * 100 / r1dNetValue, 2);
                        strategyTitle.AR7day = Math.Round((recentNetvalue - r7dNetValue) * 100 / r7dNetValue, 2);
                        strategyTitle.AR1month = Math.Round((recentNetvalue - r1mNetValue) * 100 / r1mNetValue, 2);

                        string updatestr = $"update FundInfos set R1day=@R1day,R1week=@R1week,R1month=@R1month where FundNo='444444'";
                        MySqlParameter[] pars1 = {
                                   new MySqlParameter("@R1day",MySqlDbType.Double),
                                   new MySqlParameter("@R1week", MySqlDbType.Double),
                                   new MySqlParameter("@R1month", MySqlDbType.Double)
                                 };
                        pars1[0].Value = strategyTitle.AR1day;
                        pars1[1].Value = strategyTitle.AR7day;
                        pars1[2].Value = strategyTitle.AR1month;
                        if (MySqlHelper.ExecuteNonQuery(updatestr, pars1) != 1)
                        {
                            throw new Exception();
                        }
                    }
                }

            }

            if (result != null && result.CR1day!=0)
            {
                strategyTitle.CR1day = result.CR1day;
            }
            else
            {
                sql = "select * from FundInfos where FundNo='555555' and FundUpdateTime='" + todaystr + "'";
                da = MySqlHelper.ExecuteTable(sql, pars);
                if (da.Rows.Count > 0)
                {
                    strategyTitle.CR1day = Math.Round(Convert.ToDouble(da.Rows[0]["R1day"]), 2);
                }
                else
                {
                    sql = "select NetValue,DataTime from FundDailyIDetails where FundNo='555555' and DataTime>='" + timestr + "' order by DataTime desc";
                    da = MySqlHelper.ExecuteTable(sql, pars);
                    if (da.Rows.Count > 0)
                    {
                        double recentNetvalue = Convert.ToDouble(da.Rows[0]["NetValue"]);
                        double r1dNetValue = Convert.ToDouble(da.Rows[1]["NetValue"]);

                        strategyTitle.CR1day = Math.Round((recentNetvalue - r1dNetValue) * 100 / r1dNetValue, 2);

                        string updatestr = $"update FundInfos set R1day=@R1day where FundNo='555555'";

                        MySqlParameter[] pars1 = {
                                   new MySqlParameter("@R1day",MySqlDbType.Double),
                                 };
                        pars1[0].Value = strategyTitle.CR1day;
                        if (MySqlHelper.ExecuteNonQuery(updatestr, pars1) != 1)
                        {
                            throw new Exception();
                        }
                    }
                }
            }

            if (result != null && result.SR1day != 0)
            {
                strategyTitle.SR1day = result.SR1day;
            }
            else
            {
                sql = "select * from FundInfos where FundNo='666666' and FundUpdateTime='" + todaystr + "'";
                da = MySqlHelper.ExecuteTable(sql, pars);
                if (da.Rows.Count > 0)
                {
                    strategyTitle.SR1day = Math.Round(Convert.ToDouble(da.Rows[0]["R1day"]), 2);
                }
                else
                {
                    sql = "select NetValue,DataTime from FundDailyIDetails where FundNo='666666' and DataTime>='" + timestr + "' order by DataTime desc";
                    da = MySqlHelper.ExecuteTable(sql, pars);
                    if (da.Rows.Count > 0)
                    {
                        double recentNetvalue = Convert.ToDouble(da.Rows[0]["NetValue"]);
                        double r1dNetValue = Convert.ToDouble(da.Rows[1]["NetValue"]);

                        strategyTitle.SR1day = Math.Round((recentNetvalue - r1dNetValue) * 100 / r1dNetValue, 2);

                        string updatestr = $"update FundInfos set R1day=@R1day where FundNo='666666'";

                        MySqlParameter[] pars1 = {
                                   new MySqlParameter("@R1day",MySqlDbType.Double),
                                 };
                        pars1[0].Value = strategyTitle.SR1day;
                        if (MySqlHelper.ExecuteNonQuery(updatestr, pars1) != 1)
                        {
                            throw new Exception();
                        }
                    }
                }
            }

            if (result != null && result.XYGRate !=0 && result.KJRate!=0 && result.MyStockRate!=0 && result.XYGReturn!=0 && result.RankReturn!=0 && result.KJReturn!=0 && result.MyReturn!=0 && result.SumStock!=0)
            {
                strategyTitle.XYGRate = result.XYGRate;
                strategyTitle.KJRate = result.KJRate;
                strategyTitle.MyStockRate = result.MyStockRate;
                strategyTitle.XYGReturn = result.XYGReturn;
                strategyTitle.KJReturn = result.KJReturn;
                strategyTitle.MyStockReturn = result.MyStockReturn;
                strategyTitle.MyBondReturn = result.MyBondReturn;
                strategyTitle.MyReturn = result.MyReturn;
                strategyTitle.RankReturn = result.RankReturn;
                strategyTitle.SumStock = result.SumStock;
            }
            else
            {
                //sql = "select sum(Investment*StockRate/100) from FundInfos fi right join MyFunds mf on fi.FundNo=mf.FundNo where StockRate<1 and fi.FundName not like '%ETF%';";
                //da = MySqlHelper.ExecuteTable(sql, pars);
                //double stock1 = Convert.ToDouble(da.Rows[0][0].ToString());

                //sql = "select sum(Investment*StockRate/100) from FundInfos fi right join MyFunds mf on fi.FundNo=mf.FundNo where 1<StockRate and StockRate<50 and fi.FundName not like '%ETF%';";
                //da = MySqlHelper.ExecuteTable(sql, pars);
                //double stock2 = Convert.ToDouble(da.Rows[0][0].ToString());

                sql = "select sum(Investment*StockRate/100) from FundInfos fi right join MyFunds mf on fi.FundNo=mf.FundNo where 0<StockRate and StockRate<30 and fi.FundName not like '%ETF%';";
                da = MySqlHelper.ExecuteTable(sql, pars);
                double stock3 = Convert.ToDouble(da.Rows[0][0].ToString());

                sql = "select sum(Investment) from FundInfos fi right join MyFunds mf on fi.FundNo=mf.FundNo where 30<=StockRate or fi.FundName like '%ETF%';";
                da = MySqlHelper.ExecuteTable(sql, pars);
                double stock4 = Convert.ToDouble(da.Rows[0][0].ToString());

                double sumstock = stock3 + stock4;

                strategyTitle.SumStock = sumstock;

                sql = "select sum(Investment) from MyFunds where Strategy like '%XYG%'";
                da = MySqlHelper.ExecuteTable(sql, pars);
                double XYGStock = Convert.ToDouble(da.Rows[0][0].ToString());
                strategyTitle.XYGRate = Math.Round(XYGStock * 100 / sumstock, 2);

                sql = "select sum(Investment) from MyFunds where Strategy like '%KJ%'";
                da = MySqlHelper.ExecuteTable(sql, pars);
                double KJStock = Convert.ToDouble(da.Rows[0][0].ToString());
                strategyTitle.KJRate = Math.Round(KJStock * 100 / sumstock, 2);

                sql = "select sum(Investment) from MyFunds";
                da = MySqlHelper.ExecuteTable(sql, pars);
                double totalinvestment = Convert.ToDouble(da.Rows[0][0].ToString());
                strategyTitle.MyStockRate = Math.Round(sumstock*100/totalinvestment, 2);

                sql = "select sum(Investment*ReturnRate) from FundInfos fi right join MyFunds mf on fi.FundNo=mf.FundNo where Strategy like '%XYG%'";
                da = MySqlHelper.ExecuteTable(sql, pars);
                double xygreturn = Convert.ToDouble(da.Rows[0][0].ToString());
                strategyTitle.XYGReturn = Math.Round(xygreturn*100/XYGStock, 2);

                sql = "select sum(Investment*ReturnRate) from FundInfos fi right join MyFunds mf on fi.FundNo=mf.FundNo where Strategy like '%KJ%'";
                da = MySqlHelper.ExecuteTable(sql, pars);
                double kjreturn = Convert.ToDouble(da.Rows[0][0].ToString());
                strategyTitle.KJReturn = Math.Round(kjreturn*100/ KJStock, 2);

                sql = "select sum(Investment*ReturnRate) from FundInfos fi right join MyFunds mf on fi.FundNo=mf.FundNo where StockRate>=30 or FundName like '%ETF%'";
                da = MySqlHelper.ExecuteTable(sql, pars);
                double mystockreturn = Convert.ToDouble(da.Rows[0][0].ToString());
                strategyTitle.MyStockReturn = Math.Round(mystockreturn * 100/sumstock, 2);

                sql = "select sum(Investment*ReturnRate) from FundInfos fi right join MyFunds mf on fi.FundNo=mf.FundNo where StockRate<30 and FundName not like '%ETF%'";
                da = MySqlHelper.ExecuteTable(sql, pars);
                double mybondreturn = Convert.ToDouble(da.Rows[0][0].ToString());
                sql = "select sum(Investment) from MyFunds where status=1";
                da = MySqlHelper.ExecuteTable(sql, pars);
                double total = Convert.ToDouble(da.Rows[0][0].ToString());
                strategyTitle.MyBondReturn = Math.Round(mybondreturn * 100 / (total- sumstock), 2);

                sql = "select sum(Investment) from MyFunds where Strategy like '%Rank%'";
                da = MySqlHelper.ExecuteTable(sql, pars);
                double rankStock = Convert.ToDouble(da.Rows[0][0].ToString());
                sql = "select sum(Investment*ReturnRate) from FundInfos fi right join MyFunds mf on fi.FundNo=mf.FundNo where Strategy like '%Rank%'";
                da = MySqlHelper.ExecuteTable(sql, pars);
                double rankReturn = Convert.ToDouble(da.Rows[0][0].ToString());
                strategyTitle.RankReturn = Math.Round(rankReturn * 100 / rankStock, 2);

                strategyTitle.MyReturn = Math.Round((mybondreturn + mystockreturn)*100/total,2);
            }

            redisHelper.SetRedisData<StrategyTitle>(todaystr+"-title",strategyTitle);
            
            return strategyTitle;
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
    }
}
