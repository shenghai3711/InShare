using InShare.Model;
using System.Collections.Generic;

namespace InShare.IService
{
    public interface IPostService : IServiceSupport
    {
        /// <summary>
        /// 新增帖子
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="content">帖子内容</param>
        /// <param name="imgPath">图片路径</param>
        /// <returns></returns>
        long Add(long userId, string content, string imgPath);

        /// <summary>
        /// 删除帖子
        /// </summary>
        /// <param name="postId">帖子编号</param>
        /// <returns></returns>
        bool Delete(long postId);

        /// <summary>
        /// 获取帖子信息
        /// </summary>
        /// <param name="postId">帖子编号</param>
        /// <returns></returns>
        PostEntity GetPostInfo(long postId);
        
        /// <summary>
        /// 根据用户编号获取帖子数量
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        int GetPostCount(long userId);

        /// <summary>
        /// 根据用户编号获取帖子分页列表
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="pageIndex">页索引</param>
        /// <returns></returns>
        List<PostEntity> GetPostPagerList(long userId, int pageSize, int pageIndex);

        /// <summary>
        /// 根据标记编号获取帖子数量
        /// </summary>
        /// <param name="tagId">标记编号</param>
        /// <returns></returns>
        int GetPostCountByTag(long tagId);

        /// <summary>
        /// 根据标记编号获取帖子分页列表
        /// </summary>
        /// <param name="tagId">标记编号</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="pageIndex">页索引</param>
        /// <returns></returns>
        List<PostEntity> GetPostPagerByTag(long tagId, int pageSize, int pageIndex);
    }
}
