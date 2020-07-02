using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using onlinelearningbackend.Manager;
using onlinelearningbackend.Models;

namespace onlinelearningbackend.Controllers
{
    public class MaterialVedioController : Controller
    {
        IMaterialVideoManager db;
        public MaterialVedioController(IMaterialVideoManager _db)
        {
            this.db = _db;
        }
        [HttpGet]
        [Route("api/course/vediomaterial/{id}")]
        public IActionResult MaterialVediotByCourseId(int id)
        {
            var links = db.MaterialVideotByCourseId(id);
            if (links == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(links);
            }
        }
        [HttpPost]
        [Route("api/course/addvediomaterial")]
        //////////////////////may cause error 
        public IActionResult AddMaterialVedio(VideoMaterial k)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("enter the whole data please");
            }

            return Ok(k);

        }
        [HttpPost]
        [Route("api/course/editvediomaterial")]
        //////////////////////may cause error 
        public IActionResult EditMaterialVedio(VideoMaterial k)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("enter the whole data please");
            }

            return Ok(k);

        }
        [HttpGet]
        [Route("api/course/deletevediomaterial/{MId}")]
        public IActionResult DeleteMaterialVedioByMaterialId(int MId)
        {
            db.DeleteMaterialByMaterialId(MId);


            return Ok();

        }
    }
}