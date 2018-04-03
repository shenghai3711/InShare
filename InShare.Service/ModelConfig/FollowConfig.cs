using InShare.Model;
using System.Data.Entity.ModelConfiguration;

namespace InShare.Service.ModelConfig
{
    public class FollowConfig : EntityTypeConfiguration<FollowEntity>
    {
        public FollowConfig()
        {
            this.ToTable("T_Follow");
            this.Property(f => f.FollowId).IsRequired().HasColumnName("FollowId");
            this.Property(f => f.FollowedId).IsRequired().HasColumnName("FollowedId");
            //多对一配置 （在多端配置）
            //一个用户可以关注多个用户
            //this.HasRequired(f => f.Follower).WithMany().HasForeignKey(f => f.FollowId).WillCascadeOnDelete(false);
            //一个用户可以被过个用户关注
            //this.HasRequired(f => f.Followeder).WithMany().HasForeignKey(f => f.FollowedId).WillCascadeOnDelete(false);
        }
    }
}
