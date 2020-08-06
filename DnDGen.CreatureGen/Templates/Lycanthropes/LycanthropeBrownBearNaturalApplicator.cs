using DnDGen.CreatureGen.Creatures;

namespace DnDGen.CreatureGen.Templates.Lycanthropes
{
    internal class LycanthropeBrownBearNaturalApplicator : LycanthropeApplicator
    {
        protected override string LycanthropeSpecies => CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural;
        protected override string AnimalSpecies => CreatureConstants.Bear_Brown;
    }
}
