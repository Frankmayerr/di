using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Company.Common.Control;
using NUnit.Framework.Constraints;

namespace TextCloudPainter
{
	public class WordInRectangle
	{
		public string Word { get; }

		public Rectangle Rectangle { get; }

		public WordInRectangle(string word, Rectangle rectangle)
		{
			this.Word = word;
			this.Rectangle = rectangle;
		}

		public override bool Equals(object other)
		{
			try
			{
				var wir = (WordInRectangle) other;
				return Word == wir.Word && Rectangle == wir.Rectangle;
			}
			catch (Exception)
			{
				return false;
			}
			
		}
	}
}