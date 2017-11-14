using CreatureGen.Abilities;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using CreatureGen.Generators.Defenses;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace CreatureGen.Tests.Unit.Generators.Defenses
{
    [TestFixture]
    public class SavesGeneratorTests
    {
        private Mock<ICollectionSelector> mockCollectionsSelector;
        private ISavesGenerator savesGenerator;
        private List<Feat> feats;
        private Dictionary<string, Ability> abilities;
        private List<string> allSaveFeats;
        private List<string> reflexSaveFeats;
        private List<string> fortitudeSaveFeats;
        private List<string> willSaveFeats;
        private List<string> strongReflex;
        private List<string> strongFortitude;
        private List<string> strongWill;
        private HitPoints hitPoints;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            savesGenerator = new SavesGenerator(mockCollectionsSelector.Object);
            feats = new List<Feat>();
            abilities = new Dictionary<string, Ability>();
            allSaveFeats = new List<string>();
            reflexSaveFeats = new List<string>();
            fortitudeSaveFeats = new List<string>();
            willSaveFeats = new List<string>();
            strongFortitude = new List<string>();
            strongReflex = new List<string>();
            strongWill = new List<string>();
            hitPoints = new HitPoints();

            hitPoints.HitDiceQuantity = 1;
            abilities[AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution);
            abilities[AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity);
            abilities[AbilityConstants.Wisdom] = new Ability(AbilityConstants.Wisdom);
            allSaveFeats.Add("other feat");
            reflexSaveFeats.Add("other feat");
            fortitudeSaveFeats.Add("other feat");
            willSaveFeats.Add("other feat");

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.SavingThrows))
                .Returns(allSaveFeats);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, SaveConstants.Fortitude))
                .Returns(fortitudeSaveFeats);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, SaveConstants.Reflex))
                .Returns(reflexSaveFeats);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, SaveConstants.Will))
                .Returns(willSaveFeats);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.CreatureGroups, SaveConstants.Fortitude))
                .Returns(strongFortitude);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.CreatureGroups, SaveConstants.Reflex))
                .Returns(strongReflex);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.CreatureGroups, SaveConstants.Will))
                .Returns(strongWill);
        }

        [Test]
        public void ApplyAbilityBonuses()
        {
            abilities[AbilityConstants.Constitution].BaseValue = 9266;
            abilities[AbilityConstants.Dexterity].BaseValue = 90210;
            abilities[AbilityConstants.Wisdom].BaseValue = -42;

            var saves = savesGenerator.GenerateWith("creature", hitPoints, feats, abilities);
            Assert.That(saves.Constitution, Is.EqualTo(abilities[AbilityConstants.Constitution]));
            Assert.That(saves.Dexterity, Is.EqualTo(abilities[AbilityConstants.Dexterity]));
            Assert.That(saves.Wisdom, Is.EqualTo(abilities[AbilityConstants.Wisdom]));
        }

        [TestCase(0, 0)]
        [TestCase(1, 2)]
        [TestCase(2, 3)]
        [TestCase(3, 3)]
        [TestCase(4, 4)]
        [TestCase(5, 4)]
        [TestCase(6, 5)]
        [TestCase(7, 5)]
        [TestCase(8, 6)]
        [TestCase(9, 6)]
        [TestCase(10, 7)]
        [TestCase(11, 7)]
        [TestCase(12, 8)]
        [TestCase(13, 8)]
        [TestCase(14, 9)]
        [TestCase(15, 9)]
        [TestCase(16, 10)]
        [TestCase(17, 10)]
        [TestCase(18, 11)]
        [TestCase(19, 11)]
        [TestCase(20, 12)]
        public void StrongSaveBonuses(int quantity, int saveBonus)
        {
            hitPoints.HitDiceQuantity = quantity;
            strongFortitude.Add("creature");

            var saves = savesGenerator.GenerateWith("creature", hitPoints, feats, abilities);
            Assert.That(saves.RacialFortitudeBonus, Is.EqualTo(saveBonus));
        }

        [TestCase(0, 0)]
        [TestCase(1, 0)]
        [TestCase(2, 0)]
        [TestCase(3, 1)]
        [TestCase(4, 1)]
        [TestCase(5, 1)]
        [TestCase(6, 2)]
        [TestCase(7, 2)]
        [TestCase(8, 2)]
        [TestCase(9, 3)]
        [TestCase(10, 3)]
        [TestCase(11, 3)]
        [TestCase(12, 4)]
        [TestCase(13, 4)]
        [TestCase(14, 4)]
        [TestCase(15, 5)]
        [TestCase(16, 5)]
        [TestCase(17, 5)]
        [TestCase(18, 6)]
        [TestCase(19, 6)]
        [TestCase(20, 6)]
        public void WeakSaveBonuses(int quantity, int saveBonus)
        {
            hitPoints.HitDiceQuantity = quantity;
            var saves = savesGenerator.GenerateWith("creature", hitPoints, feats, abilities);
            Assert.That(saves.RacialFortitudeBonus, Is.EqualTo(saveBonus));
        }

        [Test]
        public void ApplyStrongFortitudeBonus()
        {
            strongFortitude.Add("creature");

            var saves = savesGenerator.GenerateWith("creature", hitPoints, feats, abilities);
            Assert.That(saves.Fortitude, Is.EqualTo(2));
            Assert.That(saves.Reflex, Is.EqualTo(0));
            Assert.That(saves.Will, Is.EqualTo(0));
        }

        [Test]
        public void ApplyWeakFortitudeBonus()
        {
            var saves = savesGenerator.GenerateWith("creature", hitPoints, feats, abilities);
            Assert.That(saves.Fortitude, Is.EqualTo(0));
            Assert.That(saves.Reflex, Is.EqualTo(0));
            Assert.That(saves.Will, Is.EqualTo(0));
        }

        [Test]
        public void ApplyStrongReflexBonus()
        {
            strongReflex.Add("creature");

            var saves = savesGenerator.GenerateWith("creature", hitPoints, feats, abilities);
            Assert.That(saves.Fortitude, Is.EqualTo(0));
            Assert.That(saves.Reflex, Is.EqualTo(2));
            Assert.That(saves.Will, Is.EqualTo(0));
        }

        [Test]
        public void ApplyWeakReflexBonus()
        {
            var saves = savesGenerator.GenerateWith("creature", hitPoints, feats, abilities);
            Assert.That(saves.Fortitude, Is.EqualTo(0));
            Assert.That(saves.Reflex, Is.EqualTo(0));
            Assert.That(saves.Will, Is.EqualTo(0));
        }

        [Test]
        public void ApplyStrongWillBonus()
        {
            strongWill.Add("creature");

            var saves = savesGenerator.GenerateWith("creature", hitPoints, feats, abilities);
            Assert.That(saves.Fortitude, Is.EqualTo(0));
            Assert.That(saves.Reflex, Is.EqualTo(0));
            Assert.That(saves.Will, Is.EqualTo(2));
        }

        [Test]
        public void ApplyWeakWillBonus()
        {
            var saves = savesGenerator.GenerateWith("creature", hitPoints, feats, abilities);
            Assert.That(saves.Fortitude, Is.EqualTo(0));
            Assert.That(saves.Reflex, Is.EqualTo(0));
            Assert.That(saves.Will, Is.EqualTo(0));
        }

        [Test]
        public void ApplyFeatBonuses()
        {
            SetUpFeats();

            var saves = savesGenerator.GenerateWith("creature", hitPoints, feats, abilities);
            Assert.That(saves.Fortitude, Is.EqualTo(8));
            Assert.That(saves.Reflex, Is.EqualTo(10));
            Assert.That(saves.Will, Is.EqualTo(12));
            Assert.That(saves.CircumstantialBonus, Is.False);
        }

        private void SetUpFeats()
        {
            for (var i = 0; i < 8; i++)
            {
                var feat = new Feat();
                feat.Name = string.Format("Feat{0}", i);
                feat.Power = i + 1;

                feats.Add(feat);
            }

            feats[0].Foci = new[] { FeatConstants.Foci.All };
            feats[1].Foci = new[] { SaveConstants.Fortitude };
            feats[2].Foci = new[] { SaveConstants.Reflex };
            feats[3].Foci = new[] { SaveConstants.Will };

            allSaveFeats.Add(feats[0].Name);
            allSaveFeats.Add(feats[1].Name);
            allSaveFeats.Add(feats[2].Name);
            allSaveFeats.Add(feats[3].Name);
            fortitudeSaveFeats.Add(feats[4].Name);
            reflexSaveFeats.Add(feats[5].Name);
            willSaveFeats.Add(feats[6].Name);
        }

        [Test]
        public void CircumstantialBonusIfFeatHaveQualifications()
        {
            SetUpFeats();

            feats[0].Foci = new[] { FeatConstants.Foci.All + " against thing" };
            feats[1].Foci = new[] { SaveConstants.Fortitude + " against thing" };
            feats[2].Foci = new[] { SaveConstants.Reflex + " against thing" };
            feats[3].Foci = new[] { SaveConstants.Will + " against thing" };

            var saves = savesGenerator.GenerateWith("creature", hitPoints, feats, abilities);
            Assert.That(saves.Fortitude, Is.EqualTo(5));
            Assert.That(saves.Reflex, Is.EqualTo(6));
            Assert.That(saves.Will, Is.EqualTo(7));
            Assert.That(saves.CircumstantialBonus, Is.True);
        }

        [Test]
        public void DoNotOverwriteCircumstantialBonus()
        {
            SetUpFeats();

            feats[0].Foci = new[] { FeatConstants.Foci.All + " against thing" };

            var saves = savesGenerator.GenerateWith("creature", hitPoints, feats, abilities);
            Assert.That(saves.Fortitude, Is.EqualTo(7));
            Assert.That(saves.Reflex, Is.EqualTo(9));
            Assert.That(saves.Will, Is.EqualTo(11));
            Assert.That(saves.CircumstantialBonus, Is.True);
        }

        [Test]
        public void ApplyAllBonuses()
        {
            abilities[AbilityConstants.Constitution].BaseValue = 9266;
            abilities[AbilityConstants.Dexterity].BaseValue = 90210;
            abilities[AbilityConstants.Wisdom].BaseValue = -42;

            strongWill.Add("creature");
            strongFortitude.Add("creature");

            SetUpFeats();

            var saves = savesGenerator.GenerateWith("creature", hitPoints, feats, abilities);
            Assert.That(saves.Fortitude, Is.EqualTo(4638));
            Assert.That(saves.Reflex, Is.EqualTo(45110));
            Assert.That(saves.Will, Is.EqualTo(9));
        }
    }
}
