using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Solo.BLL;
using Solo.Common;
using Solo.Model;
using Solo.Model.ViewModel;

namespace ManagerWeb.Controllers
{

    [Authorize(Policy = "RequireRoles")]
    public class TaskController : Controller
    {

        private readonly ITaskInfoService _taskInfoService;//alt+Enter
        private readonly IJobService _jobService;

        public TaskController(ITaskInfoService taskInfoService, IJobService jobService)
        {
            _taskInfoService = taskInfoService;
            _jobService = jobService;
        }



        [HttpPost]
        //[AllowAnonymous]
        public ActionResult GetRecentTasks(int pageno)
        {
            _taskInfoService.CheckUnFinish();
            List<TaskInfo> tiList = _taskInfoService.GetRecentTaskInfos(pageno);
            return Ok(tiList);
        }




        //[AllowAnonymous]
        [HttpPost]
        public ActionResult AjaxJob(Job job)
        {
            return Ok(_jobService.GetJobs(job));
        }




        [HttpPost]
        //[AllowAnonymous]
        public ActionResult GetDetails(int Id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TaskInfo,TaskView>());
            var mapper = config.CreateMapper();
            TaskInfo taskInfo = _taskInfoService.GetEntitybyId(Id);
            var taskView = mapper.Map<TaskView>(taskInfo);
            var skill = _jobService.GetSkills(new Skill { SkillName = taskInfo.SkillName });
            if (skill.Count>0)
            {
                taskView.Type = skill[0].Type;
            }
            else
            {
                taskView.Type = "Other";
            }

            return Ok(taskView);
        }

        [HttpPost]
        //[AllowAnonymous]
        public ActionResult Add(TaskInfo taskInfo)
        {          
            if (ModelState.IsValid)
            {
                return Ok(_taskInfoService.InsertTaskInfo(taskInfo));
                //if (_taskInfoService.InsertTaskInfo(taskInfo) > -1 )
                //{

                //    return Ok(true);
                //}
                //else
                //{
                //    return Ok(false);
                //}
            }
            else
            {
                return Ok(false);
            }

        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Plan()
        {
            var skills = _jobService.GetSkills(null);
            var Dotnet = skills.Where(x => x.Type == "DotNet");
            var ImageProcess = skills.Where(x => x.Type == "ImageProcess");

            //var task = _taskInfoService.get

            string builderjson = "{'all':100, 'donet':{ ";
            foreach (var item in Dotnet)
            {
                builderjson += "'" + item.SkillName + "':" + item.Frequency + ",";
            }
            builderjson += "},'imageprocess':{ ";
            foreach (var item in ImageProcess)
            {
                builderjson += "'" + item.SkillName + "':" + item.Frequency + ",";
            }
            //builderjson += "},'plan':{'bootstrap':30,'tensorflow':40,'javascript':50,'mvc':60,'.net':70} }";
            builderjson += "},'plan':{";
            var lp = _taskInfoService.GetPlan().OrderBy(x => x.Duration);
            foreach (var item in lp)
            {
                builderjson += "'" + item.SkillName + "':" + item.Duration + ",";
            }
            builderjson += "}}";

            ViewData["builderjson"] = builderjson;
            return Ok(builderjson);
        }

        [HttpPost]
        public ActionResult DeleteTask(int Id)
        {
            if (_taskInfoService.DeleteById(Id) > 0)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }





        [HttpPost]
        public ActionResult<bool> Update(TaskInfo taskInfo)
        {
            if (!string.IsNullOrEmpty(taskInfo.Detail))
            {
                taskInfo.Detail = taskInfo.Detail.Trim();
            }
            
            if (_taskInfoService.UpdateTaskInfo(taskInfo) > 0)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }

        [HttpPost]
        public ActionResult GetTaskInfosByMonth(int year, int month)
        {
            _taskInfoService.CheckUnFinish();
            DateTime dt = new DateTime(year, month, 1);
            List<TaskInfo> tiList = _taskInfoService.getTaskInfoByDate(year, month, DateHelper.getDayCountByTime(dt));
            return Ok(tiList);
        }
    }
}