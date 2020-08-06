using DnDGen.CreatureGen.Creatures;

namespace DnDGen.CreatureGen.Templates.Lycanthropes
{
    internal class LycanthropeWolfAfflictedApplicator : LycanthropeApplicator
    {
        protected override string LycanthropeSpecies => CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted;
        protected override string AnimalSpecies => CreatureConstants.Wolf;
    }
}
