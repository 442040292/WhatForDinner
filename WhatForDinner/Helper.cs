using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Gif.Components;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace WhatForDinner
{
    public class Helper
    {
        public static void CreateGif(IEnumerable<string> newItems, string savePath)
        {
            if (newItems.Count() <= 0)
            {
                return;
            }
            List<System.Drawing.Image> images = new List<System.Drawing.Image>();
            var maxWidth = newItems.Max(x => x.Length) * 3 * 14;
            var maxHeight = 100;
            foreach (var item in newItems)
            {
                var image = FillRectangle(item, maxWidth, maxHeight);
                images.Add(image);
            }
            ConvertJpgToGif(images, savePath, 1, maxWidth, maxHeight);
        }

        /// <summary>
        /// 多张图片转成一张gif图片
        /// </summary>
        /// <param name="imageFilePaths">图片路径，放到一个数组里面</param>
        /// <param name="gifPath">生成的gif图片路径</param>
        /// <param name="time">每一帧图片间隔时间</param>
        /// <param name="w">生成的gif图片宽度</param>
        /// <param name="h">生成的gif图片高度</param>
        /// <returns></returns>
        public static bool ConvertJpgToGif(List<System.Drawing.Image> images, string gifPath, int time, int w, int h)
        {
            try
            {
                AnimatedGifEncoder e = new AnimatedGifEncoder();
                e.Start(gifPath);
                e.SetDelay(time);
                //0:循环播放    -1:不循环播放
                e.SetRepeat(0);
                for (int i = 0, count = images.Count; i < count; i++)
                {
                    //e.AddFrame(Image.FromFile(Server.MapPath(imageFilePaths[i])));

                    System.Drawing.Image img = images[i];
                    //如果多张图片的高度和宽度都不一样，可以打开这个注释
                    img = ReSetPicSize(img, w, h);
                    e.AddFrame(img);
                }
                e.Finish();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 重新调整图片的大小，使其满足要求
        /// </summary>
        /// <param name="image"></param>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <returns></returns>
        public static Image ReSetPicSize(Image image, int newWidth, int newHeight)
        {
            Bitmap bmp = new Bitmap(image);
            try
            {
                Bitmap b = new Bitmap(newWidth, newHeight);
                Graphics g = Graphics.FromImage(b);
                // 插值算法的质量 
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new System.Drawing.Rectangle(0, 0, newWidth, newHeight), new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                // return b;
                Image img = (Image)b;
                //  MessageBox.Show("Width"+img.Width.ToString() + "Height:" + img.Height.ToString());
                return img;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 在图片中绘制 矩形
        /// </summary>
        /// <param name="image"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public static System.Drawing.Image FillRectangle(string text, int rectWith, int rectHeight)
        {
            Bitmap b = new Bitmap(rectWith, rectHeight);
            System.Drawing.Image image = b;
            //b.Save("aa.jpg", ImageFormat.Jpeg);
            Graphics graphics = Graphics.FromImage(image);
            graphics.FillRectangle(new SolidBrush(System.Drawing.Color.White), new RectangleF(0, 0, rectWith, rectWith));
            graphics.DrawString(text, new Font("微软雅黑", 14), new SolidBrush(System.Drawing.Color.Black), rectWith / 2, rectHeight / 2, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });//红色的圈显示 点击位置
            graphics.Flush();
            graphics.Dispose();
            return image;
        }
    }
}
