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

        public int GetTagCount(string name)
        {
            throw new NotImplementedException();
        }

        public List<TagEntity> GetTagList(string name)
        {
            throw new NotImplementedException();
        }

        public List<TagEntity> GetTagPagerList(string name, int pageSize, int pageIndex)
        {
            throw new NotImplementedException();
        }
    }
}
