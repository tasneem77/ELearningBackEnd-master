using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using onlinelearningbackend.Models;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using System.Net.Http.Headers;
using onlinelearningbackend.Helpers;
using onlinelearningbackend.Repo.IManager;
using Microsoft.AspNetCore.Cors;
using Microsoft.CodeAnalysis.Differencing;
using onlinelearningbackend.Repo.Manager;

namespace onlinelearningbackend.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SignInManager<MyUserModel> _signInManager;
       
        private readonly UserManager<MyUserModel> _userManager;
        private readonly RoleManager<MyRoleModel> _roleManager;
        IIntakeManager db;
        private readonly ApplicationSetting _AppSetting;

        public UserController(
              IIntakeManager _db,
        UserManager<MyUserModel> userManager,
            SignInManager<MyUserModel> signInManager,
            RoleManager<MyRoleModel> roleManager,
            IOptions<ApplicationSetting> AppSetting)
        {
            this.db = _db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _AppSetting = AppSetting.Value;
        }


        [HttpPost]
        [Route("api/user/Register")]
        [EnableCors]
        // Post api/user/register
        public async Task<object> PostRegister([FromForm] UserRegisterModel NewUser)
        {

            //to create role
            // MyRoleModel iden = new MyRoleModel
            //{
            //  Name = "Student"
            //};


            //            IdentityResult res = await _roleManager.CreateAsync(iden);
            ////////////////////
            //to get the intake for the user 
            //var intake = NewUser.IntakeId; 


            ///////////////////////////
            //to get the intakeid
            int intakename=NewUser.IntakeId.GetValueOrDefault();
            var intake = db.GetIntakeByName(intakename);
            if (intake == null)
                return NotFound();
            NewUser.IntakeId = intake.IntakeId;

            /////////////////
            var file = Request?.Form?.Files?.FirstOrDefault();
            bool IsInfoValid = ModelState.IsValid;
            bool IsImageUploaded = file?.Length > 0;

            if (IsInfoValid == false)
            {
                return BadRequest(new { message = "invalid registration info" });
            }
            if (  IsImageUploaded==  false)
                {
                //if user exists
                var checkuser = await _userManager.FindByNameAsync(NewUser.UserName);
                if (checkuser != null)
                    return StatusCode(409, $"User '{NewUser.UserName}' already exists.");
                ///////////
                var user = await _userManager.CreateAsync(NewUser, NewUser.Password);
                // to assign role to user 
                var userdata = await _userManager.FindByNameAsync(NewUser.UserName);
                await _userManager.AddToRoleAsync(userdata, "Student");

                //token part
                var keytoken = Encoding.UTF8.GetBytes(_AppSetting.JWT_Secret);
                var Usertoken = await _userManager.FindByNameAsync(NewUser.UserName);
                await _userManager.AddToRoleAsync(Usertoken, "Student");
                var roletoken = await _userManager.GetRolesAsync(Usertoken);
                if (Usertoken != null && await _userManager.CheckPasswordAsync(Usertoken, NewUser.Password))
                {
                    var Token = TokenHelpers.CreateToken(Usertoken, keytoken);
                    var userrole = roletoken;
                    return Ok(new { Token, userrole });
                }


                return Ok(user);
                }

            var uploadedfilename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var IsImgExtAllowed = FSHelpers.IsImageExtensionAllowed(uploadedfilename);
            if (IsImgExtAllowed == false)
            {
                return BadRequest(new { message = "Image extensions allowed are jPG and PNG only " });

            }
            var dbImagePath = FSHelpers.SaveProfileImage(NewUser, uploadedfilename, file);
            
            NewUser.PrifleImageUrl = dbImagePath;
            //if user exists
            var checkuser2 = await _userManager.FindByNameAsync(NewUser.UserName);
            if (checkuser2 != null)
                return StatusCode(409, $"User '{NewUser.UserName}' already exists.");
            ///////////
            var result = await _userManager.CreateAsync(NewUser, NewUser.Password);
            // to assign role to user 
            var u = await _userManager.FindByNameAsync(NewUser.UserName);
         var r=   await _userManager.AddToRoleAsync(u, "Student");

            //token part
            var key = Encoding.UTF8.GetBytes(_AppSetting.JWT_Secret);
            var User = await _userManager.FindByNameAsync(NewUser.UserName);
            await _userManager.AddToRoleAsync(User, "Student");
            var role = await _userManager.GetRolesAsync(User);
            if (User != null && await _userManager.CheckPasswordAsync(User, NewUser.Password))
            {
                var Token = TokenHelpers.CreateToken(User, key);
                var userrole = role;
                return Ok(new { Token ,userrole});
            }
            else
            {
                return BadRequest(new { message = "Login Info not valid" });
            }

            
        }

        [HttpPost]
        [Route("api/user/Login")]
        //Post api/user/Login
        public async Task<IActionResult> PostLogin([FromForm] UserLoginModel model)
        {
            var IsInfoValid = ModelState.IsValid;
            if(IsInfoValid==false)
            {
                return BadRequest(new { message = "Login Info not valid" });

            }
            var key = Encoding.UTF8.GetBytes(_AppSetting.JWT_Secret);
            var User = await _userManager.FindByNameAsync(model.UserName);
            if(User==null)
            {
                return NotFound();
            }
            if (User.IsActive == false) {
                return NotFound();
            }
            var role = await _userManager.GetRolesAsync(User);
            if (User !=null && await _userManager.CheckPasswordAsync(User, model.Password))
            {
                var Token = TokenHelpers.CreateToken(User, key);
                var userrole = role;
                return Ok(new { Token,userrole });
            }
            else
            {
                return BadRequest(new { message = "Login Info not valid" });
            }
        }

        



        [HttpGet]
        [Route("api/user/Profile")]
        [Authorize]
        // Get api/user/profile 
        public async Task<Object> GetProfile()
        {
            string UserId = User.Claims.First(c => c.Type == "UserId").Value;
            var UserData = await _userManager.FindByIdAsync(UserId);
            return UserData;
        }
        [HttpPost]
        [Route("api/user/Profile")]
      //  [Authorize]
        // Get api/user/profile 
        public async Task<Object> PatchProfile([FromForm]MyUserModel EditedUser)
        {
            var file = Request?.Form?.Files?.FirstOrDefault();
            bool IsInfoValid = ModelState.IsValid;
            bool IsImageUploaded = file?.Length > 0;
           //string UserId = EditedUser.Id;
            string UserId = User.Claims.First(c => c.Type == "UserId").Value;
            var UserInDB = await _userManager.FindByIdAsync(UserId);
            string oldImagePath = UserInDB.PrifleImageUrl;

            UserInDB.UserName = EditedUser.UserName;
            UserInDB.Email = EditedUser.Email;
            UserInDB.City = EditedUser.City;
            UserInDB.PhoneNumber = EditedUser.PhoneNumber;

            if (IsInfoValid==false)
            {
                return BadRequest(new { message = "invalid Edited info" });
            }
            if (IsImageUploaded==false)
            {
                var user = await _userManager.UpdateAsync(UserInDB);
                return Ok(user);
            }

            var uploadedfilename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var dbImagePath = FSHelpers.SaveProfileImage(EditedUser, uploadedfilename, file);

            if (dbImagePath == null)
            {
                return BadRequest(new { message = dbImagePath });
            }

            UserInDB.PrifleImageUrl = dbImagePath;

            var result = await _userManager.UpdateAsync(UserInDB);
            FSHelpers.DeleteOldImage(oldImagePath);

            return Ok(result);
            //return  result;
        }

        ///////////////////////////////////////////////////////////////////
        /////to get student data
        //[HttpGet]
        //[Authorize]
        //[Route("api/student/crsid")]
        //public IActionResult GetStudentByCrsId(int id)
        //{
        //    var stds = db.GetStudentByCrsId(id);
        //    if (stds == null)
        //    { return NotFound(); }
        //    else
        //    {
        //        return Ok(stds);
        //    }
        //}
        //[HttpGet]
       
        //[Route("api/student/trackid/{id}")]
        //public IActionResult GetStudentByTrackId(int id)
        //{
        //    var stds = db.GetStudentByTrackId(id);
        //    if (stds == null)
        //    { return NotFound(); }
        //    else
        //    {
        //        return Ok(stds);
        //    }
        //}
        //[HttpGet]
        //[Route("api/student/stdid/{id}")]
 
        //public IActionResult GetStudentByStdId(int id)
        //{
        //    var stds = db.GetStudentByStdId(id);
        //    if (stds == null)
        //    { return NotFound(); }
        //    else
        //    {
        //        return Ok(stds);
        //    }
        //}

    }
}