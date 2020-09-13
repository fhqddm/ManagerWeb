using Solo.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solo.BLL
{
    public interface ITransactionInfoService
    {
        List<TransactionInfo> GetTransactionInfosByFundNo(string FundNo);

        int AddTransactionInfo(TransactionInfo transactionInfo);

        List<TransactionInfo> GetRecentTransactions();
    }
}
