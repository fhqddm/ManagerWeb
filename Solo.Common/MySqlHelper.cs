using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Solo.Common
{
    public class MySqlHelper
    {


        public static string connstr = AppConfigurtaionServices.Configuration.GetConnectionString("DefaultConnection");


        /// <summary>
        /// 封装一个执行的sql 返回受影响的行数
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sqlText, params MySqlParameter[] parameters)
        {

            using (MySqlConnection conn = new MySqlConnection(connstr))
            {

                using (MySqlCommand cmd = conn.CreateCommand())
                {

                    conn.Open();
                    cmd.CommandText = sqlText;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();

                }

            }

        }

        /// <summary>
        /// 执行sql,返回查询结果中的第一行第一列的值
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sqlText, params MySqlParameter[] parameters)
        {

            using (MySqlConnection conn = new MySqlConnection(connstr))
            {

                using (MySqlCommand cmd = conn.CreateCommand())
                {

                    conn.Open();
                    cmd.CommandText = sqlText;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();

                }

            }

        }

        /// <summary>
        /// 执行sql 返回一个DataTable
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteTable(string sqlText, params MySqlParameter[] parameters)
        {

            using (MySqlDataAdapter adapter = new MySqlDataAdapter(sqlText, connstr))
            {

                DataTable dt = new DataTable();
                if (parameters!=null)
                {
                    adapter.SelectCommand.Parameters.AddRange(parameters);
                }               
                adapter.Fill(dt);
                return dt;

            }

        }

        /// <summary>
        /// 执行sql脚本
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static MySqlDataReader ExecuteReader(string sqlText, params MySqlParameter[] parameters)
        {

            MySqlConnection conn = new MySqlConnection(connstr);
            MySqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandText = sqlText;
            cmd.Parameters.AddRange(parameters);

            return cmd.ExecuteReader(CommandBehavior.CloseConnection);

        }

        public static DataTable GetTable(string sql, CommandType type, params SqlParameter[] pars)
        {
            using (MySqlConnection conn = new MySqlConnection(connstr))
            {
                using (MySqlDataAdapter atper = new MySqlDataAdapter(sql, conn))
                {
                    atper.SelectCommand.CommandType = type;
                    if (pars != null)
                    {
                        atper.SelectCommand.Parameters.AddRange(pars);
                    }
                    DataTable da = new DataTable();
                    atper.Fill(da);
                    return da;
                }
            }
        }

    }
}