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
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Templates.HalfDragons;
using DnDGen.CreatureGen.Templates.Lycanthropes;
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
            Bind<TemplateApplicator>().To<HalfDragonBlackApplicator>().Named(CreatureConstants.Templates.HalfDragon_Black);
            Bind<TemplateApplicator>().To<HalfDragonBlueApplicator>().Named(CreatureConstants.Templates.HalfDragon_Blue);
            Bind<TemplateApplicator>().To<HalfDragonBrassApplicator>().Named(CreatureConstants.Templates.HalfDragon_Brass);
            Bind<TemplateApplicator>().To<HalfDragonBronzeApplicator>().Named(CreatureConstants.Templates.HalfDragon_Bronze);
            Bind<TemplateApplicator>().To<HalfDragonCopperApplicator>().Named(CreatureConstants.Templates.HalfDragon_Copper);
            Bind<TemplateApplicator>().To<HalfDragonGoldApplicator>().Named(CreatureConstants.Templates.HalfDragon_Gold);
            Bind<TemplateApplicator>().To<HalfDragonGreenApplicator>().Named(CreatureConstants.Templates.HalfDragon_Green);
            Bind<TemplateApplicator>().To<HalfDragonRedApplicator>().Named(CreatureConstants.Templates.HalfDragon_Red);
            Bind<TemplateApplicator>().To<HalfDragonSilverApplicator>().Named(CreatureConstants.Templates.HalfDragon_Silver);
            Bind<TemplateApplicator>().To<HalfDragonWhiteApplicator>().Named(CreatureConstants.Templates.HalfDragon_White);
            Bind<TemplateApplicator>().To<HalfFiendApplicator>().Named(CreatureConstants.Templates.HalfFiend);
            Bind<TemplateApplicator>().To<LichApplicator>().Named(CreatureConstants.Templates.Lich);
            Bind<TemplateApplicator>().To<NoneApplicator>().Named(CreatureConstants.Templates.None);
            Bind<TemplateApplicator>().To<SkeletonApplicator>().Named(CreatureConstants.Templates.Skeleton);
            Bind<TemplateApplicator>().To<VampireApplicator>().Named(CreatureConstants.Templates.Vampire);
            Bind<TemplateApplicator>().To<LycanthropeBrownBearAfflictedApplicator>().Named(CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted);
            Bind<TemplateApplicator>().To<LycanthropeBoarAfflictedApplicator>().Named(CreatureConstants.Templates.Lycanthrope_Boar_Afflicted);
            Bind<TemplateApplicator>().To<LycanthropeDireBoarAfflictedApplicator>().Named(CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted);
            Bind<TemplateApplicator>().To<LycanthropeDireRatAfflictedApplicator>().Named(CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted);
            Bind<TemplateApplicator>().To<LycanthropeTigerAfflictedApplicator>().Named(CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted);
            Bind<TemplateApplicator>().To<LycanthropeWolfAfflictedApplicator>().Named(CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted);
            Bind<TemplateApplicator>().To<LycanthropeBrownBearNaturalApplicator>().Named(CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural);
            Bind<TemplateApplicator>().To<LycanthropeBoarNaturalApplicator>().Named(CreatureConstants.Templates.Lycanthrope_Boar_Natural);
            Bind<TemplateApplicator>().To<LycanthropeDireBoarNaturalApplicator>().Named(CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural);
            Bind<TemplateApplicator>().To<LycanthropeDireRatNaturalApplicator>().Named(CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural);
            Bind<TemplateApplicator>().To<LycanthropeTigerNaturalApplicator>().Named(CreatureConstants.Templates.Lycanthrope_Tiger_Natural);
            Bind<TemplateApplicator>().To<LycanthropeWolfNaturalApplicator>().Named(CreatureConstants.Templates.Lycanthrope_Wolf_Natural);
            Bind<TemplateApplicator>().To<LycanthropeDireWolfAfflictedApplicator>().Named(CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted);
            Bind<TemplateApplicator>().To<LycanthropeDireWolfNaturalApplicator>().Named(CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural);
            Bind<TemplateApplicator>().To<ZombieApplicator>().Named(CreatureConstants.Templates.Zombie);
        }
    }
}