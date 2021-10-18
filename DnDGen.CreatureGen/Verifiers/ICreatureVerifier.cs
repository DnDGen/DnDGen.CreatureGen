namespace DnDGen.CreatureGen.Verifiers
{
    public interface ICreatureVerifier
    {
        bool VerifyCompatibility(bool asCharacter, string creature = null, string template = null, string type = null, string challengeRating = null, string alignment = null);
    }
}