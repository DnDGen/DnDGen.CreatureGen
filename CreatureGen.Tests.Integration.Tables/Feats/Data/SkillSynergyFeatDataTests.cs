using CreatureGen.Feats;
using CreatureGen.Skills;
using CreatureGen.Tables;
using NUnit.Framework;
using System.Collections.Generic;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data
{
    [TestFixture]
    public class SkillSynergyFeatDataTests : DataTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Collection.SkillSynergyFeatData; }
        }

        protected override void PopulateIndices(IEnumerable<string> collection)
        {
            indices[DataIndexConstants.SkillSynergyFeatData.FeatNameIndex] = "Feat Name";
            indices[DataIndexConstants.SkillSynergyFeatData.FocusTypeIndex] = "Focus Type";
            indices[DataIndexConstants.SkillSynergyFeatData.PowerIndex] = "Power";
        }

        [Test]
        public void CollectionNames()
        {
            var names = new[]
            {
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Bluff, null, SkillConstants.Diplomacy, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Bluff, null, SkillConstants.Disguise, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Bluff, null, SkillConstants.Intimidate, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Bluff, null, SkillConstants.SleightOfHand, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, null, SkillConstants.Appraise, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.DecipherScript, null, SkillConstants.UseMagicDevice, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.EscapeArtist, null, SkillConstants.UseRope, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.HandleAnimal, null, SkillConstants.Diplomacy, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.HandleAnimal, null, SkillConstants.Ride, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Jump, null, SkillConstants.Tumble, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Arcana, SkillConstants.Spellcraft, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering, SkillConstants.Search, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Dungeoneering, SkillConstants.Survival, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Geography, SkillConstants.Survival, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.History, SkillConstants.Knowledge, "bardic"),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local, SkillConstants.GatherInformation, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature, SkillConstants.Survival, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty, SkillConstants.Diplomacy, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ThePlanes, SkillConstants.Survival, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Search, null, SkillConstants.Survival, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.SenseMotive, null, SkillConstants.Diplomacy, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Spellcraft, null, SkillConstants.UseMagicDevice, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Survival, null, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Tumble, null, SkillConstants.Balance, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Tumble, null, SkillConstants.Jump, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.UseMagicDevice, null, SkillConstants.Spellcraft, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.UseRope, null, SkillConstants.Climb, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.UseRope, null, SkillConstants.EscapeArtist, null),
            };

            AssertCollectionNames(names);
        }

        [TestCase(SkillConstants.Bluff, null, SkillConstants.Diplomacy, null, null)]
        [TestCase(SkillConstants.Bluff, null, SkillConstants.Disguise, null, "acting")]
        [TestCase(SkillConstants.Bluff, null, SkillConstants.Intimidate, null, null)]
        [TestCase(SkillConstants.Bluff, null, SkillConstants.SleightOfHand, null, null)]
        [TestCase(SkillConstants.Craft, null, SkillConstants.Appraise, null, "related to items made with your Craft skill")]
        [TestCase(SkillConstants.DecipherScript, null, SkillConstants.UseMagicDevice, null, "with scrolls")]
        [TestCase(SkillConstants.EscapeArtist, null, SkillConstants.UseRope, null, "binding someone")]
        [TestCase(SkillConstants.HandleAnimal, null, SkillConstants.Diplomacy, null, "wild empathy")]
        [TestCase(SkillConstants.HandleAnimal, null, SkillConstants.Ride, null, null)]
        [TestCase(SkillConstants.Jump, null, SkillConstants.Tumble, null, null)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Arcana, SkillConstants.Spellcraft, null, null)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering, SkillConstants.Search, null, "find secret doors or compartments")]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Dungeoneering, SkillConstants.Survival, null, "underground")]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Geography, SkillConstants.Survival, null, "keep from getting lost or avoid natural hazards")]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.History, SkillConstants.Knowledge, "bardic", null)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local, SkillConstants.GatherInformation, null, null)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature, SkillConstants.Survival, null, "in aboveground natural environments")]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty, SkillConstants.Diplomacy, null, null)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ThePlanes, SkillConstants.Survival, null, "on other planes")]
        [TestCase(SkillConstants.Search, null, SkillConstants.Survival, null, "following tracks")]
        [TestCase(SkillConstants.SenseMotive, null, SkillConstants.Diplomacy, null, null)]
        [TestCase(SkillConstants.Spellcraft, null, SkillConstants.UseMagicDevice, null, "with scrolls")]
        [TestCase(SkillConstants.Survival, null, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature, null)]
        [TestCase(SkillConstants.Tumble, null, SkillConstants.Balance, null, null)]
        [TestCase(SkillConstants.Tumble, null, SkillConstants.Jump, null, null)]
        [TestCase(SkillConstants.UseMagicDevice, null, SkillConstants.Spellcraft, null, "decipher scrolls")]
        [TestCase(SkillConstants.UseRope, null, SkillConstants.Climb, null, "with rope")]
        [TestCase(SkillConstants.UseRope, null, SkillConstants.EscapeArtist, null, "escaping rope bonds")]
        public void SkillSynergyFeatData(string sourceSkill, string sourceFocus, string targetSkillName, string targetSkillFocus, string condition)
        {
            var focus = targetSkillName;

            if (!string.IsNullOrEmpty(targetSkillFocus))
                focus += $"/{targetSkillFocus}";

            if (!string.IsNullOrEmpty(condition))
                focus += $": {condition}";

            var data = DataIndexConstants.SkillSynergyFeatData.InitializeData();

            data[DataIndexConstants.SkillSynergyFeatData.FeatNameIndex] = FeatConstants.SpecialQualities.SkillBonus;
            data[DataIndexConstants.SkillSynergyFeatData.FocusTypeIndex] = focus;
            data[DataIndexConstants.SkillSynergyFeatData.PowerIndex] = 2.ToString();

            var key = DataIndexConstants.SkillSynergyFeatData.BuildDataKey(sourceSkill, sourceFocus, targetSkillName, targetSkillFocus);

            Data(key, data);
        }
    }
}