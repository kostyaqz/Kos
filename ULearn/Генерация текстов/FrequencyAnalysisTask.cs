using System.Collections.Generic;

namespace TextAnalysis
{
	internal static class FrequencyAnalysisTask
	{
		public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
		{
			var nGrammsCounter = new Dictionary<string, Dictionary<string, int>>();
			
			foreach (var sentences in text)
			{
				GetBigrams(sentences, nGrammsCounter);

				GetTrigrams(sentences, nGrammsCounter);
			}

			return GetFrequencyDictionary(nGrammsCounter);
		}

		private static void GetBigrams(List<string> sentence,
			Dictionary<string, Dictionary<string, int>> bigramCounter)
		{
			var counter = 1;
			foreach (var words in sentence)
			{
				if (counter < sentence.Count)
					NGrammCounter(bigramCounter, words, sentence[counter]);
				counter++;
			}
		}

		private static void GetTrigrams(List<string> sentence,
			Dictionary<string, Dictionary<string, int>> trigramCounter)
		{
			var counter = 0;
			while (counter + 2 < sentence.Count)
			{
				var twoWordsKey = sentence[counter] + " " + sentence[counter + 1];
				var thirdWordValue = sentence[counter + 2];
				if (counter + 2 < sentence.Count)
					NGrammCounter(trigramCounter, twoWordsKey, thirdWordValue);
				counter++;
			}
		}

		private static void NGrammCounter(Dictionary<string, Dictionary<string, int>> dictionaryCounter,
			string twoWordsKey,
			string thirdWordValue)
		{
			if (dictionaryCounter.ContainsKey(twoWordsKey) &&
			    dictionaryCounter[twoWordsKey].ContainsKey(thirdWordValue))
			{
				dictionaryCounter[twoWordsKey][thirdWordValue]++;
			}
			else
			{
				AddNewValue(dictionaryCounter, twoWordsKey, thirdWordValue);
			}
		}

		private static void AddNewValue(Dictionary<string, Dictionary<string, int>> dictionaryCounter, string twoWordsKey, string thirdWordValue)
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

		public static Dictionary<string, string> GetFrequencyDictionary(
			Dictionary<string, Dictionary<string, int>> dictionaryCounter)
		{
			var maxValueOfChastota = 0;
			var frequencyDictionary = new Dictionary<string, string>();
			foreach (var key1 in dictionaryCounter) maxValueOfChastota = 
				SomeForeach(key1, frequencyDictionary, maxValueOfChastota);

			return frequencyDictionary;
		}

		private static int SomeForeach(KeyValuePair<string, Dictionary<string, int>> key1, Dictionary<string, string> frequencyDictionary, int maxValueOfChastota)
		{
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

			return maxValueOfChastota;
		}
	}
}