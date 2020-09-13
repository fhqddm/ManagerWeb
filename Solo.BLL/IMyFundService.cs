using Solo.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solo.BLL
{
    public interface IMyFundService
    {
        List<MyFund> GetMyFunds(int status);
        MyFund GetMyFund(MyFund myFund);

        string GetMyFundNos(int status);

        int AddIntoMyFunds(MyFund myFund);

        int RemoveMyFund(string fundNo);
        int UpdateMyFUnd(MyFund myFund);
    }
}
