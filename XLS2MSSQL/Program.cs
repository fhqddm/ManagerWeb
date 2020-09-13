using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using ExcelDataReader;
using Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;
using Solo.BLL;
using Solo.Common;
using Solo.Model;
using DataTable = System.Data.DataTable;

namespace XLS2MSSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            
            MyInsertNetValue2DB();

            //CreatDB2XMLS();

            //InsertUnitValueToDB();

            //Ms2My();


        }

        public static void CreatDB2XMLS()
        {

            string dirPath = @"F:\FoundData\line\";

            List<string> fdlist = new List<string>();
            string sql = "select FundNo from FundInfos where FundName != 'Unknow'";
            SqlParameter[] pars = null;
            DataTable da = SqlHelper.ExecuteTable(sql, pars);
            for (int i = 0; i < da.Rows.Count; i++)
            {
                fdlist.Add(da.Rows[i]["FundNo"].ToString());
            }

            foreach (var fundNo in fdlist)
            {
                Dictionary<DateTime, double> dic = new Dictionary<DateTime, double>();
                string sql1 = "select DataTime,NetValue from FundDailyIDetails where FundNo ='"+fundNo+"' order by DataTime";
                SqlParameter[] pars1 = null;
                System.Data.DataTable da1 = SqlHelper.ExecuteTable(sql, pars);


                Application statusExcel = new Microsoft.Office.Interop.Excel.Application();
                Workbook statusWorkbook = statusExcel.Application.Workbooks.Add(true);
                Worksheet wsStatusSheet = (Worksheet)statusWorkbook.Worksheets.Add(statusWorkbook.Sheets[1], Type.Missing, Type.Missing, Type.Missing);
                ((Worksheet)statusWorkbook.Sheets["Sheet1"]).Delete();
                wsStatusSheet.Name = "Status11";

                for (int i = 0; i < da1.Rows.Count; i++)
                {

                    wsStatusSheet.Cells[i, 0] = da1.Rows[i][0].ToString();
                    wsStatusSheet.Cells[i, 1] = da1.Rows[i][1].ToString();
                }
                string filePath = dirPath+fundNo+".xls";
                statusWorkbook.SaveAs(filePath);
                Console.WriteLine(filePath);
            }

            Console.WriteLine("done");

            //List<string> fdlist = new List<string>();
            //string sql = "select DataTime,NetValue from FundDailyIDetails where FundNo = '" + FundNo + "' order by DataTime";
            //SqlParameter[] pars = null;
            //DataTable da = SqlHelper.ExecuteTable(sql, pars);
        }

        private static void MsInsertNetValue2DB()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding encoding1 = Encoding.GetEncoding(1252);
            Encoding encoding2 = Encoding.GetEncoding("GB2312");

            FundDailyDetailService fundDailyDetailService = new FundDailyDetailService();
            FundDailyIDetail fundDailyIDetail = new FundDailyIDetail();

            //DateTime lastRecordTime = new DateTime(2020, 5, 22, 0, 0, 0);
            //Console.ReadKey();

            List<string> fdlist = new List<string>();
            string sql = "select FundNo from FundInfos where FundName != 'Unknow'";
            //sql = "select fi.FundNo from FundInfos fi right join MyFunds mf on fi.FundNo=mf.FundNo where FundName != 'Unknow' order by Investment desc";
            //MySqlParameter[] pars = null;
            //DataTable da = Solo.Common.MySqlHelper.ExecuteTable(sql, pars);
            SqlParameter[] pars = null;
            DataTable da = SqlHelper.ExecuteTable(sql, pars);
            string sqltime = "select DataTime from FundDailyIDetails where FundNo='444444' order by DataTime desc";
            DataTable dbtime = SqlHelper.ExecuteTable(sqltime, pars);
            List<DateTime> dtList = new List<DateTime>();
            int dtrows = dbtime.Rows.Count;
            for (int i = 0; i < dtrows; i++)
            {
                dtList.Add(Convert.ToDateTime(dbtime.Rows[i][0]));
            }
            

            for (int i = 0; i < da.Rows.Count; i++)
            {
                fdlist.Add(da.Rows[i][0].ToString());
            }

            //测试
            fdlist = new List<string>() { "161726" };

            string[] files = Directory.GetFiles(@"F:\FoundData\line1");

            foreach (var FundNo in fdlist)
            {
                if (files.Where(x => x.Contains(FundNo)).Count() > 0)
                {
                    string FileName = files.Single(x => x.Contains(FundNo));

                    using (var stream = File.Open(FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (var readerData = ExcelReaderFactory.CreateReader(stream))
                        {
                            var result = readerData.AsDataSet();
                            //4：得到ExcelFile文件的表Sheet
                            var sheet = result.Tables["My Worksheet"];
                            var cols = sheet.Columns.Count;//ExcelFile的表Sheet的列数
                            int rows = sheet.Rows.Count;//Sheet中的行数

                            int j = 0;

                            //for (int i = 0; i < rows; i++)
                            for(int i=rows-1; i>=0; i--)
                            {                               
                                fundDailyIDetail = new FundDailyIDetail();
                                fundDailyIDetail.FundNo = FundNo;
                                fundDailyIDetail.DataTime = dtList[j];
                                fundDailyIDetail.NetValue = (float)(Convert.ToDouble(sheet.Rows[i][1]));
                                fundDailyIDetail.UnitValue = (float)(Convert.ToDouble(sheet.Rows[i][2]));
                                fundDailyIDetail.EquityReturn = (float)(Convert.ToDouble(sheet.Rows[i][3]));
                                fundDailyIDetail.UnitMoney = 0;
                                if (!string.IsNullOrEmpty(sheet.Rows[i][4].ToString()))
                                {
                                    string temp= sheet.Rows[i][4].ToString();
                                    if (sheet.Rows[i][4].ToString().Contains("分红"))
                                    {
                                        temp = sheet.Rows[i][4].ToString().Replace("分红：每份派现金", "").Replace("元", "");
                                        //UnitMoney = 0.9935;
                                    }
                                    else if (sheet.Rows[i][4].ToString().Contains("拆分"))
                                    {
                                        temp = sheet.Rows[i][4].ToString().Replace("拆分：每份基金份额折算", "").Replace("份", "");
                                        WriteLogs("depart", "aa", FundNo + ":" + i.ToString()+"拆分");
                                    }
                                    
                                    //UnitMoney = 0.9935;
                                    fundDailyIDetail.UnitMoney = (float)(Convert.ToDouble(temp));
                                }

                                fundDailyDetailService.AddDailyDetail(fundDailyIDetail);
                                Console.WriteLine(fundDailyIDetail.DataTime.ToString("yyyy-MM-dd"));

                                if (j < dtrows-1)
                                {
                                    j++;
                                }
                                else
                                {
                                    break;
                                }

                                //string sqlu = "update FundDailyIDetails set TaskName=@TaskName,DeadLine=@DeadLine where FundNo=@FundNo and DataTime=@DataTime";
                                //SqlParameter[] parsu = {
                                //     new SqlParameter("@FundNo",SqlDbType.NVarChar),
                                //     new SqlParameter("@DataTime",SqlDbType.DateTime),
                                //     new SqlParameter("@Id",SqlDbType.Int,4)
                                // };
                                //pars[0].Value = newInfo.taskName;
                                //pars[1].Value = newInfo.DeadLine;
                                //pars[2].Value = newInfo.Id;
                                //SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pars);
                            }
                            j = 0;
                            Console.WriteLine("Finish:" + FundNo);

                        }
                    }
                }
                else
                {
                    Console.WriteLine(FundNo + " does not exist");
                }
            }

        }

        private static void MyInsertNetValue2DB()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding encoding1 = Encoding.GetEncoding(1252);
            Encoding encoding2 = Encoding.GetEncoding("GB2312");

            FundDailyDetailService fundDailyDetailService = new FundDailyDetailService();
            FundDailyIDetail fundDailyIDetail = new FundDailyIDetail();

            //DateTime lastRecordTime = new DateTime(2020, 5, 22, 0, 0, 0);
            //Console.ReadKey();

            List<string> fdlist = new List<string>();
            //string sql = "select FundNo from FundInfos where FundName != 'Unknow'";
            string sql = "select fi.FundNo from FundInfos fi right join MyFunds mf on fi.FundNo=mf.FundNo where FundName != 'Unknow' order by Investment desc";
            MySqlParameter[] pars = null;
            DataTable da = Solo.Common.MySqlHelper.ExecuteTable(sql, pars);
            //SqlParameter[] pars = null;
            //DataTable da = SqlHelper.ExecuteTable(sql, pars);
            string sqltime = "select DataTime from FundDailyIDetails where FundNo='444444' order by DataTime desc";
            DataTable dbtime = Solo.Common.MySqlHelper.ExecuteTable(sqltime, pars);
            List<DateTime> dtList = new List<DateTime>();
            int dtrows = dbtime.Rows.Count;
            for (int i = 0; i < dtrows; i++)
            {
                dtList.Add(Convert.ToDateTime(dbtime.Rows[i][0]));
            }


            for (int i = 0; i < da.Rows.Count; i++)
            {
                fdlist.Add(da.Rows[i][0].ToString());
            }

            //测试
            fdlist = new List<string>() { "005827" };

            string[] files = Directory.GetFiles(@"F:\FoundData\line1");

            foreach (var FundNo in fdlist)
            {
                if (files.Where(x => x.Contains(FundNo)).Count() > 0)
                {
                    string FileName = files.Single(x => x.Contains(FundNo));

                    using (var stream = File.Open(FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (var readerData = ExcelReaderFactory.CreateReader(stream))
                        {
                            var result = readerData.AsDataSet();
                            //4：得到ExcelFile文件的表Sheet
                            var sheet = result.Tables["My Worksheet"];
                            var cols = sheet.Columns.Count;//ExcelFile的表Sheet的列数
                            int rows = sheet.Rows.Count;//Sheet中的行数

                            int j = 0;

                            //for (int i = 0; i < rows; i++)
                            for (int i = rows - 1; i >= 0; i--)
                            {
                                fundDailyIDetail = new FundDailyIDetail();
                                fundDailyIDetail.FundNo = FundNo;
                                fundDailyIDetail.DataTime = dtList[j];
                                fundDailyIDetail.NetValue = (float)(Convert.ToDouble(sheet.Rows[i][1]));
                                fundDailyIDetail.UnitValue = (float)(Convert.ToDouble(sheet.Rows[i][2]));
                                fundDailyIDetail.EquityReturn = (float)(Convert.ToDouble(sheet.Rows[i][3]));
                                fundDailyIDetail.UnitMoney = 0;
                                if (!string.IsNullOrEmpty(sheet.Rows[i][4].ToString()))
                                {
                                    string temp = sheet.Rows[i][4].ToString();
                                    if (sheet.Rows[i][4].ToString().Contains("分红"))
                                    {
                                        temp = sheet.Rows[i][4].ToString().Replace("分红：每份派现金", "").Replace("元", "");
                                        //UnitMoney = 0.9935;
                                    }
                                    else if (sheet.Rows[i][4].ToString().Contains("拆分"))
                                    {
                                        temp = sheet.Rows[i][4].ToString().Replace("拆分：每份基金份额折算", "").Replace("份", "");
                                        WriteLogs("depart", "aa", FundNo + ":" + i.ToString() + "拆分");
                                    }

                                    //UnitMoney = 0.9935;
                                    fundDailyIDetail.UnitMoney = (float)(Convert.ToDouble(temp));
                                }

                                fundDailyDetailService.AddDailyDetail(fundDailyIDetail);
                                Console.WriteLine(fundDailyIDetail.DataTime.ToString("yyyy-MM-dd"));

                                if (j < dtrows - 1)
                                {
                                    j++;
                                }
                                else
                                {
                                    break;
                                }

                                //string sqlu = "update FundDailyIDetails set TaskName=@TaskName,DeadLine=@DeadLine where FundNo=@FundNo and DataTime=@DataTime";
                                //SqlParameter[] parsu = {
                                //     new SqlParameter("@FundNo",SqlDbType.NVarChar),
                                //     new SqlParameter("@DataTime",SqlDbType.DateTime),
                                //     new SqlParameter("@Id",SqlDbType.Int,4)
                                // };
                                //pars[0].Value = newInfo.taskName;
                                //pars[1].Value = newInfo.DeadLine;
                                //pars[2].Value = newInfo.Id;
                                //SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pars);
                            }
                            j = 0;
                            Console.WriteLine("Finish:" + FundNo);

                        }
                    }
                }
                else
                {
                    Console.WriteLine(FundNo + " does not exist");
                }
            }

        }

        private static void InsertUnitValueToDB()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding encoding1 = Encoding.GetEncoding(1252);
            Encoding encoding2 = Encoding.GetEncoding("GB2312");

            HolidayService holidayService = new HolidayService();
            FundDailyDetailService fundDailyDetailService = new FundDailyDetailService();
            FundDailyIDetail fundDailyIDetail = new FundDailyIDetail();

            DateTime lastRecordTime = new DateTime(2020, 8, 13, 0, 0, 0);
            //Console.ReadKey();

            List<string> fdlist = new List<string>();
            string sql = "select FundNo from FundInfos where FundName != 'Unknow'";
            sql = "select fi.FundNo from FundInfos fi right join MyFunds mf on fi.FundNo=mf.FundNo where FundName != 'Unknow' order by Investment desc";
            SqlParameter[] pars = null;
            DataTable da = SqlHelper.ExecuteTable(sql, pars);
            for (int i = 0; i < da.Rows.Count; i++)
            {
                fdlist.Add(da.Rows[i][0].ToString());
            }

            //测试
            fdlist = new List<string>() { "003984" };

            string[] files = Directory.GetFiles(@"F:\FoundData\line1");

            foreach (var FundNo in fdlist)
            {
                if (files.Where(x => x.Contains(FundNo)).Count() > 0)
                {
                    string FileName = files.Single(x => x.Contains(FundNo));

                    using (var stream = File.Open(FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (var readerData = ExcelReaderFactory.CreateReader(stream))
                        {
                            
                            var result = readerData.AsDataSet();
                            //4：得到ExcelFile文件的表Sheet
                            var sheet = result.Tables["My Worksheet"];
                            var cols = sheet.Columns.Count;//ExcelFile的表Sheet的列数
                            int rows = sheet.Rows.Count;//Sheet中的行数
                            string newpath = FileName.Replace("line1", "line");
                            if (GetRowsCount(newpath) == rows)
                            {
                                if (fundDailyDetailService.GetFinishuUpdate(FundNo) != rows)
                                {
                                    var dtList = holidayService.GetWorkDayList(lastRecordTime, rows);

                                    for (int i = 0; i < rows; i++)
                                    {

                                        //fundDailyIDetail = new FundDailyIDetail();
                                        //fundDailyIDetail.FundNo = FundNo;
                                        //fundDailyIDetail.NetValue = (float)sheet.Rows[i][1];
                                        //fundDailyIDetail.DataTime = dtList[i];
                                        //fundDailyDetailService.AddDailyDetail(fundDailyIDetail);
                                        double NetValue = Convert.ToDouble(sheet.Rows[i][1]);
                                        double UnitValue = Convert.ToDouble(sheet.Rows[i][2]);
                                        double EquityReturn = Convert.ToDouble(sheet.Rows[i][3]);
                                        double UnitMoney = 0;
                                        if (!string.IsNullOrEmpty(sheet.Rows[i][4].ToString()))
                                        {
                                            string temp = sheet.Rows[i][4].ToString().Replace("拆分：每份基金份额折算", "").Replace("份", "");
                                            //UnitMoney = 0.9935;
                                            UnitMoney = Convert.ToDouble(temp);
                                        }

                                        string sqlu = "update FundDailyIDetails set UnitValue=@UnitValue,UnitMoney=@UnitMoney,EquityReturn=@EquityReturn where FundNo=@FundNo and DataTime=@DataTime and NetValue=@NetValue";
                                        SqlParameter[] parsu = {
                                     new SqlParameter("@FundNo",SqlDbType.NVarChar),
                                     new SqlParameter("@DataTime",SqlDbType.DateTime),
                                     new SqlParameter("@UnitValue",SqlDbType.Real),
                                     new SqlParameter("@UnitMoney",SqlDbType.Real),
                                     new SqlParameter("@EquityReturn",SqlDbType.Real),
                                     new SqlParameter("@NetValue",SqlDbType.Real)
                                    };
                                        parsu[0].Value = FundNo;
                                        parsu[1].Value = dtList[i];
                                        parsu[2].Value = UnitValue;
                                        parsu[3].Value = UnitMoney;
                                        parsu[4].Value = EquityReturn;
                                        parsu[5].Value = NetValue;
                                        int lkk = SqlHelper.ExecuteNonQuery(sqlu, CommandType.Text, parsu);
                                        if (lkk != 1)
                                        {
                                            Console.WriteLine("update error " + FundNo + "     " + dtList[i].ToString("yyyy-MM-dd"));
                                            Console.ReadKey();
                                        }
                                    }
                                    Console.WriteLine("done:" + FundNo);
                                }
                                else
                                {
                                    Console.WriteLine("已经完成:" + FundNo);
                                }                               
                            }
                            else
                            {
                                Console.WriteLine("-------------------------行数不匹配"+FundNo);
                            }

                        }
                    }
                }
                else
                {
                    Console.WriteLine(FundNo + " does not exist");
                }
            }


        }

        private static int GetRowsCount(string filePath)
        {
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var readerData = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = readerData.AsDataSet();
                    var sheet = result.Tables["My Worksheet"];
                    int rows = sheet.Rows.Count;//Sheet中的行数
                    return rows;
                }
            }
        }

        public static void Ms2My()
        {
            List<string> fdlist = new List<string>();
            string sql = "select * from FundInfos where FundName != 'Unknow'";
            SqlParameter[] pars = null;
            DataTable da = SqlHelper.ExecuteTable(sql, pars);

            using (MyContext myContext = new MyContext())
            {
                for (int i=0;i<da.Rows.Count; i++)
                {
                    float outSy = 0;
                    FundInfo fundInfo = new FundInfo();
                    fundInfo.FundNo = da.Rows[i]["FundNo"].ToString();
                    fundInfo.FundName =  da.Rows[i]["FundName"].ToString();
                    if (float.TryParse(da.Rows[i]["BuyRate"].ToString(), out outSy))
                    {
                        fundInfo.BuyRate = outSy;
                    }
                    if (float.TryParse(da.Rows[i]["TotalScale"].ToString(), out outSy))
                    {
                        fundInfo.TotalScale = outSy;
                    }
                    if (float.TryParse(da.Rows[i]["OrgHoldRate"].ToString(), out outSy))
                    {
                        fundInfo.OrgHoldRate = outSy;
                    }
                    if (float.TryParse(da.Rows[i]["NetValue"].ToString(), out outSy))
                    {
                        fundInfo.NetValue = outSy;
                    }
                    if (float.TryParse(da.Rows[i]["StockRate"].ToString(), out outSy))
                    {
                        fundInfo.StockRate = outSy;
                    }
                    if (float.TryParse(da.Rows[i]["ReturnRate"].ToString(), out outSy))
                    {
                        fundInfo.ReturnRate = outSy;
                    }
                    if (float.TryParse(da.Rows[i]["R1day"].ToString(), out outSy))
                    {
                        fundInfo.R1day = outSy;
                    }
                    if (float.TryParse(da.Rows[i]["R1week"].ToString(), out outSy))
                    {
                        fundInfo.R1week = outSy;
                    }
                    if (float.TryParse(da.Rows[i]["R1month"].ToString(), out outSy))
                    {
                        fundInfo.R1month = outSy;
                    }
                    if (float.TryParse(da.Rows[i]["R3month"].ToString(), out outSy))
                    {
                        fundInfo.R3month = outSy;
                    }
                    if (float.TryParse(da.Rows[i]["R6month"].ToString(), out outSy))
                    {
                        fundInfo.R6month = outSy;
                    }
                    if (float.TryParse(da.Rows[i]["R1year"].ToString(), out outSy))
                    {
                        fundInfo.R1year = outSy;
                    }
                    if (float.TryParse(da.Rows[i]["R2year"].ToString(), out outSy))
                    {
                        fundInfo.R2year = outSy;
                    }
                    if (float.TryParse(da.Rows[i]["R3year"].ToString(), out outSy))
                    {
                        fundInfo.R3year = outSy;
                    }
                    if (float.TryParse(da.Rows[i]["R5year"].ToString(), out outSy))
                    {
                        fundInfo.R5year = outSy;
                    }
                    if (float.TryParse(da.Rows[i]["ManagerFee"].ToString(), out outSy))
                    {
                        fundInfo.ManagerFee = outSy;
                    }
                    if (float.TryParse(da.Rows[i]["CustodyFee"].ToString(), out outSy))
                    {
                        fundInfo.CustodyFee = outSy;
                    }
                    if (float.TryParse(da.Rows[i]["SaleFee"].ToString(), out outSy))
                    {
                        fundInfo.SaleFee = outSy;
                    }
                    if (float.TryParse(da.Rows[i]["Over7dFee"].ToString(), out outSy))
                    {
                        fundInfo.Over7dFee = outSy;
                    }
                    if (float.TryParse(da.Rows[i]["Over30dFee"].ToString(), out outSy))
                    {
                        fundInfo.Over30dFee = outSy;
                    }
                    if (float.TryParse(da.Rows[i]["Over1yFee"].ToString(), out outSy))
                    {
                        fundInfo.Over1yFee = outSy;
                    }
                    if (float.TryParse(da.Rows[i]["Over2yFee"].ToString(), out outSy))
                    {
                        fundInfo.Over2yFee = outSy;
                    }
                    fundInfo.DutyDate = Convert.ToDateTime(da.Rows[i]["DutyDate"]);
                    fundInfo.FundUpdateTime = Convert.ToDateTime(da.Rows[i]["FundUpdateTime"]);
                    myContext.FundInfos.Add(fundInfo);
                }

                Console.WriteLine(myContext.SaveChanges());
            }
        }
        public static void WriteLogs(string fileName, string type, string content)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            if (!string.IsNullOrEmpty(path))
            {
                path = AppDomain.CurrentDomain.BaseDirectory + fileName;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = path + "\\" + DateTime.Now.ToString("yyyyMMdd");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = path + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                if (!File.Exists(path))
                {
                    FileStream fs = File.Create(path);
                    fs.Close();
                }
                if (File.Exists(path))
                {
                    StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default);
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff  ") + type + "-->" + content);
                    //  sw.WriteLine("----------------------------------------");
                    sw.Close();
                }
            }
        }
    }


}
