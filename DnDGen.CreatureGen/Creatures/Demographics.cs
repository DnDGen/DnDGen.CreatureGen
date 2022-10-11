namespace DnDGen.CreatureGen.Creatures
{
    public class Demographics
    {
        public string Gender { get; set; }
        public Measurement Age { get; set; }
        public Measurement MaximumAge { get; set; }
        public Measurement HeightOrLength { get; set; }
        public Measurement Weight { get; set; }

        public Demographics()
        {
            Gender = string.Empty;
            Age = new Measurement("years");
            MaximumAge = new Measurement("years");
            HeightOrLength = new Measurement("feet");
            Weight = new Measurement("pounds");
        }
    }
}
