using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.Models
{
    public class TaskSolution
    {
        

        public int TaskSolutionId { get; set; }
        public string TaskSolutionURL { get; set; }

        [ForeignKey("AspNetUsers")]
        public string MyUserModelId { get; set; }
        public virtual MyUserModel MyUserModel { get; set; }
        public int? CourseId { get; set; }
        public virtual Course Course { get; set; }
        [ForeignKey("Tasks")]
        public int? TaskId { get; set; }
        public virtual TaskClass Task { get; set; }

        public bool IsActive { get; set; } = true;
        public string FileName { get; internal set; }
    }
}
