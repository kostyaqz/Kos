using System;
using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    internal static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            return SplitSentencesIntoWords(SplitTextIntoSentences(text));
        }

        private static List<List<string>> SplitSentencesIntoWords(string[] manySentences)
        {
            var sentencesList = new List<List<string>>();

            foreach (var sentence in manySentences)
            {
                var words = new List<string>();
                var counter = 0;
                var newWord = new StringBuilder();

                while (counter < sentence.Length)
                {
                    if (char.IsLetter(sentence, counter) || sentence[counter] == '\'')
                    {
                        newWord.Append(char.ToLower(sentence[counter]));
                    }
                    else
                    {
                        if (newWord.Length != 0)
                        {
                            words.Add(newWord.ToString());
                            newWord.Clear();
                        }
                    }

                    counter++;
                }

                if (newWord.Length != 0) words.Add(newWord.ToString());

                if (words.Count > 0) sentencesList.Add(words);
            }

            return sentencesList;
        }

        private static string[] SplitTextIntoSentences(string text)
        {
            return text.Split('.', '!', '?', ';', ':', '(', ')');
        }
    }
}