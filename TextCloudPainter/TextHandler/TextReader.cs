using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TextCloudPainter.TextHandler
{
	public class TextReader:ITextReader
	{
		private readonly IFileReader fileReader;

		public TextReader(IFileReader fileReader)
		{
			this.fileReader = fileReader;
		}


		public List<string> GetWordsListFromText()
		{
			var text = fileReader.GetText();
			var separators = new string[] {",", ".", "!", "?", "\'", " ", "\'s", ":", ";", "(", ")",
				"-", "\"", "<", ">", "]", "[", "{", "}", "*" };
			var words = new List<string>();
			foreach (var line in text)
			{
				words.AddRange(line.Split(separators, StringSplitOptions.RemoveEmptyEntries)
					.Where(IsWord));
			}
			return words;
		}

		private static bool IsWord(string str)
		{
			return str.All(char.IsLetter);
		}
	}
}