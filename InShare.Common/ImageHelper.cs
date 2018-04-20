using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Qiniu.Storage;
using Qiniu.Http;
using Qiniu.Util;

namespace InShare.Common
{
    public class ImageHelper
    {
        /// <summary>
        /// 验证码图片
        /// </summary>
        /// <param name="checkCode"></param>
        /// <returns></returns>
        public static byte[] CodeImage(string checkCode)
        {
            Bitmap image = new Bitmap(80, 35);
            Graphics g = Graphics.FromImage(image);
            Random random = new Random();
            g.Clear(Color.White);
            for (int i = 0; i < 3; i++)
            {
                int x1 = random.Next(image.Width);
                int x2 = random.Next(image.Width);
                int y1 = random.Next(image.Height);
                int y2 = random.Next(image.Height);
                g.DrawLine(new Pen(Color.Black), x1, y1, x2, y2);
            }
            Font font = new Font("Arial", 23, (System.Drawing.FontStyle.Bold));
            g.DrawString(checkCode, font, new SolidBrush(Color.MediumTurquoise), 2, 2);
            for (int i = 0; i < 200; i++)
            {
                int x = random.Next(image.Width);
                int y = random.Next(image.Height);
                image.SetPixel(x, y, Color.FromArgb(random.Next()));
            }
            MemoryStream ms = new MemoryStream();
            try
            {
                image.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

        /// <summary>
        /// 上传文件到七牛云
        /// </summary>
        /// <param name="imgPath">文件路径</param>
        /// <param name="name">文件后缀名</param>
        /// <returns>服务器文件路径</returns>
        public static string UploadFile(string imgPath, string name = ".jpg")
        {
            Mac mac = new Mac("h-0RX_DYCsRy3d8NITyVejjVWDXJbVyolHgPQ5xA",
                   "gnTJ6QzZe5tVSZvTrlQLhYs0hZ-Oava2n8FcJtgs");
            // 上传文件名
            string key = string.IsNullOrEmpty(name) ? Guid.NewGuid().ToString() : Guid.NewGuid().ToString() + name;
            // 本地文件路径
            //string filePath = imgUrl;
            // 存储空间名
            string Bucket = "fyzsmanager";
            // 设置上传策略，详见：https://developer.qiniu.com/kodo/manual/1206/put-policy
            PutPolicy putPolicy = new PutPolicy();
            putPolicy.Scope = Bucket;
            putPolicy.SetExpires(3600);
            putPolicy.DeleteAfterDays = 60;//60天后自动删除
            string token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());
            Config config = new Config();
            // 设置上传区域
            config.Zone = Zone.ZONE_CN_South;
            // 设置 http 或者 https 上传
            config.UseHttps = true;
            config.UseCdnDomains = true;
            config.ChunkSize = ChunkUnit.U512K;
            // 表单上传
            FormUploader target = new FormUploader(config);
            HttpResult result = target.UploadFile(imgPath, key, token, null);
            if (result.Code == 200)
            {
                return string.Format("http://oxdwc8csx.bkt.clouddn.com/{0}", key);
            }
            return null;
        }

        /// <summary>
        /// 上传文件流到七牛云
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="name">后缀名</param>
        /// <returns></returns>
        public static string UploadStream(Stream stream, string name = ".jpg")
        {
            try
            {
                Mac mac = new Mac("h-0RX_DYCsRy3d8NITyVejjVWDXJbVyolHgPQ5xA",
                   "gnTJ6QzZe5tVSZvTrlQLhYs0hZ-Oava2n8FcJtgs");
                // 上传文件名
                string key = string.IsNullOrEmpty(name) ? Guid.NewGuid().ToString() : Guid.NewGuid().ToString() + name;
                // 本地文件路径
                //string filePath = imgUrl;
                // 存储空间名
                string Bucket = "fyzsmanager";
                // 设置上传策略，详见：https://developer.qiniu.com/kodo/manual/1206/put-policy
                PutPolicy putPolicy = new PutPolicy();
                putPolicy.Scope = Bucket;
                putPolicy.SetExpires(3600);
                putPolicy.DeleteAfterDays = 60;//60天后自动删除
                string token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());
                Config config = new Config();
                // 设置上传区域
                config.Zone = Zone.ZONE_CN_South;
                // 设置 http 或者 https 上传
                config.UseHttps = true;
                config.UseCdnDomains = true;
                config.ChunkSize = ChunkUnit.U512K;
                // 表单上传
                FormUploader target = new FormUploader(config);
                HttpResult result = target.UploadStream(stream, key, token, null);
                if (result.Code == 200)
                {
                    return string.Format("http://oxdwc8csx.bkt.clouddn.com/{0}", key);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        /// <summary>
        /// ImgBase64转图片文件流
        /// </summary>
        /// <param name="base64str"></param>
        /// <returns></returns>
        public static Stream ImgBase64ToStream(string base64str)
        {
            try
            {
                var imgData = base64str.Split(',')[1];
                //过滤特殊字符即可   
                string dummyData = imgData.Trim().Replace("%", "").Replace(",", "").Replace(" ", "+");
                if (dummyData.Length % 4 > 0)
                {
                    dummyData = dummyData.PadRight(dummyData.Length + 4 - dummyData.Length % 4, '=');
                }
                byte[] byteArray = Convert.FromBase64String(dummyData);
                return new System.IO.MemoryStream(byteArray);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
    }
}
