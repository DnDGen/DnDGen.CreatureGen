namespace CreatureGen.Creatures
{
    public interface ICreatureGenerator
    {
        Creature Generate(string creatureName, string template);
    }
}