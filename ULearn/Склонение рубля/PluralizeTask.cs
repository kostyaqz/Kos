namespace Pluralize
{
	public static class PluralizeTask
	{
		public static string PluralizeRubles(int count)
		{
			var penultNumeralAndLast = count % 100;
			var lastNumeral = count % 10;

			if (penultNumeralAndLast >= 11 && penultNumeralAndLast <= 20)
			{
				return "рублей";
			}
			else if (lastNumeral == 1)
			{
				return "рубль";
			}
			else if (lastNumeral >= 2 && lastNumeral <= 4)
			{
				return "рубля";
			}
			else
			{
				return "рублей";
			}
		}
	}
}