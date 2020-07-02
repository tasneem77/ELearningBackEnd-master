using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using onlinelearningbackend.Repo.IManager;

namespace onlinelearningbackend.Controllers
{
    public class AdminController : Controller
    {
        IAdminManager admin;
        public AdminController(IAdminManager _admin)
        {
            this.admin = _admin;
        }
        [HttpGet]
        [Route("api/admin/branches")]
        public IActionResult GEtTotalBranches()
        {
            var branch = admin.GetTotakBranches();
            if (branch == null)
                return NotFound();
            return Ok(branch);
        }
        [HttpGet]
        [Route("api/admin/tracks")]
        public IActionResult GEtTotalTracks()
        {
            var track = admin.GetTotakTracks();
            if (track == null)
                return NotFound();
            return Ok(track);
        }
        [HttpGet]
        [Route("api/admin/instructors")]
        public IActionResult GEtTotalInstructors()
        {
            var instructor = admin.GetTotakInstructors();
            if (instructor == null)
                return NotFound();
            return Ok(instructor);
        }
        [HttpGet]
        [Route("api/admin/courses")]
        public IActionResult GEtTotalCourses()
        {
            var course = admin.GetTotakCourses(); 
            if (course == null)
                return NotFound();
            return Ok(course);
        }

        [HttpGet]
        [Route("api/admin/Projects")]
        public IActionResult GEtTotalProjects()
        {
            var project = admin.GetAllProjects();
            if (project == null)
                return NotFound();
            return Ok(project);
        }
    }
}