using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.TreasureGen.Items;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Tests.Integration.Stress.Creatures
{
    [TestFixture]
    public class CreatureGeneratorTests : StressTests
    {
        private CreatureAsserter creatureAsserter;
        private ICollectionSelector collectionSelector;
        private ICreatureGenerator creatureGenerator;

        [SetUp]
        public void Setup()
        {
            creatureAsserter = new CreatureAsserter();
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
            creatureGenerator = GetNewInstanceOf<ICreatureGenerator>();
        }

        [Test]
        public void StressCreature()
        {
            stressor.Stress(GenerateAndAssertCreature);
        }

        private void GenerateAndAssertCreature()
        {
            var randomCreatureName = collectionSelector.SelectRandomFrom(allCreatures);

            var creature = creatureGenerator.Generate(randomCreatureName, CreatureConstants.Templates.None);

            creatureAsserter.AssertCreature(creature);
        }

        [Test]
        public async Task StressCreatureAsync()
        {
            await stressor.StressAsync(GenerateAndAssertCreatureAsync);
        }

        private async Task GenerateAndAssertCreatureAsync()
        {
            var randomCreatureName = collectionSelector.SelectRandomFrom(allCreatures);

            var creature = await creatureGenerator.GenerateAsync(randomCreatureName, CreatureConstants.Templates.None);

            creatureAsserter.AssertCreature(creature);
        }

        [Test]
        public void StressCreatureWithTemplate()
        {
            stressor.Stress(GenerateAndAssertCreatureWithTemplate);
        }

        private void GenerateAndAssertCreatureWithTemplate()
        {
            var randomTemplate = collectionSelector.SelectRandomFrom(allTemplates);
            var validCreatures = allCreatures.Where(c => creatureVerifier.VerifyCompatibility(c, randomTemplate));
            var randomCreatureName = collectionSelector.SelectRandomFrom(validCreatures);

            var creature = creatureGenerator.Generate(randomCreatureName, randomTemplate);

            Assert.That(creature.Template, Is.EqualTo(randomTemplate));
            creatureAsserter.AssertCreature(creature);
        }

        [Test]
        public async Task StressCreatureWithTemplateAsync()
        {
            await stressor.StressAsync(GenerateAndAssertCreatureWithTemplateAsync);
        }

        private async Task GenerateAndAssertCreatureWithTemplateAsync()
        {
            var randomTemplate = collectionSelector.SelectRandomFrom(allTemplates);
            var validCreatures = allCreatures.Where(c => creatureVerifier.VerifyCompatibility(c, randomTemplate));
            var randomCreatureName = collectionSelector.SelectRandomFrom(validCreatures);

            var creature = await creatureGenerator.GenerateAsync(randomCreatureName, randomTemplate);

            Assert.That(creature.Template, Is.EqualTo(randomTemplate));
            creatureAsserter.AssertCreature(creature);
        }

        [TestCase(CreatureConstants.Titan)]
        public void BUG_OversizedWeaponHasCorrectAttackDamage(string creatureName)
        {
            stressor.Stress(() => GenerateAndAssertCreatureWithOversizedWeapon(creatureName));
        }

        private void GenerateAndAssertCreatureWithOversizedWeapon(string creatureName)
        {
            var creature = creatureGenerator.Generate(creatureName, CreatureConstants.Templates.None);
            Assert.That(creature.Equipment, Is.Not.Null);
            Assert.That(creature.Equipment.Weapons, Is.Not.Empty.And.All.Not.Null);

            var oversizedFeat = creature.SpecialQualities.FirstOrDefault(sq => sq.Name == FeatConstants.SpecialQualities.OversizedWeapon);
            Assert.That(oversizedFeat, Is.Not.Null);
            Assert.That(oversizedFeat.Foci, Is.Not.Empty);
            Assert.That(oversizedFeat.Foci.Count(), Is.EqualTo(1));

            var oversizedSize = oversizedFeat.Foci.First();

            var weaponNames = WeaponConstants.GetAllWeapons(true, false);
            var unnaturalAttacks = creature.Attacks.Where(a => !a.IsNatural && weaponNames.Contains(a.Name));

            foreach (var attack in unnaturalAttacks)
            {
                var weapon = creature.Equipment.Weapons.FirstOrDefault(w => w.Name == attack.Name);
                Assert.That(weapon, Is.Not.Null, $"{creature.Summary}: {attack.Name}");
                Assert.That(weapon.Damage, Is.Not.Empty, $"{creature.Summary}: {weapon.Name}");
                Assert.That(weaponNames, Contains.Item(weapon.Name), $"{creature.Summary}: {weapon.Name}");

                Assert.That(attack.DamageRoll, Is.EqualTo(weapon.Damage), $"{creature.Summary} ({creature.Size}): {weapon.Name} ({weapon.Size}) [Oversized: {oversizedSize}]");

                if (weapon.Attributes.Contains(AttributeConstants.Melee))
                {
                    Assert.That(attack.AttackType, Contains.Substring("melee"), $"{creature.Summary} ({creature.Size}): {weapon.Name} ({weapon.Size}) [Oversized: {oversizedSize}]");
                }
                else if (weapon.Attributes.Contains(AttributeConstants.Ranged))
                {
                    Assert.That(attack.AttackType, Contains.Substring("ranged"), $"{creature.Summary} ({creature.Size}): {weapon.Name} ({weapon.Size}) [Oversized: {oversizedSize}]");
                }
            }

            creatureAsserter.AssertCreature(creature);
        }

        [TestCase(CreatureConstants.Titan)]
        public void BUG_OversizedWeaponHasCorrectAttackDamageAsync(string creatureName)
        {
            stressor.StressAsync(async () => await GenerateAndAssertCreatureWithOversizedWeaponAsync(creatureName));
        }

        private async Task GenerateAndAssertCreatureWithOversizedWeaponAsync(string creatureName)
        {
            var creature = await creatureGenerator.GenerateAsync(creatureName, CreatureConstants.Templates.None);
            Assert.That(creature.Equipment, Is.Not.Null);
            Assert.That(creature.Equipment.Weapons, Is.Not.Empty.And.All.Not.Null);

            var oversizedFeat = creature.SpecialQualities.FirstOrDefault(sq => sq.Name == FeatConstants.SpecialQualities.OversizedWeapon);
            Assert.That(oversizedFeat, Is.Not.Null);
            Assert.That(oversizedFeat.Foci, Is.Not.Empty);
            Assert.That(oversizedFeat.Foci.Count(), Is.EqualTo(1));

            var oversizedSize = oversizedFeat.Foci.First();

            var weaponNames = WeaponConstants.GetAllWeapons(true, false);
            var unnaturalAttacks = creature.Attacks.Where(a => !a.IsNatural && weaponNames.Contains(a.Name));

            foreach (var attack in unnaturalAttacks)
            {
                var weapon = creature.Equipment.Weapons.FirstOrDefault(w => w.Name == attack.Name);
                Assert.That(weapon, Is.Not.Null, $"{creature.Summary}: {attack.Name}");
                Assert.That(weapon.Damage, Is.Not.Empty, $"{creature.Summary}: {weapon.Name}");
                Assert.That(weaponNames, Contains.Item(weapon.Name), $"{creature.Summary}: {weapon.Name}");

                Assert.That(attack.DamageRoll, Is.EqualTo(weapon.Damage), $"{creature.Summary} ({creature.Size}): {weapon.Name} ({weapon.Size}) [Oversized: {oversizedSize}]");

                if (weapon.Attributes.Contains(AttributeConstants.Melee))
                {
                    Assert.That(attack.AttackType, Contains.Substring("melee"), $"{creature.Summary} ({creature.Size}): {weapon.Name} ({weapon.Size}) [Oversized: {oversizedSize}]");
                }
                else if (weapon.Attributes.Contains(AttributeConstants.Ranged))
                {
                    Assert.That(attack.AttackType, Contains.Substring("ranged"), $"{creature.Summary} ({creature.Size}): {weapon.Name} ({weapon.Size}) [Oversized: {oversizedSize}]");
                }
            }

            creatureAsserter.AssertCreature(creature);
        }
    }
}