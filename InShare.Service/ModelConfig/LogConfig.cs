using InShare.Model;
using System.Data.Entity.ModelConfiguration;

namespace InShare.Service.ModelConfig
{
    public class LogConfig : EntityTypeConfiguration<LogEntity>
    {
        public LogConfig()
        {
            //配置表名
            this.ToTable("T_Log");
            //配置主键
            this.HasKey(l => l.Id);
            //配置列名
            this.Property(l => l.Id).HasColumnName("LogId");
            //配置非空
            this.Property(l => l.Content).IsRequired();
            this.Property(l => l.LogType).IsRequired();
            //配置内容非空，最长为100
            this.Property(l => l.Content).IsRequired().HasMaxLength(100);
            //配置IP
            this.Property(l => l.IP).HasMaxLength(40).IsRequired();
            //配置多对一 （多端配置）
            this.HasRequired(l => l.User).WithMany().HasForeignKey(l => l.UserId).WillCascadeOnDelete(false);
        }
    }
}
