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
using DnDGen.RollGen;
using DnDGen.TreasureGen.Items;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Integration.IoC.Modules
{
    [TestFixture]
    public class GeneratorsModuleTests : IoCTests
    {
        [Test]
        public void AlignmentGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<IAlignmentGenerator>();
        }

        [Test]
        public void CreatureGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<ICreatureGenerator>();
        }

        [Test]
        public void LanguageGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<ILanguageGenerator>();
        }

        [Test]
        public void HitPointsGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<IHitPointsGenerator>();
        }

        [Test]
        public void RandomizerVerifierIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<ICreatureVerifier>();
        }

        [Test]
        public void AbilitiesGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<IAbilitiesGenerator>();
        }

        [Test]
        public void SkillsGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<ISkillsGenerator>();
        }

        [Test]
        public void FeatsGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<IFeatsGenerator>();
        }

        [Test]
        public void SavesGeneratorIsNotASingleton()
        {
            AssertNotSingleton<ISavesGenerator>();
        }

        [Test]
        public void FeatFocusGeneratorIsNotASingleton()
        {
            AssertNotSingleton<IFeatFocusGenerator>();
        }

        [Test]
        public void EXTERNAL_DiceIsInjected()
        {
            AssertNotSingleton<Dice>();
        }

        [Test]
        public void AttacksGeneratorIsNotASingleton()
        {
            AssertNotSingleton<IAttacksGenerator>();
        }

        [Test]
        public void SpeedsGeneratorIsNotASingleton()
        {
            AssertNotSingleton<ISpeedsGenerator>();
        }

        [Test]
        public void EquipmentGeneratorIsNotASingleton()
        {
            AssertNotSingleton<IEquipmentGenerator>();
        }

        [Test]
        public void MagicGeneratorIsNotASingleton()
        {
            AssertNotSingleton<IMagicGenerator>();
        }

        [Test]
        public void SpellsGeneratorIsNotASingleton()
        {
            AssertNotSingleton<ISpellsGenerator>();
        }

        [Test]
        public void EXTERNAL_ItemsGeneratorIsInjected()
        {
            AssertNotSingleton<IItemsGenerator>();
        }

        [TestCase(CreatureConstants.Templates.CelestialCreature)]
        [TestCase(CreatureConstants.Templates.FiendishCreature)]
        [TestCase(CreatureConstants.Templates.Ghost)]
        [TestCase(CreatureConstants.Templates.HalfCelestial)]
        [TestCase(CreatureConstants.Templates.HalfDragon_Black)]
        [TestCase(CreatureConstants.Templates.HalfDragon_Blue)]
        [TestCase(CreatureConstants.Templates.HalfDragon_Brass)]
        [TestCase(CreatureConstants.Templates.HalfDragon_Bronze)]
        [TestCase(CreatureConstants.Templates.HalfDragon_Copper)]
        [TestCase(CreatureConstants.Templates.HalfDragon_Gold)]
        [TestCase(CreatureConstants.Templates.HalfDragon_Green)]
        [TestCase(CreatureConstants.Templates.HalfDragon_Red)]
        [TestCase(CreatureConstants.Templates.HalfDragon_Silver)]
        [TestCase(CreatureConstants.Templates.HalfDragon_White)]
        [TestCase(CreatureConstants.Templates.HalfFiend)]
        [TestCase(CreatureConstants.Templates.Lich)]
        [TestCase(CreatureConstants.Templates.None)]
        [TestCase(CreatureConstants.Templates.Skeleton)]
        [TestCase(CreatureConstants.Templates.Vampire)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Boar_Afflicted)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Boar_Natural)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Tiger_Natural)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Wolf_Natural)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural)]
        [TestCase(CreatureConstants.Templates.Zombie)]
        public void TemplateApplicatorIsInjected(string name)
        {
            AssertNotSingleton<TemplateApplicator>(name);
        }

        [Test]
        public void CelestialCreatureApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, CelestialCreatureApplicator>(CreatureConstants.Templates.CelestialCreature);
        }

        [Test]
        public void FiendishCreatureApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, FiendishCreatureApplicator>(CreatureConstants.Templates.FiendishCreature);
        }

        [Test]
        public void GhostApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, GhostApplicator>(CreatureConstants.Templates.Ghost);
        }

        [Test]
        public void HalfCelestialApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, HalfCelestialApplicator>(CreatureConstants.Templates.HalfCelestial);
        }

        [Test]
        public void HalfDragonApplicatorIsInjected_Black()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, HalfDragonBlackApplicator>(CreatureConstants.Templates.HalfDragon_Black);
        }

        [Test]
        public void HalfDragonApplicatorIsInjected_Blue()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, HalfDragonBlueApplicator>(CreatureConstants.Templates.HalfDragon_Blue);
        }

        [Test]
        public void HalfDragonApplicatorIsInjected_Brass()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, HalfDragonBrassApplicator>(CreatureConstants.Templates.HalfDragon_Brass);
        }

        [Test]
        public void HalfDragonApplicatorIsInjected_Bronze()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, HalfDragonBronzeApplicator>(CreatureConstants.Templates.HalfDragon_Bronze);
        }

        [Test]
        public void HalfDragonApplicatorIsInjected_Copper()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, HalfDragonCopperApplicator>(CreatureConstants.Templates.HalfDragon_Copper);
        }

        [Test]
        public void HalfDragonApplicatorIsInjected_Gold()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, HalfDragonGoldApplicator>(CreatureConstants.Templates.HalfDragon_Gold);
        }

        [Test]
        public void HalfDragonApplicatorIsInjected_Green()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, HalfDragonGreenApplicator>(CreatureConstants.Templates.HalfDragon_Green);
        }

        [Test]
        public void HalfDragonApplicatorIsInjected_Red()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, HalfDragonRedApplicator>(CreatureConstants.Templates.HalfDragon_Red);
        }

        [Test]
        public void HalfDragonApplicatorIsInjected_Silver()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, HalfDragonSilverApplicator>(CreatureConstants.Templates.HalfDragon_Silver);
        }

        [Test]
        public void HalfDragonApplicatorIsInjected_White()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, HalfDragonWhiteApplicator>(CreatureConstants.Templates.HalfDragon_White);
        }

        [Test]
        public void HalfFiendApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, HalfFiendApplicator>(CreatureConstants.Templates.HalfFiend);
        }

        [Test]
        public void LichApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, LichApplicator>(CreatureConstants.Templates.Lich);
        }

        [Test]
        public void NoneApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, NoneApplicator>(CreatureConstants.Templates.None);
        }

        [Test]
        public void SkeletonApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, SkeletonApplicator>(CreatureConstants.Templates.Skeleton);
        }

        [Test]
        public void VampireApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, VampireApplicator>(CreatureConstants.Templates.Vampire);
        }

        [Test]
        public void WerebearAfflictedApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, LycanthropeBrownBearAfflictedApplicator>(CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted);
        }

        [Test]
        public void WereboarAfflictedApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, LycanthropeBoarAfflictedApplicator>(CreatureConstants.Templates.Lycanthrope_Boar_Afflicted);
        }

        [Test]
        public void DireWereboarAfflictedApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, LycanthropeDireBoarAfflictedApplicator>(CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted);
        }

        [Test]
        public void WereratAfflictedApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, LycanthropeDireRatAfflictedApplicator>(CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted);
        }

        [Test]
        public void WeretigerAfflictedApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, LycanthropeTigerAfflictedApplicator>(CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted);
        }

        [Test]
        public void WerewolfAfflictedApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, LycanthropeWolfAfflictedApplicator>(CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted);
        }

        [Test]
        public void WerebearNaturalApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, LycanthropeBrownBearNaturalApplicator>(CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural);
        }

        [Test]
        public void WereboarNaturalApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, LycanthropeBoarNaturalApplicator>(CreatureConstants.Templates.Lycanthrope_Boar_Natural);
        }

        [Test]
        public void DireWereboarNaturalApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, LycanthropeDireBoarNaturalApplicator>(CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural);
        }

        [Test]
        public void WereratNaturalApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, LycanthropeDireRatNaturalApplicator>(CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural);
        }

        [Test]
        public void WeretigerNaturalApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, LycanthropeTigerNaturalApplicator>(CreatureConstants.Templates.Lycanthrope_Tiger_Natural);
        }

        [Test]
        public void WerewolfNaturalApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, LycanthropeWolfNaturalApplicator>(CreatureConstants.Templates.Lycanthrope_Wolf_Natural);
        }

        [Test]
        public void DireWerewolfAfflictedApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, LycanthropeDireWolfAfflictedApplicator>(CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted);
        }

        [Test]
        public void DireWerewolfNaturalApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, LycanthropeDireWolfNaturalApplicator>(CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural);
        }

        [Test]
        public void ZombieApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, ZombieApplicator>(CreatureConstants.Templates.Zombie);
        }
    }
}