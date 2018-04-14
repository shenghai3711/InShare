using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InShare.Common
{
    /// <summary>
    /// ajax处理后结果
    /// </summary>
    public class AjaxResult
    {
        /// <summary>
        /// 执行的结果
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMsg { get; set; }

        /// <summary>
        /// 执行返回的数据
        /// </summary>
        public object Data { get; set; }
    }
}
