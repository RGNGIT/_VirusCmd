using System;
namespace _VirusCmd.Classes
{
	[Serializable]
	public class Megapolis: Locality
	{

		public int CitiesMerged { get; set; } 

		public Megapolis(string Name, int Population, int Infected, int Vaccinated, float Density, int CitiesMerged) : base(Name, Population, Infected, Vaccinated, Density)
		{
			this.CitiesMerged = CitiesMerged;
		}
	}
}

