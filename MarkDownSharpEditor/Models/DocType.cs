using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkDownSharpEditor.Models
{
	public class DocType
	{
		public static readonly DocType Strict = new DocType(Dtd.Html401Strict, "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.01//EN' 'http://www.w3.org/TR/html4/strict.dtd'>");

		public static readonly DocType Transitional = new DocType(Dtd.Html401Transitional, "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN' 'http://www.w3.org/TR/html4/loose.dtd'>");

		public static readonly DocType Frameset = new DocType(Dtd.Html401Frameset, "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.01 Frameset//EN' 'http://www.w3.org/TR/html4/frameset.dtd'>");

		public static readonly DocType HTML5 = new DocType(Dtd.Html5, "<!DOCTYPE html>");

		private static readonly DocType[] _docTypes = new[] { Strict, Transitional, Frameset, HTML5 };

		public Dtd Dtd { get; private set; }

		public string Declaration { get; private set; }

		private DocType(Dtd dtd, string declaration)
		{
			Dtd = dtd;
			Declaration = declaration;
		}

		public static DocType GetDocType(Dtd dtd)
		{
			DocType result;
			switch (dtd)
			{
				case Dtd.Html401Strict:
					result = Strict;
					break;
				case Dtd.Html401Transitional:
					result = Transitional;
					break;
				case Dtd.Html401Frameset:
					result = Frameset;
					break;
				case Dtd.Html5:
					result = HTML5;
					break;
				default:
					result = HTML5;
					break;
			}
			return result;
		}

		/// <summary>
		/// 定義済みDOCTYPEの配列を返します。
		/// </summary>
		/// <returns>すべての定義済みDocTypeオブジェクトの配列</returns>
		public static DocType[] GetDocTypeList()
		{
			return _docTypes;
		}
	}
}
