using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace MoecraftFramework
{
    class Rotate
    {
        #region 图片旋转函数
        public static Image RotateImg(Image pic, int degree)
        {

            if (degree == 0) { return pic; }
            Bitmap p = new Bitmap(pic.Width + pic.Height, pic.Width + pic.Height);
            Graphics g = Graphics.FromImage(p);
            g.TranslateTransform((float)pic.Width / 2, (float)pic.Height / 2);
            g.RotateTransform(degree);
            g.TranslateTransform(-(float)pic.Width / 2, -(float)pic.Height / 2);
            g.DrawImage(pic, 0, 0);
            return p;
        }
        #endregion 图片旋转函数
        public static Bitmap RotateImage(Image image, int angle)
        {
            if (image == null)
                throw new ArgumentNullException("image");
            const double pi2 = Math.PI / 2.0;
            double oldWidth = (double)image.Width;
            double oldHeight = (double)image.Height;
            double theta = ((double)angle) * Math.PI / 180.0;
            double locked_theta = theta;
            while (locked_theta < 0.0)
                locked_theta += 2 * Math.PI;
            double newWidth, newHeight;
            int nWidth, nHeight;
            #region Explaination of the calculations
            #endregion
            double adjacentTop, oppositeTop;
            double adjacentBottom, oppositeBottom;
            if ((locked_theta >= 0.0 && locked_theta < pi2) ||
                (locked_theta >= Math.PI && locked_theta < (Math.PI + pi2)))
            {
                adjacentTop = Math.Abs(Math.Cos(locked_theta)) * oldWidth;
                oppositeTop = Math.Abs(Math.Sin(locked_theta)) * oldWidth;
                adjacentBottom = Math.Abs(Math.Cos(locked_theta)) * oldHeight;
                oppositeBottom = Math.Abs(Math.Sin(locked_theta)) * oldHeight;
            }
            else
            {
                adjacentTop = Math.Abs(Math.Sin(locked_theta)) * oldHeight;
                oppositeTop = Math.Abs(Math.Cos(locked_theta)) * oldHeight;
                adjacentBottom = Math.Abs(Math.Sin(locked_theta)) * oldWidth;
                oppositeBottom = Math.Abs(Math.Cos(locked_theta)) * oldWidth;
            }
            newWidth = adjacentTop + oppositeBottom;
            newHeight = adjacentBottom + oppositeTop;
            nWidth = (int)Math.Ceiling(newWidth);
            nHeight = (int)Math.Ceiling(newHeight);
            Bitmap rotatedBmp = new Bitmap(nWidth, nHeight);
            using (Graphics g = Graphics.FromImage(rotatedBmp))
            {
                Point[] points;
                if (locked_theta >= 0.0 && locked_theta < pi2)
                {
                    points = new Point[] {
new Point( (int) oppositeBottom, 0 ),
 new Point( nWidth, (int) oppositeTop ),
 new Point( 0, (int) adjacentBottom )
 };
                }
                else if (locked_theta >= pi2 && locked_theta < Math.PI)
                {
                    points = new Point[] {
 new Point( nWidth, (int) oppositeTop ),
 new Point( (int) adjacentTop, nHeight ),
 new Point( (int) oppositeBottom, 0 )
 };
                }
                else if (locked_theta >= Math.PI && locked_theta < (Math.PI + pi2))
                {
                    points = new Point[] {
 new Point( (int) adjacentTop, nHeight ),
 new Point( 0, (int) adjacentBottom ),
 new Point( nWidth, (int) oppositeTop )
 };
                }
                else
                {
                    points = new Point[] {
 new Point( 0, (int) adjacentBottom ),
 new Point( (int) oppositeBottom, 0 ),
 new Point( (int) adjacentTop, nHeight )
 };
                }
                g.DrawImage(image, points);
            }
            return rotatedBmp;
        }
    }
}
