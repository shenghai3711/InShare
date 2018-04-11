using System.Collections.Generic;

namespace InShare.IService
{
    public interface IFollowService : IServiceSupport
    {
        /// <summary>
        /// 关注用户
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="followedId">被关注者编号</param>
        /// <returns></returns>
        bool Follow(long userId, long followedId);

        /// <summary>
        /// 取消关注用户
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="unfollowId">被取消关注用户编号</param>
        /// <returns></returns>
        bool Unfollow(long userId, long unfollowId);
        
        /// <summary>
        /// 获取关注者数量
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        int GetFollowerCount(long userId);

        /// <summary>
        /// 获取关注者编号分页列表
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="pageIndex">页索引</param>
        /// <returns></returns>
        List<long> GetFollowerPagerList(long userId, int pageSize, int pageIndex);
        
        /// <summary>
        /// 获取正在关注编号数量
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        int GetFollowingCount(long userId);

        /// <summary>
        /// 获取正在关注者编号分页列表
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="pageIndex">页索引</param>
        /// <returns></returns>
        List<long> GetFollowingPagerList(long userId, int pageSize, int pageIndex);
    }
}
