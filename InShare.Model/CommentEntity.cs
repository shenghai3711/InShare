using System.Collections.Generic;

namespace InShare.Model
{
    /// <summary>
    /// 评论实体类
    /// </summary>
    public class CommentEntity : BaseEntity
    {
        public CommentEntity()
        {
            this.Tags = new List<TagEntity>();
        }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 帖子编号
        /// </summary>
        public long PostId { get; set; }

        /// <summary>
        /// 帖子导航属性
        /// </summary>
        public virtual PostEntity Post { get; set; }

        /// <summary>
        /// 作者编号
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 作者导航属性
        /// </summary>
        public virtual UserEntity Owner { get; set; }
        
        /// <summary>
        /// 标记导航属性
        /// </summary>
        public virtual ICollection<TagEntity> Tags { get; set; }
    }
}
