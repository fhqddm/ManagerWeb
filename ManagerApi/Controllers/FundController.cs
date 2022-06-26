using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Solo.BLL;
using Solo.Common;
using Solo.Model;
using Solo.Model.QueryModel;
using Solo.Model.ViewModel;

namespace ManagerWeb.Controllers
{
    public class FundController : Controller
    {
        private readonly IFundInfoService _fundInfoService;
        private readonly IConfiguration _configuration;
        private readonly IMyFundService _myFundService;
        private readonly ITransactionInfoService _transactionInfoService;
        private readonly IHolidayService _holidayService;

        public FundController(IFundInfoService fundInfoService,IConfiguration configuration,IMyFundService myFundService,
                              ITransactionInfoService transactionInfoService,IHolidayService holidayService)
        {
            _fundInfoService = fundInfoService;
            _configuration = configuration;
            _myFundService = myFundService;
            _transactionInfoService = transactionInfoService;
            _holidayService = holidayService;

        }



        [HttpPost]
        //[IgnoreAntiforgeryToken]

        public ActionResult<IEnumerable<FundView>> AjaxSearch(FundQuery query)
        {
            if (query.UserId == 0)
            {
                var userid = 0;
                int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userid);
                query.UserId = userid;             
            }

            string querystr = "";
            if (!string.IsNullOrEmpty(query.FundNo))
            {
                querystr += query.FundNo + " ";
            }
            if (!string.IsNullOrEmpty(query.FundName))
            {
                querystr += query.FundName + " ";
            }
            if (!string.IsNullOrEmpty(query.Strategy))
            {
                querystr += query.Strategy + " ";
            }
            if (!string.IsNullOrEmpty(query.MainIndustry))
            {
                querystr += query.MainIndustry + " ";
            }
            if (query.MinTotalScale != null && query.MaxTotalScale != null)
            {
                querystr += "TotalScale:"+ query.MinTotalScale.ToString()+"-"+query.MaxTotalScale.ToString()+ " ";
            }
            if (query.MinOrgHoldRate != null && query.MaxOrgHoldRate != null)
            {
                querystr += "OrgHoldRate:" + query.MinOrgHoldRate.ToString() + "-" + query.MaxOrgHoldRate.ToString() + " ";
            }
            if (query.MinRank != null && query.MaxRank != null)
            {
                querystr += "Rank:" + query.MinRank.ToString() + "-" + query.MaxRank.ToString() + " ";
            }
            if (query.MinStockRate != null && query.MaxStockRate != null)
            {
                querystr += "StockRate:" + query.MinStockRate.ToString() + "-" + query.MaxStockRate.ToString() + " ";
            }
            if (query.MinTop10Rate != null && query.MaxTop10Rate != null)
            {
                querystr += "Top10Rate:" + query.MinTop10Rate.ToString() + "-" + query.MaxTop10Rate.ToString() + " ";
            }
            if (query.MinAvgPE != null && query.MaxAvgPE != null)
            {
                querystr += "AvgPE:" + query.MinAvgPE.ToString() + "-" + query.MaxAvgPE.ToString() + " ";
            }
            if (query.MinAvgPB != null && query.MaxAvgPB != null)
            {
                querystr += "AvgPB:" + query.MinAvgPB.ToString() + "-" + query.MaxAvgPB.ToString() + " ";
            }


            Console.WriteLine(query.UserId.ToString() + ":AjaxSearch  "+ querystr + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            var list = _fundInfoService.GetFundInfosByQuery(query);
            //超过80条，只显示80条
            //if (list.Count>400 && query.resetRedis == false)
            //{
            //    list = list.Take(400).ToList();
            //}
            #region old
            //if (query.UserId>0 && query.HasAnalysis == true)
            //{

            //    var mfs = _myFundService.GetMyFunds(new MyFund { UserId = query.UserId},query.own);
            //    string mfstr = "";
            //    foreach (var mf in mfs)
            //    {
            //        mfstr += mf.FundNo + ",";
            //    }

            //    if (query.own != OWN.Empty)
            //    {
            //        list = list.Where(x => mfstr.Contains(x.FundNo)).ToList();
            //    }

            //    foreach (var item in list)
            //    {
            //        foreach (var mf in mfs)
            //        {
            //            if (item.FundNo == mf.FundNo)
            //            {
            //                item.status = mf.Status;
            //                item.Investment = mf.Investment;
            //                double viewrr = Math.Round(Convert.ToDouble(mf.ReturnRate) * 100, 2);
            //                item.ReturnRate = viewrr;
            //            }
            //        }
            //    }
            //}
            #endregion
            return Ok(list);
        }

        [HttpPost]
        public IActionResult GetAllMyFunds(bool resetRedis)
        {          
            var myfunds = _myFundService.GetAllMyFunds();
            if (resetRedis)
            {
                RedisConn writeConn = new RedisConn(false);
                writeConn.SetRedisData("MyFunds", myfunds);
                writeConn.Close();
            }
            return Ok(myfunds);
        }

        [HttpPost]
        public IActionResult GetMyFunds()
        {
            int userId = 0;
            if (int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId))
            {
                return Ok(_fundInfoService.GetFundInfosByQuery(new FundQuery { UserId = userId, Status = 3}));
            }
            return Ok("Error");
        }

        [HttpPost]
        public IActionResult AddToMyFunds(MyFund myFund)
        {
            if (myFund.UserId == 0)
            {
                var userid = 0;
                int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userid);
                myFund.UserId = userid;
            }
            Console.WriteLine(myFund.UserId.ToString() + ":添加自选基金  " + myFund.FundNo + "  " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            if (_myFundService.AddIntoMyFunds(myFund) == 1)
            {
                RedisConn writeConn = new RedisConn(false);
                var myfunds = writeConn.GetRedisData<List<MyFund>>("MyFunds");
                myfunds.Add(myFund);
                writeConn.SetRedisData("MyFunds", myfunds);
                writeConn.Close();
                return Ok(1);
            }
            return Ok("新增失败");
        }

        [HttpPost]
        public IActionResult RemoveMyFund(string fundNo)
        {
            int userId = 0;
            if (int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId))
            {
                if (_myFundService.RemoveMyFund(fundNo, userId)==1)
                {
                    Console.WriteLine(userId.ToString() + ":删除自选基金  " + fundNo + "  " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    RedisConn writeConn = new RedisConn(false);
                    var myfunds = writeConn.GetRedisData<List<MyFund>>("MyFunds");
                    myfunds.RemoveAt(myfunds.IndexOf(myfunds.Where(x => x.FundNo == fundNo && x.UserId == userId).FirstOrDefault()));
                    writeConn.SetRedisData("MyFunds", myfunds);
                    writeConn.Close();
                    return Ok(1);
                }
                
            }
            return Ok("移除失败");

            
        }

