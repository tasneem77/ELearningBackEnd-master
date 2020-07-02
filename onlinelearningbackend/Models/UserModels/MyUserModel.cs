using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.Models
{
    public class MyUserModel : IdentityUser
    {
        public MyUserModel() : base()
        {
        }
        public MyUserModel(string UserName) : base(UserName)
        {

        }

        public string PrifleImageUrl { get; set; }
        public int? IntakeId { get; set; }
        public Intake Intake { get; set; }
        public string City { get; set; }
        public bool IsActive { get; set; } = true;

        public DateTime DateOfBirth { get; set; }
        public virtual ICollection<TaskSolution> TaskSolutions { get; set; } = new HashSet<TaskSolution>();
        public virtual ICollection<CourseMyUserModel> CourseMyUserModels { get; set; } = new HashSet<CourseMyUserModel>();
        public virtual ICollection<UserProjectModel> UserProjectModels { get; set; } = new HashSet<UserProjectModel>();
        public int? BranchId { get; set; }
        public virtual Branch Branch { get; set; }
        public int? TrackId { get; set; }
        public virtual Track Track { get; set; }

    }
}
