
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.Models
{
    public class ProjectMaterialModel
    {
        public int ProjectMaterialModelId { get; set; }
        public string FileName { get; set; }
        public string PathOnServer { get; set; }

        [ForeignKey("Project")]
        public int? ProjectId { get; set; }
        [Required]
        public ProjectModel Project { get; set; }

        public string Category { get; set; }
    }
}
