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
				"-i", "input.txt",
				"-o", "output.png",
			};
			ConsoleProgram.Main(args);
		}
	}
}