using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAnalysis
{
    internal static class TextGeneratorTask
    {
        public static string ContinuePhrase(Dictionary<string, string> nextWords, string phraseBeginning, int wordsCount)
        {
            var words = new List<string>();
            var remainingWordsNumber = 0;
            words = phraseBeginning.Split().ToList();

            if (words.Count == 1)
            {
                if (nextWords.ContainsKey(words[words.Count - 1]) && wordsCount != 0)
                {
                    AddTheWordToTheEndOfThePhrase(nextWords, ref phraseBeginning, words[words.Count - 1], words);
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
                    AddTheWordToTheEndOfThePhrase(nextWords, ref phraseBeginning, twoLastWordsOfPhrase, words);
                }
                else
                {
                    if (nextWords.ContainsKey(lastWordOfPhrase))
                    {
                        AddTheWordToTheEndOfThePhrase(nextWords, ref phraseBeginning, lastWordOfPhrase, words);
                    }
                    else
                    {
                       break;
                    }
                }
                remainingWordsNumber++;
            }

            
            return phraseBeginning;
        }

        
        
        
        
        private static void AddTheWordToTheEndOfThePhrase(Dictionary<string, string> nextWords, ref string phraseBeginning,
            string lastWord, List<string> words)
        {
            phraseBeginning = phraseBeginning + ' ' + nextWords[lastWord];
            words.Add(nextWords[lastWord]);
        }
    }
}



















