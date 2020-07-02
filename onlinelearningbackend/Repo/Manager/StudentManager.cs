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
    public class StudentManager:IStudentManager
    {
        ApplicationDbContext db;
        public StudentManager(ApplicationDbContext _db)
        {
            this.db = _db;
        }
     
 

       public List<MyUserModel> GetStudentByCrsId(int id)
        {
            var users = db.Users.FromSqlRaw<MyUserModel>($"EXEC dbo.usp_StudentS_CrsId {id}").ToList<MyUserModel>();

            return users;
        }
        public List<MyUserModel> GetStudentByTrackId(int id)
        {
            var users = db.Users.FromSqlRaw<MyUserModel>($"EXEC dbo.usp_StudentS_TrackId {id}").ToList<MyUserModel>();

            return users;
        }

        public MyUserModel GetStudentByStdId(int id)
        {
            var user = db.Users.FromSqlRaw<MyUserModel>($"EXEC dbo.usp_Students_StdId {id}");
            MyUserModel u = user as MyUserModel;
            return u;
        }

    }
}
