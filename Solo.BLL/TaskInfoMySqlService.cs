using MySql.Data.MySqlClient;
using Solo.Common;
using Solo.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlHelper = Solo.Common.MySqlHelper;

namespace Solo.BLL
{
    public class TaskInfoMySqlService
    {
        public  List<TaskInfo> getTaskInfoByDate(int year, int month, int dayCount)
        {
            DateTime dtStart = new DateTime(year, month, 1, 0, 0, 0).AddMinutes(-1);
            DateTime dtEnd = new DateTime(year, month, dayCount, 23, 59, 59).AddMinutes(1);
            try
            {
                List<TaskInfo> list = new List<TaskInfo>();
                string sql = "select * from TaskInfos where DeadLine > cast('" + dtStart + "' as datetime) and  DeadLine < cast('" + dtEnd + "' as datetime)";
                MySqlParameter[] pars = null;
                //pars[0].Value = dt;
                DataTable da = MySqlHelper.ExecuteTable(sql, pars);
                if (da.Rows.Count > 0)
                {
                    for (int i = 0; i < da.Rows.Count; i++)
                    {
                        TaskInfo ti = new TaskInfo();
                        ti.Id = Convert.ToInt32(da.Rows[i]["Id"]);
                        ti.taskName = da.Rows[i]["taskName"].ToString();
                        ti.DeadLine = Convert.ToDateTime(da.Rows[i]["DeadLine"]);
                        if (!string.IsNullOrEmpty(da.Rows[i]["Status"].ToString()))
                        {
                            ti.Status = Convert.ToInt32(da.Rows[i]["Status"]);
                        }
                        list.Add(ti);
                    }

                }
                return list;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public  List<TaskInfo> GetUnfinishList()
        {
            string sql = "select * from TaskInfo where Status=3";
            DataTable da = MySqlHelper.GetTable(sql, CommandType.Text);
            List<TaskInfo> list = null;
            if (da.Rows.Count > 0)
            {
                list = new List<TaskInfo>();
                TaskInfo newInfo = null;
                foreach (DataRow row in da.Rows)
                {
                    newInfo = new TaskInfo();
                    LoadEntity(row, newInfo);
                    list.Add(newInfo);
                }
            }
            return list;
        }

        public  int DeleteById(int id)
        {
            string sql = "delete from TaskInfo where Id=@Id";
            return MySqlHelper.ExecuteNonQuery(sql,  new MySqlParameter("@Id", id));
        }



        public  int InsertTaskInfo(TaskInfo newInfo)
        {
            string sql = "insert into TaskInfo(TaskName,DeadLine,Status) values(@TaskName,@DeadLine,@Status)";
            MySqlParameter[] pars = {
                                   new MySqlParameter("@TaskName",MySqlDbType.VarChar,32),
                                   new MySqlParameter("@DeadLine",MySqlDbType.DateTime),
                                   new MySqlParameter("@Status",MySqlDbType.Int32,4)
                                 };
            pars[0].Value = newInfo.taskName;
            pars[1].Value = newInfo.DeadLine;
            pars[2].Value = newInfo.Status;
            return MySqlHelper.ExecuteNonQuery(sql, pars);
        }

        public  void LoadEntity(DataRow row, TaskInfo newInfo)
        {
            newInfo.Id = Convert.ToInt32(row["Id"]);
            newInfo.taskName = row["TaskName"] != DBNull.Value ? row["TaskName"].ToString() : string.Empty;
            newInfo.DeadLine = Convert.ToDateTime(row["DeadLine"]);
            newInfo.Status = Convert.ToInt32(row["Status"]);
        }

        public  int UpdateTaskInfo(TaskInfo newInfo)
        {
            string sql = "update TaskInfo set TaskName=@TaskName,DeadLine=@DeadLine where Id=@Id";
            MySqlParameter[] pars = {
                                 new MySqlParameter("@TaskName",MySqlDbType.VarChar,32),
                                   new MySqlParameter("@DeadLine",MySqlDbType.DateTime),
                                     new MySqlParameter("@Id",MySqlDbType.Int32,4)
                                 };
            pars[0].Value = newInfo.taskName;
            pars[1].Value = newInfo.DeadLine;
            pars[2].Value = newInfo.Id;
            return MySqlHelper.ExecuteNonQuery(sql, pars);
        }

        public int GetRecordCount()
        {
            string sql = "select count(*) from TaskInfo";
            int count = Convert.ToInt32(MySqlHelper.ExecuteScalar(sql, null));
            return count;
        }

        public TaskInfo GetEntitybyId(int Id)
        {
            string sql = "select * from TaskInfo where id = @Id";
            MySqlParameter[] pars = {
                                   new MySqlParameter("@Id",MySqlDbType.Int32,4)
                                 };
            pars[0].Value = Id;
            DataTable da = MySqlHelper.ExecuteTable(sql, pars);
            TaskInfo ti = new TaskInfo();
            ti.Id = Id;
            ti.taskName = da.Rows[0]["TaskName"].ToString();
            ti.DeadLine = Convert.ToDateTime(da.Rows[0]["DeadLine"]);
            return ti;
        }

        public  int UpdateFinish(int Id)
        {
            string sql = "update TaskInfo set Status=2 where Id=@Id";
            MySqlParameter[] pars = {
                                      new MySqlParameter("@Id",MySqlDbType.Int32,4)
                                 };
            pars[0].Value = Id;
            return MySqlHelper.ExecuteNonQuery(sql,  pars);

        }

        public  int UpdateUnFinish(int Id)
        {
            DateTime deadlline;
            string sql1 = "select DeadLine from TaskInfo where Id=@Id";
            MySqlParameter[] pars1 = {
                                      new MySqlParameter("@Id",MySqlDbType.Int32,4)
                                 };
            pars1[0].Value = Id;
            DataTable da = MySqlHelper.ExecuteTable(sql1, pars1);
            if (da.Rows.Count == 1)
            {
                deadlline = Convert.ToDateTime(da.Rows[0]["DeadLine"]);
            }
            else
            {
                return 0;
            }

            if (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0) > deadlline)
            {
                string sql2 = "update TaskInfo set Status=1 where Id=@Id";
                MySqlParameter[] pars2 = {
                                      new MySqlParameter("@Id",MySqlDbType.Int32,4)
                                 };
                pars2[0].Value = Id;
                return MySqlHelper.ExecuteNonQuery(sql2, pars2);
            }
            else
            {
                string sql2 = "update TaskInfo set Status=3 where Id=@Id";
                MySqlParameter[] pars2 = {
                                      new MySqlParameter("@Id",MySqlDbType.Int32,4)
                                 };
                pars2[0].Value = Id;
                return MySqlHelper.ExecuteNonQuery(sql2, pars2);
            }
        }

        public int CheckUnFinish()
        {
            return 0;
        }
    }
}
