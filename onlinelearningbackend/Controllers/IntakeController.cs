using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using onlinelearningbackend.Models;
using onlinelearningbackend.Repo.IManager;


namespace onlinelearningbackend.Controllers
{

   // [ApiController]

    public class IntakeController : Controller
    {

        IIntakeManager IntakeManager;
        public IntakeController(IIntakeManager _db)
        {
            this.IntakeManager = _db;
        }



        [HttpGet]
        [Route("api/Intake")]
        public IActionResult GetAllIntakes()
        {
            var Intakes = IntakeManager.GetAllIntakes();

            if (Intakes == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(Intakes);
            }

        }


        [HttpGet]
        [Route("api/Intake/intakeid/{IntakeId}")]
        public IActionResult GetIntakeById(int IntakeId)
        {
            if (IntakeId < 1)
                return BadRequest();

            var Intakes = IntakeManager.GetIntakeById(IntakeId);

            if (Intakes == null)
                return NotFound();

            return Ok(Intakes);

        }



        [HttpPost]
        [Route("api/Intake/Add")]
        public IActionResult PostNewIntake([FromForm] Intake NewIntake)
        {
            var IntakeInDb = IntakeManager.AddIntake(NewIntake);


            return Ok(IntakeInDb);

        }
        [HttpPut]
        [Route("api/Intake/Edit/{IntakeId}")]
        public IActionResult PutIntake([FromForm] Intake EditedIntake)
        {
            var IntakeInDb = IntakeManager.EditIntakeById(EditedIntake);

            return Ok(IntakeInDb);

        }
        [HttpGet]
        [Route("api/Intake/Delete/{IntakeId}")]
        public IActionResult DeleteIntake(int IntakeId)
        {
            IntakeManager.DeleteIntakeById(IntakeId);

            return Ok();

        }
    }
}
