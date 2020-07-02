using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Magnum.FileSystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using onlinelearningbackend.Helpers;
using onlinelearningbackend.Manager;
using onlinelearningbackend.Models;

namespace onlinelearningbackend.Controllers
{
    [Authorize]
    [ApiController]
    public class TaskSolutionController : ControllerBase
    {
        // ITaskSolutionManager db;
        private IWebHostEnvironment hostingEnvironment;
        ITaskSolutionManager TaskSolutionManager;

        private readonly UserManager<MyUserModel> _userManager;
        public TaskSolutionController(ITaskSolutionManager _TSM, IWebHostEnvironment hostingEnvironment,
        UserManager<MyUserModel> userManager)
        {
            this.TaskSolutionManager = _TSM;
            this.hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("api/course/tasksStd/{tasksolutionId}")]
        public IActionResult GetTaskSolutionById(int TaskSolutionId)
        {
           var taskSolution= TaskSolutionManager.GetTaskSolutionById(TaskSolutionId);
            //after geeting the task solution from DB 
            //exctract the task solution url from the object
            var FilePathOnServer = taskSolution.TaskSolutionURL;
            //return the file in that url path to the user
            return Ok();
        }

        [HttpGet]
        [Route("api/course/tasksStd/names/{TaskId}")]
        public async Task<IActionResult> GetStudentNameUploadedSolution(int TaskId)
        {
            var taskSolutions = TaskSolutionManager.GetTaskSolutionByTaskId(TaskId);
            var studentUploadedSolutions = new List<MyUserModel>();
            foreach (var item in taskSolutions)
            {

                var student = await _userManager.FindByIdAsync(item.MyUserModelId);
                if (student == null)
                    continue;

                if (studentUploadedSolutions.Contains(student))
                    continue;

                studentUploadedSolutions.Add(student);
            }

            return Ok(studentUploadedSolutions);
        }


        [HttpGet]
        [Route("api/course/tasksStd/perstudent/{TaskId}/{StudentId}")]
        public  IActionResult gettasksolutionbytaskidandstudentid(int TaskId,string StudentId)
        {
            var taskSolutions = TaskSolutionManager.GetTaskSolutionByStudentId(StudentId,TaskId);
            

            return Ok(taskSolutions);
        }



        [HttpGet]
        [Route("api/course/TaskSolution/{TaskId}")]
        public IActionResult GetTaskSolutionByTaskId(int TaskId)
        {
            if (TaskId < 1)
                return BadRequest();

            var StudentId = User.Claims.First(c => c.Type == "UserId").Value;
            var taskSolutions = TaskSolutionManager.GetTaskSolutionByStudentId(StudentId, TaskId);


            return Ok(taskSolutions);
        }
            ///////
            ///
            [HttpPost]
        [Route("api/course/tasksStd/{TaskId}/{CourseId}")]
        ///////////////////////not sure of the route
        public async Task<IActionResult> AddTaskSolutionByStudent( int TaskId, int CourseId)
        {
            if (Request.Form.Files.Count() < 1 || TaskId <1 || CourseId <1 )
                return BadRequest();

            var files = new List<IFormFile>();

            for (int i = 0; i < Request.Form.Files?.Count(); i++)
            {
                files.Add(Request.Form.Files[i]);
            }

            var uploader = new Uploader(hostingEnvironment);

            var AllMaterial = new List<TaskSolution>();

            var StudentId = User.Claims.First(c => c.Type == "UserId").Value;
            var NewTaskSolution = new TaskSolution();
            NewTaskSolution.MyUserModelId = StudentId;


            foreach (IFormFile source in files)
            {
                if (source.Length == 0)
                    continue;

                string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.ToString().Trim('"');

                filename = uploader.EnsureCorrectFilename(filename);

                var PathToBeSavedInDB = uploader.GetPathAndFilename(filename);



                using (FileStream output = System.IO.File.Create(PathToBeSavedInDB))
                    await source.CopyToAsync(output);
                NewTaskSolution.TaskId = TaskId;
                NewTaskSolution.CourseId = CourseId;
                NewTaskSolution.FileName = filename;
                NewTaskSolution.TaskSolutionURL = PathToBeSavedInDB;
                

                var cm = TaskSolutionManager.AddTaskByStudent(NewTaskSolution);
                AllMaterial.Add(cm);
            }
            return Ok(AllMaterial);
        }



        [Route("api/course/TaskSolution/{TaskSolutionId}")]
        [HttpDelete]
        public IActionResult DeleteTaskSolutionByTaskId(int TaskSolutionId)
        {

            string StudentId = User.Claims.First(c => c.Type == "UserId").Value;

            if (TaskSolutionId < 1)
                return BadRequest();

            var taskSolution = TaskSolutionManager.GetTaskSolutionById(TaskSolutionId);
            if (taskSolution == null)
                return BadRequest();

            if (taskSolution.MyUserModelId != StudentId)
                return Unauthorized();


            var _Path = taskSolution.TaskSolutionURL;

            var DoesFileExists = System.IO.File.Exists(_Path);

            if (DoesFileExists == false)
            {
                return NotFound();
            }
            else
            {

                System.IO.File.Delete(_Path);

            TaskSolutionManager.DeleteTaskSolutionByTaskId(TaskSolutionId);
                return Ok();
            }

        }


        [HttpPost]
        [Route("api/getfile/{TaskSolutionId}")]
        public HttpResponseMessage GetFileForInstructor(int TaskSolutionId)
        {
            var tasksolution = TaskSolutionManager.GetTaskSolutionById(TaskSolutionId);
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new FileStream(tasksolution.TaskSolutionURL,FileMode.Open);
            response.Content = new StreamContent(stream);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("Resources/Download Files");

            return response;
        }

        [HttpGet]
        [Route("api/TaskSolution/Download/Id/{TaskSolutionId}")]
        public byte[] DownloadTaskSolutionById(int TaskSolutionId)
        {

            var mat = TaskSolutionManager.GetTaskSolutionById(TaskSolutionId);
            string filePath = mat.TaskSolutionURL;// Directory.GetCurrentDirectory() + "\\Uploads\\" + url;


            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return fileBytes;
        }

    }
    }
