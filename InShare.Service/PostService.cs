using InShare.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InShare.Model;
using InShare.Common;
using System.Data.Entity;

namespace InShare.Service
{
    public class PostService : IPostService
    {
        /// <summary>
        /// 发帖
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="content"></param>
        /// <param name="imgPath"></param>
        /// <param name="loaction"></param>
        /// <returns></returns>
        public long Add(long userId, string content, string imgPath, string loaction)
        {
            PostEntity post = new PostEntity
            {
                Id = RandomHelper.CreateId(15),
                UserId = userId,
                Caption = content,
                DisplayUrl = imgPath,
                ShortCode = RandomHelper.CreatePostCode(),
                CreateDateTime = DateTime.Now,
                Location = loaction,
            };
            using (InShareContext db = new InShareContext())
            {
                BaseService<TagEntity> tagService = new BaseService<TagEntity>(db);
                foreach (var tagName in StringHelper.GetTagList(content))
                {
                    //ToDo:应该减少查询次数
                    var tag = tagService.GetAll().FirstOrDefault(t => t.Name == tagName);
                    if (tag == null)
                        post.Tags.Add(new TagEntity { Name = tagName });
                    else
                        post.Tags.Add(tag);
                }
                BaseService<PostEntity> baseService = new BaseService<PostEntity>(db);
                while (baseService.GetAll().Any(p => p.ShortCode == post.ShortCode))
                {
                    post.ShortCode = RandomHelper.CreatePostCode();
                }
                while (baseService.GetAll().Any(p => p.Id == post.Id))
                {
                    post.Id = RandomHelper.CreateId(15);
                }
                db.Posts.Add(post);
                db.SaveChanges();
                return post.Id;
            }
        }

        public bool Delete(long postId)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<PostEntity> baseService = new BaseService<PostEntity>(db);
                return baseService.MakeDel(postId);
            }
        }

        public List<PostEntity> GetHomePager(long userId, int pageSize, int pageIndex)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<FollowEntity> followService = new BaseService<FollowEntity>(db);
                var followingList = followService.GetAll().Where(f => f.FollowId == userId).Select(f => f.FollowedId);
                BaseService<PostEntity> baseService = new BaseService<PostEntity>(db);
                return baseService.GetPager<DateTime>(p => followingList.Contains(p.UserId) || p.UserId == userId, p => p.CreateDateTime, pageSize, pageIndex).Include(p => p.Owner).Include(p => p.Tags).AsNoTracking().ToList();
            }
        }

        public int GetPostCount(long userId = 0, bool flag = false)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<PostEntity> baseService = new BaseService<PostEntity>(db);
                if (userId == 0)
                {
                    return baseService.GetAll(flag).Count();
                }
                return baseService.GetAll(flag).Where(p => p.UserId == userId).Count();
            }
        }

        public int GetPostCountByTag(long tagId)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<PostEntity> baseService = new BaseService<PostEntity>(db);
                return baseService.GetAll().Where(p => p.Tags.Any(t => t.Id == tagId)).Count();
            }
        }

        public PostEntity GetPostInfo(long postId)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<PostEntity> baseService = new BaseService<PostEntity>(db);
                return baseService.GetById(postId);
            }
        }

        public PostEntity GetPostInfo(string ShortCode)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<PostEntity> baseService = new BaseService<PostEntity>(db);
                var post = baseService.GetAll().Where(p => p.ShortCode == ShortCode).Include(p => p.Tags).Include(p => p.Owner.Profile);
                return post.FirstOrDefault();
            }
        }

        public List<PostEntity> GetPostPagerList(long userId, int pageSize, int pageIndex)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<PostEntity> baseService = new BaseService<PostEntity>(db);
                return baseService.GetPager<DateTime>(p => p.UserId == userId, p => p.CreateDateTime, pageSize, pageIndex).Include(p => p.Owner).Include(p => p.Tags).ToList();
            }
        }

        public List<PostEntity> GetSearchPager(long tagId, int pageSize, int pageIndex)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<PostEntity> baseService = new BaseService<PostEntity>(db);
                return baseService.GetPager<DateTime>(p => p.Tags.Any(t => t.Id == tagId), p => p.CreateDateTime, pageSize, pageIndex).Include(p => p.Owner).Include(p => p.Tags).ToList();
            }
        }

        public int GetUserCountByTag(long tagId)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<PostEntity> baseService = new BaseService<PostEntity>(db);
                return baseService.GetAll().Where(p => p.Tags.Any(t => t.Id == tagId)).GroupBy(p => p.UserId).Count();
            }
        }


        public List<PostEntity> GetPostPagerList(int pageSize, int pageIndex, bool flag = false)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<PostEntity> baseService = new BaseService<PostEntity>(db);
                return baseService.GetPager<DateTime>(p => 1 == 1, p => p.CreateDateTime, pageSize, pageIndex, flag).Include(p => p.Owner).Include(p => p.Tags).ToList();
            }
        }

        public bool MarkBatchDel(long[] postIds)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<PostEntity> baseService = new BaseService<PostEntity>(db);
                return baseService.MakeDels(postIds);
            }
        }

        public bool MarkDel(long postId)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<PostEntity> baseService = new BaseService<PostEntity>(db);
                return baseService.MakeDel(postId);
            }
        }

        public bool ReMark(long postId)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<PostEntity> baseService = new BaseService<PostEntity>(db);
                return baseService.ReMake(postId);
            }
        }

        public bool ReMarkBatch(long[] postIds)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<PostEntity> baseService = new BaseService<PostEntity>(db);
                return baseService.ReMakes(postIds);
            }
        }

        public bool Edit(long postId, string content, string imgPath, string location)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<PostEntity> baseService = new BaseService<PostEntity>(db);
                var post = baseService.GetById(postId);
                if (post == null)
                    return false;
                post.Caption = content;
                post.DisplayUrl = imgPath;
                post.Location = location;
                db.SaveChanges();
                return true;
            }
        }
    }
}
