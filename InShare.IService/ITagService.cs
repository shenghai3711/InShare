using InShare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InShare.IService
{
    public interface ITagService : IServiceSupport
    {
        /// <summary>
        /// 查询标记列表
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        List<TagEntity> GetTagList(string name);

        /// <summary>
        /// 标记列表数量
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        int GetTagCount(string name);

        /// <summary>
        /// 获取标记分页列表
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="pageIndex">页索引</param>
        /// <returns></returns>
        List<TagEntity> GetTagPagerList(string name, int pageSize, int pageIndex);

    }
}
