using InShare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InShare.IService
{
   public interface ICommentService:IServiceSupport
    {
        /// <summary>
        /// 新增评论
        /// </summary>
        /// <param name="userId">评论者编号</param>
        /// <param name="postId">帖子编号</param>
        /// <param name="content">评论内容</param>
        /// <returns></returns>
        long Add(long userId, long postId, string content);

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
        /// 根据帖子获取评论
        /// </summary>
        /// <param name="postId">帖子编号</param>
        /// <returns></returns>
        List<CommentEntity> GetPostList(long postId);

        /// <summary>
        /// 根据帖子获取评论数量
        /// </summary>
        /// <param name="postId">帖子编号</param>
        /// <returns></returns>
        int GetCommentCount(long postId);

        /// <summary>
        /// 根据帖子编号获取评论分页列表
        /// </summary>
        /// <param name="postId">帖子编号</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="pageIndex">页索引</param>
        /// <returns></returns>
        List<CommentEntity> GetCommentPagerList(long postId, int pageSize, int pageIndex);
    }
}
