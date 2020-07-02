using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using onlinelearningbackend.DAL;
using onlinelearningbackend.Models;

namespace onlinelearningbackend.Controllers
{
    [Authorize]
    [ApiController]
    public class CourseController : ControllerBase
    {
        ICourseManager _CourseManager;
        private readonly UserManager<MyUserModel> _userManager;
        public CourseController(UserManager<MyUserModel> userManager, ICourseManager CM)
        {
            _CourseManager = CM;
            _userManager = userManager;

        }
        // GET: api/Course
        [HttpGet]
        [Route("api/Course/ByCourseId/{CourseId}")]
        public IActionResult GetByCourseId(int CourseId)
        {

            // string UserId = User.Claims.First(c => c.Type == "UserId").Value;
            var c = _CourseManager.CoursesByCourseId(CourseId);
            return Ok(c);
        }
        // GET: api/Course
        [HttpGet("{StudentId}")]
        [Route("api/Course/ByStudentId/{StudentId}")]
        public IActionResult GetByStudentId(string StudentId)
        {

            string UserId = User.Claims.First(c => c.Type == "UserId").Value;
            var c = _CourseManager.CoursesByStudentId(StudentId);

            return Ok(new { c });
        }
        // GET: api/Course
        [HttpGet]
        [Route("api/Course/ByInstructorId")]
        public IActionResult GetByInstructorId()
        {
            string UserId = User.Claims.First(c => c.Type == "UserId").Value;
            var c = _CourseManager.GetAllCourses();
            var courses = new List<object>();
            foreach (var item in c)
            {
                var courseuser = _CourseManager.IsUserEnrolled(item.CourseId, UserId);
                if (courseuser == null)
                {
                    continue;
                }
                else
                {
                    courses.Add(new
                    {
                        item.CourseId,
                        item.CourseMaterialModels,
                        item.CourseMyUserModels,
                        item.CourseName,
                        item.Description,
                        item.EnrollmentKey,
                        IsEnrolled = true,
                        item.IntervalInDays,
                        item.IsActive,
                        item.TrackId
                    });
                }

            }
            return Ok(courses);
        }
        // GET: api/Course
        [HttpGet("{TrackId}")]
        [Route("api/Course/ByTrackId/{TrackId}")]
        public IActionResult GetByTrackId(int TrackId)
        {

            var c = _CourseManager.CoursesByTrackId(TrackId);
            return Ok(new { c });
        }



        [Route("api/Course/Add")]
        // POST: api/Course
        [HttpPost]
        public async Task<IActionResult> PostAddCourse([FromBody] Course Course)
        {
            if (ModelState.IsValid == false)
                return BadRequest(new { message = "invalid Course info" });

            string UserId = User.Claims.First(c => c.Type == "UserId").Value;
            var instructor = await _userManager.FindByIdAsync(UserId);
            if (instructor == null)
                return BadRequest();
            int TrackId = (int)instructor.TrackId;
            /// using user manger to get user and track id
            var c = _CourseManager.AddCourse(Course, UserId, TrackId);
            // var c = _CourseManager.AddCourse(Course);

            return Ok(new { c });
        }

        [HttpGet]
        [Route("api/Course/GetCourses")]
        public IActionResult GetAllCourses()
        {
            string UserId = User.Claims.First(c => c.Type == "UserId").Value;
            var c = _CourseManager.GetAllCourses();
            var courses = new List<object>();
            foreach (var item in c)
            {
                var courseuser = _CourseManager.IsUserEnrolled(item.CourseId, UserId);
                if (courseuser == null)
                {

                    courses.Add(new
                    {
                        item.CourseId,
                        item.CourseMaterialModels,
                        item.CourseMyUserModels,
                        item.CourseName,
                        item.Description,
                        item.EnrollmentKey,
                        IsEnrolled = false,
                        item.IntervalInDays,
                        item.IsActive,
                        item.TrackId
                    });
                }
                else
                {
                    courses.Add(new
                    {
                        item.CourseId,
                        item.CourseMaterialModels,
                        item.CourseMyUserModels,
                        item.CourseName,
                        item.Description,
                        item.EnrollmentKey,
                        IsEnrolled = true,
                        item.IntervalInDays,
                        item.IsActive,
                        item.TrackId
                    });
                }

            }
            return Ok(courses);
        }


        [Route("api/Course/Edit")]
        // PUT: api/Course/5
        [HttpPut]
        public async Task<IActionResult> PutEditCourse([FromBody] Course EditedCourse)
        {
            if (ModelState.IsValid == false)
                return BadRequest("invalid infor");
            string UserId = User.Claims.First(c => c.Type == "UserId").Value;
            var instructor = await _userManager.FindByIdAsync(UserId);
            if (instructor == null)
                return BadRequest();
            int TrackId = (int)instructor.TrackId;
            var c = _CourseManager.EditCourse(EditedCourse, UserId, TrackId);

            if (c == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(c);
            }
        }


        [Route("api/Course/Delete/{CourseId}")]
        // PUT: api/Course/5
        [HttpDelete]
        public IActionResult DeleteCourse(int CourseId)
        {
            _CourseManager.DeleteCoursesByCourseId(CourseId);
            return Ok();
        }


        [Route("api/Course/Enroll/{CourseId}/{EnrollmentKey}")]
        // PUT: api/Course/5
        [HttpGet]
        public IActionResult PostEnrollStudent(int CourseId, string EnrollmentKey)
        {
            string UserId = User.Claims.First(c => c.Type == "UserId").Value;

            if (CourseId < 0)
                return BadRequest();

            var course = _CourseManager.CoursesByCourseId(CourseId);

            if (course.EnrollmentKey != EnrollmentKey)
                return Ok(new { res = "wrong key" });


            _CourseManager.EnrollStudentInCourse(CourseId, UserId);

            return Ok(new { res = "enrolled" });
        }
        [HttpGet]
        [Route("api/Course/GetExploreCourses")]
        public IActionResult GetAllExploreCourses()
        {
            var c = _CourseManager.GetAllCourses();
            return Ok(c);
        }

    }
}