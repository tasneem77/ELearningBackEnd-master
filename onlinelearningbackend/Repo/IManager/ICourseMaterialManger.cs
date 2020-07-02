using onlinelearningbackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.Repo.IManager
{
    public interface ICourseMaterialManger
    {
        CourseMaterialModel AddMaterial(int CourseId, string PathOnServer, string Category, string filename);

        IEnumerable<CourseMaterialModel> GetMaterialByCourseId(int CourseId);
        CourseMaterialModel GetMaterialByMaterialId(int MaterialId);
        CourseMaterialModel DeleteMaterialByMaterialId(int MaterialId);

        CourseMaterialModel DeleteMaterialByPath(string PathOnServer);
    }
}