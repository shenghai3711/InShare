using InShare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// 根据帖子获取点赞者
        /// </summary>
        /// <param name="postId">帖子编号</param>
        /// <returns></returns>
        List<LikerEntity> GetPostList(long postId);

        /// <summary>
        /// 根据帖子获取点赞者数量
        /// </summary>
        /// <param name="postId">帖子编号</param>
        /// <returns></returns>
        int GetLikeCount(long postId);

        /// <summary>
        /// 根据帖子编号获取点赞分页列表
        /// </summary>
        /// <param name="postId">帖子编号</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="pageIndex">页索引</param>
        /// <returns></returns>
        List<LikerEntity> GetLikePagerList(long postId, int pageSize, int pageIndex);

        /// <summary>
        /// 根据用户编号获取点赞者数量
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        int GetLikeCountByUser(long userId);

        /// <summary>
        /// 根据用户编号获取点赞分页列表
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="pageIndex">页索引</param>
        /// <returns></returns>
        List<LikerEntity> GetLikeByUser(long userId, int pageSize, int pageIndex);
    }
}
