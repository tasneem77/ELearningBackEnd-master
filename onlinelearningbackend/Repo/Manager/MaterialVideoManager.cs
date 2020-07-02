using Microsoft.EntityFrameworkCore;
using onlinelearningbackend.Data;
using onlinelearningbackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.Manager
{
    public class MaterialVideoManager : IMaterialVideoManager
    {
        ApplicationDbContext DB;
        public MaterialVideoManager(ApplicationDbContext _DB)
        {
            DB = _DB;
        }
        public IEnumerable<VideoMaterial> AddMaterial(VideoMaterial NewMaterial)
        {
            var material = DB.VideoMaterials.FromSqlRaw("EXEC dbo.usp_VidoeMaterials_Insert {0},{1}"
                                               , NewMaterial.VideoMaterialName
                                               , NewMaterial.URL
                                               )
                                           .ToList<VideoMaterial>();
            return material;
        }

        public IEnumerable<VideoMaterial> DeleteMaterialByMaterialId(int MaterialId)
        {
            var material = DB.VideoMaterials.FromSqlRaw("EXEC dbo.usp_MatrialLink_Delete {0}", MaterialId).ToList<VideoMaterial>();
            return material;
        }

        public IEnumerable<VideoMaterial> EditMaterial(VideoMaterial EditMaterial)
        {
            var material = DB.VideoMaterials.FromSqlRaw("EXEC dbo.usp_VidoeMaterials_Insert {0},{1},{2}"
                                                , EditMaterial.VideoMaterialId
                                               , EditMaterial.VideoMaterialName
                                               , EditMaterial.URL
                                               )
                                           .ToList<VideoMaterial>();
            return material;
        }

        public IEnumerable<VideoMaterial> MaterialVideotByCourseId(int CourseId)
        {
            var material = DB.VideoMaterials.FromSqlRaw("EXEC dbo.usp_MaterialVideo_Select_by_CourseId {0}", CourseId).ToList<VideoMaterial>();
            return material;
        }
    }
}
