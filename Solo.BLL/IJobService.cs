using Solo.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solo.BLL
{
    public interface IJobService
    {
        List<Job> GetJobs(Job jobquery);

        List<Skill> GetSkills(Skill skillquery);
    }
}
