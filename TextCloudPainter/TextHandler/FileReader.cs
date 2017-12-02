using System;
using System.Collections.Generic;
using System.IO;

namespace TextCloudPainter.TextHandler
{
	public class FileReader:IFileReader
	{
		private string filename;

		public FileReader(string filename)
		{
			this.filename = filename;
		}

		public List<string> GetText()
		{
			var text = new List<string>();
			try
			{
				var line = "";
				using (var sr = new StreamReader(filename, System.Text.Encoding.Default))
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