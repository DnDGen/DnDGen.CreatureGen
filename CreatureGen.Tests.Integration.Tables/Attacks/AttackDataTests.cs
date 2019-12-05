using CreatureGen.Creatures;
using CreatureGen.Selectors.Helpers;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using EventGen;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Attacks
{
    [TestFixture]
    public class AttackDataTests : DataTests
    {
        [Inject]
        public ICollectionSelector CollectionSelector { get; set; }
        [Inject]
        public ClientIDManager ClientIDManager { get; set; }

        protected override string tableName => TableNameConstants.Collection.AttackData;

        protected override void PopulateIndices(IEnumerable<string> collection)
        {
            indices[DataIndexConstants.AttackData.DamageRollIndex] = "Damage Roll";
            indices[DataIndexConstants.AttackData.IsMeleeIndex] = "Is Melee";
            indices[DataIndexConstants.AttackData.IsNaturalIndex] = "Is Natural";
            indices[DataIndexConstants.AttackData.IsPrimaryIndex] = "Is Primary";
            indices[DataIndexConstants.AttackData.IsSpecialIndex] = "Is Special";
            indices[DataIndexConstants.AttackData.NameIndex] = "Name";
            indices[DataIndexConstants.AttackData.AttackTypeIndex] = "Attack Type";
            indices[DataIndexConstants.AttackData.DamageEffectIndex] = "Damage Effect";
            indices[DataIndexConstants.AttackData.FrequencyQuantityIndex] = "Frequency Quantity";
            indices[DataIndexConstants.AttackData.FrequencyTimePeriodIndex] = "Frequency Time Period";
            indices[DataIndexConstants.AttackData.SaveAbilityIndex] = "Save Ability";
            indices[DataIndexConstants.AttackData.SaveIndex] = "Save";
            indices[DataIndexConstants.AttackData.DamageBonusMultiplierIndex] = "Damage Bonus Multiplier";
            indices[DataIndexConstants.AttackData.SaveDcBonusIndex] = "Save DC Bonus Multiplier";
        }

        [SetUp]
        public void Setup()
        {
            var clientId = Guid.NewGuid();
            ClientIDManager.SetClientID(clientId);
        }

        [Test]
        public void AttackDataNames()
        {
            var names = CreatureConstants.All();
            AssertCollectionNames(names);
        }

        [TestCaseSource(typeof(AttackTestData), "Creatures")]
        public void AttackData(string creature, List<string[]> entries)
        {
            if (!entries.Any())
                Assert.Fail("Test case did not specify attacks or NONE");

            if (entries[0][DataIndexConstants.AttackData.NameIndex] == AttackTestData.None)
                entries.Clear();

            AssertData(creature, entries, e => AttackHelper.BuildData(e));
        }
    }
}
