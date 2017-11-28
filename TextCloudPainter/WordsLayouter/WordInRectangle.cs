using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TextCloudPainter
{
	public class WordInRectangle
	{
		public string Word { get; }

		public Rectangle Rectangle { get; }

		public WordInRectangle(string word, Rectangle rectangle)
		{
			this.Word = word;
			this.Rectangle = rectangle;
		}
	}
}