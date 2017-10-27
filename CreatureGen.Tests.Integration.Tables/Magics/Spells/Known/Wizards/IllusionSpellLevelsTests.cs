using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Wizards
{
    [TestFixture]
    public class IllusionSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Schools.Illusion);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.GhostSound,
                SpellConstants.ColorSpray,
                SpellConstants.DisguiseSelf,
                SpellConstants.MagicAura,
                SpellConstants.SilentImage,
                SpellConstants.Ventriloquism,
                SpellConstants.Blur,
                SpellConstants.HypnoticPattern,
                SpellConstants.Invisibility,
                SpellConstants.MagicMouth,
                SpellConstants.MinorImage,
                SpellConstants.MirrorImage,
                SpellConstants.Misdirection,
                SpellConstants.PhantomTrap,
                SpellConstants.Displacement,
                SpellConstants.IllusoryScript,
                SpellConstants.InvisibilitySphere,
                SpellConstants.MajorImage,
                SpellConstants.HallucinatoryTerrain,
                SpellConstants.IllusoryWall,
                SpellConstants.Invisibility_Greater,
                SpellConstants.PhantasmalKiller,
                SpellConstants.RainbowPattern,
                SpellConstants.ShadowConjuration,
                SpellConstants.Dream,
                SpellConstants.FalseVision,
                SpellConstants.MirageArcana,
                SpellConstants.Nightmare,
                SpellConstants.PersistentImage,
                SpellConstants.Seeming,
                SpellConstants.ShadowEvocation,
                SpellConstants.Mislead,
                SpellConstants.PermanentImage,
                SpellConstants.ProgrammedImage,
                SpellConstants.ShadowWalk,
                SpellConstants.Veil,
                SpellConstants.Invisibility_Mass,
                SpellConstants.ProjectImage,
                SpellConstants.ShadowConjuration_Greater,
                SpellConstants.Simulacrum,
                SpellConstants.ScintillatingPattern,
                SpellConstants.Screen,
                SpellConstants.ShadowEvocation_Greater,
                SpellConstants.Shades,
                SpellConstants.Weird
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllIllusionSpellsInAdjustmentsTable()
        {
            var spellGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Schools.Illusion]);
        }

        [TestCase(SpellConstants.GhostSound, 0)]
        [TestCase(SpellConstants.ColorSpray, 1)]
        [TestCase(SpellConstants.DisguiseSelf, 1)]
        [TestCase(SpellConstants.MagicAura, 1)]
        [TestCase(SpellConstants.SilentImage, 1)]
        [TestCase(SpellConstants.Ventriloquism, 1)]
        [TestCase(SpellConstants.Blur, 2)]
        [TestCase(SpellConstants.HypnoticPattern, 2)]
        [TestCase(SpellConstants.Invisibility, 2)]
        [TestCase(SpellConstants.MagicMouth, 2)]
        [TestCase(SpellConstants.MinorImage, 2)]
        [TestCase(SpellConstants.MirrorImage, 2)]
        [TestCase(SpellConstants.Misdirection, 2)]
        [TestCase(SpellConstants.PhantomTrap, 2)]
        [TestCase(SpellConstants.Displacement, 3)]
        [TestCase(SpellConstants.IllusoryScript, 3)]
        [TestCase(SpellConstants.InvisibilitySphere, 3)]
        [TestCase(SpellConstants.MajorImage, 3)]
        [TestCase(SpellConstants.HallucinatoryTerrain, 4)]
        [TestCase(SpellConstants.IllusoryWall, 4)]
        [TestCase(SpellConstants.Invisibility_Greater, 4)]
        [TestCase(SpellConstants.PhantasmalKiller, 4)]
        [TestCase(SpellConstants.RainbowPattern, 4)]
        [TestCase(SpellConstants.ShadowConjuration, 4)]
        [TestCase(SpellConstants.Dream, 5)]
        [TestCase(SpellConstants.FalseVision, 5)]
        [TestCase(SpellConstants.MirageArcana, 5)]
        [TestCase(SpellConstants.Nightmare, 5)]
        [TestCase(SpellConstants.PersistentImage, 5)]
        [TestCase(SpellConstants.Seeming, 5)]
        [TestCase(SpellConstants.ShadowEvocation, 5)]
        [TestCase(SpellConstants.Mislead, 6)]
        [TestCase(SpellConstants.PermanentImage, 6)]
        [TestCase(SpellConstants.ProgrammedImage, 6)]
        [TestCase(SpellConstants.ShadowWalk, 6)]
        [TestCase(SpellConstants.Veil, 6)]
        [TestCase(SpellConstants.Invisibility_Mass, 7)]
        [TestCase(SpellConstants.ProjectImage, 7)]
        [TestCase(SpellConstants.ShadowConjuration_Greater, 7)]
        [TestCase(SpellConstants.Simulacrum, 7)]
        [TestCase(SpellConstants.ScintillatingPattern, 8)]
        [TestCase(SpellConstants.Screen, 8)]
        [TestCase(SpellConstants.ShadowEvocation_Greater, 8)]
        [TestCase(SpellConstants.Shades, 9)]
        [TestCase(SpellConstants.Weird, 9)]
        public void SpellLevel(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
