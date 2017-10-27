namespace CreatureGen.Creatures
{
    public class CreatureType
    {
        public string Name { get; set; }
        public CreatureType SubType { get; set; }

        public CreatureType()
        {
            Name = string.Empty;
        }
    }
}
