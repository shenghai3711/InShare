using InShare.Model;
using System.Data.Entity.ModelConfiguration;

namespace InShare.Service.ModelConfig
{
    public class CommentConfig : EntityTypeConfiguration<CommentEntity>
    {
        public CommentConfig()
        {
            //配置表名
            this.ToTable("T_Comment");
            //配置主键
            this.HasKey(c => c.Id);
            //配置列名
            this.Property(c => c.Id).HasColumnName("CommentId");
            //配置内容为必须字段，最长50
            this.Property(c => c.Content).IsRequired().HasMaxLength(50);
            //多对一配置 （在多端配置）
            this.HasRequired(c => c.Post).WithMany().HasForeignKey(c => c.PostId).WillCascadeOnDelete(false);
            //多对一配置 （在多端配置）
            this.HasRequired(c => c.Owner).WithMany().HasForeignKey(c => c.UserId).WillCascadeOnDelete(false);
        }
    }
}
