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
        /// <returns></returns>
        public long Add(long userId, string content, string imgPath)
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
            };
            List<TagEntity> tagList = new List<TagEntity>();
            foreach (var tag in StringHelper.GetTagList(content))
            {
                if (string.IsNullOrEmpty(tag))
                    continue;
                tagList.Add(new TagEntity
                {
                    Name = tag
                });
            }
            //ToDo:先要查询是否已存在标签，否则会重复添加
            post.Tags = tagList;
            using (InShareContext db = new InShareContext())
            {
                //BaseService<PostEntity> baseService = new BaseService<PostEntity>(db);
                db.Posts.Add(post);
                db.SaveChanges();
                return post.Id;
            }
        }

        public bool Delete(long postId)
        {
            throw new NotImplementedException();
        }

        public int GetPostCount(long userId)
        {
            throw new NotImplementedException();
        }

        public int GetPostCountByTag(long tagId)
        {
            throw new NotImplementedException();
        }

        public PostEntity GetPostInfo(long postId)
        {
            throw new NotImplementedException();
        }

        public List<PostEntity> GetPostPagerByTag(long tagId, int pageSize, int pageIndex)
        {
            throw new NotImplementedException();
        }

        public List<PostEntity> GetPostPagerList(long userId, int pageSize, int pageIndex)
        {
            throw new NotImplementedException();
        }
    }
}
