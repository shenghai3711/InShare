using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InShare.DTO
{
    public class CommentDTO : BaseDTO
    {
        public string Content { get; set; }
        public DateTime CreateDateTime { get; set; }
        public OwnerDTO Owner { get; set; }
    }
}
