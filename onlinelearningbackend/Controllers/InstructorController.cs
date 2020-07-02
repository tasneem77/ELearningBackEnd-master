using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using onlinelearningbackend.Models;
using onlinelearningbackend.Repo.IManager;

namespace onlinelearningbackend.Controllers
{
    public class InstructorController : Controller
    {
        private readonly UserManager<MyUserModel> _userManager;
        private readonly RoleManager<MyRoleModel> _roleManager;
        IInstructorManager db;
        public InstructorController(IInstructorManager _db,
                  UserManager<MyUserModel> userManager,
            RoleManager<MyRoleModel> roleManager)
        {
            this.db = _db;
            _userManager = userManager;

            _roleManager = roleManager;
        }

        [HttpGet]
        [Route("api/Instructors")]
        public IActionResult GetAllInstructors()
        {
            var Instructors = db.GetAllInstrcutors();

            if (Instructors == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(Instructors);
            }

        }
       
        [HttpPost]
        [Route("api/instructor/Add")]
        public async Task<ActionResult> PostNewInstructor([FromForm] UserRegisterModel Newinstructor)
        {

            //to create role
            //MyRoleModel iden = new MyRoleModel
            //{
            //    Name = "Instructor"
            //};


            //IdentityResult res = await _roleManager.CreateAsync(iden);
            ////////////////////
            // var user = db.AddInstructor(Newinstructor);
            var user = await _userManager.CreateAsync(Newinstructor, Newinstructor.Password);
            // to assign role to user 
            var userdata = await _userManager.FindByNameAsync(Newinstructor.UserName);
            await _userManager.AddToRoleAsync(userdata, "Instructor");
            return Ok();

        }
        [HttpPut]
        [Route("api/instructor/Edit/{instructorId}")]
        public IActionResult PutInstructor([FromForm] MyUserModel Editedinstructor)
        {
            var instructorInDb = db.EditInstructor(Editedinstructor);

            return Ok(instructorInDb);

        }
        [HttpDelete]
        [Route("api/instructor/Delete/{InstructorId}")]
        public IActionResult DeleteInstructor(string InstructorId)
        {
            
            db.DeleteInstructorById(InstructorId);

            return Ok();

        }
    }
}