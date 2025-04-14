using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Tables;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures.CreatureGroups
{
    [TestFixture]
    public class CreatureTemplateGroupsTests : CreatureGroupsTableTests
    {
        [Test]
        public void CreatureGroupNames()
        {
            AssertCreatureGroupNamesAreComplete();
        }

        [Test]
        public void CelestialCreatureTemplateGroup()
        {
            var allCreatures = CreatureConstants.GetAll();
            var allTypes = collectionSelector.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes);
            var allAlignments = collectionSelector.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups);

            var templateCreatures = allCreatures.Where(c => CelestialCreatureHelper.IsCompatible(allTypes[c], allAlignments[c]));
            Assert.That(templateCreatures, Is.Not.Empty);
            AssertCreatureGroup(CreatureConstants.Templates.CelestialCreature, [.. templateCreatures]);
        }

        private static class CelestialCreatureHelper
        {
            public static bool IsCompatible(IEnumerable<string> types, IEnumerable<string> alignments)
            {
                if (types.Contains(CreatureConstants.Types.Subtypes.Incorporeal))
                    return false;

                var creatureTypes = new[]
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
                if (!creatureTypes.Contains(types.First()))
                    return false;

                if (!alignments.Any(a => !a.Contains(AlignmentConstants.Evil)))
                    return false;

                return true;
            }
        }

        [Test]
        public void FiendishCreatureTemplateGroup()
        {
            var allCreatures = CreatureConstants.GetAll();
            var allTypes = collectionSelector.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes);
            var allAlignments = collectionSelector.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups);

            var templateCreatures = allCreatures.Where(c => FiendishCreatureHelper.IsCompatible(allTypes[c], allAlignments[c]));
            Assert.That(templateCreatures, Is.Not.Empty);
            AssertCreatureGroup(CreatureConstants.Templates.FiendishCreature, [.. templateCreatures]);
        }

        private static class FiendishCreatureHelper
        {
            public static bool IsCompatible(IEnumerable<string> types, IEnumerable<string> alignments)
            {
                if (types.Contains(CreatureConstants.Types.Subtypes.Incorporeal))
                    return false;

                var creatureTypes = new[]
                {
                    CreatureConstants.Types.Aberration,
                    CreatureConstants.Types.Animal,
                    CreatureConstants.Types.Dragon,
                    CreatureConstants.Types.Fey,
                    CreatureConstants.Types.Giant,
                    CreatureConstants.Types.Humanoid,
                    CreatureConstants.Types.MagicalBeast,
                    CreatureConstants.Types.MonstrousHumanoid,
                    CreatureConstants.Types.Ooze,
                    CreatureConstants.Types.Plant,
                    CreatureConstants.Types.Vermin,
                };
                if (!creatureTypes.Contains(types.First()))
                    return false;

                if (!alignments.Any(a => !a.Contains(AlignmentConstants.Good)))
                    return false;

                return true;
            }
        }

        //TODO: Other templates
    }
}
