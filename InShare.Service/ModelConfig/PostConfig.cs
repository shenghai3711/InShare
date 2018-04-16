using InShare.Model;
using System.Data.Entity.ModelConfiguration;

namespace InShare.Service.ModelConfig
{
    public class PostConfig : EntityTypeConfiguration<PostEntity>
    {
        public PostConfig()
        {
            //配置表名
            this.ToTable("T_Post");
            //配置主键
            this.HasKey(p => p.Id);
            //配置主键列名
            this.Property(p => p.Id).HasColumnName("PostId").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            //配置图片路径不能为空
            this.Property(p => p.DisplayUrl).IsRequired().HasMaxLength(100);
            //配置地理位置
            this.Property(p => p.Location).HasMaxLength(50);
            //配置内容最长为100
            this.Property(p => p.Caption).HasMaxLength(100);
            //配置最大长度
            this.Property(p => p.ShortCode).IsRequired().HasMaxLength(20);
            //多对一配置 （在多端配置）
            this.HasRequired(p => p.Owner).WithMany().HasForeignKey(p => p.UserId).WillCascadeOnDelete(false);
            //多对多配置 （在任意多端配置，只配置一次即可）
            this.HasMany(p => p.Tags).WithMany(t => t.Posts).Map(m => m.ToTable("T_PostTags").MapLeftKey("PostId").MapRightKey("TagId"));
        }
    }
}
