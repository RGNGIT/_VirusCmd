using System;
namespace _VirusCmd.Classes
{
	public class Megapolis: Locality
	{

		public int CitiesMerged { get; set; } 

		public Megapolis(int Population, int Infected, int Vaccinated, float Density, int CitiesMerged) : base(Population, Infected, Vaccinated, Density)
		{
			this.CitiesMerged = CitiesMerged;
		}
	}
}

