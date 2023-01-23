using System;
namespace _VirusCmd.Classes
{
	public class Country
	{

		public string Name { get; set; }
		public float Budget { get; set; }
		public List<Locality> Localities { get; set; } = new();

		public Country(string Name, float Budget)
		{
			this.Name = Name;
			this.Budget = Budget;
		}

	}
}

