using InShare.Model;
using System.Data.Entity.ModelConfiguration;

namespace InShare.Service.ModelConfig
{
    public class TagConfig : EntityTypeConfiguration<TagEntity>
    {
        public TagConfig()
        {
            //配置表名
            this.ToTable("T_Tag");
            //配置主键
            this.HasKey(t => t.Id);
            //配置主键列名
            this.Property(t => t.Id).HasColumnName("TagId").IsRequired();
            //配置名称为非空，最长为30
            this.Property(t => t.Name).IsRequired().HasMaxLength(30);
        }
    }
}
