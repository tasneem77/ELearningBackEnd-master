using Microsoft.EntityFrameworkCore;
using onlinelearningbackend.Data;
using onlinelearningbackend.Models;
using onlinelearningbackend.Repo.IManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.Repo.Manager
{
    public class CourseMaterialManager : ICourseMaterialManger
    {
        ApplicationDbContext DB;
        public CourseMaterialManager(ApplicationDbContext _DB)
        {
            DB = _DB;
        }

        public CourseMaterialModel AddMaterial(int CourseId, string PathOnServer, string Category, string filename)
        {
            var material = DB.CourseMaterialModels.FromSqlRaw($"EXEC dbo.usp_CourseMaterialModel_Insert '{PathOnServer}', {CourseId},'{Category}','{filename}'").ToList().FirstOrDefault();
            return material;
        }

        public CourseMaterialModel DeleteMaterialByMaterialId(int MaterialId)
        {
            var material = DB.CourseMaterialModels.FromSqlRaw($"EXEC dbo.usp_CourseMaterialModel_Delete_by_Material_Id {MaterialId} ").ToList().FirstOrDefault();
            return material;
        }

        public CourseMaterialModel DeleteMaterialByPath(string PathOnServer)
        {
            throw new NotImplementedException();
        }

        public CourseMaterialModel GetMaterialByMaterialId(int MaterialId)
        {
            var material = DB.CourseMaterialModels.FromSqlRaw($"EXEC dbo.usp_CourseMaterialModel_Select_by_material_Id {MaterialId} ").ToList().FirstOrDefault();
            return material;
        }

        public IEnumerable<CourseMaterialModel> GetMaterialByCourseId(int CourseId)
        {
            var material = DB.CourseMaterialModels.FromSqlRaw($"EXEC dbo.usp_CourseMaterialModel_Select_by_Course_Id {CourseId} ").ToList();
            return material;
        }
    }
}