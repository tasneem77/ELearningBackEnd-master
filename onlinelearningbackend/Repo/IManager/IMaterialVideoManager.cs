using onlinelearningbackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.Manager
{
   public interface IMaterialVideoManager
    {


        IEnumerable<VideoMaterial> MaterialVideotByCourseId(int CourseId);
        IEnumerable<VideoMaterial> AddMaterial(VideoMaterial NewMaterial);
        IEnumerable<VideoMaterial> EditMaterial(VideoMaterial EditMaterial);
        IEnumerable<VideoMaterial> DeleteMaterialByMaterialId(int MaterialId);

    }
}
