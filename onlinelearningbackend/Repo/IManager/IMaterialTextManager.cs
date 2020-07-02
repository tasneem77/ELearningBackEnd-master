using onlinelearningbackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.Manager
{
  public  interface IMaterialTextManager
    {
        IEnumerable<TextMaterial> MaterialTextByCourseId(int CourseId);
        TextMaterial AddMaterial(TextMaterial NewMaterial);
        IEnumerable<TextMaterial> EditMaterial(TextMaterial EditMaterial);
        IEnumerable<TextMaterial> DeleteMaterialByMaterialId(int MaterialId);
     

    }
}
