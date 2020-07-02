using onlinelearningbackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.Repo.IManager
{
    public interface IInstructorManager
    {


        List<MyUserModel> GetAllInstrcutors();

        MyUserModel AddInstructor(MyUserModel NewInstructor);
        MyUserModel EditInstructor(MyUserModel EditedInstructor);
        MyUserModel DeleteInstructorById(string InstructorId);
    }
}
