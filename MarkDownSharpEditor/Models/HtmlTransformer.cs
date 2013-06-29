using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace MarkDownSharpEditor.Models
{
	class HtmlTransformer : IDisposable
	{
		public string CssFilePath { get; set; }

		public string HtmlFilePath { get; set; }

		public bool IsNewFile { get; set; }

		public Encoding Encoding { get; set; }

		public string TempHtmlFilePath { get; set; }

		public void WriteHtml(string markdownText, string fileName)
		{
			string title = ""; // TODO
			var htmlText = Transform(markdownText, CssFilePath, title);
			//エンコーディングしつつbyte値に変換する（richEditBoxは基本的にutf-8 = 65001）
			//Encode and convert it to 'byte' value ( richEditBox default encoding is utf-8 = 65001 )
			byte[] bytesData = Encoding.GetBytes(htmlText);

			string result;
			//-----------------------------------
			// Write to temporay file
			if (IsNewFile)
			{
				//テンポラリファイルパスを取得する
				//Get temporary file path
				if (TempHtmlFilePath == "")
				{
					// TODO:
					// TempHtmlFilePath = .Get_TemporaryHtmlFilePath(_MarkdownFilePath);
				}
				//他のプロセスからのテンポラリファイルの参照と削除を許可して開く（でないと飛ぶ）
				//Open temporary file to allow references from other processes
				using (FileStream fs = new FileStream(TempHtmlFilePath,
					FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read | FileShare.Delete))
				{
					fs.Write(bytesData, 0, bytesData.Length);
					result = TempHtmlFilePath;
				}
			}
			//-----------------------------------
			// Navigate and view in browser
			else
			{
				//Write data as it is, if the editing data is no title  
				//result = Encoding.GetEncoding(CodePageNum).GetString(bytesData);
			}

		}

		public string Transform(string markdownText, string cssFilePath, string title)
		{
			var settings = AppSettings.Instance;
			string BackgroundColorString;
			string EncodingName;

			//マーキングの色づけ
			//Marker's color
			if (settings.fHtmlHighLightColor)
			{
				Color ColorBackground = Color.FromArgb(settings.HtmlHighLightColor);
				BackgroundColorString = ColorTranslator.ToHtml(ColorBackground);
			}
			else
			{
				BackgroundColorString = "none";
			}

			//指定のエンコーディング
			//Codepage
			int CodePageNum = settings.CodePageNumber;
			try
			{
				Encoding enc = Encoding.GetEncoding(CodePageNum);
				//ブラウザ表示に対応したエンコーディングか
				//Is the encoding supported browser?
				if (enc.IsBrowserDisplay == true)
				{
					EncodingName = enc.WebName;
				}
				else
				{
					EncodingName = "utf-8";
				}
			}
			catch
			{
				//エンコーディングの取得に失敗した場合
				//Default encoding if failing to get encoding
				EncodingName = "utf-8";
			}

			var parser = GetParser();
			var content = parser.Transform(markdownText);
			//-----------------------------------

			//表示するHTMLデータを作成
			//Creat HTML data
			var data = new HtmlData()
			{
				Dtd = Dtd.Html5, // TODO:
				Charset = EncodingName,
				CssPath = cssFilePath,
				BgColor = BackgroundColorString,
				Title = title,
				Content = content
			};
			var html = new HtmlTemplate(data).TransformText();

			//パースされた内容から編集行を探す
			//Search editing line in parsed data
			string destText;
			using (var sr = new StringReader(markdownText))
			{
				string line;
				using (var sw = new StringWriter())
				{
					while ((line = sr.ReadLine()) != null)
					{
						if (line.IndexOf("<!-- edit -->") >= 0)
						{
							line = "<div class='_mk'>" + line + "</div>";
						}
						sw.WriteLine(line);
					}
					destText = sw.ToString();
				}
			}
			return destText;
		}

		public static MarkdownDeep.Markdown GetParser()
		{
			return new MarkdownDeep.Markdown()
			{
				ExtraMode = MarkDownSharpEditor.AppSettings.Instance.fMarkdownExtraMode,
				SafeMode = false
			};
		}

		public void Dispose()
		{
			if (IsNewFile)
			{
				File.Delete(HtmlFilePath);
			}
		}
	}
}
