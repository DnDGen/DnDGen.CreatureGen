using DnDGen.CreatureGen.Creatures;

namespace DnDGen.CreatureGen.Templates.Lycanthropes
{
    internal class LycanthropeDireBoarAfflictedApplicator : LycanthropeApplicator
    {
        protected override string LycanthropeSpecies => CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted;
        protected override string AnimalSpecies => CreatureConstants.Boar_Dire;
    }
}
