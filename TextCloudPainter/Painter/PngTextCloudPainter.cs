﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using NUnit.Framework;

namespace TextCloudPainter
{
	public class PngTextDrawer : ITextDrawer
	{
		private readonly Size imageSize;
		private readonly Color backgroundColor;
		private readonly FontFamily textFontFamily;
		private readonly IWordLayouter wordsPlacer;
		private readonly IBrushMaker brushMaker;

		public PngTextDrawer(IWordLayouter wordsPlacer, IBrushMaker brushMaker, FontFamily fontFamily,
			Color backgroundColor, Size imageSize)
		{
			this.wordsPlacer = wordsPlacer;
			this.brushMaker = brushMaker;
			this.backgroundColor = backgroundColor;
			textFontFamily = fontFamily;
			this.imageSize = imageSize;
		}

		public void WritePictureToFile(Dictionary<string, double> wordsStatistics, string filename)
		{
			var bitmap = new Bitmap(imageSize.Width, imageSize.Height);
			var graphics = Graphics.FromImage(bitmap);
			graphics.FillRectangle(new SolidBrush(backgroundColor), new RectangleF(0, 0, imageSize.Width, imageSize.Height));
			var sizes = GetWordsRelativeSizes(wordsStatistics.Keys, graphics);
			var wordsFormatted = wordsPlacer.GetWordsInCloud(wordsStatistics, sizes);
			if (wordsFormatted.Count != 0)
			{
				foreach (var word in wordsFormatted)
				{
					var brush = brushMaker.GetBrush();
					DrawFormattedWord(graphics, word, brush);
				}
			}

			try
			{
				bitmap.Save(filename);
			}
			catch
			{
				throw new ArgumentException("Could not write to file");
			}
		}

		private Dictionary<string, SizeF> GetWordsRelativeSizes(IEnumerable<string> words, Graphics g)
		{
			var exampleFont = new Font(textFontFamily, 10);
			return words.ToDictionary(word => word, word => g.MeasureString(word, exampleFont));
		}

		private void DrawFormattedWord(Graphics graphics, WordInRectangle word, Brush brush)
		{
			var font = GetFontOfRightSize(word, graphics, textFontFamily);
			graphics.DrawString(word.Word, font, brush, word.Rectangle);
		}

		private static Font GetFontOfRightSize(WordInRectangle word, Graphics g, FontFamily fontFamily)
		{
			var fontSize = 1;
			var curFont = new Font(fontFamily, fontSize);
			var curArea = GetAreaFromWord(word.Word, g, curFont);
			var maxArea = word.Rectangle.Height * word.Rectangle.Width;
			while (curArea <= maxArea)
			{
				fontSize++;
				curFont = new Font(fontFamily, fontSize);
				curArea = GetAreaFromWord(word.Word, g, curFont); 
			}
			if (curArea > maxArea)
				curFont = new Font(fontFamily, --fontSize);
			return curFont;
		}

		private static float GetAreaFromWord(string word, Graphics g, Font font) =>
			g.MeasureString(word, font).Height * g.MeasureString(word, font).Width;
	}
}