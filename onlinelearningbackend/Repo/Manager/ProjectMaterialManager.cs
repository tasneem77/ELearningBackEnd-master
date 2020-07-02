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
    public class ProjectMaterialManager : IProjectMaterialManager
    {
        ApplicationDbContext DB;
        public ProjectMaterialManager(ApplicationDbContext _DB)
        {
            DB = _DB;
        }

        public ProjectMaterialModel AddMaterial(int ProjectId, string PathOnServer, string Category,string filename)
        {
            var material = DB.ProjectMaterialModels.FromSqlRaw($"EXEC dbo.usp_ProjectMaterialModel_Insert '{PathOnServer}', {ProjectId},'{Category}','{filename}'").ToList().FirstOrDefault();
            return material;
        }

        public ProjectMaterialModel DeleteMaterialByMaterialId(int MaterialId)
        {
            var material = DB.ProjectMaterialModels.FromSqlRaw($"EXEC dbo.usp_ProjectMaterialModel_Delete_by_Material_Id {MaterialId} ").ToList().FirstOrDefault();
            return material;
        }

        public ProjectMaterialModel DeleteMaterialByPath(string PathOnServer)
        {
            throw new NotImplementedException();
        }

        public ProjectMaterialModel GetMaterialByMaterialId(int MaterialId)
        {
            var material = DB.ProjectMaterialModels.FromSqlRaw($"EXEC dbo.usp_ProjectMaterialModel_Select_by_material_Id {MaterialId} ").ToList().FirstOrDefault();
            return material;
        }

        public IEnumerable<ProjectMaterialModel> GetMaterialByProjectId(int ProjectId)
        {
            var material = DB.ProjectMaterialModels.FromSqlRaw($"EXEC dbo.usp_ProjectMaterialModel_Select_by_Project_Id {ProjectId} ").ToList();
            return material;
        }
    }
}
