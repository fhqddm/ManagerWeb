using Solo.Model;
using Solo.Model.QueryModel;
using Solo.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solo.BLL
{
    public interface IStockInfoService
    {
        List<StockView> GetStockInfos(StockQuery query);

    }
}
