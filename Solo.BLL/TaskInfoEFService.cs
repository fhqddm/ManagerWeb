using Microsoft.EntityFrameworkCore;
using Solo.Model;
using Solo.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solo.BLL
{
    public class TaskInfoEFService : ITaskInfoService
    {
        //private MyContext mycontext;

        //public TaskInfoEFService()
        //{
        //    mycontext = new MyContext();
        //}
        public int DeleteById(int id)
        {
            using (MyContext myContext = new MyContext())
            {

                return myContext.Database.ExecuteSqlCommand($"delete from TaskInfos where Id={id}");
            }
        }

        public TaskInfo GetEntitybyId(int Id)
        {
            using (MyContext myContext = new MyContext())
            {

                var taskInfo = myContext.TaskInfos.Single(x => x.Id == Id);

                return taskInfo;
            }
        }

        public List<PlanView> GetPlan()
        {
            using (MyContext myContext = new MyContext()) 
            {
                List<PlanView> lp = new List<PlanView>();
                var tasks = myContext.TaskInfos.ToList();
                var skills = myContext.Skills.ToList();
                foreach (var item in skills)
                {
                    double sd = 0;
                    try
                    {
                       
                       sd = tasks.Where(x => item.SkillName.Split(',')[0] == x.SkillName).Sum(x => x.Duration);
                    }
                    catch (Exception ex)
                    {

                    }
                    if (sd>0)
                    {
                        lp.Add(new PlanView
                        {
                            SkillName = item.SkillName.Split(',')[0],
                            Duration = sd
                        });
                    }
                    
                }
                return lp;
            }
        }

        public int GetRecordCount()
        {
            throw new NotImplementedException();
        }

        public List<TaskInfo> getTaskInfoByDate(int year, int month, int dayCount)
        {
            DateTime dtStart = new DateTime(year, month, 1, 0, 0, 0).AddMinutes(-1);
            DateTime dtEnd = new DateTime(year, month, dayCount, 23, 59, 59).AddMinutes(1);

            using (MyContext myContext = new MyContext())
            {

                var tflist =  (from t in myContext.TaskInfos
                              where t.DeadLine > dtStart
                              && t.DeadLine < dtEnd
                              select t).ToList();

                return tflist;
            }
            
            
                
        }

        public List<TaskInfo> GetUnfinishList()
        {
            throw new NotImplementedException();
        }

        public int InsertTaskInfo(TaskInfo newInfo)
        {
            using (MyContext myContext = new MyContext())
            {

                //if (myContext.Skills.Where(x => x.SkillName.Contains(newInfo.SkillName)).ToList().Count > 0)
                //{
                myContext.TaskInfos.Add(newInfo);
                return myContext.SaveChanges();
                //}
                //else
                //{
                //    return 0;
                //}
            }
                
        }

        public void LoadEntity(DataRow row, TaskInfo newInfo)
        {
            throw new NotImplementedException();
        }

        public int UpdateFinish(int Id)
        {
            using (MyContext myContext = new MyContext()) {
                string sql = $"update TaskInfos set Status=2 where Id={Id}";
                return myContext.Database.ExecuteSqlCommand(sql);
            }
                
        }

        public int UpdateTaskInfo(TaskInfo newInfo)
        {
            using (MyContext myContext = new MyContext())
            {
                    var taskInfo = myContext.TaskInfos.Single(x => x.Id == newInfo.Id);
                    taskInfo.SkillName = newInfo.SkillName;
                    taskInfo.Status = newInfo.Status;
                    taskInfo.taskName = newInfo.taskName;
                    taskInfo.Detail = newInfo.Detail;
                    taskInfo.Duration = newInfo.Duration;
                    taskInfo.DeadLine = newInfo.DeadLine;
                    return myContext.SaveChanges();
            }
        }

        public int UpdateUnFinish(int Id)
        {
            DateTime deadlline; 
            using (MyContext myContext = new MyContext())
            {
                TaskInfo taskInfo = myContext.TaskInfos.Single(x => x.Id == Id);

                deadlline = taskInfo.DeadLine;

                if (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0) > deadlline)
                {
                    return myContext.Database.ExecuteSqlCommand($"update TaskInfos set Status=1 where Id={Id}");
                }
                else
                {
                    return myContext.Database.ExecuteSqlCommand($"update TaskInfos set Status=3 where Id={Id}");
                }

                
            }
        }

        public int CheckUnFinish()
        {
            DateTime yestodayEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0).AddMinutes(-1);

            using (MyContext myContext = new MyContext())
            {
                var taskList = myContext.TaskInfos.Where(x => x.DeadLine < yestodayEnd).ToList();

                if (taskList.Count >0)
                {
                    taskList.ForEach(x => {
                        if (x.Status ==3)
                        {
                            x.Status = 1;
                        }
                    } );

                    return myContext.SaveChanges();
                }
                else
                {
                    return 0;
                }

                
            }
            

        }
    }
}
