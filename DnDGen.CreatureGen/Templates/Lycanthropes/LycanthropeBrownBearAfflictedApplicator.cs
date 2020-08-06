using DnDGen.CreatureGen.Creatures;

namespace DnDGen.CreatureGen.Templates.Lycanthropes
{
    internal class LycanthropeBrownBearAfflictedApplicator : LycanthropeApplicator
    {
        protected override string LycanthropeSpecies => CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted;
        protected override string AnimalSpecies => CreatureConstants.Bear_Brown;
    }
}
