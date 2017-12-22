using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web.UI;
using Autofac;
using Autofac.Core;
using CommandLine;
using ConsoleTagsCloudApp;
using TextCloudPainter.TextHandler;
using TextReader = TextCloudPainter.TextHandler.TextReader;

namespace TextCloudPainter.Program
{
	public class ConsoleProgram:IProgram
	{
		public static void Main(string[] args)
		{
			var baseDir = System.AppDomain.CurrentDomain.BaseDirectory;
			var options = new Options();

			if (!Parser.Default.ParseArguments(args, options))
			{
				Console.WriteLine("Arguments given are incorrect");
				return;
			}

			options.ParseOptions();

			options.InputFile = $"{baseDir}\\{options.InputFile}";
			options.ImageOutputFile = $"{baseDir}\\{options.ImageOutputFile}";

			var fontFamily = new FontFamily(options.FontFamily);

			var container = new ContainerBuilder();
			container.RegisterType<FileReader>()
				.WithParameter("filename", options.InputFile)
				.As<IFileReader>();
			container.RegisterType<TextReader>()
				.As<ITextReader>();
			container.RegisterType<WordHandler>()
				.As<IWordHandler>();
			container.RegisterType<TextHandler.TextHandler>()
				.WithParameter("maxWordAmount", options.MaxWordsAmount)
				.As<ITextHandler>();
			container.RegisterType<DistanceRectangleLayouter>()
				.WithParameter("center", new Point(0, 0))
				.As<IRectangleLayouter>();
			container.RegisterType<FrequencyDependentWordsLayouter>()
				.WithParameters(new List<Parameter>()
				{
					new NamedParameter("imageSize", new Size(options.ImageWidth, options.ImageHeight)),
					new NamedParameter("textFontFamily", fontFamily),

				})
				.As<IWordLayouter>();
			container.RegisterType<RandomBrushMaker>()
				.As<IBrushMaker>();
			container.RegisterType<PngTextDrawer>()
				.WithParameters(new List<Parameter>()
				{
					new NamedParameter("fontFamily", fontFamily),
					new NamedParameter("backgroundColor", ColorParser.Parse(options.BackgroundColor)),
					new NamedParameter("imageSize", new Size(options.ImageWidth, options.ImageHeight)),
					new NamedParameter("filename", options.ImageOutputFile)
				})
				.As<ITextDrawer>();

			var build = container.Build();
			var drawer = build.Resolve<ITextDrawer>();
			drawer.WritePictureToFile();
		}
	}
	
}
