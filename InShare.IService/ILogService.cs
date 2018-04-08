using InShare.Model;
using System.Collections.Generic;

namespace InShare.IService
{
    public interface ILogService : IServiceSupport
    {
        /// <summary>
        /// 新增日志
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="type">日志类型</param>
        /// <param name="content">日志内容</param>
        /// <returns></returns>
        long Add(long userId, int type, string content);

        /// <summary>
        /// 获取用户日志数量
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        int GetLogCount(long userId);

        /// <summary>
        /// 获取用户日志分页列表
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="pageIndex">页索引</param>
        /// <returns></returns>
        List<LogEntity> GetLogPagerList(long userId,int pageSize,int pageIndex);
    }
}
