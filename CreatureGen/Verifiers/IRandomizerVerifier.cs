using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Creatures;
using CreatureGen.Randomizers.Alignments;
using CreatureGen.Randomizers.CharacterClasses;
using CreatureGen.Randomizers.Races;

namespace CreatureGen.Verifiers
{
    public interface IRandomizerVerifier
    {
        bool VerifyCompatibility(IAlignmentRandomizer alignmentRandomizer, IClassNameRandomizer classNameRandomizer, ILevelRandomizer levelRandomizer, RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer);
        bool VerifyAlignmentCompatibility(Alignment alignment, IClassNameRandomizer classNameRandomizer, ILevelRandomizer levelRandomizer, RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer);
        bool VerifyCharacterClassCompatibility(Alignment alignment, CharacterClassPrototype characterClass, RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer);
        bool VerifyRaceCompatibility(Alignment alignment, CharacterClassPrototype characterClass, RacePrototype race);
    }
}