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
    public class ProjectMaterialController : ControllerBase
    {
        private IWebHostEnvironment hostingEnvironment;

        IProjectMaterialManager ProjectMaterialManager;
        IUserProjectManager UserProjectManager;
        public ProjectMaterialController(IUserProjectManager _userProjManag,IProjectMaterialManager _CMM, IWebHostEnvironment hostingEnvironment)
        {
            ProjectMaterialManager = _CMM;
            this.hostingEnvironment = hostingEnvironment;
            UserProjectManager = _userProjManag;

        }

        [HttpGet]
        [Route("api/ProjectMaterial/All/{ProjectId}")]
        public IActionResult GetProjectMaterialByProjectId(int ProjectId)
        {
            if (ProjectId < 1)
                return BadRequest();
            var material = ProjectMaterialManager.GetMaterialByProjectId(ProjectId);
            return Ok(material);
        }

        [HttpPost]
        [Route("api/ProjectMaterial/Upload/{ProjectId}/{Category}")]
        //////////////////////may cause error 
       // public async Task<IActionResult> AddProjectMaterial(int ProjectId, [FromForm(Name = "files")] List<IFormFile> files)
        public async Task<IActionResult> AddProjectMaterial(int ProjectId,string Category)
        {
            var files = new List<IFormFile>();

            for (int i = 0; i < Request.Form.Files?.Count(); i++)
            {
                files.Add(Request.Form.Files[i]);
            }


            var uploader = new Uploader(hostingEnvironment);

            var AllMaterial = new List<ProjectMaterialModel>();

            if (files.Count() == 0 || ProjectId <= 0)
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

                var cm = ProjectMaterialManager.AddMaterial(ProjectId, PathToBeSavedInDB,Category,filename);
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
        [Route("api/ProjectMaterial/Download/{FileName}")]
        public async Task<IActionResult> DownloadProjectMaterial(string FileName)
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
        [Route("api/ProjectMaterial/Download/Id/{MaterialId}")]
        public byte[] DownloadProjectMaterialByMatrialId(int MaterialId)
        {

                var mat = ProjectMaterialManager.GetMaterialByMaterialId(MaterialId);
                string filePath = mat.PathOnServer;// Directory.GetCurrentDirectory() + "\\Uploads\\" + url;


                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

                return fileBytes;
        }


        [HttpDelete]
        [Route("api/ProjectMaterial/DeleteById/{MaterialId}/{ProjectId}")]
        public IActionResult DeleteByMaterialId(int MaterialId,int ProjectId)
        {
            string StudentId = User.Claims.First(c => c.Type == "UserId").Value;
            var colab = UserProjectManager.GetUserProjectIdByStudentIdAndProjectId(StudentId, ProjectId);

            if (MaterialId < 1 )
                return BadRequest();

            if (colab == null)
                return Unauthorized();

            var material = ProjectMaterialManager.GetMaterialByMaterialId(MaterialId);
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

                ProjectMaterialManager.DeleteMaterialByMaterialId(MaterialId);
                return Ok();
            }
        }


        [HttpGet]
        [Route("api/ProjectMaterial/Delete/{FileName}")]

        ///needs to be reviewed

        public IActionResult DeleteProjectMaterial(string FileName)
        {

            var _Path = this.hostingEnvironment.WebRootPath + @"\uploads\" + FileName;

            if (_Path == null)
            {
                return NotFound();
            }
            else
            {

                System.IO.File.Delete(_Path);

                ProjectMaterialManager.DeleteMaterialByPath(_Path);
                return Ok("file deleted");
            }



        }


    }
}