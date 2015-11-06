using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalMiscUtils.Drawing.Graphing
{
	public sealed class BarGraph2D
	{
		public List<int> Data { get; private set; } = new List<int>();

		public BarGraph2D(IEnumerable<int> data)
		{
			Data = data.ToList();
		}

		public void Sort()
		{
			Data = Data.OrderBy(e => e).ToList();
		}

		public BitmapImage Render(uint clearColor, uint setColor)
		{
			int resultWidth = Data.Count;
			int maximum = Data.Max();
			double divisor = 1;
			int resultHeight = maximum;

			if (resultHeight > 1024)
			{
				while (resultHeight > 1024)
				{
					resultHeight /= 2;
					divisor *= 2;
				}
			}

			BitmapImage result = new BitmapImage(resultWidth, resultHeight);
			result.SetAllPixels(clearColor);

			for (int i = 0; i < Data.Count; i++)
			{
				int element = Data[i];
				int adjustedHeight = (element > 1) ? (int)(element / divisor) : 1;

				int y = result.Height - 1;
				while (adjustedHeight > 0)
				{
					result[i, y] = setColor;
					y--;
					adjustedHeight--;
				}
			}

			return result;
		}
	}
}
