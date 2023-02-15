using System;
namespace _VirusCmd.Classes
{
	[Serializable]
	public class SeasonSingleton
	{
		public int? Year { get; set; } = null;
		public string? Season { get; set; } = null;

		public Dictionary<string, string> VerbalPredict = new()
		{
			{ "Зима", "Холодно. Высокая вероятность заболеть" },
			{ "Весна", "Прохладно. Вероятность заболеть снижается" },
			{ "Лето", "Тепло. Низкая вероятность заболеть" },
			{ "Осень", "Прохладно. Вероятность заболеть повышается" }			
		};
	}
}

