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

        public bool Is(string creatureType)
        {
            return TraverseTypes(this, creatureType);
        }

        private bool TraverseTypes(CreatureType creatureType, string target)
        {
            if (creatureType == null)
                return false;

            if (creatureType.Name == target)
                return true;

            return TraverseTypes(creatureType.SubType, target);
        }
    }
}
