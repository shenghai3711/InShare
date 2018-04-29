using InShare.IService;
using InShare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InShare.Service
{
    public class VerifyService : IVerifyService
    {
        public VerifyEntity Add(long userId)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<VerifyEntity> baseService = new BaseService<VerifyEntity>(db);
                var entity = baseService.GetAll().FirstOrDefault(v => v.UserId == userId && v.IsValid);
                if (entity == null)
                {
                    entity = new VerifyEntity
                    {
                        UserId = userId,
                        IsValid = true,
                        VerifyCode = Guid.NewGuid().ToString("N"),
                    };
                    db.Verifys.Add(entity);
                    db.SaveChanges();
                }
                return entity;
            }
        }

        public VerifyEntity GetVerify(long userId)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<VerifyEntity> baseService = new BaseService<VerifyEntity>(db);
                return baseService.GetAll().FirstOrDefault(v => v.UserId == userId && v.IsValid);
            }
        }

        public bool UpdateValid(long userId)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<VerifyEntity> baseService = new BaseService<VerifyEntity>(db);
                foreach (var item in baseService.GetAll().Where(v => v.UserId == userId && v.IsValid))
                {
                    item.IsValid = false;
                }
                db.SaveChanges();
                return true;
            }
        }
    }
}
