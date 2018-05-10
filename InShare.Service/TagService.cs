using InShare.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InShare.Model;

namespace InShare.Service
{
    public class TagService : ITagService
    {

        public List<TagEntity> AddTags(List<string> tagNameList)
        {
            List<TagEntity> tagList = new List<TagEntity>();
            using (InShareContext db = new InShareContext())
            {
                BaseService<TagEntity> baseService = new BaseService<TagEntity>(db);
                //找出没有的添加
                foreach (var tagName in tagNameList)
                {
                    //ToDo:应该可以减少查询是否存在次数
                    if (baseService.GetAll().Any(t => t.Name != tagName))
                    {
                        db.Tags.Add(new TagEntity { Name = tagName });
                    }
                }
                db.SaveChanges();
                return baseService.GetAll().Where(t => tagNameList.Contains(t.Name)).ToList();
            }
        }

        public TagEntity AddTag(string tagName)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<TagEntity> baseService = new BaseService<TagEntity>(db);
                if (!baseService.GetAll().Any(t => t.Name == tagName))
                {
                    db.Tags.Add(new TagEntity { Name = tagName });
                    db.SaveChanges();
                }
                return baseService.GetAll().SingleOrDefault(t => t.Name == tagName);
            }
        }

        public int GetTagCount(string name = "", bool flag = false)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<TagEntity> baseService = new BaseService<TagEntity>(db);
                if (string.IsNullOrEmpty(name))
                {
                    return baseService.GetAll(flag).Count();
                }
                return baseService.GetAll(flag).Where(t => t.Name == name).Count();
            }
        }

        public List<TagEntity> GetTagList(string name)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<TagEntity> baseService = new BaseService<TagEntity>(db);
                return baseService.GetAll().Where(t => t.Name == name).ToList();
            }
        }

        public List<TagEntity> GetTagPagerList(string name, int pageSize, int pageIndex)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<TagEntity> baseService = new BaseService<TagEntity>(db);
                return baseService.GetAll().Where(t => t.Name == name).Skip(pageSize * pageIndex).Take(pageSize).ToList();
            }
        }

        public TagEntity GetTagByName(string name)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<TagEntity> baseService = new BaseService<TagEntity>(db);
                return baseService.GetAll().FirstOrDefault(t => t.Name == name);
            }
        }

        public List<TagEntity> GetTagPagerList(int pageSize, int pageIndex, bool flag = false)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<TagEntity> baseService = new BaseService<TagEntity>(db);
                return baseService.GetPager<DateTime>(p => 1 == 1, p => p.CreateDateTime, pageSize, pageIndex, flag).ToList();
            }
        }

        public bool MarkBatchDel(long[] tagIds)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<TagEntity> baseService = new BaseService<TagEntity>(db);
                return baseService.MakeDels(tagIds);
            }
        }

        public bool MarkDel(long tagId)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<TagEntity> baseService = new BaseService<TagEntity>(db);
                return baseService.MakeDel(tagId);
            }
        }

        public bool ReMark(long tagId)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<TagEntity> baseService = new BaseService<TagEntity>(db);
                return baseService.ReMake(tagId);
            }
        }

        public bool ReMarkBatch(long[] tagIds)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<TagEntity> baseService = new BaseService<TagEntity>(db);
                return baseService.ReMakes(tagIds);
            }
        }

        public bool Edit(long postId, string name)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<TagEntity> baseService = new BaseService<TagEntity>(db);
                var tag = baseService.GetById(postId);
                if (tag == null)
                    return false;
                tag.Name = name;
                db.SaveChanges();
                return true;
            }
        }

        public TagEntity GetTagById(long id)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<TagEntity> baseService = new BaseService<TagEntity>(db);
                return baseService.GetById(id);
            }
        }

        public bool Add(string tagName)
        {
            using (InShareContext db = new InShareContext())
            {
                BaseService<TagEntity> baseService = new BaseService<TagEntity>(db);
                if (!baseService.GetAll().Any(t => t.Name == tagName))
                {
                    db.Tags.Add(new TagEntity { Name = tagName });
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
