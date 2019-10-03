﻿using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAnalysis
{
    internal static class TextGeneratorTask
    {
        public static string ContinuePhrase(Dictionary<string, string> nextWords, string phraseBeginning, int wordsCount)
        {
            //Понимаем сколько слов содержится в исходной фразе
            var words = new List<string>();
            var remainingWordsNumber = 0;
            words = phraseBeginning.Split().ToList();
  
            
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            
            
            //Понимаем сколько слов у нас осталось, от этого начинаем мутить цикл
            
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            
            
            //находим 2 последних слова фразы, по ним ищем ключ в словаре
            

            while (wordsCount > remainingWordsNumber)
            {
                var twoLastWordsOfPhrase = words[words.Count - 1] + words[words.Count - 2];
                var lastWordOfPhrase = words[words.Count - 1];
                if (nextWords.ContainsKey(twoLastWordsOfPhrase))
                {
                    ////если есть, то записываем значение этого ключа в конец фразы
                    phraseBeginning = phraseBeginning + ' ' + nextWords[twoLastWordsOfPhrase];
                    words.Add(nextWords[twoLastWordsOfPhrase]);
                }
                ////если нет, то находим последнее слово фразы
                //по последнему слову фразы ищем ключ в словаре
                else
                {
                    if (nextWords.ContainsKey(lastWordOfPhrase))
                    {
                        //если нашли, то записываем его в итоговую фразу
                        phraseBeginning = phraseBeginning + ' ' + nextWords[lastWordOfPhrase];
                        words.Add(nextWords[lastWordOfPhrase]);
                    }
                    else
                    {
                        //если нет, то обрываем весь цикл и возвращаем фразу
                        break;
                    }
                }
                remainingWordsNumber++;
            }

            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            return phraseBeginning;
        }
    }
}



















