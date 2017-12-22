using System;
using System.Drawing;

namespace TextCloudPainter
{
	public class RandomBrushMaker : IBrushMaker
	{
		private readonly Random random = new Random();

		public Brush GetBrush()
		{
			return new SolidBrush(GetRandomColor(random));
		}

		public static Color GetRandomColor(Random rand)
			=> Color.FromArgb(127, rand.Next(100, 255), rand.Next(100, 255), rand.Next(100, 255));
	}
}