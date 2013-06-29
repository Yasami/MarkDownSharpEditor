using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MarkDownSharpEditor.Models
{
	class Decoration
	{
		public Color Foreground { get; set; }

		public Color Background { get; set; }

		public bool Bold { get; set; }

		public bool Italic { get; set; }
	}
}
