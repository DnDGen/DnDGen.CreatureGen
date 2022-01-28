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

        public static IEnumerable<(string Creature, string Template, bool AsCharacter)> ProblematicCreatures => new (string Creature, string Template, bool AsCharacter)[]
        {
            (CreatureConstants.Chimera_White, CreatureConstants.Templates.Skeleton, false),
            (CreatureConstants.Criosphinx, CreatureConstants.Templates.Zombie, false),
            (CreatureConstants.Dragon_Brass_Young, CreatureConstants.Templates.Ghost, false),
            (CreatureConstants.Dragon_Brass_Young, CreatureConstants.Templates.HalfCelestial, false),
            (CreatureConstants.Dragon_Bronze_GreatWyrm, CreatureConstants.Templates.HalfCelestial, false),
            (CreatureConstants.Dragon_Copper_Adult, CreatureConstants.Templates.Skeleton, false),
            (CreatureConstants.Dragon_Silver_Ancient, CreatureConstants.Templates.HalfCelestial, false),
            (CreatureConstants.Dragon_White_Old, CreatureConstants.Templates.HalfFiend, false),
            (CreatureConstants.Gargoyle_Kapoacinth, CreatureConstants.Templates.Ghost, false),
            (CreatureConstants.GrayRender, CreatureConstants.Templates.None, true),
            (CreatureConstants.Hieracosphinx, CreatureConstants.Templates.Skeleton, false),
            (CreatureConstants.Human, CreatureConstants.Templates.Ghost, false),
            (CreatureConstants.Lizardfolk, CreatureConstants.Templates.HalfFiend, false),
            (CreatureConstants.Mimic, CreatureConstants.Templates.Ghost, false),
            (CreatureConstants.Otyugh, CreatureConstants.Templates.Ghost, false),
            (CreatureConstants.Otyugh, CreatureConstants.Templates.Zombie, false),
            (CreatureConstants.RazorBoar, CreatureConstants.Templates.Ghost, false),
            (CreatureConstants.ShamblingMound, CreatureConstants.Templates.HalfFiend, true),
            (CreatureConstants.Skum, CreatureConstants.Templates.Ghost, true),
            (CreatureConstants.Troglodyte, CreatureConstants.Templates.HalfCelestial, false),
            (CreatureConstants.Xill, CreatureConstants.Templates.None, true),
        };

        public static IEnumerable ProblematicCreaturesTestCases => ProblematicCreatures.Select(pc => new TestCaseData(pc.Creature, pc.Template, pc.AsCharacter));

        public static IEnumerable<(bool AsCharacter, Filters Filters)> ProblematicFilters => new (bool AsCharacter, Filters Filters)[]
        {
                (true, new Filters()),
                (true, new Filters { Template = CreatureConstants.Templates.Ghost, Alignment = AlignmentConstants.LawfulEvil }),
                (false, new Filters { Template = CreatureConstants.Templates.Ghost, Type = CreatureConstants.Types.Undead }),
                (false, new Filters { Template = CreatureConstants.Templates.Ghost, Alignment = AlignmentConstants.ChaoticNeutral }),
                (false, new Filters
                    {
                        Template = CreatureConstants.Templates.Ghost,
                        Type = CreatureConstants.Types.Aberration,
                        ChallengeRating = ChallengeRatingConstants.CR6
                    }),
                (false, new Filters
                    {
                        Template = CreatureConstants.Templates.HalfCelestial,
                        Type = CreatureConstants.Types.Subtypes.Reptilian,
                        ChallengeRating = ChallengeRatingConstants.CR2
                    }),
                (false, new Filters
                    {
                        Template = CreatureConstants.Templates.HalfFiend,
                        Type = CreatureConstants.Types.Subtypes.Reptilian,
                        ChallengeRating = ChallengeRatingConstants.CR2
                    }),
                (false, new Filters { Template = CreatureConstants.Templates.Skeleton }),
                (false, new Filters { Template = CreatureConstants.Templates.Zombie }),
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
                (false, new Filters { Type = CreatureConstants.Types.Subtypes.Reptilian, ChallengeRating = ChallengeRatingConstants.CR2 }),
                (false, new Filters { Type = CreatureConstants.Types.Subtypes.Shapechanger }),
        };

        public static IEnumerable ProblematicFiltersTestCases => ProblematicFilters
            .Select(pf => new TestCaseData(pf.Filters.Type, pf.AsCharacter, pf.Filters.Template, pf.Filters.ChallengeRating, pf.Filters.Alignment));
    }
}
