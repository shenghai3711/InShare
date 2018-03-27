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
            //配置内容为必须字段
            this.Property(c => c.Content).IsRequired();
            //多对一配置 （在多端配置）
            this.HasRequired(c => c.Post).WithMany().HasForeignKey(c => c.PostId).WillCascadeOnDelete(false);
            //多对一配置 （在多端配置）
            this.HasRequired(c => c.Owner).WithMany().HasForeignKey(c => c.UserId).WillCascadeOnDelete(false);
            //多对多配置 （在任意多端配置，只配置一次即可）
            this.HasMany(c => c.Tags).WithMany(t => t.Comments).Map(m => m.ToTable("T_CommentTags").MapLeftKey("CommentId").MapRightKey("TagId"));
        }
    }
}
