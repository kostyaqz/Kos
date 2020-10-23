using System;
using System.Text;
using NUnit.Framework;

namespace TableParser
{
	[TestFixture]
	public class QuotedFieldTaskTests
	{
		// наверное, кейсов бы, когда строка кончается без закрытия токена, тоже хочется.
		// и кейсов с каким-то концом строки после конца токена
		[TestCase("''", 0, "", 2)]
		[TestCase("'a'", 0, "a", 3)]
		[TestCase("'/asdw'", 0, "/asdw", 7)]
		[TestCase("\"a 'b' 'c' d\"", 0, "a 'b' 'c' d", 13)]
		[TestCase("'\"1\" \"2\" \"3\"'", 0, "\"1\" \"2\" \"3\"", 13)]
		[TestCase("Крутое начало строки'\"1\" \"2\" \"3\"'", 20, "\"1\" \"2\" \"3\"", 13)]
		[TestCase("\"1\"", 0, "1", 3)]
		[TestCase(@"'\\""'", 0, @"\""", 5)]
		[TestCase(@"""\\'""", 0, @"\'", 5)]
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
			var currentIndex = startIndex + 1;// в цикле есть переменная i же

			for (var i = currentIndex; i < line.Length; i++)
			{
				if (IsQuotedTokenEnd(line, startIndex, currentIndex))
				{
					currentIndex++;
					break;
				}

				builder.Append(line[currentIndex]);
				currentIndex++;
			}

			var value = Unescape(builder.ToString());
			return new Token(value, startIndex, currentIndex - startIndex);
		}

		// вроде Replace линейный, и такое использование только увеличивает коэффициент, но все равно некруто.
		// почему бы просто при главном проходе не пропускать слеши? 
		private static string Unescape(string line)  
		{
			return line.Replace(@"\\", @"\")
				// но ведь это неправильно. вообще, несколько replace подряд с одними и теми же символами -
				// всегда повод рассмотреть их очень аккуратно.
				// Кусочек где-то в токене, выделенном одинарными кавычками:
				// '     \\"      '
				// буквально два слеша и двойная кавычка. с точки зрения логики токенов, это экранированный слеш и 
				// кавычка, не нуждающаяся в экранировании. Unesape же сделает из них одну кавычку. закодил этот кейс 
				// тесткейсом
				// плохие новости - изменением порядка Replace-ов это не починить.
				.Replace("\\\"", "\"")
				.Replace("\\\'", "\'");
		}

		private static bool IsQuotedTokenEnd(string line, int startIndex, int currentIndex)
		{
			// ну я понимаю, смотрим, тот ли это символ (кавычка), с какого токен начался.
			// потом смотрим, что кавычка эта не экранирована.
			// но. если это нужная кавычка, но она экранирована, то почему мы ее не всегда пропускаем?
			// почему, если до этого было четное количество кавычек, признаём этот символ концом токена?
			// в написанных тестах нет такого кейса. к слову, кейса, когда кавычка нужная, но экранирована, тоже нет.
			// то есть, слеши не считаются в написанных кейсах
			if (line[currentIndex] != line[startIndex]) return false;

			if (line[currentIndex - 1] != '\\') return true;

			var slashesCount = CalculateSlashesCount(line, startIndex, currentIndex);
			return slashesCount % 2 == 0;
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