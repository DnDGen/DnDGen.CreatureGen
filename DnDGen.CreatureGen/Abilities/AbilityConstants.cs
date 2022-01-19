namespace DnDGen.CreatureGen.Abilities
{
    public static class AbilityConstants
    {
        public const string Strength = "Strength";
        public const string Constitution = "Constitution";
        public const string Dexterity = "Dexterity";
        public const string Intelligence = "Intelligence";
        public const string Wisdom = "Wisdom";
        public const string Charisma = "Charisma";

        public static class RandomizerRolls
        {
            public const string Poor = "1d6+3";
            public const string Default = "1d2+9";
            public const string Good = "1d4+11";
            public const string Best = "1d3+15";
            public const string BestOfFour = "4d6k3";
            public const string OnesAsSixes = "3d6t1";
            public const string Raw = "3d6";
            public const string Wild = "2d10";
        }
    }
}