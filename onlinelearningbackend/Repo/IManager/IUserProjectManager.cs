using onlinelearningbackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.Repo.IManager
{
   public interface IUserProjectManager
    {
        public UserProjectModel GetUserProjectIdByStudentIdAndProjectId(string studentId,int ProjectId);

       public IEnumerable<UserProjectModel> GetCollaboratorIdByProjectId(int ProjectId);
        public UserProjectModel AddCollaboratorByUserId(string UserId, int ProjectId);
        public void MakeCollaboratorOwnerByUserIdAndProjectId(string UserId,int ProjectId);
        public void DeleteCollaboratorByUserIdAndProjectId(string UserId,int ProjectId);



    }
}
