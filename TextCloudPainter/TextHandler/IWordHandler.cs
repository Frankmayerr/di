using System;
using System.Collections.Generic;

namespace TextCloudPainter.TextHandler
{
	public interface IWordHandler
	{
		List<string> HandleWordsList(List<string> words);
		void AddBadWord(string word);
		void AddNewWordConversion(Func<string, string> cond);
		void AddNewWordCondition(Func<string, bool> cond);
	}
}