using System.Collections.Generic;

namespace TextAnalysis
{
	internal static class FrequencyAnalysisTask
	{
		public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
		{
			var nGramsCounter = new Dictionary<string, Dictionary<string, int>>();

			foreach (var sentences in text)
			{
				CountBigrams(sentences, nGramsCounter);

				CountTrigrams(sentences, nGramsCounter);
			}

			return GetFrequencyDictionary(nGramsCounter);
		}

		private static void CountBigrams(List<string> sentence,
			Dictionary<string, Dictionary<string, int>> bigramCounter)
		{
			var counter = 1;
			foreach (var words in sentence)
			{
				if (counter < sentence.Count)
					CountNgrams(bigramCounter, words, sentence[counter]);
				counter++;
			}
		}

		private static void CountTrigrams(List<string> sentence,
			Dictionary<string, Dictionary<string, int>> trigramCounter)
		{
			var counter = 0;
			while (counter + 2 < sentence.Count)
			{
				var twoWordsKey = sentence[counter] + " " + sentence[counter + 1];
				var thirdWordValue = sentence[counter + 2];
				if (counter + 2 < sentence.Count)
					CountNgrams(trigramCounter, twoWordsKey, thirdWordValue);
				counter++;
			}
		}

		private static void CountNgrams(Dictionary<string, Dictionary<string, int>> ngramCounter,
			string twoWordsKey,
			string thirdWordValue)
		{
			if (ngramCounter.ContainsKey(twoWordsKey) &&
			    ngramCounter[twoWordsKey].ContainsKey(thirdWordValue))
				ngramCounter[twoWordsKey][thirdWordValue]++;
			else
				AddNewNGram(ngramCounter, twoWordsKey, thirdWordValue);
		}

		private static void AddNewNGram(Dictionary<string, Dictionary<string, int>> dictionaryCounter,
			string twoWordsKey, string thirdWordValue)
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
			var frequencyCounter = 0;
			var frequencyDictionary = new Dictionary<string, string>();
			foreach (var key1 in dictionaryCounter)
				frequencyCounter = CompareLexicographically(key1, frequencyDictionary, frequencyCounter);

			return frequencyDictionary;
		}

		private static int CompareLexicographically(KeyValuePair<string, Dictionary<string, int>> key1,
			Dictionary<string, string> frequencyDictionary, int frequencyCounter)
		{
			foreach (var valueOfDictionaryCounter in key1.Value)
				if (frequencyDictionary.ContainsKey(key1.Key))
				{
					if (valueOfDictionaryCounter.Value > frequencyCounter)
					{
						frequencyCounter = valueOfDictionaryCounter.Value;
						frequencyDictionary[key1.Key] = valueOfDictionaryCounter.Key;
					}
					else if (valueOfDictionaryCounter.Value == frequencyCounter &&
					         string.CompareOrdinal(valueOfDictionaryCounter.Key, frequencyDictionary[key1.Key]) < 0)
					{
						frequencyDictionary[key1.Key] = valueOfDictionaryCounter.Key;
					}
				}
				else
				{
					frequencyDictionary.Add(key1.Key, valueOfDictionaryCounter.Key);
					frequencyCounter = valueOfDictionaryCounter.Value;
				}

			return frequencyCounter;
		}
	}
}