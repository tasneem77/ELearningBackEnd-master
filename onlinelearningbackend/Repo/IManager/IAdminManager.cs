using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.Repo.IManager
{
    public interface IAdminManager
    {
        int GetTotakBranches();
        int GetTotakTracks();
        int GetTotakInstructors();
        int GetTotakCourses();
        int GetTotalIntakes();
        int GetAllProjects();
    }
}
