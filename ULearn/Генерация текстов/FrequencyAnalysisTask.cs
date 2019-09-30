using System;
using System.Collections.Generic;

namespace TextAnalysis
{
    internal static class FrequencyAnalysisTask
    {
        static int maxValueOfChastota = 0;
        //static Dictionary<string, Dictionary<string, int>> dictionaryCounter = new Dictionary<string, Dictionary<string, int>>();
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var dictionaryCounter = new Dictionary<string, Dictionary<string, int>>();

            var returnedValue = new Dictionary<string, string>();
            
            //Выдели в отдельный метод с N граммами (а лучше в парочку - троечку)
            foreach (var sentence in text)
            {
                var counter = 1;
                foreach (var word in sentence)
                {
                    if (counter < sentence.Count)
                    {
                     //   NGrammCounter(word, sentence[counter]);

                        //Считаем сколько раз повторились ключ + значение в тексте
                        if (dictionaryCounter.ContainsKey(word) && dictionaryCounter[word].ContainsKey(sentence[counter]))
                        {
                            dictionaryCounter[word][sentence[counter]]++;
                        }
                        else
                        {
                            if (dictionaryCounter.ContainsKey(word))
                            {
                                dictionaryCounter[word].Add(sentence[counter], 1);
                            }
                            else
                            {
                                dictionaryCounter.Add(word, new Dictionary<string, int>());
                                dictionaryCounter[word].Add(sentence[counter], 1);
                            }
						
                        }
                    }
                    counter++;
                }
                

                
                var n = 0;
                while (n + 2 < sentence.Count)
                {
                    var twoWordsKey = sentence[n] + " " + sentence[n + 1];
                    var thirdWordValue = sentence[n + 2];
                    if (n + 2 < sentence.Count)
                    {
                        //NGrammCounter(twoWordsKey, thirdWordValue);
                        //Считаем сколько раз повторились ключ + значение в тексте
                        if (dictionaryCounter.ContainsKey(twoWordsKey) && dictionaryCounter[twoWordsKey].ContainsKey(thirdWordValue))
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
				    n++;
                }
                
                returnedValue = GetFrequencyDictionary(dictionaryCounter);

            }
            return returnedValue;
        }

//        public static void NGrammCounter(string externalKey, string internalKey)
//        {
//            if (dictionaryCounter.ContainsKey(externalKey) && dictionaryCounter[externalKey].ContainsKey(internalKey))
//            {
//                dictionaryCounter[externalKey][internalKey]++;
//            }
//            else
//            {
//                if (dictionaryCounter.ContainsKey(externalKey))
//                {
//                    dictionaryCounter[externalKey].Add(internalKey, 1);
//                }
//                else
//                {
//                    dictionaryCounter.Add(externalKey, new Dictionary<string, int>());
//                    dictionaryCounter[externalKey].Add(internalKey, 1);
//                }
//            }
//        }

        public static Dictionary<string, string> GetFrequencyDictionary(Dictionary<string, Dictionary<string, int>> dictionaryCounter)
        {
            var frequencyDictionary = new Dictionary<string, string>();
            foreach (var key1 in dictionaryCounter)
            {
                foreach (var valueOfDictionaryCounter in key1.Value)
                {
                    if (frequencyDictionary.ContainsKey(key1.Key))
                    {

                        if (valueOfDictionaryCounter.Value > maxValueOfChastota )
                        {
                            maxValueOfChastota = valueOfDictionaryCounter.Value;
                            frequencyDictionary[key1.Key] = valueOfDictionaryCounter.Key; 
                        }
                        else if (valueOfDictionaryCounter.Value == maxValueOfChastota && (string.CompareOrdinal(valueOfDictionaryCounter.Key, frequencyDictionary[key1.Key]) < 0))
                        {
                            frequencyDictionary[key1.Key] = valueOfDictionaryCounter.Key;
                        }
                    }
                    else
                    {
                        frequencyDictionary.Add(key1.Key, valueOfDictionaryCounter.Key);
                        maxValueOfChastota = valueOfDictionaryCounter.Value;
                    }
                }
            }
            return frequencyDictionary;
        }
    }
}