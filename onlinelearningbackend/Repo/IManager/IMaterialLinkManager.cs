using onlinelearningbackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.Manager
{
   public interface IMaterialLinkManager
    {
        // List<LinkMaterial> MaterialLinktByCourseId(int CourseId);
        // void AddMaterial(int MId, string MName, string Url, int CrsId);
        //void EditMaterial(int MId, string MName, string Url, int CrsId);
        //void DeleteMaterialByMaterialId(int MaterialId);
        IEnumerable<LinkMaterial> MaterialLinktByCourseId(int CourseId);
        IEnumerable<LinkMaterial> AddMaterial(LinkMaterial NewMaterial);
        IEnumerable<LinkMaterial> EditMaterial(LinkMaterial EditMaterial);
        IEnumerable<LinkMaterial> DeleteMaterialByMaterialId(int MaterialId);
    }
}
