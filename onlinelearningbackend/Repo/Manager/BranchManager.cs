using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using onlinelearningbackend.Data;
using onlinelearningbackend.Models;
using onlinelearningbackend.Repo.IManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.Repo.Manager
{
    public class BranchManager:IBranchManager
    {
        ApplicationDbContext db;
        public BranchManager(ApplicationDbContext _db)
        {
            this.db = _db;
        }
        public List<Branch> GetAllBranches()
        {
            var branches = db.Branches.FromSqlRaw<Branch>("EXEC dbo.usp_Branches_Select").ToList<Branch>();
            return branches;
        }
        public Branch GetBranchById(int BranchId)
        {
            var branch = db.Branches.FromSqlRaw<Branch>("EXEC dbo.usp_Branches_Select_By_Id {0}", BranchId).ToList().FirstOrDefault();
            return branch;
        }
        public Branch EditBranchById(Branch EditedBranch)
        {
            var branch = db.Branches.FromSqlRaw<Branch>("EXEC dbo.usp_Branches_Update {0},{1},{2},{3},{4}",
                                                        EditedBranch.BranchId,
                                                        EditedBranch.BranchName,
                                                        EditedBranch.BranchEmail,
                                                        EditedBranch.BranchTelephone,
                                                        EditedBranch.IsActive).ToList()
                                                        .FirstOrDefault();
            return branch;
        }
        public Branch AddBranch( Branch NewBranch)
        {
            var branch = db.Branches.FromSqlRaw<Branch>("EXEC  dbo.usp_Branches_Insert {0},{1},{2},{3}",
                                                           NewBranch.BranchName,
                                                           NewBranch.BranchEmail,
                                                           NewBranch.BranchTelephone,
                                                           NewBranch.IsActive).ToList().FirstOrDefault();
            
            return branch;
        }
        public Branch DeleteBranchById(int BranchId)
        {
            var branch=db.Branches.FromSqlRaw<Branch>("EXEC dbo.usp_Branches_Delete {0}", BranchId).ToList().FirstOrDefault();
            return branch;
        }

       
    }
}
