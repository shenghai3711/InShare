using InShare.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InShare.Model;

namespace InShare.Service
{
    public class LikeService : ILikeService
    {
        public List<long> GetLikeByUser(long userId, int pageSize, int pageIndex)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<LikerEntity> baseService = new BaseService<LikerEntity>(db);
                return baseService.GetAll().Where(l => l.UserId == userId).Skip(pageSize * pageIndex).Take(pageSize).Select(l => l.PostId).ToList();
            }
        }

        public int GetLikeCount(long postId)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<LikerEntity> baseService = new BaseService<LikerEntity>(db);
                return baseService.GetAll().Where(l => l.PostId == postId).Count();
            }
        }

        public int GetLikeCountByUser(long userId)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<LikerEntity> baseService = new BaseService<LikerEntity>(db);
                return baseService.GetAll().Where(l => l.UserId == userId).Count();
            }
        }

        public List<long> GetLikePagerList(long postId, int pageSize, int pageIndex)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<LikerEntity> baseService = new BaseService<LikerEntity>(db);
                return baseService.GetAll().Where(l => l.PostId == postId).Skip(pageSize * pageIndex).Take(pageSize).Select(p => p.UserId).ToList();
            }
        }

        public bool Like(long userId, long postId)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<LikerEntity> baseService = new BaseService<LikerEntity>(db);
                var like = baseService.GetAll().SingleOrDefault(l => l.UserId == userId && l.PostId == postId);
                if (like == null)
                    db.Likers.Add(new LikerEntity { PostId = postId, UserId = userId });
                else
                    like.IsDeleted = false;
                db.SaveChanges();
                return true;
            }
        }

        public bool UnLike(long userId, long postId)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<LikerEntity> baseService = new BaseService<LikerEntity>(db);
                var like = baseService.GetAll().SingleOrDefault(l => l.UserId == userId && l.PostId == postId);
                if (like != null)
                    like.IsDeleted = true;
                db.SaveChanges();
                return true;
            }
        }
    }
}
