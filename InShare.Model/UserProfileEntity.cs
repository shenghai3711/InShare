using System;

namespace InShare.Model
{
    /// <summary>
    /// 用户详细资料实体类
    /// </summary>
    public class UserProfileEntity : BaseEntity
    {
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 密码密文
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public bool Gender { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNum { get; set; }

        /// <summary>
        /// 上一次密码
        /// </summary>
        public string LastPassword { get; set; }

        /// <summary>
        /// 上一次密码密文
        /// </summary>
        public string LastPasswordSalt { get; set; }

        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime? LastLoginDateTime { get; set; }

        /// <summary>
        /// 用户登陆IP
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 导航属性
        /// </summary>
        public virtual UserEntity User { get; set; }
    }
}
