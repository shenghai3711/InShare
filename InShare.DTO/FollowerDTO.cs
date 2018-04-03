using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InShare.DTO
{
    public class FollowerDTO : BaseDTO
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string ProfilePic { get; set; }
        public bool Followed { get; set; }
        public bool FollowMe { get; set; }
    }
}
