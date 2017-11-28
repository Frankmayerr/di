using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Constraints;
using NHunspell;

namespace TextCloudPainter.TextHandler
{
	public class WordHandler:IWordHandler
	{
		private List<Func<string, bool>> wordConditions = new List<Func<string, bool>>();
		private List<Func<string, string>> wordConversions = new List<Func<string, string>>();
		private HashSet<string> badWords = new HashSet<string>();
		public int WordMinLength = 20;

		public WordHandler(Hunspell hunspell = null)
		{
			wordConditions.Add(word => word.Length >= WordMinLength);
			wordConditions.Add(word => !badWords.Contains(word));
			wordConversions.Add(word => word.ToLowerInvariant());
			// добавить преобразование в начальную форму и определение по части речи
		}

		public void AddNewWordCondition(Func<string, bool> cond)
		{
			wordConditions.Add(cond);
		}

		public void AddNewWordConversion(Func<string, string> cond)
		{
			wordConversions.Add(cond);
		}

		public void AddBadWord(string word)
		{
			badWords.Add(word);
		}

		public string GetHandledWord(string word)
		{
			return wordConversions.Aggregate(word, (current, conversion) => conversion(current));
		}

		public bool IsGoodWord(string word)
		{
			return wordConditions.All(condition => condition(word));
		}

		public List<string> HandleWordsList(List<string> words)
		{
			return (from word in words where IsGoodWord(word) select GetHandledWord(word)).ToList();
		}

	}
}