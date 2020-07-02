using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using onlinelearningbackend.DAL;
using onlinelearningbackend.Helpers;
using onlinelearningbackend.Models;
using onlinelearningbackend.Repo.IManager;

namespace onlinelearningbackend.Controllers
{
    [ApiController]
    [Authorize]
    public class CourseMaterialController : ControllerBase
    {
        private IWebHostEnvironment hostingEnvironment;

        ICourseMaterialManger CourseMaterialManager;
       // IUserProjectManager UserProjectManager;
        public CourseMaterialController( ICourseMaterialManger _CMM, IWebHostEnvironment hostingEnvironment)
        {
            CourseMaterialManager = _CMM;
            this.hostingEnvironment = hostingEnvironment;
            //UserProjectManager = _userProjManag;

        }

        [HttpGet]
        [Route("api/CourseMaterial/All/{CourseId}")]
        public IActionResult GetCourseMaterialByCourseId(int CourseId)
        {
            if (CourseId < 1)
                return BadRequest();
            var material = CourseMaterialManager.GetMaterialByCourseId(CourseId);
            return Ok(material);
        }

        [HttpPost]
        [Route("api/CourseMaterial/Upload/{CourseId}/{Category}")]
        //////////////////////may cause error 
        // public async Task<IActionResult> AddCourseMaterial(int CourseId, [FromForm(Name = "files")] List<IFormFile> files)
        public async Task<IActionResult> AddCourseMaterial(int CourseId, string Category)
        {
            var files = new List<IFormFile>();

            for (int i = 0; i < Request.Form.Files?.Count(); i++)
            {
                files.Add(Request.Form.Files[i]);
            }


            var uploader = new Uploader(hostingEnvironment);

            var AllMaterial = new List<CourseMaterialModel>();

            if (files.Count() == 0 || CourseId <= 0)
                return BadRequest();
            foreach (IFormFile source in files)
            {
                if (source.Length == 0)
                    continue;
                //get uploaded file name as in the user pc
                string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.ToString().Trim('"');

                filename = uploader.EnsureCorrectFilename(filename);

                var PathToBeSavedInDB = uploader.GetPathAndFilename(filename);

                using (FileStream output = System.IO.File.Create(PathToBeSavedInDB))
                    await source.CopyToAsync(output);

                var cm = CourseMaterialManager.AddMaterial(CourseId, PathToBeSavedInDB, Category, filename);
                AllMaterial.Add(cm);
            }
            return Ok(AllMaterial);
        }


        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt","text/plain" },
                {".pdf","application/pdf" },
                {".jpg","image/jpeg" },
                {".jpeg","image/jpeg" },
                {".png","image/png" },
                {".doc","application/msword" }
            };
        }

        [HttpGet]
        [Route("api/CourseMaterial/Download/{FileName}")]
        public async Task<IActionResult> DownloadCourseMaterial(string FileName)
        {

            var _Path = this.hostingEnvironment.WebRootPath + @"\uploads\" + FileName;
            var memory = new MemoryStream();
            using (var stream = new FileStream(_Path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            var ext = Path.GetExtension(_Path).ToLowerInvariant();
            return File(memory, GetMimeTypes()[ext], Path.GetFileName(_Path));
        }

        [HttpGet]
        [Route("api/CourseMaterial/Download/Id/{MaterialId}")]
        public  byte[] DownloadCourseMaterialByMatrialId(int MaterialId)
        {
            var mat = CourseMaterialManager.GetMaterialByMaterialId(MaterialId);
            string filePath = mat.PathOnServer;

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return fileBytes;
        }


        [HttpDelete]
        [Route("api/CourseMaterial/DeleteById/{MaterialId}/{CourseId}")]
        public IActionResult DeleteByMaterialId(int MaterialId, int CourseId)
        {

            if (MaterialId < 1)
                return BadRequest();

            var material = CourseMaterialManager.GetMaterialByMaterialId(MaterialId);
            if (material == null)
                return BadRequest();

            var _Path = material.PathOnServer;

            if (_Path == null)
            {
                return NotFound();
            }
            else
            {

                System.IO.File.Delete(_Path);

                CourseMaterialManager.DeleteMaterialByMaterialId(MaterialId);
                return Ok();
            }
        }


        [HttpGet]
        [Route("api/CourseMaterial/Delete/{FileName}")]

        ///needs to be reviewed

        public IActionResult DeleteCourseMaterial(string FileName)
        {

            var _Path = this.hostingEnvironment.WebRootPath + @"\uploads\" + FileName;

            if (_Path == null)
            {
                return NotFound();
            }
            else
            {

                System.IO.File.Delete(_Path);

                CourseMaterialManager.DeleteMaterialByPath(_Path);
                return Ok("file deleted");
            }



        }


    }
}