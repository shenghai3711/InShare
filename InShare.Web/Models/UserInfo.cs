using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InShare.Web.Models
{
    public class UserInfo
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public int PostCount { get; set; }
        public int FollowingCount { get; set; }
        public int FollowerCount { get; set; }
        public string ProfilePic { get; set; }
        public string FullName { get; set; }
        public string Biography { get; set; }
        public bool IsFollowing { get; set; }
        public string CreateDateTime { get; set; }
    }
}