using Microsoft.EntityFrameworkCore;
using Solo.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solo.BLL
{
    public class MyFundService : IMyFundService
    {
        public int AddIntoMyFunds(MyFund myFund)
        {
            using (MyContext mycontext = new MyContext()) 
            {                
                mycontext.MyFunds.Add(myFund);
                return mycontext.SaveChanges();
            }
        }

        public MyFund GetMyFund(MyFund myFund)
        {
            using (MyContext mycontext = new MyContext())
            {
                return mycontext.MyFunds.Where(x => x.FundNo==myFund.FundNo).SingleOrDefault();
            }
        }

        public string GetMyFundNos(int status)
        {
            var selList = GetMyFunds(status);
            string sels = "";
            for (int i = 0; i < selList.Count; i++)
            {
                sels += selList[i].FundNo;
                if (i < selList.Count - 1)
                {
                    sels += ",";
                }
            }
            return sels;
        }

        public List<MyFund> GetMyFunds(int status)
        {
            using (MyContext myContext = new MyContext())
            {
                if (status>2)
                {
                   return myContext.MyFunds.ToList();
                }
                else
                {
                    return myContext.MyFunds.Where(x => x.Status == status).ToList();
                }
            }
            
        }

        public int RemoveMyFund(string fundNo)
        {
            using (MyContext myContext = new MyContext())
            {
                return myContext.Database.ExecuteSqlRaw($"delete from MyFunds where FundNo={fundNo}");
            }
        }

        public int UpdateMyFUnd(MyFund myFund)
        {
            using (MyContext myContext = new MyContext())
            {
                myContext.MyFunds.Update(myFund);
                return myContext.SaveChanges();
            }
        }
    }
}
