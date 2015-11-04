using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalMiscUtils.Unsafe;

namespace UnsafeTests
{
	class Program
	{
		static void Main(string[] args)
		{
			Bitmap bitmap = (Bitmap)Image.FromFile(@"C:\Users\Chris\Documents\Files\Pictures\Pictures\CS Series\1cs Series\1cs000163.png");
			var asPixels = UnsafeBitmapHelpers.BitmapToPixels(bitmap);
			Console.WriteLine($"{asPixels[0]:X2}, {asPixels[1]:X2}, {asPixels[2]:X2}");
			Bitmap bitmap2 = UnsafeBitmapHelpers.PixelsToImage(asPixels, bitmap.Width, bitmap.Height);
			bitmap2.Save(@"C:\Users\Chris\Documents\Files\Pictures\Pictures\test.png", ImageFormat.Png);
		}
	}
}
