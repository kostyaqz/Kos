using System.Collections.Generic;
using NUnit.Framework;

namespace Passwords
{
	public class CaseAlternatorTask
	{
		//Тесты будут вызывать этот метод

		public static List<string> AlternateCharCases(string lowercaseWord)
		{
			var result = new List<string>();
			AlternateCharCases(lowercaseWord.ToCharArray(), 0, result);
			return result;
		}

		private static void AlternateCharCases(char[] word, int startIndex, List<string> result)
		{
			if (startIndex == word.Length)
			{
				AddNewWord(word, result);
				return;
			}

			if (char.IsLetter(word[startIndex]))
				IterateOverAllRegistersForEachLetter(word, startIndex, result);
			else
				AlternateCharCases(word, startIndex + 1, result);
		}

		private static void IterateOverAllRegistersForEachLetter(char[] word, int startIndex, List<string> result)
		{
			word[startIndex] = char.ToLower(word[startIndex]);
			AlternateCharCases(word, startIndex + 1, result);
			word[startIndex] = char.ToUpper(word[startIndex]);
			if (CheckIfUppercaseIsPossible(word, startIndex)) return;
			AlternateCharCases(word, startIndex + 1, result);
		}

		private static void AddNewWord(char[] word, List<string> result)
		{
			result.Add(new string(word));
			RemoveDuplicates(result);
		}

		private static void RemoveDuplicates(List<string> result)
		{
			var index = result.Count - 1;
			while (index > 0)
				if (result[index] == result[index - 1])
				{
					if (index < result.Count - 1)
						(result[index], result[result.Count - 1]) = (result[result.Count - 1], result[index]);
					result.RemoveAt(result.Count - 1);
					index--;
				}
				else
				{
					index--;
				}
		}

		private static bool CheckIfUppercaseIsPossible(char[] word, int startIndex)
		{
			return char.IsLower(word[startIndex]);
		}

		[TestFixture]
		private class TestPass
		{
			[TestCase("8", "8")]
			[TestCase("8345", "8345")]
			[TestCase("a", "a", "A")]
			[TestCase("aa", "aa", "aA", "Aa", "AA")]
			[TestCase("ab", "ab", "aB", "Ab", "AB")]
			[TestCase("cat", "cat", "caT", "cAt", "cAT", "Cat", "CaT", "CAt", "CAT")]
			[TestCase("mama", "mama", "mamA", "maMa", "maMA", "mAma", "mAmA", "mAMa", "mAMA", "Mama", "MamA", "MaMa",
				"MaMA", "MAma", "MAmA", "MAMa", "MAMA")]
			[TestCase("c5t", "c5t", "c5T", "C5t", "C5T")]
			[TestCase("2r", "2r", "2R")]
			[TestCase("r1", "r1", "R1")]
			[TestCase("c t", "c t", "c T", "C t", "C T")]
			[TestCase("kit123", "kit123", "kiT123", "kIt123", "kIT123", "Kit123", "KiT123", "KIt123", "KIT123")]
			[TestCase("0kit123", "0kit123", "0kiT123", "0kIt123", "0kIT123", "0Kit123", "0KiT123", "0KIt123",
				"0KIT123")]
			[TestCase("straße", "straße", "straßE", "strAße", "strAßE", "stRaße", "stRaßE", "stRAße", "stRAßE",
				"sTraße", "sTraßE", "sTrAße", "sTrAßE", "sTRaße", "sTRaßE", "sTRAße", "sTRAßE", "Straße", "StraßE",
				"StrAße", "StrAßE", "StRaße", "StRaßE", "StRAße", "StRAßE", "STraße", "STraßE", "STrAße", "STrAßE",
				"STRaße", "STRaßE", "STRAße", "STRAßE")]
			[TestCase("ⅲ ⅳ ⅷ", "ⅲ ⅳ ⅷ")]
			[TestCase("age ⅷ", "age ⅷ", "agE ⅷ", "aGe ⅷ", "aGE ⅷ", "Age ⅷ", "AgE ⅷ", "AGe ⅷ", "AGE ⅷ")]
			[TestCase("sße", "sße", "sßE", "Sße", "SßE")]
			[TestCase("עשעכף", "עשעכף")]
			public void Test1(string inputStr, params string[] okList)
			{
				var actualList = new List<string>();
				foreach (var e in okList)
					actualList.Add(e);
				var result = AlternateCharCases(inputStr);
				Assert.AreEqual(result, actualList);
			}
		}
	}
}