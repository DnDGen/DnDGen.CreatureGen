using DnDGen.CreatureGen.Creatures;

namespace DnDGen.CreatureGen.Generators.Creatures
{
    public interface ICreatureGenerator
    {
        Creature Generate(string creatureName, string template);
    }
}