using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Solo.Common
{
    public class LinearRegression
    {
        public static LineReturn GetTrendEquation(string FundNo)
        {
            try
            {
                LineReturn lineReturn = new LineReturn();
                List<Point> equationList = new List<Point>();
                List<Point> _PList1 = new List<Point>();
                List<Point> _PList2 = new List<Point>();
                List<Point> _PList3 = new List<Point>();
                List<Point> _PList4 = new List<Point>();
                List<Point> _PList5 = new List<Point>();
                List<Point> _PList6 = new List<Point>();
                List<Point> _PList7 = new List<Point>();
                List<Point> _PList8 = new List<Point>();
                List<Point> _PList9 = new List<Point>();
                List<int> begins = new List<int> { 0, 0, 0 ,0,0,0,0,0,0};


                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding encoding1 = Encoding.GetEncoding(1252);
                Encoding encoding2 = Encoding.GetEncoding("GB2312");
                //Console.ReadKey();

                //string[] files = Directory.GetFiles(@"F:\FoundData\line");
                //Console.WriteLine(AppConfigurtaionServices.Configuration.GetSection("XlsFilePath").Value);
                string[] files = Directory.GetFiles(AppConfigurtaionServices.Configuration.GetSection("XlsFilePath").Value);

                int rows = 0;
                if (files.Where(x => x.Contains(FundNo)).Count() > 0)
                {
                    string FileName = files.Single(x => x.Contains(FundNo));

                    using (var stream = File.Open(FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (var readerData = ExcelReaderFactory.CreateReader(stream))
                        {
                            //3：通过reader得到数据（需要NuGet包ExcelDataReader.DataSet ）
                            var result = readerData.AsDataSet();                           
                            //4：得到ExcelFile文件的表Sheet
                            var sheet = result.Tables["My Worksheet"];
                            lineReturn.dataSet = sheet;
                            var cols = sheet.Columns.Count;//ExcelFile的表Sheet的列数
                            rows = sheet.Rows.Count;//Sheet中的行数

                            for (int i = 0; i < rows; i++)
                            {
                                //var db = Convert.ToDouble(sheet.Rows[i][0]);
                                //Console.WriteLine(db);
                                //5年
                                if (i > (rows - 1226))
                                {
                                    // _PList4.Add(new Point((double)sheet.Rows[i][0], (double)sheet.Rows[i][1]));
                                    _PList4.Add(new Point(i, (double)sheet.Rows[i][1]));
                                }
                                //3年
                                if (i > (rows - 736))
                                {
                                    //_PList3.Add(new Point((double)sheet.Rows[i][0], (double)sheet.Rows[i][1]));
                                    _PList3.Add(new Point(i, (double)sheet.Rows[i][1]));
                                } 
                                //2年
                                if (i > (rows - 491))
                                {
                                    //_PList2.Add(new Point((double)sheet.Rows[i][0], (double)sheet.Rows[i][1]));
                                    _PList2.Add(new Point(i, (double)sheet.Rows[i][1]));
                                }
                                //1年
                                if (i > (rows - 246))
                                {
                                    // _PList1.Add(new Point((double)sheet.Rows[i][0], (double)sheet.Rows[i][1]));
                                    _PList1.Add(new Point(i, (double)sheet.Rows[i][1]));
                                }
                                if (i > (rows - 123))
                                {
                                    // _PList1.Add(new Point((double)sheet.Rows[i][0], (double)sheet.Rows[i][1]));
                                    _PList9.Add(new Point(i, (double)sheet.Rows[i][1]));
                                }
                                //60日
                                if (i > (rows - 61))
                                {
                                    _PList5.Add(new Point(i, (double)sheet.Rows[i][1]));
                                }
                                //30日
                                if (i > (rows - 31))
                                {
                                    _PList6.Add(new Point(i, (double)sheet.Rows[i][1]));
                                }
                                //10日
                                if (i > (rows - 11))
                                {
                                    _PList7.Add(new Point(i, (double)sheet.Rows[i][1]));
                                }
                                //5日
                                if (i > (rows - 6))
                                {
                                    _PList8.Add(new Point(i, (double)sheet.Rows[i][1]));
                                }
                            }

                        }

                    }

                    Line line1 = LinearRegression.LinearRegressionSolve(_PList1);//1年
                    Line line2 = LinearRegression.LinearRegressionSolve(_PList2);//2年
                    Line line3 = LinearRegression.LinearRegressionSolve(_PList3);//3年
                    Line line4 = LinearRegression.LinearRegressionSolve(_PList4);//5年
                    Line line5 = LinearRegression.LinearRegressionSolve(_PList5);//60日
                    Line line6 = LinearRegression.LinearRegressionSolve(_PList6);//30日
                    Line line7 = LinearRegression.LinearRegressionSolve(_PList7);//10日
                    Line line8 = LinearRegression.LinearRegressionSolve(_PList8);//5日

                    equationList.Add(new Point(line1.K, line1.B));
                    equationList.Add(new Point(line2.K, line2.B));
                    equationList.Add(new Point(line3.K, line3.B));
                    equationList.Add(new Point(line4.K, line4.B));
                    equationList.Add(new Point(line5.K, line5.B));
                    equationList.Add(new Point(line6.K, line6.B));
                    equationList.Add(new Point(line7.K, line7.B));
                    equationList.Add(new Point(line8.K, line8.B));
                    begins[0] = rows- _PList1.Count;
                    begins[1] = rows- _PList2.Count;
                    begins[2] = rows- _PList3.Count;
                    begins[3] = rows - _PList4.Count;
                    begins[4] = rows;
                    begins[5] = rows - _PList5.Count;
                    begins[6] = rows - _PList6.Count;
                    begins[7] = rows - _PList7.Count;
                    begins[8] = rows - _PList8.Count;
                    lineReturn.topValue = _PList3.Max(x => x.Y);                  
                    lineReturn.points = equationList;
                    lineReturn.begins = begins;      
                    
                    return lineReturn;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }

        public static LineReturn GetTrendDbEquation(string FundNo)
        {
            try
            {
                LineReturn lineReturn = new LineReturn();
                List<Point> equationList = new List<Point>();
                List<Point> _PList1 = new List<Point>();
                List<Point> _PList2 = new List<Point>();
                List<Point> _PList3 = new List<Point>();
                List<Point> _PList4 = new List<Point>();
                List<int> begins = new List<int> { 0, 0, 0, 0 };

                Stopwatch watch = new Stopwatch();
                watch.Start();
                List<string> fdlist = new List<string>();
                string sql = "select DataTime,NetValue from FundDailyIDetails where FundNo = '"+ FundNo + "' order by DataTime";
                SqlParameter[] pars = null;
                DataTable da = SqlHelper.ExecuteTable(sql, pars);
                int rows = da.Rows.Count;
                lineReturn.dataSet = da;
                double time = watch.Elapsed.TotalSeconds;
                Console.WriteLine(time);
                watch.Stop();
                if (rows >0)
                {
                    for (int i = 0; i < rows; i++)
                    {
                        //var db = Convert.ToDouble(sheet.Rows[i][0]);
                        //Console.WriteLine(db);
                        DateTime dt = Convert.ToDateTime(da.Rows[i][0]);
                        if (dt >= DateTime.Now.AddYears(-5))
                        {
                            _PList4.Add(new Point((double)i, Math.Round(Convert.ToDouble(da.Rows[i]["NetValue"]), 4)));
                        }
                        if (dt >= DateTime.Now.AddYears(-3))
                        {

                            _PList3.Add(new Point((double)i, Math.Round(Convert.ToDouble(da.Rows[i]["NetValue"]), 4)));
                        }
                        if (dt >= DateTime.Now.AddYears(-2))
                        {
                            _PList2.Add(new Point((double)i, Math.Round(Convert.ToDouble(da.Rows[i]["NetValue"]), 4)));
                        }
                        if (dt >= DateTime.Now.AddYears(-1))
                        {
                            _PList1.Add(new Point((double)i, Math.Round(Convert.ToDouble(da.Rows[i]["NetValue"]), 4)));
                        }
                    }
                    Line line1 = LinearRegression.LinearRegressionSolve(_PList1);
                    Line line2 = LinearRegression.LinearRegressionSolve(_PList2);
                    Line line3 = LinearRegression.LinearRegressionSolve(_PList3);
                    Line line4 = LinearRegression.LinearRegressionSolve(_PList4);

                    equationList.Add(new Point(line1.K, line1.B));
                    equationList.Add(new Point(line2.K, line2.B));
                    equationList.Add(new Point(line3.K, line3.B));
                    equationList.Add(new Point(line4.K, line4.B));
                    begins[0] = rows - _PList1.Count;
                    begins[1] = rows - _PList2.Count;
                    begins[2] = rows - _PList3.Count;
                    begins[3] = rows;
                    lineReturn.topValue = _PList3.Max(x => x.Y);
                    lineReturn.points = equationList;
                    lineReturn.begins = begins;
                    return lineReturn;
                }
                else
                {
                    return null;
                }               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;

            }

        }

        public static Line LinearRegressionSolve(List<Point> _plist)
        {
            double k = 0, b = 0;
            double sumX = 0, sumY = 0;
            double avgX = 0, avgY = 0;
            foreach (var v in _plist)
            {
                sumX += v.X;
                sumY += v.Y;
            }
            avgX = sumX / (_plist.Count + 0.0);
            avgY = sumY / (_plist.Count + 0.0);

            //sumA=(x-avgX)(y-avgY)的和 sumB=(x-avgX)平方
            double sumA = 0, sumB = 0;
            foreach (var v in _plist)
            {
                sumA += (v.X - avgX) * (v.Y - avgY);
                sumB += (v.X - avgX) * (v.X - avgX);
            }
            k = sumA / (sumB + 0.0);
            b = avgY - k * avgX;

            Line line = new Line(k, b);
            return line;
        }

        public static Line LinearRegressionSolve2(List<Point> _plist)
        {
            double k = 0, b = 0;
            double sumX = 0, sumY = 0;
            double sumXY = 0, sumXX = 0;
            foreach (var v in _plist)
            {
                sumX += v.X;
                sumY += v.Y;
                sumXY += v.X * v.Y;
                sumXX += v.X * v.X;
            }

            k = (sumX * sumY / (_plist.Count + 0.0) - sumXY) / (sumX * sumX / (_plist.Count + 0.0) - sumXX);
            b = (sumY - k * sumX) / (_plist.Count + 0.00);

            Line line = new Line(k, b);
            return line;
        }

        public static Line LinearRegressionSolve3(List<Point> _plist)
        {
            double k = 0, b = 0;
            double sumX = 0, sumY = 0;
            double avgX = 0, avgY = 0;
            double sumXY = 0, sumXX = 0;
            foreach (var v in _plist)
            {
                sumX += v.X;
                sumY += v.Y;
                sumXY += v.X * v.Y;
                sumXX += v.X * v.X;
            }
            avgX = sumX / (_plist.Count + 0.0);
            avgY = sumY / (_plist.Count + 0.0);

            k = (sumXY - avgY * sumX) / (sumXX - avgX * sumX);
            b = avgY - k * avgX;

            Line line = new Line(k, b);
            return line;
        }


    }



    public class Line
    {
        public double K { get; set; }
        public double B { get; set; }

        public Line(double k, double b)
        {
            K = k;
            B = b;
        }
    }

    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

    public class LineReturn
    {
        public double topValue { get; set; }
        public List<Point> points { get; set; }

        public List<int> begins { get; set; }
        public DataTable dataSet { get; set; }

        public DateTime updatetime { get; set; }
    }
}
