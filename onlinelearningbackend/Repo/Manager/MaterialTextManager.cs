using System;
using System.Collections.Generic;
using System.Linq;
using onlinelearningbackend.Data;
using Microsoft.EntityFrameworkCore;
using onlinelearningbackend.Models;


namespace onlinelearningbackend.Manager
{
    public class MaterialTextManager : IMaterialTextManager
    {
        ApplicationDbContext DB;
        public MaterialTextManager(ApplicationDbContext _DB)
        {
            DB = _DB;
        }
        public TextMaterial AddMaterial(TextMaterial NewMaterial)
        {
           var material=DB.TextMaterials.FromSqlRaw("EXEC dbo.usp_TextMaterials_Insert {0},{1},{2}"
                                                , NewMaterial.TextMaterialName
                                                ,NewMaterial.URL
                                                ,NewMaterial.CourseId
                                                ).ToList().FirstOrDefault();
            return material;


        }

        public IEnumerable<TextMaterial> DeleteMaterialByMaterialId(int MaterialId)
        {
            var material = DB.TextMaterials.FromSqlRaw("EXEC dbo.usp_MatrialText_Delete {0}", MaterialId);
            return material;
        }
        /// <summary>
        /// --------------------------------------remain-----------------------------------------------
        /// </summary>
  
        public IEnumerable<TextMaterial> EditMaterial(TextMaterial EditMaterial)
        {
            var material = DB.TextMaterials.FromSqlRaw("EXEC dbo.usp_TextMaterials_Insert {0},{1},{2}"
                                                , EditMaterial.TextMaterialId
                                                , EditMaterial.TextMaterialName
                                                , EditMaterial.URL
                                                )  ;
            return material;
        }

        public IEnumerable<TextMaterial> MaterialTextByCourseId(int CourseId)
        {
            var material = DB.TextMaterials.FromSqlRaw("EXEC dbo.usp_MaterialText_Select_by_CourseId {0}", CourseId).ToList<TextMaterial>();
            
            return material;
        }
    }
}
