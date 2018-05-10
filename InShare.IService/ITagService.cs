using InShare.Model;
using System.Collections.Generic;

namespace InShare.IService
{
    public interface ITagService : IServiceSupport
    {

        /// <summary>
        /// 新增标记
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool Add(string name);

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
        int GetTagCount(string name = "", bool flag = false);

        /// <summary>
        /// 获取标记分页列表
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="pageIndex">页索引</param>
        /// <returns></returns>
        List<TagEntity> GetTagPagerList(string name, int pageSize, int pageIndex);

        /// <summary>
        /// 根据名称获取标签实体
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        TagEntity GetTagByName(string name);

        #region 后台操作

        List<TagEntity> GetTagPagerList(int pageSize, int pageIndex, bool flag = false);

        bool MarkBatchDel(long[] tagIds);

        bool MarkDel(long tagId);

        bool ReMark(long tagId);

        bool ReMarkBatch(long[] tagIds);

        bool Edit(long postId, string name);

        TagEntity GetTagById(long id);

        #endregion
    }
}
