using System;
using System.Globalization;

namespace ConsoleApp1
{
    class Program3
    {
        public static void Lection3()
        {
            //Строки - это последовательности символов
            string myString = "Hello, world!";

            // + — это операция "приписывания" одной строки к другой:
            string s = "Hello" + " " + "world";

            // Можно обращаться к отдельным символам
            char c = myString[1]; //'e' — нумерация символов с нуля.
            char myChar = 'e'; // одинарные кавычки используются для символов. Двойные — для строк.

            //У строк есть собственные методы и переменные (правильно называть это свойствами),
            //которые позволяют узнать информацию о строке 
            Console.WriteLine(myString.Length);

            myString = myString.Substring(0, 5);
            Console.WriteLine(myString);

            string strangeSymbols = "© 2014 Σγμβόλσ";

            //Тип string может иметь особое значение - null.
            //Это не пустая строка, а отсутствие всякой строки.
            myString = null;

            //Интересно, что тип int такого значения иметь не может.
            //int a=null;

            int number = int.Parse("42"); //Из строки в число
            string numString = 42.ToString(); // Из числа в строку
            double number2 = double.Parse("34.42"); // Зависит от настроек операционной системы

            //Следующий вызов не зависит от настроек и всегда ожидает точку в качестве разделителя:
            number2 = double.Parse("34.42", CultureInfo.InvariantCulture);

            //Следующий вызов не зависит от настроек и всегда использует точку в качестве разделителя:
            string invariantNumber2 = number2.ToString(CultureInfo.InvariantCulture);
            Console.WriteLine(invariantNumber2); //34.42
        }
    }
}
