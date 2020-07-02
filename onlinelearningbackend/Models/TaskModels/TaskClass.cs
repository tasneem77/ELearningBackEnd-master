using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.Models
{
    public class TaskClass
    {
        [Key]
        public int TaskId { get; set; }
        [Required]
        public string TaskName { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        public bool IsActive { get; set; } = true;//c# bool => bit data type in mssql(1 true ,,,,, 0 false)
        public int? CourseId { get; set; }
        public virtual ICollection<TaskSolution> TaskSolutions { get; set; } = new HashSet<TaskSolution>();
        //public virtual ICollection<Course> Courses { get; set; } = new HashSet<Course>();

        public virtual Course Course { get; set; }
    }
}
