using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalMiscUtils.Unsafe;

namespace PersonalMiscUtils.Drawing
{
	public sealed class BitmapImage
	{
		private uint[] argbPixels;

		public int Width { get; }
		public int Height { get; }

		public uint this[int x, int y]
		{
			get
			{
				return argbPixels[GetPixelIndex(x, y)];
			}
			set
			{
				argbPixels[GetPixelIndex(x, y)] = value;
			}
		}

		public BitmapImage(int width, int height)
		{
			Width = width;
			Height = height;
			argbPixels = new uint[width * height];
		}

		public BitmapImage(Image image)
		{
			Width = image.Width;
			Height = image.Height;
			argbPixels = UnsafeBitmapHelpers.BitmapToPixels(new Bitmap(image));
		}

		private int GetPixelIndex(int x, int y)
		{
			if (x < 0 || x >= Width || y < 0 || y >= Height) { throw new ArgumentOutOfRangeException($"Tried to get a pixel at x:{x}, y:{y}, which is outside of the image."); }
			return (y * Width) + x;
		}

		public void SetAllPixels(uint pixel)
		{
			for (int i = 0; i < argbPixels.Length; i++)
			{
				argbPixels[i] = pixel;
			}
		}

		public void Save(string filePath, ImageFormat format)
		{
			Bitmap bitmap = UnsafeBitmapHelpers.PixelsToImage(argbPixels, Width, Height);
			bitmap.Save(filePath, format);
		}

		public void SaveAsCBMP(string filePath)
		{
			if (string.IsNullOrEmpty(filePath)) { throw new ArgumentException($"The path \"{filePath}\" is not a valid path."); }
			File.Delete(filePath);

			using (BinaryWriter writer = new BinaryWriter(new FileStream(filePath, FileMode.CreateNew)))
			{
				writer.Write(new byte[] { 0x43, 0x42, 0x4D, 0x50 });		// ASCII: Magic number "CBMP"
				writer.Write(Width);										// int: Width of image
				writer.Write(Height);										// int: Height of image
				writer.Write((byte)32);										// int: Bits per pixel

				foreach (uint pixel in argbPixels) { writer.Write(pixel); }	// uint[]: Pixels
			}
		}

		public static BitmapImage FromFile(string filePath)
		{
			return new BitmapImage(Image.FromFile(filePath));
		}

		public static BitmapImage FromCBMP(string filePath)
		{
			if (string.IsNullOrEmpty(filePath)) { throw new ArgumentException($"The path \"{filePath}\" is not a valid path."); }
			int width, height;

			using (BinaryReader reader = new BinaryReader(new FileStream(filePath, FileMode.Open)))
			{
				byte[] magicNumber = reader.ReadBytes(4);
				if (magicNumber[0] != 0x43 || magicNumber[1] != 0x42 || magicNumber[2] != 0x4D || magicNumber[3] != 0x50) { throw new IOException($"The file at \"{filePath}\" is not a valid CBMP file (magic number is {Encoding.ASCII.GetString(magicNumber)}"); }
				width = reader.ReadInt32();
				height = reader.ReadInt32();
				if (reader.ReadByte() != 32) { throw new NotSupportedException("Bit depths other than 32 bits per pixel aren't supported yet."); }

				BitmapImage result = new BitmapImage(width, height);
				for (int y = 0; y < height; y++)
				{
					for (int x = 0; x < width; x++)
					{
						result[x, y] = reader.ReadUInt32();
					}
				}

				return result;
			}
		}
	}
}
