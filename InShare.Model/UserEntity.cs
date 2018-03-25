using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 密码密文
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// 上一次密码
        /// </summary>
        public string LastPassword { get; set; }

        /// <summary>
        /// 上一次密码密文
        /// </summary>
        public string LastPasswordSalt { get; set; }

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
        /// 上次登录时间
        /// </summary>
        public DateTime? LastLoginDateTime { get; set; }

        /// <summary>
        /// 用户登陆IP
        /// </summary>
        public string IP { get; set; }
    }
}
