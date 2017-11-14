using CreatureGen.Creatures;

namespace CreatureGen.Generators.Creatures
{
    public interface ICreatureGenerator
    {
        Creature Generate(string creatureName, string template);
    }
}