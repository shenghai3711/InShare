using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InShare.DTO
{
    public class UserProfileDTO : BaseDTO
    {
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public bool Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNum { get; set; }
        public string LastPassword { get; set; }
        public string LastPasswordSalt { get; set; }
        public DateTime? LastLoginDateTime { get; set; }
        public string IP { get; set; }
        public DateTime? LastEditDateTime { get; set; }
    }
}
