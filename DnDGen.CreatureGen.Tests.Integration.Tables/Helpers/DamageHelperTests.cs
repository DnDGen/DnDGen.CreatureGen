using NUnit.Framework;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Helpers
{
    [TestFixture]
    public class DamageHelperTests : IntegrationTests
    {
        private DamageHelper damageHelper;

        [SetUp]
        public void Setup()
        {
            damageHelper = GetNewInstanceOf<DamageHelper>();
        }

        [Test]
        public void AttackDamageKeysAreUnique()
        {
            var creatureAttackDamageKeys = damageHelper.GetAllCreatureDamageKeys();
            var templateAttackDamageKeys = damageHelper.GetAllTemplateDamageKeys();

            Assert.That(creatureAttackDamageKeys, Is.Unique);
            Assert.That(templateAttackDamageKeys, Is.Unique);
            Assert.That(creatureAttackDamageKeys.Concat(templateAttackDamageKeys), Is.Unique);
        }
    }
}
