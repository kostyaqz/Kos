// В этом пространстве имен содержатся средства для работы с изображениями. Чтобы оно стало доступно, в проект был подключен Reference на сборку System.Drawing.dll

/*
Начните с точки (1, 0)
Создайте генератор рандомных чисел с сидом seed

На каждой итерации:

1. Выберите случайно одно из следующих преобразований и примените его к текущей точке:

	Преобразование 1. (поворот на 45° и сжатие в sqrt(2) раз):
	x' = (x · cos(45°) - y · sin(45°)) / sqrt(2)
	y' = (x · sin(45°) + y · cos(45°)) / sqrt(2)

	Преобразование 2. (поворот на 135°, сжатие в sqrt(2) раз, сдвиг по X на единицу):
	x' = (x · cos(135°) - y · sin(135°)) / sqrt(2) + 1
	y' = (x · sin(135°) + y · cos(135°)) / sqrt(2)

2. Нарисуйте текущую точку методом pixels.SetPixel(x, y)

*/

using System;

namespace Fractals
{
	internal static class DragonFractalTask
	{
		public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
		{
			var coordinates = new double[] {1, 0};

			var random = new Random(seed);

			for (var i = 0; i < iterationsCount; i++)
			{
				// Обратить внимание на то, что массив присваивается вызову метода, это важно, иначе работать не будет
				coordinates = CalculateNextCoordinates(random.Next(0,2), coordinates); 
				pixels.SetPixel(coordinates[0], coordinates[1]);
			}
		}

		public static double[] CalculateNextCoordinates(int randomNumber, double[] coordinates)
		{
			if (randomNumber == 0)
			{
				var x1 = (coordinates[0] * Math.Cos(Math.PI / 4) - coordinates[1] * Math.Sin(Math.PI / 4)) /
				         Math.Sqrt(2);
				var y1 = (coordinates[0] * Math.Sin(Math.PI / 4) + coordinates[1] * Math.Cos(Math.PI / 4)) /
				         Math.Sqrt(2);

				return new[] {x1, y1};
			}
			else
			{
				var x2 = (coordinates[0] * Math.Cos(Math.PI * 3 / 4) - coordinates[1] * Math.Sin(Math.PI * 3 / 4)) /
				         Math.Sqrt(2) + 1;
				var y2 = (coordinates[0] * Math.Sin(Math.PI * 3 / 4) + coordinates[1] * Math.Cos(Math.PI * 3 / 4)) /
				         Math.Sqrt(2);
				return new[] {x2, y2};
			}
		}
	}
}