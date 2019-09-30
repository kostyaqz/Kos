namespace Names
{
	internal static class HeatmapTask
	{
		public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
		{
			var days = new string[30];
			for (var x = 0; x < days.Length; x++)
				days[x] = (x + 2).ToString();

			var mounth = new string[12];
			for (var y = 0; y < mounth.Length; y++)
				mounth[y] = (y + 1).ToString();

			var histogramData = new double[30, 12];

			for (var i = 0; i < names.Length; i++)
			{
				if (names[i].BirthDate.Day != 1)
					histogramData[names[i].BirthDate.Day - 2, names[i].BirthDate.Month - 1]++;
			}

			return new HeatmapData("Карта интенсивностей", histogramData, days, mounth);
		}

	}
}