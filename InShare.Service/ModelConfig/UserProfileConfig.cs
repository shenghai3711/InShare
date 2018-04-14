using InShare.Model;
using System.Data.Entity.ModelConfiguration;

namespace InShare.Service.ModelConfig
{
    public class UserProfileConfig : EntityTypeConfiguration<UserProfileEntity>
    {
        public UserProfileConfig()
        {
            this.ToTable("T_UserProfile");
            //设置主键,并且与Profile表一对一关联 https://www.cnblogs.com/bidianqing/p/7512992.html
            this.HasKey(p => p.Id).HasRequired(p => p.User).WithOptional(u => u.Profile).WillCascadeOnDelete(false);

            this.Property(p => p.Id).HasColumnName("ProfileId").HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            this.Property(p => p.Email).HasMaxLength(50);
            this.Property(p => p.LastPassword).HasMaxLength(32);
            this.Property(p => p.LastPasswordSalt).HasMaxLength(10);
            this.Property(p => p.Password).IsRequired().HasMaxLength(32);
            this.Property(p => p.PasswordSalt).IsRequired().HasMaxLength(10);
            this.Property(p => p.PhoneNum).HasMaxLength(11);
            this.Property(p => p.IP).HasMaxLength(15);
        }
    }
}
