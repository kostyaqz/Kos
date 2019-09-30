using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Branching
{
	class Class1
	{
		public static void WrongMain()
		{
			// в бинарном виде 0.1 представляется бесконечной дробью 0.00011001100...,
			// в типах данных float и double хранится только начало этой дроби,
			// поэтому число 0.1 представляется с погрешностью. 
			double x = 1.0 / 10;
			double sum = 0;
			for (int i = 0; i < 10; i++)
				sum += x;
			Console.WriteLine(sum == 1);
		}


		//Кажется, что он должен вывести True, однако на самом деле выводит False.
		//При выполнении этих операций, погрешность накапливается, и это приводит к неверным результатам.
		//Для того, чтобы избежать этого, при сравнивать числа с плавающей точкой нужно с учётом погрешности, вот так:
		static void RightMain()
		{
			double x = 1.0 / 10;
			double sum = 0;
			for (int i = 0; i < 10; i++)
				sum += x;
			Console.WriteLine(Math.Abs(sum - 1) < 1e-9);
		}

		//Здесь мы проверяем, что числа равны не в точности, но что модуль их разности меньше некоторого маленького числа.
		//Всегда используйте такую конструкцию при сравнении чисел с плавающей точкой.
	}
}
