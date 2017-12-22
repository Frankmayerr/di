using System;
using System.Drawing;
using System.Linq;

namespace TextCloudPainter
{
	internal static class ColorParser
	{
		public static Color HexToColor(string hex)
		{
			return ColorTranslator.FromHtml(hex);
		}

		public static Color ChannelsToColor(int[] channels)
		{
			if (channels.Length == 4)
				return Color.FromArgb(channels[0], channels[1], channels[2], channels[3]);
			return Color.FromArgb(channels[0], channels[1], channels[2]);
		}

		public static Color Parse(string[] stringRepresentation)
		{
			if (stringRepresentation.Length == 1)
				return HexToColor(stringRepresentation[0]);
			if (stringRepresentation.Length == 4 || stringRepresentation.Length == 3)
				return ChannelsToColor(stringRepresentation.Select(int.Parse).ToArray());
			throw new ArgumentException("Unknown color representation");
		}
	}

}