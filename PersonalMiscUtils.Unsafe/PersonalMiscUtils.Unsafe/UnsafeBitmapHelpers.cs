using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalMiscUtils.Unsafe
{
	public static class UnsafeBitmapHelpers
	{
		public static uint[] BitmapToPixels(Bitmap bitmap)
		{
			// Clone the bitmap to make sure it's 32bpp ARGB
			// Bitmap argbClone = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppArgb);
			// using (Graphics graphics = Graphics.FromImage(argbClone))
			// {
			//	graphics.DrawImage(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height);
			// }
			// bitmap = argbClone;

			uint[] result = new uint[bitmap.Width * bitmap.Height];
			BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
			int stride = data.Stride;
			IntPtr scan0 = data.Scan0;

			unsafe
			{
				int i = 0;
				for (int y = 0; y < bitmap.Height; y++)
				{
					for (int x = 0; x < bitmap.Width; x++, i++)
					{
						uint pixel = 0u;
						byte* pb = (byte*)scan0 + (i * 4);

						pixel += *pb;						// Blue
						pixel += (uint)(*(pb + 1) << 8);    // Green
						pixel += (uint)(*(pb + 2) << 16);   // Red
						pixel += (uint)(*(pb + 3) << 24);   // Alpha
						result[i] = pixel;
					}
				}
			}

			bitmap.UnlockBits(data);
			// bitmap.Dispose();
			return result;
		}

		public static Bitmap PixelsToImage(uint[] pixels, int width, int height)
		{
			Bitmap result = new Bitmap(width, height, PixelFormat.Format32bppArgb);
			BitmapData data = result.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
			IntPtr scan0 = data.Scan0;
			
			unsafe
			{
				fixed (uint* pui = &pixels[0])
				{
					byte* pb = (byte*)pui;
					for (int i = 0; i < pixels.Length * 4; i++)
					{
						*(byte*)(scan0 + i) = *(pb + i);
					}
				}
			}

			result.UnlockBits(data);
			return result;
		}
	}
}