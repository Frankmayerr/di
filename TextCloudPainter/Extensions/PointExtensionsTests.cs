using System;
using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace TextCloudPainter
{
	[TestFixture]
	public class PointExtensionsTests
	{
		[Test]
		public void ForPoint_MakeRectanglesWithTopsInIt_CorrectCoordinates()
		{
			var point = new Point(1, 1);
			var size = new Size(2, 4);
			var excpectedResult = new List<Rectangle>()
			{
				new Rectangle(point, size),
				new Rectangle(new Point(-1, -3), size),
				new Rectangle(new Point(1, -3), size),
				new Rectangle(new Point(-1, 1), size)
			};
			var result = point.GetRectanglesAroundPoint(size);
			result.ShouldAllBeEquivalentTo(excpectedResult);
		}
	}
}