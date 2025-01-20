using DnDGen.CreatureGen.Creatures;

namespace DnDGen.CreatureGen.Generators.Creatures
{
    public interface IDemographicsGenerator
    {
        Demographics Generate(string creatureName);
        Demographics Update(Demographics source, string creature, string template, bool addWingspan = false, string size = "", bool overwriteAppearance = false);
    }
}