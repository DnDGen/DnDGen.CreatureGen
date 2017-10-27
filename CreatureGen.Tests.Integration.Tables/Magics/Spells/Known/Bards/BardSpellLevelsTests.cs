using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Bards
{
    [TestFixture]
    public class BardSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Bard);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.DancingLights,
                SpellConstants.Daze,
                SpellConstants.DetectMagic,
                SpellConstants.Flare,
                SpellConstants.GhostSound,
                SpellConstants.KnowDirection,
                SpellConstants.Light,
                SpellConstants.Lullaby,
                SpellConstants.MageHand,
                SpellConstants.Mending,
                SpellConstants.Message,
                SpellConstants.OpenClose,
                SpellConstants.Prestidigitation,
                SpellConstants.ReadMagic,
                SpellConstants.Resistance,
                SpellConstants.SummonInstrument,
                SpellConstants.Alarm,
                SpellConstants.AnimateRope,
                SpellConstants.CauseFear,
                SpellConstants.CharmPerson,
                SpellConstants.ComprehendLanguages,
                SpellConstants.Confusion_Lesser,
                SpellConstants.CureInflictLightWounds,
                SpellConstants.DetectSecretDoors,
                SpellConstants.DisguiseSelf,
                SpellConstants.Erase,
                SpellConstants.ExpeditiousRetreat,
                SpellConstants.FeatherFall,
                SpellConstants.Grease,
                SpellConstants.HideousLaughter,
                SpellConstants.Hypnotism,
                SpellConstants.Identify,
                SpellConstants.MagicMouth,
                SpellConstants.MagicAura,
                SpellConstants.ObscureObject,
                SpellConstants.RemoveFear,
                SpellConstants.SilentImage,
                SpellConstants.Sleep,
                SpellConstants.SummonMonsterI,
                SpellConstants.UndetectableAlignment,
                SpellConstants.UnseenServant,
                SpellConstants.Ventriloquism,
                SpellConstants.AlterSelf,
                SpellConstants.AnimalMessenger,
                SpellConstants.AnimalTrance,
                SpellConstants.BlindnessDeafness,
                SpellConstants.Blur,
                SpellConstants.CalmEmotions,
                SpellConstants.CatsGrace,
                SpellConstants.CureInflictModerateWounds,
                SpellConstants.Darkness,
                SpellConstants.DazeMonster,
                SpellConstants.DelayPoison,
                SpellConstants.DetectThoughts,
                SpellConstants.EaglesSplendor,
                SpellConstants.Enthrall,
                SpellConstants.FoxsCunning,
                SpellConstants.Glitterdust,
                SpellConstants.Heroism,
                SpellConstants.HoldPerson,
                SpellConstants.HypnoticPattern,
                SpellConstants.Invisibility,
                SpellConstants.LocateObject,
                SpellConstants.MinorImage,
                SpellConstants.MirrorImage,
                SpellConstants.Misdirection,
                SpellConstants.Pyrotechnics,
                SpellConstants.Rage,
                SpellConstants.Scare,
                SpellConstants.Shatter,
                SpellConstants.Silence,
                SpellConstants.SoundBurst,
                SpellConstants.Suggestion,
                SpellConstants.SummonMonsterII,
                SpellConstants.SummonSwarm,
                SpellConstants.Tongues,
                SpellConstants.WhisperingWind,
                SpellConstants.Blink,
                SpellConstants.CharmMonster,
                SpellConstants.ClairaudienceClairvoyance,
                SpellConstants.Confusion,
                SpellConstants.CrushingDespair,
                SpellConstants.CureInflictSeriousWounds,
                SpellConstants.Daylight,
                SpellConstants.DeepSlumber,
                SpellConstants.DispelMagic,
                SpellConstants.Displacement,
                SpellConstants.Fear,
                SpellConstants.GaseousForm,
                SpellConstants.Geas_Lesser,
                SpellConstants.Glibness,
                SpellConstants.GoodHope,
                SpellConstants.Haste,
                SpellConstants.IllusoryScript,
                SpellConstants.InvisibilitySphere,
                SpellConstants.MajorImage,
                SpellConstants.PhantomSteed,
                SpellConstants.RemoveCurse,
                SpellConstants.Scrying,
                SpellConstants.SculptSound,
                SpellConstants.SecretPage,
                SpellConstants.SeeInvisibility,
                SpellConstants.SepiaSnakeSigil,
                SpellConstants.Slow,
                SpellConstants.SpeakWithAnimals,
                SpellConstants.SummonMonsterIII,
                SpellConstants.TinyHut,
                SpellConstants.BreakEnchantment,
                SpellConstants.CureInflictCriticalWounds,
                SpellConstants.DetectScrying,
                SpellConstants.DimensionDoor,
                SpellConstants.DominatePerson,
                SpellConstants.FreedomOfMovement,
                SpellConstants.HallucinatoryTerrain,
                SpellConstants.HoldMonster,
                SpellConstants.Invisibility_Greater,
                SpellConstants.LegendLore,
                SpellConstants.LocateCreature,
                SpellConstants.ModifyMemory,
                SpellConstants.NeutralizePoison,
                SpellConstants.RainbowPattern,
                SpellConstants.RepelVermin,
                SpellConstants.SecureShelter,
                SpellConstants.ShadowConjuration,
                SpellConstants.Shout,
                SpellConstants.SpeakWithPlants,
                SpellConstants.SummonMonsterIV,
                SpellConstants.ZoneOfSilence,
                SpellConstants.CureInflictLightWounds_Mass,
                SpellConstants.DispelMagic_Greater,
                SpellConstants.Dream,
                SpellConstants.FalseVision,
                SpellConstants.Heroism_Greater,
                SpellConstants.MindFog,
                SpellConstants.MirageArcana,
                SpellConstants.Mislead,
                SpellConstants.Nightmare,
                SpellConstants.PersistentImage,
                SpellConstants.Seeming,
                SpellConstants.ShadowEvocation,
                SpellConstants.ShadowWalk,
                SpellConstants.SongOfDiscord,
                SpellConstants.Suggestion_Mass,
                SpellConstants.SummonMonsterV,
                SpellConstants.AnalyzeDweomer,
                SpellConstants.AnimateObjects,
                SpellConstants.CatsGrace_Mass,
                SpellConstants.CharmMonster_Mass,
                SpellConstants.CureInflictModerateWounds_Mass,
                SpellConstants.EaglesSplendor_Mass,
                SpellConstants.Eyebite,
                SpellConstants.FindThePath,
                SpellConstants.FoxsCunning_Mass,
                SpellConstants.GeasQuest,
                SpellConstants.HeroesFeast,
                SpellConstants.IrresistibleDance,
                SpellConstants.PermanentImage,
                SpellConstants.ProgrammedImage,
                SpellConstants.ProjectImage,
                SpellConstants.Scrying_Greater,
                SpellConstants.Shout_Greater,
                SpellConstants.SummonMonsterVI,
                SpellConstants.SympatheticVibration,
                SpellConstants.Veil
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllBardSpellsInAdjustmentsTable()
        {
            var spellGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Bard]);
        }


        [TestCase(SpellConstants.DancingLights, 0)]
        [TestCase(SpellConstants.Daze, 0)]
        [TestCase(SpellConstants.DetectMagic, 0)]
        [TestCase(SpellConstants.Flare, 0)]
        [TestCase(SpellConstants.GhostSound, 0)]
        [TestCase(SpellConstants.KnowDirection, 0)]
        [TestCase(SpellConstants.Light, 0)]
        [TestCase(SpellConstants.Lullaby, 0)]
        [TestCase(SpellConstants.MageHand, 0)]
        [TestCase(SpellConstants.Mending, 0)]
        [TestCase(SpellConstants.Message, 0)]
        [TestCase(SpellConstants.OpenClose, 0)]
        [TestCase(SpellConstants.Prestidigitation, 0)]
        [TestCase(SpellConstants.ReadMagic, 0)]
        [TestCase(SpellConstants.Resistance, 0)]
        [TestCase(SpellConstants.SummonInstrument, 0)]
        [TestCase(SpellConstants.Alarm, 1)]
        [TestCase(SpellConstants.AnimateRope, 1)]
        [TestCase(SpellConstants.CauseFear, 1)]
        [TestCase(SpellConstants.CharmPerson, 1)]
        [TestCase(SpellConstants.ComprehendLanguages, 1)]
        [TestCase(SpellConstants.Confusion_Lesser, 1)]
        [TestCase(SpellConstants.CureInflictLightWounds, 1)]
        [TestCase(SpellConstants.DetectSecretDoors, 1)]
        [TestCase(SpellConstants.DisguiseSelf, 1)]
        [TestCase(SpellConstants.Erase, 1)]
        [TestCase(SpellConstants.ExpeditiousRetreat, 1)]
        [TestCase(SpellConstants.FeatherFall, 1)]
        [TestCase(SpellConstants.Grease, 1)]
        [TestCase(SpellConstants.HideousLaughter, 1)]
        [TestCase(SpellConstants.Hypnotism, 1)]
        [TestCase(SpellConstants.Identify, 1)]
        [TestCase(SpellConstants.MagicMouth, 1)]
        [TestCase(SpellConstants.MagicAura, 1)]
        [TestCase(SpellConstants.ObscureObject, 1)]
        [TestCase(SpellConstants.RemoveFear, 1)]
        [TestCase(SpellConstants.SilentImage, 1)]
        [TestCase(SpellConstants.Sleep, 1)]
        [TestCase(SpellConstants.SummonMonsterI, 1)]
        [TestCase(SpellConstants.UndetectableAlignment, 1)]
        [TestCase(SpellConstants.UnseenServant, 1)]
        [TestCase(SpellConstants.Ventriloquism, 1)]
        [TestCase(SpellConstants.AlterSelf, 2)]
        [TestCase(SpellConstants.AnimalMessenger, 2)]
        [TestCase(SpellConstants.AnimalTrance, 2)]
        [TestCase(SpellConstants.BlindnessDeafness, 2)]
        [TestCase(SpellConstants.Blur, 2)]
        [TestCase(SpellConstants.CalmEmotions, 2)]
        [TestCase(SpellConstants.CatsGrace, 2)]
        [TestCase(SpellConstants.CureInflictModerateWounds, 2)]
        [TestCase(SpellConstants.Darkness, 2)]
        [TestCase(SpellConstants.DazeMonster, 2)]
        [TestCase(SpellConstants.DelayPoison, 2)]
        [TestCase(SpellConstants.DetectThoughts, 2)]
        [TestCase(SpellConstants.EaglesSplendor, 2)]
        [TestCase(SpellConstants.Enthrall, 2)]
        [TestCase(SpellConstants.FoxsCunning, 2)]
        [TestCase(SpellConstants.Glitterdust, 2)]
        [TestCase(SpellConstants.Heroism, 2)]
        [TestCase(SpellConstants.HoldPerson, 2)]
        [TestCase(SpellConstants.HypnoticPattern, 2)]
        [TestCase(SpellConstants.Invisibility, 2)]
        [TestCase(SpellConstants.LocateObject, 2)]
        [TestCase(SpellConstants.MinorImage, 2)]
        [TestCase(SpellConstants.MirrorImage, 2)]
        [TestCase(SpellConstants.Misdirection, 2)]
        [TestCase(SpellConstants.Pyrotechnics, 2)]
        [TestCase(SpellConstants.Rage, 2)]
        [TestCase(SpellConstants.Scare, 2)]
        [TestCase(SpellConstants.Shatter, 2)]
        [TestCase(SpellConstants.Silence, 2)]
        [TestCase(SpellConstants.SoundBurst, 2)]
        [TestCase(SpellConstants.Suggestion, 2)]
        [TestCase(SpellConstants.SummonMonsterII, 2)]
        [TestCase(SpellConstants.SummonSwarm, 2)]
        [TestCase(SpellConstants.Tongues, 2)]
        [TestCase(SpellConstants.WhisperingWind, 2)]
        [TestCase(SpellConstants.Blink, 3)]
        [TestCase(SpellConstants.CharmMonster, 3)]
        [TestCase(SpellConstants.ClairaudienceClairvoyance, 3)]
        [TestCase(SpellConstants.Confusion, 3)]
        [TestCase(SpellConstants.CrushingDespair, 3)]
        [TestCase(SpellConstants.CureInflictSeriousWounds, 3)]
        [TestCase(SpellConstants.Daylight, 3)]
        [TestCase(SpellConstants.DeepSlumber, 3)]
        [TestCase(SpellConstants.DispelMagic, 3)]
        [TestCase(SpellConstants.Displacement, 3)]
        [TestCase(SpellConstants.Fear, 3)]
        [TestCase(SpellConstants.GaseousForm, 3)]
        [TestCase(SpellConstants.Geas_Lesser, 3)]
        [TestCase(SpellConstants.Glibness, 3)]
        [TestCase(SpellConstants.GoodHope, 3)]
        [TestCase(SpellConstants.Haste, 3)]
        [TestCase(SpellConstants.IllusoryScript, 3)]
        [TestCase(SpellConstants.InvisibilitySphere, 3)]
        [TestCase(SpellConstants.MajorImage, 3)]
        [TestCase(SpellConstants.PhantomSteed, 3)]
        [TestCase(SpellConstants.RemoveCurse, 3)]
        [TestCase(SpellConstants.Scrying, 3)]
        [TestCase(SpellConstants.SculptSound, 3)]
        [TestCase(SpellConstants.SecretPage, 3)]
        [TestCase(SpellConstants.SeeInvisibility, 3)]
        [TestCase(SpellConstants.SepiaSnakeSigil, 3)]
        [TestCase(SpellConstants.Slow, 3)]
        [TestCase(SpellConstants.SpeakWithAnimals, 3)]
        [TestCase(SpellConstants.SummonMonsterIII, 3)]
        [TestCase(SpellConstants.TinyHut, 3)]
        [TestCase(SpellConstants.BreakEnchantment, 4)]
        [TestCase(SpellConstants.CureInflictCriticalWounds, 4)]
        [TestCase(SpellConstants.DetectScrying, 4)]
        [TestCase(SpellConstants.DimensionDoor, 4)]
        [TestCase(SpellConstants.DominatePerson, 4)]
        [TestCase(SpellConstants.FreedomOfMovement, 4)]
        [TestCase(SpellConstants.HallucinatoryTerrain, 4)]
        [TestCase(SpellConstants.HoldMonster, 4)]
        [TestCase(SpellConstants.Invisibility_Greater, 4)]
        [TestCase(SpellConstants.LegendLore, 4)]
        [TestCase(SpellConstants.LocateCreature, 4)]
        [TestCase(SpellConstants.ModifyMemory, 4)]
        [TestCase(SpellConstants.NeutralizePoison, 4)]
        [TestCase(SpellConstants.RainbowPattern, 4)]
        [TestCase(SpellConstants.RepelVermin, 4)]
        [TestCase(SpellConstants.SecureShelter, 4)]
        [TestCase(SpellConstants.ShadowConjuration, 4)]
        [TestCase(SpellConstants.Shout, 4)]
        [TestCase(SpellConstants.SpeakWithPlants, 4)]
        [TestCase(SpellConstants.SummonMonsterIV, 4)]
        [TestCase(SpellConstants.ZoneOfSilence, 4)]
        [TestCase(SpellConstants.CureInflictLightWounds_Mass, 5)]
        [TestCase(SpellConstants.DispelMagic_Greater, 5)]
        [TestCase(SpellConstants.Dream, 5)]
        [TestCase(SpellConstants.FalseVision, 5)]
        [TestCase(SpellConstants.Heroism_Greater, 5)]
        [TestCase(SpellConstants.MindFog, 5)]
        [TestCase(SpellConstants.MirageArcana, 5)]
        [TestCase(SpellConstants.Mislead, 5)]
        [TestCase(SpellConstants.Nightmare, 5)]
        [TestCase(SpellConstants.PersistentImage, 5)]
        [TestCase(SpellConstants.Seeming, 5)]
        [TestCase(SpellConstants.ShadowEvocation, 5)]
        [TestCase(SpellConstants.ShadowWalk, 5)]
        [TestCase(SpellConstants.SongOfDiscord, 5)]
        [TestCase(SpellConstants.Suggestion_Mass, 5)]
        [TestCase(SpellConstants.SummonMonsterV, 5)]
        [TestCase(SpellConstants.AnalyzeDweomer, 6)]
        [TestCase(SpellConstants.AnimateObjects, 6)]
        [TestCase(SpellConstants.CatsGrace_Mass, 6)]
        [TestCase(SpellConstants.CharmMonster_Mass, 6)]
        [TestCase(SpellConstants.CureInflictModerateWounds_Mass, 6)]
        [TestCase(SpellConstants.EaglesSplendor_Mass, 6)]
        [TestCase(SpellConstants.Eyebite, 6)]
        [TestCase(SpellConstants.FindThePath, 6)]
        [TestCase(SpellConstants.FoxsCunning_Mass, 6)]
        [TestCase(SpellConstants.GeasQuest, 6)]
        [TestCase(SpellConstants.HeroesFeast, 6)]
        [TestCase(SpellConstants.IrresistibleDance, 6)]
        [TestCase(SpellConstants.PermanentImage, 6)]
        [TestCase(SpellConstants.ProgrammedImage, 6)]
        [TestCase(SpellConstants.ProjectImage, 6)]
        [TestCase(SpellConstants.Scrying_Greater, 6)]
        [TestCase(SpellConstants.Shout_Greater, 6)]
        [TestCase(SpellConstants.SummonMonsterVI, 6)]
        [TestCase(SpellConstants.SympatheticVibration, 6)]
        [TestCase(SpellConstants.Veil, 6)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
