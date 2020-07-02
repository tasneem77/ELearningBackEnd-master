
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        [Required]
        public string CourseName { get; set; }
        [Required]

        public string Description { get; set; }

        [Required]

        public int IntervalInDays { get; set; }
        //public DateTime StartingDate { get; set; }

        [Required]
        public string EnrollmentKey { get; set; }

        public bool? IsActive { get; set; } = true;

        public int? TrackId { get; set; }
        public virtual Track Track { get; set; }
        public virtual Topic Topic { get; set; }

        public virtual ICollection<CourseMyUserModel> CourseMyUserModels { get; set; } = new HashSet<CourseMyUserModel>();
        public virtual ICollection<TaskClass> Tasks { get; set; } = new HashSet<TaskClass>();
        public virtual ICollection<TaskSolution> TaskSolutions { get; set; } = new HashSet<TaskSolution>();
        public virtual ICollection<TextMaterial> TextMaterials { get; set; } = new HashSet<TextMaterial>();
        public virtual ICollection<VideoMaterial> VideoMaterials { get; set; } = new HashSet<VideoMaterial>();
        public virtual ICollection<LinkMaterial> LinkMaterials { get; set; } = new HashSet<LinkMaterial>();
        public virtual ICollection<CourseMaterialModel> CourseMaterialModels { get; set; } = new HashSet<CourseMaterialModel>();


    }
}
