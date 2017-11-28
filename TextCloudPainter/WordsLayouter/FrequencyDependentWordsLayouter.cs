using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TextCloudPainter
{
	public class FrequencyDependentWordsLayouter:IWordLayouter
	{
		public readonly Size ImageSize;
		private readonly IRectangleLayouter layouter;
		private readonly FontFamily textFontFamily;
		private readonly Graphics graphics;
		public int ImageWidth => ImageSize.Width;
		public int ImageHeight => ImageSize.Height;
		public int Area => ImageHeight * ImageWidth;

		public FrequencyDependentWordsLayouter(IRectangleLayouter layouter, Size imageSize, FontFamily textFontFamily, Graphics graphics)
		{
			ImageSize = imageSize;
			this.textFontFamily = textFontFamily;
			this.graphics = graphics;
			this.layouter = layouter;
		}

		public List<WordInRectangle> GetWordsInCloud(Dictionary<string, double> wordsStatistics, Dictionary<string, SizeF> wordsRelevantSizes)
		{
			if (wordsStatistics.Count == 0)
				return new List<WordInRectangle>(new WordInRectangle[0]);
			var sizes = new List<Size>();
			foreach (var wordItem in wordsStatistics)
			{
				var wordFontSize = GetWordSizeFromFontParameters(wordItem.Key, graphics);
				var size = GetSizeFromWordStatistic(wordFontSize, wordItem.Value);
				sizes.Add(size.Ceil());
			}
			var rectangles = layouter
				.PutAllRectangles(sizes);
			rectangles = layouter.GetShiftedRectanglesWithPositiveCoordinates(rectangles);
			return wordsStatistics.Keys
				.Zip(rectangles, (word, rect) => new WordInRectangle(word, rect))
				.ToList();
		}

		private SizeF GetSizeFromWordStatistic(SizeF wordSize, double percentage)
		{
			var newWordArea = Area * percentage / 100;
			var wordArea = wordSize.Height * wordSize.Width;
			// system of two equations
			var newHeight = Math.Sqrt(wordArea * newWordArea) / wordSize.Width;
			var newWidth = Math.Sqrt(wordArea * newWordArea) / wordSize.Height;
			return new SizeF((float)newHeight, (float)newWidth);
		}

		private SizeF GetWordSizeFromFontParameters(string word, Graphics g)
		{
			var initialFont = new Font(textFontFamily, 10);
			return g.MeasureString(word, initialFont);
		}
	}
}