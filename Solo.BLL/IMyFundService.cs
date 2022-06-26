using Solo.Model.QueryModel;
using Solo.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solo.BLL
{
    public interface IMyFundService
    {
        List<MyFund> GetMyFunds(MyFund myFund,OWN own);
        MyFund GetMyFund(MyFund myFund);

        string GetMyFundNos(int status);

        List<MyFund> GetAllMyFunds();

        int AddIntoMyFunds(MyFund myFund);

        int RemoveMyFund(string fundNo,int userId);
        int UpdateMyFUnd(MyFund myFund);
    }
}
