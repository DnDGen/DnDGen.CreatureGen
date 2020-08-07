using DnDGen.CreatureGen.Creatures;

namespace DnDGen.CreatureGen.Templates.Lycanthropes
{
    internal class LycanthropeDireWolfNaturalApplicator : LycanthropeApplicator
    {
        protected override string LycanthropeSpecies => CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural;
        protected override string AnimalSpecies => CreatureConstants.Wolf_Dire;
    }
}
