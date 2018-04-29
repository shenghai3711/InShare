using InShare.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InShare.Service.ModelConfig
{
    public class VerifyConfig : EntityTypeConfiguration<VerifyEntity>
    {
        public VerifyConfig()
        {
            this.ToTable("T_Verify");
            this.Property(v => v.Id).HasColumnName("VerifyId");
            this.Property(v => v.VerifyCode).IsRequired().HasMaxLength(100);
            this.HasRequired(v => v.User).WithMany().HasForeignKey(v => v.UserId).WillCascadeOnDelete(false);
        }
    }
}
