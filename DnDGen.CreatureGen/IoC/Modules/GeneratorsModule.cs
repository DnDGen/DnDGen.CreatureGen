using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Abilities;
using DnDGen.CreatureGen.Generators.Alignments;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Generators.Defenses;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Generators.Items;
using DnDGen.CreatureGen.Generators.Languages;
using DnDGen.CreatureGen.Generators.Magics;
using DnDGen.CreatureGen.Generators.Skills;
using DnDGen.CreatureGen.IoC.Factories;
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
            Bind<ICreaturePrototypeFactory>().To<CreaturePrototypeFactory>();

            Bind<IHitPointsGenerator>().To<HitPointsGenerator>();
            Bind<IArmorClassGenerator>().To<ArmorClassGenerator>();
            Bind<ISavesGenerator>().To<SavesGenerator>();
            Bind<IAttacksGenerator>().To<AttacksGenerator>();
            Bind<ISpeedsGenerator>().To<SpeedsGenerator>();
            Bind<IEquipmentGenerator>().To<EquipmentGenerator>();
            Bind<IMagicGenerator>().To<MagicGenerator>();
            Bind<ISpellsGenerator>().To<SpellsGenerator>();
            Bind<ILanguageGenerator>().To<LanguageGenerator>();
            Bind<IDemographicsGenerator>().To<DemographicsGenerator>();

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
            Bind<TemplateApplicator>()
                .ToMethod(c => HalfDragonApplicatorFactory.Build(c, CreatureConstants.Templates.HalfDragon_Black))
                .Named(CreatureConstants.Templates.HalfDragon_Black);
            Bind<TemplateApplicator>()
                .ToMethod(c => HalfDragonApplicatorFactory.Build(c, CreatureConstants.Templates.HalfDragon_Blue))
                .Named(CreatureConstants.Templates.HalfDragon_Blue);
            Bind<TemplateApplicator>()
                .ToMethod(c => HalfDragonApplicatorFactory.Build(c, CreatureConstants.Templates.HalfDragon_Brass))
                .Named(CreatureConstants.Templates.HalfDragon_Brass);
            Bind<TemplateApplicator>()
                .ToMethod(c => HalfDragonApplicatorFactory.Build(c, CreatureConstants.Templates.HalfDragon_Bronze))
                .Named(CreatureConstants.Templates.HalfDragon_Bronze);
            Bind<TemplateApplicator>()
                .ToMethod(c => HalfDragonApplicatorFactory.Build(c, CreatureConstants.Templates.HalfDragon_Copper))
                .Named(CreatureConstants.Templates.HalfDragon_Copper);
            Bind<TemplateApplicator>()
                .ToMethod(c => HalfDragonApplicatorFactory.Build(c, CreatureConstants.Templates.HalfDragon_Gold))
                .Named(CreatureConstants.Templates.HalfDragon_Gold);
            Bind<TemplateApplicator>()
                .ToMethod(c => HalfDragonApplicatorFactory.Build(c, CreatureConstants.Templates.HalfDragon_Green))
                .Named(CreatureConstants.Templates.HalfDragon_Green);
            Bind<TemplateApplicator>()
                .ToMethod(c => HalfDragonApplicatorFactory.Build(c, CreatureConstants.Templates.HalfDragon_Red))
                .Named(CreatureConstants.Templates.HalfDragon_Red);
            Bind<TemplateApplicator>()
                .ToMethod(c => HalfDragonApplicatorFactory.Build(c, CreatureConstants.Templates.HalfDragon_Silver))
                .Named(CreatureConstants.Templates.HalfDragon_Silver);
            Bind<TemplateApplicator>()
                .ToMethod(c => HalfDragonApplicatorFactory.Build(c, CreatureConstants.Templates.HalfDragon_White))
                .Named(CreatureConstants.Templates.HalfDragon_White);
            Bind<TemplateApplicator>().To<HalfFiendApplicator>().Named(CreatureConstants.Templates.HalfFiend);
            Bind<TemplateApplicator>().To<LichApplicator>().Named(CreatureConstants.Templates.Lich);
            Bind<TemplateApplicator>().To<NoneApplicator>().Named(CreatureConstants.Templates.None);
            Bind<TemplateApplicator>().To<SkeletonApplicator>().Named(CreatureConstants.Templates.Skeleton);
            Bind<TemplateApplicator>().To<VampireApplicator>().Named(CreatureConstants.Templates.Vampire);
            Bind<TemplateApplicator>()
                .ToMethod(c => LycanthropeApplicatorFactory.Build(c, CreatureConstants.Templates.Lycanthrope_Bear_Black_Afflicted, CreatureConstants.Bear_Black, false))
                .Named(CreatureConstants.Templates.Lycanthrope_Bear_Black_Afflicted);
            Bind<TemplateApplicator>()
                .ToMethod(c => LycanthropeApplicatorFactory.Build(c, CreatureConstants.Templates.Lycanthrope_Bear_Black_Natural, CreatureConstants.Bear_Black, true))
                .Named(CreatureConstants.Templates.Lycanthrope_Bear_Black_Natural);
            Bind<TemplateApplicator>()
                .ToMethod(c => LycanthropeApplicatorFactory.Build(c, CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted, CreatureConstants.Bear_Brown, false))
                .Named(CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted);
            Bind<TemplateApplicator>()
                .ToMethod(c => LycanthropeApplicatorFactory.Build(c, CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural, CreatureConstants.Bear_Brown, true))
                .Named(CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural);
            Bind<TemplateApplicator>()
                .ToMethod(c => LycanthropeApplicatorFactory.Build(c, CreatureConstants.Templates.Lycanthrope_Bear_Dire_Afflicted, CreatureConstants.Bear_Dire, false))
                .Named(CreatureConstants.Templates.Lycanthrope_Bear_Dire_Afflicted);
            Bind<TemplateApplicator>()
                .ToMethod(c => LycanthropeApplicatorFactory.Build(c, CreatureConstants.Templates.Lycanthrope_Bear_Dire_Natural, CreatureConstants.Bear_Dire, true))
                .Named(CreatureConstants.Templates.Lycanthrope_Bear_Dire_Natural);
            Bind<TemplateApplicator>()
                .ToMethod(c => LycanthropeApplicatorFactory.Build(c, CreatureConstants.Templates.Lycanthrope_Bear_Polar_Afflicted, CreatureConstants.Bear_Polar, false))
                .Named(CreatureConstants.Templates.Lycanthrope_Bear_Polar_Afflicted);
            Bind<TemplateApplicator>()
                .ToMethod(c => LycanthropeApplicatorFactory.Build(c, CreatureConstants.Templates.Lycanthrope_Bear_Polar_Natural, CreatureConstants.Bear_Polar, true))
                .Named(CreatureConstants.Templates.Lycanthrope_Bear_Polar_Natural);
            Bind<TemplateApplicator>()
                .ToMethod(c => LycanthropeApplicatorFactory.Build(c, CreatureConstants.Templates.Lycanthrope_Boar_Afflicted, CreatureConstants.Boar, false))
                .Named(CreatureConstants.Templates.Lycanthrope_Boar_Afflicted);
            Bind<TemplateApplicator>()
                .ToMethod(c => LycanthropeApplicatorFactory.Build(c, CreatureConstants.Templates.Lycanthrope_Boar_Natural, CreatureConstants.Boar, true))
                .Named(CreatureConstants.Templates.Lycanthrope_Boar_Natural);
            Bind<TemplateApplicator>()
                .ToMethod(c => LycanthropeApplicatorFactory.Build(c, CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted, CreatureConstants.Boar_Dire, false))
                .Named(CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted);
            Bind<TemplateApplicator>()
                .ToMethod(c => LycanthropeApplicatorFactory.Build(c, CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural, CreatureConstants.Boar_Dire, true))
                .Named(CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural);
            Bind<TemplateApplicator>()
                .ToMethod(c => LycanthropeApplicatorFactory.Build(c, CreatureConstants.Templates.Lycanthrope_Rat_Afflicted, CreatureConstants.Rat, false))
                .Named(CreatureConstants.Templates.Lycanthrope_Rat_Afflicted);
            Bind<TemplateApplicator>()
                .ToMethod(c => LycanthropeApplicatorFactory.Build(c, CreatureConstants.Templates.Lycanthrope_Rat_Natural, CreatureConstants.Rat, true))
                .Named(CreatureConstants.Templates.Lycanthrope_Rat_Natural);
            Bind<TemplateApplicator>()
                .ToMethod(c => LycanthropeApplicatorFactory.Build(c, CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted, CreatureConstants.Rat_Dire, false))
                .Named(CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted);
            Bind<TemplateApplicator>()
                .ToMethod(c => LycanthropeApplicatorFactory.Build(c, CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural, CreatureConstants.Rat_Dire, true))
                .Named(CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural);
            Bind<TemplateApplicator>()
                .ToMethod(c => LycanthropeApplicatorFactory.Build(c, CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted, CreatureConstants.Tiger, false))
                .Named(CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted);
            Bind<TemplateApplicator>()
                .ToMethod(c => LycanthropeApplicatorFactory.Build(c, CreatureConstants.Templates.Lycanthrope_Tiger_Natural, CreatureConstants.Tiger, true))
                .Named(CreatureConstants.Templates.Lycanthrope_Tiger_Natural);
            Bind<TemplateApplicator>()
                .ToMethod(c => LycanthropeApplicatorFactory.Build(c, CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Afflicted, CreatureConstants.Tiger_Dire, false))
                .Named(CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Afflicted);
            Bind<TemplateApplicator>()
                .ToMethod(c => LycanthropeApplicatorFactory.Build(c, CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Natural, CreatureConstants.Tiger_Dire, true))
                .Named(CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Natural);
            Bind<TemplateApplicator>()
                .ToMethod(c => LycanthropeApplicatorFactory.Build(c, CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted, CreatureConstants.Wolf, false))
                .Named(CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted);
            Bind<TemplateApplicator>()
                .ToMethod(c => LycanthropeApplicatorFactory.Build(c, CreatureConstants.Templates.Lycanthrope_Wolf_Natural, CreatureConstants.Wolf, true))
                .Named(CreatureConstants.Templates.Lycanthrope_Wolf_Natural);
            Bind<TemplateApplicator>()
                .ToMethod(c => LycanthropeApplicatorFactory.Build(c, CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted, CreatureConstants.Wolf_Dire, false))
                .Named(CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted);
            Bind<TemplateApplicator>()
                .ToMethod(c => LycanthropeApplicatorFactory.Build(c, CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural, CreatureConstants.Wolf_Dire, true))
                .Named(CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural);
            Bind<TemplateApplicator>().To<ZombieApplicator>().Named(CreatureConstants.Templates.Zombie);
        }
    }
}