using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using onlinelearningbackend.Data;
//using Microsoft.EntityFrameworkCore;
using onlinelearningbackend.Models;

namespace onlinelearningbackend.Manager
{
    public class taskManager : ITaskManager
    {
        ApplicationDbContext DB;
        public taskManager(ApplicationDbContext _DB)
        {
            DB = _DB;
        }
        public IEnumerable<TaskClass> AddTask(int CourseId, TaskClass NewTask)
        {
            var Tasks = DB.Tasks.FromSqlRaw("EXEC dbo.usp_Tasks_Insert {0},{1},{2},{3}"
                                            , NewTask.TaskName
                                            ,CourseId
                                            , NewTask.Description
                                            , NewTask.DueDate

                                            ).ToList<TaskClass>();
            return Tasks;

            
        }

        public int DeleteTaskByTaskId(int TaskId)
        {
            var Tasks = DB.Database.ExecuteSqlCommand("EXEC dbo.usp_Tasks_Delete {0}", TaskId);
            return 1;
        }

        public IEnumerable<TaskClass> EditTask(TaskClass EditedTask)
        {
            var Tasks = DB.Tasks.FromSqlRaw("EXEC dbo.usp_Tasks_Update {0},{1},{2},{3}"
                                           , EditedTask.TaskId
                                           , EditedTask.TaskName
                                           , EditedTask.Description
                                           , EditedTask.DueDate

                                           ).ToList<TaskClass>();
            return Tasks;
        }

        public IEnumerable<TaskClass> GetTaskByCourseId(int CourseId)
        {
            var Tasks = DB.Tasks.FromSqlRaw("EXEC dbo.usp_Tasks_Select_By_Course_Id {0}", CourseId).ToList<TaskClass>();
            return Tasks;
        }

        //public IEnumerable<TaskClass> TaskByInstructorId(int InstructorId)
        //{
        //    var Tasks = DB.Tasks.FromSqlRaw("EXEC dbo.usp_Tasks_Select_by_Student_Id {0}", InstructorId).ToList<TaskClass>();
        //    return Tasks;
        //}

        public TaskClass GetTaskByTaskId(int TaskId)
        {
            var Tasks = DB.Tasks.FromSqlRaw("EXEC dbo.usp_Tasks_Select {0}", TaskId).ToList<TaskClass>().FirstOrDefault();
            return Tasks;
        }
    }
}