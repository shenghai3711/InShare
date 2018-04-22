using InShare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InShare.Web.Models
{
    public class PostInfo
    {
        public PostInfo(PostEntity post)
        {
            this.Id = post.Id;
            this.ShortCode = post.ShortCode;
            this.Caption = post.Caption;
            this.DisplayUrl = post.DisplayUrl;
            this.Location = post.Location;
            this.UserName = post.Owner.UserName;
            this.UserId = post.UserId;
            this.DateTime = string.Format("{0:R}", post.CreateDateTime);//string.Format("{0:G}",dt)
            this.Tags = post.Tags.Count == 0 ? new List<string>() : post.Tags.Select(t => t.Name).ToList();
        }
        public long Id { get; set; }
        public string ShortCode { get; set; }
        public string Caption { get; set; }
        public string DisplayUrl { get; set; }
        public string Location { get; set; }
        public string UserName { get; set; }
        public long UserId { get; set; }
        public string DateTime { get; set; }
        public List<string> Tags { get; set; }
    }
}