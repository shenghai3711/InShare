using System;
using System.Collections.Generic;

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
        /// 详细资料导航属性
        /// </summary>
        public virtual UserProfileEntity Profile { get; set; }
    }
}
