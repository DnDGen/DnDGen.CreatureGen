using System.Collections.Generic;

namespace CreatureGen.Creatures
{
    public interface ICreatureGenerator
    {
        IEnumerable<Creature> Generate(string creatureName, string template, int quantity);
        Creature Generate(string creatureName, string template);
    }
}