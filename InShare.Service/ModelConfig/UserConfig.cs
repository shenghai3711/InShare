using InShare.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InShare.Service.ModelConfig
{
    public class UserConfig : EntityTypeConfiguration<UserEntity>
    {
        public UserConfig()
        {
            //设置数据库表名
            this.ToTable("T_User");
            //设置主键
            this.HasKey(u => u.Id);
            //设置编号名称，不自动增长，
            this.Property(u => u.Id).HasColumnName("UserId").HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            this.Property(u => u.UserName).IsRequired().HasMaxLength(10).IsUnicode(false);
            this.Property(u => u.FullName).IsRequired().HasMaxLength(20).IsUnicode(false);
            this.Property(u => u.Biography).IsOptional().HasMaxLength(100).IsUnicode(false);
            this.Property(u => u.CreateDateTime).IsRequired();
            this.Property(u => u.Email).IsRequired();
            this.Property(u => u.FollowerIds).IsOptional();
            this.Property(u => u.FollowingIds).IsOptional();
            this.Property(u => u.Gender).IsRequired();
            this.Property(u => u.IP).IsOptional();
            this.Property(u => u.LastLoginDateTime).IsOptional();
            this.Property(u => u.LastPassword).HasMaxLength(16).IsOptional();
            this.Property(u => u.LastPasswordSalt).IsOptional().HasMaxLength(10);
            this.Property(u => u.MutualFollowerIds).IsOptional();
            this.Property(u => u.Password).IsRequired().HasMaxLength(16);
            this.Property(u => u.PasswordSalt).IsRequired().HasMaxLength(10);
            this.Property(u => u.PhoneNum).IsOptional().HasMaxLength(11);
            this.Property(u => u.ProfilePic).IsOptional();
        }
    }
}
