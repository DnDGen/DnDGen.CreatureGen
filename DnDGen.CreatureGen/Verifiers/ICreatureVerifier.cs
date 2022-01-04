using DnDGen.CreatureGen.Generators.Creatures;

namespace DnDGen.CreatureGen.Verifiers
{
    public interface ICreatureVerifier
    {
        bool VerifyCompatibility(bool asCharacter, string creature = null, RandomFilters filters = null);
    }
}