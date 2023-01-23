using System;
namespace _VirusCmd.Classes
{
	// Общий класс для населенного пункта
	public class Locality
	{
		public int Population { get; set; }
		public int Infected { get; set; }
		public int Vaccinated { get; set; }
		public float Density { get; set; }

		public Locality(int Population, int Infected, int Vaccinated, float Density)
		{
			if((Infected + Vaccinated) > Population)
            {
				throw new Exception(
					@"Ситуация, когда сумма вакцинированных 
                    и инфецированных больше популяции невозможна!"
                );
            } else
            {
				this.Population = Population;
				this.Infected = Infected;
				this.Vaccinated = Vaccinated;
				this.Density = Density;
			}
		}

		public float CountInfectedPercentage() => (Infected / Population) * 100;

	}
}

