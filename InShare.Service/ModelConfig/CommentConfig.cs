using InShare.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InShare.Service.ModelConfig
{
    public class CommentConfig : EntityTypeConfiguration<CommentEntity>
    {
        public CommentConfig()
        {
            this.ToTable("T_Comment");
        }
    }
}
