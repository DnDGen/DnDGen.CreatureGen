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

        string GenerateRandomNameOfTemplate(string template, string challengeRating = null);
        Creature GenerateRandomOfTemplate(string template, string challengeRating = null);
        Task<Creature> GenerateRandomOfTemplateAsync(string template, string challengeRating = null);

        string GenerateRandomNameOfTemplateAsCharacter(string template, string challengeRating = null);
        Creature GenerateRandomOfTemplateAsCharacter(string template, string challengeRating = null);
        Task<Creature> GenerateRandomOfTemplateAsCharacterAsync(string template, string challengeRating = null);

        (string CreatureName, string Template) GenerateRandomNameOfType(string creatureType, string challengeRating = null);
        Creature GenerateRandomOfType(string creatureType, string challengeRating = null);
        Task<Creature> GenerateRandomOfTypeAsync(string creatureType, string challengeRating = null);

        (string CreatureName, string Template) GenerateRandomNameOfTypeAsCharacter(string creatureType, string challengeRating = null);
        Creature GenerateRandomOfTypeAsCharacter(string creatureType, string challengeRating = null);
        Task<Creature> GenerateRandomOfTypeAsCharacterAsync(string creatureType, string challengeRating = null);

        (string CreatureName, string Template) GenerateRandomNameOfChallengeRating(string challengeRating);
        Creature GenerateRandomOfChallengeRating(string challengeRating);
        Task<Creature> GenerateRandomOfChallengeRatingAsync(string challengeRating);

        (string CreatureName, string Template) GenerateRandomNameOfChallengeRatingAsCharacter(string challengeRating);
        Creature GenerateRandomOfChallengeRatingAsCharacter(string challengeRating);
        Task<Creature> GenerateRandomOfChallengeRatingAsCharacterAsync(string challengeRating);
    }
}