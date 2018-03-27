using InShare.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InShare.Service.ModelConfig
{
    public class UserProfileConfig : EntityTypeConfiguration<UserProfileEntity>
    {
        public UserProfileConfig()
        {
            this.ToTable("T_UserProfile");
            this.Property(u => u.Email).IsRequired();
            this.Property(u => u.Gender).IsRequired();
            this.Property(u => u.LastPassword).HasMaxLength(16);
            this.Property(u => u.LastPasswordSalt).HasMaxLength(10);
            this.Property(u => u.Password).IsRequired().HasMaxLength(16);
            this.Property(u => u.PasswordSalt).IsRequired().HasMaxLength(10);
            this.Property(u => u.PhoneNum).HasMaxLength(11);
        }
    }
}
