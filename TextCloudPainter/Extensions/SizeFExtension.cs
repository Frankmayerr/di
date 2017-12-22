using System;
using System.Drawing;

namespace TextCloudPainter
{
	public static class SizeFExtension
	{
		public static SizeF GetNewSizeWithCoefficient(this SizeF size, float coef)
		{
			return new SizeF(size.Width * coef, size.Height * coef);
		}
		public static Size Floor(this SizeF size) => 
			new Size((int)Math.Floor(size.Width), (int)Math.Floor(size.Height));
	}
}