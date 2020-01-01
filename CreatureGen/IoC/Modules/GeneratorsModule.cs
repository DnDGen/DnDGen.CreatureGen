using CreatureGen.Creatures;
using CreatureGen.Generators.Abilities;
using CreatureGen.Generators.Alignments;
using CreatureGen.Generators.Attacks;
using CreatureGen.Generators.Creatures;
using CreatureGen.Generators.Defenses;
using CreatureGen.Generators.Feats;
using CreatureGen.Generators.Skills;
using CreatureGen.Templates;
using CreatureGen.Verifiers;
using Ninject.Modules;

namespace CreatureGen.IoC.Modules
{
    internal class GeneratorsModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICreatureVerifier>().To<CreatureVerifier>();

            Bind<IHitPointsGenerator>().To<HitPointsGenerator>();
            Bind<IArmorClassGenerator>().To<ArmorClassGenerator>();
            Bind<ISavesGenerator>().To<SavesGenerator>();
            Bind<IAttacksGenerator>().To<AttacksGenerator>();
            Bind<ISpeedsGenerator>().To<SpeedsGenerator>();

            BindDecoratedGenerators();
        }

        private void BindDecoratedGenerators()
        {
            Bind<ICreatureGenerator>().To<CreatureGenerator>().WhenInjectedInto<CreatureGeneratorEventDecorator>();
            Bind<ICreatureGenerator>().To<CreatureGeneratorEventDecorator>();

            Bind<IAlignmentGenerator>().To<AlignmentGenerator>().WhenInjectedInto<AlignmentGeneratorEventDecorator>();
            Bind<IAlignmentGenerator>().To<AlignmentGeneratorEventDecorator>();

            Bind<IAbilitiesGenerator>().To<AbilitiesGenerator>().WhenInjectedInto<AbilitiesGeneratorEventDecorator>();
            Bind<IAbilitiesGenerator>().To<AbilitiesGeneratorEventDecorator>();

            Bind<ISkillsGenerator>().To<SkillsGenerator>().WhenInjectedInto<SkillsGeneratorEventGenDecorator>();
            Bind<ISkillsGenerator>().To<SkillsGeneratorEventGenDecorator>();

            Bind<IFeatsGenerator>().To<FeatsGenerator>().WhenInjectedInto<FeatsGeneratorEventDecorator>();
            Bind<IFeatsGenerator>().To<FeatsGeneratorEventDecorator>();

            Bind<IFeatFocusGenerator>().To<FeatFocusGenerator>().WhenInjectedInto<FeatFocusGeneratorEventDecorator>();
            Bind<IFeatFocusGenerator>().To<FeatFocusGeneratorEventDecorator>();

            Bind<TemplateApplicator>().To<CelestialCreatureApplicator>().Named(CreatureConstants.Templates.CelestialCreature);
            Bind<TemplateApplicator>().To<FiendishCreatureApplicator>().Named(CreatureConstants.Templates.FiendishCreature);
            Bind<TemplateApplicator>().To<GhostApplicator>().Named(CreatureConstants.Templates.Ghost);
            Bind<TemplateApplicator>().To<HalfCelestialApplicator>().Named(CreatureConstants.Templates.HalfCelestial);
            Bind<TemplateApplicator>().To<HalfDragonApplicator>().Named(CreatureConstants.Templates.HalfDragon);
            Bind<TemplateApplicator>().To<HalfFiendApplicator>().Named(CreatureConstants.Templates.HalfFiend);
            Bind<TemplateApplicator>().To<LichApplicator>().Named(CreatureConstants.Templates.Lich);
            Bind<TemplateApplicator>().To<NoneApplicator>().Named(CreatureConstants.Templates.None);
            Bind<TemplateApplicator>().To<SkeletonApplicator>().Named(CreatureConstants.Templates.Skeleton);
            Bind<TemplateApplicator>().To<VampireApplicator>().Named(CreatureConstants.Templates.Vampire);
            Bind<TemplateApplicator>().To<WerebearApplicator>().Named(CreatureConstants.Templates.Werebear);
            Bind<TemplateApplicator>().To<WereboarApplicator>().Named(CreatureConstants.Templates.Wereboar);
            Bind<TemplateApplicator>().To<WereratApplicator>().Named(CreatureConstants.Templates.Wererat);
            Bind<TemplateApplicator>().To<WeretigerApplicator>().Named(CreatureConstants.Templates.Weretiger);
            Bind<TemplateApplicator>().To<WerewolfApplicator>().Named(CreatureConstants.Templates.Werewolf);
            Bind<TemplateApplicator>().To<ZombieApplicator>().Named(CreatureConstants.Templates.Zombie);
        }
    }
}