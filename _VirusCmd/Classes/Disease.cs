namespace _VirusCmd.Classes
{
    [Serializable]
    public class Disease
    {

        public Disease(string Name, string Description) 
        {
            this.Name = Name;
            this.Description = Description;
        }

        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}