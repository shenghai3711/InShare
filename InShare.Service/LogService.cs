using InShare.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InShare.Model;

namespace InShare.Service
{
    public class LogService : ILogService
    {
        public long Add(long userId, int type, string content, string ip = "")
        {
            LogEntity log = new LogEntity
            {
                UserId = userId,
                LogType = type,
                Content = content,
                IP = ip
            };
            using (InShareContext db = new InShareContext())
            {
                db.Logs.Add(log);
                db.SaveChanges();
                return log.Id;
            }
        }

        public int GetLogCount(long userId)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<LogEntity> baseService = new BaseService<LogEntity>(db);
                return baseService.GetAll().Where(l => l.UserId == userId).Count();
            }
        }

        public List<LogEntity> GetLogPagerList(long userId, int pageSize, int pageIndex)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<LogEntity> baseService = new BaseService<LogEntity>(db);
                return baseService.GetAll().Where(l => l.UserId == userId).Skip(pageSize * pageIndex).Take(pageSize).ToList();
            }
        }
    }
}
