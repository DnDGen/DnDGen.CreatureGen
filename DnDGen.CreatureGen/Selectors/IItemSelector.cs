using DnDGen.TreasureGen.Items;

namespace DnDGen.CreatureGen.Selectors
{
    internal interface IItemSelector
    {
        string SelectFrom(Item source);
        Item SelectFrom(string source);
    }
}
