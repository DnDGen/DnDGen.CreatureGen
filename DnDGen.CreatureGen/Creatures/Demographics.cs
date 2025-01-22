namespace DnDGen.CreatureGen.Creatures
{
    public class Demographics
    {
        public string Gender { get; set; }
        public Measurement Age { get; set; }
        public Measurement MaximumAge { get; set; }
        public Measurement Height { get; set; }
        public Measurement Length { get; set; }
        public Measurement Weight { get; set; }
        public Measurement Wingspan { get; set; }
        public string Skin { get; set; }
        public string Hair { get; set; }
        public string Eyes { get; set; }
        public string Other { get; set; }

        public Demographics()
        {
            Gender = string.Empty;
            Age = new Measurement("years");
            MaximumAge = new Measurement("years");
            Height = new Measurement("inches");
            Length = new Measurement("inches");
            Wingspan = new Measurement("inches");
            Weight = new Measurement("pounds");
            Skin = string.Empty;
            Hair = string.Empty;
            Eyes = string.Empty;
            Other = string.Empty;
        }
    }
}
