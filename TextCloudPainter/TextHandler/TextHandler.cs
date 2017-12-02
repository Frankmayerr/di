using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Castle.Core.Smtp;

namespace TextCloudPainter.TextHandler
{
	public class TextHandler:ITextHandler
	{
		private readonly List<string> words;
		private readonly ITextReader textReader;
		private readonly int maxWordAmount;

		public TextHandler(ITextReader textReader, IWordHandler wordHandler, int maxWordAmount)
		{
			this.maxWordAmount = maxWordAmount;
			words = textReader.GetWordsListFromText();
			this.textReader = textReader;
			words = wordHandler.HandleWordsList(words);
		}

		public Dictionary<string, double> GetWordFrequencyPercentageStatistic()
		{
			var wordsStat = GetStatisticsFromWordsList(words);
			var frequencySum = wordsStat.Values.Sum();
			var res = new Dictionary<string, double>();
			foreach (var word in wordsStat)
				res[word.Key] = (float)word.Value / frequencySum;
			return res;
		}

		public Dictionary<string, int> GetStatisticsFromTextFile()
		{
			return GetStatisticsFromWordsList(words);
		}

		public Dictionary<string, int> GetStatisticsFromWordsList(List<string> wordsList)
		{
			var res = new Dictionary<string, int>();
			foreach (var word in wordsList)
				if (!res.ContainsKey(word))
					res[word] = 1;
				else
					res[word]++;
			return res.OrderByDescending(x => x.Value).Take(maxWordAmount).ToDictionary(x => x.Key, x => x.Value);
		}
	}
}