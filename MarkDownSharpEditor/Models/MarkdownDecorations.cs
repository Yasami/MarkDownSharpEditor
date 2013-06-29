using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkDownSharpEditor.Models
{
	class MarkdownDecorations
	{
		public Decoration MainText { get; private set; }

		public Decoration LineBreak { get; private set; }

		public Decoration Headline1 { get; private set; }

		public Decoration Headline2 { get; private set; }

		public Decoration Headline3 { get; private set; }

		public Decoration Headline4 { get; private set; }

		public Decoration Blockquotes { get; private set; }

		public Decoration List { get; private set; }

		public Decoration CodeBlocks { get; private set; }

		public Decoration Horizontal { get; private set; }

		public Decoration Links { get; private set; }

		public Decoration Emphasis { get; private set; }

		public Decoration Code { get; private set; }

		public Decoration Images { get; private set; }

		public Decoration Comment { get; private set; }

		// TODO: Markdown Extra

		public string ToXml()
		{
			// TODO: Implementatio
			return "";
		}

		public void FromXml(string xml)
		{
		}
	}
}
