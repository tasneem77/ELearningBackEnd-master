using onlinelearningbackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.Repo.IManager
{
   public interface IProjectManager
    {
        IEnumerable<ProjectModel> GetAllProjects();

        ProjectModel GetProjectById(int ProjectId);
        IEnumerable<ProjectModel> GetProjectByTrackId(int TrackId);
        IEnumerable<ProjectModel> GetProjectByStudentId(string StudentId);
        ProjectModel AddProjectByTrackId(ProjectModel NewProject,int TrackId, string StudentId,int IntakeId);
        ProjectModel EditProject(ProjectModel EDitedProject);
        void DeleteProject(int ProjectId);

    }
}
