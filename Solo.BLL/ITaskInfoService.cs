using Solo.Model;
using Solo.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Solo.BLL
{
    public interface ITaskInfoService
    {
        List<TaskInfo> getTaskInfoByDate(int year, int month, int dayCount);

        public List<TaskInfo> GetUnfinishList();
        int DeleteById(int id);

        int InsertTaskInfo(TaskInfo newInfo);

        void LoadEntity(DataRow row, TaskInfo newInfo);

        int UpdateTaskInfo(TaskInfo newInfo);

        int GetRecordCount();
        TaskInfo GetEntitybyId(int Id);
        int UpdateFinish(int Id);
        int UpdateUnFinish(int Id);

        int CheckUnFinish();

        List<PlanView> GetPlan();

        List<TaskInfo> GetRecentTaskInfos(int pageno);
    }
}
