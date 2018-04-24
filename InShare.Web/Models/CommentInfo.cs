using InShare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InShare.Web.Models
{
    public class CommentInfo
    {
        public CommentInfo(CommentEntity comment)
        {
            this.Id = comment.Id;
            this.Content = comment.Content;
            this.UserId = comment.UserId;
            this.UserName = comment.Owner.UserName;
            this.UserIcon = comment.Owner.ProfilePic;
            this.PostId = comment.PostId;
            this.ParentId = comment.ParentId.Value;
            this.DateTime = string.Format("{0:R}", comment.CreateDateTime);
        }
        public long Id { get; set; }
        public string Content { get; set; }
        public string UserIcon { get; set; }
        public string UserName { get; set; }
        public long PostId { get; set; }
        public long ParentId { get; set; }
        public long UserId { get; set; }
        public string DateTime { get; set; }
    }
}