namespace InShare.Model
{
    /// <summary>
    /// 日志实体类
    /// </summary>
    public class LogEntity : BaseEntity
    {
        /// <summary>
        /// 日志类型
        /// Like，Follow，Comment，Tag，Unfollow，Delete操作
        /// </summary>
        public int LogType { get; set; }

        /// <summary>
        /// 日志内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户导航属性
        /// </summary>
        public virtual UserEntity User { get; set; }
    }
    
}
