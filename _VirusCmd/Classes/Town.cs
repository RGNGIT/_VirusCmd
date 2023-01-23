using System;
namespace _VirusCmd.Classes
{
	public class Town: Locality
	{

		public string Elder { get; set; }
		public int HouseholdAmount { get; set; }

		public Town(int Population, int Infected, int Vaccinated, float Density, string Elder, int HouseholdAmount) : base(Population, Infected, Vaccinated, Density)
		{
			this.Elder = Elder;
			this.HouseholdAmount = HouseholdAmount;
		}
	}
}

