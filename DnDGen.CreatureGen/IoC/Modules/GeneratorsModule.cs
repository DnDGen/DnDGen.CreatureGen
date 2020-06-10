using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Abilities;
using DnDGen.CreatureGen.Generators.Alignments;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Generators.Defenses;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Generators.Items;
using DnDGen.CreatureGen.Generators.Magics;
using DnDGen.CreatureGen.Generators.Skills;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Verifiers;
using Ninject.Modules;

namespace DnDGen.CreatureGen.IoC.Modules
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
            Bind<IEquipmentGenerator>().To<EquipmentGenerator>();
            Bind<IMagicGenerator>().To<MagicGenerator>();
            Bind<ISpellsGenerator>().To<SpellsGenerator>();

            Bind<ICreatureGenerator>().To<CreatureGenerator>();
            Bind<IAlignmentGenerator>().To<AlignmentGenerator>();
            Bind<IAbilitiesGenerator>().To<AbilitiesGenerator>();
            Bind<ISkillsGenerator>().To<SkillsGenerator>();
            Bind<IFeatsGenerator>().To<FeatsGenerator>();
            Bind<IFeatFocusGenerator>().To<FeatFocusGenerator>();

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