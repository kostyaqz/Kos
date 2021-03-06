﻿using System.Collections.Generic;

namespace TextAnalysis
{
	internal static class FrequencyAnalysisTask
	{
		public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
		{
			var nGramsCounter = new Dictionary<string, Dictionary<string, int>>();

			foreach (var sentences in text)
			{
				// эти два прохода выглядят достаточно неоптимально, потенциально
				// их можно в 1 схлопнуть хотя бы, конечно, но так зато читаемость выше.
				// Прикольно балансировать тут между производительностью и читаемостью в зависимости от потребностей.
				CountBigrams(sentences, nGramsCounter);

				CountTrigrams(sentences, nGramsCounter);
			}

			return GetFrequencyDictionary(nGramsCounter);
		}

		private static void CountBigrams(List<string> sentence,
			Dictionary<string, Dictionary<string, int>> bigramCounter)
		{
			var counter = 1;
			// слово, а не слова тогда
			foreach (var word in sentence)
			{
				if (counter < sentence.Count)
					CountNgrams(bigramCounter, word, sentence[counter]);
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
			string phraseStart, string phraseEnd)
		{
			// формально, это именование некорректно для биграмм (для следующего тожеактуально)
			if (ngramCounter.ContainsKey(phraseStart) &&
			    ngramCounter[phraseStart].ContainsKey(phraseEnd))
				ngramCounter[phraseStart][phraseEnd]++;
			else
				AddNewNGram(ngramCounter, phraseStart, phraseEnd);
		}

		private static void AddNewNGram(Dictionary<string, Dictionary<string, int>> dictionaryCounter,
			string phraseStart, string phraseEnd)
		{
			// один и тот же вызов в if и else, можноо объединить
			if (!dictionaryCounter.ContainsKey(phraseStart))
				dictionaryCounter.Add(phraseStart, new Dictionary<string, int>());

			dictionaryCounter[phraseStart].Add(phraseEnd, 1);
		}

		public static Dictionary<string, string> GetFrequencyDictionary(
			Dictionary<string, Dictionary<string, int>> dictionaryCounter)
		{
			var frequencyDictionary = new Dictionary<string, string>();
			foreach (var phraseStart in dictionaryCounter)
			{
				var word = CompareLexicographically(phraseStart.Value);
				frequencyDictionary.Add(phraseStart.Key, word);
			}

			return frequencyDictionary;
		}

		private static string CompareLexicographically(Dictionary<string, int> phraseEndRepetitionCount)
		{
			string mostFrequentWord = null;
			var frequency = 0;
			foreach (var wordWithFrequency in phraseEndRepetitionCount)
				if (mostFrequentWord == null)
				{
					mostFrequentWord = wordWithFrequency.Key;
					frequency = wordWithFrequency.Value;
				}
				else
				{
					if (wordWithFrequency.Value > frequency)
					{
						mostFrequentWord = wordWithFrequency.Key;
						frequency = wordWithFrequency.Value;
					}
					else if (wordWithFrequency.Value == frequency &&
					         string.CompareOrdinal(wordWithFrequency.Key, mostFrequentWord) < 0)
					{
						mostFrequentWord = wordWithFrequency.Key;
					}
				}

			return mostFrequentWord;
		}
	}
}