using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TextCloudPainter
{
	public class DistanceRectangleLayouter:IRectangleLayouter
	{
		public readonly Point Center;
		public List<Rectangle> prevRects = new List<Rectangle>();

		public DistanceRectangleLayouter(Point center)
		{
			Center = center;
		}

		public Rectangle PutNextRectangle(Size rectangleSize)
		{
			if (prevRects.Count == 0)
			{
				prevRects.Add(new Rectangle(new Point(Center.X - rectangleSize.Width / 2, Center.Y - rectangleSize.Height / 2), rectangleSize));
				return prevRects[0];
			}
			var nextRect = prevRects
				.SelectMany(rect => rect.GetRectangleVertexes())
				.SelectMany(point => point.GetRectanglesAroundPoint(rectangleSize))
				.Distinct()
				.Where(CanAdd)
				.OrderBy(rect => rect.Center().GetDistance(Center))
				.FirstOrDefault();

			prevRects.Add(nextRect);
			return nextRect;

		}

		public List<Rectangle> GetShiftedRectanglesWithPositiveCoordinates(IEnumerable<Rectangle> rectangles)
		{
			var points = rectangles.SelectMany(rect => rect.GetRectangleVertexes());
			var shift = new Point
			{
				X = Math.Min(points.Select(p => p.X).Min(), 0),
				Y = Math.Min(points.Select(p => p.Y).Min(), 0)
			};
			return rectangles.Select(rect => new Rectangle(new Point(rect.X - shift.X, rect.Top - shift.Y), rect.Size)).ToList();
		}

		private bool CanAdd(Rectangle rect)
		{
			return prevRects.All(r => !rect.IntersectsWith(r));
		}

		public IEnumerable<Rectangle> PutAllRectangles(IEnumerable<Size> sizes)
		{
			foreach (var size in sizes)
				PutNextRectangle(size);
			return prevRects;
		}
	} 
}
