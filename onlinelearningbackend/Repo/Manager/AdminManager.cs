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
    public class AdminManager : IAdminManager
    {
        ApplicationDbContext db;
        public AdminManager(ApplicationDbContext _db)
        {
            this.db = _db;
        }

        public int GetTotakBranches()
        {

            //  var x=db.Database.ExecuteSqlRaw("EXEC dbo.usp_Number_Branch")
            var total = db.Branches.FromSqlRaw<Branch>("EXEC dbo.usp_Number_Branch").ToList().Count();
                //("EXEC dbo.usp_Number_Branch").Single();
            return total;
        }

        public int GetTotakCourses()
        {
            var total = db.Courses.FromSqlRaw("EXEC dbo.usp_Courses_SelectAll").ToList<Course>().Count();
           
            return total;
        }

        public int GetTotakInstructors()
        {
            
            var total = db.Users.FromSqlRaw<MyUserModel>(" EXEC dbo.usp_Number_Instructor").ToList().Count();
            return total;
        }

        public int GetTotakTracks()
        {
   
            var total = db.Tracks.FromSqlRaw<Track>(" EXEC dbo.usp_Number_Track").ToList().Count();
            return total;
        }
        public int GetTotalIntakes()
        {

            var total = db.Intakes.FromSqlRaw<Intake>(" EXEC dbo.usp_Number_Intake").ToList().Count();
            return total;
        }
        public int GetAllProjects()

        {
            var projects = db.ProjectModels.FromSqlRaw("EXEC dbo.usp_ProjectModels_Select").ToList<ProjectModel>().Count();
            return projects;
        }
    }
}
