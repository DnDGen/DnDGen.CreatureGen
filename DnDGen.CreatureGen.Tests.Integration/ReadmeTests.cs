using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Abilities;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.IoC;
using DnDGen.CreatureGen.Verifiers;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DnDGen.CreatureGen.Tests.Integration
{
    [TestFixture]
    public partial class ReadmeTests : IntegrationTests
    {
        private const string ThemeSongBadge = "[![Theme Song](https://img.shields.io/badge/theme_song-Suno-ff69b4)]";
        private const string RecordUrl = "https://github.com/DnDGen/DnDGen.CreatureGen/blob/main/docs/theme-song/README.md";
        private const BindingFlags DeclaredPublicMembers = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;

        private static readonly Type[] PublicTypes = typeof(Creature).Assembly.GetExportedTypes();
        private static readonly Dictionary<Type, string> TypeKeywords = new()
        {
            [typeof(bool)] = "bool",
            [typeof(char)] = "char",
            [typeof(double)] = "double",
            [typeof(int)] = "int",
            [typeof(long)] = "long",
            [typeof(object)] = "object",
            [typeof(string)] = "string",
            [typeof(void)] = "void",
        };

        private string[] _readme;

        [OneTimeSetUp]
        public void ReadReadme()
        {
            var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "README.md");
            _readme = File.ReadAllLines(path);
        }

        [Test]
        public void GettingTheGenerators()
        {
            var newKernel = new StandardKernel();
            CreatureGenModuleLoader.LoadModules(newKernel);

            Assert.That(newKernel.Get<ICreatureGenerator>(), Is.Not.Null);
            Assert.That(newKernel.Get<ICreatureVerifier>(), Is.Not.Null);
        }

        [Test]
        public void GeneratingASpecificCreature()
        {
            var creatureGenerator = GetNewInstanceOf<ICreatureGenerator>();

            var ogre = creatureGenerator.Generate(false, CreatureConstants.Ogre);
            var zombieOgre = creatureGenerator.Generate(false, CreatureConstants.Ogre, null, CreatureConstants.Templates.Zombie);

            Assert.That(ogre.Name, Is.EqualTo(CreatureConstants.Ogre));
            Assert.That(ogre.Templates, Is.Empty);
            Assert.That(zombieOgre.Name, Is.EqualTo(CreatureConstants.Ogre));
            Assert.That(zombieOgre.Templates, Is.EqualTo([CreatureConstants.Templates.Zombie]));
        }

        [Test]
        public void GeneratingASpecificCreature_Async()
        {
            var creatureGenerator = GetNewInstanceOf<ICreatureGenerator>();

            var humanCharacter = creatureGenerator.GenerateAsync(true, CreatureConstants.Human).Result;

            Assert.That(humanCharacter.Name, Is.EqualTo(CreatureConstants.Human));
        }

        [Test]
        public void ChainingTemplates()
        {
            var creatureGenerator = GetNewInstanceOf<ICreatureGenerator>();

            var halfDragonHalfFiend = creatureGenerator.Generate(
                false,
                CreatureConstants.Human,
                null,
                CreatureConstants.Templates.HalfDragon_Black,
                CreatureConstants.Templates.HalfFiend);

            Assert.That(halfDragonHalfFiend.Templates, Is.EqualTo(
            [
                CreatureConstants.Templates.HalfDragon_Black,
                CreatureConstants.Templates.HalfFiend,
            ]));
        }

        [Test]
        public void AbilityScores()
        {
            var creatureGenerator = GetNewInstanceOf<ICreatureGenerator>();

            var abilityRandomizer = new AbilityRandomizer(AbilityConstants.RandomizerRolls.BestOfFour)
            {
                PriorityAbility = AbilityConstants.Intelligence
            };

            var elfLich = creatureGenerator.Generate(true, CreatureConstants.Elf_High, abilityRandomizer, CreatureConstants.Templates.Lich);

            var setRandomizer = new AbilityRandomizer();
            setRandomizer.SetRolls[AbilityConstants.Strength] = 18;
            setRandomizer.SetRolls[AbilityConstants.Charisma] = 8;

            var strongDwarf = creatureGenerator.Generate(true, CreatureConstants.Dwarf_Hill, setRandomizer);

            Assert.That(elfLich.Templates, Is.EqualTo([CreatureConstants.Templates.Lich]));
            Assert.That(strongDwarf.Abilities[AbilityConstants.Strength].BaseScore, Is.EqualTo(18));
        }

        [Test]
        public void AbilityScores_AnyValidRoll()
        {
            var creatureGenerator = GetNewInstanceOf<ICreatureGenerator>();
            var abilityRandomizer = new AbilityRandomizer("2d6+6");

            var dwarf = creatureGenerator.Generate(true, CreatureConstants.Dwarf_Hill, abilityRandomizer);

            Assert.That(dwarf.Abilities.Values.Select(a => a.BaseScore), Has.All.InRange(8, 18));
        }

        [Test]
        public void RandomCreatures()
        {
            var creatureGenerator = GetNewInstanceOf<ICreatureGenerator>();

            var (creatureName, templates) = creatureGenerator.GenerateRandomName(false);
            var randomCreature = creatureGenerator.GenerateRandom(false);
            var randomCharacter = creatureGenerator.GenerateRandomAsync(true).Result;
            var randomGhost = creatureGenerator.GenerateRandom(false, null, null, CreatureConstants.Templates.Ghost);
            var randomNonTemplated = creatureGenerator.GenerateRandom(false, null, null, CreatureConstants.Templates.None);

            Assert.That(creatureName, Is.Not.Empty);
            Assert.That(templates, Is.Not.Null);
            Assert.That(randomCreature, Is.Not.Null);
            Assert.That(randomCharacter, Is.Not.Null);
            Assert.That(randomGhost.Templates, Is.EqualTo([CreatureConstants.Templates.Ghost]));
            Assert.That(randomNonTemplated.Templates, Is.Empty);
        }

        [Test]
        public void RandomCreaturesWithFilters()
        {
            var creatureGenerator = GetNewInstanceOf<ICreatureGenerator>();

            var filters = new Filters
            {
                Types = [CreatureConstants.Types.Humanoid],
                ChallengeRatings = [ChallengeRatingConstants.CR3, ChallengeRatingConstants.CR4],
                Alignments = [AlignmentConstants.NeutralEvil, AlignmentConstants.ChaoticEvil],
            };
            var abilityRandomizer = new AbilityRandomizer(AbilityConstants.RandomizerRolls.BestOfFour);

            var (creatureName, _) = creatureGenerator.GenerateRandomName(false, filters);
            var randomWithFilters = creatureGenerator.GenerateRandom(false, null, filters);
            var randomHalfFiend = creatureGenerator.GenerateRandomAsync(false, abilityRandomizer, filters, CreatureConstants.Templates.HalfFiend).Result;

            Assert.That(creatureName, Is.Not.Empty);
            Assert.That(randomWithFilters.Type.AllTypes, Contains.Item(CreatureConstants.Types.Humanoid));
            Assert.That(randomHalfFiend.Templates, Is.EqualTo([CreatureConstants.Templates.HalfFiend]));
        }

        [Test]
        public void CheckingCompatibility()
        {
            var creatureVerifier = GetNewInstanceOf<ICreatureVerifier>();
            var creatureGenerator = GetNewInstanceOf<ICreatureGenerator>();
            var abilityRandomizer = new AbilityRandomizer(AbilityConstants.RandomizerRolls.BestOfFour);
            var filters = new Filters { Types = [CreatureConstants.Types.Humanoid] };

            var canBeGhost = creatureVerifier.VerifyCompatibility(false, CreatureConstants.Ogre, null, null, CreatureConstants.Templates.Ghost);
            var anyValidCreature = creatureVerifier.VerifyCompatibility(false, null, abilityRandomizer, filters);

            Assert.That(canBeGhost, Is.True);
            Assert.That(anyValidCreature, Is.True);
            Assert.That(() => creatureGenerator.Generate(false, CreatureConstants.Ogre, null, CreatureConstants.Templates.Ghost), Throws.Nothing);
        }

        [Test]
        public void IncompatibleCreature_Throws()
        {
            var creatureGenerator = GetNewInstanceOf<ICreatureGenerator>();
            var setRandomizer = new AbilityRandomizer();
            setRandomizer.SetRolls[AbilityConstants.Charisma] = 1;

            Assert.That(() => creatureGenerator.Generate(false, CreatureConstants.Ogre, setRandomizer, CreatureConstants.Templates.Ghost),
                Throws.InstanceOf<InvalidCreatureException>());
        }

        [Test]
        public void ThemeSong_HasBadgeBesideTheBuildBadge()
        {
            var buildBadgeLine = _readme.FirstOrDefault(line => line.StartsWith("[![Build Status]")) ?? string.Empty;
            Assert.That(buildBadgeLine, Does.Contain($" {ThemeSongBadge}(https://suno.com/s/"));
        }

        [Test]
        public void ThemeSong_HasSectionAtTheEnd()
        {
            var headings = GetLinesOutsideCodeBlocks(_readme).Where(line => line.StartsWith("## "));
            Assert.That(headings.LastOrDefault(), Is.EqualTo("## Theme Song"));
        }

        [Test]
        public void ThemeSong_LinksToTheRecord()
        {
            var links = GetLinks(_readme);
            Assert.That(links, Does.Contain(RecordUrl));

            var recordPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "docs", "theme-song", "README.md");
            Assert.That(recordPath, Does.Exist);
        }

        [Test]
        public void ThemeSong_LinksToTheSameSongAsTheRecord()
        {
            var recordPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "docs", "theme-song", "README.md");
            var record = File.ReadAllLines(recordPath);
            string[] recordSongs = [.. GetLinks(record).Where(IsSunoLink).Distinct()];
            string[] readmeSongs = [.. GetLinks(_readme).Where(IsSunoLink).Distinct()];

            Assert.That(recordSongs, Has.Exactly(1).Items, "The record needs to link to one song");
            Assert.That(readmeSongs, Is.EqualTo(recordSongs));
        }

        [Test]
        public void DocumentLinks_AreAbsolute()
        {
            var relativeLinks = GetLinks(_readme).Where(link => !link.StartsWith("http://") && !link.StartsWith("https://"));
            Assert.That(relativeLinks, Is.Empty, "Relative links don't work on NuGet");
        }

        [Test]
        public void Constructors_AreAllDocumented()
        {
            var constructors = PublicTypes
                .Where(type => !IsConstantsClass(type))
                .SelectMany(type => type.GetConstructors().Select(constructor => $"{WriteTypeName(type)}.{WriteMember(constructor)}"));

            Assert.That(GetDocumented("Constructor"), Is.EquivalentTo(constructors));
        }

        [Test]
        public void Methods_AreAllDocumented()
        {
            var methods = PublicTypes
                .SelectMany(type => type.GetMethods(DeclaredPublicMembers)
                    .Where(method => !method.IsSpecialName && !OverridesObject(method))
                    .Select(method => $"{WriteTypeName(type)}.{WriteMember(method)}"));

            Assert.That(GetDocumented("Method"), Is.EquivalentTo(methods));
        }

        [Test]
        public void Properties_AreAllDocumented()
        {
            var members = PublicTypes.SelectMany(type =>
            {
                var fields = IsConstantsClass(type) ? [] : type.GetFields(DeclaredPublicMembers).Select(field => field.Name);
                var properties = type.GetProperties(DeclaredPublicMembers).Select(property => property.Name);

                return fields.Concat(properties).Select(member => $"{WriteTypeName(type)}.{member}");
            });

            Assert.That(GetDocumented("Property"), Is.EquivalentTo(members));
        }

        [Test]
        public void ConstantsClasses_AreAllDocumented()
        {
            var constantsClasses = PublicTypes.Where(IsConstantsClass).Select(WriteTypeName);
            Assert.That(GetTableRows("Constants").SelectMany(GetCodeSpans), Is.EquivalentTo(constantsClasses));
        }

        private static bool IsConstantsClass(Type type) => type.IsAbstract
            && type.IsSealed
            && (type.Name.EndsWith("Constants") || (type.DeclaringType != null && IsConstantsClass(type.DeclaringType)));

        private static bool OverridesObject(MethodInfo method) => method.GetBaseDefinition().DeclaringType == typeof(object);

        private static string WriteTypeName(Type type) => type.DeclaringType == null ? type.Name : $"{WriteTypeName(type.DeclaringType)}.{type.Name}";

        private string[] GetDocumented(string kind) => [.. GetTableRows(kind)
            .SelectMany(row => CodeSpanPartialRegex().Matches(row.Cells[0]).Select(match => $"{row.Heading}.{match.Groups[1].Value}"))];

        private static string[] GetCodeSpans(TableRow row) => [.. CodeSpanPartialRegex().Matches(row.Cells[0]).Select(match => match.Groups[1].Value)];

        private IEnumerable<TableRow> GetTableRows(string kind)
        {
            var heading = string.Empty;
            string[] header = [];

            foreach (var line in GetLinesOutsideCodeBlocks(_readme))
            {
                if (line.StartsWith('#'))
                    heading = line.TrimStart('#').Trim().Trim('`');

                if (!line.StartsWith('|'))
                {
                    header = [];
                    continue;
                }

                string[] cells = [.. line.Trim().Trim('|').Split('|').Select(cell => cell.Trim())];

                if (header.Length == 0)
                    header = cells;
                else if (header[0] == kind && !cells[0].StartsWith("---"))
                    yield return new TableRow(heading, cells);
            }
        }

        private static string WriteMember(MethodBase member)
        {
            var name = member is ConstructorInfo ? member.DeclaringType.Name : member.Name;

            if (member.IsGenericMethod)
                name += $"<{string.Join(", ", member.GetGenericArguments().Select(WriteType))}>";

            var parameters = member.GetParameters().Select(WriteParameter);

            return $"{name}({string.Join(", ", parameters)})";
        }

        private static string WriteParameter(ParameterInfo parameter)
        {
            var written = $"{WriteType(parameter.ParameterType)} {parameter.Name}";

            if (parameter.IsDefined(typeof(ParamArrayAttribute)))
                written = $"params {written}";

            if (parameter.HasDefaultValue)
                written += $" = {WriteValue(parameter.DefaultValue)}";

            return written;
        }

        private static string WriteType(Type type)
        {
            if (Nullable.GetUnderlyingType(type) is Type underlyingType)
                return $"{WriteType(underlyingType)}?";

            if (type.IsArray)
                return $"{WriteType(type.GetElementType())}[]";

            if (type.IsGenericType && type.FullName.StartsWith("System.ValueTuple"))
                return $"({string.Join(", ", type.GetGenericArguments().Select(WriteType))})";

            if (type.IsGenericType)
                return $"{type.Name[..type.Name.IndexOf('`')]}<{string.Join(", ", type.GetGenericArguments().Select(WriteType))}>";

            return TypeKeywords.GetValueOrDefault(type, type.Name);
        }

        private static string WriteValue(object value) => value switch
        {
            null => "null",
            bool boolean => boolean ? "true" : "false",
            char character => $"'{character.ToString().Replace("\\", "\\\\")}'",
            string text => $"\"{text}\"",
            _ => Convert.ToString(value, CultureInfo.InvariantCulture),
        };

        private static bool IsSunoLink(string link) => link.StartsWith("https://suno.com/");

        private static string[] GetLinks(IEnumerable<string> lines)
        {
            var links = GetLinesOutsideCodeBlocks(lines)
                .SelectMany(line => MarkdownLinkPartialRegex().Matches(line).Concat(HtmlLinkPartialRegex().Matches(line)))
                .Select(match => match.Groups[1].Value);

            return [.. links];
        }

        private static IEnumerable<string> GetLinesOutsideCodeBlocks(IEnumerable<string> lines)
        {
            var inCodeBlock = false;

            foreach (var line in lines)
            {
                if (line.StartsWith("```"))
                    inCodeBlock = !inCodeBlock;
                else if (!inCodeBlock)
                    yield return line;
            }
        }

        private record TableRow(string Heading, string[] Cells);

        [GeneratedRegex("`([^`]+)`")]
        private static partial Regex CodeSpanPartialRegex();

        [GeneratedRegex(@"\]\(([^)\s]+)\)")]
        private static partial Regex MarkdownLinkPartialRegex();

        [GeneratedRegex(@"(?:href|src)=""([^""]+)""")]
        private static partial Regex HtmlLinkPartialRegex();
    }
}
