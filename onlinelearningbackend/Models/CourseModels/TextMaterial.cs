using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.Models
{
    public class TextMaterial
    {
        public int TextMaterialId { get; set; }
        public int CourseId { get; set; }

        public string TextMaterialName { get; set; }
        public string URL { get; set; }

        public virtual Course Course { get; set; }

    }
}
