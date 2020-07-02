using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using onlinelearningbackend.Repo.IManager;
using onlinelearningbackend.Models;

namespace onlinelearningbackend.Controllers
{
    [ApiController]

    public class BranchController : Controller
    {

        IBranchManager BranchManager;
        public BranchController(IBranchManager _db)
        {
            this.BranchManager = _db;
        }



        [HttpGet]
        [Route("api/branch")]
        public  IActionResult GetAllBranchs()
        {
            var branches = BranchManager.GetAllBranches();

            if (branches == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(branches);
            }

        }
        [HttpGet]
        [Route("api/branch/{BranchId}")]
        public IActionResult GetBranchById(int BranchId)
        { 
            if (BranchId < 1)
                return BadRequest();

            var branches = BranchManager.GetBranchById(BranchId);

            if (branches == null)
                return NotFound();
           
            return Ok(branches);

        }
        [HttpPost]
        [Route("api/branch/Add")]
        public IActionResult PostNewBranch([FromForm] Branch NewBranch )
        {
            var BranchInDb = BranchManager.AddBranch(NewBranch);
        
              
            return Ok(BranchInDb);

        }
        [HttpPut]
        [Route("api/branch/Edit/{BranchId}")]
        public IActionResult PutBranch([FromForm] Branch EditedBranch)
        {
            var BranchInDb = BranchManager.EditBranchById(EditedBranch);

            return Ok(BranchInDb);

        }
        [HttpGet]
        [Route("api/branch/Delete/{BranchId}")]
        public IActionResult DeleteBranch(int BranchId)
        {
            BranchManager.DeleteBranchById(BranchId);

            return Ok();

        }
    }
}