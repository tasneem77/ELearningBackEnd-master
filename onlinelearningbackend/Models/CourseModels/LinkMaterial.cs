using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.Models
{
    public class LinkMaterial
    {
        public int LinkMaterialId { get; set; }
        public string LinkMaterialName { get; set; }
        public string URL { get; set; }
        public virtual Course Course { get; set; }
    }
}
