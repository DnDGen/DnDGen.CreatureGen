namespace CreatureGen.Selectors.Helpers
{
    public static class TypeAndAmountHelper
    {
        private const char SEPERATOR = '/';

        public static string BuildData(string type, string amount)
        {
            return type + SEPERATOR + amount;
        }

        public static string[] ParseData(string input)
        {
            return input.Split(SEPERATOR);
        }
    }
}
