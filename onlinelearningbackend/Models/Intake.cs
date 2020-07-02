using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.Models
{
    public class Intake
    {
        public int IntakeId { get; set; }
        public int IntakeName { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual ICollection<ProjectModel> ProjectModels { get; set; } = new HashSet<ProjectModel>();
        public virtual ICollection<MyUserModel> MyUserModels { get; set; } = new HashSet<MyUserModel>();

    }
}
