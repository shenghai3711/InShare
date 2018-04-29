using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InShare.Common
{
    /// <summary>
    /// 随机类
    /// </summary>
    public static class RandomHelper
    {
        /// <summary>
        /// 创建编号
        /// </summary>
        /// <param name="length">长度(用户编号：10，帖子编号：19)</param>
        /// <returns></returns>
        public static long CreateId(int length = 10)
        {
            char[] data = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            return Convert.ToInt64(CreateRandomStr(data, length));
        }


        /// <summary>
        /// 创建帖子链接码
        /// </summary>
        /// <returns></returns>
        public static string CreatePostCode()
        {
            string str = Guid.NewGuid().ToString();
            return str.Substring(str.LastIndexOf('-')+1);
        }

        /// <summary>
        /// 创建密码盐
        /// </summary>
        /// <returns></returns>
        public static string CreateSalt()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 10);
        }

        /// <summary>
        /// 创建图片验证码
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string CreateImageCode(int length = 4)
        {
            char[] data = { 'a', 'b', 'c', 'd', 'e', 'f', 'r', 's', 't', 'w', 'x', 'y', '2', '3', '4' };
            return CreateRandomStr(data, length);
        }

        /// <summary>
        /// 创建随机字符串
        /// </summary>
        /// <param name="data"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private static string CreateRandomStr(char[] data, int length)
        {
            StringBuilder sb = new StringBuilder();
            Random rand = new Random();
            for (int i = 0; i < length; i++)
            {
                int index = rand.Next(data.Length);
                char ch = data[index];
                sb.Append(ch);
            }
            return sb.ToString();
        }

    }
}
