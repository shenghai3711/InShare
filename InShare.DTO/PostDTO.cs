using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InShare.DTO
{
    public class PostDTO : BaseDTO
    {
        public string ShortCode { get; set; }
        public string Caption { get; set; }
        public string DisplayUrl { get; set; }
        public string Location { get; set; }
        public DateTime CreateDateTime { get; set; }
        public OwnerDTO Owner { get; set; }
        public List<CommentDTO> CommentList { get; set; }
        public List<OwnerDTO> LikerList { get; set; }
        public List<TagDTO> TagList { get; set; }
    }
}
