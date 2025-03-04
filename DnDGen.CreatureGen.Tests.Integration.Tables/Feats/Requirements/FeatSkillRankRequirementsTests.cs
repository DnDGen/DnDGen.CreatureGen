﻿using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Feats.Requirements
{
    [TestFixture]
    public class FeatSkillRankRequirementsTests : TypesAndAmountsTests
    {
        protected override string tableName => TableNameConstants.TypeAndAmount.FeatSkillRankRequirements;

        [Test]
        public void FeatSkillRankRequirementsNames()
        {
            var names = GetNames();

            AssertCollectionNames(names);
        }

        private IEnumerable<string> GetNames()
        {
            var feats = FeatConstants.All();
            var metamagic = FeatConstants.Metamagic.All();
            var monster = FeatConstants.Monster.All();
            var craft = FeatConstants.MagicItemCreation.All();

            var names = feats.Union(metamagic).Union(monster).Union(craft);

            return names;
        }

        [TestCaseSource(typeof(SkillRankRequirementsTestData), nameof(SkillRankRequirementsTestData.Feats))]
        [TestCaseSource(typeof(SkillRankRequirementsTestData), nameof(SkillRankRequirementsTestData.Metamagic))]
        [TestCaseSource(typeof(SkillRankRequirementsTestData), nameof(SkillRankRequirementsTestData.Monster))]
        [TestCaseSource(typeof(SkillRankRequirementsTestData), nameof(SkillRankRequirementsTestData.Craft))]
        public void SkillRankRequirements(string name, Dictionary<string, int> typesAndAmounts)
        {
            AssertTypesAndAmounts(name, typesAndAmounts);
        }

        [Test]
        public void NoSkillRankRequirements()
        {
            var names = GetNames();
            var feats = SkillRankRequirementsTestData.GetFeatSkillRankRequirementNames();

            var emptyRequirements = names.Except(feats);

            foreach (var requirement in emptyRequirements)
            {
                var empty = new Dictionary<string, int>();
                AssertTypesAndAmounts(requirement, empty);
            }
        }

        public class SkillRankRequirementsTestData
        {
            public static IEnumerable Feats
            {
                get
                {
                    var testCases = new Dictionary<string, Dictionary<string, int>>();
                    var feats = GetFeatSkillRankRequirementNames();

                    foreach (var feat in feats)
                    {
                        testCases[feat] = new Dictionary<string, int>();
                    }

                    testCases[FeatConstants.MountedArchery][SkillConstants.Ride] = 1;
                    testCases[FeatConstants.MountedCombat][SkillConstants.Ride] = 1;
                    testCases[FeatConstants.RideByAttack][SkillConstants.Ride] = 1;
                    testCases[FeatConstants.SpiritedCharge][SkillConstants.Ride] = 1;
                    testCases[FeatConstants.Trample][SkillConstants.Ride] = 1;

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }

            public static IEnumerable<string> GetFeatSkillRankRequirementNames()
            {
                return new[]
                {
                    FeatConstants.MountedArchery,
                    FeatConstants.MountedCombat,
                    FeatConstants.RideByAttack,
                    FeatConstants.SpiritedCharge,
                    FeatConstants.Trample,
                };
            }

            public static IEnumerable Metamagic
            {
                get
                {
                    var testCases = new Dictionary<string, Dictionary<string, int>>();

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }

            public static IEnumerable Monster
            {
                get
                {
                    var testCases = new Dictionary<string, Dictionary<string, int>>();

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }

            public static IEnumerable Craft
            {
                get
                {
                    var testCases = new Dictionary<string, Dictionary<string, int>>();

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }
        }
    }
}
