using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalMiscUtils.Drawing;
using PersonalMiscUtils.Drawing.Graphing;

namespace DrawingTests
{
	class Program
	{
		static void Main(string[] args)
		{
			string testFilePath = @"C:\Users\Chris\Documents\Files\Pictures\Pictures\The First Generation\IMG_0212.JPG";
			string outFilePath = @"C:\Users\Chris\Documents\GitHub\PersonalMiscUtils\PersonalMiscUtils.Drawing\DrawingTests\bin\Debug\out.png";
			var testFileBytes = File.ReadAllBytes(testFilePath).Select(i => (int)i);
			BarGraph2D graph = new BarGraph2D(testFileBytes);
			BitmapImage bitmap = graph.Render(0xFFFFFFFF, 0xFF0000FF);
			bitmap.SaveAsCBMP(outFilePath);
            Console.ReadKey();
        }
	}
}
