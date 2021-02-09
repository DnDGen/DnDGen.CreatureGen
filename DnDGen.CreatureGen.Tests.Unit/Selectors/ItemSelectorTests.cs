using DnDGen.CreatureGen.Selectors;
using DnDGen.TreasureGen.Items;
using DnDGen.TreasureGen.Items.Magical;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Selectors
{
    [TestFixture]
    public class ItemSelectorTests
    {
        private IItemSelector itemSelector;

        [SetUp]
        public void Setup()
        {
            itemSelector = new ItemSelector();
        }

        [Test]
        public void SelectItem()
        {
            var item = itemSelector.SelectFrom("item[item type]");
            Assert.That(item.Name, Is.EqualTo("item"));
            Assert.That(item.ItemType, Is.EqualTo("item type"));
            Assert.That(item.Traits, Is.Empty);
            Assert.That(item.Magic.SpecialAbilities, Is.Empty);
            Assert.That(item.Magic.Bonus, Is.EqualTo(0));
            Assert.That(item.IsMagical, Is.False);
            Assert.That(item.Quantity, Is.EqualTo(1));
        }

        [Test]
        public void SelectItemWithMagicBonus()
        {
            var item = itemSelector.SelectFrom("item[item type](9266)");
            Assert.That(item.Name, Is.EqualTo("item"));
            Assert.That(item.ItemType, Is.EqualTo("item type"));
            Assert.That(item.Magic.Bonus, Is.EqualTo(9266));
            Assert.That(item.Traits, Is.Empty);
            Assert.That(item.Magic.SpecialAbilities, Is.Empty);
            Assert.That(item.IsMagical, Is.True);
            Assert.That(item.Quantity, Is.EqualTo(1));
        }

        [Test]
        public void SelectItemWithSpecialAbilities()
        {
            var item = itemSelector.SelectFrom("item[item type](9266){special ability,other special ability}");
            Assert.That(item.Name, Is.EqualTo("item"));
            Assert.That(item.ItemType, Is.EqualTo("item type"));
            Assert.That(item.Magic.Bonus, Is.EqualTo(9266));
            Assert.That(item.Traits, Is.Empty);
            Assert.That(item.IsMagical, Is.True);
            Assert.That(item.Magic.SpecialAbilities.Count, Is.EqualTo(2));
            Assert.That(item.Quantity, Is.EqualTo(1));

            var first = item.Magic.SpecialAbilities.First();
            var last = item.Magic.SpecialAbilities.Last();
            Assert.That(first.Name, Is.EqualTo("special ability"));
            Assert.That(first.BonusEquivalent, Is.EqualTo(0));
            Assert.That(last.Name, Is.EqualTo("other special ability"));
            Assert.That(last.BonusEquivalent, Is.EqualTo(0));
        }

        [Test]
        public void SelectItemWithCustomSpecialAbilities()
        {
            var item = itemSelector.SelectFrom("item[item type](9266){special ability$90210$,other special ability$42$}");
            Assert.That(item.Name, Is.EqualTo("item"));
            Assert.That(item.ItemType, Is.EqualTo("item type"));
            Assert.That(item.Magic.Bonus, Is.EqualTo(9266));
            Assert.That(item.Traits, Is.Empty);
            Assert.That(item.IsMagical, Is.True);
            Assert.That(item.Magic.SpecialAbilities.Count, Is.EqualTo(2));
            Assert.That(item.Quantity, Is.EqualTo(1));

            var first = item.Magic.SpecialAbilities.First();
            var last = item.Magic.SpecialAbilities.Last();
            Assert.That(first.Name, Is.EqualTo("special ability"));
            Assert.That(first.BonusEquivalent, Is.EqualTo(90210));
            Assert.That(last.Name, Is.EqualTo("other special ability"));
            Assert.That(last.BonusEquivalent, Is.EqualTo(42));
        }

        [Test]
        public void SelectItemWithTraits()
        {
            var item = itemSelector.SelectFrom("item[item type]#trait,other trait#");
            Assert.That(item.Name, Is.EqualTo("item"));
            Assert.That(item.ItemType, Is.EqualTo("item type"));
            Assert.That(item.Magic.Bonus, Is.EqualTo(0));
            Assert.That(item.Traits, Contains.Item("trait"));
            Assert.That(item.Traits, Contains.Item("other trait"));
            Assert.That(item.Traits.Count, Is.EqualTo(2));
            Assert.That(item.Magic.SpecialAbilities, Is.Empty);
            Assert.That(item.Magic.Bonus, Is.EqualTo(0));
            Assert.That(item.IsMagical, Is.False);
            Assert.That(item.Quantity, Is.EqualTo(1));
        }

        [Test]
        public void SelectItemWithMagic()
        {
            var item = itemSelector.SelectFrom("item[item type]@True@");
            Assert.That(item.Name, Is.EqualTo("item"));
            Assert.That(item.ItemType, Is.EqualTo("item type"));
            Assert.That(item.Magic.Bonus, Is.EqualTo(0));
            Assert.That(item.IsMagical, Is.True);
            Assert.That(item.Traits, Is.Empty);
            Assert.That(item.Magic.SpecialAbilities, Is.Empty);
            Assert.That(item.Magic.Bonus, Is.EqualTo(0));
            Assert.That(item.Quantity, Is.EqualTo(1));
        }

        [Test]
        public void SelectItemWithEverything()
        {
            var item = itemSelector.SelectFrom("item[item type]#trait,other trait#(9266){special ability$90210$,other special ability$42$}@True@");
            Assert.That(item.Name, Is.EqualTo("item"));
            Assert.That(item.ItemType, Is.EqualTo("item type"));
            Assert.That(item.Magic.Bonus, Is.EqualTo(9266));
            Assert.That(item.IsMagical, Is.True);
            Assert.That(item.Traits, Contains.Item("trait"));
            Assert.That(item.Traits, Contains.Item("other trait"));
            Assert.That(item.Traits.Count, Is.EqualTo(2));
            Assert.That(item.Magic.SpecialAbilities.Count, Is.EqualTo(2));

            var first = item.Magic.SpecialAbilities.First();
            var last = item.Magic.SpecialAbilities.Last();
            Assert.That(first.Name, Is.EqualTo("special ability"));
            Assert.That(first.BonusEquivalent, Is.EqualTo(90210));
            Assert.That(last.Name, Is.EqualTo("other special ability"));
            Assert.That(last.BonusEquivalent, Is.EqualTo(42));
            Assert.That(item.Quantity, Is.EqualTo(1));
        }

        [TestCase(WeaponConstants.Arrow)]
        [TestCase(WeaponConstants.CrossbowBolt)]
        [TestCase(WeaponConstants.Shuriken)]
        [TestCase(WeaponConstants.SlingBullet)]
        public void SelectWithQuantityOf0_Ammunition(string ammo)
        {
            var ammos = WeaponConstants.GetAllAmmunition(false, false);
            Assert.That(ammos, Contains.Item(ammo)
                .And.EquivalentTo(new[]
                {
                    WeaponConstants.Arrow,
                    WeaponConstants.CrossbowBolt,
                    WeaponConstants.Shuriken,
                    WeaponConstants.SlingBullet,
                }));

            var item = itemSelector.SelectFrom($"{ammo}[{ItemTypeConstants.Weapon}]");
            Assert.That(item.Name, Is.EqualTo(ammo), ammo);
            Assert.That(item.ItemType, Is.EqualTo(ItemTypeConstants.Weapon), ammo);
            Assert.That(item.Traits, Is.Empty, ammo);
            Assert.That(item.Magic.SpecialAbilities, Is.Empty, ammo);
            Assert.That(item.Magic.Bonus, Is.Zero, ammo);
            Assert.That(item.IsMagical, Is.False, ammo);
            Assert.That(item.Quantity, Is.Zero, ammo);
        }

        [TestCase(WeaponConstants.Javelin)]
        [TestCase(WeaponConstants.Dart)]
        public void SelectWithQuantityOf0_Thrown(string weapon)
        {
            var thrown = WeaponConstants.GetAllThrown(false, false);
            var simple = WeaponConstants.GetAllSimple(false, false);
            var melee = WeaponConstants.GetAllMelee(false, false);

            var thrownRanged = thrown.Except(melee).Intersect(simple);
            Assert.That(thrownRanged, Contains.Item(weapon)
                .And.EquivalentTo(new[]
                {
                    WeaponConstants.Javelin,
                    WeaponConstants.Dart,
                }));

            var item = itemSelector.SelectFrom($"{weapon}[{ItemTypeConstants.Weapon}]");
            Assert.That(item.Name, Is.EqualTo(weapon), weapon);
            Assert.That(item.ItemType, Is.EqualTo(ItemTypeConstants.Weapon), weapon);
            Assert.That(item.Traits, Is.Empty, weapon);
            Assert.That(item.Magic.SpecialAbilities, Is.Empty, weapon);
            Assert.That(item.Magic.Bonus, Is.Zero, weapon);
            Assert.That(item.IsMagical, Is.False, weapon);
            Assert.That(item.Quantity, Is.Zero, weapon);
        }

        [Test]
        public void SelectWithQuantityOf1_AllOtherWeapons()
        {
            var allWeapons = WeaponConstants.GetAllWeapons(false, false);
            var ammos = WeaponConstants.GetAllAmmunition(false, false);
            var thrown = WeaponConstants.GetAllThrown(false, false);
            var simple = WeaponConstants.GetAllSimple(false, false);
            var melee = WeaponConstants.GetAllMelee(false, false);
            var thrownRanged = thrown.Except(melee).Intersect(simple);

            var weapons = allWeapons.Except(ammos).Except(thrownRanged);

            foreach (var weapon in weapons)
            {
                var item = itemSelector.SelectFrom($"{weapon}[{ItemTypeConstants.Weapon}]");
                Assert.That(item.Name, Is.EqualTo(weapon), weapon);
                Assert.That(item.ItemType, Is.EqualTo(ItemTypeConstants.Weapon), weapon);
                Assert.That(item.Traits, Is.Empty, weapon);
                Assert.That(item.Magic.SpecialAbilities, Is.Empty, weapon);
                Assert.That(item.Magic.Bonus, Is.Zero, weapon);
                Assert.That(item.IsMagical, Is.False, weapon);
                Assert.That(item.Quantity, Is.EqualTo(1), weapon);
            }
        }

        [Test]
        public void SelectTemplateFromItem()
        {
            var item = new Item();
            item.Name = "item";
            item.ItemType = "item type";

            var template = itemSelector.SelectFrom(item);
            Assert.That(template, Is.EqualTo("item[item type]"));
        }

        [Test]
        public void SelectTemplateFromItemWithMagicBonus()
        {
            var item = new Item();
            item.Name = "item";
            item.ItemType = "item type";
            item.Magic.Bonus = 9266;

            var template = itemSelector.SelectFrom(item);
            Assert.That(template, Is.EqualTo("item[item type](9266)@True@"));
        }

        [Test]
        public void SelectTemplateFromItemWithSpecialAbilities()
        {
            var item = new Item();
            item.Name = "item";
            item.ItemType = "item type";
            item.Magic.Bonus = 9266;
            item.Magic.SpecialAbilities = new[]
            {
                new SpecialAbility { Name = "special ability" },
                new SpecialAbility { Name = "other special ability" },
            };

            var template = itemSelector.SelectFrom(item);
            Assert.That(template, Is.EqualTo("item[item type](9266){special ability,other special ability}@True@"));
        }

        [Test]
        public void SelectTemplateFromItemWithCustomSpecialAbilities()
        {
            var item = new Item();
            item.Name = "item";
            item.ItemType = "item type";
            item.Magic.Bonus = 9266;
            item.Magic.SpecialAbilities = new[]
            {
                new SpecialAbility { Name = "special ability", BonusEquivalent = 90210 },
                new SpecialAbility { Name = "other special ability", BonusEquivalent = 42 },
            };

            var template = itemSelector.SelectFrom(item);
            Assert.That(template, Is.EqualTo("item[item type](9266){special ability$90210$,other special ability$42$}@True@"));
        }

        [Test]
        public void SelectTemplateFromItemWithTraits()
        {
            var item = new Item();
            item.Name = "item";
            item.ItemType = "item type";
            item.Traits.Add("trait");
            item.Traits.Add("other trait");

            var template = itemSelector.SelectFrom(item);
            Assert.That(template, Is.EqualTo("item[item type]#trait,other trait#"));
        }

        [Test]
        public void SelectTemplateFromItemWithMagic()
        {
            var item = new Item();
            item.Name = "item";
            item.ItemType = "item type";
            item.IsMagical = true;

            var template = itemSelector.SelectFrom(item);
            Assert.That(template, Is.EqualTo("item[item type]@True@"));
        }

        [Test]
        public void SelectTemplateFromItemWithEverything()
        {
            var item = new Item();
            item.Name = "item";
            item.ItemType = "item type";
            item.IsMagical = true;
            item.Traits.Add("trait");
            item.Traits.Add("other trait");
            item.Magic.Bonus = 9266;
            item.Magic.SpecialAbilities = new[]
            {
                new SpecialAbility { Name = "special ability", BonusEquivalent = 90210 },
                new SpecialAbility { Name = "other special ability", BonusEquivalent = 42 },
            };

            var template = itemSelector.SelectFrom(item);
            Assert.That(template, Is.EqualTo("item[item type]#trait,other trait#(9266){special ability$90210$,other special ability$42$}@True@"));
        }

        [Test]
        public void SelectTemplateFromItemWithEverythingExceptSetMagic()
        {
            var item = new Item();
            item.Name = "item";
            item.ItemType = "item type";
            item.Traits.Add("trait");
            item.Traits.Add("other trait");
            item.Magic.Bonus = 9266;
            item.Magic.SpecialAbilities = new[]
            {
                new SpecialAbility { Name = "special ability", BonusEquivalent = 90210 },
                new SpecialAbility { Name = "other special ability", BonusEquivalent = 42 },
            };

            var template = itemSelector.SelectFrom(item);
            Assert.That(template, Is.EqualTo("item[item type]#trait,other trait#(9266){special ability$90210$,other special ability$42$}@True@"));
        }
    }
}
