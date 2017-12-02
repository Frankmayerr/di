using System;
using System.Collections.Generic;
using System.Drawing;
using CommandLine;
using CommandLine.Text;
using NUnit.Framework;
using TextCloudPainter;

namespace ConsoleTagsCloudApp
{
	public class Options
	{
		[Option('i', "input_file", HelpText = "Text for building words cloud", Required = true)]
		public string InputFile { get; set; }

		[Option('o', "output_file", HelpText = "Image name for words cloud", Required = true)]
		public string ImageOutputFile { get; set; }

		//[Option('w', "word_conditions", HelpText = "List of conditions - will it be added or not")]
		//public List<Func<string, bool>> WordsConditions { get; set; }

		[OptionArray('b', "background_color", DefaultValue = new[] { "255", "0", "0", "0" },
			HelpText = ("Background color in specified format:\n"))]
		public string[] BackgroundColor { get; set; }

		[Option('s', "text_brush_selector", DefaultValue = BrushTypes.Random, HelpText = "Kind of brush to use. Possible types are Random, ...")]
		public BrushTypes TextBrushSelector { get; set; }

		[Option('f', "font", DefaultValue = "Times New Roman", HelpText = "Font of text in the image")]
		public string FontFamily { get; set; }

		[Option('w', "image_width", DefaultValue = 5000, HelpText = "Width of words cloud image")]
		public int ImageWidth { get; set; }

		[Option('h', "image_height", DefaultValue = 5000, HelpText = "Height of words cloud image")]
		public int ImageHeight { get; set; }

		[Option('n', "max_words_amount", DefaultValue = 100,
			HelpText = "Number of words to draw in words cloud. Don't have enough words - draw all of them.")]
		public int MaxWordsAmount { get; set; }

		[HelpOption]
		public string GetUsage()
		{
			return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
		}
	}
}
