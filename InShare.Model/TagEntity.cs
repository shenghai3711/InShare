using System.Collections.Generic;

namespace InShare.Model
{
    /// <summary>
    /// 帖子标记实体类
    /// </summary>
    public class TagEntity : BaseEntity
    {
        public TagEntity()
        {
            this.Posts = new List<PostEntity>();
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 帖子导航属性
        /// </summary>
        public virtual ICollection<PostEntity> Posts { get; set; }
    }
}
