namespace _VirusCmd.Classes
{
    [Serializable]
    public class Bacteria: Disease
    {

        public Bacteria(string Name, string Description, string Zone): base(Name, Description) 
        {
            this.Zone = Zone;
        }

        public string? Zone { get; set; }
    }
}
