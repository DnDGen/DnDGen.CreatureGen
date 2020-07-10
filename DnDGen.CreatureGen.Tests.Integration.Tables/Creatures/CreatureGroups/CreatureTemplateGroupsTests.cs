using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Tests.Integration.TestData;
using DnDGen.Infrastructure.Selectors.Collections;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures.CreatureGroups
{
    [TestFixture]
    public class CreatureTemplateGroupsTests : CreatureGroupsTableTests
    {
        private ICollectionSelector collectionSelector;

        [SetUp]
        public void Setup()
        {
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
        }

        [Test]
        public void CreatureGroupNames()
        {
            AssertCreatureGroupNamesAreComplete();
        }

        [TestCase(CreatureConstants.Templates.Ghost,
            CreatureConstants.Types.Aberration,
            CreatureConstants.Types.Animal,
            CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Giant,
            CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.MonstrousHumanoid,
            CreatureConstants.Types.Plant)]
        [TestCase(CreatureConstants.Templates.HalfDragon,
            CreatureConstants.Types.Aberration,
            CreatureConstants.Types.Animal,
            CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Fey,
            CreatureConstants.Types.Giant,
            CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.MonstrousHumanoid,
            CreatureConstants.Types.Plant,
            CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Templates.Lich,
            CreatureConstants.Types.Humanoid)]
        [TestCase(CreatureConstants.Templates.None)]
        [TestCase(CreatureConstants.Templates.Skeleton,
            CreatureConstants.Groups.HasSkeleton)]
        [TestCase(CreatureConstants.Templates.Vampire,
            CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.Templates.Werebear,
            CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Templates.Wereboar,
            CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Templates.Wererat,
            CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Templates.Weretiger,
            CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Templates.Werewolf,
            CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Templates.Zombie,
            CreatureConstants.Groups.HasSkeleton)]
        public void CreatureTemplateGroup(string template, params string[] group)
        {
            AssertDistinctCollection(template, group);
        }

        [Test]
        public void CelestialCreatureGroup()
        {
            var types = new[]
            {
                CreatureConstants.Types.Aberration,
                CreatureConstants.Types.Animal,
                CreatureConstants.Types.Dragon,
                CreatureConstants.Types.Fey,
                CreatureConstants.Types.Giant,
                CreatureConstants.Types.Humanoid,
                CreatureConstants.Types.MagicalBeast,
                CreatureConstants.Types.MonstrousHumanoid,
                CreatureConstants.Types.Plant,
                CreatureConstants.Types.Vermin,
            };

            var goodnesses = new[]
            {
                AlignmentConstants.Good,
                AlignmentConstants.Neutral,
            };

            var validCreatures = GetValidAlignmentBasedTemplateGroup(types, goodnesses);
            var celestialCreatures = collectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Templates.CelestialCreature);

            Assert.That(celestialCreatures, Is.EquivalentTo(validCreatures));

            //These races have proven problematic in the past
            Assert.That(celestialCreatures, Contains.Item(CreatureConstants.Human));
            Assert.That(celestialCreatures, Contains.Item(CreatureConstants.Elf_Half));
            Assert.That(celestialCreatures, Contains.Item(CreatureConstants.Orc_Half));
        }

        private IEnumerable<string> GetValidAlignmentBasedTemplateGroup(IEnumerable<string> types, IEnumerable<string> goodnesses)
        {
            var typeCreatures = GetCreaturesOfTypes(types);

            var validCreatures = typeCreatures.Where(c => AlignmentMatches(c, goodnesses));

            var incorporealCreatures = collectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Types.Subtypes.Incorporeal);
            var augmentedCreatures = collectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Types.Subtypes.Augmented);
            var extraplanarCreatures = collectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Types.Subtypes.Extraplanar);
            validCreatures = validCreatures
                .Except(incorporealCreatures)
                .Except(augmentedCreatures)
                .Except(extraplanarCreatures);

            return validCreatures;
        }

        private bool AlignmentMatches(string creature, IEnumerable<string> goodnesses)
        {
            var creatureAlignments = collectionSelector.SelectFrom(TableNameConstants.Collection.AlignmentGroups, creature);

            return creatureAlignments.Any(a =>
                a == AlignmentConstants.Modifiers.Any
                || goodnesses.Any(g =>
                    a.EndsWith(g)));
        }

        private IEnumerable<string> GetCreaturesOfTypes(IEnumerable<string> types)
        {
            var creatureTypes = collectionMapper.Map(TableNameConstants.Collection.CreatureTypes);
            var creaturesOfTypes = creatureTypes
                .Where(kvp => types.Contains(kvp.Value.First()))
                .Select(kvp => kvp.Key);

            return creaturesOfTypes;
        }

        [Test]
        public void HalfCelestialCreatureGroup()
        {
            var types = new[]
            {
                CreatureConstants.Types.Aberration,
                CreatureConstants.Types.Animal,
                CreatureConstants.Types.Dragon,
                CreatureConstants.Types.Fey,
                CreatureConstants.Types.Giant,
                CreatureConstants.Types.Humanoid,
                CreatureConstants.Types.MagicalBeast,
                CreatureConstants.Types.MonstrousHumanoid,
                CreatureConstants.Types.Plant,
                CreatureConstants.Types.Vermin,
            };

            var goodnesses = new[]
            {
                AlignmentConstants.Good,
                AlignmentConstants.Neutral,
            };

            var validCreatures = GetValidAlignmentBasedTemplateGroup(types, goodnesses);
            var celestialCreatures = collectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Templates.HalfCelestial);

            Assert.That(celestialCreatures, Is.EquivalentTo(validCreatures));

            //These races have proven problematic in the past
            Assert.That(celestialCreatures, Contains.Item(CreatureConstants.Human));
            Assert.That(celestialCreatures, Contains.Item(CreatureConstants.Elf_Half));
            Assert.That(celestialCreatures, Contains.Item(CreatureConstants.Orc_Half));
        }

        [Test]
        public void HalfDragonCreatureGroup()
        {
            var types = new[]
            {
                CreatureConstants.Types.Aberration,
                CreatureConstants.Types.Animal,
                CreatureConstants.Types.Dragon,
                CreatureConstants.Types.Fey,
                CreatureConstants.Types.Giant,
                CreatureConstants.Types.Humanoid,
                CreatureConstants.Types.MagicalBeast,
                CreatureConstants.Types.MonstrousHumanoid,
                CreatureConstants.Types.Plant,
                CreatureConstants.Types.Vermin,
            };

            var typeCreatures = GetCreaturesOfTypes(types);

            var incorporealCreatures = collectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Types.Subtypes.Incorporeal);
            var augmentedCreatures = collectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Types.Subtypes.Augmented);
            var validCreatures = typeCreatures.Except(incorporealCreatures).Except(augmentedCreatures);

            var dragonCreatures = collectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Templates.HalfDragon);

            Assert.That(dragonCreatures, Is.EquivalentTo(validCreatures));
        }

        [Test]
        public void FiendishCreatureGroup()
        {
            var types = new[]
            {
                CreatureConstants.Types.Aberration,
                CreatureConstants.Types.Animal,
                CreatureConstants.Types.Dragon,
                CreatureConstants.Types.Fey,
                CreatureConstants.Types.Giant,
                CreatureConstants.Types.Humanoid,
                CreatureConstants.Types.MagicalBeast,
                CreatureConstants.Types.MonstrousHumanoid,
                CreatureConstants.Types.Plant,
                CreatureConstants.Types.Vermin,
            };

            var goodnesses = new[]
            {
                AlignmentConstants.Evil,
                AlignmentConstants.Neutral,
            };

            var validCreatures = GetValidAlignmentBasedTemplateGroup(types, goodnesses);
            var fiendishCreatures = collectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Templates.FiendishCreature);

            Assert.That(fiendishCreatures, Is.EquivalentTo(validCreatures));

            //These races have proven problematic in the past
            Assert.That(fiendishCreatures, Contains.Item(CreatureConstants.Human));
            Assert.That(fiendishCreatures, Contains.Item(CreatureConstants.Elf_Half));
            Assert.That(fiendishCreatures, Contains.Item(CreatureConstants.Orc_Half));
        }

        [Test]
        public void HalfFiendCreatureGroup()
        {
            var types = new[]
            {
                CreatureConstants.Types.Aberration,
                CreatureConstants.Types.Animal,
                CreatureConstants.Types.Dragon,
                CreatureConstants.Types.Fey,
                CreatureConstants.Types.Giant,
                CreatureConstants.Types.Humanoid,
                CreatureConstants.Types.MagicalBeast,
                CreatureConstants.Types.MonstrousHumanoid,
                CreatureConstants.Types.Plant,
                CreatureConstants.Types.Vermin,
            };

            var goodnesses = new[]
            {
                AlignmentConstants.Evil,
                AlignmentConstants.Neutral,
            };

            var validCreatures = GetValidAlignmentBasedTemplateGroup(types, goodnesses);
            var fiendishCreatures = collectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Templates.HalfFiend);

            Assert.That(fiendishCreatures, Is.EquivalentTo(validCreatures));

            //These races have proven problematic in the past
            Assert.That(fiendishCreatures, Contains.Item(CreatureConstants.Human));
            Assert.That(fiendishCreatures, Contains.Item(CreatureConstants.Elf_Half));
            Assert.That(fiendishCreatures, Contains.Item(CreatureConstants.Orc_Half));
        }

        [TestCaseSource(typeof(CreatureTestData), "Templates")]
        public void TemplateGroupIsNotInfinitelyRecursive(string template)
        {
            var templateCreatures = collectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, template);

            Assert.That(templateCreatures, Is.Empty);
        }
    }
}
