using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Collections;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Selectors.Collections
{
    public class AttackSelectorTests : IntegrationTests
    {
        private IAttackSelector attackSelector;

        [SetUp]
        public void Setup()
        {
            attackSelector = GetNewInstanceOf<IAttackSelector>();
        }

        [Test]
        public void BUG_AttackDamageDataIsPreservedThroughIterator()
        {
            var attackSelections = attackSelector.Select(CreatureConstants.Human, SizeConstants.Medium);
            var unarmedStrike = attackSelections.FirstOrDefault(a => a.Name == "Unarmed Strike");

            Assert.That(unarmedStrike, Is.Not.Null);
            Assert.That(unarmedStrike.Damages, Has.Count.EqualTo(1));
            Assert.That(unarmedStrike.Damages[0].Roll, Is.EqualTo("1d3"));
            Assert.That(unarmedStrike.Damages[0].Type, Is.EqualTo("Bludgeoning"));
        }
    }
}
