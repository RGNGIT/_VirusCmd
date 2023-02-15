namespace _VirusCmd.Classes
{
    [Serializable]
    public class Virus : Disease
    {

        public Virus(string Name, string Description, string Shtamm, string IncubationPeriod) : base(Name, Description)
        {
            this.Shtamm = Shtamm;
            this.IncubationPeriod = IncubationPeriod;
        }

        public string? Shtamm { get; set; }
        public string? IncubationPeriod { get; set; }
    }
}