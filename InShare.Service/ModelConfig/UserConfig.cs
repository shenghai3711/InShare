using InShare.Model;
using System.Data.Entity.ModelConfiguration;

namespace InShare.Service.ModelConfig
{
    public class UserConfig : EntityTypeConfiguration<UserEntity>
    {
        public UserConfig()
        {
            //设置数据库表名
            this.ToTable("T_User");
            //设置主键,并且与Profile表一对一关联 https://www.cnblogs.com/bidianqing/p/7512992.html
            this.HasKey(u => u.Id).HasRequired(u => u.Profile).WithOptional(p => p.User).WillCascadeOnDelete(false);
            //设置编号名称，不自动增长，
            this.Property(u => u.Id).HasColumnName("UserId").HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            this.Property(u => u.UserName).IsRequired().HasMaxLength(10);
            this.Property(u => u.FullName).IsRequired().HasMaxLength(20);
            this.Property(u => u.Biography).HasMaxLength(100);
            this.Property(u => u.CreateDateTime).IsRequired();
        }
    }
}
