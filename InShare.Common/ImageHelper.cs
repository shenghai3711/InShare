using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
    }
}
