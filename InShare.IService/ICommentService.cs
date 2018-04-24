using InShare.Model;
using System.Collections.Generic;

namespace InShare.IService
{
    public interface ICommentService : IServiceSupport
    {
        /// <summary>
        /// 新增评论
        /// </summary>
        /// <param name="userId">评论者编号</param>
        /// <param name="postId">帖子编号</param>
        /// <param name="content">评论内容</param>
        /// <param name="parentId">回复评论编号</param>
        /// <returns></returns>
        long Add(long userId, long postId, string content, long parentId = 0);

        /// <summary>
        /// 根据评论编号删除评论
        /// </summary>
        /// <param name="commentId">评论编号</param>
        /// <returns></returns>
        bool DeleteById(long commentId);

        /// <summary>
        /// 根据帖子编号删除评论
        /// </summary>
        /// <param name="postId">帖子编号</param>
        /// <returns></returns>
        bool DeleteByPostId(long postId);

        /// <summary>
        /// 根据帖子获取评论数量
        /// </summary>
        /// <param name="postId">帖子编号</param>
        /// <returns></returns>
        int GetCommentCount(long postId);

        /// <summary>
        /// 根据帖子编号获取评论分页列表
        /// (不包括回复评论)
        /// </summary>
        /// <param name="postId">帖子编号</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="pageIndex">页索引</param>
        /// <returns></returns>
        List<CommentEntity> GetCommentPagerList(long postId, int pageSize, int pageIndex);

        /// <summary>
        /// 根据评论编号获取回复评论分页列表
        /// </summary>
        /// <param name="commentId">评论编号</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="pageIndex">页索引</param>
        /// <returns></returns>
        List<CommentEntity> GetReCommentPagerList(long commentId, int pageSize, int pageIndex);
    }
}
