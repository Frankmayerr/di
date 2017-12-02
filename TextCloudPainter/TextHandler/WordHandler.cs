using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using FluentAssertions.Equivalency;
using NUnit.Framework.Constraints;
using NHunspell;

namespace TextCloudPainter.TextHandler
{
	public class WordHandler:IWordHandler
	{
		private List<Func<string, bool>> wordConditions = new List<Func<string, bool>>();
		private List<Func<string, string>> wordConversions = new List<Func<string, string>>();
		private HashSet<string> badWords = new HashSet<string>();
		public int WordMinLength = 3;

		public WordHandler()
		{
			wordConditions.Add(word => word.Length >= WordMinLength);
			wordConditions.Add(word => !badWords.Contains(word));
			wordConversions.Add(word => word.ToLowerInvariant());
			//wordConversions.Add(word =>
			//{
			//	using (var hunspell = new Hunspell("dicts/en_US.aff", "dicts/en_US.dic"))
			//	{
			//		var stems = hunspell.Stem(word);
			//		return stems.Any() ? stems[0] : word;
			//	}
			//});
			//wordConversions.Add(word =>
			//{
			//	var sw = new StringWriter();
			//	Console.SetOut(sw);
			//	Console.SetError(sw);
			//	using (var hunspell = new Hunspell("dicts/ru.aff", "dicts/ru.dic"))
			//	{
			//		var stems = hunspell.Stem(word);
			//		return stems.Any() ? stems[0] : word;
			//	}
			//});
		}

		public void AddNewWordCondition(Func<string, bool> cond)
		{
			wordConditions.Add(cond);
		}

		public void AddNewWordConversion(Func<string, string> conv)
		{
			wordConversions.Add(conv);
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