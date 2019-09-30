﻿//Ключевое слово enum используется для объявления перечисления — отдельного типа, который состоит из набора именованных констант, называемого списком перечислителей.
//Обычно лучше всего определять перечисление непосредственно в пространстве имен, чтобы всем классам в пространстве имен было одинаково удобно обращаться к нему.
//Однако перечисление также может быть вложенным в классе или структуре.По умолчанию первый перечислитель имеет значение 0, и значение каждого последующего
//перечислителя увеличивается на 1.


//1
//enum DayOfTheWeek // Объявление перечисления
//2
//{
//3
//    Monday, // Это перечислитель. По умолчанию DayOfTheWeek.Monday = 0;
//4
//    Tuesday, // И это перечислитель. DayOfTheWeek.Tuesday = 1;
//5
//    Wednesday,
//6
//    Thursday,
//7
//    Friday,
//8
//    Saturday,
//9
//    Sunday
//10
//}
//Значение любого перечислителя можно изменить.В этом случае, значения всех последующих перечислителей будет опираться на новое значение предшествующего перечислителя


//1
//enum DayOfTheWeek_v2
//2
//{
//3
//    Monday, // По умолчанию DayOfTheWeek.Monday = 0;
//4
//    Tuesday, // DayOfTheWeek.Tuesday = 1;
//5
//    Wednesday = 10, // DayOfTheWeek.Wednesday теперь равен 10, а не 2,
//6
//    Thursday, // а значение DayOfTheWeek.Thursday будет равно 11;
//7
//    Friday,
//8
//    Saturday,
//9
//    Sunday
//10
//}
//Типы перечислений По умолчанию - int, но также можно использовать типы byte, sbyte, short, ushort, int, uint, long и ulong. Пример объявления перечисления типа short


//1
//enum DayOfTheWeek_v3 : short
//2
//{
//3
//    Monday,
//4
//    Tuesday,
//5
//    Wednesday,
//6
//    Thursday,
//7
//    Friday,
//8
//    Saturday,
//9
//    Sunday
//10
//}
//Для получения значения перечислителя, необходимо выполнить приведение типа перечислителя к типу перечисления


//1
//Console.WriteLine((int) DayOfTheWeek.Tuesday);
//Константы
//Константы объявляются с помощью ключевого слова const.


//1
//const double Pi = 3.14159;
//Объявлять константы можно внутри методов(там где объявляются переменные) и внутри классов(где объявляются поля).

//Константа-поле всегда является статическим, при этом слово static писать не нужно.