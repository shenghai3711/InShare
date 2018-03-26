using InShare.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InShare.Service.ModelConfig
{
    public class PostConfig : EntityTypeConfiguration<PostEntity>
    {
        public PostConfig()
        {
            this.ToTable("T_Post");
            this.HasKey(p => p.Id);
            this.Property(p => p.Id).HasColumnName("PostId").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);

        }
    }
}
