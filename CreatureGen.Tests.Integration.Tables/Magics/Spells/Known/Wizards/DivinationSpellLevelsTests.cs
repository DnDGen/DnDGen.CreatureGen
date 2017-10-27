using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Wizards
{
    [TestFixture]
    public class DivinationSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Schools.Divination);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.DetectPoison,
                SpellConstants.DetectMagic,
                SpellConstants.ReadMagic,
                SpellConstants.ComprehendLanguages,
                SpellConstants.DetectSecretDoors,
                SpellConstants.DetectUndead,
                SpellConstants.Identify,
                SpellConstants.TrueStrike,
                SpellConstants.DetectThoughts,
                SpellConstants.LocateObject,
                SpellConstants.SeeInvisibility,
                SpellConstants.ArcaneSight,
                SpellConstants.ClairaudienceClairvoyance,
                SpellConstants.Tongues,
                SpellConstants.ArcaneEye,
                SpellConstants.DetectScrying,
                SpellConstants.LocateCreature,
                SpellConstants.Scrying,
                SpellConstants.ContactOtherPlane,
                SpellConstants.PryingEyes,
                SpellConstants.TelepathicBond,
                SpellConstants.AnalyzeDweomer,
                SpellConstants.LegendLore,
                SpellConstants.TrueSeeing,
                SpellConstants.ArcaneSight_Greater,
                SpellConstants.Scrying_Greater,
                SpellConstants.Vision,
                SpellConstants.DiscernLocation,
                SpellConstants.MomentOfPrescience,
                SpellConstants.PryingEyes_Greater,
                SpellConstants.Foresight
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllDivinationSpellsInAdjustmentsTable()
        {
            var spellGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Schools.Divination]);
        }

        [TestCase(SpellConstants.DetectPoison, 0)]
        [TestCase(SpellConstants.DetectMagic, 0)]
        [TestCase(SpellConstants.ReadMagic, 0)]
        [TestCase(SpellConstants.ComprehendLanguages, 1)]
        [TestCase(SpellConstants.DetectSecretDoors, 1)]
        [TestCase(SpellConstants.DetectUndead, 1)]
        [TestCase(SpellConstants.Identify, 1)]
        [TestCase(SpellConstants.TrueStrike, 1)]
        [TestCase(SpellConstants.DetectThoughts, 2)]
        [TestCase(SpellConstants.LocateObject, 2)]
        [TestCase(SpellConstants.SeeInvisibility, 2)]
        [TestCase(SpellConstants.ArcaneSight, 3)]
        [TestCase(SpellConstants.ClairaudienceClairvoyance, 3)]
        [TestCase(SpellConstants.Tongues, 3)]
        [TestCase(SpellConstants.ArcaneEye, 4)]
        [TestCase(SpellConstants.DetectScrying, 4)]
        [TestCase(SpellConstants.LocateCreature, 4)]
        [TestCase(SpellConstants.Scrying, 4)]
        [TestCase(SpellConstants.ContactOtherPlane, 5)]
        [TestCase(SpellConstants.PryingEyes, 5)]
        [TestCase(SpellConstants.TelepathicBond, 5)]
        [TestCase(SpellConstants.AnalyzeDweomer, 6)]
        [TestCase(SpellConstants.LegendLore, 6)]
        [TestCase(SpellConstants.TrueSeeing, 6)]
        [TestCase(SpellConstants.ArcaneSight_Greater, 7)]
        [TestCase(SpellConstants.Scrying_Greater, 7)]
        [TestCase(SpellConstants.Vision, 7)]
        [TestCase(SpellConstants.DiscernLocation, 8)]
        [TestCase(SpellConstants.MomentOfPrescience, 8)]
        [TestCase(SpellConstants.PryingEyes_Greater, 8)]
        [TestCase(SpellConstants.Foresight, 9)]
        public void SpellLevel(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
