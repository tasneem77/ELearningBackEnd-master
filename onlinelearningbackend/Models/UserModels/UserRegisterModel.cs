using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.Models
{
    public class UserRegisterModel:MyUserModel
    {
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
