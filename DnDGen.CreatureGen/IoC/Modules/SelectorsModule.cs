using DnDGen.CreatureGen.Selectors;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.Infrastructure.IoC.Modules;
using Ninject.Modules;

namespace DnDGen.CreatureGen.IoC.Modules
{
    internal class SelectorsModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IFeatsSelector>().To<FeatsSelector>();
            Bind<IAttackSelector>().To<AttackSelector>();
            Bind<IAdvancementSelector>().To<AdvancementSelector>();
            Bind<IItemSelector>().To<ItemSelector>();

            Kernel.BindDataSelection<AdvancementDataSelection>();
            Kernel.BindDataSelection<AttackDataSelection>();
            Kernel.BindDataSelection<DamageDataSelection>();
            Kernel.BindDataSelection<BonusDataSelection>();
            Kernel.BindDataSelection<CreatureDataSelection>();
            Kernel.BindDataSelection<FeatDataSelection>();
            Kernel.BindDataSelection<SpecialQualityDataSelection>();
            Kernel.BindDataSelection<SkillDataSelection>();
        }
    }
}