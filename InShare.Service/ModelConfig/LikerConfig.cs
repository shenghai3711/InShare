using InShare.Model;
using System.Data.Entity.ModelConfiguration;

namespace InShare.Service.ModelConfig
{
    public class LikerConfig : EntityTypeConfiguration<LikerEntity>
    {
        public LikerConfig()
        {
            //配置表名
            this.ToTable("T_Liker");
            this.Property(l => l.Id).HasColumnName("LikerId");
            //多对一配置 （在多端配置）
            this.HasRequired(l => l.Post).WithMany().HasForeignKey(l => l.PostId).WillCascadeOnDelete(false);
            //一对一配置
            this.HasRequired(l => l.User).WithMany().HasForeignKey(l => l.UserId).WillCascadeOnDelete(false);
        }
    }
}