        [HttpGet]
        public IActionResult GetFundDetail(string FundNo)
        {
            
            int userId = 0;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
       

            FundDetailView fundDetailView = new FundDetailView();

            if (string.IsNullOrEmpty(FundNo))
            {
                FundNo = "005827";
            }

            //var result = LinearRegression.GetTrendEquation(FundNo);
            RedisConn readConn = new RedisConn(true);



            var result = readConn.GetRedisData<LineReturn>(FundNo);
            if (result == null)
            {
                result = LinearRegression.GetTrendEquation(FundNo);
                RedisConn writeConn = new RedisConn(false);
                writeConn.SetRedisData(FundNo, result);
                writeConn.Close();
            }


            if (result != null)
            {
                var data = result.dataSet;

                //string sm10="";
                FundView fundView = _fundInfoService.GetFundInfosByQuery(new FundQuery { FundNo = FundNo, MaxDutyDate = new DateTime(2100, 1, 1),UserId= userId }).FirstOrDefault();
                //AnalysisView anView = _fundInfoService.GetAnalysisByQuery(new AnalysisQuery { FundNo = FundNo }).Single(x => x.FundNo == FundNo);
                //var lastRecordTime = fundView.FundUpdateTime;

                Console.WriteLine(userId.ToString() + ": " + FundNo + "-" + fundView.FundName + "  " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                var lastRecordTime = Convert.ToDateTime(result.dataSet.Rows[result.dataSet.Rows.Count - 1][0]);

                int RowsCount = data.Rows.Count;
                //var dtList = _holidayService.GetWorkDayList(lastRecordTime, RowsCount);                


                var dataYlist = new List<Array>{ };
                var dataY1List = new List<Array> { };
                var dataY2List = new List<Array> { };
                var dataY3List = new List<Array> { };
                var dataY4List = new List<Array> { };
                var dataYM1List = new List<Array> { };
                var dataYM2List = new List<Array> { };
                var dataYD10List = new List<Array> { };
                var dataYD5List = new List<Array> { };

                for (int i = 0; i < RowsCount; i++)
                {

                    string timeStr = Convert.ToDateTime(data.Rows[i][0]).ToString("yyyy-MM-dd");
                    //fundDetailView.dataY.
                    dataYlist.Add(new string[] { timeStr, Math.Round(Convert.ToDouble(data.Rows[i][1]), 4).ToString() });

                    if (i >= RowsCount - 245)
                    {
                        var temp1 = Math.Round(i * result.points[0].X + result.points[0].Y, 4).ToString();
                        dataY1List.Add(new string[] { timeStr, temp1 });
                    }
                    if (i >= RowsCount - 490 && RowsCount > 245)
                    {
                        var temp2 = Math.Round(i * result.points[1].X + result.points[1].Y, 4).ToString();
                        dataY2List.Add(new string[] { timeStr, temp2 });
                    }
                    if (i >= RowsCount - 735 && RowsCount > 490)
                    {
                        var temp3 = Math.Round(i * result.points[2].X + result.points[2].Y, 4).ToString();
                        dataY3List.Add(new string[] { timeStr, temp3 });
                    }
                    if (i >= RowsCount - 1225 && RowsCount > 735)
                    {
                        var temp4 = Math.Round(i * result.points[3].X + result.points[3].Y, 4).ToString();
                        dataY4List.Add(new string[] { timeStr, temp4 });
                    }
                    if (i >= RowsCount - 60)
                    {
                        var tempm2 = Math.Round(i * result.points[4].X + result.points[4].Y, 4).ToString();
                        dataYM2List.Add(new string[] { timeStr, tempm2 });
                        //sm10 = sm10 + i + ",";
                    }
                    if (i >= RowsCount - 30)
                    {
                        //sm5++;
                        var tempm1 = Math.Round(i * result.points[5].X + result.points[5].Y, 4).ToString();
                        dataYM1List.Add(new string[] { timeStr, tempm1 });
                    }
                    if (i >= RowsCount - 10)
                    {
                        //sm5++;
                        var tempd10 = Math.Round(i * result.points[6].X + result.points[6].Y, 4).ToString();
                        dataYD10List.Add(new string[] { timeStr, tempd10 });
                    }
                    if (i >= RowsCount - 5)
                    {
                        //sm5++;
                        var tempd5 = Math.Round(i * result.points[7].X + result.points[7].Y, 4).ToString();
                        dataYD5List.Add(new string[] { timeStr, tempd5 });
                    }
                }
              
                
                var trans = _transactionInfoService.GetTransactionInfosByFundNo(FundNo, userId);

                var dataPointList = new List<string>();

                string dataPointStr = @"{ type: 'max', name: '最大值', itemStyle: {color: '#FF0000'}}";

                dataPointList.Add(dataPointStr); 
                //var dataPointList = new List<object>();
                //var obj = JsonConvert.DeserializeObject<object>(@"{ type: 'max', name: '最大值', itemStyle: {color: '#FF0000'}}");
                //dataPointList.Add(obj);


                if (FundNo == "444444" || FundNo == "555555" || FundNo == "666666")
                {
                    dataPointStr = "{ type: 'min', name: '最小值', itemStyle: {color: '#00FF00'}}";
                    dataPointList.Add(dataPointStr);
                }
                for (int i = 0; i < trans.Count; i++)
                {
                    //var index =(int)(lastRecordTime - trans[i].ConfirmTime).TotalDays;
                    //var index = dtList.IndexOf(trans[i].ConfirmTime);  
                    int index = 0;
                    for (int k = result.dataSet.Rows.Count - 1; k >= 0; k--)
                    {
                        //if (.ToString() == trans[i].ConfirmTime.ToString("yyyy-MM-dd"))
                        if (Convert.ToDateTime(result.dataSet.Rows[k][0]) == trans[i].ConfirmTime)
                        {
                            index = k;
                            break;
                        }
                    }

                    double netValue = Math.Round(Convert.ToDouble(result.dataSet.Rows[index][1]), 4);
                    if (trans[i].TransactionValue >= 0)
                    {
                        dataPointStr = @"{ name: '交易', xAxis: new Date('" + trans[i].ConfirmTime.ToString("yyyy/MM/dd") + "'),yAxis:" + netValue + ",value:'买入" + trans[i].TransactionValue + "',itemStyle: {color: '#FF3622'}}";
                        //obj = @"{ name: '交易', xAxis: new Date('" + trans[i].ConfirmTime.ToString("yyyy/MM/dd") + "'),yAxis:" + netValue + ",value:'买入" + trans[i].TransactionValue + "',itemStyle: {color: '#FF3622'}}";
                        //dataPointList.Add(obj);
                        dataPointList.Add(dataPointStr);
                    }
                    else
                    {
                        dataPointStr = @"{ name: '交易', xAxis: new Date('" + trans[i].ConfirmTime.ToString("yyyy/MM/dd") + "'),yAxis:" + netValue + ",value:'卖出" + trans[i].TransactionValue + "',itemStyle: {color: '#005656'}}";
                        dataPointList.Add(dataPointStr);
                    }
                    //if (i <= trans.Count - 1)
                    //{
                    //    dataPointStr += ",";

                    //}
                }
                //dataPointStr += "]";



                fundDetailView.fundView = fundView;

                double normalized = 1;
                if (RowsCount >= 1225)
                {
                    normalized = ((RowsCount - 1225) * result.points[3].X + result.points[3].Y);
                }
                else if (RowsCount < 1225 && RowsCount >= 735)
                {
                    normalized = ((RowsCount - 735) * result.points[2].X + result.points[2].Y);
                }
                else if (RowsCount < 735 && RowsCount >= 490)
                {
                    normalized = ((RowsCount - 490) * result.points[1].X + result.points[1].Y);
                }
                else if (RowsCount < 490 && RowsCount >= 245)
                {
                    normalized = ((RowsCount - 245) * result.points[0].X + result.points[0].Y);
                }
                else if (RowsCount < 245)
                {
                    normalized = result.points[0].Y;
                }

                if (RowsCount - 245 >= 0)
                {
                    fundDetailView.Linear1 = Math.Round(result.points[0].X * 10000 / normalized, 2);
                }
                if (RowsCount - 490 >= 0)
                {
                    fundDetailView.Linear2 = Math.Round(result.points[1].X * 10000 / normalized, 2);
                }
                if (RowsCount - 735 >= 0)
                {
                    fundDetailView.Linear3 = Math.Round(result.points[2].X * 10000 / normalized, 2);
                }
                if (RowsCount - 1225 >= 0)
                {
                    fundDetailView.Linear5 = Math.Round(result.points[3].X * 10000 / normalized, 2);
                }

                fundDetailView.dataY = dataYlist.ToArray();
                fundDetailView.dataYV1 = dataY1List.ToArray();
                fundDetailView.dataYV2 = dataY2List.ToArray();
                fundDetailView.dataYV3 = dataY3List.ToArray();
                fundDetailView.dataYV4 = dataY4List.ToArray();
                fundDetailView.dataYVM1 = dataYM1List.ToArray();
                fundDetailView.dataYVM2 = dataYM2List.ToArray();
                fundDetailView.dataYVD10 = dataYD10List.ToArray();
                fundDetailView.dataYVD5 = dataYD5List.ToArray();
                fundDetailView.fundView.ReturnRate = 0;
                fundDetailView.CurrentHold = 0;

                if (userId>0 && fundView.TotalFee>0)
                {
                    MyFund myFund = _myFundService.GetMyFund(new MyFund { UserId = userId, FundNo = FundNo });
                    if (myFund != null)
                    {
                        if (myFund.Investment>0)
                        {
                            fundDetailView.fundView.ReturnRate = Math.Round(Convert.ToDouble(myFund.ReturnRate) * 100, 2);
                            //fundDetailView.CurrentHold = Convert.ToInt32((1 + myFund.ReturnRate) * myFund.Investment);
                            fundDetailView.CurrentHold = Convert.ToInt32(fundDetailView.fundView.NetValue * myFund.HoldShares / myFund.Cost);
                        }
                        
                    }

                }
                

                //ViewData["dataPointStr"] =@"[{ type: 'max', name: '最大值' }, { name: '买入', xAxis: new Date('2020/2/20'), yAxis: 2, value: '买入1000' }]";
                fundDetailView.dataPoint = dataPointList.ToArray();
            }
            readConn.Close();
            return Ok(fundDetailView);
        }


        [HttpGet]
        public IActionResult GetCombination(string FundNo)
        {
            int userId = 0;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
            RedisConn readConn = new RedisConn(true);
            string fundName = _fundInfoService.GetFundInfosByQuery(new FundQuery { FundNo = FundNo, MaxDutyDate = new DateTime(2100, 1, 1), UserId = userId }).FirstOrDefault().FundName;

            //FundView fundView = _fundInfoService.GetFundInfosByQuery(new FundQuery { FundNo = FundNo, MaxDutyDate = new DateTime(2100, 1, 1), UserId = userId }).FirstOrDefault();

            //var compares = new List<string>() {"333333","555555"};

            var hs300 = readConn.GetRedisData<LineReturn>("333333")==null?null: readConn.GetRedisData<LineReturn>("333333").dataSet;
            RedisConn writeConn = new RedisConn(false);
            if (hs300 == null)
            {
                hs300 = LinearRegression.GetTrendEquation("333333").dataSet;
                writeConn.SetRedisData("333333", hs300);
            }

            var cyb = readConn.GetRedisData<LineReturn>("555555") == null ? null : readConn.GetRedisData<LineReturn>("555555").dataSet;
            if (cyb == null)
            {
                cyb = LinearRegression.GetTrendEquation("555555").dataSet;
                writeConn.SetRedisData("555555", cyb);
            }

            var zz500 = readConn.GetRedisData<LineReturn>("111111") == null ? null : readConn.GetRedisData<LineReturn>("111111").dataSet;
            if (zz500 == null)
            {
                zz500 = LinearRegression.GetTrendEquation("111111").dataSet;
                writeConn.SetRedisData("111111", zz500);
            }

            var fund = readConn.GetRedisData<LineReturn>(FundNo) == null ? null : readConn.GetRedisData<LineReturn>(FundNo).dataSet;
            if (fund == null)
            {
                fund = LinearRegression.GetTrendEquation(FundNo).dataSet;
                writeConn.SetRedisData(FundNo, fund);
            }
            //var hs300 = redisHelper.GetRedisData<LineReturn>("333333").dataSet;
            //var fund = redisHelper.GetRedisData<LineReturn>(FundNo).dataSet;

            int RowsCount = fund.Rows.Count;

            int fund_base_index = RowsCount - 245;
            int hs300_base_index = hs300.Rows.Count - 245;
            int cyb_base_index = cyb.Rows.Count - 245;
            int zz500_base_index = zz500.Rows.Count - 245;

            if (hs300.Rows[fund_base_index][0].ToString() != fund.Rows[fund_base_index][0].ToString())
            {
                for (int i = 0; i < hs300.Rows.Count; i++)
                {
                    if (hs300.Rows[i][0].ToString() == fund.Rows[fund_base_index][0].ToString())
                    {
                        hs300_base_index = i;                      
                    }
                    if (cyb.Rows[i][0].ToString() == fund.Rows[fund_base_index][0].ToString())
                    {
                        cyb_base_index = i;
                    }
                    if (zz500.Rows[i][0].ToString() == fund.Rows[fund_base_index][0].ToString())
                    {
                        zz500_base_index = i;
                    }
                }

            }


            double base_hs300_1y = Convert.ToDouble(hs300.Rows[hs300_base_index][1]);
            double fund_1y = Convert.ToDouble(fund.Rows[fund_base_index][1]);
            double cyb_1y = Convert.ToDouble(cyb.Rows[cyb_base_index][1]);
            double zz500_1y = Convert.ToDouble(zz500.Rows[zz500_base_index][1]);



            Dictionary<string, double> hs300_dic = new Dictionary<string, double>();
            Dictionary<string, double> cyb_dic = new Dictionary<string, double>();
            Dictionary<string, double> zz500_dic = new Dictionary<string, double>();

            foreach (DataRow row in hs300.Rows)
            {
                hs300_dic.Add(Convert.ToDateTime(row[0]).ToString("yyyy-MM-dd"), Convert.ToDouble(row[1]));
            }
            foreach (DataRow row in cyb.Rows)
            {
                cyb_dic.Add(Convert.ToDateTime(row[0]).ToString("yyyy-MM-dd"), Convert.ToDouble(row[1]));
            }
            foreach (DataRow row in zz500.Rows)
            {
                zz500_dic.Add(Convert.ToDateTime(row[0]).ToString("yyyy-MM-dd"), Convert.ToDouble(row[1]));
            }

            var data_hs300 = new List<Array> { };
            var data_fund = new List<Array> { };
            var data_cyb = new List<Array> { };
            var data_zz500 = new List<Array> { };


            for (int i = fund_base_index; i < RowsCount; i++)
            {
                try
                {
                    string dtstr = Convert.ToDateTime(fund.Rows[i][0]).ToString("yyyy-MM-dd");


                    var hs300_value = Math.Round(hs300_dic[dtstr] / base_hs300_1y, 4);
                    var fund_value = Math.Round(Convert.ToDouble(fund.Rows[i][1]) / fund_1y, 4);
                    var cyb_value = Math.Round(cyb_dic[dtstr] / cyb_1y, 4);
                    var zz500_value = Math.Round(zz500_dic[dtstr] / zz500_1y, 4);

                    data_hs300.Add(new string[] { dtstr, hs300_value.ToString() });
                    data_fund.Add(new string[] { dtstr, fund_value.ToString() });
                    data_cyb.Add(new string[] { dtstr, cyb_value.ToString() });
                    data_zz500.Add(new string[] { dtstr, zz500_value.ToString() });
                }
                catch 
                {

                }
                

            }
            CombinationView combinationView = new CombinationView();
            combinationView.FundNo = FundNo;
            combinationView.FundName = fundName;
            combinationView.dataFund = data_fund.ToArray();
            combinationView.dataHs300 = data_hs300.ToArray();
            combinationView.dataCyb = data_cyb.ToArray();
            combinationView.dataZz500 = data_zz500.ToArray();

            readConn.Close();
            writeConn.Close();
            return Ok(combinationView);

            //return Ok("Error");
            
        }



        [HttpGet]

        public IActionResult RefreshRedis()
        {
            //RedisHelper redis = new RedisHelper(Solo.BLL.AppConfigurtaionServices.Configuration.GetConnectionString("RedisConnection"));
            //redis.ClearDb();
            return Ok(true);
        }


        [HttpPost]
        public ActionResult<IEnumerable<AnalysisView>> AjaxStrategy(StrategyQuery query)
        {
            if(query.UserId==0)
            {
                var userid = 0;
                int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userid);
                query.UserId = userid;
            }
            Console.WriteLine(query.UserId.ToString() + ":Strategy  " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            var list = _fundInfoService.GetStrategyByQuery(query);
            var transactions = _transactionInfoService.GetRecentTransactions(query.UserId);
            foreach (var st in list)
            {
                st.Recommend = CalculateRecommend(st);
                var tran = transactions.Where(x => x.FundNo == st.FundNo).SingleOrDefault();
                st.RecentAddMoney = tran == null ? 0 : Convert.ToInt32(tran.TransactionValue);
            }
            return Ok(list);
        }

        [HttpPost]
        public IActionResult GetProcessPercent()
        {
            //double res = _fundInfoService.GetProcessPercent();
            //Random random = new Random();
            //var res1 = random.Next(1, 100);
            //var res2 = random.Next(1, 100);
            //var news = new { process1 = res1,process2 = res2 };
            return Ok(_fundInfoService.GetProcessPercent());


        }

        private double CalculateRecommend(StrategyView st)
        {
            double rescore = 0;
            try
            {
                //rescore = (double)st.ValuationScore * 0 - (double)st.Estimate * 20 - (double)st.D_ExpD5 * 4 -
                //          (double)st.D_ExpD10 * 10 - (double)st.D_ExpM1 * 12 - (double)st.D_ExpM2 * 12 - (double)st.D_Exp1 * 8 -
                //          (double)st.D_Exp2 * 6 - (double)st.D_Exp3 * 4 + (double)st.D_Top * 24;
                //if (st.LinearM1>1)
                //{
                //    rescore += 80;
                //}
                //if (st.LinearM2 > 1)
                //{
                //    rescore += 50;
                //}
                rescore = (double)st.ValuationScore * 0 - (double)st.D_Max5D * 10 - (double)st.D_Max10D * 15 - (double)st.D_A60 * 30 -
                          (double)st.D_ExpM2 * 5 - (double)st.D_Exp1 * 5 - (double)st.D_Exp2 * 3 - (double)st.D_Exp3 * 2 + (double)st.D_Top * 24;
                if (st.LinearM1 > 1)
                {
                    rescore += 80;
                }
                if (st.LinearM2 > 1)
                {
                    rescore += 50;
                }
                rescore = Math.Round(rescore / 10, 1);
                return rescore;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        [HttpPost]
        public IActionResult AddTransactionInfo(TransactionInfo trans)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            trans.UserId = userId;

            if (Convert.ToInt32(trans.ConfirmTime.DayOfWeek)>=1 && Convert.ToInt32(trans.ConfirmTime.DayOfWeek) <= 5 && !_holidayService.IsHoliday(trans.ConfirmTime))
            {
                if (_transactionInfoService.AddTransactionInfo(trans) == 1)
                {
                    RedisConn writeConn = new RedisConn(false);
                    writeConn.Lpush(trans.FundNo, trans.UserId);
                    writeConn.Close();
                    return Ok(true);
                }
                else
                {
                    return Ok(false);
                }
            }
            else
            {
                return Ok("添加失败，请检查日期是否为开市日!");
            }
            
        }

        [HttpPost]
        public IActionResult GetRecentTransactions()
        {
            int userId = 0;
            if (int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId))
            {
                var list = _transactionInfoService.GetRecentTransactions(userId);
                return Ok(list);
            }
            else
            {
                return Ok("未登录");
            }
            
        }

        [HttpPost]

        public IActionResult IsHoliday(bool resetRedis = false)
        {
            return Ok(_holidayService.IsHoliday(DateTime.Now, resetRedis));
        }


        [HttpPost]

        public IActionResult GenerateCombination(string[] fundnos, int outFundCount, int gtype,int minPoolRate,int prefer)
        {
            var userid = 0;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userid);
            Console.WriteLine(userid.ToString() + ":生成组合 " + fundnos[0] + ":minPoolRate "+ minPoolRate.ToString() + ":prefer " + prefer.ToString());
            if (gtype == 2)
            {
                List<string> stocklist = new List<string>();
                List<FundView> fundViews = new List<FundView>();
                List<FundView> finalFunds = new List<FundView>();
                fundnos = fundnos[0].Split(',');

                var baseViews = _fundInfoService.GetFundInfosByQuery(new FundQuery {MaxDutyDate=new DateTime(2100,1,1),bondstock=2 });

                foreach (var fundno in fundnos)
                {
                    try
                    {
                        var fundView = baseViews.Where(x => x.FundNo == fundno).FirstOrDefault();
                        fundViews.Add(fundView);
                    }
                    catch (Exception)
                    {

                    }                                    
                }
                fundViews = fundViews.OrderBy(x => x.Rank_Id).ToList();
                foreach (var fund in fundViews)
                {
                    if (outFundCount > 0)
                    {
                        int repeat = 0;
                        var temp = new List<string>();

                        fund.StockNos.Split(',').ToList().ForEach(x => {
                            if (!stocklist.Contains(x))
                            {
                                temp.Add(x);
                            }
                            else
                            {
                                repeat++;
                            }
                        });

                        //重复持仓必需小于4
                        if (repeat < 4)
                        {
                            stocklist.AddRange(temp);
                            finalFunds.Add(fund);
                            outFundCount--;
                        }
                    }
                    else
                    {
                        break;
                    }

                }
                return Ok(finalFunds);
            }
            else
            {
                var requires = fundnos[0].Split(',').ToList();
                var fundViews = _fundInfoService.GetFundInfosByQuery(new FundQuery { bondstock = 2 }).OrderBy(x => x.Rank_Id).Where(x => !(x.FundName.Contains("指数")|| x.FundName.Contains("沪深300") || x.FundName.Contains("上证50")) && (x.Rank_Id<500 || x.TotalScale>20 || x.Top10Rate>10));
                var finalFunds = new List<FundView>();
                int count = 10;
                if (prefer == 1)
                {
                    foreach (var fund in fundViews)
                    {
                        try
                        {
                            double poolRate = 0;
                            foreach (var require in requires)
                            {
                                if (fund.StockNos.Contains(require))
                                {
                                    int index = Array.IndexOf(fund.StockNos.Split(','), require);
                                    poolRate = poolRate + Convert.ToDouble(fund.StockPositions.Split(',')[index]);
                                }
                            }
                            if (poolRate > minPoolRate)
                            {
                                fund.PoolRate = Math.Round(poolRate,2);
                                finalFunds.Add(fund);
                            }
                        }
                        catch (Exception)
                        {

                            
                        }
                        

                    }
                    finalFunds = finalFunds.OrderByDescending(x => x.PoolRate).ToList();
                    if (finalFunds.Count> outFundCount)
                    {
                        finalFunds = finalFunds.Take(outFundCount).ToList();
                    }


                }
                else
                {
                    while (count > 0 && requires.Count > 0)
                    {
                        foreach (var fund in fundViews)
                        {
                            try
                            {
                                double poolRate = 0;
                                var holds = new List<string>();
                                foreach (var require in requires)
                                {
                                    if (fund.StockNos.Contains(require))
                                    {
                                        holds.Add(require);
                                        int index = Array.IndexOf(fund.StockNos.Split(','), require);
                                        poolRate = poolRate + Convert.ToDouble(fund.StockPositions.Split(',')[index]);
                                    }
                                }
                                if (holds.Count >= count && poolRate > minPoolRate)
                                {
                                    foreach (var hold in holds)
                                    {
                                        requires.Remove(hold);

                                    }
                                    fund.PoolRate = Math.Round(poolRate, 2);
                                    finalFunds.Add(fund);
                                    if (finalFunds.Count >= outFundCount)
                                    {
                                        break;
                                    }
                                }
                            }
                            catch (Exception)
                            {

                                
                            }
                            
                        }
                        if (finalFunds.Count >= outFundCount)
                        {
                            break;
                        }
                        count--;
                    }
                }
                
                

                return Ok(finalFunds);
            }

            
            
        }


