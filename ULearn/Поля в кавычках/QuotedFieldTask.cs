using System.Text;
using NUnit.Framework;

namespace TableParser
{
	[TestFixture]
	public class QuotedFieldTaskTests
	{
		[TestCase("''", 0, "", 2)]
		[TestCase("'a'", 0, "a", 3)]
		[TestCase("'/asdw'", 0, "/asdw", 7)]
		[TestCase("\"a 'b' 'c' d\"", 0, "a 'b' 'c' d", 13)]
		[TestCase("'\"1\" \"2\" \"3\"'", 0, "\"1\" \"2\" \"3\"", 13)]
		[TestCase("Крутое начало строки'\"1\" \"2\" \"3\"'", 20, "\"1\" \"2\" \"3\"", 13)]
		[TestCase("\"1\"", 0, "1", 3)]
		[TestCase(@"'\\""'", 0, @"\""", 5)]
		[TestCase(@"""\\'""", 0, @"\'", 5)]
		[TestCase(@"""\\'""aa", 0, @"\'", 5)]
		[TestCase(@"""\\'", 0, @"\'", 4)]
		[TestCase(@"'\\\\\\\\\\'", 0, @"\\\\\", 12)]
		[TestCase(@"'aaaaa\\'", 0, @"aaaaa\", 9)]
		[TestCase(@"'aaa\\'aa", 0, @"aaa\", 7)]
		public void Test(string line, int startIndex, string expectedValue, int expectedLength)
		{
			var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
			Assert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
		}
	}

	internal class QuotedFieldTask
	{
		public static Token ReadQuotedField(string line, int startIndex)
		{
			var builder = new StringBuilder();
			var currentIndex = startIndex + 1;

			while (currentIndex < line.Length)
			{
				if (IsQuotedTokenEnd(line, startIndex, currentIndex))
				{
					currentIndex++;
					break;
				}

				builder.Append(line[currentIndex]);
				currentIndex++;
			}

			return new Token(Unescape(builder), startIndex, currentIndex - startIndex);
		}

		private static string Unescape(StringBuilder line)
		{
			for (var i = 1; i < line.Length; i++)
				if ((line[i - 1] == '\\') & (line[i] == '\\' || line[i] == '"' || line[i] == '\''))
					line.Remove(i - 1, 1);

			return line.ToString();
		}

		private static bool IsQuotedTokenEnd(string line, int startIndex, int currentIndex)
		{
			if (line[currentIndex] != line[startIndex]) return false;
			if (line[currentIndex - 1] != '\\') return true;
			return CalculateSlashesCount(line, startIndex, currentIndex) % 2 == 0;
		}

		private static int CalculateSlashesCount(string line, int startIndex, int currentIndex)
		{
			var slashCount = 0;

			for (var i = currentIndex - 1; i >= startIndex; i--)
				if (line[i] == '\\') slashCount++;
				else break;

			return slashCount;
		}
	}
}