﻿using InShare.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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
        /// <param name="location">地理位置</param>
        /// <returns></returns>
        long Add(long userId, string content, string imgPath, string location);

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
        /// 获取帖子信息
        /// </summary>
        /// <param name="ShortCode">链接码</param>
        /// <returns></returns>
        PostEntity GetPostInfo(string ShortCode);

        /// <summary>
        /// 根据用户编号获取帖子数量
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        int GetPostCount(long userId = 0, bool flag = false);

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
        /// 根据标记编号获取参与用户数量
        /// </summary>
        /// <param name="tagId">标记编号</param>
        /// <returns></returns>
        int GetUserCountByTag(long tagId);

        /// <summary>
        /// 获取主页帖子
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="pageIndex">页索引</param>
        /// <returns></returns>
        List<PostEntity> GetHomePager(long userId, int pageSize, int pageIndex);

        /// <summary>
        /// 获取搜索页帖子
        /// </summary>
        /// <param name="tagId">标记编号</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="pageIndex">页索引</param>
        /// <returns></returns>
        List<PostEntity> GetSearchPager(long tagId, int pageSize, int pageIndex);

        #region 后台操作

        List<PostEntity> GetPostPagerList(int pageSize, int pageIndex, bool flag = false);

        bool MarkBatchDel(long[] postIds);

        bool MarkDel(long postId);

        bool ReMark(long postId);

        bool ReMarkBatch(long[] postIds);

        bool Edit(long postId, string content, string imgPath, string location);

        #endregion

    }
}
