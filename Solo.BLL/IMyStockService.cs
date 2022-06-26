using Solo.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solo.BLL
{
    public interface IMyStockService
    {
        int AddMyStock(MyStock myStock);
        int AddMyStocks(string stockNos,string StockNames, int userId);
        int RemoveMyStock(MyStock myStock);
        List<MyStock> GetMyStocks(int UserId);
    }
}
