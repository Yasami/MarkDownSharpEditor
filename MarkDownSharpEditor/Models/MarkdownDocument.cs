using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarkDownSharpEditor.Properties;
using System.ComponentModel;

namespace MarkDownSharpEditor.Models
{
	class MarkdownDocument : INotifyPropertyChanged
	{
		private string _MarkdownFilePath;

		private string _TempHtmlFilePath;

		private string _CssFilePath;
		
		private string _savedText;

		private ICollection<MarkdownSyntaxKeyword> _keywords;

		private MarkdownDecorator _decorator;

		private HtmlTransformer _transformer;

		#region IsNewFile変更通知プロパティ
		private bool _IsNewFile;

		public bool IsNewFile
		{
			get
			{ return _IsNewFile; }
			set
			{
				if (_IsNewFile == value)
					return;
				_IsNewFile = value;
				if (_transformer != null)
					_transformer.IsNewFile = value;
				RaisePropertyChanged("IsNewFile");
			}
		}
		#endregion

		#region Modified変更通知プロパティ
		private bool _Modified;

		public bool Modified
		{
			get
			{ return _Modified; }
			set
			{
				if (_Modified == value)
					return;
				_Modified = value;
				RaisePropertyChanged("Modified");
			}
		}
		#endregion

		#region Text変更通知プロパティ
		private string _Text;

		public string Text
		{
			get
			{ return _Text; }
			set
			{
				if (_Text == value)
					return;
				_Text = value;
				RaisePropertyChanged("Text");
			}
		}
		#endregion

		public event PropertyChangedEventHandler PropertyChanged;

		public void Save()
		{
			// TODO: writeToFile
			_savedText = Text;
		}


		private void RaisePropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		private void OnTextChanged(string text)
		{
			var fileName = string.IsNullOrEmpty(_MarkdownFilePath) ? Resources.NoFileName : _MarkdownFilePath;

			if (_savedText != text)
			{
				fileName += Resources.FlagChanged; //"(更新)"; "(Changed)"
			}
			Text = fileName + " - " + Application.ProductName;
		}
	}
}
