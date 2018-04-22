using InShare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InShare.Service
{
    public class BaseService<T> where T : BaseEntity
    {
        private InShareContext context;
        public BaseService(InShareContext _context)
        {
            this.context = _context;
        }

        /// <summary>
        /// 查询所有没被软删除的数据，如果要查询被软删除的数据，则传入参数true
        /// </summary>
        /// <param name="asnotrack">如果只查询不修改数据则传入true</param>
        /// <param name="flag">指明要查询的数据是否被软删除</param>
        /// <returns>返回查询结果集合</returns>
        public IQueryable<T> GetAll(bool asnotrack, bool flag)
        {
            var result = context.Set<T>();
            var result2 = asnotrack ? result.AsNoTracking().Where(e => e.IsDeleted == flag) :
                result.Where(e => e.IsDeleted == flag);
            return result2;
        }

        /// <summary>
        /// 默认查询所有没被软删除的数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll(bool flag = false)
        {
            return context.Set<T>().Where(e => e.IsDeleted == flag);
        }

        /// <summary>
        /// 获取总数据条数
        /// </summary>
        /// <returns></returns>
        public long GetCount()
        {
            return GetAll().LongCount();
        }

        /// <summary>
        /// 分页查询(根据创建时间排序)
        /// </summary>
        /// <param name="startIndex">从第某条开始</param>
        /// <param name="count">要查询的条数</param>
        /// <returns>返回查询的集合</returns>
        public IQueryable<T> GetPageData(int startIndex, int count)
        {
            return GetAll().OrderBy(e => e.CreateDateTime).Skip(startIndex).Take(count);
        }

        /// <summary>
        /// 根据Id获取对象信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(long id)
        {
            return GetAll().Where(e => e.Id == id).SingleOrDefault();
        }

        /// <summary>
        /// 根据Id恢复一条数据
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>恢复成功返回True，否则返回False</returns>
        public bool ReMake(long id)
        {
            var user = GetAll().SingleOrDefault(t => t.Id == id);
            if (user == null) return false;
            user.IsDeleted = false;
            context.SaveChanges();
            return true;
        }

        /// <summary>
        /// 根据Id软删除一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool MakeDel(long id)
        {
            var user = GetAll().SingleOrDefault(t => t.Id == id);
            if (user == null) return false;
            user.IsDeleted = true;
            context.SaveChanges();
            return true;
        }

        /// <summary>
        /// 根据Id判断是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsExist(long id)
        {
            return GetById(id) != null;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="whereLambda">条件Lambda表达式</param>
        /// <param name="orderBy">排序Lambda表达式</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="pageIndex">页索引</param>
        /// <returns></returns>
        public IQueryable<T> GetPager<TKey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderBy, int pageSize, int pageIndex)
        {
            return GetAll().Where(whereLambda).OrderByDescending(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
    }
}
