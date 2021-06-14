﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using itraTsk4secondTry.Models.Inretfaces;

namespace itraTsk4secondTry.Models
{
    public class UserReg:IUser
    {
        [StringLength(20)]
        public string name { get; set; }

        public string Email { get; set; }

        [StringLength(20)]
        public string Password { get; set; }
        
        public string PasswordConfirm { get; set; }

        public bool CheckPasswords()
        {
            return Password != PasswordConfirm;
        }

        public string Datetime { get; set; }

        public DateTime GetDate() { return Convert.ToDateTime(Datetime);}
    }
}
