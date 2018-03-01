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
            foreach (string item in strInput.Split(' '))
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

        public static T DeepClone<T>(this T a)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, a);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }

        public static Bitmap MarshalClone(this Bitmap bmp)
        {
            var Bytes = _BitmapToByteArray(bmp,out int Width, out int Height);
            return CopyDataToBitmap(Bytes,Width,Height);
        }

        private static byte[] _BitmapToByteArray(Bitmap bitmap, out int Width, out int Height)
        {
            BitmapData bmpdata = null;
            try
            {
                Width = bitmap.Width;
                Height = bitmap.Height;
                bmpdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
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

        public static Bitmap CopyDataToBitmap(byte[] data, int Width, int Height)
        {
            //Here create the Bitmap to the know height, width and format
            Bitmap bmp = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);

            //Create a BitmapData and Lock all pixels to be written 
            BitmapData bmpData = bmp.LockBits(
                                 new Rectangle(0, 0, bmp.Width, bmp.Height),
                                 ImageLockMode.WriteOnly, bmp.PixelFormat);

            //Copy the data from the byte array into BitmapData.Scan0
            Marshal.Copy(data, 0, bmpData.Scan0, data.Length);


            //Unlock the pixels
            bmp.UnlockBits(bmpData);


            //Return the bitmap 
            return bmp;
        }
    }
}
