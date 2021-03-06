﻿using System.Collections.Generic;

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
        /// 地理位置
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 转发帖子编号
        /// </summary>
        public long? RePostId { get; set; }

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
    }
}
