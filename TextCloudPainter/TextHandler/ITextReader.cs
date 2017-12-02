using System;
using System.Collections.Generic;

namespace TextCloudPainter.TextHandler
{
	public interface ITextReader
	{
		List<string> GetWordsListFromText();
	}
}