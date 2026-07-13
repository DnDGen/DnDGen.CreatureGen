using DnDGen.CreatureGen.Generators.Creatures;
using System.Collections;

namespace DnDGen.CreatureGen.Verifiers
{
    public interface ICreatureVerifier
    {
        bool VerifyCompatibility(bool asCharacter, string creature = null, Filters filters = null);
    }
}