using System.Collections.Generic;
using System.Drawing;

namespace TextCloudPainter
{
	public interface IWordLayouter
	{
		int ImageWidth { get; }
		int ImageHeight { get; }
		List<WordInRectangle> GetWordsInCloud();
	}
}