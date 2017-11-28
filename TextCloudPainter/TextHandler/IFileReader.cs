using System.Collections.Generic;

namespace TextCloudPainter.TextHandler
{
	public interface IFileReader
	{
		List<string> GetText(string filemane);
	}
}