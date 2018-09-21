namespace CreatureGen.Selectors.Helpers
{
    public static class TypeAndAmountHelper
    {
        private const char SEPERATOR = '@';

        public static string Build(string type, string amount)
        {
            return type + SEPERATOR + amount;
        }

        public static string[] Parse(string input)
        {
            return input.Split(SEPERATOR);
        }
    }
}
