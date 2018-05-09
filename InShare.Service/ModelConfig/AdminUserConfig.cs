using InShare.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InShare.Service.ModelConfig
{
    public class AdminUserConfig : EntityTypeConfiguration<AdminUserEntity>
    {
        public AdminUserConfig()
        {
            this.ToTable("T_AdminUser");
            this.Property(a => a.UserName).IsRequired().HasMaxLength(32);
            this.Property(a => a.Password).IsRequired().HasMaxLength(50);
        }
    }
}
