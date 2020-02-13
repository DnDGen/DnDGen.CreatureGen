using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.EventGen;
using DnDGen.Infrastructure.Selectors.Collections;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures.CreatureGroups
{
    [TestFixture]
    public class CreatureTemplateGroupsTests : CreatureGroupsTableTests
    {
        [Inject]
        public ICollectionSelector CollectionSelector { get; set; }
        [Inject]
        public ClientIDManager ClientIdManager { get; set; }

        [SetUp]
        public void Setup()
        {
            var clientId = Guid.NewGuid();
            ClientIdManager.SetClientID(clientId);
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

            var alignmentGroups = new[]
            {
                AlignmentConstants.Modifiers.Always + AlignmentConstants.Good,
                AlignmentConstants.Modifiers.Always + AlignmentConstants.Neutral,
                AlignmentConstants.Modifiers.Any + AlignmentConstants.Good,
                AlignmentConstants.Modifiers.Any + AlignmentConstants.Neutral,
            };

            var typeCreatures = GetCreaturesOfTypes(types);

            var validCreatures = typeCreatures.Where(c => AlignmentMatches(c, alignmentGroups));
            validCreatures = validCreatures.Where(c => !c.Contains("Celestial"));

            var incorporealCreatures = CollectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Types.Subtypes.Incorporeal);
            var augmentedCreatures = CollectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Types.Subtypes.Augmented);
            validCreatures = validCreatures.Except(incorporealCreatures).Except(augmentedCreatures);

            var celestialCreatures = CollectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Templates.CelestialCreature);

            Assert.That(celestialCreatures, Is.EquivalentTo(validCreatures));
        }

        private bool AlignmentMatches(string creature, params string[] alignmentGroups)
        {
            var alignments = GetAlignments(alignmentGroups);
            var creatureAlignmentGroups = CollectionSelector.SelectFrom(TableNameConstants.Collection.AlignmentGroups, creature);
            if (!creatureAlignmentGroups.Any())
                creatureAlignmentGroups = new[] { AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral };

            return creatureAlignmentGroups.Any(g => alignments.Any(a => g.Contains(a)))
                || creatureAlignmentGroups.Intersect(alignmentGroups).Any();
        }

        private IEnumerable<string> GetAlignments(params string[] alignmentGroups)
        {
            var alignments = new List<string>();

            foreach (var alignmentGroup in alignmentGroups)
            {
                var explodedAlignments = CollectionSelector.Explode(TableNameConstants.Collection.AlignmentGroups, alignmentGroup);
                alignments.AddRange(explodedAlignments);
            }

            return alignments;
        }

        private IEnumerable<string> GetCreaturesOfTypes(params string[] types)
        {
            var creatureTypes = CollectionMapper.Map(TableNameConstants.Collection.CreatureTypes);
            var creaturesOfTypes = creatureTypes.Where(kvp => types.Contains(kvp.Value.First())).Select(kvp => kvp.Key);

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

            var alignmentGroups = new[]
            {
                AlignmentConstants.Modifiers.Always + AlignmentConstants.Good,
                AlignmentConstants.Modifiers.Always + AlignmentConstants.Neutral,
                AlignmentConstants.Modifiers.Any + AlignmentConstants.Good,
                AlignmentConstants.Modifiers.Any + AlignmentConstants.Neutral,
            };

            var typeCreatures = GetCreaturesOfTypes(types);

            var validCreatures = typeCreatures.Where(c => AlignmentMatches(c, alignmentGroups));
            validCreatures = validCreatures.Where(c => !c.Contains("Celestial"));

            var incorporealCreatures = CollectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Types.Subtypes.Incorporeal);
            var augmentedCreatures = CollectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Types.Subtypes.Augmented);
            validCreatures = validCreatures.Except(incorporealCreatures).Except(augmentedCreatures);

            var celestialCreatures = CollectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Templates.HalfCelestial);

            Assert.That(celestialCreatures, Is.EquivalentTo(validCreatures));
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

            var incorporealCreatures = CollectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Types.Subtypes.Incorporeal);
            var augmentedCreatures = CollectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Types.Subtypes.Augmented);
            var validCreatures = typeCreatures.Except(incorporealCreatures).Except(augmentedCreatures);

            var dragonCreatures = CollectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Templates.HalfDragon);

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

            var alignmentGroups = new[]
            {
                AlignmentConstants.Modifiers.Always + AlignmentConstants.Evil,
                AlignmentConstants.Modifiers.Always + AlignmentConstants.Neutral,
                AlignmentConstants.Modifiers.Any + AlignmentConstants.Evil,
                AlignmentConstants.Modifiers.Any + AlignmentConstants.Neutral,
            };

            var typeCreatures = GetCreaturesOfTypes(types);

            var validCreatures = typeCreatures.Where(c => AlignmentMatches(c, alignmentGroups));
            validCreatures = validCreatures.Where(c => !c.Contains("Fiendish"));

            var incorporealCreatures = CollectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Types.Subtypes.Incorporeal);
            var augmentedCreatures = CollectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Types.Subtypes.Augmented);
            validCreatures = validCreatures.Except(incorporealCreatures).Except(augmentedCreatures);

            var fiendishCreatures = CollectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Templates.FiendishCreature);

            Assert.That(fiendishCreatures, Is.EquivalentTo(validCreatures));
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

            var alignmentGroups = new[]
            {
                AlignmentConstants.Modifiers.Always + AlignmentConstants.Evil,
                AlignmentConstants.Modifiers.Always + AlignmentConstants.Neutral,
                AlignmentConstants.Modifiers.Any + AlignmentConstants.Evil,
                AlignmentConstants.Modifiers.Any + AlignmentConstants.Neutral,
            };

            var typeCreatures = GetCreaturesOfTypes(types);

            var validCreatures = typeCreatures.Where(c => AlignmentMatches(c, alignmentGroups));
            validCreatures = validCreatures.Where(c => !c.Contains("Fiendish"));

            var incorporealCreatures = CollectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Types.Subtypes.Incorporeal);
            var augmentedCreatures = CollectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Types.Subtypes.Augmented);
            validCreatures = validCreatures.Except(incorporealCreatures).Except(augmentedCreatures);

            var fiendishCreatures = CollectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Templates.HalfFiend);

            Assert.That(fiendishCreatures, Is.EquivalentTo(validCreatures));
        }
    }
}
