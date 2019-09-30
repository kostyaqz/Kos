using System;

namespace ConsoleApp1
{
	class Program
	{
		static void Main()
		{
			int[] numbers = new int[] { 5, 8, 9, 4, 5 };
			foreach (int i in numbers)
			{
				Console.WriteLine(i);
			}
		}
	}
}
