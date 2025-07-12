using DnDGen.CreatureGen.Creatures;

namespace DnDGen.CreatureGen.Generators.Creatures
{
    public interface IDemographicsGenerator
    {
        Demographics Generate(string creatureName);
        Demographics UpdateByTemplate(Demographics source, string creature, string template, bool addWingspan = false, bool overwriteAppearance = false);
        Demographics AdjustDemographicsBySize(Demographics demographics, string originalSize, string advancedSize);
    }
}