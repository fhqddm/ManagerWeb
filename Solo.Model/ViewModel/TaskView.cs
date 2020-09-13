using System;
using System.Collections.Generic;
using System.Text;

namespace Solo.Model.ViewModel
{
    public class TaskView
    {
        public int Id { get; set; }
        public string taskName { get; set; }
        public DateTime DeadLine { get; set; }
        public Nullable<int> Status { get; set; }// 1:过期,2.完成,3:待完成
        public string Detail { get; set; }
        public string SkillName { get; set; }
        public double Duration { get; set; }
        public string Type { get; set; }

    }
}
