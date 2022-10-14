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
using DnDGen.CreatureGen.Tests.Integration.TestData;
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

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Templates))]
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
        public void HalfDragonApplicatorIsInjected(string template)
        {
            var applicator = AssertNamedIsInstanceOf<TemplateApplicator, HalfDragonApplicator>(template);
            var halfDragonApplicator = applicator as HalfDragonApplicator;
            Assert.That(halfDragonApplicator.DragonSpecies, Is.EqualTo(template));
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

        [TestCase(CreatureConstants.Templates.Lycanthrope_Bear_Black_Afflicted, CreatureConstants.Bear_Black, false)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Bear_Black_Natural, CreatureConstants.Bear_Black, true)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted, CreatureConstants.Bear_Brown, false)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural, CreatureConstants.Bear_Brown, true)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Bear_Dire_Afflicted, CreatureConstants.Bear_Dire, false)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Bear_Dire_Natural, CreatureConstants.Bear_Dire, true)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Bear_Polar_Afflicted, CreatureConstants.Bear_Polar, false)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Bear_Polar_Natural, CreatureConstants.Bear_Polar, true)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Boar_Afflicted, CreatureConstants.Boar, false)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Boar_Natural, CreatureConstants.Boar, true)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted, CreatureConstants.Boar_Dire, false)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural, CreatureConstants.Boar_Dire, true)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Rat_Afflicted, CreatureConstants.Rat, false)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Rat_Natural, CreatureConstants.Rat, true)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted, CreatureConstants.Rat_Dire, false)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural, CreatureConstants.Rat_Dire, true)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted, CreatureConstants.Tiger, false)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Tiger_Natural, CreatureConstants.Tiger, true)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Afflicted, CreatureConstants.Tiger_Dire, false)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Natural, CreatureConstants.Tiger_Dire, true)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted, CreatureConstants.Wolf, false)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Wolf_Natural, CreatureConstants.Wolf, true)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted, CreatureConstants.Wolf_Dire, false)]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural, CreatureConstants.Wolf_Dire, true)]
        public void LycanthropeApplicatorIsInjected(string template, string animal, bool isNatural)
        {
            var applicator = AssertNamedIsInstanceOf<TemplateApplicator, LycanthropeApplicator>(template);
            var lycanthropeApplicator = applicator as LycanthropeApplicator;
            Assert.That(lycanthropeApplicator.LycanthropeSpecies, Is.EqualTo(template));
            Assert.That(lycanthropeApplicator.AnimalSpecies, Is.EqualTo(animal));
            Assert.That(lycanthropeApplicator.IsNatural, Is.EqualTo(isNatural));

            if (template.Contains("Natural"))
                Assert.That(lycanthropeApplicator.IsNatural, Is.True);
            else if (template.Contains("Afflicted"))
                Assert.That(lycanthropeApplicator.IsNatural, Is.False);
            else
                Assert.Fail($"Lycanthrope '{template}' is neither Natural nor Afflicted");
        }

        [Test]
        public void ZombieApplicatorIsInjected()
        {
            AssertNamedIsInstanceOf<TemplateApplicator, ZombieApplicator>(CreatureConstants.Templates.Zombie);
        }

        [Test]
        public void CreaturePrototypeFactoryIsInjected()
        {
            AssertNotSingleton<ICreaturePrototypeFactory>();
        }

        [Test]
        public void DemographicsGeneratorIsInjected()
        {
            AssertNotSingleton<IDemographicsGenerator>();
        }
    }
}