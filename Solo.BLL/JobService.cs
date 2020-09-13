using Solo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solo.BLL
{
    public class JobService : IJobService
    {
        public List<Job> GetJobs(Job query)
        {           
            using (MyContext myContext = new MyContext())
            {
               
               var list =  myContext.Jobs.ToList();
                if (query != null)
                {
                    if (!string.IsNullOrEmpty(query.Requirement))
                    {
                        list = list.Where(x => x.Requirement.ToLower().Contains(query.Requirement.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(query.City))
                    {
                        list = list.Where(x => x.City.Contains(query.City)).ToList();
                    }
                    if (!string.IsNullOrEmpty(query.Type))
                    {
                        list = list.Where(x => x.Type.Contains(query.Type)).ToList();
                    }
                }
                
                return list;
            }
        }

        public List<Skill> GetSkills(Skill skillquery)
        {
            using (MyContext myContext = new MyContext())
            {
                List<Skill> list = new List<Skill>();
                list = myContext.Skills.OrderBy(a => a.Frequency).ToList();

                if (skillquery != null)
                {
                    if (!string.IsNullOrEmpty(skillquery.SkillName))
                    {
                        list = list.Where(x => x.SkillName.Contains(skillquery.SkillName)).ToList();
                    }
                    if (!string.IsNullOrEmpty(skillquery.Type))
                    {
                        list = list.Where(x => x.Type == skillquery.Type).OrderBy(a => a.Frequency).ToList();
                    }
                }               
                
                foreach (var item in list)
                {
                    if (item.SkillName.Contains(","))
                    {
                        item.SkillName = item.SkillName.Split(',')[0];
                    }
                }
                return list;
            }
        }
    }
}
