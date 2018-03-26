using InShare.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InShare.Service.ModelConfig
{
    public class LogConfig : EntityTypeConfiguration<LogEntity>
    {
        public LogConfig()
        {
            this.ToTable("T_Log");
            this.Property(l => l.Content).IsRequired().IsUnicode(false);
            //关联外键
            this.HasRequired(l => l.User).WithMany().HasForeignKey(l => l.UserId).WillCascadeOnDelete(false);
        }
    }
}
