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
 

        public class IntakeManager : IIntakeManager
        {
            ApplicationDbContext db;
            public IntakeManager(ApplicationDbContext _db)
            {
                this.db = _db;
            }
            public List<Intake> GetAllIntakes()
            {
                var Intakes = db.Intakes.FromSqlRaw<Intake>("EXEC dbo.usp_Intakes_SelectAll").ToList<Intake>();
                return Intakes;
            }
            public Intake GetIntakeByName(int IntakeName)
            {
                var Intake = db.Intakes.FromSqlRaw<Intake>("EXEC dbo.usp_Intakes_SelectByName {0}", IntakeName)
                                                                                    .ToList().FirstOrDefault();
                return Intake;
            }
            public Intake EditIntakeById(Intake EditedIntake)
            {
                var Intake = db.Intakes.FromSqlRaw<Intake>("EXEC dbo.usp_Intakes_Update {0},{1}",
                                                            EditedIntake.IntakeId,
                                                            EditedIntake.IntakeName).ToList()
                                                            .FirstOrDefault();
                return Intake;
            }
            public Intake AddIntake(Intake NewIntake)
            {
                var Intake = db.Intakes.FromSqlRaw<Intake>("EXEC  dbo.usp_Intakes_Insert {0},{1}",
                                                               NewIntake.IntakeName,NewIntake.IsActive
                                                               ).ToList().FirstOrDefault();

                return Intake;
            }
            public Intake DeleteIntakeById(int IntakeId)
            {
                var Intake = db.Intakes.FromSqlRaw<Intake>("EXEC dbo.usp_Intakes_Delete {0}", IntakeId)
                                                                                .ToList().FirstOrDefault();
                return Intake;
            }

       public Intake GetIntakeById(int IntakeId)
        {
            var Intake = db.Intakes.FromSqlRaw<Intake>("EXEC dbo.usp_Intakes_Select {0}", IntakeId)
                                                                               .ToList().FirstOrDefault();
            return Intake;
        }




    }
}
