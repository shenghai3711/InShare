using InShare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InShare.IService
{
    public interface IVerifyService : IServiceSupport
    {
        /// <summary>
        /// 生成一个验证
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        VerifyEntity Add(long userId);

        /// <summary>
        /// 修改验证状态
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        bool UpdateValid(long userId);

        /// <summary>
        /// 根据用户取出有效的验证码
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        VerifyEntity GetVerify(long userId);
    }
}
