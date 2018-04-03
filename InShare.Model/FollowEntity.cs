namespace InShare.Model
{
    /// <summary>
    /// 关注实体类
    /// </summary>
    public class FollowEntity : BaseEntity
    {
        /// <summary>
        /// 关注者编号
        /// </summary>
        public long FollowId { get; set; }

        /// <summary>
        /// 被关注者编号
        /// </summary>
        public long FollowedId { get; set; }
    }
}
