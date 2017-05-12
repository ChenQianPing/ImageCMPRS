using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCMPRS
{
    public class ImageHelper
    {
        /// <summary>
        /// 无损压缩图片
        /// </summary>
        /// <param name="sFile">原图片</param>
        /// <param name="dFile">压缩后保存位置</param>
        /// <param name="dHeight">高度</param>
        /// <param name="dWidth">宽度</param>
        /// <param name="flag">压缩质量1-100</param>
        /// <returns></returns>
        public static bool GetPicThumbnail(string sFile, string dFile, int dHeight, int dWidth, int flag)
        {
            var iSource = Image.FromFile(sFile);
            var tFormat = iSource.RawFormat;
            int sW = 0, sH = 0;
            // 按比例缩放
            var temSize = new Size(iSource.Width, iSource.Height);
            if (temSize.Width > dHeight || temSize.Width > dWidth) 
            {
                if ((temSize.Width * dHeight) > (temSize.Height * dWidth))
                {
                    sW = dWidth;
                    sH = (dWidth * temSize.Height) / temSize.Width;
                }
                else
                {
                    sH = dHeight;
                    sW = (temSize.Width * dHeight) / temSize.Height;
                }
            }
            else
            {
                sW = temSize.Width;
                sH = temSize.Height;
            }

            var ob = new Bitmap(dWidth, dHeight);
            var g = Graphics.FromImage(ob);
            g.Clear(Color.WhiteSmoke);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(iSource, new Rectangle((dWidth - sW) / 2, (dHeight - sH) / 2, sW, sH), 0, 0, iSource.Width, iSource.Height, GraphicsUnit.Pixel);
            g.Dispose();

            // 以下代码为保存图片时，设置压缩质量
            var ep = new EncoderParameters();
            var qy = new long[1];

            // 设置压缩的比例1-100
            qy[0] = flag; 
            var eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;
            try
            {
                var arrayIci = ImageCodecInfo.GetImageEncoders();

                // JPEG、PNG
                var jpegIcIinfo = arrayIci.FirstOrDefault(t => t.FormatDescription.Equals("png"));

                if (jpegIcIinfo != null)
                {
                    ob.Save(dFile, jpegIcIinfo, ep); // dFile是压缩后的新路径
                }
                else
                {
                    ob.Save(dFile, tFormat);
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                iSource.Dispose();
                ob.Dispose();
            }
        }
    }
}


/**
 * 1.25M 压缩为 224KB；
 * https://tinypng.com/
 * 
 * 使用TinyPNG提供的API，对图片进行压缩（C#）
 * 注册获取key https://tinypng.com/developers
 * 每个月有500张免费使用；
 * 
 * Your usage this month
 * Compressed images: 0 / 500
 * API key：E-xqtoRic_RpVEAe3y1G0lZua0XUkAJG
 */

