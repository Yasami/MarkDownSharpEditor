using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using MarkDownSharpEditor.Models;

namespace MarkDownSharpEditor.Models
{
	[TestFixture]
	public class DocTypeTest
	{
		static TestCaseData[] ConstantFieldsTestCases = new[]
		{ 
			new TestCaseData(DocType.Strict, Dtd.Html401Strict, @"<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.01//EN' 'http://www.w3.org/TR/html4/strict.dtd'>"),
			new TestCaseData(DocType.Transitional, Dtd.Html401Transitional, @"<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN' 'http://www.w3.org/TR/html4/loose.dtd'>"),
			new TestCaseData(DocType.Frameset, Dtd.Html401Frameset, @"<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.01 Frameset//EN' 'http://www.w3.org/TR/html4/frameset.dtd'>"),
			new TestCaseData(DocType.HTML5, Dtd.Html5, @"<!DOCTYPE html>")
		};

		[TestCaseSource("ConstantFieldsTestCases")]
		public void ConstantFieldsTest(DocType docType, Dtd dtd, string declaration)
		{
			Assert.That(docType.Dtd, Is.EqualTo(dtd));
			Assert.That(docType.Declaration, Is.EqualTo(declaration));
		}

		static TestCaseData[] GetDocTypeTestCases = new[] 
		{
			new TestCaseData(Dtd.Html401Strict).Returns(DocType.Strict),
			new TestCaseData(Dtd.Html401Transitional).Returns(DocType.Transitional),
			new TestCaseData(Dtd.Html401Frameset).Returns(DocType.Frameset),
			new TestCaseData(Dtd.Html5).Returns(DocType.HTML5),
			new TestCaseData((Dtd)0x10).Returns(DocType.HTML5).SetDescription("Default value is HTML5"),
		};

		[TestCaseSource("GetDocTypeTestCases")]
		public DocType GetDocTypeTest(Dtd dtd)
		{
			return DocType.GetDocType(dtd);
		}

		[Test]
		public void GetDocTypeListTest()
		{
			var expected = new [] {DocType.Strict, DocType.Transitional, DocType.Frameset, DocType.HTML5};
			var actual = DocType.GetDocTypeList();
			CollectionAssert.AreEquivalent(expected, actual);
		}
	}
}
