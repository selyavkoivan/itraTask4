using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using itraTsk4secondTry.Models.Inretfaces;

namespace itraTsk4secondTry.Models
{
    public class User: IdentityUser
    {
        [Required]
        public bool Status { get; set; }

        [Key]
        public int UserId { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }
        public DateTime? AuthDate { get; set; }

        public User(IUser user)
        {
            this.UserName = user.name;
            this.Status = false;
            this.RegistrationDate = user.GetDate();
            this.Email = user.Email;
        }
        public User()
        {

        }
    }
}
