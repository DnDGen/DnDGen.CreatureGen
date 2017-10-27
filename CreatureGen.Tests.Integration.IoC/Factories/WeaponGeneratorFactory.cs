using CharacterGen.Generators;
using CharacterGen.Generators.Domain.Items;
using CharacterGen.Generators.Items;
using CharacterGen.Selectors;
using Ninject;
using TreasureGen.Common.Items;
using TreasureGen.Generators.Items.Magical;
using TreasureGen.Generators.Items.Mundane;

namespace CharacterGen.Bootstrap.Factories
{
    public static class WeaponGeneratorFactory
    {
        public static IWeaponGenerator CreateWith(IKernel kernel)
        {
            var collectionsSelector = kernel.Get<ICollectionsSelector>();
            var percentileSelector = kernel.Get<IPercentileSelector>();
            var mundaneWeaponGenerator = kernel.Get<MundaneItemGenerator>(ItemTypeConstants.Weapon);
            var magicalWeaponGenerator = kernel.Get<MagicalItemGenerator>(ItemTypeConstants.Weapon);
            var generator = kernel.Get<Generator>();

            return new WeaponGenerator(collectionsSelector, percentileSelector, mundaneWeaponGenerator,
                magicalWeaponGenerator, generator);
        }
    }
}
