using System;
using System.Windows.Forms;

namespace MarkDownSharpEditorTest
{
	static class Program
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			NUnit.Gui.AppEntry.Main(new string[] { System.Windows.Forms.Application.ExecutablePath, "/run" });
		}
	}
}
