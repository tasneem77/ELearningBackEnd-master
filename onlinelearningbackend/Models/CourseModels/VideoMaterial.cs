using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.Models
{
    public class VideoMaterial
    {
        public int VideoMaterialId { get; set; }
        public string VideoMaterialName { get; set; }
        public string URL { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

    }
}
