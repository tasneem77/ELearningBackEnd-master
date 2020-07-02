using System;
using System.Collections.Generic;
using onlinelearningbackend.Models;

namespace onlinelearningbackend.Manager
{
   public interface ITaskManager
    {
        TaskClass GetTaskByTaskId(int TaskId);
        //IEnumerable<TaskClass> TaskByInstructorId(int InstructorId);//comment
        IEnumerable<TaskClass> GetTaskByCourseId(int CourseId);
        /// taskclass
        IEnumerable<TaskClass> AddTask(int CourseId, TaskClass NewTask);
        IEnumerable<TaskClass> EditTask(TaskClass EditedTask);
        //void
        int DeleteTaskByTaskId(int TaskId);

    }
}
