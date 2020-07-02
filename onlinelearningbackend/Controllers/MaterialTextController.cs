using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using onlinelearningbackend.Helpers;
using onlinelearningbackend.Manager;
using onlinelearningbackend.Models;
using onlinelearningbackend.Repo.IManager;

namespace onlinelearningbackend.Controllers
{
    public class MaterialTextController : Controller
    {
        IMaterialTextManager db;
        private IWebHostEnvironment hostingEnvironment;
        ICourseMaterialManger CourseMaterialManager;

        public MaterialTextController(IMaterialTextManager _db,ICourseMaterialManger cmm, IWebHostEnvironment hostingEnvironment)
        {
            this.db = _db;
            this.hostingEnvironment = hostingEnvironment;
            CourseMaterialManager = cmm;
        }

        [HttpPost]
        [Route("api/course/addtextmaterial/{CourseId}")]
       // [Authorize]
        public async Task<IActionResult> AddMaterial( int CourseId)
        {

            var files = new List<IFormFile>();

            for (int i = 0; i < Request.Form.Files?.Count(); i++)
            {
                files.Add(Request.Form.Files[i]);
            }


            var uploader = new Uploader(hostingEnvironment);

            var AllMaterial = new List<TextMaterial>();
            var mat = new TextMaterial();

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
                mat.CourseId = CourseId;
                mat.TextMaterialName = filename;
                mat.URL = PathToBeSavedInDB;

                var cm = db.AddMaterial(mat);
                AllMaterial.Add(cm);
            }
            return Ok(AllMaterial);
        }
        [HttpGet]
        [Route("api/materialtext/{CourseId}")]
        [Authorize]
        [EnableCors]
        public IActionResult MaterialTextByCourseId(int CourseId)
        {
            if (CourseId == 0)
            {
                BadRequest(new { Message = "invalid course id" });
            }
            var material = db.MaterialTextByCourseId(CourseId);
            if (material == null)
            {
                return NotFound();
            }

            return Ok(material);
        }
        [Authorize]
        [HttpGet]
        [Route("api/course/deletetextmaterial/{MId}")]
        public IActionResult DeleteMaterialByMaterialId(int MId)
        {
            db.DeleteMaterialByMaterialId(MId);


            return Ok();

        }
        [HttpGet]
        [Route("api/material/getfile/{materialid}")]
        [Authorize]
        public byte[] DownloadAsync(int materialid)
        {
            var mat = CourseMaterialManager.GetMaterialByMaterialId(materialid);
            string filePath = mat.PathOnServer;// Directory.GetCurrentDirectory() + "\\Uploads\\" + url;
            string fileName = mat.FileName;

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            //var stream = new FileStream(filePath, FileMode.Open);
            //return File(fileBytes, "application/force-download", fileName);
            return fileBytes;

        }
    }
}