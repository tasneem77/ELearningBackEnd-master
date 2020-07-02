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
    public class UserProjectManager:IUserProjectManager
    {
        ApplicationDbContext db;
        public UserProjectManager(ApplicationDbContext _db)
        {
            this.db = _db;
        }

        public UserProjectModel GetUserProjectIdByStudentIdAndProjectId(string studentId, int ProjectId)
        {
            var UserProject = db.UserProjectModels.FromSqlRaw<UserProjectModel>($"EXEC dbo.usp_UserProjectModel_Select_By_Student_ID_And_Project_Id '{studentId}',{ProjectId}").ToList<UserProjectModel>().FirstOrDefault();
            return UserProject;
        }
        public IEnumerable<UserProjectModel> GetCollaboratorIdByProjectId(int ProjectId)
        {
            var Collaborators = db.UserProjectModels.FromSqlRaw<UserProjectModel>($"EXEC dbo.usp_UserProjectModel_Select_By_Project_ID {ProjectId}").ToList<UserProjectModel>();
            return Collaborators;
        }
        public UserProjectModel AddCollaboratorByUserId(string UserId,int ProjectId)
        {
           var addedcolab= db.UserProjectModels.FromSqlRaw<UserProjectModel>($"EXEC dbo.usp_UserProjectModel_Add_Collaborator '{UserId}' , {ProjectId}").ToList().FirstOrDefault();
            return addedcolab;
        }
        public void MakeCollaboratorOwnerByUserIdAndProjectId(string UserId, int ProjectId)
        {
           db.Database.ExecuteSqlRaw($"EXEC dbo.usp_UserProjectModel_Make_Owner '{UserId}',{ProjectId}");
           
        }

        public void DeleteCollaboratorByUserIdAndProjectId(string UserId,int ProjectId)
        {
            var x= db.Database.ExecuteSqlRaw($"EXEC dbo.usp_UserProjectModel_Delete '{UserId}',{ProjectId}");
         
        }
    }
}
