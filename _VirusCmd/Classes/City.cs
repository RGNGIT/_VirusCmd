using System;
namespace _VirusCmd.Classes
{
	[Serializable]
	public class City: Locality
	{

		public int TCAmount { get; set; }
		public float TCAttendPercentage { get; set; }

		public City(string Name, int Population, int Infected, int Vaccinated, float Density, int TCAmount, float TCAttendPercentage) : base(Name, Population, Infected, Vaccinated, Density)
		{
			this.TCAmount = TCAmount;
			this.TCAttendPercentage = TCAttendPercentage;
		}
	}
}

