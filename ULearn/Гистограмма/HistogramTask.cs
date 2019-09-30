namespace Names
{
	internal static class HistogramTask
	{
		public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
		{
			var minDays = 1;
			var defaultDaysInMonth = 31;

			var days = new string[defaultDaysInMonth];
			for (var y = 0; y < days.Length; y++)
				days[y] = (y + minDays).ToString();

			var birthsCounts = new double[defaultDaysInMonth];
			foreach (var firstName in names)
				if ((firstName.Name == name) && (firstName.BirthDate.Day != 1))
					birthsCounts[firstName.BirthDate.Day - minDays]++;

			return new HistogramData("Рождаемость по дням", days, birthsCounts);
		}
	}
}