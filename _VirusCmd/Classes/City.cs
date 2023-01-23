using System;
namespace _VirusCmd.Classes
{
	public class City: Locality
	{

		public int TCAmount { get; set; }
		public float TCAttendPercentage { get; set; }

		public City(int Population, int Infected, int Vaccinated, float Density, int TCAmount, float TCAttendPercentage) : base(Population, Infected, Vaccinated, Density)
		{
			this.TCAmount = TCAmount;
			this.TCAttendPercentage = TCAttendPercentage;
		}
	}
}

