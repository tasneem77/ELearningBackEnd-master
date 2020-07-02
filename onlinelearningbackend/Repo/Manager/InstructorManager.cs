using Microsoft.AspNetCore.Identity;
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
    public class InstructorManager : IInstructorManager
    {
   
        ApplicationDbContext db;
        public InstructorManager(ApplicationDbContext _db
        )
        {
            this.db = _db;
        
        }
        public MyUserModel AddInstructor(MyUserModel NewInstructor)
        {
            var instructor = db.Users.FromSqlRaw<MyUserModel>("exec dbo.usp_AspNetUsers_Insert {0},{1},{2},{3},{4},{5},{6},{7}",
                NewInstructor.UserName,
                NewInstructor.Email,
                NewInstructor.PasswordHash,
                NewInstructor.PhoneNumber,
                NewInstructor.BranchId,
                NewInstructor.City,
                NewInstructor.TrackId,
                NewInstructor.IsActive).ToList().FirstOrDefault();
            return instructor;
        }

        public MyUserModel DeleteInstructorById(string InstructorId)
        {
         var j=   db.Users.FromSqlRaw<MyUserModel>($"EXEC dbo.usp_AspNetUsers_Delete '{InstructorId}'").ToList().FirstOrDefault();
            return j;
        }

        public MyUserModel EditInstructor(MyUserModel EditedInstructor)
        {
            var instructor= db.Users.FromSqlRaw<MyUserModel>($"EXEC dbo.usp_Tracks_Update '{EditedInstructor.UserName}'").ToList().FirstOrDefault();
            return instructor;
        }

        public List<MyUserModel> GetAllInstrcutors()
        {
            var instructors = db.Users.FromSqlRaw<MyUserModel>($"EXEC dbo.usp_AspNetUsers_Select ").ToList();
            return instructors;
        }
    }
}
