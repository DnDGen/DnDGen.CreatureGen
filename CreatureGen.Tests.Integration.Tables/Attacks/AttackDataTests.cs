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
            indices[DataIndexConstants.AttackData.DamageIndex] = "Damage";
            indices[DataIndexConstants.AttackData.IsMeleeIndex] = "Is Melee";
            indices[DataIndexConstants.AttackData.IsNaturalIndex] = "Is Natural";
            indices[DataIndexConstants.AttackData.IsPrimaryIndex] = "Is Primary";
            indices[DataIndexConstants.AttackData.IsSpecialIndex] = "Is Special";
            indices[DataIndexConstants.AttackData.NameIndex] = "Name";
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

            var data = entries.Select(e => AttackHelper.BuildData(e)).ToArray();

            AssertDistinctCollection(creature, data);
        }
    }
}
