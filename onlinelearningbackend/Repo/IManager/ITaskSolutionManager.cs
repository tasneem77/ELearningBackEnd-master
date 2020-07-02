using onlinelearningbackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.Manager
{
   public interface ITaskSolutionManager
    {
        TaskSolution GetTaskSolutionById(int TaskSolutionId);
       TaskSolution AddTaskByStudent( TaskSolution newTaskSolution);
        IEnumerable<TaskSolution> GetTaskSolutionByStudentId(string StudentId, int TaskId);
        IEnumerable<TaskSolution> GetTaskSolutionByTaskId( int TaskId);

        IEnumerable<TaskSolution> EditTaskSolution(string StudentId, TaskSolution newTaskSolution);
      void DeleteTaskSolutionByTaskId(int TaskSolutionId);
    }
}