        [Authorize(Policy = "RequireRoles")]
        [HttpPost]
        public IActionResult GetPositionViews()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //var userId = 1;
            var positionViews = _fundInfoService.GetFundPositionViews(userId);
            var stratgies = _fundInfoService.GetStrategyByQuery(new StrategyQuery {bondstock =2,UserId = userId,status=1});
            foreach (var positionView in positionViews)
            {               
                positionView.SuggestPosition = Math.Round(positionView.XYG_Position * 0.35 + positionView.KJ_Position * 0.2 + positionView.ALEX_Position * 0.25, 2);
                var strategy = stratgies.Where(x => x.FundNo == positionView.FundNo).FirstOrDefault();
                if (strategy != null)
                {
                    if (strategy.Rank_Id <= 10)
                    {
                        positionView.SuggestPosition = Math.Round(positionView.SuggestPosition + 2, 2);
                    }
                    if (strategy.Rank_Id <= 30)
                    {
                        positionView.SuggestPosition = Math.Round(positionView.SuggestPosition + 2, 2);
                    }
                }

                if (positionView.FundNo == "00YYYL")
                {
                    positionView.MyHolds = 0;
                    positionView.FundName = "医疗医药";
                    var yyyls = stratgies.Where(x => x.FundName.Contains("医")).ToList();
                    foreach (var yyyl in yyyls)
                    {
                        positionView.MyHolds = positionView.MyHolds + (double)yyyl.CurrentHold;
                    }                                   
                }
                else if (positionView.FundNo == "000XNY")
                {
                    positionView.MyHolds = 0;
                    positionView.FundName = "新能源";
                    var xnys = stratgies.Where(x => x.FundName.Contains("新能源")).ToList();
                    foreach (var xny in xnys)
                    {
                        positionView.MyHolds = positionView.MyHolds + (double)xny.CurrentHold;
                    }
                }
                else
                {
                    
                    if (strategy != null)
                    {                  
                        positionView.MyHolds = (double)strategy.CurrentHold;
                        positionView.FundName = strategy.FundName;
                    }
                }
                positionView.MyPosition = Math.Round(positionView.MyHolds / 2000, 2);//20万仓位
                
                positionView.Gap = Math.Round((positionView.SuggestPosition - positionView.MyPosition) * 2000, 2);//20万仓位
            }

