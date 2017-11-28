using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace TextCloudPainter
{
	[TestFixture]
	public class RectangleExtensionsTests
	{
		[Test]
		public void FromRectangle_GetVertexes_CorrectResult()
		{
			var rect = new Rectangle(new Point(0, 0), new Size(2, 5));
			var expectedTopList = new[] {new Point(0, 0), new Point(2, 0), new Point(0, 5), new Point(2, 5)};
			rect.GetRectangleVertexes()
				.ShouldAllBeEquivalentTo(expectedTopList);
		}
	}
}