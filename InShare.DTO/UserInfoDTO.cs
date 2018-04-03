using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InShare.DTO
{
    public class UserInfoDTO : BaseDTO
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public bool IsPrivate { get; set; }
        public string ProfilePic { get; set; }
        public string Biography { get; set; }
        public List<FollowerDTO> Followers { get; set; }
        public List<FollowerDTO> Followings { get; set; }
        public List<PostDTO> Posts { get; set; }
    }
}
