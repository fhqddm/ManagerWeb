using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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

        public FundController(IFundInfoService fundInfoService,IConfiguration configuration,IMyFundService myFundService,ITransactionInfoService transactionInfoService,IHolidayService holidayService)
        {
            _fundInfoService = fundInfoService;
            _configuration = configuration;
            _myFundService = myFundService;
            _transactionInfoService = transactionInfoService;
            _holidayService = holidayService;
        }
        public IActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                ViewData["userName"] = HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.Name).Value;
            }
            var list = _fundInfoService.GetFundInfosByQuery(new FundQuery { own = OWN.OnlyHoldWaits });
            string straa="";
            foreach (var item in list)
            {
                straa = straa +"'" +item.FundNo + "'" + ",";
            }
            ViewData["fvList"] = list;
            return View();
        }

        [HttpPost]
        public IActionResult Search([FromForm]FundQuery query)
        {
            var list = _fundInfoService.GetFundInfosByQuery(query);
            ViewData["fvList"] = list;
            return View("Index");
        }

        [HttpPost]

        public ActionResult<IEnumerable<FundView>> AjaxSearch(FundQuery query)
        {
            var list = _fundInfoService.GetFundInfosByQuery(query);
            ViewData["fvList"] = list;
            return Ok(list);
        }

        [HttpPost]
        public IActionResult AddToMyFunds(MyFund myFund)
        {
            return Ok(_myFundService.AddIntoMyFunds(myFund));
        }

        [HttpPost]
        public IActionResult RemoveMyFund(string fundNo)
        {
            return Ok(_myFundService.RemoveMyFund(fundNo));
        }

        public IActionResult Analysis()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                ViewData["userName"] = HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.Name).Value;
            }
            var avList = _fundInfoService.GetAnalysisByQuery(new AnalysisQuery { own = OWN.OnlyHoldWaits,MinRank_Id=1});

            ViewData["anList"] = avList;
            return View();
        }

        [HttpPost]
        public ActionResult<IEnumerable<AnalysisView>> AjaxAnalysis(AnalysisQuery query)
        {
            var list = _fundInfoService.GetAnalysisByQuery(query);

            if (query.MinD_Top!=null)
            {
                list = list.Where(x => x.D_Top >= query.MinD_Top).ToList();
            }
            if (query.MaxD_Top != null)
            {
                list = list.Where(x => x.D_Top <= query.MaxD_Top).ToList();
            }
            if (query.MinD_Exp1 != null)
            {
                list = list.Where(x => x.D_Exp1 >= query.MinD_Exp1).ToList();
            }
            if (query.MaxD_Exp1 != null)
            {
                list = list.Where(x => x.D_Exp1 <= query.MaxD_Exp1).ToList();
            }
            if (query.MaxD_Exp2 != null)
            {
                list = list.Where(x => x.D_Exp2 <= query.MaxD_Exp2).ToList();
            }
            if (query.MinD_Exp2 != null)
            {
                list = list.Where(x => x.D_Exp2 >= query.MinD_Exp2).ToList();
            }
            if (query.MaxD_Exp3 != null)
            {
                list = list.Where(x => x.D_Exp3 <= query.MaxD_Exp3).ToList();
            }
            if (query.MinD_Exp3 != null)
            {
                list = list.Where(x => x.D_Exp3 >= query.MinD_Exp3).ToList();
            }
            if (query.MaxLinear5 != null)
            {
                list = list.Where(x => x.Linear5 <= query.MaxLinear5).ToList();
            }
            if (query.MinLinear5 != null)
            {
                list = list.Where(x => x.Linear5 >= query.MinLinear5).ToList();
            }
            ViewData["anList"] = list;
            return Ok(list);
        }

        public IActionResult Test(string FundNo)
        {
            if (string.IsNullOrEmpty(FundNo))
            {
                FundNo = "485111";
            }

            //var result = LinearRegression.GetTrendEquation(FundNo);
            RedisHelper redisHelper = new RedisHelper(Solo.BLL.AppConfigurtaionServices.Configuration.GetConnectionString("RedisConnection"));
            var result = redisHelper.GetRedisData<LineReturn>(FundNo);
            if (result == null)
            {
                result = LinearRegression.GetTrendEquation(FundNo);
                redisHelper.SetRedisData(FundNo, result);
            }


            if (result!= null)
            {
                var data = result.dataSet;
                string xStr = "[";
                string yStr = "[";
                string y1ValueStr = "[";
                string y2ValueStr = "[";
                string y3ValueStr = "[";
                string y4ValueStr = "[";
                string m2ValueStr = "[";
                string m1ValueStr = "[";
                string d10ValueStr = "[";
                string d5ValueStr = "[";
                //string sm10="";
                FundView fundView = _fundInfoService.GetFundInfosByQuery(new FundQuery { FundNo = FundNo ,own=OWN.Empty,MaxDutyDate=new DateTime(2100,1,1)}).Single(x => x.FundNo == FundNo);
                //AnalysisView anView = _fundInfoService.GetAnalysisByQuery(new AnalysisQuery { FundNo = FundNo }).Single(x => x.FundNo == FundNo);
                //var lastRecordTime = fundView.FundUpdateTime;
                var lastRecordTime = Convert.ToDateTime(result.dataSet.Rows[result.dataSet.Rows.Count - 1][0]);

                int RowsCount = data.Rows.Count;
                //var dtList = _holidayService.GetWorkDayList(lastRecordTime, RowsCount);                

                Stopwatch watch = new Stopwatch();
                watch.Start();

                for (int i = 0; i < RowsCount; i++)
                {
                    //xStr += Math.Round(Convert.ToDouble(data.Rows[i]["NetValue"]),4);
                    //string timeStr = "[new Date(\"" + lastRecordTime.AddDays(i - RowsCount + 1).ToString("yyyy/MM/dd") + "\"),";
                    //string timeStr = "[new Date(\"" + dtList[i].ToString("yyyy/MM/dd") + "\"),";
                    string timeStr = "[new Date(\"" + Convert.ToDateTime(data.Rows[i][0]).ToString("yyyy/MM/dd") + "\"),";
                    string temp1 = "";
                    string temp2 = "";
                    string temp3 = "";
                    string temp4 = "";
                    string tempm2 = "";
                    string tempm1 = "";
                    string tempd10 = "";
                    string tempd5 = "";
                    if (i>=RowsCount-245)
                    {
                        temp1 = Math.Round(i * result.points[0].X + result.points[0].Y, 4).ToString();
                    }
                    if (i >= RowsCount - 490 && RowsCount>245)
                    {
                        temp2 = Math.Round(i * result.points[1].X + result.points[1].Y, 4).ToString();
                    }
                    if (i >= RowsCount - 735 && RowsCount> 490)
                    {
                        temp3 = Math.Round(i * result.points[2].X + result.points[2].Y, 4).ToString();
                    }
                    if (i >= RowsCount - 1225 && RowsCount > 735)
                    {
                        temp4 = Math.Round(i * result.points[3].X + result.points[3].Y, 4).ToString();
                    }
                    if (i>=RowsCount-60)
                    {
                        tempm2 = Math.Round(i * result.points[4].X + result.points[4].Y, 4).ToString();
                        //sm10 = sm10 + i + ",";
                    }
                    if (i >= RowsCount - 30)
                    {
                        //sm5++;
                        tempm1 = Math.Round(i * result.points[5].X + result.points[5].Y, 4).ToString();                       
                    }
                    if (i >= RowsCount - 10)
                    {
                        //sm5++;
                        tempd10 = Math.Round(i * result.points[6].X + result.points[6].Y, 4).ToString();
                    }
                    if (i >= RowsCount - 5)
                    {
                        //sm5++;
                        tempd5 = Math.Round(i * result.points[7].X + result.points[7].Y, 4).ToString();
                    }

                    y1ValueStr += timeStr + temp1 + "]";
                    y2ValueStr += timeStr + temp2 + "]";
                    y3ValueStr += timeStr + temp3 + "]";
                    y4ValueStr += timeStr + temp4 + "]";
                    m2ValueStr  += timeStr + tempm2 + "]";
                    m1ValueStr += timeStr + tempm1 + "]";
                    d10ValueStr += timeStr + tempd10 + "]";
                    d5ValueStr += timeStr + tempd5 + "]";
                    //xStr +="new Date(\""+ DateTime.Now.AddDays(i-RowsCount).ToString("yyyy/MM/dd")+ "\")";
                    //yStr += Convert.ToDouble(data.Rows[i][1]);
                    yStr += timeStr + Math.Round(Convert.ToDouble(data.Rows[i][1]),4)+"]";
                    if (i == RowsCount - 1)
                    {
                        xStr += "]";
                        yStr += ']';
                        y1ValueStr += "]";
                        y2ValueStr += "]";
                        y3ValueStr += "]";
                        y4ValueStr += "]";
                        m2ValueStr += "]";
                        m1ValueStr += "]";
                        d10ValueStr += "]";
                        d5ValueStr += "]";
                    }
                    else
                    {
                        xStr += ",";
                        yStr += ",";
                        y1ValueStr += ",";
                        y2ValueStr += ",";
                        y3ValueStr += ",";
                        y4ValueStr += ",";
                        m2ValueStr += ",";
                        m1ValueStr += ",";
                        d10ValueStr += ",";
                        d5ValueStr += ",";
                    }
                }


                var trans = _transactionInfoService.GetTransactionInfosByFundNo(FundNo);
                //string dataPointStr = @"[{ type: 'max', name: '最大值', color:'blue'},";
                string dataPointStr = @"[{ type: 'max', name: '最大值', itemStyle: {color: '#FF0000'}},";
                if (FundNo == "444444" || FundNo == "555555" || FundNo == "666666")
                {
                    dataPointStr += "{ type: 'min', name: '最小值', itemStyle: {color: '#00FF00'}},";
                }
                for (int i = 0; i < trans.Count; i++)
                {
                    //var index =(int)(lastRecordTime - trans[i].ConfirmTime).TotalDays;
                    //var index = dtList.IndexOf(trans[i].ConfirmTime);  
                    int index = 0;
                    for (int k = result.dataSet.Rows.Count-1; k >=0; k--)
                    {
                        //if (.ToString() == trans[i].ConfirmTime.ToString("yyyy-MM-dd"))
                        if(Convert.ToDateTime(result.dataSet.Rows[k][0])==trans[i].ConfirmTime)
                        {
                            index = k;
                            break;
                        }
                    }

                    double netValue = Math.Round(Convert.ToDouble(result.dataSet.Rows[index][1]),4);
                    if (trans[i].TransactionValue>=0)
                    {
                        dataPointStr += @"{ name: '交易', xAxis: new Date('" + trans[i].ConfirmTime.ToString("yyyy/MM/dd") + "'),yAxis:" + netValue + ",value:'买入" + trans[i].TransactionValue + "',itemStyle: {color: '#FF3622'}}";
                    }
                    else
                    {
                        dataPointStr += @"{ name: '交易', xAxis: new Date('" + trans[i].ConfirmTime.ToString("yyyy/MM/dd") + "'),yAxis:" + netValue + ",value:'卖出" + trans[i].TransactionValue + "',itemStyle: {color: '#005656'}}";
                    }
                    if (i<=trans.Count-1)
                    {
                        dataPointStr += ",";
                       
                    }
                }
                dataPointStr += "]";

                double time = watch.Elapsed.TotalSeconds;
                Console.WriteLine(time);
                watch.Stop();
                

                ViewData["xStr"] = xStr;
                ViewData["yStr"] = yStr;                            
                ViewData["y1ValueStr"] = y1ValueStr;
                ViewData["y2ValueStr"] = y2ValueStr;
                ViewData["y3ValueStr"] = y3ValueStr;
                ViewData["y4ValueStr"] = y4ValueStr;
                ViewData["m2ValueStr"] = m2ValueStr;
                ViewData["m1ValueStr"] = m1ValueStr;
                ViewData["d10ValueStr"] = d10ValueStr;
                ViewData["d5ValueStr"] = d5ValueStr;
                ViewData["FundNo"] = FundNo;
                ViewData["FundName"] = fundView.FundName;
                ViewData["TotalScale"] = fundView.TotalScale;
                ViewData["TotalFee"] = fundView.TotalFee;
                ViewData["OrgHoldRate"] = fundView.OrgHoldRate;
                ViewData["StockRate"] = fundView.StockRate;
                ViewData["DutyDate"] = fundView.DutyDate.ToString("yyyy-MM-dd");
                ViewData["R1day"] = fundView.R1day;
                ViewData["R1week"] = fundView.R1week;
                ViewData["R1month"] = fundView.R1month;
                ViewData["R3month"] = fundView.R3month;
                ViewData["R6month"] = fundView.R6month;
                ViewData["R1year"] = fundView.R1year;
                ViewData["R2year"] = fundView.R2year;
                ViewData["R3year"] = fundView.R3year;
                ViewData["R5year"] = fundView.R5year;
                ViewData["Over30d"] = fundView.Over30dFee;
                ViewData["Over1yFee"] = fundView.Over1yFee;
                //ViewData["Linear1"] = Math.Round(result.points[0].X * 10000/365,2);
                //ViewData["Linear2"] = Math.Round(result.points[1].X * 10000/ 365, 2);
                //ViewData["Linear3"] = Math.Round(result.points[2].X * 10000/ 365, 2);
                ViewData["Linear1"] = 0;
                ViewData["Linear2"] = 0;
                ViewData["Linear3"] = 0;
                ViewData["Linear5"] = 0;

                double normalized = 1;
                if (RowsCount>=1225)
                {
                    normalized = ((RowsCount - 1225) * result.points[3].X + result.points[3].Y);
                }
                else if (RowsCount<1225 && RowsCount>=735)
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
                else if(RowsCount < 245)
                {
                    normalized = result.points[0].Y;
                }

                if (RowsCount-245>=0)
                {
                    ViewData["Linear1"] = Math.Round(result.points[0].X * 10000 / normalized, 2);
                }                
                if (RowsCount - 490>=0)
                {
                    ViewData["Linear2"] = Math.Round(result.points[1].X * 10000 / normalized, 2);
                }
                if (RowsCount - 735>=0)
                {
                    ViewData["Linear3"] = Math.Round(result.points[2].X * 10000 / normalized, 2);
                }
                if (RowsCount - 1225>=0)
                {
                    ViewData["Linear5"] = Math.Round(result.points[3].X * 10000 / normalized, 2);
                }
                
                //ViewData["dataPointStr"] =@"[{ type: 'max', name: '最大值' }, { name: '买入', xAxis: new Date('2020/2/20'), yAxis: 2, value: '买入1000' }]";
                ViewData["dataPointStr"] = dataPointStr;

            }
            return View();
        }

        [HttpPost]

        public IActionResult RefreshRedis()
        {
            RedisHelper redis = new RedisHelper(Solo.BLL.AppConfigurtaionServices.Configuration.GetConnectionString("RedisConnection"));
            redis.ClearDb();
            return Ok();
        }

        public IActionResult Strategy()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                ViewData["userName"] = HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.Name).Value;
            }

            var stList = _fundInfoService.GetStrategyByQuery(new StrategyQuery {status=1,isnew=false,bondstock=3});

            foreach (var st in stList)
            {
                st.Recommend = CalculateRecommend(st);
            }

            //可优化
            var strategyTitle = _fundInfoService.GetStrategyTitle();
            ViewData["A-R1day"] = strategyTitle.AR1day;
            ViewData["C-R1day"] = strategyTitle.CR1day;
            ViewData["S-R1day"] = strategyTitle.SR1day;
            ViewData["A-R1month"] = strategyTitle.AR1month;
            ViewData["XYG-Rate"] = strategyTitle.XYGRate;
            ViewData["KJ-Rate"] = strategyTitle.KJRate;
            ViewData["XYG-Return"] = strategyTitle.XYGReturn;
            ViewData["KJ-Return"] = strategyTitle.KJReturn;
            ViewData["Rank-Return"] = strategyTitle.RankReturn;
            ViewData["MyStock-Return"] = strategyTitle.MyStockReturn;
            ViewData["MyBond-Return"] = strategyTitle.MyBondReturn;
            ViewData["My-Return"] = strategyTitle.MyReturn;
            ViewData["MyStock-Rate"] = strategyTitle.MyStockRate;
            ViewData["SumStock"] = strategyTitle.SumStock;
            ViewData["stList"] = stList;
            ViewData["listCount"] = stList.Count;
            ViewData["Total"] = stList.Sum(p => p.Investment);
            return View();
        }

        [HttpPost]
        public ActionResult<IEnumerable<AnalysisView>> AjaxStrategy(StrategyQuery query)
        {
            var list = _fundInfoService.GetStrategyByQuery(query);
            foreach (var st in list)
            {
                st.Recommend = CalculateRecommend(st);
            }
            var sum = list.Sum(p => p.Investment);
            ViewData["anList"] = list;
            //ViewData["listCount"] = list.Count;
            //ViewData["Total"] = list.Sum(p=>p.Investment);
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
                rescore = (double)st.ValuationScore * 1 - (double)st.Estimate * 20 - (double)st.D_ExpD5 * 4 - (double)st.D_ExpD10 * 10 -
                          (double)st.D_ExpM1 * 12 - (double)st.D_ExpM2 * 12 - (double)st.D_Exp1 * 8 - (double)st.D_Exp2 * 6 -
                          (double)st.D_Exp3 * 4 + (double)st.D_Top * 23;
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
            
            if (_transactionInfoService.AddTransactionInfo(trans)==1)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }

        [HttpPost]
        public IActionResult GetRecentTransactions()
        {
            var list = _transactionInfoService.GetRecentTransactions();
            return Ok(list);
        }

        public IActionResult Wind()
        {
            return View();
        }
    }
}