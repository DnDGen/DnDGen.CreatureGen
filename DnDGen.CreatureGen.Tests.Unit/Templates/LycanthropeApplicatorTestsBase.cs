using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Generators.Defenses;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Generators.Skills;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Tests.Unit.TestCaseSources;
using DnDGen.Infrastructure.Models;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using Moq;
using Moq.Language;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Templates
{
    internal abstract class LycanthropeApplicatorTestsBase
    {
        protected LycanthropeApplicator applicator;
        protected Mock<ICollectionDataSelector<CreatureDataSelection>> mockCreatureDataSelector;
        protected Mock<IHitPointsGenerator> mockHitPointsGenerator;
        protected Mock<Dice> mockDice;
        protected Mock<ICollectionTypeAndAmountSelector> mockTypeAndAmountSelector;
        protected Mock<IFeatsGenerator> mockFeatsGenerator;
        protected Mock<IAttacksGenerator> mockAttacksGenerator;
        protected Mock<ISavesGenerator> mockSavesGenerator;
        protected Mock<ISkillsGenerator> mockSkillsGenerator;
        protected Mock<ISpeedsGenerator> mockSpeedsGenerator;
        protected Mock<IDemographicsGenerator> mockDemographicsGenerator;
        protected HitPoints animalHitPoints;
        protected Random random;
        protected CreatureDataSelection animalData;
        protected int baseRoll;
        protected double baseAverage;
        protected List<Skill> animalSkills;
        protected List<Attack> animalAttacks;
        protected List<Feat> animalSpecialQualities;
        protected List<Feat> animalFeats;
        protected int animalBaseAttack;
        protected Dictionary<string, Save> animalSaves;
        protected Dictionary<string, Measurement> animalSpeeds;
        protected Dictionary<string, ISetupSequentialResult<IEnumerable<int>>> rollSequences;
        protected Dictionary<string, ISetupSequentialResult<double>> averageSequences;

        [SetUp]
        public void BaseSetup()
        {
            mockCreatureDataSelector = new Mock<ICollectionDataSelector<CreatureDataSelection>>();
            mockHitPointsGenerator = new Mock<IHitPointsGenerator>();
            mockDice = new Mock<Dice>();
            mockTypeAndAmountSelector = new Mock<ICollectionTypeAndAmountSelector>();
            mockFeatsGenerator = new Mock<IFeatsGenerator>();
            mockAttacksGenerator = new Mock<IAttacksGenerator>();
            mockSavesGenerator = new Mock<ISavesGenerator>();
            mockSkillsGenerator = new Mock<ISkillsGenerator>();
            mockSpeedsGenerator = new Mock<ISpeedsGenerator>();
            mockDemographicsGenerator = new Mock<IDemographicsGenerator>();

            applicator = new LycanthropeApplicator(
                mockCreatureDataSelector.Object,
                mockHitPointsGenerator.Object,
                mockDice.Object,
                mockTypeAndAmountSelector.Object,
                mockFeatsGenerator.Object,
                mockAttacksGenerator.Object,
                mockSavesGenerator.Object,
                mockSkillsGenerator.Object,
                mockSpeedsGenerator.Object,
                mockDemographicsGenerator.Object)
            {
                LycanthropeSpecies = "my lycanthrope",
                AnimalSpecies = "my animal"
            };

            animalHitPoints = new HitPoints();
            random = new Random();
            animalData = new CreatureDataSelection();
            animalSkills = [];
            animalAttacks = [];
            animalSpecialQualities = [];
            animalFeats = [];
            animalSaves = [];
            animalSpeeds = [];
            rollSequences = [];
            averageSequences = [];
        }

        protected void SetUpAnimal(
            string animal,
            Creature baseCreature,
            int naturalArmor = -1,
            string size = null,
            double hitDiceQuantity = -1,
            int hitDiceDie = 0,
            int roll = 0,
            double average = 0)
        {
            SetUpAnimalBasics(animal, naturalArmor, size, hitDiceQuantity, hitDiceDie);

            //Hit points
            var hitDie = animalHitPoints.HitDice[^1];

            mockHitPointsGenerator
                .Setup(g => g.GenerateFor(
                    hitDie.Quantity,
                    hitDie.HitDie,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Animal),
                    baseCreature.Abilities[AbilityConstants.Constitution],
                    animalData.Size,
                    0))
                .Returns(animalHitPoints);

            if (roll == 0)
                roll = random.Next(hitDie.RoundedQuantity * hitDie.HitDie) + hitDie.RoundedQuantity;

            SetUpRoll(hitDie, roll);

            if (average == 0)
                average = hitDie.RoundedQuantity * hitDie.HitDie / 2d + hitDie.RoundedQuantity;

            SetUpRoll(hitDie, average);

            //Skills
            animalSkills.Add(new Skill("animal skill 1", baseCreature.Abilities[AbilityConstants.Strength], hitDie.RoundedQuantity + 3)
            {
                ClassSkill = true,
                Ranks = random.Next(hitDie.RoundedQuantity)
            });
            animalSkills.Add(new Skill("animal skill 2", baseCreature.Abilities[AbilityConstants.Strength], hitDie.RoundedQuantity + 3)
            {
                ClassSkill = true,
                Ranks = random.Next(hitDie.RoundedQuantity)
            });

            mockSkillsGenerator
                .Setup(g => g.GenerateFor(
                    animalHitPoints,
                    animal,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Animal),
                    baseCreature.Abilities,
                    baseCreature.CanUseEquipment,
                    animalData.Size,
                    false))
                .Returns(animalSkills);

            mockSkillsGenerator
                .Setup(g => g.ApplySkillPointsAsRanks(
                    It.IsAny<IEnumerable<Skill>>(),
                    animalHitPoints,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Animal),
                    baseCreature.Abilities,
                    false))
                .Returns(animalSkills);

            //Special Qualities
            animalSpecialQualities.Add(new Feat { Name = "animal special quality 1" });
            animalSpecialQualities.Add(new Feat { Name = "animal special quality 2" });

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    animal,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Animal),
                    animalHitPoints,
                    baseCreature.Abilities,
                    animalSkills,
                    animalData.CanUseEquipment,
                    animalData.Size,
                    baseCreature.Alignment))
                .Returns(animalSpecialQualities);

            //Attacks
            animalBaseAttack = random.Next(100);

            mockAttacksGenerator
                .Setup(g => g.GenerateBaseAttackBonus(
                    animalData.BaseAttackQuality,
                    animalHitPoints))
                .Returns(animalBaseAttack);

            animalAttacks.Add(new Attack { Name = "animal attack 1" });
            animalAttacks.Add(new Attack { Name = "animal attack 2" });

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    animal,
                    animalData.Size,
                    baseCreature.BaseAttackBonus + animalBaseAttack,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.HitDice[0].RoundedQuantity + hitDie.RoundedQuantity,
                    baseCreature.Demographics.Gender))
                .Returns(animalAttacks);

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    animalAttacks,
                    It.IsAny<IEnumerable<Feat>>(),
                    baseCreature.Abilities))
                .Returns(animalAttacks);

            //Feats
            animalFeats.Add(new Feat { Name = "animal feat 1" });
            animalFeats.Add(new Feat { Name = "animal feat 2" });

            mockFeatsGenerator
                .Setup(g => g.GenerateFeats(
                    animalHitPoints,
                    animalBaseAttack,
                    baseCreature.Abilities,
                    animalSkills,
                    animalAttacks,
                    animalSpecialQualities,
                    animalData.CasterLevel,
                    baseCreature.Speeds,
                    animalData.NaturalArmor,
                    animalData.NumberOfHands,
                    animalData.Size,
                    animalData.CanUseEquipment))
                .Returns(animalFeats);

            //Saves
            animalSaves[SaveConstants.Fortitude] = new Save { BaseValue = random.Next(20) + 1 };
            animalSaves[SaveConstants.Reflex] = new Save { BaseValue = random.Next(20) + 1 };
            animalSaves[SaveConstants.Will] = new Save { BaseValue = random.Next(20) + 1 };

            mockSavesGenerator
                .Setup(g => g.GenerateWith(
                    animal,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Animal),
                    animalHitPoints,
                    It.Is<IEnumerable<Feat>>(ff => ff.IsEquivalentTo(animalSpecialQualities.Union(animalFeats))),
                    baseCreature.Abilities))
                .Returns(animalSaves);

            //Speeds
            animalSpeeds[SpeedConstants.Land] = new Measurement("feet per round") { Value = random.Next(100) + 1 };

            mockSpeedsGenerator
                .Setup(g => g.Generate(animal))
                .Returns(animalSpeeds);
        }

        protected void SetUpAnimalBasics(
            string animal,
            int naturalArmor = -1,
            string size = null,
            double hitDiceQuantity = -1,
            int hitDiceDie = 0)
        {
            //Data
            animalData.Size = size ?? "animal size";
            animalData.CasterLevel = 0;
            animalData.NumberOfHands = random.Next(3);
            animalData.CanUseEquipment = false;
            animalData.NaturalArmor = naturalArmor > -1 ? naturalArmor : random.Next(20);
            animalData.HitDiceQuantity = hitDiceQuantity > -1 ? hitDiceQuantity : random.Next(30) + 1;
            animalData.HitDie = hitDiceDie > 0 ? hitDiceDie : random.Next(7) + 6;
            animalData.BaseAttackQuality = BaseAttackQuality.Average;
            animalData.Types = [CreatureConstants.Types.Animal];

            mockCreatureDataSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.CreatureData, animal))
                .Returns(animalData);

            //Hit points
            var hitDie = new HitDice
            {
                Quantity = animalData.GetEffectiveHitDiceQuantity(false),
                HitDie = animalData.HitDie,
            };
            animalHitPoints.HitDice.Add(hitDie);
        }

        protected void SetAnimalAbilityAdjustments(string animal, int str = 0, int dex = 0, int con = 0)
        {
            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments, animal))
                .Returns(
                [
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Strength, AmountAsDouble = str },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Dexterity, AmountAsDouble = dex },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Constitution, AmountAsDouble = con },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Intelligence, AmountAsDouble = -666 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Wisdom, AmountAsDouble = -666 },
                    new TypeAndAmountDataSelection { Type = AbilityConstants.Charisma, AmountAsDouble = -666 },
                ]);
        }

        protected void SetUpRoll(HitDice hitDice, int roll)
        {
            var key = $"{hitDice.RoundedQuantity}d{hitDice.HitDie}";

            if (!rollSequences.ContainsKey(key))
                rollSequences[key] = mockDice.SetupSequence(d => d.Roll(hitDice.RoundedQuantity).d(hitDice.HitDie).AsIndividualRolls<int>());

            rollSequences[key] = rollSequences[key].Returns([roll]);
        }

        protected void SetUpRoll(HitDice hitDice, double average)
        {
            var key = $"{hitDice.RoundedQuantity}d{hitDice.HitDie}";

            if (!averageSequences.ContainsKey(key))
                averageSequences[key] = mockDice.SetupSequence(d => d.Roll(hitDice.RoundedQuantity).d(hitDice.HitDie).AsPotentialAverage());

            averageSequences[key] = averageSequences[key].Returns(average);
        }
    }
}
