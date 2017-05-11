using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCMPRS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");

            const string sFile = @"E:\10001003\主界面.png";
            const string dFile = @"E:\10001003\主界面d.png";
            var dHeight = 0;
            var dWidth = 0;
            const int flag = 10;

            // 获取图片文件的宽度和高度
            using (var fs = new FileStream(sFile, FileMode.Open, FileAccess.Read))
            {
                var image = System.Drawing.Image.FromStream(fs);
                dWidth = image.Width;
                dHeight = image.Height;
            }

            var isSuccess = ImageHelper.GetPicThumbnail(sFile, dFile, dHeight, dWidth, flag);

            Console.WriteLine(isSuccess);
            Console.ReadLine();
        }
    }
}
