using Microsoft.EntityFrameworkCore;
using Solo.Model.QueryModel;
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
                if (mycontext.MyFunds.Where(x=>x.FundNo==myFund.FundNo && x.UserId == myFund.UserId).Count()==0)
                {
                    mycontext.MyFunds.Add(myFund);
                    return mycontext.SaveChanges();
                }
                else
                {
                    return 0;
                }
            }
        }

        public List<MyFund> GetAllMyFunds()
        {
            using (MyContext mycontext = new MyContext())
            {               
                return mycontext.MyFunds.ToList();
            }
        }

        public MyFund GetMyFund(MyFund myFund)
        {
            using (MyContext mycontext = new MyContext())
            {
                try
                {
                    return mycontext.MyFunds.Where(x => x.FundNo == myFund.FundNo && x.UserId == myFund.UserId).SingleOrDefault();
                }
                catch
                {

                    return null;
                }
                
            }
        }

        public string GetMyFundNos(int status)
        {
            var selList = GetMyFunds(new MyFund { Status =status},OWN.Waits);
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

        public List<MyFund> GetMyFunds(MyFund myFund,OWN own)
        {
            using (MyContext myContext = new MyContext())
            {
                if (own == OWN.Empty || own == OWN.HoldWaits)
                {
                    return myContext.MyFunds.Where(x => x.UserId == myFund.UserId).ToList();
                }
                else if(own == OWN.Holds)
                {
                    return myContext.MyFunds.Where(x => x.UserId == myFund.UserId && x.Status == 1).ToList();
                }
                else if (own == OWN.Waits)
                {
                    return myContext.MyFunds.Where(x => x.UserId == myFund.UserId && x.Status == 2).ToList();
                }
                return null;            
            }
            
        }

        public int RemoveMyFund(string fundNo,int userId)
        {
            using (MyContext myContext = new MyContext())
            {
                return myContext.Database.ExecuteSqlRaw($"delete from MyFunds where FundNo={fundNo} and UserId={userId}");
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
