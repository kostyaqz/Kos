using System;
using System.Collections.Generic;



namespace Lectures
{

	internal class Program
	{
		static int maxValueOfChastota = 0;
		private static void Main()
		{
			var nGrammDictionary = new Dictionary<string, string>();
			var sentence = new List<string>();
			var dictionaryCounter = new Dictionary<string, Dictionary<string,int>>();


			
			

			sentence.Add("здарова");
			sentence.Add("мудила");
			sentence.Add("здарова");
			sentence.Add("мудила");
			sentence.Add("здарова");
			sentence.Add("мудила");
			sentence.Add("тупая");
			
			
			
			
			var counter = 1;

			foreach (var key in sentence)
			{
				if (counter < sentence.Count)
				{
//					//Записываем пару ключ-значение, если такого ключа еще не было (ключ - ОДНО слово)
//					if (!nGrammDictionary.ContainsKey(key))
//					{
//						nGrammDictionary[key] = sentence[counter];
//					}
//					else
//					{
//						//Сравниваем значения ключа из словаря и то значение, которое только что пришло и выбираем лексиграфически меньшее из них
//						if ((String.CompareOrdinal(sentence[counter],  nGrammDictionary[key])) < 0)
//						{
//							nGrammDictionary[key] = sentence[counter];
//						}
//					}
					//Считаем сколько раз повторились ключ + значение в тексте
					if (dictionaryCounter.ContainsKey(key) && dictionaryCounter[key].ContainsKey(sentence[counter]))
					{
						dictionaryCounter[key][sentence[counter]]++;
						
					}
					else
					{

						if (dictionaryCounter.ContainsKey(key))
						{
							dictionaryCounter[key].Add(sentence[counter], 1);
						}
						else
						{
							dictionaryCounter.Add(key, new Dictionary<string, int>());
							dictionaryCounter[key].Add(sentence[counter], 1);
						}
						
					}
				}
				counter++;
			}


			var n = 0;
			while (n + 2 < sentence.Count)
			{
				if (n + 2 < sentence.Count)
				{
//					//Записываем пару ключ-значение, если такого ключа еще не было (ключ - ДВА слова)
//					if (!nGrammDictionary.ContainsKey(sentence[n] + " " + sentence[n + 1]))
//					{
//						nGrammDictionary[sentence[n] + " " + sentence[n + 1]] = sentence[n + 2];
//					}
//					else
//					{
//						//Сравниваем значения ключа из словаря и то значение, которое только что пришло и выбираем лексиграфически меньшее из них
//						if (String.CompareOrdinal(sentence[n + 2],  nGrammDictionary[sentence[n] + " " + sentence[n+1]]) < 0)
//						{
//							nGrammDictionary[sentence[n] + " " + sentence[n + 1]] = sentence[n + 2];
//						}
//					}
					
//					//Считаем сколько раз повторились ключ + значение в тексте
					if (dictionaryCounter.ContainsKey(sentence[n] + " " + sentence[n + 1]) && dictionaryCounter[sentence[n] + " " + sentence[n + 1]].ContainsKey(sentence[n+2]))
					{
						dictionaryCounter[sentence[n] + " " + sentence[n + 1]][sentence[n+2]]++;
					}
					else
					{
						if (dictionaryCounter.ContainsKey(sentence[n] + " " + sentence[n + 1]))
						{
							dictionaryCounter[sentence[n] + " " + sentence[n + 1]].Add(sentence[n+2], 1);
						}
						else
						{
							dictionaryCounter.Add(sentence[n] + " " + sentence[n + 1], new Dictionary<string, int>());
							dictionaryCounter[sentence[n] + " " + sentence[n + 1]].Add(sentence[n+2], 1);
						}
					}
				}
				
				n++;
			}

			//Выводим на экран всё говно, что нагенерили выше
			foreach (var e in nGrammDictionary) Console.WriteLine(e.Key + "\t" + e.Value);
			Console.WriteLine("Дальше следующий цикл выводов");


             foreach (var key1 in dictionaryCounter)
             {
	             foreach (var tuple2 in key1.Value)
	             {
		             Console.WriteLine($"Key1 {key1.Key}; Key2 {tuple2.Key} Value2 {tuple2.Value}");
	             }
             }

			//Пытаемся оставить только наиболее частотные варианты в словаре
			//Для каждого Key1 посмотреть Value2, если Value2 больше, чем предыдущее, то записать Key2 
			//
			//Если в словаре есть уже значение Key1, то тогда посмотреть VAlue2, если оно больше, чем то, которое было, то записать, если нет, то 
			//в словаре косяк, его видно из лога (отсеивает дубликаты записей, но неправильно учитывает цифру по которой надо сеять)
			
			var frequencyDictionary = new Dictionary<string, string>();
             foreach (var key1 in dictionaryCounter)
             {
	             //valueOfDictionaryCounter - Значение, в виде [здарова, 2]. Там и текст и кол-во повторов
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
             Console.WriteLine("дальше будет вывод следующего словаря");
             foreach (var e in frequencyDictionary)
             { 
	             Console.WriteLine($"Key {e.Key}; value {e.Value}");
             }
		}
	}
}
