using Autofac.Core;
using NUnit.Framework;
using TextCloudPainter.Program;

namespace TextCloudPainter
{
	[TestFixture]
	public class ProgramTests
	{
		[Test]
		public void Run()
		{
			var program = new ConsoleProgram();
			string[] args = new[]
			{
				"-i", "Bukowsky_short.txt",
				"-o", "buk.png",
			};
			ConsoleProgram.Main(args);
		}
	}
}