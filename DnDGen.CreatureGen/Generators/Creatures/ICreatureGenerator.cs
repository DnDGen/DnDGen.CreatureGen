using DnDGen.CreatureGen.Creatures;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Generators.Creatures
{
    public interface ICreatureGenerator
    {
        Creature Generate(string creatureName, string template, bool asCharacter);
        Task<Creature> GenerateAsync(string creatureName, string template, bool asCharacter);

        (string Creature, string Template) GenerateRandomName(bool asCharacter, string template = null, string type = null, string challengeRating = null);
        Creature GenerateRandom(bool asCharacter, string template = null, string type = null, string challengeRating = null);
        Task<Creature> GenerateRandomAsync(bool asCharacter, string template = null, string type = null, string challengeRating = null);
    }
}