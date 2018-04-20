using InShare.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InShare.Model;
using InShare.Common;

namespace InShare.Service
{
    public class UserService : IUserService
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="fullName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public long Add(string userName, string fullName, string password)
        {
            long id = RandomHelper.CreateId(10);
            string salt = RandomHelper.CreateSalt();
            UserEntity user = new UserEntity
            {
                Id = id,
                UserName = userName,
                FullName = fullName,
                Profile = new UserProfileEntity
                {
                    Id = id,
                    Password = EncryptHelper.MD5Encrypt(id + password + salt),
                    PasswordSalt = salt,
                }
            };
            using (InShareContext db = new InShareContext())
            {
                BaseService<UserEntity> baseService = new BaseService<UserEntity>(db);
                while (true)
                {
                    if (baseService.IsExist(user.Id))
                        user.Id = user.Profile.Id = RandomHelper.CreateId(10);
                    else
                        break;
                }
                db.Users.Add(user);
                db.SaveChanges();
                return id;
            }
        }

        public bool CheckLogin(string userName, string password)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<UserEntity> baseService = new BaseService<UserEntity>(db);
                var user = baseService.GetAll().FirstOrDefault(u => u.UserName == userName);
                if (user != null)
                {
                    if (EncryptHelper.MD5Encrypt(user.Id + password + user.Profile.PasswordSalt) == user.Profile.Password)
                    {
                        user.Profile.LastLoginDateTime = DateTime.Now;
                        db.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// 修改资料
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <param name="fullName"></param>
        /// <param name="bio"></param>
        /// <param name="isPrivate"></param>
        /// <param name="genger"></param>
        /// <param name="profilePic"></param>
        /// <returns></returns>
        public bool Edit(long userId, string userName, string fullName, string bio, bool isPrivate, bool genger, string profilePic)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<UserEntity> baseService = new BaseService<UserEntity>(db);
                var user = baseService.GetById(userId);
                if (user == null)
                {
                    throw new Exception("此用户不存在");
                }
                user.UserName = userName;
                user.FullName = fullName;
                user.Biography = bio;
                user.IsPrivate = isPrivate;
                user.Profile.Gender = genger;
                user.ProfilePic = profilePic;
                user.Profile.LastEditDateTime = DateTime.Now;
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 根据名称获取数量
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public long GetAllUserCount(string name)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<UserEntity> baseService = new BaseService<UserEntity>(db);
                return baseService.GetAll().Where(u => u.FullName.Contains(name) || u.UserName.Contains(name)).Count();
            }
        }

        /// <summary>
        /// 根据用户编号获取用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserEntity GetUserById(long userId)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<UserEntity> baseService = new BaseService<UserEntity>(db);
                return baseService.GetById(userId);
            }
        }

        /// <summary>
        /// 根据名称获取分页列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public List<UserEntity> GetUserByName(string name, int pageSize, int pageIndex)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<UserEntity> baseService = new BaseService<UserEntity>(db);
                return baseService.GetPager<DateTime>(u => u.FullName.Contains(name) || u.UserName.Contains(name), u => u.CreateDateTime, pageSize, pageIndex).ToList();
            }
        }

        /// <summary>
        /// 根据账号获取用户
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public UserEntity GetUserByUserName(string userName)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<UserEntity> baseService = new BaseService<UserEntity>(db);
                return baseService.GetAll().FirstOrDefault(u => u.UserName == userName);
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pwd"></param>
        /// <param name="oldPwd"></param>
        /// <returns></returns>
        public bool UpdatePwd(long userId, string pwd, string oldPwd)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<UserEntity> baseService = new BaseService<UserEntity>(db);
                var user = baseService.GetById(userId);
                if (user == null)
                {
                    throw new Exception("此用户不存在");
                }
                if (user.Profile.Password != EncryptHelper.MD5Encrypt(userId + oldPwd + user.Profile.PasswordSalt))
                {
                    throw new Exception("旧密码不匹配");
                }
                user.Profile.LastPasswordSalt = user.Profile.PasswordSalt;
                user.Profile.LastPassword = user.Profile.Password;
                user.Profile.PasswordSalt = RandomHelper.CreateSalt();
                user.Profile.Password = EncryptHelper.MD5Encrypt(userId + pwd + user.Profile.PasswordSalt);
                user.Profile.LastEditDateTime = DateTime.Now;
                db.SaveChanges();
                return true;
            }
        }
    }
}
