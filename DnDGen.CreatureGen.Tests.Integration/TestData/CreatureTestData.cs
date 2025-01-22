using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Creatures;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.TestData
{
    public class CreatureTestData
    {
        public static IEnumerable Creatures => CreatureConstants.GetAll().Select(c => new TestCaseData(c));
        public static IEnumerable Characters => CreatureConstants.GetAllCharacters().Select(c => new TestCaseData(c));
        public static IEnumerable Templates => CreatureConstants.Templates.GetAll().Select(t => new TestCaseData(t));
        public static IEnumerable Types => CreatureConstants.Types.GetAll().Select(t => new TestCaseData(t));
        public static IEnumerable Subtypes => CreatureConstants.Types.Subtypes.GetAll().Select(t => new TestCaseData(t));

        public static IEnumerable<(bool AsCharacter, string Creature, string[] Templates)> ProblematicCreatures =>
            [
                (false, CreatureConstants.Chimera_White, [CreatureConstants.Templates.Skeleton]),
                (false, CreatureConstants.Chimera_Green, [CreatureConstants.Templates.HalfCelestial]),
                (false, CreatureConstants.Criosphinx, [CreatureConstants.Templates.Zombie]),
                (false, CreatureConstants.DisplacerBeast_PackLord, [CreatureConstants.Templates.HalfFiend]),
                (false, CreatureConstants.Dragon_Brass_Young, [CreatureConstants.Templates.Ghost]),
                (false, CreatureConstants.Dragon_Brass_Young, [CreatureConstants.Templates.HalfCelestial]),
                (false, CreatureConstants.Dragon_Bronze_GreatWyrm, [CreatureConstants.Templates.HalfCelestial]),
                (false, CreatureConstants.Dragon_Copper_Adult, [CreatureConstants.Templates.Skeleton]),
                (false, CreatureConstants.Dragon_Silver_Ancient, [CreatureConstants.Templates.HalfCelestial]),
                (false, CreatureConstants.Dragon_White_Old, [CreatureConstants.Templates.HalfFiend]),
                (false, CreatureConstants.Elemental_Air_Small, [CreatureConstants.Templates.HalfCelestial]),
                (false, CreatureConstants.Elemental_Fire_Medium, [CreatureConstants.Templates.HalfFiend]),
                (true, CreatureConstants.Gargoyle, [CreatureConstants.Templates.HalfCelestial]),
                (true, CreatureConstants.Gargoyle, [CreatureConstants.Templates.HalfFiend]),
                (false, CreatureConstants.Gargoyle_Kapoacinth, [CreatureConstants.Templates.Ghost]),
                (true, CreatureConstants.Gargoyle_Kapoacinth, [CreatureConstants.Templates.HalfCelestial]),
                (false, CreatureConstants.GibberingMouther, [CreatureConstants.Templates.HalfCelestial]),
                (false, CreatureConstants.GibberingMouther, [CreatureConstants.Templates.HalfFiend]),
                (true, CreatureConstants.Gnoll, [CreatureConstants.Templates.Ghost]),
                (true, CreatureConstants.GrayRender, []),
                (false, CreatureConstants.Hieracosphinx, [CreatureConstants.Templates.Skeleton]),
                (false, CreatureConstants.Human, [CreatureConstants.Templates.Ghost]),
                (false, CreatureConstants.Kobold, [CreatureConstants.Templates.HalfFiend]),
                (false, CreatureConstants.Lizardfolk, [CreatureConstants.Templates.HalfCelestial]),
                (false, CreatureConstants.Lizardfolk, [CreatureConstants.Templates.HalfFiend]),
                (false, CreatureConstants.Mimic, [CreatureConstants.Templates.Ghost]),
                (false, CreatureConstants.Otyugh, [CreatureConstants.Templates.Ghost]),
                (false, CreatureConstants.Otyugh, [CreatureConstants.Templates.Zombie]),
                (false, CreatureConstants.RazorBoar, [CreatureConstants.Templates.Ghost]),
                (true, CreatureConstants.ShamblingMound, [CreatureConstants.Templates.HalfFiend]),
                (true, CreatureConstants.Skum, [CreatureConstants.Templates.Ghost]),
                (false, CreatureConstants.Troglodyte, [CreatureConstants.Templates.HalfCelestial]),
                (false, CreatureConstants.Troglodyte, [CreatureConstants.Templates.HalfFiend]),
                (true, CreatureConstants.Xill, []),
            ];

        public static IEnumerable ProblematicCreaturesTestCases => ProblematicCreatures.Select(pc => new TestCaseData(pc.AsCharacter, pc.Creature, pc.Templates));

        public static IEnumerable<(bool AsCharacter, Filters Filters)> ProblematicFilters =>
        [
            (true, new Filters()),
            (false, new Filters { ChallengeRating = ChallengeRatingConstants.CR15 }),
            (false, new Filters { Type = CreatureConstants.Types.Aberration, ChallengeRating = ChallengeRatingConstants.CR6 }),
            (false, new Filters { Type = CreatureConstants.Types.Dragon }),
            (false, new Filters { Type = CreatureConstants.Types.Giant }),
            (false, new Filters { Type = CreatureConstants.Types.Humanoid }),
            (false, new Filters { Type = CreatureConstants.Types.MagicalBeast }),
            (false, new Filters { Type = CreatureConstants.Types.Outsider }),
            (true, new Filters { Type = CreatureConstants.Types.Plant, Alignment = AlignmentConstants.LawfulEvil }),
            (false, new Filters { Type = CreatureConstants.Types.Undead }),
            (false, new Filters { Type = CreatureConstants.Types.Subtypes.Augmented }),
            (true, new Filters { Type = CreatureConstants.Types.Subtypes.Earth, ChallengeRating = ChallengeRatingConstants.CR5 }),
            (false, new Filters { Type = CreatureConstants.Types.Subtypes.Incorporeal }),
            (false, new Filters { Type = CreatureConstants.Types.Subtypes.Native }),
            (true, new Filters { Type = CreatureConstants.Types.Subtypes.Reptilian, ChallengeRating = ChallengeRatingConstants.CR0 }),
            (false, new Filters { Type = CreatureConstants.Types.Subtypes.Reptilian, ChallengeRating = ChallengeRatingConstants.CR1_4th }),
            (false, new Filters { Type = CreatureConstants.Types.Subtypes.Reptilian, ChallengeRating = ChallengeRatingConstants.CR1_3rd }),
            (false, new Filters { Type = CreatureConstants.Types.Subtypes.Reptilian, ChallengeRating = ChallengeRatingConstants.CR1 }),
            (false, new Filters { Type = CreatureConstants.Types.Subtypes.Reptilian, ChallengeRating = ChallengeRatingConstants.CR2 }),
            (false, new Filters { Type = CreatureConstants.Types.Subtypes.Shapechanger }),
            (false, new Filters
                {
                    Templates = [CreatureConstants.Templates.Ghost],
                    Alignment = AlignmentConstants.ChaoticNeutral
                }),
            (true, new Filters
                {
                    Templates = [CreatureConstants.Templates.Ghost],
                    Alignment = AlignmentConstants.LawfulEvil
                }),
            (false, new Filters
                {
                    Templates = [CreatureConstants.Templates.Ghost],
                    Type = CreatureConstants.Types.Undead
                }),
            (true, new Filters
                {
                    Templates = [CreatureConstants.Templates.Ghost],
                    Type = CreatureConstants.Types.Subtypes.Gnoll
                }),
            (false, new Filters
                {
                    Templates = [CreatureConstants.Templates.Ghost],
                    Type = CreatureConstants.Types.Aberration,
                    ChallengeRating = ChallengeRatingConstants.CR6
                }),
            (false, new Filters
                {
                    Templates = [CreatureConstants.Templates.HalfCelestial],
                    Type = CreatureConstants.Types.Aberration
                }),
            (false, new Filters
                {
                    Templates = [CreatureConstants.Templates.HalfCelestial],
                    Type = CreatureConstants.Types.Subtypes.Native
                }),
            (true, new Filters
                {
                    Templates = [CreatureConstants.Templates.HalfCelestial],
                    Type = CreatureConstants.Types.Subtypes.Earth,
                    ChallengeRating = ChallengeRatingConstants.CR5
                }),
            (false, new Filters
                {
                    Templates = [CreatureConstants.Templates.HalfCelestial],
                    Type = CreatureConstants.Types.Subtypes.Reptilian,
                    ChallengeRating = ChallengeRatingConstants.CR2
                }),
            (false, new Filters
                {
                    Templates = [CreatureConstants.Templates.HalfFiend],
                    Type = CreatureConstants.Types.Aberration
                }),
            (true, new Filters
                {
                    Templates = [CreatureConstants.Templates.HalfFiend],
                    Type = CreatureConstants.Types.Plant,
                    Alignment = AlignmentConstants.LawfulEvil
                }),
            (false, new Filters
                {
                    Templates = [CreatureConstants.Templates.HalfFiend],
                    Type = CreatureConstants.Types.Subtypes.Native
                }),
            (true, new Filters
                {
                    Templates = [CreatureConstants.Templates.HalfFiend],
                    Type = CreatureConstants.Types.Subtypes.Earth,
                    ChallengeRating = ChallengeRatingConstants.CR5
                }),
            (false, new Filters
                {
                    Templates = [CreatureConstants.Templates.HalfFiend],
                    Type = CreatureConstants.Types.Subtypes.Reptilian,
                    ChallengeRating = ChallengeRatingConstants.CR2
                }),
            (false, new Filters
                {
                    Templates = [CreatureConstants.Templates.HalfFiend],
                    Type = CreatureConstants.Types.Subtypes.Reptilian,
                    ChallengeRating = ChallengeRatingConstants.CR1_3rd
                }),
            (false, new Filters
                {
                    Templates = [CreatureConstants.Templates.HalfFiend],
                    ChallengeRating = ChallengeRatingConstants.CR15
                }),
            (false, new Filters
                {
                    Templates = [CreatureConstants.Templates.Skeleton]
                }),
            (false, new Filters
                {
                    Templates = [CreatureConstants.Templates.Zombie]
                }),
        ];

        public static IEnumerable ProblematicFiltersTestCases => ProblematicFilters
            .Select(pf => new TestCaseData(pf.Filters.Type, pf.AsCharacter, pf.Filters.Templates.FirstOrDefault(), pf.Filters.ChallengeRating, pf.Filters.Alignment));
    }
}
