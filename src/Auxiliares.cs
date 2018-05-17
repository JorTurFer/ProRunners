using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Sistema_Nuria
{
    static class Auxiliares
    {
        public static string RebuildName(string strInput)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string item in strInput.Trim().Split(' '))
            {
                string tmp = item.ToLower();
                sb.Append(tmp.First().ToString().ToUpper() + tmp.Substring(1) + " ");
            }

            return sb.ToString().TrimEnd();

        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source?.IndexOf(toCheck, comp) >= 0;
        }

        public static ImageFormat ToImageFormat(this int value)
        {
            ImageFormat ret;

            switch(value)
            {
                case 0:
                    ret = ImageFormat.FullHD;
                    break;
                case 1:
                    ret = ImageFormat.InterHD;
                    break;
                case 2:
                default:
                    ret = ImageFormat.VideoHD;
                    break;
            }
            return ret;
        }

      
        public static byte[] BitmapToByteArray(this Bitmap bitmap, out int Width, out int Height)
        {
            BitmapData bmpdata = null;
            try
            {
                Width = bitmap.Width;
                Height = bitmap.Height;
                bmpdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);
                int numbytes = bmpdata.Stride * bitmap.Height;
                byte[] bytedata = new byte[numbytes];
                IntPtr ptr = bmpdata.Scan0;

                Marshal.Copy(ptr, bytedata, 0, numbytes);

                return bytedata;
            }
            finally
            {
                if (bmpdata != null)
                    bitmap.UnlockBits(bmpdata);
            }
        }      
    }
}
