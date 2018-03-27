using System;

namespace InShare.Model
{
    /// <summary>
    /// 用户实体类
    /// </summary>
    public class UserEntity : BaseEntity
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 用户简介
        /// </summary>
        public string Biography { get; set; }

        /// <summary>
        /// 是否为私有用户
        /// </summary>
        public bool IsPrivate { get; set; }

        /// <summary>
        /// 资料图片
        /// </summary>
        public string ProfilePic { get; set; }
        
        /// <summary>
        /// 关注者编号
        /// 是否关注，数量遍历Id即可
        /// </summary>
        public string FollowerIds { get; set; }

        /// <summary>
        /// 关注者编号
        /// 是否被关注，数量遍历Id即可
        /// </summary>
        public string FollowingIds { get; set; }

        /// <summary>
        /// 共同关注者Id
        /// 数量遍历Id即可
        /// </summary>
        public string MutualFollowerIds { get; set; }

        /// <summary>
        /// 详细资料导航属性
        /// </summary>
        public virtual UserProfileEntity Profile { get; set; }

        /// <summary>
        /// Liker导航属性
        /// </summary>
        public virtual LikerEntity Liker { get; set; }

    }
}
