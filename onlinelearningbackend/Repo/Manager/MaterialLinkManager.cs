using Microsoft.EntityFrameworkCore;
using onlinelearningbackend.Data;
using onlinelearningbackend.Manager;
using onlinelearningbackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.Repository.Repo
{
    public class MaterialLinkManager : IMaterialLinkManager
    {
        ApplicationDbContext db;
        public MaterialLinkManager(ApplicationDbContext _db)
        {
            this.db = _db;
        }
   
        public IEnumerable<LinkMaterial> AddMaterial(LinkMaterial NewMaterial)
        {
            var material = db.LinkMaterials.FromSqlRaw("EXEC dbo.usp_LinkMaterials_Insert {0},{1}"
                                               , NewMaterial.LinkMaterialName
                                               , NewMaterial.URL
                                               )
                                           .ToList<LinkMaterial>();
            return material;
        }

        public IEnumerable<LinkMaterial> DeleteMaterialByMaterialId(int MaterialId)
        {
            var material = db.LinkMaterials.FromSqlRaw("EXEC dbo.usp_MatrialLink_Delete {0}", MaterialId).ToList<LinkMaterial>();
            return material;
        }

        public IEnumerable<LinkMaterial> EditMaterial(LinkMaterial EditMaterial)
        {
            var material = db.LinkMaterials.FromSqlRaw("EXEC dbo.usp_LinkMaterials_Insert {0},{1},{2}"
                                               , EditMaterial.LinkMaterialId
                                              , EditMaterial.LinkMaterialName
                                              , EditMaterial.URL
                                              )
                                          .ToList<LinkMaterial>();
            return material;
        }

        public IEnumerable<LinkMaterial> MaterialLinktByCourseId(int CourseId)
        {
            var material = db.LinkMaterials.FromSqlRaw("EXEC dbo.usp_MaterialLink_Select_by_CourseId {0}", CourseId).ToList<LinkMaterial>();
            return material;
        }
        //public void AddMaterial(int MId,string MName,string Url,int CrsId)
        //{
        //    db.LinkMaterials.FromSqlRaw<LinkMaterial>($"EXEC usp_LinkMaterials_Insert {MId}{MName}{Url}{CrsId}");
        //}

        //public void DeleteMaterialByMaterialId(int MaterialId)
        //{
        //    db.LinkMaterials.FromSqlRaw($"EXEC usp_LinkMaterials_Delete {MaterialId}");
        //}

        //public void EditMaterial(int MId, string MName, string Url, int CrsId)
        //{
        //    db.LinkMaterials.FromSqlRaw<LinkMaterial>($"EXEC usp_LinkMaterials_Update {MId}{MName}{Url}{CrsId}");
        //}

        //public List<LinkMaterial> MaterialLinktByCourseId(int CourseId)
        //{
        //    var materials = db.LinkMaterials.FromSqlRaw($"EXEC dbo.usp_LinkMaterials_CrsId {CourseId}").ToList();
        //    return materials;
        //}
    }
}
