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
            new (bool AsCharacter, string Creature, string[] Templates)[]
            {
                (false, CreatureConstants.Chimera_White, new[] { CreatureConstants.Templates.Skeleton }),
                (false, CreatureConstants.Criosphinx, new[] { CreatureConstants.Templates.Zombie }),
                (false, CreatureConstants.DisplacerBeast_PackLord, new[] { CreatureConstants.Templates.HalfFiend }),
                (false, CreatureConstants.Dragon_Brass_Young, new[] { CreatureConstants.Templates.Ghost }),
                (false, CreatureConstants.Dragon_Brass_Young, new[] { CreatureConstants.Templates.HalfCelestial }),
                (false, CreatureConstants.Dragon_Bronze_GreatWyrm, new[] { CreatureConstants.Templates.HalfCelestial }),
                (false, CreatureConstants.Dragon_Copper_Adult, new[] { CreatureConstants.Templates.Skeleton }),
                (false, CreatureConstants.Dragon_Silver_Ancient, new[] { CreatureConstants.Templates.HalfCelestial }),
                (false, CreatureConstants.Dragon_White_Old, new[] { CreatureConstants.Templates.HalfFiend }),
                (false, CreatureConstants.Elemental_Air_Small, new[] { CreatureConstants.Templates.HalfCelestial }),
                (true, CreatureConstants.Gargoyle, new[] { CreatureConstants.Templates.HalfCelestial }),
                (false, CreatureConstants.Gargoyle_Kapoacinth, new[] { CreatureConstants.Templates.Ghost }),
                (true, CreatureConstants.GrayRender, new string[0]),
                (false, CreatureConstants.Hieracosphinx, new[] { CreatureConstants.Templates.Skeleton}),
                (false, CreatureConstants.Human, new[] { CreatureConstants.Templates.Ghost }),
                (false, CreatureConstants.Kobold, new[] { CreatureConstants.Templates.HalfFiend}),
                (false, CreatureConstants.Lizardfolk, new[] { CreatureConstants.Templates.HalfFiend}),
                (false, CreatureConstants.Mimic, new[] { CreatureConstants.Templates.Ghost}),
                (false, CreatureConstants.Otyugh, new[] { CreatureConstants.Templates.Ghost}),
                (false, CreatureConstants.Otyugh, new[] { CreatureConstants.Templates.Zombie}),
                (false, CreatureConstants.RazorBoar, new[] { CreatureConstants.Templates.Ghost}),
                (true, CreatureConstants.ShamblingMound, new[] { CreatureConstants.Templates.HalfFiend}),
                (true, CreatureConstants.Skum, new[] { CreatureConstants.Templates.Ghost}),
                (false, CreatureConstants.Troglodyte, new[] { CreatureConstants.Templates.HalfCelestial }),
                (true, CreatureConstants.Xill, new string[0]),
            };

        public static IEnumerable ProblematicCreaturesTestCases => ProblematicCreatures.Select(pc => new TestCaseData(pc.AsCharacter, pc.Creature, pc.Templates));

        public static IEnumerable<(bool AsCharacter, Filters Filters)> ProblematicFilters => new (bool AsCharacter, Filters Filters)[]
        {
            (true, new Filters()),
            (true, new Filters
                {
                    Templates = new List<string> { CreatureConstants.Templates.Ghost },
                    Alignment = AlignmentConstants.LawfulEvil
                }),
            (true, new Filters { Type = CreatureConstants.Types.Subtypes.Earth, ChallengeRating = ChallengeRatingConstants.CR5 }),
            (true, new Filters { Type = CreatureConstants.Types.Subtypes.Reptilian, ChallengeRating = ChallengeRatingConstants.CR0 }),
            (true, new Filters
                {
                    Templates = new List<string> { CreatureConstants.Templates.HalfCelestial },
                    Type = CreatureConstants.Types.Subtypes.Earth,
                    ChallengeRating = ChallengeRatingConstants.CR5
                }),
            (false, new Filters
                {
                    Templates = new List<string> { CreatureConstants.Templates.Ghost },
                    Type = CreatureConstants.Types.Undead
                }),
            (false, new Filters
                {
                    Templates = new List<string> { CreatureConstants.Templates.Ghost },
                    Alignment = AlignmentConstants.ChaoticNeutral
                }),
            (false, new Filters
                {
                    Templates = new List<string> { CreatureConstants.Templates.Ghost },
                    Type = CreatureConstants.Types.Aberration,
                    ChallengeRating = ChallengeRatingConstants.CR6
                }),
            (false, new Filters
                {
                    Templates = new List<string> { CreatureConstants.Templates.HalfCelestial },
                    Type = CreatureConstants.Types.Subtypes.Native
                }),
            (false, new Filters
                {
                    Templates = new List<string> { CreatureConstants.Templates.HalfCelestial },
                    Type = CreatureConstants.Types.Subtypes.Reptilian,
                    ChallengeRating = ChallengeRatingConstants.CR2
                }),
            (false, new Filters
                {
                    Templates = new List<string> { CreatureConstants.Templates.HalfFiend },
                    Type = CreatureConstants.Types.Subtypes.Reptilian,
                    ChallengeRating = ChallengeRatingConstants.CR2
                }),
            (false, new Filters
                {
                    Templates = new List<string> { CreatureConstants.Templates.HalfFiend },
                    Type = CreatureConstants.Types.Subtypes.Reptilian,
                    ChallengeRating = ChallengeRatingConstants.CR1_3rd
                }),
            (false, new Filters
                {
                    Templates = new List<string> { CreatureConstants.Templates.HalfFiend },
                    ChallengeRating = ChallengeRatingConstants.CR15
                }),
            (false, new Filters
                {
                    Templates = new List<string> { CreatureConstants.Templates.Skeleton }
                }),
            (false, new Filters
                {
                    Templates = new List<string> { CreatureConstants.Templates.Zombie }
                }),
            (false, new Filters { Type = CreatureConstants.Types.Aberration, ChallengeRating = ChallengeRatingConstants.CR6 }),
            (false, new Filters { Type = CreatureConstants.Types.Dragon }),
            (false, new Filters { Type = CreatureConstants.Types.Giant }),
            (false, new Filters { Type = CreatureConstants.Types.Humanoid }),
            (false, new Filters { Type = CreatureConstants.Types.Outsider }),
            (false, new Filters { Type = CreatureConstants.Types.MagicalBeast }),
            (false, new Filters { Type = CreatureConstants.Types.Undead }),
            (false, new Filters { Type = CreatureConstants.Types.Subtypes.Augmented }),
            (false, new Filters { Type = CreatureConstants.Types.Subtypes.Incorporeal }),
            (false, new Filters { Type = CreatureConstants.Types.Subtypes.Native }),
            (false, new Filters { Type = CreatureConstants.Types.Subtypes.Reptilian, ChallengeRating = ChallengeRatingConstants.CR1_4th }),
            (false, new Filters { Type = CreatureConstants.Types.Subtypes.Reptilian, ChallengeRating = ChallengeRatingConstants.CR1_3rd }),
            (false, new Filters { Type = CreatureConstants.Types.Subtypes.Reptilian, ChallengeRating = ChallengeRatingConstants.CR1 }),
            (false, new Filters { Type = CreatureConstants.Types.Subtypes.Reptilian, ChallengeRating = ChallengeRatingConstants.CR2 }),
            (false, new Filters { Type = CreatureConstants.Types.Subtypes.Shapechanger }),
            (false, new Filters { ChallengeRating = ChallengeRatingConstants.CR15 }),
        };

        public static IEnumerable ProblematicFiltersTestCases => ProblematicFilters
            .Select(pf => new TestCaseData(pf.Filters.Type, pf.AsCharacter, pf.Filters.Templates.FirstOrDefault(), pf.Filters.ChallengeRating, pf.Filters.Alignment));
    }
}
