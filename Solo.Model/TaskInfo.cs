using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Solo.Model
{
    public class TaskInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "名不能为空!")]
        [MaxLength(20)]
        public string taskName { get; set; }
        public DateTime DeadLine { get; set; }
        public Nullable<int> Status { get; set; }// 1:过期,2.完成,3:待完成
        public string Detail { get; set; }
        public string SkillName { get; set; }
        public double Duration { get; set; }
    }
}
