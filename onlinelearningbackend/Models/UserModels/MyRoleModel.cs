using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.Models
{
    public class MyRoleModel:IdentityRole
    {
        public MyRoleModel():base()
        {

        }
        public MyRoleModel(string RoleName):base(RoleName)
        {

        }
        public MyRoleModel(string RoleName, string _RoleDescriptionn):base(RoleName)
        {
            RoleDescription = _RoleDescriptionn;
        }
        public string RoleDescription { get; set; }
    }
}
