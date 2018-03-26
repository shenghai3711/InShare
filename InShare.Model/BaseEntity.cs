using System;

namespace InShare.Model
{
    /// <summary>
    /// 基础实体类
    /// </summary>
    public class BaseEntity
    {
        public BaseEntity()
        {
            this.CreateDateTime = DateTime.Now;
            this.IsDeleted = false;
        }

        /// <summary>
        /// 编号
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// 是否标记为删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
