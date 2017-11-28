using System;
using System.Collections.Generic;
using System.IO;

namespace TextCloudPainter.TextHandler
{
	public class FileReader:IFileReader
	{
		public List<string> GetText(string filename)
		{
			var text = new List<string>();
			try
			{
				var line = "";
				using (var sr = new StreamReader(filename))
					while ((line = sr.ReadLine()) != null)
						text.Add(line);
			}
			catch (Exception e)
			{
				Console.WriteLine("Wrong Files (or not found)");
			}
			return text;
		}
	}
}