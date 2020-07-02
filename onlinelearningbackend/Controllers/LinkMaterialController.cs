using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using onlinelearningbackend.Manager;
using onlinelearningbackend.Models;

namespace onlinelearningbackend.Controllers
{
    public class LinkMaterialController : Controller
    {
        IMaterialLinkManager db;
        public LinkMaterialController(IMaterialLinkManager _db)
        {
            this.db = _db;
        }
        [HttpGet("{CourseId}")]
        [Route("api/course/materiallink/{id}")]
 
        public IActionResult MaterialLinktByCourseId(int id)
        {
            var links = db.MaterialLinktByCourseId(id);
            if(links==null)
            {
                return NotFound();
            }
            else
            {
                return Ok(links);
            }
        }
        [HttpPost]
        [Route("api/course/addmaterial")]

        /////////////////////////////////////may cause error 
        public IActionResult AddMaterial(LinkMaterial k)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("enter the whole data please");
            }

            return Ok(k);

        }
        [HttpPost]
        [Route("api/course/editmaterial")]

        /////////////////////////////////////////////may cause error
        public IActionResult EditMaterial(LinkMaterial k)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("enter the whole data please");
            }

            return Ok(k);

        }
        [HttpGet]
        [Route("api/course/deletematerial/{MId}")]
        public IActionResult DeleteMaterialByMaterialId(int MId)
        {
            db.DeleteMaterialByMaterialId(MId);
          

            return Ok();

        }
     
    }
}