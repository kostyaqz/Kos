using System;

namespace Rectangles
{
	public static class RectanglesTask
	{
		// Пересекаются ли два прямоугольника (пересечение только по границе также считается пересечением)
		// так можно обратиться к координатам левого верхнего угла первого прямоугольника: r1.Left, r1.Top
		public static bool AreIntersected(Rectangle r1, Rectangle r2)
		{
			return DetermineIntersection(r1.Top, r2.Top, r1.Bottom, r1.Left, r2.Left, r1.Right) ||
			       DetermineIntersection(r1.Top, r2.Top, r1.Bottom, r2.Left, r1.Left, r2.Right) ||
			       DetermineIntersection(r2.Top, r1.Top, r2.Bottom, r2.Left, r1.Left, r2.Right) ||
			       DetermineIntersection(r2.Top, r1.Top, r2.Bottom, r1.Left, r2.Left, r1.Right);
		}

		public static bool DetermineIntersection
		(int heightMin, int heightMiddle, int heightMax, int widthMin, int widthMiddle, int widthMax)
		{
			return heightMin <= heightMiddle && heightMiddle <= heightMax && widthMin <= widthMiddle &&
			       widthMiddle <= widthMax;
		}

		// Площадь пересечения прямоугольников
		public static int IntersectionSquare(Rectangle r1, Rectangle r2)
		{
			if (r2.Left > r1.Left && r2.Left > r1.Right || r1.Left > r2.Left && r1.Left > r2.Right ||
			    r1.Top > r2.Top && r1.Top > r2.Bottom || r2.Top > r1.Top && r2.Top > r1.Bottom)
				return 0;
			return CalculatingIntersectionSize(r1.Bottom, r1.Top, r2.Bottom, r2.Top) *
			       CalculatingIntersectionSize(r1.Right, r1.Left, r2.Right, r2.Left);
		}

		public static int CalculatingIntersectionSize(int r1Max, int r1Min, int r2Max, int r2Min)
		{
			var externalBottom = Math.Min(r1Max, r2Max);
			var outerTop = Math.Max(r1Min, r2Min);
			var innerTop = Math.Min(outerTop, externalBottom);
			var innerBottom = Math.Max(outerTop, externalBottom);
			return innerBottom - innerTop;
		}

		public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
		{
			if (r1.Top >= r2.Top && r1.Bottom <= r2.Bottom && r1.Left >= r2.Left && r1.Right <= r2.Right)
				return 0;
			if (r2.Top >= r1.Top && r2.Bottom <= r1.Bottom && r2.Left >= r1.Left && r2.Right <= r1.Right)
				return 1;
			return -1;
		}
	}
}