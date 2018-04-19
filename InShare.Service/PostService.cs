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
            string shortCode = RandomHelper.CreatePostCode();
            PostEntity post = new PostEntity
            {
                Id = RandomHelper.CreateId(19),
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
                return baseService.GetPager<DateTime>(p => followingList.Contains(userId), p => p.CreateDateTime, pageSize, pageIndex).ToList();
            }
        }

        public int GetPostCount(long userId)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<PostEntity> baseService = new BaseService<PostEntity>(db);
                return baseService.GetAll().Where(p => p.UserId == userId).Count();
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

        public List<PostEntity> GetPostPagerByTag(long tagId, int pageSize, int pageIndex)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<PostEntity> baseService = new BaseService<PostEntity>(db);
                return baseService.GetPager<DateTime>(p => p.Tags.Any(t => t.Id == tagId), p => p.CreateDateTime, pageSize, pageIndex).ToList();
            }
        }

        public List<PostEntity> GetPostPagerList(long userId, int pageSize, int pageIndex)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<PostEntity> baseService = new BaseService<PostEntity>(db);
                return baseService.GetPager<DateTime>(p => p.UserId == userId, p => p.CreateDateTime, pageSize, pageIndex).ToList();
            }
        }
    }
}
