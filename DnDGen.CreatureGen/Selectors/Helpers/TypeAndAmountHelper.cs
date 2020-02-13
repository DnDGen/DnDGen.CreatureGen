using DnDGen.CreatureGen.Selectors.Selections;

namespace DnDGen.CreatureGen.Selectors.Helpers
{
    public static class TypeAndAmountHelper
    {
        public static string Build(string type, string amount)
        {
            return type + TypeAndAmountSelection.Divider + amount;
        }

        public static string[] Parse(string input)
        {
            return input.Split(TypeAndAmountSelection.Divider);
        }
    }
}
