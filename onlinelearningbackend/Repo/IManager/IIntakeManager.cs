using onlinelearningbackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.Repo.IManager
{
    public interface IIntakeManager
    {
        List<Intake> GetAllIntakes();
        Intake GetIntakeByName(int IntakeName);
        Intake GetIntakeById(int IntakeId);
        Intake AddIntake(Intake NewIntake);
        Intake EditIntakeById(Intake EditedIntake);
        Intake DeleteIntakeById(int IntakeId);
    }
}
