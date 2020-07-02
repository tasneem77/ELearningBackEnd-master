using onlinelearningbackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.Repo.IManager
{
   public interface IProjectMaterialManager
    {
        ProjectMaterialModel AddMaterial(int ProjectId, string PathOnServer,string Category,string filename);
      
        IEnumerable<ProjectMaterialModel> GetMaterialByProjectId(int ProjectId);
        ProjectMaterialModel GetMaterialByMaterialId(int MaterialId);
        ProjectMaterialModel DeleteMaterialByMaterialId(int MaterialId);

        ProjectMaterialModel DeleteMaterialByPath(string PathOnServer);
    }
}
