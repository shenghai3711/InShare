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

            //设置编号名称，不自动增长，
            this.Property(u => u.Id).HasColumnName("UserId").HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            this.Property(u => u.UserName).IsRequired().HasMaxLength(10);
            this.Property(u => u.FullName).IsRequired().HasMaxLength(20);
            this.Property(u => u.Biography).HasMaxLength(100);
        }
    }
}
