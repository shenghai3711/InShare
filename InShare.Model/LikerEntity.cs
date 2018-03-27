using System.Collections.Generic;

namespace InShare.Model
{
    /// <summary>
    /// liker实体类
    /// </summary>
    public class LikerEntity : BaseEntity
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户导航属性
        /// </summary>
        public virtual UserEntity User { get; set; }

        /// <summary>
        /// 帖子编号
        /// </summary>
        public long PostId { get; set; }

        /// <summary>
        /// 帖子导航属性
        /// </summary>
        public virtual PostEntity Post { get; set; }

    }
}
