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
        /// <param name="email"></param>
        /// <param name="userName"></param>
        /// <param name="fullName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public long Add(string email, string userName, string fullName, string password)
        {
            long id = RandomHelper.CreateId(10);
            string salt = RandomHelper.CreateSalt();
            UserEntity user = new UserEntity
            {
                UserName = userName,
                FullName = fullName,
                Profile = new UserProfileEntity
                {
                    Password = EncryptHelper.MD5Encrypt(id + password + salt),
                    PasswordSalt = salt,
                    Email = email
                }
            };
            using (InShareContext db = new InShareContext())
            {
                BaseService<UserEntity> baseService = new BaseService<UserEntity>(db);
                while (true)
                {
                    if (baseService.IsExist(id))
                        id = RandomHelper.CreateId(10);
                    else
                        break;
                }
                user.Id = user.Profile.Id = id;
                db.Users.Add(user);
                db.SaveChanges();
                return id;
            }
        }

        public bool Edit(long userId, string userName, string fullName, string bio, bool isPrivate, bool genger, string email)
        {
            throw new NotImplementedException();
        }

        public long GetAllUserCount(string name)
        {
            throw new NotImplementedException();
        }

        public UserEntity GetUserById(long userId)
        {
            throw new NotImplementedException();
        }

        public List<UserEntity> GetUserByName(string name, int pageSize, int pageIndex)
        {
            throw new NotImplementedException();
        }

        public UserEntity GetUserByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePwd(long userId, string pwd, string oldPwd)
        {
            throw new NotImplementedException();
        }
    }
}
