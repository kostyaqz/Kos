using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAnalysis
{
	internal static class TextGeneratorTask
	{
		public static string ContinuePhrase(Dictionary<string, string> nextWords, string phraseBeginning,
			int wordsCount)
		{
			var remainingWordsNumber = 0;
			var words = phraseBeginning.Split().ToList();

			if (words.Count == 1)
			{
				if (nextWords.ContainsKey(words[words.Count - 1]) && wordsCount != 0)
				{
					AddWordToTheEndOfPhrase(nextWords, words[words.Count - 1], words);
					remainingWordsNumber++;
				}
				else
				{
					return phraseBeginning;
				}
			}

			while (wordsCount > remainingWordsNumber)
			{
				var twoLastWordsOfPhrase = words[words.Count - 2] + ' ' + words[words.Count - 1];
				var lastWordOfPhrase = words[words.Count - 1];

				if (nextWords.ContainsKey(twoLastWordsOfPhrase))
				{
					AddWordToTheEndOfPhrase(nextWords, twoLastWordsOfPhrase, words);
				}
				else
				{
					if (nextWords.ContainsKey(lastWordOfPhrase))
						AddWordToTheEndOfPhrase(nextWords, lastWordOfPhrase, words);
					else
						break;
				}

				remainingWordsNumber++;
			}

			return WriteWordsWithSpace(words);
		}


		private static void AddWordToTheEndOfPhrase(Dictionary<string, string> nextWords,
			string lastWord, List<string> words)
		{
			words.Add(nextWords[lastWord]);
		}

		private static string WriteWordsWithSpace(List<string> words)
		{
			var completePhrase = new StringBuilder();

			for (var i = 0; i < words.Count; i++)
			{
				completePhrase.Append(words[i]);
				if (i != words.Count - 1) completePhrase.Append(" ");
			}

			return completePhrase.ToString();
		}
	}
}