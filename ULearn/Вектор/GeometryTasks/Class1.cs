using System;

namespace GeometryTasks
{
	public class Vector
	{
		public double X;
		public double Y;

		public double GetLength()
		{
			return Geometry.GetLength(this);
		}

		public Vector Add(Vector vector)
		{
			return Geometry.Add(this, vector);
		}

		public bool Belongs(Segment segment)
		{
			return Geometry.IsVectorInSegment(this, segment);
		}
	}

	public class Segment
	{
		public Vector Begin;
		public Vector End;

		public double GetLenth()
		{
			return Geometry.GetLength(this);
		}

		public bool Contains(Vector vector)
		{
			return Geometry.IsVectorInSegment(vector, this);
		}
	}

	public class Geometry
	{
		public static double GetLength(Vector vector)
		{
			return Math.Abs(Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2)));
		}

		public static double GetLength(Segment segment)
		{
			return Math.Abs(Math.Sqrt(Math.Pow(segment.End.X - segment.Begin.X, 2) +
			                          Math.Pow(segment.End.Y - segment.Begin.Y, 2)));
		}

		public static Vector Add(Vector vector1, Vector vector2)
		{
			var resultVector = new Vector();
			resultVector.X = vector1.X + vector2.X;
			resultVector.Y = vector1.Y + vector2.Y;
			return resultVector;
		}

		public static bool IsVectorInSegment(Vector vector, Segment segment)
		{
			var lengthFromBeginToPoint = Math.Abs(Math.Sqrt(Math.Pow(vector.X - segment.Begin.X, 2)
			                                                + Math.Pow(vector.Y - segment.Begin.Y, 2)));
			var lengthFromEndToPoint = Math.Abs(Math.Sqrt(Math.Pow(segment.End.X - vector.X, 2)
			                                              + Math.Pow(segment.End.Y - vector.Y, 2)));
			var segmentLength = GetLength(segment);
			return lengthFromBeginToPoint + lengthFromEndToPoint == segmentLength;
		}
	}
}