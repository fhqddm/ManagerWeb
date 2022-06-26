using Microsoft.EntityFrameworkCore;
using Solo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solo.BLL
{
    public class MyStockService : IMyStockService
    {
        public int AddMyStock(MyStock myStock)
        {
            using (MyContext mycontext = new MyContext())
            {
                if (mycontext.MyStocks.Where(x => x.StockNo == myStock.StockNo && x.UserId == myStock.UserId).Count() == 0)
                {
                    mycontext.MyStocks.Add(myStock);
                    return mycontext.SaveChanges();
                }
                else
                {
                    return 0;
                }
                
            }
        }

        public int AddMyStocks(string stockNos,string stockNames,int userId)
        {
            using (MyContext mycontext = new MyContext())
            {
                var mystocks = mycontext.MyStocks.Where(x => x.UserId == userId).ToList();
                string[] myStockNos = stockNos.Split(',');
                string[] myStockNames = stockNames.Split(',');
                for (int i = 0; i < myStockNos.Count(); i++)
                {
                    if (mystocks.Where(x=>x.StockNo == myStockNos[i]).ToList().Count==0)
                    {
                        MyStock mystock = new MyStock
                        {
                            StockNo = myStockNos[i],
                            StockName = myStockNames[i],
                            UserId = userId
                        };

                        mycontext.MyStocks.Add(mystock);
                    }
                    
                }
                //foreach (var myno in myStockNos)
                //{

                //    MyStock mystock = new MyStock
                //    {
                //        StockNo = myno,
                //        StockName = stockInfos.Where(x => x.StockNo == myno).SingleOrDefault().StockName,
                //        UserId = userId
                //    };
                //    mycontext.MyStocks.Add(mystock);

                //}
                return mycontext.SaveChanges();
            }
            

        }

        public List<MyStock> GetMyStocks(int UserId)
        {
            using (MyContext mycontext = new MyContext())
            {             
                return mycontext.MyStocks.Where(x=>x.UserId == UserId).ToList();
            }
        }

        public int RemoveMyStock(MyStock myStock)
        {
            using (MyContext myContext = new MyContext())
            {
                return myContext.Database.ExecuteSqlRaw($"delete from MyStocks where StockNo='{myStock.StockNo}' and UserId={myStock.UserId}");
            }
        }
    }
}
