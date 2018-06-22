namespace CreatureGen.Creatures
{
    public class SizeConstants
    {
        public const string Colossal = "Colossal";
        public const string Gargantuan = "Gargantuan";
        public const string Huge = "Huge";
        public const string Large = "Large";
        public const string Medium = "Medium";
        public const string Small = "Small";
        public const string Tiny = "Tiny";
        public const string Diminutive = "Diminutive";
        public const string Fine = "Fine";

        public static string[] GetOrdered()
        {
            return new[]
            {
                Fine,
                Diminutive,
                Tiny,
                Small,
                Medium,
                Large,
                Huge,
                Gargantuan,
                Colossal
            };
        }
    }
}