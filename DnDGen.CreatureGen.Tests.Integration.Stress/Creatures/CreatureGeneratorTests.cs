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
            var randomCreatureName = collectionSelector.SelectRandomFrom(allCreatures);
            var randomTemplate = collectionSelector.SelectRandomFrom(allTemplates);

            var attempts = 100;
            while (!creatureVerifier.VerifyCompatibility(randomCreatureName, randomTemplate) && attempts-- > 0)
                randomCreatureName = collectionSelector.SelectRandomFrom(allCreatures);

            if (attempts == 0 && !creatureVerifier.VerifyCompatibility(randomCreatureName, randomTemplate))
                Assert.Fail($"After 100 attempts, could not find a creature that fits template {randomTemplate}");

            var creature = creatureGenerator.Generate(randomCreatureName, randomTemplate);

            creatureAsserter.AssertCreature(creature);
        }

        [Test]
        public async Task StressCreatureWithTemplateAsync()
        {
            await stressor.StressAsync(GenerateAndAssertCreatureWithTemplateAsync);
        }

        private async Task GenerateAndAssertCreatureWithTemplateAsync()
        {
            var randomCreatureName = collectionSelector.SelectRandomFrom(allCreatures);
            var randomTemplate = collectionSelector.SelectRandomFrom(allTemplates);

            var attempts = 100;
            while (!creatureVerifier.VerifyCompatibility(randomCreatureName, randomTemplate) && attempts-- > 0)
                randomCreatureName = collectionSelector.SelectRandomFrom(allCreatures);

            if (attempts == 0 && !creatureVerifier.VerifyCompatibility(randomCreatureName, randomTemplate))
                Assert.Fail($"After 100 attempts, could not find a creature that fits template {randomTemplate}");

            var creature = await creatureGenerator.GenerateAsync(randomCreatureName, randomTemplate);

            creatureAsserter.AssertCreature(creature);
        }

        [TestCase(CreatureConstants.Titan)]
        public void BUG_OversizedWeaponHasCorrectAttackDamage(string creatureName)
        {
            stressor.Stress(() => GenerateAndAssertCreatureWIthOversizedWeapon(creatureName));
        }

        private void GenerateAndAssertCreatureWIthOversizedWeapon(string creatureName)
        {
            var creature = creatureGenerator.Generate(creatureName, CreatureConstants.Templates.None);
            creatureAsserter.AssertCreature(creature);
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
        }

        [TestCase(CreatureConstants.Titan)]
        public void BUG_OversizedWeaponHasCorrectAttackDamageAsync(string creatureName)
        {
            stressor.StressAsync(async () => await GenerateAndAssertCreatureWIthOversizedWeaponAsync(creatureName));
        }

        private async Task GenerateAndAssertCreatureWIthOversizedWeaponAsync(string creatureName)
        {
            var creature = await creatureGenerator.GenerateAsync(creatureName, CreatureConstants.Templates.None);
            creatureAsserter.AssertCreature(creature);
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
        }
    }
}