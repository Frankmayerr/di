using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TextCloudPainter.TextHandler;

namespace TextCloudPainter
{
	public class FrequencyDependentWordsLayouter:IWordLayouter
	{
		public readonly Size ImageSize;
		private readonly IRectangleLayouter layouter;
		private readonly FontFamily textFontFamily;
		private readonly Graphics graphics;
		private readonly Dictionary<string, double> wordsStatistics;
		public int ImageWidth => ImageSize.Width;
		public int ImageHeight => ImageSize.Height;
		public int Area
		{
			get
			{
				var radius = Math.Min(ImageHeight, ImageWidth) / 2;
				var mes = (int) Math.Floor(radius * radius * 3.14159265359*0.8);
				return mes;
			}
		}

		public FrequencyDependentWordsLayouter(ITextHandler textHandler, IRectangleLayouter layouter, Size imageSize, FontFamily textFontFamily)
		{
			wordsStatistics = textHandler.GetWordFrequencyPercentageStatistic();
			ImageSize = imageSize;
			this.textFontFamily = textFontFamily;
			var image = new Bitmap(ImageWidth, ImageHeight);
			this.graphics = Graphics.FromImage(image);
			this.layouter = layouter;
		}

		public List<WordInRectangle> GetWordsInCloud()
		{
			if (wordsStatistics.Count == 0)
				return new List<WordInRectangle>(new WordInRectangle[0]);
			var sizes = new List<Size>();
			foreach (var wordItem in wordsStatistics)
			{
				var wordFontSize = GetWordSizeFromFontParameters(wordItem.Key, graphics);
				var size = GetSizeFromWordStatistic(wordFontSize, wordItem.Value);
				sizes.Add(size.Floor());
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
			var newWordArea = Area * percentage;
			var wordArea = wordSize.Height * wordSize.Width;
			// system of two equations
			var newHeight = Math.Sqrt(wordArea * newWordArea) / wordSize.Height;
			var newWidth = Math.Sqrt(wordArea * newWordArea) / wordSize.Width;
			return new SizeF((float)newHeight, (float)newWidth);
		}

		private SizeF GetWordSizeFromFontParameters(string word, Graphics g)
		{
			var initialFont = new Font(textFontFamily, 10);
			return g.MeasureString(word, initialFont);
		}
	}
}