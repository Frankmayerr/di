using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace TextCloudPainter
{
	public static class RectangleExtensions
	{
		public static List<Point> GetRectangleVertexes(this Rectangle rect)
		{
			var pts = new List<Point>
			{
				new Point(rect.Left, rect.Bottom),
				new Point(rect.Right, rect.Top),
				new Point(rect.Left, rect.Top),
				new Point(rect.Right, rect.Bottom)
			};
			return pts;
		}

		public static Point Center(this Rectangle rect) =>
			new Point(rect.Left + rect.Width / 2, rect.Bottom + rect.Height / 2);
	}
}