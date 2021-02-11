using DnDGen.CreatureGen.Creatures;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Generators.Creatures
{
    public interface ICreatureGenerator
    {
        Creature Generate(string creatureName, string template);
        Task<Creature> GenerateAsync(string creatureName, string template);
        Creature GenerateAsCharacter(string creatureName, string template);
        Task<Creature> GenerateAsCharacterAsync(string creatureName, string template);
    }
}