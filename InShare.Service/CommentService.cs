using InShare.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InShare.Model;

namespace InShare.Service
{
    public class CommentService : ICommentService
    {
        public long Add(long userId, long postId, string content)
        {
            CommentEntity comment = new CommentEntity
            {
                PostId = postId,
                UserId = userId,
                Content = content
            };
            using (InShareContext db = new InShareContext())
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return comment.Id;
            }
        }

        public bool DeleteById(long commentId)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<CommentEntity> baseService = new BaseService<CommentEntity>(db);
                return baseService.MakeDel(commentId);
            }
        }

        public bool DeleteByPostId(long postId)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<CommentEntity> baseService = new BaseService<CommentEntity>(db);
                foreach (var comment in baseService.GetAll().Where(c => c.PostId == postId))
                {
                    comment.IsDeleted = true;
                }
                db.SaveChanges();
                return true;
            }
        }

        public int GetCommentCount(long postId)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<CommentEntity> baseService = new BaseService<CommentEntity>(db);
                return baseService.GetAll().Where(c => c.PostId == postId).Count();
            }
        }

        public List<CommentEntity> GetCommentPagerList(long postId, int pageSize, int pageIndex)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<CommentEntity> baseService = new BaseService<CommentEntity>(db);
                return baseService.GetAll().Where(c => c.PostId == postId).Skip(pageSize * pageIndex).Take(pageSize).ToList();
            }
        }
    }
}
