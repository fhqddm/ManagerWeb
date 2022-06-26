using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Solo.BLL;
using Solo.Common;
using Solo.Model;
using Solo.Model.QueryModel;
using Solo.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ManagerApi.Controllers
{
    public class StockController : Controller
    {
        public readonly IStockInfoService _stockInfoService;
        public readonly IFundInfoService _fundInfoService;
        public readonly IMyStockService _myStockService;


        public StockController(IStockInfoService stockInfoService, IFundInfoService fundInfoService, IMyStockService myStockService)
        {
            _stockInfoService = stockInfoService;
            _fundInfoService = fundInfoService;
            _myStockService = myStockService;
        }

        [HttpPost]
        public IActionResult GetStockInfos(StockQuery query)
        {
            int userId = 0;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
            string querystr = "";
            if (!string.IsNullOrEmpty(query.StockNo))
            {
                querystr += query.StockNo + " ";
            }
            if (!string.IsNullOrEmpty(query.StockName))
            {
                querystr += query.StockName + " ";
            }
            if (!string.IsNullOrEmpty(query.StockNos))
            {
                querystr += query.StockNos + " ";
            }
            if (!string.IsNullOrEmpty(query.IndustryName))
            {
                querystr += query.IndustryName + " ";
            }
            Console.WriteLine(userId.ToString() + ":Stock  " +querystr + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            var stockViews = _stockInfoService.GetStockInfos(query);
            if (query.UserId > 0)
            {
                var stratgies = _fundInfoService.GetStrategyByQuery(new StrategyQuery
                {
                    UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    isnew = false,
                    status = 1,
                    bondstock = 2
                });
                foreach (var strategy in stratgies)
                {
                    var stockNos = strategy.StockNos.Split(',').ToList();
                    var stockPositions = strategy.StockPositions.Split(',').ToList();
                    foreach (var stockNo in stockNos)
                    {
                        if (!string.IsNullOrEmpty(stockNo))
                        {
                            var stockView = stockViews.Where(x => x.StockNo == stockNo).FirstOrDefault();
                            var index = stockNos.IndexOf(stockNo);
                            if (stockView != null)
                            {
                                stockView.CurrentMoney = (int)(stockView.CurrentMoney + (strategy.CurrentHold * Convert.ToDouble(stockPositions[index]) * 0.01));
                                stockView.MyFundNames = stockView.MyFundNames + strategy.FundName + ",";
                            }
                            
                        }
                        
                    }
                }
                if (query.HoldWait == 1)
                {
                    stockViews = stockViews.Where(x => x.CurrentMoney > 0).ToList();
                }
            }
            return Ok(stockViews);
        }

        [HttpPost]
        public IActionResult GetFundTips(string fundstr)
        {
            var res = _fundInfoService.GetFundNameTips(fundstr);
            return Ok(res);
        }


        [HttpPost]
        public IActionResult GetPoolStockViews(StockQuery query)
        {
            int userId = 0;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
            Console.WriteLine(userId.ToString() + ":GetPoolStockViews  " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            List<PoolStockView> poolStockViews = new List<PoolStockView>();
            var list = _stockInfoService.GetStockInfos(query);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<StockView, PoolStockView>());
            var mapper = config.CreateMapper();
            foreach (var item in list)
            {
                poolStockViews.Add(mapper.Map<PoolStockView>(item));
            }
            return Ok(poolStockViews);
        }

        [HttpPost]
        public IActionResult AddToMyStock(MyStock myStock)
        {
            int userId = 0;
            if (int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId))
            {
                Console.WriteLine(userId.ToString() + ":添加自选股票  " + myStock.StockName + "  " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                if (_myStockService.AddMyStock(myStock) == 1)
                {
                    return Ok(1);
                }
            }

            return Ok("添加失败");
        }

        [HttpPost]
        public IActionResult AddToMyStocks(string stockNos,string stockNames)
        {
            int userId = 0;
            if (int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId))
            {

                if (_myStockService.AddMyStocks(stockNos,stockNames,userId) > 0)
                {
                    return Ok(1);
                }
            }
            
            return Ok("添加失败");
        }

        [HttpPost]
        public IActionResult RemoveMyStock(MyStock myStock)
        {
            int userId = 0;
            if (int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId))
            {
                Console.WriteLine(userId.ToString() + ":删除自选股票  " + myStock.StockNo + "  " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                if (_myStockService.RemoveMyStock(myStock) == 1)
                {
                    return Ok(1);
                }

            }
            return Ok("移除失败");


        }

        [HttpPost]
        public IActionResult GetMyStocks()
        {
            int userId = 0;
            if (int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId))
            {
                return Ok(_myStockService.GetMyStocks(userId));

            }
            return Ok("移除失败");
            
        }

        [HttpPost]
        public IActionResult GeStockTipByQueryStr(string queryStr)
        {
            //int userId = 0;
            //int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
            //Console.WriteLine(userId.ToString() + ":GeStockTipByQueryStr  " + queryStr + "  " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            List<PoolStockView> poolStockViews = new List<PoolStockView>();
            var list = _stockInfoService.GetStockInfos(new StockQuery());
            if (MyUtils.HasChinese(queryStr))
            {
                list = list.Where(x => x.StockName.Contains(queryStr)).ToList();

            }
            else
            {
                list = list.Where(x => x.StockNo.Split('.')[1].StartsWith(queryStr)).ToList();
                
            }
            var config = new MapperConfiguration(cfg => cfg.CreateMap<StockView, PoolStockView>());
            var mapper = config.CreateMapper();
            foreach (var item in list)
            {
                poolStockViews.Add(mapper.Map<PoolStockView>(item));
            }
            return Ok(poolStockViews);           
        }

        [HttpPost]
        public IActionResult AddPoolStockView(string queryStr)
        {
            int userId = 0;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
            Console.WriteLine(userId.ToString() + ":AddPoolStockView  " + queryStr + "  " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            StockView stockView = _stockInfoService.GetStockInfos(new StockQuery { StockNo = queryStr.Split(':')[0]}).SingleOrDefault();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<StockView, PoolStockView>());
            
            var mapper = config.CreateMapper();
            var poolStockView = mapper.Map<PoolStockView>(stockView);
            return Ok(poolStockView);
        }

        [HttpPost]
        public IActionResult BatchPoolStockViews(string stocknos)
        {
            try
            {
                var userid = 0;
                int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userid);
                Console.WriteLine(userid.ToString() + ":  BatchPoolStockViews " + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                List<PoolStockView> poolStockViews = new List<PoolStockView>();
                var stocks = new List<StockView>();

                if (stocknos == "OrgTop50")
                {
                    stocks = _stockInfoService.GetStockInfos(new StockQuery()).Where(x => x.FundTop10HoldRate > 4.5).ToList();
                }
                else
                {
                    stocks = _stockInfoService.GetStockInfos(new StockQuery()).Where(x => stocknos.Contains(x.StockNo.Split('.')[1])).ToList();
                }

                var config = new MapperConfiguration(cfg => cfg.CreateMap<StockView, PoolStockView>());
                var mapper = config.CreateMapper();
                foreach (var item in stocks)
                {
                    poolStockViews.Add(mapper.Map<PoolStockView>(item));
                }
                return Ok(poolStockViews);
            }
            catch (Exception ex)
            {
                Console.WriteLine("BatchPoolStockViews Error: "+ stocknos);
                return null;
            }
            
        }
    }
}
