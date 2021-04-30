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

        string GenerateRandomNameOfTemplate(string template);
        Creature GenerateRandomOfTemplate(string template);
        Task<Creature> GenerateRandomOfTemplateAsync(string template);

        string GenerateRandomNameOfTemplateAsCharacter(string template);
        Creature GenerateRandomOfTemplateAsCharacter(string template);
        Task<Creature> GenerateRandomOfTemplateAsCharacterAsync(string template);

        (string CreatureName, string Template) GenerateRandomNameOfType(string creatureType);
        Creature GenerateRandomOfType(string creatureType);
        Task<Creature> GenerateRandomOfTypeAsync(string creatureType);

        (string CreatureName, string Template) GenerateRandomNameOfTypeAsCharacter(string creatureType);
        Creature GenerateRandomOfTypeAsCharacter(string creatureType);
        Task<Creature> GenerateRandomOfTypeAsCharacterAsync(string creatureType);
    }
}