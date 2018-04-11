using InShare.Model;
using System.Collections.Generic;

namespace InShare.IService
{
    public interface ILikeService : IServiceSupport
    {
        /// <summary>
        /// 点赞
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="postId">帖子编号</param>
        /// <returns></returns>
        bool Like(long userId, long postId);

        /// <summary>
        /// 取消点赞
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="postId">帖子编号</param>
        /// <returns></returns>
        bool UnLike(long userId, long postId);
        
        /// <summary>
        /// 根据帖子获取点赞者数量
        /// </summary>
        /// <param name="postId">帖子编号</param>
        /// <returns></returns>
        int GetLikeCount(long postId);

        /// <summary>
        /// 根据帖子编号获取点赞者分页列表
        /// </summary>
        /// <param name="postId">帖子编号</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="pageIndex">页索引</param>
        /// <returns></returns>
        List<long> GetLikePagerList(long postId, int pageSize, int pageIndex);

        /// <summary>
        /// 根据用户编号获取点赞数量
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        int GetLikeCountByUser(long userId);

        /// <summary>
        /// 根据用户编号获取帖子分页列表
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="pageIndex">页索引</param>
        /// <returns></returns>
        List<long> GetLikeByUser(long userId, int pageSize, int pageIndex);
    }
}
