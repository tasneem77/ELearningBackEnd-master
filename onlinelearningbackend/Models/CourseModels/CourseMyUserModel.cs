using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.Models
{
    public class CourseMyUserModel
    {
        public int CourseMyUserModelId { get; set; }
        public int? CourseId { get; set; }
        public Course Course { get; set; }

        public bool? IsActive { get; set; } = true;

        [ForeignKey("AspNetUsers")]
        public string MyUserModelId { get; set; }
        public MyUserModel MyUserModel { get; set; }
    }
}
