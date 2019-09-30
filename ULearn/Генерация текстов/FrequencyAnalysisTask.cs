using System.Collections.Generic;

namespace TextAnalysis
{
	internal static class FrequencyAnalysisTask
	{
		public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
		{
			var dictionaryCounter = new Dictionary<string, Dictionary<string, int>>();
			var returnedValue = new Dictionary<string, string>();

			foreach (var sentence in text)
			{
				var counter = 1;
				foreach (var word in sentence)
				{
					if (counter < sentence.Count)
						NGrammCounter(dictionaryCounter, word, sentence[counter]);
					counter++;
				}

				var n = 0;
				while (n + 2 < sentence.Count)
				{
					var twoWordsKey = sentence[n] + " " + sentence[n + 1];
					var thirdWordValue = sentence[n + 2];
					if (n + 2 < sentence.Count)
						NGrammCounter(dictionaryCounter, twoWordsKey, thirdWordValue);
					n++;
				}

				returnedValue = GetFrequencyDictionary(dictionaryCounter);
			}

			return returnedValue;
		}
		private static void NGrammCounter(Dictionary<string, Dictionary<string, int>> dictionaryCounter, string twoWordsKey,
			string thirdWordValue)
		{
			if (dictionaryCounter.ContainsKey(twoWordsKey) &&
			    dictionaryCounter[twoWordsKey].ContainsKey(thirdWordValue))
			{
				dictionaryCounter[twoWordsKey][thirdWordValue]++;
			}
			else
			{
				if (dictionaryCounter.ContainsKey(twoWordsKey))
				{
					dictionaryCounter[twoWordsKey].Add(thirdWordValue, 1);
				}
				else
				{
					dictionaryCounter.Add(twoWordsKey, new Dictionary<string, int>());
					dictionaryCounter[twoWordsKey].Add(thirdWordValue, 1);
				}
			}
		}
		
		public static Dictionary<string, string> GetFrequencyDictionary(
			Dictionary<string, Dictionary<string, int>> dictionaryCounter)
		{
			var maxValueOfChastota = 0;
			var frequencyDictionary = new Dictionary<string, string>();
			foreach (var key1 in dictionaryCounter)
			foreach (var valueOfDictionaryCounter in key1.Value)
				if (frequencyDictionary.ContainsKey(key1.Key))
				{
					if (valueOfDictionaryCounter.Value > maxValueOfChastota)
					{
						maxValueOfChastota = valueOfDictionaryCounter.Value;
						frequencyDictionary[key1.Key] = valueOfDictionaryCounter.Key;
					}
					else if (valueOfDictionaryCounter.Value == maxValueOfChastota &&
					         string.CompareOrdinal(valueOfDictionaryCounter.Key, frequencyDictionary[key1.Key]) < 0)
					{
						frequencyDictionary[key1.Key] = valueOfDictionaryCounter.Key;
					}
				}
				else
				{
					frequencyDictionary.Add(key1.Key, valueOfDictionaryCounter.Key);
					maxValueOfChastota = valueOfDictionaryCounter.Value;
				}

			return frequencyDictionary;
		}
	}
}