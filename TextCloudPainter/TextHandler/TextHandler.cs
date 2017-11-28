using System.Collections.Generic;
using System.Linq;

namespace TextCloudPainter.TextHandler
{
	public class TextHandler:ITextHandler
	{
		private readonly IFileReader fileReader;
		private readonly ITextReader textReader;
		private readonly IWordHandler wordHandler;

		public TextHandler(IFileReader fileReader, ITextReader textReader, IWordHandler wordHandler)
		{
			this.fileReader = fileReader;
			this.textReader = textReader;
			this.wordHandler = wordHandler;
		}

		public Dictionary<string, double> GetWordFrequencyPercentageStatistic(Dictionary<string, int> words)
		{
			var frequencySum = words.Values.Sum();
			var res = new Dictionary<string, double>();
			foreach (var word in words)
				res[word.Key] = word.Value / frequencySum;
			return res;
		}

		public Dictionary<string, int> GetStatisticsFromTextFile(string filename)
		{
			var text = fileReader.GetText(filename);
			var words = textReader.GetWordsListFromText(text);
			words = wordHandler.HandleWordsList(words);
			return GetStatisticsFromWordsList(words);
		}

		public Dictionary<string, int> GetStatisticsFromWordsList(List<string> words)
		{
			var res = new Dictionary<string, int>();
			foreach (var word in words)
				res[word]++;
			return res;
		}
	}
}