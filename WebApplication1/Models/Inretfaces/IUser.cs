using System;

namespace itraTsk4secondTry.Models.Inretfaces
{
    public interface IUser
    {

        public string name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Datetime { get; set; }
        public DateTime GetDate();
    }
}