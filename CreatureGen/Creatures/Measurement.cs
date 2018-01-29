namespace CreatureGen.Creatures
{
    public class Measurement
    {
        public double Value { get; set; }
        public string Description { get; set; }

        public readonly string Unit;

        public Measurement(string unit)
        {
            Description = string.Empty;
            Unit = unit;
        }
    }
}
