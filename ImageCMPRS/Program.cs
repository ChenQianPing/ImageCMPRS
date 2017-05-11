using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ImageCMPRS
{
    class Program
    {
        public static int FileNums = 0;
        public static int Status = 0;

        static void Main(string[] args)
        {
            /*
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
            */

            /*
            Console.WriteLine("请输入TinyPng.com的API KEY,获取地址:https://tinypng.com/developers");
            var key = Console.ReadLine();
            */

            const string key = "E-xqtoRic_RpVEAe3y1G0lZua0XUkAJG";

            if (!System.IO.Directory.Exists("NewImg"))
            {
                System.IO.Directory.CreateDirectory("NewImg");
                Console.WriteLine("已创建NewImg目录，请把需要处理的图片放到该目录下");
                Console.ReadKey(true);
                return;
            }

            if (!System.IO.Directory.Exists("CompressedImg"))
            {
                System.IO.Directory.CreateDirectory("CompressedImg");
            }

            var url = "https://api.tinify.com/shrink";
            var fileStrs = System.IO.Directory.GetFiles("NewImg");
            FileNums = fileStrs.Length;
            foreach (var s in fileStrs)
            {
                var info = new System.IO.FileInfo(s);
                if (info.Extension == ".png" || info.Extension == ".PNG" || info.Extension == ".jpg" || info.Extension == ".JPG")
                {
                    var input = @"NewImg/" + info.Name;
                    var output = @"CompressedImg/" + info.Name;
                    SendReq(url, key, input, output);
                }
            }
            Console.WriteLine("共" + FileNums + "个文件,请等待下载完成...");
            Console.ReadKey(true);

        }

        public static async void SendReq(string url, string key, string input, string output)
        {
            await GetCompressImg(url, key, input, output);
            Console.WriteLine("已完成:" + input);
            Status++;
            if (Status == FileNums)
            {
                Console.WriteLine("下载已全部完成，共" + Status + "个文件");
            }
        }

        public static async Task GetCompressImg(string url, string key, string input, string output)
        {
            var client = new WebClient();
            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes("api:" + key));
            client.Headers.Add(HttpRequestHeader.Authorization, "Basic " + auth);
            try
            {
                await client.UploadDataTaskAsync(url, File.ReadAllBytes(input));
                await client.DownloadFileTaskAsync(client.ResponseHeaders["Location"], output);
            }
            catch (WebException)
            {
                Console.WriteLine("网络请求失败:" + input);
            }
        }

    }
}
