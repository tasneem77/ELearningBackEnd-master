using Microsoft.EntityFrameworkCore;
using onlinelearningbackend.Data;
using onlinelearningbackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.Manager
{
    public class TaskSolutionManager : ITaskSolutionManager
    {
        ApplicationDbContext DB;
        public TaskSolutionManager(ApplicationDbContext _DB)
        {
            DB = _DB;
        }


       public IEnumerable<TaskSolution> GetTaskSolutionByTaskId(int TaskId)
        {
            var TaskSolution = DB.TaskSolutions.FromSqlRaw("EXEC dbo.usp_TaskSolutions_Select_Task_Id {0}",
                                                  TaskId).ToList<TaskSolution>();
            return TaskSolution;
        }

        public TaskSolution AddTaskByStudent( TaskSolution newTaskSolution)
        {
            var TaskSolution = DB.TaskSolutions.FromSqlRaw("EXEC dbo.usp_TaskSolutions_Insert {0},{1},{2},{3},{4}",
                                                        newTaskSolution.MyUserModelId,
                                                        newTaskSolution.TaskId,
                                                        newTaskSolution.CourseId,
                                                        newTaskSolution.TaskSolutionURL,
                                                        newTaskSolution.FileName).ToList<TaskSolution>().FirstOrDefault();
            return TaskSolution;
        }

        public void DeleteTaskSolutionByTaskId(int TaskSolutionId)
        {
            var TaskSolution = DB.Database.ExecuteSqlRaw("EXEC dbo.usp_TaskSolutions_Delete {0}", TaskSolutionId);
           
        }

        public IEnumerable<TaskSolution> EditTaskSolution(string StudentId, TaskSolution newTaskSolution)
        {
            var TaskSolution = DB.TaskSolutions.FromSqlRaw("EXEC dbo.usp_TaskSolutions_Update {0}",
                                                     newTaskSolution.TaskSolutionId,
                                                     StudentId,
                                                     newTaskSolution.TaskSolutionURL).ToList<TaskSolution>();
            return TaskSolution;
        }

        public TaskSolution GetTaskSolutionById(int TaskSolutionId)
        {
            var TaskSolution = DB.TaskSolutions.FromSqlRaw("EXEC dbo.usp_TaskSolutions_Select {0}",
                                                   TaskSolutionId).ToList<TaskSolution>().FirstOrDefault();
            return TaskSolution;
        }
       public IEnumerable<TaskSolution> GetTaskSolutionByStudentId(string StudentId, int TaskId)
        {
            var TaskSolution = DB.TaskSolutions.FromSqlRaw("EXEC dbo.usp_TaskSolutions_Select_By_Student_Task_Id {0},{1}",
                                                    StudentId,
                                                    TaskId).ToList<TaskSolution>();
            return TaskSolution;
        }
    }
}
