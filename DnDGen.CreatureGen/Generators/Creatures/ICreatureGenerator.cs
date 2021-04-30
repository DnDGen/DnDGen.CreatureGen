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
        Creature GenerateRandomOfTemplate(string template);
        Task<Creature> GenerateRandomOfTemplateAsync(string template);
        Creature GenerateRandomOfTemplateAsCharacter(string template);
        Task<Creature> GenerateRandomOfTemplateAsCharacterAsync(string template);
    }
}