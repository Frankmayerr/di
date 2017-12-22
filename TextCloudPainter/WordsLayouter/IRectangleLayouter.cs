using System.Collections.Generic;
using System.Drawing;

namespace TextCloudPainter
{
	public interface IRectangleLayouter
	{
		IEnumerable<Rectangle> PutAllRectangles(IEnumerable<Size> sizes);
		List<Rectangle> GetShiftedRectanglesWithPositiveCoordinates(IEnumerable<Rectangle> rectangles);
	}
}