using onlinelearningbackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.Repo.IManager
{
    public interface IStudentManager
    {
       
        List<MyUserModel> GetStudentByCrsId(int id);
        List<MyUserModel> GetStudentByTrackId(int id);

        MyUserModel GetStudentByStdId(int id);
    }
}
