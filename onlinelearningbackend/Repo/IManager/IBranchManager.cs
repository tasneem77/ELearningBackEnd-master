using onlinelearningbackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.Repo.IManager
{
    public interface IBranchManager
    {
        List<Branch> GetAllBranches();
        Branch GetBranchById(int BranchId);
        Branch AddBranch(Branch NewBranch);
        Branch EditBranchById(Branch EditedBranch);
        Branch DeleteBranchById(int BranchId);
    }
}
