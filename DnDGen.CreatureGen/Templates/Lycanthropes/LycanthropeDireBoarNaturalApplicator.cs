using DnDGen.CreatureGen.Creatures;

namespace DnDGen.CreatureGen.Templates.Lycanthropes
{
    internal class LycanthropeDireBoarNaturalApplicator : LycanthropeApplicator
    {
        protected override string LycanthropeSpecies => CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural;
        protected override string AnimalSpecies => CreatureConstants.Boar_Dire;
    }
}
