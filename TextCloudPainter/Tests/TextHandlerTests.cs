using System.Collections.Generic;
using System.Runtime.InteropServices;
using FluentAssertions;
using NUnit.Framework;
using Moq;
using TextCloudPainter.TextHandler;

namespace TextCloudPainter.Tests
{
	[TestFixture]
	public class TextHandlerTests
	{
		private List<string> text;
		private ITextReader textReader;
		private ITextHandler textHandler;

		[SetUp]
		public void SetUp()
		{
			text = new List<string> {"aaa bbb", "bbb"};

			var fileReader = new Mock<IFileReader>();
			fileReader.Setup(x => x.GetText()).Returns(text);

			textReader = new TextReader(fileReader.Object);

			var wordHandler = new Mock<IWordHandler>();
			wordHandler.Setup(x => x.HandleWordsList(It.IsAny<List<string>>()))
				.Returns(new List<string> {"aaa", "bbb", "bbb"});

			textHandler = new TextHandler.TextHandler(textReader, wordHandler.Object, 50);
		}

		[Test]
		public void TextReader_GetWordsList()
		{
			
			textReader.GetWordsListFromText().ShouldBeEquivalentTo(new List<string> ()
			{
				"aaa", "bbb", "bbb"
			});
		}

		[Test]
		public void textHandler_GetStatistic()
		{
			var excected = new Dictionary<string, int>(new Dictionary<string, int>()
			{
				{"aaa", 1},
				{"bbb", 2}
			});
			var actual = textHandler.GetStatisticsFromTextFile();
			actual["aaa"].Should().Be(excected["aaa"]);
			actual["bbb"].Should().Be(excected["bbb"]);
		}
	}
}