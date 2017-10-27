using CreatureGen.Domain.Selectors.Collections;
using Ninject.Modules;

namespace CreatureGen.Domain.IoC.Modules
{
    internal class SelectorsModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILanguageCollectionsSelector>().To<LanguageCollectionsSelector>();
            Bind<IAdjustmentsSelector>().To<AdjustmentsSelector>();
            Bind<IAbilityAdjustmentsSelector>().To<AbilityAdjustmentsSelector>();
            Bind<ISkillSelector>().To<SkillSelector>();
            Bind<ILeadershipSelector>().To<LeadershipSelector>();

            Bind<IFeatsSelector>().To<FeatsSelector>().WhenInjectedInto<FeatsSelectorEventGenDecorator>();
            Bind<IFeatsSelector>().To<FeatsSelectorEventGenDecorator>();
        }
    }
}