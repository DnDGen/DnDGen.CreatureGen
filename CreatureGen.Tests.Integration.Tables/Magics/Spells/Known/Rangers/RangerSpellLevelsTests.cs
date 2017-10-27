using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Rangers
{
    [TestFixture]
    public class RangerSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Ranger);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.Alarm,
                SpellConstants.AnimalMessenger,
                SpellConstants.CalmAnimals,
                SpellConstants.CharmAnimal,
                SpellConstants.DelayPoison,
                SpellConstants.DetectAnimalsOrPlants,
                SpellConstants.DetectPoison,
                SpellConstants.DetectSnaresAndPits,
                SpellConstants.EndureElements,
                SpellConstants.Entangle,
                SpellConstants.HideFromAnimals,
                SpellConstants.Jump,
                SpellConstants.Longstrider,
                SpellConstants.MagicFang,
                SpellConstants.PassWithoutTrace,
                SpellConstants.ReadMagic,
                SpellConstants.ResistEnergy,
                SpellConstants.SpeakWithAnimals,
                SpellConstants.SummonNaturesAllyI,
                SpellConstants.Barkskin,
                SpellConstants.BearsEndurance,
                SpellConstants.CatsGrace,
                SpellConstants.CureInflictLightWounds,
                SpellConstants.HoldAnimal,
                SpellConstants.OwlsWisdom,
                SpellConstants.ProtectionFromEnergy,
                SpellConstants.Snare,
                SpellConstants.SpeakWithPlants,
                SpellConstants.SpikeGrowth,
                SpellConstants.SummonNaturesAllyII,
                SpellConstants.WindWall,
                SpellConstants.CommandPlants,
                SpellConstants.CureInflictModerateWounds,
                SpellConstants.Darkvision,
                SpellConstants.DiminishPlants,
                SpellConstants.MagicFang_Greater,
                SpellConstants.NeutralizePoison,
                SpellConstants.PlantGrowth,
                SpellConstants.ReduceAnimal,
                SpellConstants.RemoveDisease,
                SpellConstants.RepelVermin,
                SpellConstants.SummonNaturesAllyIII,
                SpellConstants.TreeShape,
                SpellConstants.WaterWalk,
                SpellConstants.AnimalGrowth,
                SpellConstants.CommuneWithNature,
                SpellConstants.CureInflictSeriousWounds,
                SpellConstants.FreedomOfMovement,
                SpellConstants.Nondetection,
                SpellConstants.SummonNaturesAllyIV,
                SpellConstants.TreeStride
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllRangerSpellsInAdjustmentsTable()
        {
            var spellGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Ranger]);
        }

        [TestCase(SpellConstants.Alarm, 1)]
        [TestCase(SpellConstants.AnimalMessenger, 1)]
        [TestCase(SpellConstants.CalmAnimals, 1)]
        [TestCase(SpellConstants.CharmAnimal, 1)]
        [TestCase(SpellConstants.DelayPoison, 1)]
        [TestCase(SpellConstants.DetectAnimalsOrPlants, 1)]
        [TestCase(SpellConstants.DetectPoison, 1)]
        [TestCase(SpellConstants.DetectSnaresAndPits, 1)]
        [TestCase(SpellConstants.EndureElements, 1)]
        [TestCase(SpellConstants.Entangle, 1)]
        [TestCase(SpellConstants.HideFromAnimals, 1)]
        [TestCase(SpellConstants.Jump, 1)]
        [TestCase(SpellConstants.Longstrider, 1)]
        [TestCase(SpellConstants.MagicFang, 1)]
        [TestCase(SpellConstants.PassWithoutTrace, 1)]
        [TestCase(SpellConstants.ReadMagic, 1)]
        [TestCase(SpellConstants.ResistEnergy, 1)]
        [TestCase(SpellConstants.SpeakWithAnimals, 1)]
        [TestCase(SpellConstants.SummonNaturesAllyI, 1)]
        [TestCase(SpellConstants.Barkskin, 2)]
        [TestCase(SpellConstants.BearsEndurance, 2)]
        [TestCase(SpellConstants.CatsGrace, 2)]
        [TestCase(SpellConstants.CureInflictLightWounds, 2)]
        [TestCase(SpellConstants.HoldAnimal, 2)]
        [TestCase(SpellConstants.OwlsWisdom, 2)]
        [TestCase(SpellConstants.ProtectionFromEnergy, 2)]
        [TestCase(SpellConstants.Snare, 2)]
        [TestCase(SpellConstants.SpeakWithPlants, 2)]
        [TestCase(SpellConstants.SpikeGrowth, 2)]
        [TestCase(SpellConstants.SummonNaturesAllyII, 2)]
        [TestCase(SpellConstants.WindWall, 2)]
        [TestCase(SpellConstants.CommandPlants, 3)]
        [TestCase(SpellConstants.CureInflictModerateWounds, 3)]
        [TestCase(SpellConstants.Darkvision, 3)]
        [TestCase(SpellConstants.DiminishPlants, 3)]
        [TestCase(SpellConstants.MagicFang_Greater, 3)]
        [TestCase(SpellConstants.NeutralizePoison, 3)]
        [TestCase(SpellConstants.PlantGrowth, 3)]
        [TestCase(SpellConstants.ReduceAnimal, 3)]
        [TestCase(SpellConstants.RemoveDisease, 3)]
        [TestCase(SpellConstants.RepelVermin, 3)]
        [TestCase(SpellConstants.SummonNaturesAllyIII, 3)]
        [TestCase(SpellConstants.TreeShape, 3)]
        [TestCase(SpellConstants.WaterWalk, 3)]
        [TestCase(SpellConstants.AnimalGrowth, 4)]
        [TestCase(SpellConstants.CommuneWithNature, 4)]
        [TestCase(SpellConstants.CureInflictSeriousWounds, 4)]
        [TestCase(SpellConstants.FreedomOfMovement, 4)]
        [TestCase(SpellConstants.Nondetection, 4)]
        [TestCase(SpellConstants.SummonNaturesAllyIV, 4)]
        [TestCase(SpellConstants.TreeStride, 4)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