            return Ok(positionViews);
        }



        [Authorize(Policy = "RequireRoles")]
        [HttpPost]
        public IActionResult UpdatePosition(MasterPosition masterPosition)
        {
            return Ok(_fundInfoService.FundPositionUpdate(masterPosition));
        }

        [Authorize(Policy = "RequireRoles")]
        [HttpPost]
        public IActionResult GetDailyRate()
        {
            return Ok(_fundInfoService.GetDailyRate());
        }


        [HttpPost]

        public IActionResult Diagnosis(string[] fundnos,string positionStr)
        {
            var userid = 0;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userid);
            Console.WriteLine(userid.ToString() + ":诊断 " + fundnos[0]+" 仓位 "+ positionStr);

            //string[] fundnos = new string[] { "110011", "001216", "206009", "110022" ,"000251", "320022" };
            fundnos = fundnos[0].Split(',');
            RedisConn readConn = new RedisConn(true);
            var positionStrArr = positionStr.Split(',');
            bool allZero = true;
            foreach (var item in positionStrArr)
            {
                if (item != "0" && item != "")
                {
                    allZero = false;
                }
            }
            List<double> pl = new List<double>();
            if (allZero == true)
            {
                for (int i = 0; i < fundnos.Count(); i++)
                {
                    pl.Add(0.01);
                }
            }
            else
            {
                for (int i = 0; i < positionStrArr.Count(); i++)
                {
                    if (positionStrArr[i]!= "")
                    {
                        pl.Add(Convert.ToDouble(positionStrArr[i]) / 100);
                    }
                    else
                    {
                        pl.Add(0);
                    }
                }
            }                       
            double[] positions = pl.ToArray();

            FundView fundView = new FundView();
            var redisResult = readConn.GetRedisData<List<FundView>>("FundViews");
            var hs300 = redisResult.Where(x => x.FundNo == "333333").SingleOrDefault();
            var zz500 = redisResult.Where(x => x.FundNo == "111111").SingleOrDefault();
            var funviews = redisResult.Where(x => fundnos.Contains(x.FundNo)).ToList();
            fundView.FundNo = "000000";
            fundView.FundName = "本组合";

            double sum_r1y_p = 0;
            double sum_r2y_p = 0;
            double sum_r3y_p = 0;
            double sum_r5y_p = 0;
            double sum_mstr_p = 0;
            double sum_std_p = 0;
            double sum_shp_p = 0;
            for (int i=0;i<fundnos.Count(); i++)
            {
                var fund = funviews.Where(x => x.FundNo == fundnos[i]).SingleOrDefault();
                if (fund.R1year != 0)
                {
                    sum_r1y_p = sum_r1y_p + positions[i];
                }
                if (fund.R2year != 0)
                {
                    sum_r2y_p = sum_r2y_p + positions[i];
                }
                if (fund.R3year != 0)
                {
                    sum_r3y_p = sum_r3y_p + positions[i];
                }
                if (fund.R5year != 0)
                {
                    sum_r5y_p = sum_r5y_p + positions[i];
                }
                if (fund.Maxretra != 0)
                {
                    sum_mstr_p = sum_mstr_p + positions[i];
                }
                if (fund.Sharp != 0)
                {
                    sum_shp_p = sum_shp_p + positions[i];
                }
                if (fund.Stddev != 0)
                {
                    sum_std_p = sum_std_p + positions[i];
                }
                fundView.R1year = fundView.R1year + fund.R1year * positions[i];
                fundView.R2year = fundView.R2year + fund.R2year * positions[i];
                fundView.R3year = fundView.R3year + fund.R3year * positions[i];
                fundView.R5year = fundView.R5year + fund.R5year * positions[i];
                fundView.Sharp = fundView.Sharp + fund.Sharp * positions[i];
                fundView.Maxretra = fundView.Maxretra + fund.Maxretra * positions[i]*0.9;
                fundView.Stddev = fundView.Stddev + fund.Stddev * positions[i]*0.8;
            }
            if (sum_r1y_p>0)
            {
                fundView.R1year = fundView.R1year / sum_r1y_p;
            }
            if (sum_r2y_p > 0)
            {
                fundView.R2year = fundView.R2year / sum_r2y_p;
            }
            if (sum_r3y_p > 0)
            {
                fundView.R3year = fundView.R3year / sum_r3y_p;
            }
            if (sum_r5y_p > 0)
            {
                fundView.R5year = fundView.R5year / sum_r5y_p;
            }
            if (sum_mstr_p > 0)
            {
                fundView.Maxretra = fundView.Maxretra / sum_mstr_p;
            }
            if (sum_shp_p > 0)
            {
                fundView.Sharp = fundView.Sharp / sum_shp_p;
            }
            if (sum_std_p > 0)
            {
                fundView.Stddev = fundView.Stddev / sum_std_p;
            }

            fundView.R1year = Math.Round(Convert.ToDouble(fundView.R1year), 2);
            fundView.R2year = Math.Round(Convert.ToDouble(fundView.R2year), 2);
            fundView.R3year = Math.Round(Convert.ToDouble(fundView.R3year), 2);
            fundView.R5year = Math.Round(Convert.ToDouble(fundView.R5year), 2);

            fundView.Maxretra = Math.Round(fundView.Maxretra, 2);
            fundView.Stddev = Math.Round(fundView.Stddev, 2);
            fundView.Sharp = Math.Round(fundView.Sharp, 2);

            var result = new List<FundView>() { fundView, hs300, zz500 };

            readConn.Close();
            return Ok(result);

        }


        //public IActionResult Diagnosis(string[] fundnos)
        //{

        //    var userid = 0;
        //    int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userid);
        //    Console.WriteLine(userid.ToString() + ":诊断 " + fundnos[0]);

        //    //string[] fundnos = new string[] { "110011", "001216", "206009", "110022" ,"000251", "320022" };
        //    fundnos = fundnos[0].Split(',');
        //    RedisConn readConn = new RedisConn(true);

        //    FundView fundView = new FundView();
        //    var redisResult = readConn.GetRedisData<List<FundView>>("FundViews");
        //    var hs300 = redisResult.Where(x => x.FundNo == "333333").SingleOrDefault();
        //    var zz500 = redisResult.Where(x => x.FundNo == "111111").SingleOrDefault();
        //    var funviews = redisResult.Where(x => fundnos.Contains(x.FundNo)).ToList();
        //    fundView.FundNo = "000000";
        //    fundView.FundName = "本组合";

        //    if (funviews.Where(x => x.R1year != 0).ToList().Count == fundnos.Count())
        //    {
        //        fundView.R1year = funviews.Average(x => x.R1year);
        //    }
        //    else
        //    {
        //        fundView.R1year = funviews.Sum(x => x.R1year) / funviews.Where(x => x.R1year != 0).ToList().Count;
        //    }

        //    if (funviews.Where(x=>x.R2year != 0).ToList().Count == fundnos.Count())
        //    {
        //        fundView.R2year = funviews.Average(x => x.R2year);
        //    }
        //    else
        //    {
        //        fundView.R2year = funviews.Sum(x => x.R2year) / funviews.Where(x => x.R2year != 0).ToList().Count;
        //    }

        //    if (funviews.Where(x => x.R3year != 0).ToList().Count == fundnos.Count())
        //    {
        //        fundView.R3year = funviews.Average(x => x.R3year);
        //    }
        //    else
        //    {
        //        fundView.R3year = funviews.Sum(x => x.R3year) / funviews.Where(x => x.R3year != 0).ToList().Count;
        //    }

        //    if (funviews.Where(x => x.R5year != 0).ToList().Count == fundnos.Count())
        //    {
        //        fundView.R5year = funviews.Average(x => x.R5year);
        //    }
        //    else
        //    {
        //        fundView.R5year = funviews.Sum(x => x.R5year) / funviews.Where(x => x.R5year != 0).ToList().Count;
        //    }

        //    if (funviews.Where(x => x.Maxretra != 0).ToList().Count == fundnos.Count())
        //    {
        //        fundView.Maxretra = funviews.Average(x => x.Maxretra)*0.9;
        //    }
        //    else
        //    {
        //        fundView.Maxretra = funviews.Sum(x => x.Maxretra) * 0.9 / funviews.Where(x => x.Maxretra != 0).ToList().Count;
        //    }

        //    if (funviews.Where(x => x.Stddev != 0).ToList().Count == fundnos.Count())
        //    {
        //        fundView.Stddev = funviews.Average(x => x.Stddev) * 0.8;
        //    }
        //    else
        //    {
        //        fundView.Stddev = funviews.Sum(x => x.Stddev) * 0.8 / funviews.Where(x => x.Stddev != 0).ToList().Count;
        //    }

        //    if (funviews.Where(x => x.Sharp != 0).ToList().Count == fundnos.Count())
        //    {
        //        fundView.Sharp = funviews.Average(x => x.Sharp);
        //    }
        //    else
        //    {
        //        fundView.Sharp = funviews.Sum(x => x.Sharp) / funviews.Where(x => x.Sharp != 0).ToList().Count;
        //    }

        //    fundView.R1year = Math.Round(Convert.ToDouble(fundView.R1year), 2);
        //    fundView.R2year = (float)Math.Round(Convert.ToDouble(fundView.R2year), 2);
        //    fundView.R3year = (float)Math.Round(Convert.ToDouble(fundView.R3year), 2);
        //    fundView.R5year = (float)Math.Round(Convert.ToDouble(fundView.R5year), 2);

        //    fundView.Maxretra = Math.Round(fundView.Maxretra, 2);
        //    fundView.Stddev = Math.Round(fundView.Stddev, 2);
        //    fundView.Sharp = Math.Round(fundView.Sharp, 2);

        //    var result = new List<FundView>() { fundView, hs300, zz500 };

        //    readConn.Close();
        //    return Ok(result);
        //}

        [HttpPost]
        public IActionResult DiagnosisPc(string[] fundnos, int period,string positionStr)
        {
            var userid = 0;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userid);         
            if (period == 0 )
            {
                period = 245;
            }
            Console.WriteLine(userid.ToString() + ":Chart " + period.ToString() + " " + fundnos[0] + " 仓位 " + positionStr);
            //string[] fundnos = new string[] { "110011", "001216", "206009", "110022", "000251", "320022" };
            fundnos = fundnos[0].Split(',');
            int fundCount = fundnos.Count();
            var positionStrArr = positionStr.Split(',');
            bool allZero = true;
            foreach (var item in positionStrArr)
            {
                if (item != "0" && item !="")
                {
                    allZero = false;
                }
            }
            List<double> pl = new List<double>();
            if (allZero == true)
            {
                for (int i = 0; i < fundnos.Count(); i++)
                {
                    pl.Add(0.01);
                }
            }
            else
            {
                for (int i = 0; i < positionStrArr.Count(); i++)
                {
                    if (positionStrArr[i] != "")
                    {
                        pl.Add(Convert.ToDouble(positionStrArr[i]) / 100);
                    }
                    else
                    {
                        pl.Add(0);
                    }
                }
            }
            double[] positions = pl.ToArray();
            RedisConn writeConn = new RedisConn(false);
            List<DataTable> listdt = new List<DataTable>();
            foreach (var fundno in fundnos)
            {
                var result = writeConn.GetRedisData<LineReturn>(fundno);
                if (result == null)
                {
                    result = LinearRegression.GetTrendEquation(fundno);
                    writeConn.SetRedisData(fundno, result);
                }
                if (result.dataSet.Rows.Count>= period)
                {
                    listdt.Add(result.dataSet);
                }
                
            }

            var hs300 = writeConn.GetRedisData<LineReturn>("333333") == null ? null : writeConn.GetRedisData<LineReturn>("333333").dataSet;
            if (hs300 == null)
            {
                hs300 = LinearRegression.GetTrendEquation("333333").dataSet;
                writeConn.SetRedisData("333333", hs300);
            }

            var cyb = writeConn.GetRedisData<LineReturn>("555555") == null ? null : writeConn.GetRedisData<LineReturn>("555555").dataSet;
            if (cyb == null)
            {
                cyb = LinearRegression.GetTrendEquation("555555").dataSet;
                writeConn.SetRedisData("555555", cyb);
            }

            var zz500 = writeConn.GetRedisData<LineReturn>("111111") == null ? null : writeConn.GetRedisData<LineReturn>("111111").dataSet;
            if (zz500 == null)
            {
                zz500 = LinearRegression.GetTrendEquation("111111").dataSet;
                writeConn.SetRedisData("111111", zz500);
            }



            List<DateValue> fund = new List<DateValue>();


            for (int i = 1; i <= period; i++)
            {
                string date = "";
                double netvalue = 0;
                double sum_nv_p = 0;
                //foreach (var dt in listdt)
                for(int j=0;j<listdt.Count;j++)
                {
                    if ((double)listdt[j].Rows[listdt[j].Rows.Count - i][1] > 0 && (date == "" || date == listdt[j].Rows[listdt[j].Rows.Count - i][0].ToString()))
                    {
                        date = listdt[j].Rows[listdt[j].Rows.Count - i][0].ToString();
                        //netvalue = netvalue + (double)dt.Rows[dt.Rows.Count - i][1];
                        netvalue = netvalue + positions[j]*(double)listdt[j].Rows[listdt[j].Rows.Count - i][1];
                        sum_nv_p = sum_nv_p + positions[j];
                    }
                }
                if (sum_nv_p>0)
                {
                    netvalue = netvalue / sum_nv_p;
                    fund.Add(new DateValue { Date = date, NetValue = netvalue });
                }
                

            }


            //int RowsCount = fund.Rows.Count;

            int hs300_base_index = hs300.Rows.Count - period;
            int cyb_base_index = cyb.Rows.Count - period;
            int zz500_base_index = zz500.Rows.Count - period;

            if (cyb.Rows[cyb_base_index][0].ToString() != fund[period-1].Date)
            {
                for (int i = 0; i < cyb.Rows.Count; i++)
                {
                    
                    if (i<hs300.Rows.Count && hs300.Rows[i][0].ToString() == fund[period - 1].Date)
                    {
                        hs300_base_index = i;
                    }
                    if (i <cyb.Rows.Count && cyb.Rows[i][0].ToString() == fund[period - 1].Date)
                    {
                        cyb_base_index = i;
                    }
                    if (i < zz500.Rows.Count && zz500.Rows[i][0].ToString() == fund[period - 1].Date)
                    {
                        zz500_base_index = i;
                    }
                }

            }

            double base_hs300_1y = Convert.ToDouble(hs300.Rows[hs300_base_index][1]);
            double fund_1y = Convert.ToDouble(fund[period - 1].NetValue);
            double cyb_1y = Convert.ToDouble(cyb.Rows[cyb_base_index][1]);
            double zz500_1y = Convert.ToDouble(zz500.Rows[zz500_base_index][1]);

            Dictionary<string, double> hs300_dic = new Dictionary<string, double>();
            Dictionary<string, double> cyb_dic = new Dictionary<string, double>();
            Dictionary<string, double> zz500_dic = new Dictionary<string, double>();
            Dictionary<string, double> fund_dic = new Dictionary<string, double>();

            foreach (DataRow row in hs300.Rows)
            {
                hs300_dic.Add(Convert.ToDateTime(row[0]).ToString("yyyy-MM-dd"), Convert.ToDouble(row[1]));
            }
            foreach (DataRow row in cyb.Rows)
            {
                cyb_dic.Add(Convert.ToDateTime(row[0]).ToString("yyyy-MM-dd"), Convert.ToDouble(row[1]));
            }
            foreach (DataRow row in zz500.Rows)
            {
                zz500_dic.Add(Convert.ToDateTime(row[0]).ToString("yyyy-MM-dd"), Convert.ToDouble(row[1]));
            }
            foreach (var netvalue in fund)
            {
                fund_dic.Add(netvalue.Date, netvalue.NetValue);
            }

            var data_hs300 = new List<Array> { };
            var data_fund = new List<Array> { };
            var data_cyb = new List<Array> { };
            var data_zz500 = new List<Array> { };


            for (int i = hs300_base_index; i < hs300.Rows.Count; i++)
            {
                try
                {
                    string dtstr = Convert.ToDateTime(hs300.Rows[i][0]).ToString("yyyy-MM-dd");

                    
                    var hs300_value = Math.Round(hs300_dic[dtstr] / base_hs300_1y, 4);
                    var fund_value = Math.Round(fund_dic[dtstr] / fund_1y, 4);
                    var cyb_value = Math.Round(cyb_dic[dtstr] / cyb_1y, 4);
                    var zz500_value = Math.Round(zz500_dic[dtstr] / zz500_1y, 4);

                    data_hs300.Add(new string[] { dtstr, hs300_value.ToString() });
                    data_fund.Add(new string[] { dtstr, fund_value.ToString() });
                    data_cyb.Add(new string[] { dtstr, cyb_value.ToString() });
                    data_zz500.Add(new string[] { dtstr, zz500_value.ToString() });
                }
                catch
                {

                }


            }
            CombinationView combinationView = new CombinationView();
            combinationView.FundNo = "";
            combinationView.FundName = "本组合";
            combinationView.dataFund = data_fund.ToArray();
            combinationView.dataHs300 = data_hs300.ToArray();
            combinationView.dataCyb = data_cyb.ToArray();
            combinationView.dataZz500 = data_zz500.ToArray();


            writeConn.Close();
            return Ok(combinationView);
        }



        //[HttpPost]
        //public IActionResult DiagnosisPc(string[] fundnos, int period, int[] positions)
        //{
        //    var userid = 0;
        //    int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userid);
        //    if (period == 0)
        //    {
        //        period = 245;
        //    }
        //    Console.WriteLine(userid.ToString() + ":诊断" + period.ToString() + " " + fundnos[0]);
        //    //string[] fundnos = new string[] { "110011", "001216", "206009", "110022", "000251", "320022" };
        //    fundnos = fundnos[0].Split(',');
        //    int fundCount = fundnos.Count();
        //    RedisConn writeConn = new RedisConn(false);
        //    List<DataTable> listdt = new List<DataTable>();
        //    foreach (var fundno in fundnos)
        //    {
        //        var result = writeConn.GetRedisData<LineReturn>(fundno);
        //        if (result == null)
        //        {
        //            result = LinearRegression.GetTrendEquation(fundno);
        //            writeConn.SetRedisData(fundno, result);
        //        }
        //        if (result.dataSet.Rows.Count >= period)
        //        {
        //            listdt.Add(result.dataSet);
        //        }

        //    }

        //    var hs300 = writeConn.GetRedisData<LineReturn>("333333") == null ? null : writeConn.GetRedisData<LineReturn>("333333").dataSet;
        //    if (hs300 == null)
        //    {
        //        hs300 = LinearRegression.GetTrendEquation("333333").dataSet;
        //        writeConn.SetRedisData("333333", hs300);
        //    }

        //    var cyb = writeConn.GetRedisData<LineReturn>("555555") == null ? null : writeConn.GetRedisData<LineReturn>("555555").dataSet;
        //    if (cyb == null)
        //    {
        //        cyb = LinearRegression.GetTrendEquation("555555").dataSet;
        //        writeConn.SetRedisData("555555", cyb);
        //    }

        //    var zz500 = writeConn.GetRedisData<LineReturn>("111111") == null ? null : writeConn.GetRedisData<LineReturn>("111111").dataSet;
        //    if (zz500 == null)
        //    {
        //        zz500 = LinearRegression.GetTrendEquation("111111").dataSet;
        //        writeConn.SetRedisData("111111", zz500);
        //    }



        //    List<DateValue> fund = new List<DateValue>();


        //    for (int i = 1; i <= period; i++)
        //    {
        //        string date = "";
        //        double netvalue = 0;
        //        int count = 0;
        //        foreach (var dt in listdt)
        //        {
        //            if ((double)dt.Rows[dt.Rows.Count - i][1] > 0 && (date == "" || date == dt.Rows[dt.Rows.Count - i][0].ToString()))
        //            {
        //                date = dt.Rows[dt.Rows.Count - i][0].ToString();
        //                netvalue = netvalue + (double)dt.Rows[dt.Rows.Count - i][1];
        //                count++;
        //            }
        //        }
        //        if (count > 0 && date != "")
        //        {
        //            fund.Add(new DateValue { Date = date, NetValue = Math.Round(netvalue / count, 4) });
        //        }


        //    }


        //    //int RowsCount = fund.Rows.Count;

        //    int hs300_base_index = hs300.Rows.Count - period;
        //    int cyb_base_index = cyb.Rows.Count - period;
        //    int zz500_base_index = zz500.Rows.Count - period;

        //    if (cyb.Rows[cyb_base_index][0].ToString() != fund[period - 1].Date)
        //    {
        //        for (int i = 0; i < cyb.Rows.Count; i++)
        //        {

        //            if (i < hs300.Rows.Count && hs300.Rows[i][0].ToString() == fund[period - 1].Date)
        //            {
        //                hs300_base_index = i;
        //            }
        //            if (i < cyb.Rows.Count && cyb.Rows[i][0].ToString() == fund[period - 1].Date)
        //            {
        //                cyb_base_index = i;
        //            }
        //            if (i < zz500.Rows.Count && zz500.Rows[i][0].ToString() == fund[period - 1].Date)
        //            {
        //                zz500_base_index = i;
        //            }
        //        }

        //    }

        //    double base_hs300_1y = Convert.ToDouble(hs300.Rows[hs300_base_index][1]);
        //    double fund_1y = Convert.ToDouble(fund[period - 1].NetValue);
        //    double cyb_1y = Convert.ToDouble(cyb.Rows[cyb_base_index][1]);
        //    double zz500_1y = Convert.ToDouble(zz500.Rows[zz500_base_index][1]);

        //    Dictionary<string, double> hs300_dic = new Dictionary<string, double>();
        //    Dictionary<string, double> cyb_dic = new Dictionary<string, double>();
        //    Dictionary<string, double> zz500_dic = new Dictionary<string, double>();
        //    Dictionary<string, double> fund_dic = new Dictionary<string, double>();

        //    foreach (DataRow row in hs300.Rows)
        //    {
        //        hs300_dic.Add(Convert.ToDateTime(row[0]).ToString("yyyy-MM-dd"), Convert.ToDouble(row[1]));
        //    }
        //    foreach (DataRow row in cyb.Rows)
        //    {
        //        cyb_dic.Add(Convert.ToDateTime(row[0]).ToString("yyyy-MM-dd"), Convert.ToDouble(row[1]));
        //    }
        //    foreach (DataRow row in zz500.Rows)
        //    {
        //        zz500_dic.Add(Convert.ToDateTime(row[0]).ToString("yyyy-MM-dd"), Convert.ToDouble(row[1]));
        //    }
        //    foreach (var netvalue in fund)
        //    {
        //        fund_dic.Add(netvalue.Date, netvalue.NetValue);
        //    }

        //    var data_hs300 = new List<Array> { };
        //    var data_fund = new List<Array> { };
        //    var data_cyb = new List<Array> { };
        //    var data_zz500 = new List<Array> { };


        //    for (int i = hs300_base_index; i < hs300.Rows.Count; i++)
        //    {
        //        try
        //        {
        //            string dtstr = Convert.ToDateTime(hs300.Rows[i][0]).ToString("yyyy-MM-dd");


        //            var hs300_value = Math.Round(hs300_dic[dtstr] / base_hs300_1y, 4);
        //            var fund_value = Math.Round(fund_dic[dtstr] / fund_1y, 4);
        //            var cyb_value = Math.Round(cyb_dic[dtstr] / cyb_1y, 4);
        //            var zz500_value = Math.Round(zz500_dic[dtstr] / zz500_1y, 4);

        //            data_hs300.Add(new string[] { dtstr, hs300_value.ToString() });
        //            data_fund.Add(new string[] { dtstr, fund_value.ToString() });
        //            data_cyb.Add(new string[] { dtstr, cyb_value.ToString() });
        //            data_zz500.Add(new string[] { dtstr, zz500_value.ToString() });
        //        }
        //        catch
        //        {

        //        }


        //    }
        //    CombinationView combinationView = new CombinationView();
        //    combinationView.FundNo = "";
        //    combinationView.FundName = "本组合";
        //    combinationView.dataFund = data_fund.ToArray();
        //    combinationView.dataHs300 = data_hs300.ToArray();
        //    combinationView.dataCyb = data_cyb.ToArray();
        //    combinationView.dataZz500 = data_zz500.ToArray();


        //    writeConn.Close();
        //    return Ok(combinationView);
        //}

        [HttpPost]
        public IActionResult GetPoolFundViews(FundQuery query)
        {
            var userid = 0;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userid);
            Console.WriteLine(userid.ToString() + ":GetPoolFundViews  " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            List<PoolFundView> poolFundViews = new List<PoolFundView>();
            var list = _fundInfoService.GetFundInfosByQuery(query);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<FundView, PoolFundView>());
            var mapper = config.CreateMapper();
            foreach (var item in list)
            {
                poolFundViews.Add(mapper.Map<PoolFundView>(item));
            }           
            return Ok(poolFundViews);
        }

        [HttpPost]
        public IActionResult GetStockAnalysis(string fundnos)
        {
            var fundViews = _fundInfoService.GetFundInfosByQuery(new FundQuery { bondstock = 2 });
            var funds = fundViews.Where(x => fundnos.Contains(x.FundNo)).ToList();
            var stockAnalysis = new List<StockAnalysisView>();
            foreach (var fund in funds)
            {
                for (int i=0;i<fund.StockNames.Split(',').Count();i++)
                {
                    var sa = stockAnalysis.Where(x => x.StockName == fund.StockNames.Split(',')[i]).SingleOrDefault();
                    if (sa == null)
                    {
                        sa = new StockAnalysisView();
                        sa.StockName = fund.StockNames.Split(',')[i];
                        sa.Repeat = 1;
                        sa.FundPositionStr = fund.FundName + ":" + fund.StockPositions.Split(',')[i] + "%  ";
                        stockAnalysis.Add(sa);
                    }
                    else
                    {
                        sa.Repeat = sa.Repeat + 1;
                        sa.FundPositionStr = sa.FundPositionStr + fund.FundName + ":" + fund.StockPositions.Split(',')[i] + "%  ";
                    }
                }
            }
            if (stockAnalysis != null)
            {
                stockAnalysis = stockAnalysis.Where(x=>x.Repeat>=3).OrderByDescending(x => x.Repeat).ToList();
            }
            return Ok(stockAnalysis);


        }

        [HttpPost]
        public IActionResult GetDiagnosisFunds(FundQuery query)
        {
            var userid = 0;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userid);
            Console.WriteLine(userid.ToString() + ":GetDiagnosisFunds  " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            List<DiagnosisFund> diagnosisFunds = new List<DiagnosisFund>();
            var list = _fundInfoService.GetFundInfosByQuery(query);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<FundView, DiagnosisFund>());
            var mapper = config.CreateMapper();
            foreach (var item in list)
            {
                diagnosisFunds.Add(mapper.Map<DiagnosisFund>(item));
            }
            return Ok(diagnosisFunds);
        }



        [HttpPost]
        public IActionResult GetFundTipByQueryStr(string queryStr)
        {
            var res = _fundInfoService.GetFundPureTips("");
            if (MyUtils.HasChinese(queryStr))
            {
                res = res.Where(x => x.FundName.Contains(queryStr)).ToList();
                return Ok(res);
            }
            else
            {
                res = res.Where(x => x.FundNo.StartsWith(queryStr)).ToList();
                return Ok(res);
            }          
        }
        [HttpPost]
        public IActionResult AddDiagnosisFund(string queryStr)
        {
            try
            {
                var userid = 0;
                int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userid);
                Console.WriteLine(userid.ToString() + ":AddDiagnosisFund  " + queryStr + "   " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                DiagnosisFund diagnosisFund = new DiagnosisFund();
                var fundView = _fundInfoService.GetFundInfosByQuery(new FundQuery { FundNo = queryStr.Split(':')[0] }).SingleOrDefault();
                var config = new MapperConfiguration(cfg => cfg.CreateMap<FundView, DiagnosisFund>());
                var mapper = config.CreateMapper();
                diagnosisFund = mapper.Map<DiagnosisFund>(fundView);
                return Ok(diagnosisFund);
            }
            catch (Exception ex)
            {

                Console.WriteLine(queryStr+ "  "+ ex.Message);
                return null;
            }
            
        }

        [HttpPost]
        public IActionResult BatchDiagnosisFunds(string strategy)
        {
            var userid = 0;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userid);
            Console.WriteLine(userid.ToString() + ":  batchDiagnosisFunds " + strategy + "   " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            List<DiagnosisFund> diagnosisFunds = new List<DiagnosisFund>();
            var list = _fundInfoService.GetFundInfosByQuery(new FundQuery { MaxDutyDate=new DateTime(2100,1,1),isnew=false,bondstock=2, HasAnalysis=true,UserId=userid });
            if (strategy == "MyFunds")
            {
                list = list.Where(x=>x.UserId==userid && x.status>0).ToList();
            }
            else
            {
                list = list.Where(x => x.Strategy.Contains(strategy)).ToList();
            }
            var config = new MapperConfiguration(cfg => cfg.CreateMap<FundView, DiagnosisFund>());
            var mapper = config.CreateMapper();
            foreach (var item in list)
            {
                
                diagnosisFunds.Add(mapper.Map<DiagnosisFund>(item));
            }
            return Ok(diagnosisFunds);
        }

        [HttpPost]
        public IActionResult GetFundPureTips(string fundstr)
        {
            var res = _fundInfoService.GetFundPureTips(fundstr);
            return Ok(res);
        }

        [HttpPost]
        public IActionResult AddPoolFund(string queryStr)
        {
            try
            {
                var userid = 0;
                int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userid);
                Console.WriteLine(userid.ToString() + ":AddPoolFund  " + queryStr + "   " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                PoolFundView poolFund = new PoolFundView();
                var fundView = _fundInfoService.GetFundInfosByQuery(new FundQuery { FundNo = queryStr.Split(':')[0] }).SingleOrDefault();
                var config = new MapperConfiguration(cfg => cfg.CreateMap<FundView, PoolFundView>());
                var mapper = config.CreateMapper();
                poolFund = mapper.Map<PoolFundView>(fundView);
                return Ok(poolFund);
            }
            catch (Exception ex)
            {

                Console.WriteLine(queryStr + "  " + ex.Message);
                return null;
            }
            
        }

        [HttpPost]
        public IActionResult BatchPoolFunds(string strategy)
        {
            var userid = 0;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userid);
            Console.WriteLine(userid.ToString() + ":  batchPoolFunds " + strategy + "   " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            List<PoolFundView> poolFunds = new List<PoolFundView>();
            var list = _fundInfoService.GetFundInfosByQuery(new FundQuery { MaxDutyDate = new DateTime(2100, 1, 1), isnew = false, bondstock = 2, HasAnalysis = true, UserId = userid });
            if (strategy == "MyFunds")
            {
                list = list.Where(x => x.UserId == userid && x.status > 0).ToList();
            }
            else
            {
                list = list.Where(x => x.Strategy.Contains(strategy)).ToList();
            }
            var config = new MapperConfiguration(cfg => cfg.CreateMap<FundView, PoolFundView>());
            var mapper = config.CreateMapper();
            foreach (var item in list)
            {
                poolFunds.Add(mapper.Map<PoolFundView>(item));
            }
            return Ok(poolFunds);
        }


        private class DateValue
        {
            public string Date;
            public double NetValue;
        }
    }

   
}