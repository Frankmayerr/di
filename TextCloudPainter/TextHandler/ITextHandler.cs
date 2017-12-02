using System.Collections.Generic;

namespace TextCloudPainter.TextHandler
{
	public interface ITextHandler
	{
		Dictionary<string, double> GetWordFrequencyPercentageStatistic();
		Dictionary<string, int> GetStatisticsFromTextFile();
	}
}