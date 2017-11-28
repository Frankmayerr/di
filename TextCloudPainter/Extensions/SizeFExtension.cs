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
		public static Size Ceil(this SizeF size) => 
			new Size((int)Math.Ceiling(size.Width), (int)Math.Ceiling(size.Height));
	}
}