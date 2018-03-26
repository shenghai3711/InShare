using System.Collections.Generic;

namespace InShare.Model
{
    /// <summary>
    /// 帖子实体类
    /// </summary>
    public class PostEntity : BaseEntity
    {
        public PostEntity()
        {
            this.Tags = new List<TagEntity>();
            this.Likers = new List<UserEntity>();
        }

        /// <summary>
        /// Post链接码
        /// </summary>
        public string ShortCode { get; set; }

        /// <summary>
        /// Post标题，内容
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// 图片链接（本地路径）
        /// </summary>
        public string DisplayUrl { get; set; }

        /// <summary>
        /// 是否为视频
        /// </summary>
        public bool IsVideo { get; set; }

        /// <summary>
        /// 作者
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

        /// <summary>
        /// Liker导航属性
        /// </summary>
        public virtual ICollection<UserEntity> Likers { get; set; }
    }
}
