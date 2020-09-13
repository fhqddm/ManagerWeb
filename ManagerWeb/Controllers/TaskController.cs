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
    [Authorize(Policy = "Admin")]
    public class TaskController : Controller
    {

        private readonly ITaskInfoService _taskInfoService;//alt+Enter
        private readonly IJobService _jobService;

        public TaskController(ITaskInfoService taskInfoService, IJobService jobService)
        {
            _taskInfoService = taskInfoService;
            _jobService = jobService;
        }

        // GET: Task
        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                ViewData["userName"] = HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.Name).Value;
            }
            _taskInfoService.CheckUnFinish();

            List<TaskInfo> tiList = _taskInfoService.getTaskInfoByDate(DateTime.Now.Year, DateTime.Now.Month, DateHelper.getDayCountByTime(DateTime.Now));

            ViewData["tiList"] = tiList;
            ViewData["selectedDate"] = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            ViewData["dayCount"] = DateHelper.getDayCountByTime(DateTime.Now);
            return View();
        }

        [AllowAnonymous]
        public ActionResult Plan()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                ViewData["userName"] = HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.Name).Value;
            }

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
            var lp = _taskInfoService.GetPlan().OrderBy(x=>x.Duration);
            foreach (var item in lp)
            {
                builderjson += "'" + item.SkillName + "':" + item.Duration + ",";
            }
            builderjson += "}}";

            ViewData["builderjson"] = builderjson;
            return View();
        }
        [AllowAnonymous]
        public ActionResult Job()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                ViewData["userName"] = HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.Name).Value;
            }

            ViewData["Jobs"] = _jobService.GetJobs(new Job { Type = ".net", City = "长沙" });
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult AjaxJob(Job job)
        {
            return Ok(_jobService.GetJobs(job));
        }

        public ActionResult Edit()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                ViewData["userName"] = HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.Name).Value;
            }

            return View("Edit");
        }

        public ActionResult Detail(int Id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                ViewData["userName"] = HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.Name).Value;
            }

            TaskInfo taskInfo = _taskInfoService.GetEntitybyId(Id);
            if (!string.IsNullOrEmpty(taskInfo.Detail))
            {
                taskInfo.Detail = taskInfo.Detail.Trim();
            }
            
            ViewData["Type"] = _jobService.GetSkills(new Skill { SkillName = taskInfo.SkillName })[0].Type;
            return View(taskInfo);
        }

        [HttpPost]
        public ActionResult GetDetails(int Id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TaskInfo,TaskView>());
            var mapper = config.CreateMapper();
            TaskInfo taskInfo = _taskInfoService.GetEntitybyId(Id);
            var taskView = mapper.Map<TaskView>(taskInfo);
            taskView.Type = _jobService.GetSkills(new Skill { SkillName = taskInfo.SkillName })[0].Type;

            return Ok(taskView);
        }

        [HttpPost]
        public ActionResult Add(TaskInfo taskInfo)
        {
            //TaskInfo taskInfo = new TaskInfo();
            //taskInfo.taskName = taskName;
            //taskInfo.DeadLine = DeadLine;
            if (ModelState.IsValid)
            {
                if (taskInfo.DeadLine < new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0))
                {
                    taskInfo.Status = 1;
                }
                else
                {
                    taskInfo.Status = 3;
                }

                if (_taskInfoService.InsertTaskInfo(taskInfo) == 1)
                {
                    return Ok(true);
                }
                else
                {
                    return Ok(false);
                }
            }
            else
            {
                return Ok(false);
            }

        }

        [HttpPost]
        public ActionResult DeleteTask(int Id)
        {
            if (_taskInfoService.DeleteById(Id) > 0)
            {
                return RedirectToAction("Index", "Task");
            }
            else
            {
                return View("Error");
            }
        }


        public ActionResult DateChange(int yearSelected, int monthSelected)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                ViewData["userName"] = HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.Name).Value;
            }

            DateTime dt = new DateTime(yearSelected, monthSelected, 1);
            List<TaskInfo> tiList = _taskInfoService.getTaskInfoByDate(yearSelected, monthSelected, DateHelper.getDayCountByTime(dt));
            ViewData["tiList"] = tiList;
            ViewData["selectedDate"] = dt;
            ViewData["dayCount"] = DateHelper.getDayCountByTime(dt);
            return View("Index");
        }

        [HttpPost]
        public ActionResult SetFinish(int Id)
        {
            if (_taskInfoService.UpdateFinish(Id) > 0)
            {
                return RedirectToAction("Index", "Task");
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult SetUnFinish(int Id)
        {
            if (_taskInfoService.UpdateUnFinish(Id) > 0)
            {
                return RedirectToAction("Index", "Task");
            }
            else
            {
                return View("Error");
            }
        }

        //public ActionResult Update(TaskInfo taskInfo)
        //{
        //    if (HttpContext.User.Identity.IsAuthenticated)
        //    {
        //        ViewData["userName"] = HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.Name).Value;
        //    }

        //    if (_taskInfoService.UpdateTaskInfo(taskInfo)>0)
        //    {
        //        return RedirectToAction("Index", "Task");
        //    }
        //    else
        //    {
        //        return View("Error");
        //    }
        //}

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
    }
}