using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Branching
{
	class Program31
	{
		public static void Main()
		{
			//Сравнение, как сложение или деление, тоже имеет возвращаемое значение
			Console.WriteLine(5 < 4);

			// И его можно сохранить в переменную логического типа
			// Это тип, абсолютно равноправный с int, double и другими 
			bool comparisonResult = 6 > 7;
			Console.WriteLine(comparisonResult);

			//Константы истины и лжи
			bool trueValue = true;
			bool falseValue = false;

			//Все операции сравнения
			Console.WriteLine(5 == 6);
			Console.WriteLine(5 != 6);
			Console.WriteLine(5 < 5);
			Console.WriteLine(5 <= 5);
			Console.WriteLine(5 > 5);
			Console.WriteLine(5 >= 5);

			// Операция "И", или конъюнкция
			Console.WriteLine((5 < 4) && (3 < 4));

			// Операция "ИЛИ", или дизъюнкция
			Console.WriteLine((5 < 4) || (3 < 4));

			// Операция "НЕ", или отрицание
			Console.WriteLine(!(5 < 4));
		}
	}
}
