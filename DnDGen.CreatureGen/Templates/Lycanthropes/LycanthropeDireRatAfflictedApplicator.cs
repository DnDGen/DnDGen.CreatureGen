using DnDGen.CreatureGen.Creatures;

namespace DnDGen.CreatureGen.Templates.Lycanthropes
{
    internal class LycanthropeDireRatAfflictedApplicator : LycanthropeApplicator
    {
        protected override string LycanthropeSpecies => CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted;
        protected override string AnimalSpecies => CreatureConstants.Rat_Dire;
    }
}
