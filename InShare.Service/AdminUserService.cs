using InShare.IService;
using InShare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InShare.Service
{
    public class AdminUserService : IAdminUserService
    {
        public bool Login(string userName, string password)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<AdminUserEntity> baseService = new BaseService<AdminUserEntity>(db);
                return baseService.GetAll().Any(a => a.UserName == userName && a.Password == password);
            }
        }
    }
}
