using DnDGen.CreatureGen.Creatures;

namespace DnDGen.CreatureGen.Templates.Lycanthropes
{
    internal class LycanthropeDireRatNaturalApplicator : LycanthropeApplicator
    {
        protected override string LycanthropeSpecies => CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural;
        protected override string AnimalSpecies => CreatureConstants.Rat_Dire;
    }
}
