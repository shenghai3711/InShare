using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InShare.Web.Models
{
    public class PostInfo
    {
        public long Id { get; set; }
        public string ShortCode { get; set; }
        public string Caption { get; set; }
        public string DisplayUrl { get; set; }
        public string Location { get; set; }
    }
}