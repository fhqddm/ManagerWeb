using Microsoft.EntityFrameworkCore;
using Solo.Common;
using Solo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solo.BLL
{
    public class TransactionInfoService : ITransactionInfoService
    {
        public int AddTransactionInfo(TransactionInfo transactionInfo)
        {
            using (MyContext myContext = new MyContext())
            {
                //myContext.TransactionInfos.Add(transactionInfo);
                //return myContext.SaveChanges();
                string sql = "";
                if (transactionInfo.TransactionValue>0)
                {
                    sql = $"call BuyIn('{transactionInfo.FundNo}','{transactionInfo.ConfirmTime.ToString("yyyy-MM-dd")}',{transactionInfo.TransactionValue})";
                }
                else
                {
                    sql = $"call SellOut('{transactionInfo.FundNo}','{transactionInfo.ConfirmTime.ToString("yyyy-MM-dd")}',{transactionInfo.TransactionValue})";
                }
                myContext.Database.ExecuteSqlCommandAsync(sql);
                return 1;

            }
        }

        public List<TransactionInfo> GetRecentTransactions()
        {
            using (MyContext myContext = new MyContext())
            {
                var date7 = DateHelper.getFormatDateTime(DateTime.Now.AddDays(-6));
                var list = myContext.TransactionInfos.Where(x => x.ConfirmTime >= date7).ToList();
                List<string> flist = new List<string>();
                foreach (var item in list)
                {
                    if (!flist.Contains(item.FundNo))
                    {
                        flist.Add(item.FundNo);
                    }
                }
                List<TransactionInfo> res = new List<TransactionInfo>();
                foreach (var fundno in flist)
                {
                    TransactionInfo transaction = new TransactionInfo();
                    transaction.FundNo = fundno;
                    transaction.TransactionValue = list.Where(x => x.FundNo == fundno && x.FundNo != "444444").Sum(x => x.TransactionValue);
                    res.Add(transaction);
                }
                return res;

            }
        }

        public List<TransactionInfo> GetTransactionInfosByFundNo(string FundNo)
        {
            using (MyContext myContext = new MyContext())
            {

                return myContext.TransactionInfos.Where(x => x.FundNo == FundNo).ToList();

            }
        }
    }
}
