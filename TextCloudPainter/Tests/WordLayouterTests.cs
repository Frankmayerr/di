using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TextCloudPainter.TextHandler;

namespace TextCloudPainter.Tests
{
	[TestFixture]
	public class WordLayouterTests
	{
		private IWordLayouter wordLayouter;

		[SetUp]
		public void SetUp()
		{
			var rects = new List<Rectangle>()
			{
				new Rectangle(0, 0, 1, 1),
				new Rectangle(1, 1, 2, 2),
			};
			var wordsFrequency = new Dictionary<string, double>()
			{
				{"aaa", 0.3 },
				{"bbb", 0.7 }
			};
			var rectangleLayouter = new Mock<IRectangleLayouter>();
			rectangleLayouter.Setup(x => x.GetShiftedRectanglesWithPositiveCoordinates(It.IsAny<List<Rectangle>>()))
				.Returns(rects);

			var textHandler = new Mock<ITextHandler>();
			textHandler.Setup(x => x.GetWordFrequencyPercentageStatistic())
				.Returns(wordsFrequency);

			wordLayouter = new FrequencyDependentWordsLayouter(textHandler.Object, rectangleLayouter.Object, 
				new Size(100,100), new FontFamily("Times New Roman"));
		}


		[Test]
		public void FromWordLayouter_GetWordInCloud()
		{
			var expected = new List<WordInRectangle>()
			{
				new WordInRectangle("aaa", new Rectangle(0,0,1,1)),
				new WordInRectangle("bbb", new Rectangle(1,1,2,2))
			};
			var actual = wordLayouter.GetWordsInCloud();
			CollectionAssert.AreEquivalent(expected, actual);
		}
	}
}