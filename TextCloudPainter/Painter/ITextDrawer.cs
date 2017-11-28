using System.Collections.Generic;

namespace TextCloudPainter
{
	public interface ITextDrawer
	{
		void WritePictureToFile(Dictionary<string, double> wordsStatistics, string filename);
	}
}