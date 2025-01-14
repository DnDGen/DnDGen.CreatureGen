using DnDGen.CreatureGen.Creatures;

namespace DnDGen.CreatureGen.Generators.Creatures
{
    public interface IDemographicsGenerator
    {
        Demographics Generate(string creatureName);
        Measurement GenerateWingspan(string creatureName, string baseKey);
        Demographics Update(Demographics source, string template, string size, bool addWingspan = false, bool overwriteAppearance = false);
    }
}