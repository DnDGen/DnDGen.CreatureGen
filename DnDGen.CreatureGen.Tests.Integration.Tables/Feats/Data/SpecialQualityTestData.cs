using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Magics;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.RollGen;
using DnDGen.TreasureGen.Items;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Feats.Data
{
    public class SpecialQualityTestData
    {
        private static string BuildData(
            string creature,
            string featName,
            string focus = "",
            int frequencyQuantity = 0,
            string frequencyTimePeriod = "",
            int power = 0,
            int randomFociQuantity = 0,
            bool requiresEquipment = false,
            string saveAbility = "",
            string save = "",
            int saveBaseValue = 0,
            int minHitDice = 0,
            int maxHitDice = int.MaxValue)
        {
            return BuildData(creature, featName, randomFociQuantity.ToString(), focus, frequencyQuantity, frequencyTimePeriod, power, requiresEquipment, saveAbility, save, saveBaseValue, minHitDice, maxHitDice);
        }

        private static string BuildData(
            string creature,
            string featName,
            string randomFociQuantity,
            string focus = "",
            int frequencyQuantity = 0,
            string frequencyTimePeriod = "",
            int power = 0,
            bool requiresEquipment = false,
            string saveAbility = "",
            string save = "",
            int saveBaseValue = 0,
            int minHitDice = 0,
            int maxHitDice = int.MaxValue)
        {
            var minimumAbilities = GetMinimumAbilities();
            var requiredFeats = GetRequiredFeats();
            var requiredSizes = GetRequiredSizes();
            var requiredAlignments = GetRequiredAlignments();

            var abilitiesKey = creature + featName + focus;
            var sizeKey = creature + featName + focus;
            var alignmentKey = creature + featName + focus;
            var featKey = creature + featName;

            return Infrastructure.Helpers.DataHelper.Parse(new SpecialQualityDataSelection
            {
                Feat = featName,
                FocusType = focus,
                FrequencyQuantity = frequencyQuantity,
                FrequencyTimePeriod = frequencyTimePeriod,
                Power = power,
                RandomFociQuantityRoll = randomFociQuantity,
                RequiresEquipment = requiresEquipment,
                Save = save,
                SaveAbility = saveAbility,
                SaveBaseValue = saveBaseValue,
                MinHitDice = minHitDice,
                MaxHitDice = maxHitDice,
                RequiredFeats = requiredFeats.ContainsKey(featKey) ? requiredFeats[featKey] : [],
                MinimumAbilities = minimumAbilities.ContainsKey(abilitiesKey) ? minimumAbilities[abilitiesKey] : [],
                RequiredSizes = requiredSizes.ContainsKey(sizeKey) ? requiredSizes[sizeKey] : [],
                RequiredAlignments = requiredAlignments.ContainsKey(alignmentKey) ? requiredAlignments[alignmentKey] : [],
            });
        }

        private static Dictionary<string, IEnumerable<string>> GetRequiredAlignments()
        {
            var testCases = new Dictionary<string, IEnumerable<string>>
            {
                [CreatureConstants.Titan + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.BestowCurse] = [AlignmentConstants.ChaoticEvil],
                [CreatureConstants.Titan + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.CrushingHand] = [AlignmentConstants.ChaoticEvil],
                [CreatureConstants.Titan + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.Daylight] = [AlignmentConstants.ChaoticGood, AlignmentConstants.ChaoticNeutral],
                [CreatureConstants.Titan + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.DeeperDarkness] = [AlignmentConstants.ChaoticEvil],
                [CreatureConstants.Titan + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.HolySmite] = [AlignmentConstants.ChaoticGood, AlignmentConstants.ChaoticNeutral],
                [CreatureConstants.Titan + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.RemoveCurse] = [AlignmentConstants.ChaoticGood, AlignmentConstants.ChaoticNeutral],
                [CreatureConstants.Titan + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.Restoration_Greater] = [AlignmentConstants.ChaoticGood, AlignmentConstants.ChaoticNeutral],
                [CreatureConstants.Titan + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.UnholyBlight] = [AlignmentConstants.ChaoticEvil]
            };

            return testCases;
        }

        private static Dictionary<string, IEnumerable<string>> GetRequiredSizes()
        {
            var testCases = new Dictionary<string, IEnumerable<string>>
            {
                [CreatureConstants.Giant_Cloud + FeatConstants.SpecialQualities.OversizedWeapon + SizeConstants.Gargantuan] = [SizeConstants.Huge],

                [CreatureConstants.Titan + FeatConstants.SpecialQualities.OversizedWeapon + SizeConstants.Gargantuan] = [SizeConstants.Huge],
                [CreatureConstants.Titan + FeatConstants.SpecialQualities.OversizedWeapon + SizeConstants.Colossal] = [SizeConstants.Gargantuan],

                [CreatureConstants.Types.Subtypes.Swarm + FeatConstants.SpecialQualities.HalfDamage + AttributeConstants.DamageTypes.Piercing] = [SizeConstants.Tiny],
                [CreatureConstants.Types.Subtypes.Swarm + FeatConstants.SpecialQualities.HalfDamage + AttributeConstants.DamageTypes.Slashing] = [SizeConstants.Tiny],
                [CreatureConstants.Types.Subtypes.Swarm + FeatConstants.SpecialQualities.Immunity + "Weapon damage"] = [SizeConstants.Diminutive, SizeConstants.Fine],
                [CreatureConstants.Types.Subtypes.Swarm + FeatConstants.SpecialQualities.Vulnerability + "High winds"] = [SizeConstants.Diminutive, SizeConstants.Fine]
            };

            return testCases;
        }

        private static Dictionary<string, IEnumerable<FeatDataSelection.RequiredFeatDataSelection>> GetRequiredFeats()
        {
            var testCases = new Dictionary<string, IEnumerable<FeatDataSelection.RequiredFeatDataSelection>>
            {
                [CreatureConstants.Types.Aberration + FeatConstants.ShieldProficiency] = [new() { Feat = FeatConstants.ArmorProficiency_Light }],

                [CreatureConstants.Types.Elemental + FeatConstants.ShieldProficiency] = [new() { Feat = FeatConstants.ArmorProficiency_Light }],

                [CreatureConstants.Types.Fey + FeatConstants.ShieldProficiency] = [new() { Feat = FeatConstants.ArmorProficiency_Light }],

                [CreatureConstants.Types.Giant + FeatConstants.ShieldProficiency] = [new() { Feat = FeatConstants.ArmorProficiency_Light }],

                [CreatureConstants.Types.Humanoid + FeatConstants.ShieldProficiency] = [new() { Feat = FeatConstants.ArmorProficiency_Light }],

                [CreatureConstants.Types.MonstrousHumanoid + FeatConstants.ShieldProficiency] = [new() { Feat = FeatConstants.ArmorProficiency_Light }],

                [CreatureConstants.Types.Outsider + FeatConstants.ShieldProficiency] = [new() { Feat = FeatConstants.ArmorProficiency_Light }],

                [CreatureConstants.Types.Undead + FeatConstants.ShieldProficiency] = [new() { Feat = FeatConstants.ArmorProficiency_Light }],

                [CreatureConstants.Types.Subtypes.Shapechanger + FeatConstants.ShieldProficiency] = [new() { Feat = FeatConstants.ArmorProficiency_Light }]
            };

            return testCases;
        }

        private static Dictionary<string, Dictionary<string, int>> GetMinimumAbilities()
        {
            var testCases = new Dictionary<string, Dictionary<string, int>>
            {
                [CreatureConstants.Dwarf_Deep + FeatConstants.WeaponProficiency_Martial + WeaponConstants.DwarvenWaraxe] = new() { [AbilityConstants.Strength] = 13 },

                [CreatureConstants.Dwarf_Hill + FeatConstants.WeaponProficiency_Martial + WeaponConstants.DwarvenWaraxe] = new() { [AbilityConstants.Strength] = 13 },

                [CreatureConstants.Dwarf_Mountain + FeatConstants.WeaponProficiency_Martial + WeaponConstants.DwarvenWaraxe] = new() { [AbilityConstants.Strength] = 13 },

                [CreatureConstants.Gnome_Forest + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.DancingLights] = new() { [AbilityConstants.Charisma] = 10 },
                [CreatureConstants.Gnome_Forest + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.GhostSound] = new() { [AbilityConstants.Charisma] = 10 },
                [CreatureConstants.Gnome_Forest + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.Prestidigitation] = new() { [AbilityConstants.Charisma] = 10 },

                [CreatureConstants.Gnome_Rock + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.DancingLights] = new() { [AbilityConstants.Charisma] = 10 },
                [CreatureConstants.Gnome_Rock + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.GhostSound] = new() { [AbilityConstants.Charisma] = 10 },
                [CreatureConstants.Gnome_Rock + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.Prestidigitation] = new() { [AbilityConstants.Charisma] = 10 },

                [CreatureConstants.Templates.HalfCelestial + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.ProtectionFromEvil] = new()
                {
                    [AbilityConstants.Wisdom] = 8,
                    [AbilityConstants.Intelligence] = 8
                },
                [CreatureConstants.Templates.HalfCelestial + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.Bless] = new()
                {
                    [AbilityConstants.Wisdom] = 8,
                    [AbilityConstants.Intelligence] = 8
                },
                [CreatureConstants.Templates.HalfCelestial + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.Aid] = new()
                {
                    [AbilityConstants.Wisdom] = 8,
                    [AbilityConstants.Intelligence] = 8
                },
                [CreatureConstants.Templates.HalfCelestial + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.DetectEvil] = new()
                {
                    [AbilityConstants.Wisdom] = 8,
                    [AbilityConstants.Intelligence] = 8
                },
                [CreatureConstants.Templates.HalfCelestial + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.CureSeriousWounds] = new()
                {
                    [AbilityConstants.Wisdom] = 8,
                    [AbilityConstants.Intelligence] = 8
                },
                [CreatureConstants.Templates.HalfCelestial + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.NeutralizePoison] = new()
                {
                    [AbilityConstants.Wisdom] = 8,
                    [AbilityConstants.Intelligence] = 8
                },
                [CreatureConstants.Templates.HalfCelestial + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.HolySmite] = new()
                {
                    [AbilityConstants.Wisdom] = 8,
                    [AbilityConstants.Intelligence] = 8
                },
                [CreatureConstants.Templates.HalfCelestial + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.RemoveDisease] = new()
                {
                    [AbilityConstants.Wisdom] = 8,
                    [AbilityConstants.Intelligence] = 8
                },
                [CreatureConstants.Templates.HalfCelestial + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.DispelEvil] = new()
                {
                    [AbilityConstants.Wisdom] = 8,
                    [AbilityConstants.Intelligence] = 8
                },
                [CreatureConstants.Templates.HalfCelestial + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.HolyWord] = new()
                {
                    [AbilityConstants.Wisdom] = 8,
                    [AbilityConstants.Intelligence] = 8
                },
                [CreatureConstants.Templates.HalfCelestial + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.HolyAura] = new()
                {
                    [AbilityConstants.Wisdom] = 8,
                    [AbilityConstants.Intelligence] = 8
                },
                [CreatureConstants.Templates.HalfCelestial + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.Hallow] = new()
                {
                    [AbilityConstants.Wisdom] = 8,
                    [AbilityConstants.Intelligence] = 8
                },
                [CreatureConstants.Templates.HalfCelestial + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.CharmMonster_Mass] = new()
                {
                    [AbilityConstants.Wisdom] = 8,
                    [AbilityConstants.Intelligence] = 8
                },
                [CreatureConstants.Templates.HalfCelestial + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.SummonMonsterIX] = new()
                {
                    [AbilityConstants.Wisdom] = 8,
                    [AbilityConstants.Intelligence] = 8
                },
                [CreatureConstants.Templates.HalfCelestial + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.Resurrection] = new()
                {
                    [AbilityConstants.Wisdom] = 8,
                    [AbilityConstants.Intelligence] = 8
                },

                [CreatureConstants.Templates.HalfFiend + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.Darkness] = new()
                {
                    [AbilityConstants.Wisdom] = 8,
                    [AbilityConstants.Intelligence] = 8
                },
                [CreatureConstants.Templates.HalfFiend + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.Desecrate] = new()
                {
                    [AbilityConstants.Wisdom] = 8,
                    [AbilityConstants.Intelligence] = 8
                },
                [CreatureConstants.Templates.HalfFiend + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.UnholyBlight] = new()
                {
                    [AbilityConstants.Wisdom] = 8,
                    [AbilityConstants.Intelligence] = 8
                },
                [CreatureConstants.Templates.HalfFiend + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.Poison] = new()
                {
                    [AbilityConstants.Wisdom] = 8,
                    [AbilityConstants.Intelligence] = 8
                },
                [CreatureConstants.Templates.HalfFiend + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.Contagion] = new()
                {
                    [AbilityConstants.Wisdom] = 8,
                    [AbilityConstants.Intelligence] = 8
                },
                [CreatureConstants.Templates.HalfFiend + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.Blasphemy] = new()
                {
                    [AbilityConstants.Wisdom] = 8,
                    [AbilityConstants.Intelligence] = 8
                },
                [CreatureConstants.Templates.HalfFiend + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.UnholyAura] = new()
                {
                    [AbilityConstants.Wisdom] = 8,
                    [AbilityConstants.Intelligence] = 8
                },
                [CreatureConstants.Templates.HalfFiend + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.Unhallow] = new()
                {
                    [AbilityConstants.Wisdom] = 8,
                    [AbilityConstants.Intelligence] = 8
                },
                [CreatureConstants.Templates.HalfFiend + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.HorridWilting] = new()
                {
                    [AbilityConstants.Wisdom] = 8,
                    [AbilityConstants.Intelligence] = 8
                },
                [CreatureConstants.Templates.HalfFiend + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.SummonMonsterIX] = new()
                {
                    [AbilityConstants.Wisdom] = 8,
                    [AbilityConstants.Intelligence] = 8
                },
                [CreatureConstants.Templates.HalfFiend + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.Destruction] = new()
                {
                    [AbilityConstants.Wisdom] = 8,
                    [AbilityConstants.Intelligence] = 8
                },

                [CreatureConstants.Templates.Vampire + FeatConstants.Dodge] = new() { [AbilityConstants.Dexterity] = 13 }
            };

            return testCases;
        }

        public static Dictionary<string, List<string>> GetTemplateData()
        {
            var testCases = new Dictionary<string, List<string>>();
            foreach (var template in CreatureConstants.Templates.GetAll())
            {
                testCases[template] = [];
            }

            testCases[CreatureConstants.Templates.CelestialCreature].Add(BuildData(CreatureConstants.Templates.CelestialCreature, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Templates.CelestialCreature].Add(BuildData(CreatureConstants.Templates.CelestialCreature, FeatConstants.SpecialQualities.SpellResistance, power: 6, maxHitDice: 1));
            testCases[CreatureConstants.Templates.CelestialCreature].Add(BuildData(CreatureConstants.Templates.CelestialCreature, FeatConstants.SpecialQualities.SpellResistance, power: 7, minHitDice: 2, maxHitDice: 2));
            testCases[CreatureConstants.Templates.CelestialCreature].Add(BuildData(CreatureConstants.Templates.CelestialCreature, FeatConstants.SpecialQualities.SpellResistance, power: 8, minHitDice: 3, maxHitDice: 3));
            testCases[CreatureConstants.Templates.CelestialCreature].Add(BuildData(CreatureConstants.Templates.CelestialCreature, FeatConstants.SpecialQualities.SpellResistance, power: 9, minHitDice: 4, maxHitDice: 4));
            testCases[CreatureConstants.Templates.CelestialCreature].Add(BuildData(CreatureConstants.Templates.CelestialCreature, FeatConstants.SpecialQualities.SpellResistance, power: 10, minHitDice: 5, maxHitDice: 5));
            testCases[CreatureConstants.Templates.CelestialCreature].Add(BuildData(CreatureConstants.Templates.CelestialCreature, FeatConstants.SpecialQualities.SpellResistance, power: 11, minHitDice: 6, maxHitDice: 6));
            testCases[CreatureConstants.Templates.CelestialCreature].Add(BuildData(CreatureConstants.Templates.CelestialCreature, FeatConstants.SpecialQualities.SpellResistance, power: 12, minHitDice: 7, maxHitDice: 7));
            testCases[CreatureConstants.Templates.CelestialCreature].Add(BuildData(CreatureConstants.Templates.CelestialCreature, FeatConstants.SpecialQualities.SpellResistance, power: 13, minHitDice: 8, maxHitDice: 8));
            testCases[CreatureConstants.Templates.CelestialCreature].Add(BuildData(CreatureConstants.Templates.CelestialCreature, FeatConstants.SpecialQualities.SpellResistance, power: 14, minHitDice: 9, maxHitDice: 9));
            testCases[CreatureConstants.Templates.CelestialCreature].Add(BuildData(CreatureConstants.Templates.CelestialCreature, FeatConstants.SpecialQualities.SpellResistance, power: 15, minHitDice: 10, maxHitDice: 10));
            testCases[CreatureConstants.Templates.CelestialCreature].Add(BuildData(CreatureConstants.Templates.CelestialCreature, FeatConstants.SpecialQualities.SpellResistance, power: 16, minHitDice: 11, maxHitDice: 11));
            testCases[CreatureConstants.Templates.CelestialCreature].Add(BuildData(CreatureConstants.Templates.CelestialCreature, FeatConstants.SpecialQualities.SpellResistance, power: 17, minHitDice: 12, maxHitDice: 12));
            testCases[CreatureConstants.Templates.CelestialCreature].Add(BuildData(CreatureConstants.Templates.CelestialCreature, FeatConstants.SpecialQualities.SpellResistance, power: 18, minHitDice: 13, maxHitDice: 13));
            testCases[CreatureConstants.Templates.CelestialCreature].Add(BuildData(CreatureConstants.Templates.CelestialCreature, FeatConstants.SpecialQualities.SpellResistance, power: 19, minHitDice: 14, maxHitDice: 14));
            testCases[CreatureConstants.Templates.CelestialCreature].Add(BuildData(CreatureConstants.Templates.CelestialCreature, FeatConstants.SpecialQualities.SpellResistance, power: 20, minHitDice: 15, maxHitDice: 15));
            testCases[CreatureConstants.Templates.CelestialCreature].Add(BuildData(CreatureConstants.Templates.CelestialCreature, FeatConstants.SpecialQualities.SpellResistance, power: 21, minHitDice: 16, maxHitDice: 16));
            testCases[CreatureConstants.Templates.CelestialCreature].Add(BuildData(CreatureConstants.Templates.CelestialCreature, FeatConstants.SpecialQualities.SpellResistance, power: 22, minHitDice: 17, maxHitDice: 17));
            testCases[CreatureConstants.Templates.CelestialCreature].Add(BuildData(CreatureConstants.Templates.CelestialCreature, FeatConstants.SpecialQualities.SpellResistance, power: 23, minHitDice: 18, maxHitDice: 18));
            testCases[CreatureConstants.Templates.CelestialCreature].Add(BuildData(CreatureConstants.Templates.CelestialCreature, FeatConstants.SpecialQualities.SpellResistance, power: 24, minHitDice: 19, maxHitDice: 19));
            testCases[CreatureConstants.Templates.CelestialCreature].Add(BuildData(CreatureConstants.Templates.CelestialCreature, FeatConstants.SpecialQualities.SpellResistance, power: 25, minHitDice: 20));
            testCases[CreatureConstants.Templates.CelestialCreature].Add(BuildData(CreatureConstants.Templates.CelestialCreature, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round, maxHitDice: 7));
            testCases[CreatureConstants.Templates.CelestialCreature].Add(BuildData(CreatureConstants.Templates.CelestialCreature, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round, minHitDice: 8));
            testCases[CreatureConstants.Templates.CelestialCreature].Add(BuildData(CreatureConstants.Templates.CelestialCreature, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round, maxHitDice: 7));
            testCases[CreatureConstants.Templates.CelestialCreature].Add(BuildData(CreatureConstants.Templates.CelestialCreature, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round, minHitDice: 8));
            testCases[CreatureConstants.Templates.CelestialCreature].Add(BuildData(CreatureConstants.Templates.CelestialCreature, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round, maxHitDice: 7));
            testCases[CreatureConstants.Templates.CelestialCreature].Add(BuildData(CreatureConstants.Templates.CelestialCreature, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round, minHitDice: 8));
            testCases[CreatureConstants.Templates.CelestialCreature].Add(BuildData(CreatureConstants.Templates.CelestialCreature, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit, minHitDice: 4, maxHitDice: 11));
            testCases[CreatureConstants.Templates.CelestialCreature].Add(BuildData(CreatureConstants.Templates.CelestialCreature, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit, minHitDice: 12));

            testCases[CreatureConstants.Templates.FiendishCreature].Add(BuildData(CreatureConstants.Templates.FiendishCreature, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Templates.FiendishCreature].Add(BuildData(CreatureConstants.Templates.FiendishCreature, FeatConstants.SpecialQualities.SpellResistance, power: 6, maxHitDice: 1));
            testCases[CreatureConstants.Templates.FiendishCreature].Add(BuildData(CreatureConstants.Templates.FiendishCreature, FeatConstants.SpecialQualities.SpellResistance, power: 7, minHitDice: 2, maxHitDice: 2));
            testCases[CreatureConstants.Templates.FiendishCreature].Add(BuildData(CreatureConstants.Templates.FiendishCreature, FeatConstants.SpecialQualities.SpellResistance, power: 8, minHitDice: 3, maxHitDice: 3));
            testCases[CreatureConstants.Templates.FiendishCreature].Add(BuildData(CreatureConstants.Templates.FiendishCreature, FeatConstants.SpecialQualities.SpellResistance, power: 9, minHitDice: 4, maxHitDice: 4));
            testCases[CreatureConstants.Templates.FiendishCreature].Add(BuildData(CreatureConstants.Templates.FiendishCreature, FeatConstants.SpecialQualities.SpellResistance, power: 10, minHitDice: 5, maxHitDice: 5));
            testCases[CreatureConstants.Templates.FiendishCreature].Add(BuildData(CreatureConstants.Templates.FiendishCreature, FeatConstants.SpecialQualities.SpellResistance, power: 11, minHitDice: 6, maxHitDice: 6));
            testCases[CreatureConstants.Templates.FiendishCreature].Add(BuildData(CreatureConstants.Templates.FiendishCreature, FeatConstants.SpecialQualities.SpellResistance, power: 12, minHitDice: 7, maxHitDice: 7));
            testCases[CreatureConstants.Templates.FiendishCreature].Add(BuildData(CreatureConstants.Templates.FiendishCreature, FeatConstants.SpecialQualities.SpellResistance, power: 13, minHitDice: 8, maxHitDice: 8));
            testCases[CreatureConstants.Templates.FiendishCreature].Add(BuildData(CreatureConstants.Templates.FiendishCreature, FeatConstants.SpecialQualities.SpellResistance, power: 14, minHitDice: 9, maxHitDice: 9));
            testCases[CreatureConstants.Templates.FiendishCreature].Add(BuildData(CreatureConstants.Templates.FiendishCreature, FeatConstants.SpecialQualities.SpellResistance, power: 15, minHitDice: 10, maxHitDice: 10));
            testCases[CreatureConstants.Templates.FiendishCreature].Add(BuildData(CreatureConstants.Templates.FiendishCreature, FeatConstants.SpecialQualities.SpellResistance, power: 16, minHitDice: 11, maxHitDice: 11));
            testCases[CreatureConstants.Templates.FiendishCreature].Add(BuildData(CreatureConstants.Templates.FiendishCreature, FeatConstants.SpecialQualities.SpellResistance, power: 17, minHitDice: 12, maxHitDice: 12));
            testCases[CreatureConstants.Templates.FiendishCreature].Add(BuildData(CreatureConstants.Templates.FiendishCreature, FeatConstants.SpecialQualities.SpellResistance, power: 18, minHitDice: 13, maxHitDice: 13));
            testCases[CreatureConstants.Templates.FiendishCreature].Add(BuildData(CreatureConstants.Templates.FiendishCreature, FeatConstants.SpecialQualities.SpellResistance, power: 19, minHitDice: 14, maxHitDice: 14));
            testCases[CreatureConstants.Templates.FiendishCreature].Add(BuildData(CreatureConstants.Templates.FiendishCreature, FeatConstants.SpecialQualities.SpellResistance, power: 20, minHitDice: 15, maxHitDice: 15));
            testCases[CreatureConstants.Templates.FiendishCreature].Add(BuildData(CreatureConstants.Templates.FiendishCreature, FeatConstants.SpecialQualities.SpellResistance, power: 21, minHitDice: 16, maxHitDice: 16));
            testCases[CreatureConstants.Templates.FiendishCreature].Add(BuildData(CreatureConstants.Templates.FiendishCreature, FeatConstants.SpecialQualities.SpellResistance, power: 22, minHitDice: 17, maxHitDice: 17));
            testCases[CreatureConstants.Templates.FiendishCreature].Add(BuildData(CreatureConstants.Templates.FiendishCreature, FeatConstants.SpecialQualities.SpellResistance, power: 23, minHitDice: 18, maxHitDice: 18));
            testCases[CreatureConstants.Templates.FiendishCreature].Add(BuildData(CreatureConstants.Templates.FiendishCreature, FeatConstants.SpecialQualities.SpellResistance, power: 24, minHitDice: 19, maxHitDice: 19));
            testCases[CreatureConstants.Templates.FiendishCreature].Add(BuildData(CreatureConstants.Templates.FiendishCreature, FeatConstants.SpecialQualities.SpellResistance, power: 25, minHitDice: 20));
            testCases[CreatureConstants.Templates.FiendishCreature].Add(BuildData(CreatureConstants.Templates.FiendishCreature, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round, maxHitDice: 7));
            testCases[CreatureConstants.Templates.FiendishCreature].Add(BuildData(CreatureConstants.Templates.FiendishCreature, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round, minHitDice: 8));
            testCases[CreatureConstants.Templates.FiendishCreature].Add(BuildData(CreatureConstants.Templates.FiendishCreature, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round, maxHitDice: 7));
            testCases[CreatureConstants.Templates.FiendishCreature].Add(BuildData(CreatureConstants.Templates.FiendishCreature, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round, minHitDice: 8));
            testCases[CreatureConstants.Templates.FiendishCreature].Add(BuildData(CreatureConstants.Templates.FiendishCreature, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit, minHitDice: 4, maxHitDice: 11));
            testCases[CreatureConstants.Templates.FiendishCreature].Add(BuildData(CreatureConstants.Templates.FiendishCreature, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit, minHitDice: 12));

            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.Immunity, focus: "Disease"));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellResistance, power: 11, maxHitDice: 1));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellResistance, power: 12, minHitDice: 2, maxHitDice: 2));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellResistance, power: 13, minHitDice: 3, maxHitDice: 3));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellResistance, power: 14, minHitDice: 4, maxHitDice: 4));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellResistance, power: 15, minHitDice: 5, maxHitDice: 5));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellResistance, power: 16, minHitDice: 6, maxHitDice: 6));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellResistance, power: 17, minHitDice: 7, maxHitDice: 7));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellResistance, power: 18, minHitDice: 8, maxHitDice: 8));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellResistance, power: 19, minHitDice: 9, maxHitDice: 9));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellResistance, power: 20, minHitDice: 10, maxHitDice: 10));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellResistance, power: 21, minHitDice: 11, maxHitDice: 11));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellResistance, power: 22, minHitDice: 12, maxHitDice: 12));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellResistance, power: 23, minHitDice: 13, maxHitDice: 13));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellResistance, power: 24, minHitDice: 14, maxHitDice: 14));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellResistance, power: 25, minHitDice: 15, maxHitDice: 15));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellResistance, power: 26, minHitDice: 16, maxHitDice: 16));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellResistance, power: 27, minHitDice: 17, maxHitDice: 17));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellResistance, power: 28, minHitDice: 18, maxHitDice: 18));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellResistance, power: 29, minHitDice: 19, maxHitDice: 19));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellResistance, power: 30, minHitDice: 20, maxHitDice: 20));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellResistance, power: 31, minHitDice: 21, maxHitDice: 21));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellResistance, power: 32, minHitDice: 22, maxHitDice: 22));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellResistance, power: 33, minHitDice: 23, maxHitDice: 23));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellResistance, power: 34, minHitDice: 24, maxHitDice: 24));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellResistance, power: 35, minHitDice: 25));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit, maxHitDice: 11));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit, minHitDice: 12));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Daylight, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ProtectionFromEvil, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, minHitDice: 1));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, minHitDice: 1));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Aid, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, minHitDice: 3));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectEvil, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, minHitDice: 3));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CureSeriousWounds, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, minHitDice: 5));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.NeutralizePoison, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, minHitDice: 5));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HolySmite, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, minHitDice: 7, save: SaveConstants.Will, saveAbility: AbilityConstants.Charisma, saveBaseValue: 14));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveDisease, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, minHitDice: 7));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelEvil, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, minHitDice: 9, save: SaveConstants.Will, saveAbility: AbilityConstants.Charisma, saveBaseValue: 15));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HolyWord, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, minHitDice: 11, save: SaveConstants.Will, saveAbility: AbilityConstants.Charisma, saveBaseValue: 17));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HolyAura, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, minHitDice: 13, save: SaveConstants.Will, saveAbility: AbilityConstants.Charisma, saveBaseValue: 18));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Hallow, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, minHitDice: 13));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster_Mass, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, minHitDice: 15, save: SaveConstants.Will, saveAbility: AbilityConstants.Charisma, saveBaseValue: 18));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SummonMonsterIX, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, minHitDice: 17));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(CreatureConstants.Templates.HalfCelestial, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Resurrection, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, minHitDice: 19));

            testCases[CreatureConstants.Templates.HalfDragon_Black].Add(BuildData(CreatureConstants.Templates.HalfDragon_Black, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Templates.HalfDragon_Black].Add(BuildData(CreatureConstants.Templates.HalfDragon_Black, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.HalfDragon_Black].Add(BuildData(CreatureConstants.Templates.HalfDragon_Black, FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
            testCases[CreatureConstants.Templates.HalfDragon_Black].Add(BuildData(CreatureConstants.Templates.HalfDragon_Black, FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
            testCases[CreatureConstants.Templates.HalfDragon_Black].Add(BuildData(CreatureConstants.Templates.HalfDragon_Black, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));

            testCases[CreatureConstants.Templates.HalfDragon_Blue].Add(BuildData(CreatureConstants.Templates.HalfDragon_Blue, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Templates.HalfDragon_Blue].Add(BuildData(CreatureConstants.Templates.HalfDragon_Blue, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.HalfDragon_Blue].Add(BuildData(CreatureConstants.Templates.HalfDragon_Blue, FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
            testCases[CreatureConstants.Templates.HalfDragon_Blue].Add(BuildData(CreatureConstants.Templates.HalfDragon_Blue, FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
            testCases[CreatureConstants.Templates.HalfDragon_Blue].Add(BuildData(CreatureConstants.Templates.HalfDragon_Blue, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));

            testCases[CreatureConstants.Templates.HalfDragon_Brass].Add(BuildData(CreatureConstants.Templates.HalfDragon_Brass, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Templates.HalfDragon_Brass].Add(BuildData(CreatureConstants.Templates.HalfDragon_Brass, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.HalfDragon_Brass].Add(BuildData(CreatureConstants.Templates.HalfDragon_Brass, FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
            testCases[CreatureConstants.Templates.HalfDragon_Brass].Add(BuildData(CreatureConstants.Templates.HalfDragon_Brass, FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
            testCases[CreatureConstants.Templates.HalfDragon_Brass].Add(BuildData(CreatureConstants.Templates.HalfDragon_Brass, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));

            testCases[CreatureConstants.Templates.HalfDragon_Bronze].Add(BuildData(CreatureConstants.Templates.HalfDragon_Bronze, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Templates.HalfDragon_Bronze].Add(BuildData(CreatureConstants.Templates.HalfDragon_Bronze, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.HalfDragon_Bronze].Add(BuildData(CreatureConstants.Templates.HalfDragon_Bronze, FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
            testCases[CreatureConstants.Templates.HalfDragon_Bronze].Add(BuildData(CreatureConstants.Templates.HalfDragon_Bronze, FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
            testCases[CreatureConstants.Templates.HalfDragon_Bronze].Add(BuildData(CreatureConstants.Templates.HalfDragon_Bronze, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));

            testCases[CreatureConstants.Templates.HalfDragon_Copper].Add(BuildData(CreatureConstants.Templates.HalfDragon_Copper, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Templates.HalfDragon_Copper].Add(BuildData(CreatureConstants.Templates.HalfDragon_Copper, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.HalfDragon_Copper].Add(BuildData(CreatureConstants.Templates.HalfDragon_Copper, FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
            testCases[CreatureConstants.Templates.HalfDragon_Copper].Add(BuildData(CreatureConstants.Templates.HalfDragon_Copper, FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
            testCases[CreatureConstants.Templates.HalfDragon_Copper].Add(BuildData(CreatureConstants.Templates.HalfDragon_Copper, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));

            testCases[CreatureConstants.Templates.HalfDragon_Gold].Add(BuildData(CreatureConstants.Templates.HalfDragon_Gold, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Templates.HalfDragon_Gold].Add(BuildData(CreatureConstants.Templates.HalfDragon_Gold, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.HalfDragon_Gold].Add(BuildData(CreatureConstants.Templates.HalfDragon_Gold, FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
            testCases[CreatureConstants.Templates.HalfDragon_Gold].Add(BuildData(CreatureConstants.Templates.HalfDragon_Gold, FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
            testCases[CreatureConstants.Templates.HalfDragon_Gold].Add(BuildData(CreatureConstants.Templates.HalfDragon_Gold, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));

            testCases[CreatureConstants.Templates.HalfDragon_Green].Add(BuildData(CreatureConstants.Templates.HalfDragon_Green, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Templates.HalfDragon_Green].Add(BuildData(CreatureConstants.Templates.HalfDragon_Green, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.HalfDragon_Green].Add(BuildData(CreatureConstants.Templates.HalfDragon_Green, FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
            testCases[CreatureConstants.Templates.HalfDragon_Green].Add(BuildData(CreatureConstants.Templates.HalfDragon_Green, FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
            testCases[CreatureConstants.Templates.HalfDragon_Green].Add(BuildData(CreatureConstants.Templates.HalfDragon_Green, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));

            testCases[CreatureConstants.Templates.HalfDragon_Red].Add(BuildData(CreatureConstants.Templates.HalfDragon_Red, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Templates.HalfDragon_Red].Add(BuildData(CreatureConstants.Templates.HalfDragon_Red, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.HalfDragon_Red].Add(BuildData(CreatureConstants.Templates.HalfDragon_Red, FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
            testCases[CreatureConstants.Templates.HalfDragon_Red].Add(BuildData(CreatureConstants.Templates.HalfDragon_Red, FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
            testCases[CreatureConstants.Templates.HalfDragon_Red].Add(BuildData(CreatureConstants.Templates.HalfDragon_Red, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));

            testCases[CreatureConstants.Templates.HalfDragon_Silver].Add(BuildData(CreatureConstants.Templates.HalfDragon_Silver, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Templates.HalfDragon_Silver].Add(BuildData(CreatureConstants.Templates.HalfDragon_Silver, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.HalfDragon_Silver].Add(BuildData(CreatureConstants.Templates.HalfDragon_Silver, FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
            testCases[CreatureConstants.Templates.HalfDragon_Silver].Add(BuildData(CreatureConstants.Templates.HalfDragon_Silver, FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
            testCases[CreatureConstants.Templates.HalfDragon_Silver].Add(BuildData(CreatureConstants.Templates.HalfDragon_Silver, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));

            testCases[CreatureConstants.Templates.HalfDragon_White].Add(BuildData(CreatureConstants.Templates.HalfDragon_White, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Templates.HalfDragon_White].Add(BuildData(CreatureConstants.Templates.HalfDragon_White, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.HalfDragon_White].Add(BuildData(CreatureConstants.Templates.HalfDragon_White, FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
            testCases[CreatureConstants.Templates.HalfDragon_White].Add(BuildData(CreatureConstants.Templates.HalfDragon_White, FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
            testCases[CreatureConstants.Templates.HalfDragon_White].Add(BuildData(CreatureConstants.Templates.HalfDragon_White, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));

            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellResistance, power: 11, maxHitDice: 1));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellResistance, power: 12, minHitDice: 2, maxHitDice: 2));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellResistance, power: 13, minHitDice: 3, maxHitDice: 3));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellResistance, power: 14, minHitDice: 4, maxHitDice: 4));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellResistance, power: 15, minHitDice: 5, maxHitDice: 5));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellResistance, power: 16, minHitDice: 6, maxHitDice: 6));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellResistance, power: 17, minHitDice: 7, maxHitDice: 7));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellResistance, power: 18, minHitDice: 8, maxHitDice: 8));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellResistance, power: 19, minHitDice: 9, maxHitDice: 9));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellResistance, power: 20, minHitDice: 10, maxHitDice: 10));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellResistance, power: 21, minHitDice: 11, maxHitDice: 11));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellResistance, power: 22, minHitDice: 12, maxHitDice: 12));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellResistance, power: 23, minHitDice: 13, maxHitDice: 13));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellResistance, power: 24, minHitDice: 14, maxHitDice: 14));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellResistance, power: 25, minHitDice: 15, maxHitDice: 15));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellResistance, power: 26, minHitDice: 16, maxHitDice: 16));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellResistance, power: 27, minHitDice: 17, maxHitDice: 17));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellResistance, power: 28, minHitDice: 18, maxHitDice: 18));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellResistance, power: 29, minHitDice: 19, maxHitDice: 19));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellResistance, power: 30, minHitDice: 20, maxHitDice: 20));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellResistance, power: 31, minHitDice: 21, maxHitDice: 21));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellResistance, power: 32, minHitDice: 22, maxHitDice: 22));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellResistance, power: 33, minHitDice: 23, maxHitDice: 23));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellResistance, power: 34, minHitDice: 24, maxHitDice: 24));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellResistance, power: 35, minHitDice: 25));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit, maxHitDice: 11));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit, minHitDice: 12));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, minHitDice: 1));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Desecrate, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, minHitDice: 3));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyBlight, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, minHitDice: 5, save: SaveConstants.Will, saveAbility: AbilityConstants.Charisma, saveBaseValue: 14));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Poison, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, minHitDice: 7, save: SaveConstants.Fortitude, saveAbility: AbilityConstants.Charisma, saveBaseValue: 14));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Contagion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, minHitDice: 9, save: SaveConstants.Fortitude, saveAbility: AbilityConstants.Charisma, saveBaseValue: 13));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blasphemy, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, minHitDice: 11, save: SaveConstants.Will, saveAbility: AbilityConstants.Charisma, saveBaseValue: 17));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyAura, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, minHitDice: 13, save: SaveConstants.Will, saveAbility: AbilityConstants.Charisma, saveBaseValue: 18));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Unhallow, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, minHitDice: 13));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HorridWilting, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, minHitDice: 15, save: SaveConstants.Fortitude, saveAbility: AbilityConstants.Charisma, saveBaseValue: 18));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SummonMonsterIX, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, minHitDice: 17));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(CreatureConstants.Templates.HalfFiend, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Destruction, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, minHitDice: 19, save: SaveConstants.Fortitude, saveAbility: AbilityConstants.Charisma, saveBaseValue: 17));

            testCases[CreatureConstants.Templates.Ghost].Add(BuildData(CreatureConstants.Templates.Ghost, FeatConstants.SpecialQualities.Rejuvenation, frequencyQuantity: 1, frequencyTimePeriod: $"2d4 {FeatConstants.Frequencies.Day}"));
            testCases[CreatureConstants.Templates.Ghost].Add(BuildData(CreatureConstants.Templates.Ghost, FeatConstants.SpecialQualities.TurnResistance, power: 4));

            testCases[CreatureConstants.Templates.Lich].Add(BuildData(CreatureConstants.Templates.Lich, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to bludgeoning magic", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Templates.Lich].Add(BuildData(CreatureConstants.Templates.Lich, FeatConstants.SpecialQualities.TurnResistance, power: 4));
            testCases[CreatureConstants.Templates.Lich].Add(BuildData(CreatureConstants.Templates.Lich, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
            testCases[CreatureConstants.Templates.Lich].Add(BuildData(CreatureConstants.Templates.Lich, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Templates.Lich].Add(BuildData(CreatureConstants.Templates.Lich, FeatConstants.SpecialQualities.Immunity, focus: "Polymorph"));
            testCases[CreatureConstants.Templates.Lich].Add(BuildData(CreatureConstants.Templates.Lich, FeatConstants.SpecialQualities.Immunity, focus: "Mind-Affecting Attacks"));
            testCases[CreatureConstants.Templates.Lich].Add(BuildData(CreatureConstants.Templates.Lich, FeatConstants.MagicItemCreation.CraftWondrousItem));

            testCases[CreatureConstants.Templates.None] = [];

            testCases[CreatureConstants.Templates.Skeleton].Add(BuildData(CreatureConstants.Templates.Skeleton, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to bludgeoning", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Templates.Skeleton].Add(BuildData(CreatureConstants.Templates.Skeleton, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
            testCases[CreatureConstants.Templates.Skeleton].Add(BuildData(CreatureConstants.Templates.Skeleton, FeatConstants.Initiative_Improved, power: 4));

            testCases[CreatureConstants.Templates.Vampire].Add(BuildData(CreatureConstants.Templates.Vampire, FeatConstants.SpecialQualities.AlternateForm, focus: "Bat, dire bat, wolf, or dire wolf", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.Vampire].Add(BuildData(CreatureConstants.Templates.Vampire, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to silver magic", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Templates.Vampire].Add(BuildData(CreatureConstants.Templates.Vampire, FeatConstants.SpecialQualities.TurnResistance, power: 4));
            testCases[CreatureConstants.Templates.Vampire].Add(BuildData(CreatureConstants.Templates.Vampire, FeatConstants.SpecialQualities.EnergyResistance, power: 10, focus: FeatConstants.Foci.Elements.Cold, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.Vampire].Add(BuildData(CreatureConstants.Templates.Vampire, FeatConstants.SpecialQualities.EnergyResistance, power: 10, focus: FeatConstants.Foci.Elements.Electricity, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.Vampire].Add(BuildData(CreatureConstants.Templates.Vampire, FeatConstants.SpecialQualities.FastHealing, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.Vampire].Add(BuildData(CreatureConstants.Templates.Vampire, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GaseousForm, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.Vampire].Add(BuildData(CreatureConstants.Templates.Vampire, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
            testCases[CreatureConstants.Templates.Vampire].Add(BuildData(CreatureConstants.Templates.Vampire, FeatConstants.Alertness, power: 2));
            testCases[CreatureConstants.Templates.Vampire].Add(BuildData(CreatureConstants.Templates.Vampire, FeatConstants.Initiative_Improved, power: 4));
            testCases[CreatureConstants.Templates.Vampire].Add(BuildData(CreatureConstants.Templates.Vampire, FeatConstants.LightningReflexes, power: 2));
            testCases[CreatureConstants.Templates.Vampire].Add(BuildData(CreatureConstants.Templates.Vampire, FeatConstants.CombatReflexes));
            testCases[CreatureConstants.Templates.Vampire].Add(BuildData(CreatureConstants.Templates.Vampire, FeatConstants.Dodge, power: 1));

            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Black_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Black_Afflicted, FeatConstants.SpecialQualities.AlternateForm, focus: "Black bear or bear-humanoid hybrid", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Black_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Black_Afflicted, FeatConstants.SpecialQualities.LycanthropicEmpathy, focus: "Bears and dire bears", power: 4));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Black_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Black_Afflicted, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Black_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Black_Afflicted, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Black_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Black_Afflicted, FeatConstants.SpecialQualities.DamageReduction + ": in Animal or Hybrid form", focus: "Vulnerable to silver", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Black_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Black_Afflicted, FeatConstants.IronWill, power: 2));

            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted, FeatConstants.SpecialQualities.AlternateForm, focus: "Brown bear or bear-humanoid hybrid", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted, FeatConstants.SpecialQualities.LycanthropicEmpathy, focus: "Bears and dire bears", power: 4));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted, FeatConstants.SpecialQualities.DamageReduction + ": in Animal or Hybrid form", focus: "Vulnerable to silver", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted, FeatConstants.IronWill, power: 2));

            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Dire_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Dire_Afflicted, FeatConstants.SpecialQualities.AlternateForm, focus: "Dire bear or bear-humanoid hybrid", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Dire_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Dire_Afflicted, FeatConstants.SpecialQualities.LycanthropicEmpathy, focus: "Bears and dire bears", power: 4));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Dire_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Dire_Afflicted, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Dire_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Dire_Afflicted, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Dire_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Dire_Afflicted, FeatConstants.SpecialQualities.DamageReduction + ": in Animal or Hybrid form", focus: "Vulnerable to silver", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Dire_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Dire_Afflicted, FeatConstants.IronWill, power: 2));

            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Polar_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Polar_Afflicted, FeatConstants.SpecialQualities.AlternateForm, focus: "Polar bear or bear-humanoid hybrid", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Polar_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Polar_Afflicted, FeatConstants.SpecialQualities.LycanthropicEmpathy, focus: "Bears and dire bears", power: 4));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Polar_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Polar_Afflicted, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Polar_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Polar_Afflicted, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Polar_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Polar_Afflicted, FeatConstants.SpecialQualities.DamageReduction + ": in Animal or Hybrid form", focus: "Vulnerable to silver", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Polar_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Polar_Afflicted, FeatConstants.IronWill, power: 2));

            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Boar_Afflicted, FeatConstants.SpecialQualities.AlternateForm, focus: "Boar or boar-humanoid hybrid", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Boar_Afflicted, FeatConstants.SpecialQualities.LycanthropicEmpathy, focus: "Boars and dire boars", power: 4));
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Boar_Afflicted, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Boar_Afflicted, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Boar_Afflicted, FeatConstants.SpecialQualities.DamageReduction + ": in Animal or Hybrid form", focus: "Vulnerable to silver", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Boar_Afflicted, FeatConstants.IronWill, power: 2));

            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted, FeatConstants.SpecialQualities.AlternateForm, focus: "Dire Boar or boar-humanoid hybrid", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted, FeatConstants.SpecialQualities.LycanthropicEmpathy, focus: "Boars and dire boars", power: 4));
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted, FeatConstants.SpecialQualities.DamageReduction + ": in Animal or Hybrid form", focus: "Vulnerable to silver", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted, FeatConstants.IronWill, power: 2));

            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Rat_Afflicted, FeatConstants.SpecialQualities.AlternateForm, focus: "Rat or bipedal rat-humanoid hybrid", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Rat_Afflicted, FeatConstants.SpecialQualities.LycanthropicEmpathy, focus: "Rats and dire rats", power: 4));
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Rat_Afflicted, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Rat_Afflicted, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Rat_Afflicted, FeatConstants.SpecialQualities.DamageReduction + ": in Animal or Hybrid form", focus: "Vulnerable to silver", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Rat_Afflicted, FeatConstants.IronWill, power: 2));

            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted, FeatConstants.SpecialQualities.AlternateForm, focus: "Dire rat or bipedal rat-humanoid hybrid", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted, FeatConstants.SpecialQualities.LycanthropicEmpathy, focus: "Rats and dire rats", power: 4));
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted, FeatConstants.SpecialQualities.DamageReduction + ": in Animal or Hybrid form", focus: "Vulnerable to silver", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted, FeatConstants.IronWill, power: 2));

            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted, FeatConstants.SpecialQualities.AlternateForm, focus: "Tiger or bipedal tiger-humanoid hybrid", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted, FeatConstants.SpecialQualities.LycanthropicEmpathy, focus: "Tigers and dire tigers", power: 4));
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted, FeatConstants.SpecialQualities.DamageReduction + ": in Animal or Hybrid form", focus: "Vulnerable to silver", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted, FeatConstants.IronWill, power: 2));

            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Afflicted, FeatConstants.SpecialQualities.AlternateForm, focus: "Dire Tiger or bipedal tiger-humanoid hybrid", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Afflicted, FeatConstants.SpecialQualities.LycanthropicEmpathy, focus: "Tigers and dire tigers", power: 4));
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Afflicted, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Afflicted, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Afflicted, FeatConstants.SpecialQualities.DamageReduction + ": in Animal or Hybrid form", focus: "Vulnerable to silver", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Afflicted, FeatConstants.IronWill, power: 2));

            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted, FeatConstants.SpecialQualities.AlternateForm, focus: "Wolf or wolf-humanoid hybrid", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted, FeatConstants.SpecialQualities.LycanthropicEmpathy, focus: "Wolves and dire wolves", power: 4));
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted, FeatConstants.SpecialQualities.DamageReduction + ": in Animal or Hybrid form", focus: "Vulnerable to silver", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted, FeatConstants.IronWill, power: 2));

            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Black_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Black_Natural, FeatConstants.SpecialQualities.AlternateForm, focus: "Black bear or bear-humanoid hybrid", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Black_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Black_Natural, FeatConstants.SpecialQualities.LycanthropicEmpathy, focus: "Bears and dire bears", power: 4));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Black_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Black_Natural, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Black_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Black_Natural, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Black_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Black_Natural, FeatConstants.SpecialQualities.DamageReduction + ": in Animal or Hybrid form", focus: "Vulnerable to silver", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Black_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Black_Natural, FeatConstants.IronWill, power: 2));

            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural, FeatConstants.SpecialQualities.AlternateForm, focus: "Brown bear or bear-humanoid hybrid", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural, FeatConstants.SpecialQualities.LycanthropicEmpathy, focus: "Bears and dire bears", power: 4));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural, FeatConstants.SpecialQualities.DamageReduction + ": in Animal or Hybrid form", focus: "Vulnerable to silver", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural, FeatConstants.IronWill, power: 2));

            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Dire_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Dire_Natural, FeatConstants.SpecialQualities.AlternateForm, focus: "Dire bear or bear-humanoid hybrid", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Dire_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Dire_Natural, FeatConstants.SpecialQualities.LycanthropicEmpathy, focus: "Bears and dire bears", power: 4));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Dire_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Dire_Natural, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Dire_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Dire_Natural, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Dire_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Dire_Natural, FeatConstants.SpecialQualities.DamageReduction + ": in Animal or Hybrid form", focus: "Vulnerable to silver", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Dire_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Dire_Natural, FeatConstants.IronWill, power: 2));

            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Polar_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Polar_Natural, FeatConstants.SpecialQualities.AlternateForm, focus: "Polar bear or bear-humanoid hybrid", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Polar_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Polar_Natural, FeatConstants.SpecialQualities.LycanthropicEmpathy, focus: "Bears and dire bears", power: 4));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Polar_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Polar_Natural, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Polar_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Polar_Natural, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Polar_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Polar_Natural, FeatConstants.SpecialQualities.DamageReduction + ": in Animal or Hybrid form", focus: "Vulnerable to silver", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Polar_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Polar_Natural, FeatConstants.IronWill, power: 2));

            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Boar_Natural, FeatConstants.SpecialQualities.AlternateForm, focus: "Boar or boar-humanoid hybrid", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Boar_Natural, FeatConstants.SpecialQualities.LycanthropicEmpathy, focus: "Boars and dire boars", power: 4));
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Boar_Natural, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Boar_Natural, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Boar_Natural, FeatConstants.SpecialQualities.DamageReduction + ": in Animal or Hybrid form", focus: "Vulnerable to silver", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Boar_Natural, FeatConstants.IronWill, power: 2));

            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural, FeatConstants.SpecialQualities.AlternateForm, focus: "Dire Boar or boar-humanoid hybrid", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural, FeatConstants.SpecialQualities.LycanthropicEmpathy, focus: "Boars and dire boars", power: 4));
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural, FeatConstants.SpecialQualities.DamageReduction + ": in Animal or Hybrid form", focus: "Vulnerable to silver", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural, FeatConstants.IronWill, power: 2));

            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Rat_Natural, FeatConstants.SpecialQualities.AlternateForm, focus: "Rat or bipedal rat-humanoid hybrid", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Rat_Natural, FeatConstants.SpecialQualities.LycanthropicEmpathy, focus: "Rats and dire rats", power: 4));
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Rat_Natural, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Rat_Natural, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Rat_Natural, FeatConstants.SpecialQualities.DamageReduction + ": in Animal or Hybrid form", focus: "Vulnerable to silver", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Rat_Natural, FeatConstants.IronWill, power: 2));

            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural, FeatConstants.SpecialQualities.AlternateForm, focus: "Dire rat or bipedal rat-humanoid hybrid", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural, FeatConstants.SpecialQualities.LycanthropicEmpathy, focus: "Rats and dire rats", power: 4));
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural, FeatConstants.SpecialQualities.DamageReduction + ": in Animal or Hybrid form", focus: "Vulnerable to silver", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural, FeatConstants.IronWill, power: 2));

            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Tiger_Natural, FeatConstants.SpecialQualities.AlternateForm, focus: "Tiger or bipedal tiger-humanoid hybrid", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Tiger_Natural, FeatConstants.SpecialQualities.LycanthropicEmpathy, focus: "Tigers and dire tigers", power: 4));
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Tiger_Natural, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Tiger_Natural, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Tiger_Natural, FeatConstants.SpecialQualities.DamageReduction + ": in Animal or Hybrid form", focus: "Vulnerable to silver", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Tiger_Natural, FeatConstants.IronWill, power: 2));

            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Natural, FeatConstants.SpecialQualities.AlternateForm, focus: "Dire Tiger or bipedal tiger-humanoid hybrid", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Natural, FeatConstants.SpecialQualities.LycanthropicEmpathy, focus: "Tigers and dire tigers", power: 4));
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Natural, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Natural, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Natural, FeatConstants.SpecialQualities.DamageReduction + ": in Animal or Hybrid form", focus: "Vulnerable to silver", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Natural, FeatConstants.IronWill, power: 2));

            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Wolf_Natural, FeatConstants.SpecialQualities.AlternateForm, focus: "Wolf or wolf-humanoid hybrid", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Wolf_Natural, FeatConstants.SpecialQualities.LycanthropicEmpathy, focus: "Wolves and dire wolves", power: 4));
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Wolf_Natural, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Wolf_Natural, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Wolf_Natural, FeatConstants.SpecialQualities.DamageReduction + ": in Animal or Hybrid form", focus: "Vulnerable to silver", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Wolf_Natural, FeatConstants.IronWill, power: 2));

            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted, FeatConstants.SpecialQualities.AlternateForm, focus: "Dire Wolf or wolf-humanoid hybrid", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted, FeatConstants.SpecialQualities.LycanthropicEmpathy, focus: "Wolves and dire wolves", power: 4));
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted, FeatConstants.SpecialQualities.DamageReduction + ": in Animal or Hybrid form", focus: "Vulnerable to silver", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted, FeatConstants.IronWill, power: 2));

            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural, FeatConstants.SpecialQualities.AlternateForm, focus: "Dire Wolf or wolf-humanoid hybrid", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural, FeatConstants.SpecialQualities.LycanthropicEmpathy, focus: "Wolves and dire wolves", power: 4));
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural, FeatConstants.SpecialQualities.DamageReduction + ": in Animal or Hybrid form", focus: "Vulnerable to silver", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural].Add(BuildData(CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural, FeatConstants.IronWill, power: 2));

            testCases[CreatureConstants.Templates.Zombie].Add(BuildData(CreatureConstants.Templates.Zombie, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to slashing", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Templates.Zombie].Add(BuildData(CreatureConstants.Templates.Zombie, FeatConstants.SpecialQualities.SingleActionsOnly));
            testCases[CreatureConstants.Templates.Zombie].Add(BuildData(CreatureConstants.Templates.Zombie, FeatConstants.Toughness, power: 3));

            return testCases;
        }

        public static Dictionary<string, List<string>> GetCreatureData()
        {
            var testCases = new Dictionary<string, List<string>>();
            foreach (var creature in CreatureConstants.GetAll())
            {
                testCases[creature] = [];
            }

            testCases[CreatureConstants.Aasimar].Add(BuildData(CreatureConstants.Aasimar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Daylight, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Aasimar].Add(BuildData(CreatureConstants.Aasimar, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Aasimar].Add(BuildData(CreatureConstants.Aasimar, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Aasimar].Add(BuildData(CreatureConstants.Aasimar, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Aasimar].Add(BuildData(CreatureConstants.Aasimar, FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
            testCases[CreatureConstants.Aasimar].Add(BuildData(CreatureConstants.Aasimar, FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));

            testCases[CreatureConstants.Aboleth].Add(BuildData(CreatureConstants.Aboleth, FeatConstants.SpecialQualities.MucusCloud, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveBaseValue: 14));
            testCases[CreatureConstants.Aboleth].Add(BuildData(CreatureConstants.Aboleth, FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.HypnoticPattern, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.Aboleth].Add(BuildData(CreatureConstants.Aboleth, FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.IllusoryWall, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Aboleth].Add(BuildData(CreatureConstants.Aboleth, FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.MirageArcana, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
            testCases[CreatureConstants.Aboleth].Add(BuildData(CreatureConstants.Aboleth, FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.PersistentImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
            testCases[CreatureConstants.Aboleth].Add(BuildData(CreatureConstants.Aboleth, FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.ProgrammedImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 16));
            testCases[CreatureConstants.Aboleth].Add(BuildData(CreatureConstants.Aboleth, FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.ProjectImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));
            testCases[CreatureConstants.Aboleth].Add(BuildData(CreatureConstants.Aboleth, FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.Veil, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 16));

            testCases[CreatureConstants.Achaierai].Add(BuildData(CreatureConstants.Achaierai, FeatConstants.SpecialQualities.SpellResistance, power: 19));

            testCases[CreatureConstants.Allip].Add(BuildData(CreatureConstants.Allip, FeatConstants.SpecialQualities.TurnResistance, power: 2));

            testCases[CreatureConstants.Androsphinx] = [];

            testCases[CreatureConstants.Angel_AstralDeva].Add(BuildData(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.ChangeShape, focus: "Small or Medium Humanoid", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Angel_AstralDeva].Add(BuildData(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to evil", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Angel_AstralDeva].Add(BuildData(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellResistance, power: 30));
            testCases[CreatureConstants.Angel_AstralDeva].Add(BuildData(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.UncannyDodge));
            testCases[CreatureConstants.Angel_AstralDeva].Add(BuildData(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Aid, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Angel_AstralDeva].Add(BuildData(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ContinualFlame, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Angel_AstralDeva].Add(BuildData(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectEvil, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Angel_AstralDeva].Add(BuildData(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DiscernLies, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Angel_AstralDeva].Add(BuildData(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelEvil, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
            testCases[CreatureConstants.Angel_AstralDeva].Add(BuildData(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Angel_AstralDeva].Add(BuildData(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HolyAura, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 18));
            testCases[CreatureConstants.Angel_AstralDeva].Add(BuildData(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HolySmite, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Angel_AstralDeva].Add(BuildData(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HolyWord, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));
            testCases[CreatureConstants.Angel_AstralDeva].Add(BuildData(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + " (self only)", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Angel_AstralDeva].Add(BuildData(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlaneShift, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));
            testCases[CreatureConstants.Angel_AstralDeva].Add(BuildData(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveCurse, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Angel_AstralDeva].Add(BuildData(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveDisease, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 13));
            testCases[CreatureConstants.Angel_AstralDeva].Add(BuildData(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveFear, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
            testCases[CreatureConstants.Angel_AstralDeva].Add(BuildData(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CureLightWounds, frequencyQuantity: 7, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
            testCases[CreatureConstants.Angel_AstralDeva].Add(BuildData(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyQuantity: 7, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Angel_AstralDeva].Add(BuildData(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.BladeBarrier, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 16));
            testCases[CreatureConstants.Angel_AstralDeva].Add(BuildData(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Heal, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 16));
            testCases[CreatureConstants.Angel_AstralDeva].Add(BuildData(CreatureConstants.Angel_AstralDeva, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.HeavyMace, requiresEquipment: true));

            testCases[CreatureConstants.Angel_Planetar].Add(BuildData(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.ChangeShape, focus: "Small or Medium Humanoid", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Angel_Planetar].Add(BuildData(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to evil", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Angel_Planetar].Add(BuildData(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.Regeneration, focus: "Does not regenerate evil damage", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Angel_Planetar].Add(BuildData(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellResistance, power: 30));
            testCases[CreatureConstants.Angel_Planetar].Add(BuildData(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ContinualFlame, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Angel_Planetar].Add(BuildData(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Angel_Planetar].Add(BuildData(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HolySmite, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Angel_Planetar].Add(BuildData(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + " (self only)", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Angel_Planetar].Add(BuildData(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Restoration_Lesser, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.Angel_Planetar].Add(BuildData(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveCurse, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Angel_Planetar].Add(BuildData(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveDisease, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 13));
            testCases[CreatureConstants.Angel_Planetar].Add(BuildData(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveFear, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
            testCases[CreatureConstants.Angel_Planetar].Add(BuildData(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithDead, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Angel_Planetar].Add(BuildData(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.BladeBarrier, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 16));
            testCases[CreatureConstants.Angel_Planetar].Add(BuildData(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FlameStrike, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
            testCases[CreatureConstants.Angel_Planetar].Add(BuildData(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PowerWordStun, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Angel_Planetar].Add(BuildData(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RaiseDead, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Angel_Planetar].Add(BuildData(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WavesOfFatigue, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Angel_Planetar].Add(BuildData(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Earthquake, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 18));
            testCases[CreatureConstants.Angel_Planetar].Add(BuildData(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Restoration_Greater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));
            testCases[CreatureConstants.Angel_Planetar].Add(BuildData(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster_Mass, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 18));
            testCases[CreatureConstants.Angel_Planetar].Add(BuildData(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WavesOfExhaustion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Angel_Planetar].Add(BuildData(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectEvil, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
            testCases[CreatureConstants.Angel_Planetar].Add(BuildData(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectSnaresAndPits, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
            testCases[CreatureConstants.Angel_Planetar].Add(BuildData(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DiscernLies, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
            testCases[CreatureConstants.Angel_Planetar].Add(BuildData(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
            testCases[CreatureConstants.Angel_Planetar].Add(BuildData(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TrueSeeing, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
            testCases[CreatureConstants.Angel_Planetar].Add(BuildData(CreatureConstants.Angel_Planetar, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatsword, requiresEquipment: true));

            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.ChangeShape, focus: "Small or Medium Humanoid", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to epic evil", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.Regeneration, focus: "Does not regenerate evil damage", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellResistance, power: 32));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Aid, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AnimateObjects, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Commune, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ContinualFlame, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DimensionalAnchor, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HolySmite, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Imprisonment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 19));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + " (self only)", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Restoration_Lesser, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveCurse, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveDisease, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 13));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveFear, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ResistEnergy, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SummonMonsterVII, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithDead, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WavesOfFatigue, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.BladeBarrier, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 16));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Earthquake, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 18));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Heal, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 16));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster_Mass, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 18));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Permanency, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Resurrection, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WavesOfExhaustion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Restoration_Greater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PowerWordBlind, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PowerWordKill, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PowerWordStun, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PrismaticSpray, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, saveBaseValue: 17));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Wish, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectEvil, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectSnaresAndPits, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DiscernLies, frequencyTimePeriod: FeatConstants.Frequencies.Constant, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TrueSeeing, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatsword, requiresEquipment: true));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(CreatureConstants.Angel_Solar, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));

            testCases[CreatureConstants.AnimatedObject_Colossal] = [];
            testCases[CreatureConstants.AnimatedObject_Colossal_Flexible] = [];
            testCases[CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Long] = [];
            testCases[CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Long_Wooden] = [];
            testCases[CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Tall] = [];
            testCases[CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Tall_Wooden] = [];
            testCases[CreatureConstants.AnimatedObject_Colossal_Sheetlike] = [];
            testCases[CreatureConstants.AnimatedObject_Colossal_TwoLegs] = [];
            testCases[CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden] = [];
            testCases[CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden] = [];
            testCases[CreatureConstants.AnimatedObject_Colossal_Wooden] = [];
            testCases[CreatureConstants.AnimatedObject_Gargantuan] = [];
            testCases[CreatureConstants.AnimatedObject_Gargantuan_Flexible] = [];
            testCases[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Long] = [];
            testCases[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Long_Wooden] = [];
            testCases[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Tall] = [];
            testCases[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Tall_Wooden] = [];
            testCases[CreatureConstants.AnimatedObject_Gargantuan_Sheetlike] = [];
            testCases[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs] = [];
            testCases[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs_Wooden] = [];
            testCases[CreatureConstants.AnimatedObject_Gargantuan_Wheels_Wooden] = [];
            testCases[CreatureConstants.AnimatedObject_Gargantuan_Wooden] = [];
            testCases[CreatureConstants.AnimatedObject_Huge] = [];
            testCases[CreatureConstants.AnimatedObject_Huge_Flexible] = [];
            testCases[CreatureConstants.AnimatedObject_Huge_MultipleLegs_Long] = [];
            testCases[CreatureConstants.AnimatedObject_Huge_MultipleLegs_Long_Wooden] = [];
            testCases[CreatureConstants.AnimatedObject_Huge_MultipleLegs_Tall] = [];
            testCases[CreatureConstants.AnimatedObject_Huge_MultipleLegs_Tall_Wooden] = [];
            testCases[CreatureConstants.AnimatedObject_Huge_Sheetlike] = [];
            testCases[CreatureConstants.AnimatedObject_Huge_TwoLegs] = [];
            testCases[CreatureConstants.AnimatedObject_Huge_TwoLegs_Wooden] = [];
            testCases[CreatureConstants.AnimatedObject_Huge_Wheels_Wooden] = [];
            testCases[CreatureConstants.AnimatedObject_Huge_Wooden] = [];
            testCases[CreatureConstants.AnimatedObject_Large] = [];
            testCases[CreatureConstants.AnimatedObject_Large_Flexible] = [];
            testCases[CreatureConstants.AnimatedObject_Large_MultipleLegs_Long] = [];
            testCases[CreatureConstants.AnimatedObject_Large_MultipleLegs_Long_Wooden] = [];
            testCases[CreatureConstants.AnimatedObject_Large_MultipleLegs_Tall] = [];
            testCases[CreatureConstants.AnimatedObject_Large_MultipleLegs_Tall_Wooden] = [];
            testCases[CreatureConstants.AnimatedObject_Large_Sheetlike] = [];
            testCases[CreatureConstants.AnimatedObject_Large_TwoLegs] = [];
            testCases[CreatureConstants.AnimatedObject_Large_TwoLegs_Wooden] = [];
            testCases[CreatureConstants.AnimatedObject_Large_Wheels_Wooden] = [];
            testCases[CreatureConstants.AnimatedObject_Large_Wooden] = [];
            testCases[CreatureConstants.AnimatedObject_Medium] = [];
            testCases[CreatureConstants.AnimatedObject_Medium_Flexible] = [];
            testCases[CreatureConstants.AnimatedObject_Medium_MultipleLegs] = [];
            testCases[CreatureConstants.AnimatedObject_Medium_MultipleLegs_Wooden] = [];
            testCases[CreatureConstants.AnimatedObject_Medium_Sheetlike] = [];
            testCases[CreatureConstants.AnimatedObject_Medium_TwoLegs] = [];
            testCases[CreatureConstants.AnimatedObject_Medium_TwoLegs_Wooden] = [];
            testCases[CreatureConstants.AnimatedObject_Medium_Wheels_Wooden] = [];
            testCases[CreatureConstants.AnimatedObject_Medium_Wooden] = [];
            testCases[CreatureConstants.AnimatedObject_Small] = [];
            testCases[CreatureConstants.AnimatedObject_Small_Flexible] = [];
            testCases[CreatureConstants.AnimatedObject_Small_MultipleLegs] = [];
            testCases[CreatureConstants.AnimatedObject_Small_MultipleLegs_Wooden] = [];
            testCases[CreatureConstants.AnimatedObject_Small_Sheetlike] = [];
            testCases[CreatureConstants.AnimatedObject_Small_TwoLegs] = [];
            testCases[CreatureConstants.AnimatedObject_Small_TwoLegs_Wooden] = [];
            testCases[CreatureConstants.AnimatedObject_Small_Wheels_Wooden] = [];
            testCases[CreatureConstants.AnimatedObject_Small_Wooden] = [];
            testCases[CreatureConstants.AnimatedObject_Tiny] = [];
            testCases[CreatureConstants.AnimatedObject_Tiny_Flexible] = [];
            testCases[CreatureConstants.AnimatedObject_Tiny_MultipleLegs] = [];
            testCases[CreatureConstants.AnimatedObject_Tiny_MultipleLegs_Wooden] = [];
            testCases[CreatureConstants.AnimatedObject_Tiny_Sheetlike] = [];
            testCases[CreatureConstants.AnimatedObject_Tiny_TwoLegs] = [];
            testCases[CreatureConstants.AnimatedObject_Tiny_TwoLegs_Wooden] = [];
            testCases[CreatureConstants.AnimatedObject_Tiny_Wheels_Wooden] = [];
            testCases[CreatureConstants.AnimatedObject_Tiny_Wooden] = [];

            testCases[CreatureConstants.Ankheg].Add(BuildData(CreatureConstants.Ankheg, FeatConstants.SpecialQualities.Tremorsense, power: 60));

            testCases[CreatureConstants.Annis].Add(BuildData(CreatureConstants.Annis, FeatConstants.SpecialQualities.SpellResistance, power: 19));
            testCases[CreatureConstants.Annis].Add(BuildData(CreatureConstants.Annis, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to bludgeoning weapons", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Annis].Add(BuildData(CreatureConstants.Annis, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DisguiseSelf, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Annis].Add(BuildData(CreatureConstants.Annis, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Ant_Giant_Worker].Add(BuildData(CreatureConstants.Ant_Giant_Worker, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Ant_Giant_Worker].Add(BuildData(CreatureConstants.Ant_Giant_Worker, FeatConstants.Track));

            testCases[CreatureConstants.Ant_Giant_Soldier].Add(BuildData(CreatureConstants.Ant_Giant_Soldier, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Ant_Giant_Soldier].Add(BuildData(CreatureConstants.Ant_Giant_Soldier, FeatConstants.Track));

            testCases[CreatureConstants.Ant_Giant_Queen].Add(BuildData(CreatureConstants.Ant_Giant_Queen, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Ant_Giant_Queen].Add(BuildData(CreatureConstants.Ant_Giant_Queen, FeatConstants.Track));

            testCases[CreatureConstants.Ape].Add(BuildData(CreatureConstants.Ape, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Ape_Dire].Add(BuildData(CreatureConstants.Ape_Dire, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Aranea].Add(BuildData(CreatureConstants.Aranea, FeatConstants.SpecialQualities.ChangeShape, focus: "Small or Medium humanoid; or Medium spider-human hybrid (like a Lycanthrope)", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

            testCases[CreatureConstants.Arrowhawk_Juvenile].Add(BuildData(CreatureConstants.Arrowhawk_Juvenile, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Arrowhawk_Juvenile].Add(BuildData(CreatureConstants.Arrowhawk_Juvenile, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Arrowhawk_Juvenile].Add(BuildData(CreatureConstants.Arrowhawk_Juvenile, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.Arrowhawk_Juvenile].Add(BuildData(CreatureConstants.Arrowhawk_Juvenile, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Arrowhawk_Juvenile].Add(BuildData(CreatureConstants.Arrowhawk_Juvenile, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

            testCases[CreatureConstants.Arrowhawk_Adult].Add(BuildData(CreatureConstants.Arrowhawk_Adult, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Arrowhawk_Adult].Add(BuildData(CreatureConstants.Arrowhawk_Adult, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Arrowhawk_Adult].Add(BuildData(CreatureConstants.Arrowhawk_Adult, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.Arrowhawk_Adult].Add(BuildData(CreatureConstants.Arrowhawk_Adult, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Arrowhawk_Adult].Add(BuildData(CreatureConstants.Arrowhawk_Adult, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

            testCases[CreatureConstants.Arrowhawk_Elder].Add(BuildData(CreatureConstants.Arrowhawk_Elder, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Arrowhawk_Elder].Add(BuildData(CreatureConstants.Arrowhawk_Elder, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Arrowhawk_Elder].Add(BuildData(CreatureConstants.Arrowhawk_Elder, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.Arrowhawk_Elder].Add(BuildData(CreatureConstants.Arrowhawk_Elder, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Arrowhawk_Elder].Add(BuildData(CreatureConstants.Arrowhawk_Elder, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Arrowhawk_Elder].Add(BuildData(CreatureConstants.Arrowhawk_Elder, FeatConstants.WeaponFocus, focus: "Bite"));

            testCases[CreatureConstants.AssassinVine].Add(BuildData(CreatureConstants.AssassinVine, FeatConstants.SpecialQualities.Blindsight, power: 30));
            testCases[CreatureConstants.AssassinVine].Add(BuildData(CreatureConstants.AssassinVine, FeatConstants.SpecialQualities.Camouflage));
            testCases[CreatureConstants.AssassinVine].Add(BuildData(CreatureConstants.AssassinVine, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.AssassinVine].Add(BuildData(CreatureConstants.AssassinVine, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.AssassinVine].Add(BuildData(CreatureConstants.AssassinVine, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));

            testCases[CreatureConstants.Athach] = [];

            testCases[CreatureConstants.Avoral].Add(BuildData(CreatureConstants.Avoral, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to evil or silver weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Avoral].Add(BuildData(CreatureConstants.Avoral, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Avoral].Add(BuildData(CreatureConstants.Avoral, FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
            testCases[CreatureConstants.Avoral].Add(BuildData(CreatureConstants.Avoral, FeatConstants.SpecialQualities.LayOnHands));
            testCases[CreatureConstants.Avoral].Add(BuildData(CreatureConstants.Avoral, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Avoral].Add(BuildData(CreatureConstants.Avoral, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Avoral].Add(BuildData(CreatureConstants.Avoral, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Sonic, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Avoral].Add(BuildData(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
            testCases[CreatureConstants.Avoral].Add(BuildData(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TrueSeeing, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Avoral].Add(BuildData(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Aid, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Avoral].Add(BuildData(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blur + ": self only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Avoral].Add(BuildData(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Command, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
            testCases[CreatureConstants.Avoral].Add(BuildData(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Avoral].Add(BuildData(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DimensionDoor, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Avoral].Add(BuildData(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Avoral].Add(BuildData(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 12));
            testCases[CreatureConstants.Avoral].Add(BuildData(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldPerson, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Avoral].Add(BuildData(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Light, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Avoral].Add(BuildData(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicCircleAgainstEvil + ": self only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Avoral].Add(BuildData(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicMissile, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Avoral].Add(BuildData(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Avoral].Add(BuildData(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LightningBolt, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 13));
            testCases[CreatureConstants.Avoral].Add(BuildData(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellResistance, power: 25));

            testCases[CreatureConstants.Azer].Add(BuildData(CreatureConstants.Azer, FeatConstants.SpecialQualities.SpellResistance, power: 13));
            testCases[CreatureConstants.Azer].Add(BuildData(CreatureConstants.Azer, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.LightHammer, requiresEquipment: true));
            testCases[CreatureConstants.Azer].Add(BuildData(CreatureConstants.Azer, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Warhammer, requiresEquipment: true));
            testCases[CreatureConstants.Azer].Add(BuildData(CreatureConstants.Azer, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Spear, requiresEquipment: true));
            testCases[CreatureConstants.Azer].Add(BuildData(CreatureConstants.Azer, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Shortspear, requiresEquipment: true));
            testCases[CreatureConstants.Azer].Add(BuildData(CreatureConstants.Azer, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Longspear, requiresEquipment: true));

            testCases[CreatureConstants.Babau].Add(BuildData(CreatureConstants.Babau, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Babau].Add(BuildData(CreatureConstants.Babau, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.Babau].Add(BuildData(CreatureConstants.Babau, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Babau].Add(BuildData(CreatureConstants.Babau, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Babau].Add(BuildData(CreatureConstants.Babau, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Babau].Add(BuildData(CreatureConstants.Babau, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good or cold iron weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Babau].Add(BuildData(CreatureConstants.Babau, FeatConstants.SpecialQualities.ProtectiveSlime, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveBaseValue: 13));
            testCases[CreatureConstants.Babau].Add(BuildData(CreatureConstants.Babau, FeatConstants.SpecialQualities.SpellResistance, power: 14));
            testCases[CreatureConstants.Babau].Add(BuildData(CreatureConstants.Babau, FeatConstants.SpecialQualities.Telepathy, power: 100));
            testCases[CreatureConstants.Babau].Add(BuildData(CreatureConstants.Babau, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Babau].Add(BuildData(CreatureConstants.Babau, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Babau].Add(BuildData(CreatureConstants.Babau, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Babau].Add(BuildData(CreatureConstants.Babau, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

            testCases[CreatureConstants.Baboon].Add(BuildData(CreatureConstants.Baboon, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Badger].Add(BuildData(CreatureConstants.Badger, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Badger].Add(BuildData(CreatureConstants.Badger, FeatConstants.Track));
            testCases[CreatureConstants.Badger].Add(BuildData(CreatureConstants.Badger, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Badger_Dire].Add(BuildData(CreatureConstants.Badger_Dire, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Badger_Dire].Add(BuildData(CreatureConstants.Badger_Dire, FeatConstants.Track));

            testCases[CreatureConstants.Balor].Add(BuildData(CreatureConstants.Balor, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Balor].Add(BuildData(CreatureConstants.Balor, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
            testCases[CreatureConstants.Balor].Add(BuildData(CreatureConstants.Balor, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.Balor].Add(BuildData(CreatureConstants.Balor, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Balor].Add(BuildData(CreatureConstants.Balor, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Balor].Add(BuildData(CreatureConstants.Balor, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good, cold iron weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Balor].Add(BuildData(CreatureConstants.Balor, FeatConstants.SpecialQualities.FlamingBody));
            testCases[CreatureConstants.Balor].Add(BuildData(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellResistance, power: 28));
            testCases[CreatureConstants.Balor].Add(BuildData(CreatureConstants.Balor, FeatConstants.SpecialQualities.Telepathy, power: 100));
            testCases[CreatureConstants.Balor].Add(BuildData(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TrueSeeing, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
            testCases[CreatureConstants.Balor].Add(BuildData(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blasphemy, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));
            testCases[CreatureConstants.Balor].Add(BuildData(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DominateMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 19));
            testCases[CreatureConstants.Balor].Add(BuildData(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Balor].Add(BuildData(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Balor].Add(BuildData(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Insanity, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));
            testCases[CreatureConstants.Balor].Add(BuildData(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PowerWordStun, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Balor].Add(BuildData(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Telekinesis, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
            testCases[CreatureConstants.Balor].Add(BuildData(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyAura, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 18));
            testCases[CreatureConstants.Balor].Add(BuildData(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FireStorm, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 18));
            testCases[CreatureConstants.Balor].Add(BuildData(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Implosion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 19));
            testCases[CreatureConstants.Balor].Add(BuildData(CreatureConstants.Balor, FeatConstants.WeaponProficiency_Exotic, focus: WeaponConstants.Whip, requiresEquipment: true));
            testCases[CreatureConstants.Balor].Add(BuildData(CreatureConstants.Balor, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));

            testCases[CreatureConstants.BarbedDevil_Hamatula].Add(BuildData(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.BarbedDefense));
            testCases[CreatureConstants.BarbedDevil_Hamatula].Add(BuildData(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.BarbedDevil_Hamatula].Add(BuildData(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
            testCases[CreatureConstants.BarbedDevil_Hamatula].Add(BuildData(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.BarbedDevil_Hamatula].Add(BuildData(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.BarbedDevil_Hamatula].Add(BuildData(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.BarbedDevil_Hamatula].Add(BuildData(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.SeeInDarkness, focus: "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell"));
            testCases[CreatureConstants.BarbedDevil_Hamatula].Add(BuildData(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.SpellResistance, power: 23));
            testCases[CreatureConstants.BarbedDevil_Hamatula].Add(BuildData(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.Telepathy, power: 100));
            testCases[CreatureConstants.BarbedDevil_Hamatula].Add(BuildData(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.BarbedDevil_Hamatula].Add(BuildData(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldPerson, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.BarbedDevil_Hamatula].Add(BuildData(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MajorImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.BarbedDevil_Hamatula].Add(BuildData(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ScorchingRay + ": 2 rays only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.BarbedDevil_Hamatula].Add(BuildData(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.OrdersWrath, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.BarbedDevil_Hamatula].Add(BuildData(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyBlight, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));

            testCases[CreatureConstants.Barghest].Add(BuildData(CreatureConstants.Barghest, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Barghest].Add(BuildData(CreatureConstants.Barghest, FeatConstants.SpecialQualities.ChangeShape, focus: "Goblin or wolf", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Barghest].Add(BuildData(CreatureConstants.Barghest, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Barghest].Add(BuildData(CreatureConstants.Barghest, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PassWithoutTrace + ": in wolf form", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Barghest].Add(BuildData(CreatureConstants.Barghest, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blink, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Barghest].Add(BuildData(CreatureConstants.Barghest, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Levitate, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Barghest].Add(BuildData(CreatureConstants.Barghest, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Misdirection, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.Barghest].Add(BuildData(CreatureConstants.Barghest, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Rage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Barghest].Add(BuildData(CreatureConstants.Barghest, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Barghest].Add(BuildData(CreatureConstants.Barghest, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CrushingDespair, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Barghest].Add(BuildData(CreatureConstants.Barghest, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DimensionDoor, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Barghest_Greater].Add(BuildData(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Barghest_Greater].Add(BuildData(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.ChangeShape, focus: "Goblin or wolf", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Barghest_Greater].Add(BuildData(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Barghest_Greater].Add(BuildData(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PassWithoutTrace + ": in wolf form", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Barghest_Greater].Add(BuildData(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blink, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Barghest_Greater].Add(BuildData(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Levitate, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Barghest_Greater].Add(BuildData(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Misdirection, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.Barghest_Greater].Add(BuildData(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Rage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Barghest_Greater].Add(BuildData(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.InvisibilitySphere, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Barghest_Greater].Add(BuildData(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Barghest_Greater].Add(BuildData(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CrushingDespair, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Barghest_Greater].Add(BuildData(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DimensionDoor, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Barghest_Greater].Add(BuildData(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.BullsStrength_Mass, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Barghest_Greater].Add(BuildData(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EnlargePerson_Mass, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Basilisk] = [];

            testCases[CreatureConstants.Basilisk_Greater] = [];

            testCases[CreatureConstants.Bat].Add(BuildData(CreatureConstants.Bat, FeatConstants.SpecialQualities.Blindsense, power: 20));

            testCases[CreatureConstants.Bat_Dire].Add(BuildData(CreatureConstants.Bat_Dire, FeatConstants.SpecialQualities.Blindsense, power: 40));

            testCases[CreatureConstants.Bat_Swarm].Add(BuildData(CreatureConstants.Bat_Swarm, FeatConstants.SpecialQualities.Blindsense, power: 20));

            testCases[CreatureConstants.Bear_Black].Add(BuildData(CreatureConstants.Bear_Black, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Bear_Brown].Add(BuildData(CreatureConstants.Bear_Brown, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Bear_Dire].Add(BuildData(CreatureConstants.Bear_Dire, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Bear_Polar].Add(BuildData(CreatureConstants.Bear_Polar, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.BeardedDevil_Barbazu].Add(BuildData(CreatureConstants.BeardedDevil_Barbazu, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good or silver weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.BeardedDevil_Barbazu].Add(BuildData(CreatureConstants.BeardedDevil_Barbazu, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
            testCases[CreatureConstants.BeardedDevil_Barbazu].Add(BuildData(CreatureConstants.BeardedDevil_Barbazu, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.BeardedDevil_Barbazu].Add(BuildData(CreatureConstants.BeardedDevil_Barbazu, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.BeardedDevil_Barbazu].Add(BuildData(CreatureConstants.BeardedDevil_Barbazu, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.BeardedDevil_Barbazu].Add(BuildData(CreatureConstants.BeardedDevil_Barbazu, FeatConstants.SpecialQualities.SeeInDarkness, focus: "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell"));
            testCases[CreatureConstants.BeardedDevil_Barbazu].Add(BuildData(CreatureConstants.BeardedDevil_Barbazu, FeatConstants.SpecialQualities.SpellResistance, power: 17));
            testCases[CreatureConstants.BeardedDevil_Barbazu].Add(BuildData(CreatureConstants.BeardedDevil_Barbazu, FeatConstants.SpecialQualities.Telepathy, power: 100));
            testCases[CreatureConstants.BeardedDevil_Barbazu].Add(BuildData(CreatureConstants.BeardedDevil_Barbazu, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.BeardedDevil_Barbazu].Add(BuildData(CreatureConstants.BeardedDevil_Barbazu, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Glaive, requiresEquipment: true));

            testCases[CreatureConstants.Bebilith].Add(BuildData(CreatureConstants.Bebilith, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Bebilith].Add(BuildData(CreatureConstants.Bebilith, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Bebilith].Add(BuildData(CreatureConstants.Bebilith, FeatConstants.SpecialQualities.Telepathy, power: 100));
            testCases[CreatureConstants.Bebilith].Add(BuildData(CreatureConstants.Bebilith, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlaneShift + ": self only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

            testCases[CreatureConstants.Bee_Giant] = [];

            testCases[CreatureConstants.Behir].Add(BuildData(CreatureConstants.Behir, FeatConstants.SpecialQualities.Immunity, focus: "Tripping"));
            testCases[CreatureConstants.Behir].Add(BuildData(CreatureConstants.Behir, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Behir].Add(BuildData(CreatureConstants.Behir, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Beholder].Add(BuildData(CreatureConstants.Beholder, FeatConstants.SpecialQualities.AllAroundVision));
            testCases[CreatureConstants.Beholder].Add(BuildData(CreatureConstants.Beholder, FeatConstants.SpecialQualities.AntimagicCone));
            testCases[CreatureConstants.Beholder].Add(BuildData(CreatureConstants.Beholder, FeatConstants.SpecialQualities.Flight));
            testCases[CreatureConstants.Beholder].Add(BuildData(CreatureConstants.Beholder, FeatConstants.Alertness, power: 2));

            testCases[CreatureConstants.Beholder_Gauth].Add(BuildData(CreatureConstants.Beholder_Gauth, FeatConstants.SpecialQualities.AllAroundVision));
            testCases[CreatureConstants.Beholder_Gauth].Add(BuildData(CreatureConstants.Beholder_Gauth, FeatConstants.SpecialQualities.Flight));
            testCases[CreatureConstants.Beholder_Gauth].Add(BuildData(CreatureConstants.Beholder_Gauth, FeatConstants.Alertness, power: 2));

            testCases[CreatureConstants.Belker].Add(BuildData(CreatureConstants.Belker, FeatConstants.SpecialQualities.SmokeForm));

            testCases[CreatureConstants.Bison].Add(BuildData(CreatureConstants.Bison, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.BlackPudding].Add(BuildData(CreatureConstants.BlackPudding, FeatConstants.SpecialQualities.Split));

            testCases[CreatureConstants.BlackPudding_Elder].Add(BuildData(CreatureConstants.BlackPudding_Elder, FeatConstants.SpecialQualities.Split));

            testCases[CreatureConstants.BlinkDog].Add(BuildData(CreatureConstants.BlinkDog, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blink, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.BlinkDog].Add(BuildData(CreatureConstants.BlinkDog, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DimensionDoor, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.BlinkDog].Add(BuildData(CreatureConstants.BlinkDog, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.BlinkDog].Add(BuildData(CreatureConstants.BlinkDog, FeatConstants.Track));

            testCases[CreatureConstants.Boar].Add(BuildData(CreatureConstants.Boar, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Boar_Dire].Add(BuildData(CreatureConstants.Boar_Dire, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Bodak].Add(BuildData(CreatureConstants.Bodak, FeatConstants.SpecialQualities.Vulnerability, focus: "Sunlight"));
            testCases[CreatureConstants.Bodak].Add(BuildData(CreatureConstants.Bodak, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Bodak].Add(BuildData(CreatureConstants.Bodak, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Bodak].Add(BuildData(CreatureConstants.Bodak, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Bodak].Add(BuildData(CreatureConstants.Bodak, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

            testCases[CreatureConstants.BombardierBeetle_Giant] = [];

            testCases[CreatureConstants.BoneDevil_Osyluth].Add(BuildData(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.BoneDevil_Osyluth].Add(BuildData(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
            testCases[CreatureConstants.BoneDevil_Osyluth].Add(BuildData(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.BoneDevil_Osyluth].Add(BuildData(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.BoneDevil_Osyluth].Add(BuildData(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.BoneDevil_Osyluth].Add(BuildData(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.SeeInDarkness, focus: "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell"));
            testCases[CreatureConstants.BoneDevil_Osyluth].Add(BuildData(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.SpellResistance, power: 21));
            testCases[CreatureConstants.BoneDevil_Osyluth].Add(BuildData(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.Telepathy, power: 100));
            testCases[CreatureConstants.BoneDevil_Osyluth].Add(BuildData(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.BoneDevil_Osyluth].Add(BuildData(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DimensionalAnchor, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.BoneDevil_Osyluth].Add(BuildData(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fly, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.BoneDevil_Osyluth].Add(BuildData(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + ": self only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.BoneDevil_Osyluth].Add(BuildData(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MajorImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.BoneDevil_Osyluth].Add(BuildData(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfIce, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

            testCases[CreatureConstants.Bralani].Add(BuildData(CreatureConstants.Bralani, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Bralani].Add(BuildData(CreatureConstants.Bralani, FeatConstants.SpecialQualities.AlternateForm, focus: "Humanoid or whirlwind form", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Bralani].Add(BuildData(CreatureConstants.Bralani, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron or evil weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Bralani].Add(BuildData(CreatureConstants.Bralani, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Bralani].Add(BuildData(CreatureConstants.Bralani, FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
            testCases[CreatureConstants.Bralani].Add(BuildData(CreatureConstants.Bralani, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Bralani].Add(BuildData(CreatureConstants.Bralani, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Bralani].Add(BuildData(CreatureConstants.Bralani, FeatConstants.SpecialQualities.SpellResistance, power: 17));
            testCases[CreatureConstants.Bralani].Add(BuildData(CreatureConstants.Bralani, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Tongues, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
            testCases[CreatureConstants.Bralani].Add(BuildData(CreatureConstants.Bralani, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blur, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Bralani].Add(BuildData(CreatureConstants.Bralani, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmPerson, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
            testCases[CreatureConstants.Bralani].Add(BuildData(CreatureConstants.Bralani, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 12));
            testCases[CreatureConstants.Bralani].Add(BuildData(CreatureConstants.Bralani, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MirrorImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Bralani].Add(BuildData(CreatureConstants.Bralani, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WindWall, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Bralani].Add(BuildData(CreatureConstants.Bralani, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LightningBolt, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 13));
            testCases[CreatureConstants.Bralani].Add(BuildData(CreatureConstants.Bralani, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CureSeriousWounds, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Bralani].Add(BuildData(CreatureConstants.Bralani, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Scimitar, requiresEquipment: true));
            testCases[CreatureConstants.Bralani].Add(BuildData(CreatureConstants.Bralani, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));

            testCases[CreatureConstants.Bugbear].Add(BuildData(CreatureConstants.Bugbear, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Bugbear].Add(BuildData(CreatureConstants.Bugbear, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Bugbear].Add(BuildData(CreatureConstants.Bugbear, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Morningstar, requiresEquipment: true));
            testCases[CreatureConstants.Bugbear].Add(BuildData(CreatureConstants.Bugbear, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Javelin, requiresEquipment: true));
            testCases[CreatureConstants.Bugbear].Add(BuildData(CreatureConstants.Bugbear, FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

            testCases[CreatureConstants.Bulette].Add(BuildData(CreatureConstants.Bulette, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Bulette].Add(BuildData(CreatureConstants.Bulette, FeatConstants.SpecialQualities.Tremorsense, power: 60));

            testCases[CreatureConstants.Camel_Bactrian].Add(BuildData(CreatureConstants.Camel_Bactrian, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Camel_Dromedary].Add(BuildData(CreatureConstants.Camel_Bactrian, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.CarrionCrawler].Add(BuildData(CreatureConstants.CarrionCrawler, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.CarrionCrawler].Add(BuildData(CreatureConstants.CarrionCrawler, FeatConstants.Alertness, power: 2));

            testCases[CreatureConstants.Cat].Add(BuildData(CreatureConstants.Cat, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Cat].Add(BuildData(CreatureConstants.Cat, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Centaur].Add(BuildData(CreatureConstants.Centaur, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
            testCases[CreatureConstants.Centaur].Add(BuildData(CreatureConstants.Centaur, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
            testCases[CreatureConstants.Centaur].Add(BuildData(CreatureConstants.Centaur, FeatConstants.MountedCombat));

            testCases[CreatureConstants.Centipede_Monstrous_Tiny].Add(BuildData(CreatureConstants.Centipede_Monstrous_Tiny, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Centipede_Monstrous_Small].Add(BuildData(CreatureConstants.Centipede_Monstrous_Small, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Centipede_Monstrous_Medium].Add(BuildData(CreatureConstants.Centipede_Monstrous_Medium, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Centipede_Monstrous_Large].Add(BuildData(CreatureConstants.Centipede_Monstrous_Large, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Centipede_Monstrous_Huge] = [];

            testCases[CreatureConstants.Centipede_Monstrous_Gargantuan] = [];

            testCases[CreatureConstants.Centipede_Monstrous_Colossal] = [];

            testCases[CreatureConstants.Centipede_Swarm].Add(BuildData(CreatureConstants.Centipede_Swarm, FeatConstants.SpecialQualities.Tremorsense, power: 30));
            testCases[CreatureConstants.Centipede_Swarm].Add(BuildData(CreatureConstants.Centipede_Swarm, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.ChainDevil_Kyton].Add(BuildData(CreatureConstants.ChainDevil_Kyton, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good or silver weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.ChainDevil_Kyton].Add(BuildData(CreatureConstants.ChainDevil_Kyton, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
            testCases[CreatureConstants.ChainDevil_Kyton].Add(BuildData(CreatureConstants.ChainDevil_Kyton, FeatConstants.SpecialQualities.Regeneration, focus: "Does not regenerate damage from silver weapons or good-aligned damage", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.ChainDevil_Kyton].Add(BuildData(CreatureConstants.ChainDevil_Kyton, FeatConstants.SpecialQualities.SpellResistance, power: 18));
            testCases[CreatureConstants.ChainDevil_Kyton].Add(BuildData(CreatureConstants.ChainDevil_Kyton, FeatConstants.WeaponProficiency_Exotic, focus: WeaponConstants.SpikedChain, requiresEquipment: true));

            testCases[CreatureConstants.ChaosBeast].Add(BuildData(CreatureConstants.ChaosBeast, FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
            testCases[CreatureConstants.ChaosBeast].Add(BuildData(CreatureConstants.ChaosBeast, FeatConstants.SpecialQualities.Immunity, focus: "Transformation"));
            testCases[CreatureConstants.ChaosBeast].Add(BuildData(CreatureConstants.ChaosBeast, FeatConstants.SpecialQualities.SpellResistance, power: 15));

            testCases[CreatureConstants.Cheetah].Add(BuildData(CreatureConstants.Cheetah, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Cheetah].Add(BuildData(CreatureConstants.Cheetah, FeatConstants.SpecialQualities.Sprint, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));

            testCases[CreatureConstants.Chimera_Black].Add(BuildData(CreatureConstants.Chimera_Black, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Chimera_Blue].Add(BuildData(CreatureConstants.Chimera_Blue, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Chimera_Green].Add(BuildData(CreatureConstants.Chimera_Green, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Chimera_Red].Add(BuildData(CreatureConstants.Chimera_Red, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Chimera_White].Add(BuildData(CreatureConstants.Chimera_White, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Choker].Add(BuildData(CreatureConstants.Choker, FeatConstants.SpecialQualities.Quickness));
            testCases[CreatureConstants.Choker].Add(BuildData(CreatureConstants.Choker, FeatConstants.Initiative_Improved, power: 4));

            testCases[CreatureConstants.Chuul].Add(BuildData(CreatureConstants.Chuul, FeatConstants.SpecialQualities.Amphibious));
            testCases[CreatureConstants.Chuul].Add(BuildData(CreatureConstants.Chuul, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));

            testCases[CreatureConstants.Cloaker].Add(BuildData(CreatureConstants.Cloaker, FeatConstants.SpecialQualities.ShadowShift));

            testCases[CreatureConstants.Cockatrice].Add(BuildData(CreatureConstants.Cockatrice, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Couatl].Add(BuildData(CreatureConstants.Couatl, FeatConstants.SpecialQualities.ChangeShape, focus: "Any Small or Medium humanoid", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Couatl].Add(BuildData(CreatureConstants.Couatl, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EtherealJaunt, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Couatl].Add(BuildData(CreatureConstants.Couatl, FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.DetectChaos, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Couatl].Add(BuildData(CreatureConstants.Couatl, FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.DetectEvil, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Couatl].Add(BuildData(CreatureConstants.Couatl, FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.DetectGood, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Couatl].Add(BuildData(CreatureConstants.Couatl, FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.DetectLaw, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Couatl].Add(BuildData(CreatureConstants.Couatl, FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.DetectThoughts, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.Couatl].Add(BuildData(CreatureConstants.Couatl, FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.Invisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Couatl].Add(BuildData(CreatureConstants.Couatl, FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.PlaneShift, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));
            testCases[CreatureConstants.Couatl].Add(BuildData(CreatureConstants.Couatl, FeatConstants.SpecialQualities.Telepathy, power: 90));
            testCases[CreatureConstants.Couatl].Add(BuildData(CreatureConstants.Couatl, FeatConstants.EschewMaterials));

            testCases[CreatureConstants.Criosphinx] = [];

            testCases[CreatureConstants.Crocodile].Add(BuildData(CreatureConstants.Crocodile, FeatConstants.SpecialQualities.HoldBreath));

            testCases[CreatureConstants.Crocodile_Giant].Add(BuildData(CreatureConstants.Crocodile_Giant, FeatConstants.SpecialQualities.HoldBreath));

            testCases[CreatureConstants.Cryohydra_5Heads].Add(BuildData(CreatureConstants.Cryohydra_5Heads, FeatConstants.SpecialQualities.FastHealing, power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Cryohydra_5Heads].Add(BuildData(CreatureConstants.Cryohydra_5Heads, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Cryohydra_5Heads].Add(BuildData(CreatureConstants.Cryohydra_5Heads, FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

            testCases[CreatureConstants.Cryohydra_6Heads].Add(BuildData(CreatureConstants.Cryohydra_6Heads, FeatConstants.SpecialQualities.FastHealing, power: 16, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Cryohydra_6Heads].Add(BuildData(CreatureConstants.Cryohydra_6Heads, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Cryohydra_6Heads].Add(BuildData(CreatureConstants.Cryohydra_6Heads, FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

            testCases[CreatureConstants.Cryohydra_7Heads].Add(BuildData(CreatureConstants.Cryohydra_7Heads, FeatConstants.SpecialQualities.FastHealing, power: 17, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Cryohydra_7Heads].Add(BuildData(CreatureConstants.Cryohydra_7Heads, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Cryohydra_7Heads].Add(BuildData(CreatureConstants.Cryohydra_7Heads, FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

            testCases[CreatureConstants.Cryohydra_8Heads].Add(BuildData(CreatureConstants.Cryohydra_8Heads, FeatConstants.SpecialQualities.FastHealing, power: 18, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Cryohydra_8Heads].Add(BuildData(CreatureConstants.Cryohydra_8Heads, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Cryohydra_8Heads].Add(BuildData(CreatureConstants.Cryohydra_8Heads, FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

            testCases[CreatureConstants.Cryohydra_9Heads].Add(BuildData(CreatureConstants.Cryohydra_9Heads, FeatConstants.SpecialQualities.FastHealing, power: 19, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Cryohydra_9Heads].Add(BuildData(CreatureConstants.Cryohydra_9Heads, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Cryohydra_9Heads].Add(BuildData(CreatureConstants.Cryohydra_9Heads, FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

            testCases[CreatureConstants.Cryohydra_10Heads].Add(BuildData(CreatureConstants.Cryohydra_10Heads, FeatConstants.SpecialQualities.FastHealing, power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Cryohydra_10Heads].Add(BuildData(CreatureConstants.Cryohydra_10Heads, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Cryohydra_10Heads].Add(BuildData(CreatureConstants.Cryohydra_10Heads, FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

            testCases[CreatureConstants.Cryohydra_11Heads].Add(BuildData(CreatureConstants.Cryohydra_11Heads, FeatConstants.SpecialQualities.FastHealing, power: 21, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Cryohydra_11Heads].Add(BuildData(CreatureConstants.Cryohydra_11Heads, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Cryohydra_11Heads].Add(BuildData(CreatureConstants.Cryohydra_11Heads, FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

            testCases[CreatureConstants.Cryohydra_12Heads].Add(BuildData(CreatureConstants.Cryohydra_12Heads, FeatConstants.SpecialQualities.FastHealing, power: 22, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Cryohydra_12Heads].Add(BuildData(CreatureConstants.Cryohydra_12Heads, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Cryohydra_12Heads].Add(BuildData(CreatureConstants.Cryohydra_12Heads, FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

            testCases[CreatureConstants.Darkmantle].Add(BuildData(CreatureConstants.Darkmantle, FeatConstants.SpecialQualities.Blind));
            testCases[CreatureConstants.Darkmantle].Add(BuildData(CreatureConstants.Darkmantle, FeatConstants.SpecialQualities.Blindsight, power: 90));
            testCases[CreatureConstants.Darkmantle].Add(BuildData(CreatureConstants.Darkmantle, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Deinonychus].Add(BuildData(CreatureConstants.Deinonychus, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Delver].Add(BuildData(CreatureConstants.Delver, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Delver].Add(BuildData(CreatureConstants.Delver, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneShape, frequencyQuantity: 6, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
            testCases[CreatureConstants.Delver].Add(BuildData(CreatureConstants.Delver, FeatConstants.SpecialQualities.Tremorsense, power: 60));

            testCases[CreatureConstants.Derro].Add(BuildData(CreatureConstants.Derro, FeatConstants.SpecialQualities.Madness));
            testCases[CreatureConstants.Derro].Add(BuildData(CreatureConstants.Derro, FeatConstants.SpecialQualities.SpellResistance, power: 15));
            testCases[CreatureConstants.Derro].Add(BuildData(CreatureConstants.Derro, FeatConstants.SpecialQualities.Vulnerability, focus: "Sunlight"));
            testCases[CreatureConstants.Derro].Add(BuildData(CreatureConstants.Derro, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Derro].Add(BuildData(CreatureConstants.Derro, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GhostSound, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Derro].Add(BuildData(CreatureConstants.Derro, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Daze, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 10));
            testCases[CreatureConstants.Derro].Add(BuildData(CreatureConstants.Derro, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SoundBurst, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.Derro].Add(BuildData(CreatureConstants.Derro, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.ShortSword, requiresEquipment: true));
            testCases[CreatureConstants.Derro].Add(BuildData(CreatureConstants.Derro, FeatConstants.WeaponProficiency_Exotic, focus: WeaponConstants.LightRepeatingCrossbow, requiresEquipment: true));
            testCases[CreatureConstants.Derro].Add(BuildData(CreatureConstants.Derro, FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

            testCases[CreatureConstants.Derro_Sane].Add(BuildData(CreatureConstants.Derro_Sane, FeatConstants.SpecialQualities.SpellResistance, power: 15));
            testCases[CreatureConstants.Derro_Sane].Add(BuildData(CreatureConstants.Derro_Sane, FeatConstants.SpecialQualities.Vulnerability, focus: "Sunlight"));
            testCases[CreatureConstants.Derro_Sane].Add(BuildData(CreatureConstants.Derro_Sane, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Derro_Sane].Add(BuildData(CreatureConstants.Derro_Sane, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GhostSound, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Derro_Sane].Add(BuildData(CreatureConstants.Derro_Sane, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Daze, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 10));
            testCases[CreatureConstants.Derro_Sane].Add(BuildData(CreatureConstants.Derro_Sane, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SoundBurst, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.Derro_Sane].Add(BuildData(CreatureConstants.Derro_Sane, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.ShortSword, requiresEquipment: true));
            testCases[CreatureConstants.Derro_Sane].Add(BuildData(CreatureConstants.Derro_Sane, FeatConstants.WeaponProficiency_Exotic, focus: WeaponConstants.LightRepeatingCrossbow, requiresEquipment: true));
            testCases[CreatureConstants.Derro_Sane].Add(BuildData(CreatureConstants.Derro_Sane, FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

            testCases[CreatureConstants.Destrachan].Add(BuildData(CreatureConstants.Destrachan, FeatConstants.SpecialQualities.Blind));
            testCases[CreatureConstants.Destrachan].Add(BuildData(CreatureConstants.Destrachan, FeatConstants.SpecialQualities.Blindsight, power: 100));
            testCases[CreatureConstants.Destrachan].Add(BuildData(CreatureConstants.Destrachan, FeatConstants.SpecialQualities.Immunity, focus: "Gaze attacks"));
            testCases[CreatureConstants.Destrachan].Add(BuildData(CreatureConstants.Destrachan, FeatConstants.SpecialQualities.Immunity, focus: "Visual effects"));
            testCases[CreatureConstants.Destrachan].Add(BuildData(CreatureConstants.Destrachan, FeatConstants.SpecialQualities.Immunity, focus: "Illusions"));
            testCases[CreatureConstants.Destrachan].Add(BuildData(CreatureConstants.Destrachan, FeatConstants.SpecialQualities.Immunity, focus: "Attacks that rely on sight"));

            testCases[CreatureConstants.Devourer].Add(BuildData(CreatureConstants.Devourer, FeatConstants.SpecialQualities.SpellDeflection));
            testCases[CreatureConstants.Devourer].Add(BuildData(CreatureConstants.Devourer, FeatConstants.SpecialQualities.SpellResistance, power: 21));
            testCases[CreatureConstants.Devourer].Add(BuildData(CreatureConstants.Devourer, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Confusion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Devourer].Add(BuildData(CreatureConstants.Devourer, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlUndead, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));
            testCases[CreatureConstants.Devourer].Add(BuildData(CreatureConstants.Devourer, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GhoulTouch, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 12));
            testCases[CreatureConstants.Devourer].Add(BuildData(CreatureConstants.Devourer, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlanarAlly_Lesser, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Devourer].Add(BuildData(CreatureConstants.Devourer, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RayOfEnfeeblement, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Devourer].Add(BuildData(CreatureConstants.Devourer, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpectralHand, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Devourer].Add(BuildData(CreatureConstants.Devourer, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Devourer].Add(BuildData(CreatureConstants.Devourer, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TrueSeeing, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

            testCases[CreatureConstants.Digester].Add(BuildData(CreatureConstants.Digester, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Digester].Add(BuildData(CreatureConstants.Digester, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.DisplacerBeast].Add(BuildData(CreatureConstants.DisplacerBeast, FeatConstants.SpecialQualities.Displacement));

            testCases[CreatureConstants.DisplacerBeast_PackLord].Add(BuildData(CreatureConstants.DisplacerBeast_PackLord, FeatConstants.SpecialQualities.Displacement));

            testCases[CreatureConstants.Djinni].Add(BuildData(CreatureConstants.Djinni, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Djinni].Add(BuildData(CreatureConstants.Djinni, FeatConstants.SpecialQualities.Telepathy, power: 100));
            testCases[CreatureConstants.Djinni].Add(BuildData(CreatureConstants.Djinni, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlaneShift + ": Genie and up to 8 other creatures, provided they all link hands with the genie", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Djinni].Add(BuildData(CreatureConstants.Djinni, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + ": self only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Djinni].Add(BuildData(CreatureConstants.Djinni, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateFoodAndWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Djinni].Add(BuildData(CreatureConstants.Djinni, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateWater + ": creates wine instead of water", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Djinni].Add(BuildData(CreatureConstants.Djinni, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MajorCreation + ": created vegetable matter is permanent", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Djinni].Add(BuildData(CreatureConstants.Djinni, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PersistentImage, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
            testCases[CreatureConstants.Djinni].Add(BuildData(CreatureConstants.Djinni, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WindWalk, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Djinni].Add(BuildData(CreatureConstants.Djinni, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GaseousForm, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Djinni].Add(BuildData(CreatureConstants.Djinni, FeatConstants.Initiative_Improved, power: 4));

            testCases[CreatureConstants.Djinni_Noble].Add(BuildData(CreatureConstants.Djinni_Noble, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Djinni_Noble].Add(BuildData(CreatureConstants.Djinni_Noble, FeatConstants.SpecialQualities.Telepathy, power: 100));
            testCases[CreatureConstants.Djinni_Noble].Add(BuildData(CreatureConstants.Djinni_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlaneShift + ": Genie and up to 8 other creatures, provided they all link hands with the genie", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Djinni_Noble].Add(BuildData(CreatureConstants.Djinni_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + ": self only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Djinni_Noble].Add(BuildData(CreatureConstants.Djinni_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateFoodAndWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Djinni_Noble].Add(BuildData(CreatureConstants.Djinni_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateWater + ": creates wine instead of water", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Djinni_Noble].Add(BuildData(CreatureConstants.Djinni_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MajorCreation + ": created vegetable matter is permanent", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Djinni_Noble].Add(BuildData(CreatureConstants.Djinni_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PersistentImage, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
            testCases[CreatureConstants.Djinni_Noble].Add(BuildData(CreatureConstants.Djinni_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WindWalk, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Djinni_Noble].Add(BuildData(CreatureConstants.Djinni_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GaseousForm, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Djinni_Noble].Add(BuildData(CreatureConstants.Djinni_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Wish + ": 3 wishes to any non-genie who captures it", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Djinni_Noble].Add(BuildData(CreatureConstants.Djinni_Noble, FeatConstants.Initiative_Improved, power: 4));

            testCases[CreatureConstants.Dog].Add(BuildData(CreatureConstants.Dog, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Dog].Add(BuildData(CreatureConstants.Dog, FeatConstants.Track));

            testCases[CreatureConstants.Dog_Riding].Add(BuildData(CreatureConstants.Dog_Riding, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Dog_Riding].Add(BuildData(CreatureConstants.Dog_Riding, FeatConstants.Track));

            testCases[CreatureConstants.Donkey].Add(BuildData(CreatureConstants.Donkey, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Doppelganger].Add(BuildData(CreatureConstants.Doppelganger, FeatConstants.SpecialQualities.ChangeShape, focus: "Any Small or Medium Humanoid", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Doppelganger].Add(BuildData(CreatureConstants.Doppelganger, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.Doppelganger].Add(BuildData(CreatureConstants.Doppelganger, FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
            testCases[CreatureConstants.Doppelganger].Add(BuildData(CreatureConstants.Doppelganger, FeatConstants.SpecialQualities.Immunity, focus: "Charm effects"));

            testCases[CreatureConstants.DragonTurtle].Add(BuildData(CreatureConstants.DragonTurtle, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.DragonTurtle].Add(BuildData(CreatureConstants.DragonTurtle, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));

            testCases[CreatureConstants.Dragon_Black_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Black_Wyrmling, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Black_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Black_Wyrmling, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Black_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Black_Wyrmling, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Black_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Black_Wyrmling, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Black_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Black_Wyrmling, FeatConstants.SpecialQualities.WaterBreathing));

            testCases[CreatureConstants.Dragon_Black_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Black_VeryYoung, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Black_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Black_VeryYoung, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Black_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Black_VeryYoung, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Black_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Black_VeryYoung, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Black_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Black_VeryYoung, FeatConstants.SpecialQualities.WaterBreathing));

            testCases[CreatureConstants.Dragon_Black_Young].Add(BuildData(CreatureConstants.Dragon_Black_Young, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Black_Young].Add(BuildData(CreatureConstants.Dragon_Black_Young, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Black_Young].Add(BuildData(CreatureConstants.Dragon_Black_Young, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Black_Young].Add(BuildData(CreatureConstants.Dragon_Black_Young, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Black_Young].Add(BuildData(CreatureConstants.Dragon_Black_Young, FeatConstants.SpecialQualities.WaterBreathing));

            testCases[CreatureConstants.Dragon_Black_Juvenile].Add(BuildData(CreatureConstants.Dragon_Black_Juvenile, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Black_Juvenile].Add(BuildData(CreatureConstants.Dragon_Black_Juvenile, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Black_Juvenile].Add(BuildData(CreatureConstants.Dragon_Black_Juvenile, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Black_Juvenile].Add(BuildData(CreatureConstants.Dragon_Black_Juvenile, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Black_Juvenile].Add(BuildData(CreatureConstants.Dragon_Black_Juvenile, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Black_Juvenile].Add(BuildData(CreatureConstants.Dragon_Black_Juvenile, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Black_YoungAdult, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Black_YoungAdult, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Black_YoungAdult, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Black_YoungAdult, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Black_YoungAdult, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Black_YoungAdult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Black_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Black_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, power: 17));

            testCases[CreatureConstants.Dragon_Black_Adult].Add(BuildData(CreatureConstants.Dragon_Black_Adult, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Black_Adult].Add(BuildData(CreatureConstants.Dragon_Black_Adult, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Black_Adult].Add(BuildData(CreatureConstants.Dragon_Black_Adult, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Black_Adult].Add(BuildData(CreatureConstants.Dragon_Black_Adult, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Black_Adult].Add(BuildData(CreatureConstants.Dragon_Black_Adult, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Black_Adult].Add(BuildData(CreatureConstants.Dragon_Black_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Black_Adult].Add(BuildData(CreatureConstants.Dragon_Black_Adult, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Black_Adult].Add(BuildData(CreatureConstants.Dragon_Black_Adult, FeatConstants.SpecialQualities.SpellResistance, power: 18));
            testCases[CreatureConstants.Dragon_Black_Adult].Add(BuildData(CreatureConstants.Dragon_Black_Adult, FeatConstants.SpecialQualities.CorruptWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 19));

            testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Black_MatureAdult, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Black_MatureAdult, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Black_MatureAdult, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Black_MatureAdult, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Black_MatureAdult, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Black_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Black_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Black_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, power: 21));
            testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Black_MatureAdult, FeatConstants.SpecialQualities.CorruptWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 21));

            testCases[CreatureConstants.Dragon_Black_Old].Add(BuildData(CreatureConstants.Dragon_Black_Old, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Black_Old].Add(BuildData(CreatureConstants.Dragon_Black_Old, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Black_Old].Add(BuildData(CreatureConstants.Dragon_Black_Old, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Black_Old].Add(BuildData(CreatureConstants.Dragon_Black_Old, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Black_Old].Add(BuildData(CreatureConstants.Dragon_Black_Old, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Black_Old].Add(BuildData(CreatureConstants.Dragon_Black_Old, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Black_Old].Add(BuildData(CreatureConstants.Dragon_Black_Old, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Black_Old].Add(BuildData(CreatureConstants.Dragon_Black_Old, FeatConstants.SpecialQualities.SpellResistance, power: 22));
            testCases[CreatureConstants.Dragon_Black_Old].Add(BuildData(CreatureConstants.Dragon_Black_Old, FeatConstants.SpecialQualities.CorruptWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 22));
            testCases[CreatureConstants.Dragon_Black_Old].Add(BuildData(CreatureConstants.Dragon_Black_Old, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Black_VeryOld].Add(BuildData(CreatureConstants.Dragon_Black_VeryOld, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Black_VeryOld].Add(BuildData(CreatureConstants.Dragon_Black_VeryOld, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Black_VeryOld].Add(BuildData(CreatureConstants.Dragon_Black_VeryOld, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Black_VeryOld].Add(BuildData(CreatureConstants.Dragon_Black_VeryOld, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Black_VeryOld].Add(BuildData(CreatureConstants.Dragon_Black_VeryOld, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Black_VeryOld].Add(BuildData(CreatureConstants.Dragon_Black_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Black_VeryOld].Add(BuildData(CreatureConstants.Dragon_Black_VeryOld, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Black_VeryOld].Add(BuildData(CreatureConstants.Dragon_Black_VeryOld, FeatConstants.SpecialQualities.SpellResistance, power: 23));
            testCases[CreatureConstants.Dragon_Black_VeryOld].Add(BuildData(CreatureConstants.Dragon_Black_VeryOld, FeatConstants.SpecialQualities.CorruptWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 24));
            testCases[CreatureConstants.Dragon_Black_VeryOld].Add(BuildData(CreatureConstants.Dragon_Black_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Black_Ancient].Add(BuildData(CreatureConstants.Dragon_Black_Ancient, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Black_Ancient].Add(BuildData(CreatureConstants.Dragon_Black_Ancient, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Black_Ancient].Add(BuildData(CreatureConstants.Dragon_Black_Ancient, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Black_Ancient].Add(BuildData(CreatureConstants.Dragon_Black_Ancient, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Black_Ancient].Add(BuildData(CreatureConstants.Dragon_Black_Ancient, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Black_Ancient].Add(BuildData(CreatureConstants.Dragon_Black_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Black_Ancient].Add(BuildData(CreatureConstants.Dragon_Black_Ancient, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Black_Ancient].Add(BuildData(CreatureConstants.Dragon_Black_Ancient, FeatConstants.SpecialQualities.SpellResistance, power: 25));
            testCases[CreatureConstants.Dragon_Black_Ancient].Add(BuildData(CreatureConstants.Dragon_Black_Ancient, FeatConstants.SpecialQualities.CorruptWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 25));
            testCases[CreatureConstants.Dragon_Black_Ancient].Add(BuildData(CreatureConstants.Dragon_Black_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Black_Ancient].Add(BuildData(CreatureConstants.Dragon_Black_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.InsectPlague, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Black_Wyrm].Add(BuildData(CreatureConstants.Dragon_Black_Wyrm, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Black_Wyrm].Add(BuildData(CreatureConstants.Dragon_Black_Wyrm, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Black_Wyrm].Add(BuildData(CreatureConstants.Dragon_Black_Wyrm, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Black_Wyrm].Add(BuildData(CreatureConstants.Dragon_Black_Wyrm, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Black_Wyrm].Add(BuildData(CreatureConstants.Dragon_Black_Wyrm, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Black_Wyrm].Add(BuildData(CreatureConstants.Dragon_Black_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Black_Wyrm].Add(BuildData(CreatureConstants.Dragon_Black_Wyrm, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Black_Wyrm].Add(BuildData(CreatureConstants.Dragon_Black_Wyrm, FeatConstants.SpecialQualities.SpellResistance, power: 26));
            testCases[CreatureConstants.Dragon_Black_Wyrm].Add(BuildData(CreatureConstants.Dragon_Black_Wyrm, FeatConstants.SpecialQualities.CorruptWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 27));
            testCases[CreatureConstants.Dragon_Black_Wyrm].Add(BuildData(CreatureConstants.Dragon_Black_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Black_Wyrm].Add(BuildData(CreatureConstants.Dragon_Black_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.InsectPlague, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, power: 28));
            testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.CorruptWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 28));
            testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.InsectPlague, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.CharmReptiles, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Blue_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Blue_Wyrmling, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Blue_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Blue_Wyrmling, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Blue_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Blue_Wyrmling, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Blue_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Blue_Wyrmling, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Dragon_Blue_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Blue_Wyrmling, FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Blue_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Blue_VeryYoung, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Blue_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Blue_VeryYoung, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Blue_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Blue_VeryYoung, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Blue_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Blue_VeryYoung, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Dragon_Blue_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Blue_VeryYoung, FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Blue_Young].Add(BuildData(CreatureConstants.Dragon_Blue_Young, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Blue_Young].Add(BuildData(CreatureConstants.Dragon_Blue_Young, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Blue_Young].Add(BuildData(CreatureConstants.Dragon_Blue_Young, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Blue_Young].Add(BuildData(CreatureConstants.Dragon_Blue_Young, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Dragon_Blue_Young].Add(BuildData(CreatureConstants.Dragon_Blue_Young, FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(BuildData(CreatureConstants.Dragon_Blue_Juvenile, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(BuildData(CreatureConstants.Dragon_Blue_Juvenile, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(BuildData(CreatureConstants.Dragon_Blue_Juvenile, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(BuildData(CreatureConstants.Dragon_Blue_Juvenile, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(BuildData(CreatureConstants.Dragon_Blue_Juvenile, FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(BuildData(CreatureConstants.Dragon_Blue_Juvenile, FeatConstants.SpecialQualities.SoundImitation));

            testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Blue_YoungAdult, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Blue_YoungAdult, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Blue_YoungAdult, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Blue_YoungAdult, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Blue_YoungAdult, FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 19));
            testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Blue_YoungAdult, FeatConstants.SpecialQualities.SoundImitation, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 19));
            testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Blue_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Blue_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, power: 19));

            testCases[CreatureConstants.Dragon_Blue_Adult].Add(BuildData(CreatureConstants.Dragon_Blue_Adult, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Blue_Adult].Add(BuildData(CreatureConstants.Dragon_Blue_Adult, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Blue_Adult].Add(BuildData(CreatureConstants.Dragon_Blue_Adult, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Blue_Adult].Add(BuildData(CreatureConstants.Dragon_Blue_Adult, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Dragon_Blue_Adult].Add(BuildData(CreatureConstants.Dragon_Blue_Adult, FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 20));
            testCases[CreatureConstants.Dragon_Blue_Adult].Add(BuildData(CreatureConstants.Dragon_Blue_Adult, FeatConstants.SpecialQualities.SoundImitation, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 20));
            testCases[CreatureConstants.Dragon_Blue_Adult].Add(BuildData(CreatureConstants.Dragon_Blue_Adult, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Blue_Adult].Add(BuildData(CreatureConstants.Dragon_Blue_Adult, FeatConstants.SpecialQualities.SpellResistance, power: 21));
            testCases[CreatureConstants.Dragon_Blue_Adult].Add(BuildData(CreatureConstants.Dragon_Blue_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));

            testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Blue_MatureAdult, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Blue_MatureAdult, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Blue_MatureAdult, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Blue_MatureAdult, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Blue_MatureAdult, FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 22));
            testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Blue_MatureAdult, FeatConstants.SpecialQualities.SoundImitation, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 22));
            testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Blue_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Blue_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, power: 22));
            testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Blue_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));

            testCases[CreatureConstants.Dragon_Blue_Old].Add(BuildData(CreatureConstants.Dragon_Blue_Old, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Blue_Old].Add(BuildData(CreatureConstants.Dragon_Blue_Old, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Blue_Old].Add(BuildData(CreatureConstants.Dragon_Blue_Old, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Blue_Old].Add(BuildData(CreatureConstants.Dragon_Blue_Old, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Dragon_Blue_Old].Add(BuildData(CreatureConstants.Dragon_Blue_Old, FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 23));
            testCases[CreatureConstants.Dragon_Blue_Old].Add(BuildData(CreatureConstants.Dragon_Blue_Old, FeatConstants.SpecialQualities.SoundImitation, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 23));
            testCases[CreatureConstants.Dragon_Blue_Old].Add(BuildData(CreatureConstants.Dragon_Blue_Old, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Blue_Old].Add(BuildData(CreatureConstants.Dragon_Blue_Old, FeatConstants.SpecialQualities.SpellResistance, power: 24));
            testCases[CreatureConstants.Dragon_Blue_Old].Add(BuildData(CreatureConstants.Dragon_Blue_Old, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
            testCases[CreatureConstants.Dragon_Blue_Old].Add(BuildData(CreatureConstants.Dragon_Blue_Old, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HallucinatoryTerrain, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));

            testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(BuildData(CreatureConstants.Dragon_Blue_VeryOld, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(BuildData(CreatureConstants.Dragon_Blue_VeryOld, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(BuildData(CreatureConstants.Dragon_Blue_VeryOld, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(BuildData(CreatureConstants.Dragon_Blue_VeryOld, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(BuildData(CreatureConstants.Dragon_Blue_VeryOld, FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 25));
            testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(BuildData(CreatureConstants.Dragon_Blue_VeryOld, FeatConstants.SpecialQualities.SoundImitation, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 25));
            testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(BuildData(CreatureConstants.Dragon_Blue_VeryOld, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(BuildData(CreatureConstants.Dragon_Blue_VeryOld, FeatConstants.SpecialQualities.SpellResistance, power: 25));
            testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(BuildData(CreatureConstants.Dragon_Blue_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
            testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(BuildData(CreatureConstants.Dragon_Blue_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HallucinatoryTerrain, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));

            testCases[CreatureConstants.Dragon_Blue_Ancient].Add(BuildData(CreatureConstants.Dragon_Blue_Ancient, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Blue_Ancient].Add(BuildData(CreatureConstants.Dragon_Blue_Ancient, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Blue_Ancient].Add(BuildData(CreatureConstants.Dragon_Blue_Ancient, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Blue_Ancient].Add(BuildData(CreatureConstants.Dragon_Blue_Ancient, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Dragon_Blue_Ancient].Add(BuildData(CreatureConstants.Dragon_Blue_Ancient, FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 26));
            testCases[CreatureConstants.Dragon_Blue_Ancient].Add(BuildData(CreatureConstants.Dragon_Blue_Ancient, FeatConstants.SpecialQualities.SoundImitation, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 26));
            testCases[CreatureConstants.Dragon_Blue_Ancient].Add(BuildData(CreatureConstants.Dragon_Blue_Ancient, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Blue_Ancient].Add(BuildData(CreatureConstants.Dragon_Blue_Ancient, FeatConstants.SpecialQualities.SpellResistance, power: 27));
            testCases[CreatureConstants.Dragon_Blue_Ancient].Add(BuildData(CreatureConstants.Dragon_Blue_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
            testCases[CreatureConstants.Dragon_Blue_Ancient].Add(BuildData(CreatureConstants.Dragon_Blue_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HallucinatoryTerrain, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Dragon_Blue_Ancient].Add(BuildData(CreatureConstants.Dragon_Blue_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Veil, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 16));

            testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(BuildData(CreatureConstants.Dragon_Blue_Wyrm, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(BuildData(CreatureConstants.Dragon_Blue_Wyrm, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(BuildData(CreatureConstants.Dragon_Blue_Wyrm, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(BuildData(CreatureConstants.Dragon_Blue_Wyrm, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(BuildData(CreatureConstants.Dragon_Blue_Wyrm, FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 28));
            testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(BuildData(CreatureConstants.Dragon_Blue_Wyrm, FeatConstants.SpecialQualities.SoundImitation, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 28));
            testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(BuildData(CreatureConstants.Dragon_Blue_Wyrm, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(BuildData(CreatureConstants.Dragon_Blue_Wyrm, FeatConstants.SpecialQualities.SpellResistance, power: 29));
            testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(BuildData(CreatureConstants.Dragon_Blue_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
            testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(BuildData(CreatureConstants.Dragon_Blue_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HallucinatoryTerrain, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(BuildData(CreatureConstants.Dragon_Blue_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Veil, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 16));

            testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 29));
            testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.SoundImitation, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 29));
            testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, power: 31));
            testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
            testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HallucinatoryTerrain, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Veil, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 16));
            testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MirageArcana, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));

            testCases[CreatureConstants.Dragon_Green_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Green_Wyrmling, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Green_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Green_Wyrmling, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Green_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Green_Wyrmling, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Green_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Green_Wyrmling, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Green_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Green_Wyrmling, FeatConstants.SpecialQualities.WaterBreathing));

            testCases[CreatureConstants.Dragon_Green_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Green_VeryYoung, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Green_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Green_VeryYoung, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Green_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Green_VeryYoung, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Green_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Green_VeryYoung, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Green_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Green_VeryYoung, FeatConstants.SpecialQualities.WaterBreathing));

            testCases[CreatureConstants.Dragon_Green_Young].Add(BuildData(CreatureConstants.Dragon_Green_Young, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Green_Young].Add(BuildData(CreatureConstants.Dragon_Green_Young, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Green_Young].Add(BuildData(CreatureConstants.Dragon_Green_Young, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Green_Young].Add(BuildData(CreatureConstants.Dragon_Green_Young, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Green_Young].Add(BuildData(CreatureConstants.Dragon_Green_Young, FeatConstants.SpecialQualities.WaterBreathing));

            testCases[CreatureConstants.Dragon_Green_Juvenile].Add(BuildData(CreatureConstants.Dragon_Green_Juvenile, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Green_Juvenile].Add(BuildData(CreatureConstants.Dragon_Green_Juvenile, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Green_Juvenile].Add(BuildData(CreatureConstants.Dragon_Green_Juvenile, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Green_Juvenile].Add(BuildData(CreatureConstants.Dragon_Green_Juvenile, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Green_Juvenile].Add(BuildData(CreatureConstants.Dragon_Green_Juvenile, FeatConstants.SpecialQualities.WaterBreathing));

            testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Green_YoungAdult, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Green_YoungAdult, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Green_YoungAdult, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Green_YoungAdult, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Green_YoungAdult, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Green_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Green_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, power: 19));

            testCases[CreatureConstants.Dragon_Green_Adult].Add(BuildData(CreatureConstants.Dragon_Green_Adult, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Green_Adult].Add(BuildData(CreatureConstants.Dragon_Green_Adult, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Green_Adult].Add(BuildData(CreatureConstants.Dragon_Green_Adult, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Green_Adult].Add(BuildData(CreatureConstants.Dragon_Green_Adult, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Green_Adult].Add(BuildData(CreatureConstants.Dragon_Green_Adult, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Green_Adult].Add(BuildData(CreatureConstants.Dragon_Green_Adult, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Green_Adult].Add(BuildData(CreatureConstants.Dragon_Green_Adult, FeatConstants.SpecialQualities.SpellResistance, power: 21));
            testCases[CreatureConstants.Dragon_Green_Adult].Add(BuildData(CreatureConstants.Dragon_Green_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));

            testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Green_MatureAdult, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Green_MatureAdult, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Green_MatureAdult, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Green_MatureAdult, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Green_MatureAdult, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Green_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Green_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, power: 22));
            testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Green_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));

            testCases[CreatureConstants.Dragon_Green_Old].Add(BuildData(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Green_Old].Add(BuildData(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Green_Old].Add(BuildData(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Green_Old].Add(BuildData(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Green_Old].Add(BuildData(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Green_Old].Add(BuildData(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Green_Old].Add(BuildData(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.SpellResistance, power: 24));
            testCases[CreatureConstants.Dragon_Green_Old].Add(BuildData(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Dragon_Green_Old].Add(BuildData(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Green_VeryOld].Add(BuildData(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Green_VeryOld].Add(BuildData(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Green_VeryOld].Add(BuildData(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Green_VeryOld].Add(BuildData(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Green_VeryOld].Add(BuildData(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Green_VeryOld].Add(BuildData(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Green_VeryOld].Add(BuildData(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.SpellResistance, power: 25));
            testCases[CreatureConstants.Dragon_Green_VeryOld].Add(BuildData(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Dragon_Green_VeryOld].Add(BuildData(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Green_Ancient].Add(BuildData(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Green_Ancient].Add(BuildData(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Green_Ancient].Add(BuildData(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Green_Ancient].Add(BuildData(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Green_Ancient].Add(BuildData(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Green_Ancient].Add(BuildData(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Green_Ancient].Add(BuildData(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.SpellResistance, power: 27));
            testCases[CreatureConstants.Dragon_Green_Ancient].Add(BuildData(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Dragon_Green_Ancient].Add(BuildData(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Green_Ancient].Add(BuildData(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DominatePerson, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));

            testCases[CreatureConstants.Dragon_Green_Wyrm].Add(BuildData(CreatureConstants.Dragon_Green_Wyrm, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Green_Wyrm].Add(BuildData(CreatureConstants.Dragon_Green_Wyrm, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Green_Wyrm].Add(BuildData(CreatureConstants.Dragon_Green_Wyrm, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Green_Wyrm].Add(BuildData(CreatureConstants.Dragon_Green_Wyrm, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Green_Wyrm].Add(BuildData(CreatureConstants.Dragon_Green_Wyrm, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Green_Wyrm].Add(BuildData(CreatureConstants.Dragon_Green_Wyrm, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Green_Wyrm].Add(BuildData(CreatureConstants.Dragon_Green_Wyrm, FeatConstants.SpecialQualities.SpellResistance, power: 28));
            testCases[CreatureConstants.Dragon_Green_Wyrm].Add(BuildData(CreatureConstants.Dragon_Green_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Dragon_Green_Wyrm].Add(BuildData(CreatureConstants.Dragon_Green_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Green_Wyrm].Add(BuildData(CreatureConstants.Dragon_Green_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DominatePerson, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));

            testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, power: 30));
            testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DominatePerson, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
            testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CommandPlants, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));

            testCases[CreatureConstants.Dragon_Red_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Red_Wyrmling, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Red_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Red_Wyrmling, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Red_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Red_Wyrmling, FeatConstants.SpecialQualities.Darkvision, power: 120));

            testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Red_VeryYoung, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Red_VeryYoung, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Red_VeryYoung, FeatConstants.SpecialQualities.Darkvision, power: 120));

            testCases[CreatureConstants.Dragon_Red_Young].Add(BuildData(CreatureConstants.Dragon_Red_Young, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Red_Young].Add(BuildData(CreatureConstants.Dragon_Red_Young, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Red_Young].Add(BuildData(CreatureConstants.Dragon_Red_Young, FeatConstants.SpecialQualities.Darkvision, power: 120));

            testCases[CreatureConstants.Dragon_Red_Juvenile].Add(BuildData(CreatureConstants.Dragon_Red_Juvenile, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Red_Juvenile].Add(BuildData(CreatureConstants.Dragon_Red_Juvenile, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Red_Juvenile].Add(BuildData(CreatureConstants.Dragon_Red_Juvenile, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Red_Juvenile].Add(BuildData(CreatureConstants.Dragon_Red_Juvenile, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 4, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Red_YoungAdult, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Red_YoungAdult, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Red_YoungAdult, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Red_YoungAdult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 5, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Red_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Red_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, power: 19));

            testCases[CreatureConstants.Dragon_Red_Adult].Add(BuildData(CreatureConstants.Dragon_Red_Adult, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Red_Adult].Add(BuildData(CreatureConstants.Dragon_Red_Adult, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Red_Adult].Add(BuildData(CreatureConstants.Dragon_Red_Adult, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Red_Adult].Add(BuildData(CreatureConstants.Dragon_Red_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 6, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Red_Adult].Add(BuildData(CreatureConstants.Dragon_Red_Adult, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Red_Adult].Add(BuildData(CreatureConstants.Dragon_Red_Adult, FeatConstants.SpecialQualities.SpellResistance, power: 21));

            testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Red_MatureAdult, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Red_MatureAdult, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Red_MatureAdult, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Red_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 7, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Red_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Red_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, power: 23));

            testCases[CreatureConstants.Dragon_Red_Old].Add(BuildData(CreatureConstants.Dragon_Red_Old, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Red_Old].Add(BuildData(CreatureConstants.Dragon_Red_Old, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Red_Old].Add(BuildData(CreatureConstants.Dragon_Red_Old, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Red_Old].Add(BuildData(CreatureConstants.Dragon_Red_Old, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 8, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Red_Old].Add(BuildData(CreatureConstants.Dragon_Red_Old, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Red_Old].Add(BuildData(CreatureConstants.Dragon_Red_Old, FeatConstants.SpecialQualities.SpellResistance, power: 24));
            testCases[CreatureConstants.Dragon_Red_Old].Add(BuildData(CreatureConstants.Dragon_Red_Old, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));

            testCases[CreatureConstants.Dragon_Red_VeryOld].Add(BuildData(CreatureConstants.Dragon_Red_VeryOld, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Red_VeryOld].Add(BuildData(CreatureConstants.Dragon_Red_VeryOld, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Red_VeryOld].Add(BuildData(CreatureConstants.Dragon_Red_VeryOld, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Red_VeryOld].Add(BuildData(CreatureConstants.Dragon_Red_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 9, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Red_VeryOld].Add(BuildData(CreatureConstants.Dragon_Red_VeryOld, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Red_VeryOld].Add(BuildData(CreatureConstants.Dragon_Red_VeryOld, FeatConstants.SpecialQualities.SpellResistance, power: 26));
            testCases[CreatureConstants.Dragon_Red_VeryOld].Add(BuildData(CreatureConstants.Dragon_Red_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));

            testCases[CreatureConstants.Dragon_Red_Ancient].Add(BuildData(CreatureConstants.Dragon_Red_Ancient, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Red_Ancient].Add(BuildData(CreatureConstants.Dragon_Red_Ancient, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Red_Ancient].Add(BuildData(CreatureConstants.Dragon_Red_Ancient, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Red_Ancient].Add(BuildData(CreatureConstants.Dragon_Red_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 10, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Red_Ancient].Add(BuildData(CreatureConstants.Dragon_Red_Ancient, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Red_Ancient].Add(BuildData(CreatureConstants.Dragon_Red_Ancient, FeatConstants.SpecialQualities.SpellResistance, power: 28));
            testCases[CreatureConstants.Dragon_Red_Ancient].Add(BuildData(CreatureConstants.Dragon_Red_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Dragon_Red_Ancient].Add(BuildData(CreatureConstants.Dragon_Red_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FindThePath, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 16));

            testCases[CreatureConstants.Dragon_Red_Wyrm].Add(BuildData(CreatureConstants.Dragon_Red_Wyrm, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Red_Wyrm].Add(BuildData(CreatureConstants.Dragon_Red_Wyrm, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Red_Wyrm].Add(BuildData(CreatureConstants.Dragon_Red_Wyrm, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Red_Wyrm].Add(BuildData(CreatureConstants.Dragon_Red_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 11, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Red_Wyrm].Add(BuildData(CreatureConstants.Dragon_Red_Wyrm, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Red_Wyrm].Add(BuildData(CreatureConstants.Dragon_Red_Wyrm, FeatConstants.SpecialQualities.SpellResistance, power: 30));
            testCases[CreatureConstants.Dragon_Red_Wyrm].Add(BuildData(CreatureConstants.Dragon_Red_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Dragon_Red_Wyrm].Add(BuildData(CreatureConstants.Dragon_Red_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FindThePath, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 16));

            testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Red_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Red_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Red_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Red_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 12, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Red_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Red_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, power: 32));
            testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Red_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Red_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FindThePath, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 16));
            testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Red_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DiscernLocation, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_White_Wyrmling].Add(BuildData(CreatureConstants.Dragon_White_Wyrmling, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_White_Wyrmling].Add(BuildData(CreatureConstants.Dragon_White_Wyrmling, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_White_Wyrmling].Add(BuildData(CreatureConstants.Dragon_White_Wyrmling, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_White_Wyrmling].Add(BuildData(CreatureConstants.Dragon_White_Wyrmling, FeatConstants.SpecialQualities.Icewalking));

            testCases[CreatureConstants.Dragon_White_VeryYoung].Add(BuildData(CreatureConstants.Dragon_White_VeryYoung, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_White_VeryYoung].Add(BuildData(CreatureConstants.Dragon_White_VeryYoung, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_White_VeryYoung].Add(BuildData(CreatureConstants.Dragon_White_VeryYoung, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_White_VeryYoung].Add(BuildData(CreatureConstants.Dragon_White_VeryYoung, FeatConstants.SpecialQualities.Icewalking));

            testCases[CreatureConstants.Dragon_White_Young].Add(BuildData(CreatureConstants.Dragon_White_Young, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_White_Young].Add(BuildData(CreatureConstants.Dragon_White_Young, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_White_Young].Add(BuildData(CreatureConstants.Dragon_White_Young, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_White_Young].Add(BuildData(CreatureConstants.Dragon_White_Young, FeatConstants.SpecialQualities.Icewalking));

            testCases[CreatureConstants.Dragon_White_Juvenile].Add(BuildData(CreatureConstants.Dragon_White_Juvenile, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_White_Juvenile].Add(BuildData(CreatureConstants.Dragon_White_Juvenile, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_White_Juvenile].Add(BuildData(CreatureConstants.Dragon_White_Juvenile, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_White_Juvenile].Add(BuildData(CreatureConstants.Dragon_White_Juvenile, FeatConstants.SpecialQualities.Icewalking));
            testCases[CreatureConstants.Dragon_White_Juvenile].Add(BuildData(CreatureConstants.Dragon_White_Juvenile, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_White_YoungAdult].Add(BuildData(CreatureConstants.Dragon_White_YoungAdult, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_White_YoungAdult].Add(BuildData(CreatureConstants.Dragon_White_YoungAdult, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_White_YoungAdult].Add(BuildData(CreatureConstants.Dragon_White_YoungAdult, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_White_YoungAdult].Add(BuildData(CreatureConstants.Dragon_White_YoungAdult, FeatConstants.SpecialQualities.Icewalking));
            testCases[CreatureConstants.Dragon_White_YoungAdult].Add(BuildData(CreatureConstants.Dragon_White_YoungAdult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_White_YoungAdult].Add(BuildData(CreatureConstants.Dragon_White_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_White_YoungAdult].Add(BuildData(CreatureConstants.Dragon_White_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, power: 16));

            testCases[CreatureConstants.Dragon_White_Adult].Add(BuildData(CreatureConstants.Dragon_White_Adult, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_White_Adult].Add(BuildData(CreatureConstants.Dragon_White_Adult, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_White_Adult].Add(BuildData(CreatureConstants.Dragon_White_Adult, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_White_Adult].Add(BuildData(CreatureConstants.Dragon_White_Adult, FeatConstants.SpecialQualities.Icewalking));
            testCases[CreatureConstants.Dragon_White_Adult].Add(BuildData(CreatureConstants.Dragon_White_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_White_Adult].Add(BuildData(CreatureConstants.Dragon_White_Adult, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_White_Adult].Add(BuildData(CreatureConstants.Dragon_White_Adult, FeatConstants.SpecialQualities.SpellResistance, power: 18));
            testCases[CreatureConstants.Dragon_White_Adult].Add(BuildData(CreatureConstants.Dragon_White_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 12));

            testCases[CreatureConstants.Dragon_White_MatureAdult].Add(BuildData(CreatureConstants.Dragon_White_MatureAdult, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_White_MatureAdult].Add(BuildData(CreatureConstants.Dragon_White_MatureAdult, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_White_MatureAdult].Add(BuildData(CreatureConstants.Dragon_White_MatureAdult, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_White_MatureAdult].Add(BuildData(CreatureConstants.Dragon_White_MatureAdult, FeatConstants.SpecialQualities.Icewalking));
            testCases[CreatureConstants.Dragon_White_MatureAdult].Add(BuildData(CreatureConstants.Dragon_White_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_White_MatureAdult].Add(BuildData(CreatureConstants.Dragon_White_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_White_MatureAdult].Add(BuildData(CreatureConstants.Dragon_White_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, power: 20));
            testCases[CreatureConstants.Dragon_White_MatureAdult].Add(BuildData(CreatureConstants.Dragon_White_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 12));

            testCases[CreatureConstants.Dragon_White_Old].Add(BuildData(CreatureConstants.Dragon_White_Old, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_White_Old].Add(BuildData(CreatureConstants.Dragon_White_Old, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_White_Old].Add(BuildData(CreatureConstants.Dragon_White_Old, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_White_Old].Add(BuildData(CreatureConstants.Dragon_White_Old, FeatConstants.SpecialQualities.Icewalking));
            testCases[CreatureConstants.Dragon_White_Old].Add(BuildData(CreatureConstants.Dragon_White_Old, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_White_Old].Add(BuildData(CreatureConstants.Dragon_White_Old, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_White_Old].Add(BuildData(CreatureConstants.Dragon_White_Old, FeatConstants.SpecialQualities.SpellResistance, power: 21));
            testCases[CreatureConstants.Dragon_White_Old].Add(BuildData(CreatureConstants.Dragon_White_Old, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 12));
            testCases[CreatureConstants.Dragon_White_Old].Add(BuildData(CreatureConstants.Dragon_White_Old, FeatConstants.SpecialQualities.FreezingFog, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));

            testCases[CreatureConstants.Dragon_White_VeryOld].Add(BuildData(CreatureConstants.Dragon_White_VeryOld, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_White_VeryOld].Add(BuildData(CreatureConstants.Dragon_White_VeryOld, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_White_VeryOld].Add(BuildData(CreatureConstants.Dragon_White_VeryOld, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_White_VeryOld].Add(BuildData(CreatureConstants.Dragon_White_VeryOld, FeatConstants.SpecialQualities.Icewalking));
            testCases[CreatureConstants.Dragon_White_VeryOld].Add(BuildData(CreatureConstants.Dragon_White_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_White_VeryOld].Add(BuildData(CreatureConstants.Dragon_White_VeryOld, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_White_VeryOld].Add(BuildData(CreatureConstants.Dragon_White_VeryOld, FeatConstants.SpecialQualities.SpellResistance, power: 23));
            testCases[CreatureConstants.Dragon_White_VeryOld].Add(BuildData(CreatureConstants.Dragon_White_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 12));
            testCases[CreatureConstants.Dragon_White_VeryOld].Add(BuildData(CreatureConstants.Dragon_White_VeryOld, FeatConstants.SpecialQualities.FreezingFog, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));

            testCases[CreatureConstants.Dragon_White_Ancient].Add(BuildData(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_White_Ancient].Add(BuildData(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_White_Ancient].Add(BuildData(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_White_Ancient].Add(BuildData(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.Icewalking));
            testCases[CreatureConstants.Dragon_White_Ancient].Add(BuildData(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_White_Ancient].Add(BuildData(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_White_Ancient].Add(BuildData(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.SpellResistance, power: 24));
            testCases[CreatureConstants.Dragon_White_Ancient].Add(BuildData(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 12));
            testCases[CreatureConstants.Dragon_White_Ancient].Add(BuildData(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.FreezingFog, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
            testCases[CreatureConstants.Dragon_White_Ancient].Add(BuildData(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfIce, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 14));

            testCases[CreatureConstants.Dragon_White_Wyrm].Add(BuildData(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_White_Wyrm].Add(BuildData(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_White_Wyrm].Add(BuildData(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_White_Wyrm].Add(BuildData(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.Icewalking));
            testCases[CreatureConstants.Dragon_White_Wyrm].Add(BuildData(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_White_Wyrm].Add(BuildData(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_White_Wyrm].Add(BuildData(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.SpellResistance, power: 25));
            testCases[CreatureConstants.Dragon_White_Wyrm].Add(BuildData(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 12));
            testCases[CreatureConstants.Dragon_White_Wyrm].Add(BuildData(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.FreezingFog, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
            testCases[CreatureConstants.Dragon_White_Wyrm].Add(BuildData(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfIce, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 14));

            testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.Icewalking));
            testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, power: 27));
            testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 12));
            testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.FreezingFog, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
            testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfIce, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 14));
            testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Brass_Wyrmling, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Brass_Wyrmling, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Brass_Wyrmling, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Brass_Wyrmling, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

            testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Brass_VeryYoung, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Brass_VeryYoung, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Brass_VeryYoung, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Brass_VeryYoung, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

            testCases[CreatureConstants.Dragon_Brass_Young].Add(BuildData(CreatureConstants.Dragon_Brass_Young, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Brass_Young].Add(BuildData(CreatureConstants.Dragon_Brass_Young, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Brass_Young].Add(BuildData(CreatureConstants.Dragon_Brass_Young, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Brass_Young].Add(BuildData(CreatureConstants.Dragon_Brass_Young, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

            testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(BuildData(CreatureConstants.Dragon_Brass_Juvenile, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(BuildData(CreatureConstants.Dragon_Brass_Juvenile, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(BuildData(CreatureConstants.Dragon_Brass_Juvenile, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(BuildData(CreatureConstants.Dragon_Brass_Juvenile, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(BuildData(CreatureConstants.Dragon_Brass_Juvenile, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 40 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Brass_YoungAdult, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Brass_YoungAdult, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Brass_YoungAdult, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Brass_YoungAdult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Brass_YoungAdult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 50 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Brass_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Brass_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, power: 18));

            testCases[CreatureConstants.Dragon_Brass_Adult].Add(BuildData(CreatureConstants.Dragon_Brass_Adult, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Brass_Adult].Add(BuildData(CreatureConstants.Dragon_Brass_Adult, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Brass_Adult].Add(BuildData(CreatureConstants.Dragon_Brass_Adult, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Brass_Adult].Add(BuildData(CreatureConstants.Dragon_Brass_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Dragon_Brass_Adult].Add(BuildData(CreatureConstants.Dragon_Brass_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 60 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Brass_Adult].Add(BuildData(CreatureConstants.Dragon_Brass_Adult, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Brass_Adult].Add(BuildData(CreatureConstants.Dragon_Brass_Adult, FeatConstants.SpecialQualities.SpellResistance, power: 20));
            testCases[CreatureConstants.Dragon_Brass_Adult].Add(BuildData(CreatureConstants.Dragon_Brass_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));

            testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Brass_MatureAdult, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Brass_MatureAdult, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Brass_MatureAdult, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Brass_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Brass_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 70 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Brass_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Brass_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, power: 22));
            testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Brass_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));

            testCases[CreatureConstants.Dragon_Brass_Old].Add(BuildData(CreatureConstants.Dragon_Brass_Old, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Brass_Old].Add(BuildData(CreatureConstants.Dragon_Brass_Old, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Brass_Old].Add(BuildData(CreatureConstants.Dragon_Brass_Old, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Brass_Old].Add(BuildData(CreatureConstants.Dragon_Brass_Old, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Dragon_Brass_Old].Add(BuildData(CreatureConstants.Dragon_Brass_Old, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 80 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Brass_Old].Add(BuildData(CreatureConstants.Dragon_Brass_Old, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Brass_Old].Add(BuildData(CreatureConstants.Dragon_Brass_Old, FeatConstants.SpecialQualities.SpellResistance, power: 24));
            testCases[CreatureConstants.Dragon_Brass_Old].Add(BuildData(CreatureConstants.Dragon_Brass_Old, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Dragon_Brass_Old].Add(BuildData(CreatureConstants.Dragon_Brass_Old, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 15));

            testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(BuildData(CreatureConstants.Dragon_Brass_VeryOld, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(BuildData(CreatureConstants.Dragon_Brass_VeryOld, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(BuildData(CreatureConstants.Dragon_Brass_VeryOld, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(BuildData(CreatureConstants.Dragon_Brass_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(BuildData(CreatureConstants.Dragon_Brass_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 90 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(BuildData(CreatureConstants.Dragon_Brass_VeryOld, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(BuildData(CreatureConstants.Dragon_Brass_VeryOld, FeatConstants.SpecialQualities.SpellResistance, power: 25));
            testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(BuildData(CreatureConstants.Dragon_Brass_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(BuildData(CreatureConstants.Dragon_Brass_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 15));

            testCases[CreatureConstants.Dragon_Brass_Ancient].Add(BuildData(CreatureConstants.Dragon_Brass_Ancient, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Brass_Ancient].Add(BuildData(CreatureConstants.Dragon_Brass_Ancient, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Brass_Ancient].Add(BuildData(CreatureConstants.Dragon_Brass_Ancient, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Brass_Ancient].Add(BuildData(CreatureConstants.Dragon_Brass_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Dragon_Brass_Ancient].Add(BuildData(CreatureConstants.Dragon_Brass_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 100 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Brass_Ancient].Add(BuildData(CreatureConstants.Dragon_Brass_Ancient, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Brass_Ancient].Add(BuildData(CreatureConstants.Dragon_Brass_Ancient, FeatConstants.SpecialQualities.SpellResistance, power: 27));
            testCases[CreatureConstants.Dragon_Brass_Ancient].Add(BuildData(CreatureConstants.Dragon_Brass_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Dragon_Brass_Ancient].Add(BuildData(CreatureConstants.Dragon_Brass_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 15));
            testCases[CreatureConstants.Dragon_Brass_Ancient].Add(BuildData(CreatureConstants.Dragon_Brass_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(BuildData(CreatureConstants.Dragon_Brass_Wyrm, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(BuildData(CreatureConstants.Dragon_Brass_Wyrm, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(BuildData(CreatureConstants.Dragon_Brass_Wyrm, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(BuildData(CreatureConstants.Dragon_Brass_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(BuildData(CreatureConstants.Dragon_Brass_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 110 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(BuildData(CreatureConstants.Dragon_Brass_Wyrm, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(BuildData(CreatureConstants.Dragon_Brass_Wyrm, FeatConstants.SpecialQualities.SpellResistance, power: 28));
            testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(BuildData(CreatureConstants.Dragon_Brass_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(BuildData(CreatureConstants.Dragon_Brass_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 15));
            testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(BuildData(CreatureConstants.Dragon_Brass_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Brass_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Brass_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Brass_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Brass_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Brass_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 120 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Brass_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Brass_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, power: 30));
            testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Brass_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Brass_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 15));
            testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Brass_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Brass_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SummonMonsterVII + ": one Djinni", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Bronze_Wyrmling, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Bronze_Wyrmling, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Bronze_Wyrmling, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Bronze_Wyrmling, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Bronze_Wyrmling, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Bronze_Wyrmling, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

            testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Bronze_VeryYoung, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Bronze_VeryYoung, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Bronze_VeryYoung, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Bronze_VeryYoung, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Bronze_VeryYoung, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Bronze_VeryYoung, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

            testCases[CreatureConstants.Dragon_Bronze_Young].Add(BuildData(CreatureConstants.Dragon_Bronze_Young, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Bronze_Young].Add(BuildData(CreatureConstants.Dragon_Bronze_Young, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Bronze_Young].Add(BuildData(CreatureConstants.Dragon_Bronze_Young, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Bronze_Young].Add(BuildData(CreatureConstants.Dragon_Bronze_Young, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Dragon_Bronze_Young].Add(BuildData(CreatureConstants.Dragon_Bronze_Young, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Bronze_Young].Add(BuildData(CreatureConstants.Dragon_Bronze_Young, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Dragon_Bronze_Young].Add(BuildData(CreatureConstants.Dragon_Bronze_Young, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(BuildData(CreatureConstants.Dragon_Bronze_Juvenile, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(BuildData(CreatureConstants.Dragon_Bronze_Juvenile, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(BuildData(CreatureConstants.Dragon_Bronze_Juvenile, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(BuildData(CreatureConstants.Dragon_Bronze_Juvenile, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(BuildData(CreatureConstants.Dragon_Bronze_Juvenile, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(BuildData(CreatureConstants.Dragon_Bronze_Juvenile, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(BuildData(CreatureConstants.Dragon_Bronze_Juvenile, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Bronze_YoungAdult, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Bronze_YoungAdult, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Bronze_YoungAdult, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Bronze_YoungAdult, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Bronze_YoungAdult, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Bronze_YoungAdult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Bronze_YoungAdult, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Bronze_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Bronze_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, power: 20));

            testCases[CreatureConstants.Dragon_Bronze_Adult].Add(BuildData(CreatureConstants.Dragon_Bronze_Adult, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Bronze_Adult].Add(BuildData(CreatureConstants.Dragon_Bronze_Adult, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Bronze_Adult].Add(BuildData(CreatureConstants.Dragon_Bronze_Adult, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Bronze_Adult].Add(BuildData(CreatureConstants.Dragon_Bronze_Adult, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Dragon_Bronze_Adult].Add(BuildData(CreatureConstants.Dragon_Bronze_Adult, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Bronze_Adult].Add(BuildData(CreatureConstants.Dragon_Bronze_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Dragon_Bronze_Adult].Add(BuildData(CreatureConstants.Dragon_Bronze_Adult, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Bronze_Adult].Add(BuildData(CreatureConstants.Dragon_Bronze_Adult, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Bronze_Adult].Add(BuildData(CreatureConstants.Dragon_Bronze_Adult, FeatConstants.SpecialQualities.SpellResistance, power: 22));
            testCases[CreatureConstants.Dragon_Bronze_Adult].Add(BuildData(CreatureConstants.Dragon_Bronze_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateFoodAndWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Bronze_Adult].Add(BuildData(CreatureConstants.Dragon_Bronze_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Bronze_MatureAdult, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Bronze_MatureAdult, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Bronze_MatureAdult, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Bronze_MatureAdult, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Bronze_MatureAdult, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Bronze_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Bronze_MatureAdult, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Bronze_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Bronze_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, power: 23));
            testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Bronze_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateFoodAndWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Bronze_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Bronze_Old].Add(BuildData(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Bronze_Old].Add(BuildData(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Bronze_Old].Add(BuildData(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Bronze_Old].Add(BuildData(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Dragon_Bronze_Old].Add(BuildData(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Bronze_Old].Add(BuildData(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Dragon_Bronze_Old].Add(BuildData(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Bronze_Old].Add(BuildData(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Bronze_Old].Add(BuildData(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.SpellResistance, power: 25));
            testCases[CreatureConstants.Dragon_Bronze_Old].Add(BuildData(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateFoodAndWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Bronze_Old].Add(BuildData(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Bronze_Old].Add(BuildData(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));

            testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(BuildData(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(BuildData(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(BuildData(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(BuildData(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(BuildData(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(BuildData(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(BuildData(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(BuildData(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(BuildData(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.SpellResistance, power: 26));
            testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(BuildData(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateFoodAndWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(BuildData(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(BuildData(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));

            testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(BuildData(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(BuildData(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(BuildData(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(BuildData(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(BuildData(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(BuildData(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(BuildData(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(BuildData(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(BuildData(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.SpellResistance, power: 28));
            testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(BuildData(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateFoodAndWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(BuildData(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(BuildData(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(BuildData(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));

            testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(BuildData(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(BuildData(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(BuildData(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(BuildData(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(BuildData(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(BuildData(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(BuildData(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(BuildData(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(BuildData(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.SpellResistance, power: 29));
            testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(BuildData(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateFoodAndWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(BuildData(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(BuildData(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(BuildData(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));

            testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, power: 31));
            testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateFoodAndWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Copper_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Copper_Wyrmling, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Copper_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Copper_Wyrmling, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Copper_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Copper_Wyrmling, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Copper_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Copper_Wyrmling, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Copper_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Copper_Wyrmling, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));

            testCases[CreatureConstants.Dragon_Copper_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Copper_VeryYoung, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Copper_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Copper_VeryYoung, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Copper_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Copper_VeryYoung, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Copper_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Copper_VeryYoung, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Copper_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Copper_VeryYoung, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));

            testCases[CreatureConstants.Dragon_Copper_Young].Add(BuildData(CreatureConstants.Dragon_Copper_Young, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Copper_Young].Add(BuildData(CreatureConstants.Dragon_Copper_Young, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Copper_Young].Add(BuildData(CreatureConstants.Dragon_Copper_Young, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Copper_Young].Add(BuildData(CreatureConstants.Dragon_Copper_Young, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Copper_Young].Add(BuildData(CreatureConstants.Dragon_Copper_Young, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));

            testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(BuildData(CreatureConstants.Dragon_Copper_Juvenile, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(BuildData(CreatureConstants.Dragon_Copper_Juvenile, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(BuildData(CreatureConstants.Dragon_Copper_Juvenile, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(BuildData(CreatureConstants.Dragon_Copper_Juvenile, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(BuildData(CreatureConstants.Dragon_Copper_Juvenile, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));

            testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Copper_YoungAdult, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Copper_YoungAdult, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Copper_YoungAdult, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Copper_YoungAdult, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Copper_YoungAdult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
            testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Copper_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Copper_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, power: 19));

            testCases[CreatureConstants.Dragon_Copper_Adult].Add(BuildData(CreatureConstants.Dragon_Copper_Adult, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Copper_Adult].Add(BuildData(CreatureConstants.Dragon_Copper_Adult, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Copper_Adult].Add(BuildData(CreatureConstants.Dragon_Copper_Adult, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Copper_Adult].Add(BuildData(CreatureConstants.Dragon_Copper_Adult, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Copper_Adult].Add(BuildData(CreatureConstants.Dragon_Copper_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
            testCases[CreatureConstants.Dragon_Copper_Adult].Add(BuildData(CreatureConstants.Dragon_Copper_Adult, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Copper_Adult].Add(BuildData(CreatureConstants.Dragon_Copper_Adult, FeatConstants.SpecialQualities.SpellResistance, power: 21));
            testCases[CreatureConstants.Dragon_Copper_Adult].Add(BuildData(CreatureConstants.Dragon_Copper_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneShape, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Copper_MatureAdult, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Copper_MatureAdult, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Copper_MatureAdult, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Copper_MatureAdult, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Copper_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
            testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Copper_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Copper_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, power: 23));
            testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Copper_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneShape, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Copper_Old].Add(BuildData(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Copper_Old].Add(BuildData(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Copper_Old].Add(BuildData(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Copper_Old].Add(BuildData(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Copper_Old].Add(BuildData(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
            testCases[CreatureConstants.Dragon_Copper_Old].Add(BuildData(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Copper_Old].Add(BuildData(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.SpellResistance, power: 25));
            testCases[CreatureConstants.Dragon_Copper_Old].Add(BuildData(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneShape, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Copper_Old].Add(BuildData(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteMudToRock, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
            testCases[CreatureConstants.Dragon_Copper_Old].Add(BuildData(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteRockToMud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));

            testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(BuildData(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(BuildData(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(BuildData(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(BuildData(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(BuildData(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
            testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(BuildData(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(BuildData(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.SpellResistance, power: 26));
            testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(BuildData(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneShape, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(BuildData(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteMudToRock, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
            testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(BuildData(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteRockToMud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));

            testCases[CreatureConstants.Dragon_Copper_Ancient].Add(BuildData(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Copper_Ancient].Add(BuildData(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Copper_Ancient].Add(BuildData(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Copper_Ancient].Add(BuildData(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Copper_Ancient].Add(BuildData(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
            testCases[CreatureConstants.Dragon_Copper_Ancient].Add(BuildData(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Copper_Ancient].Add(BuildData(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.SpellResistance, power: 28));
            testCases[CreatureConstants.Dragon_Copper_Ancient].Add(BuildData(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneShape, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Copper_Ancient].Add(BuildData(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteMudToRock, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
            testCases[CreatureConstants.Dragon_Copper_Ancient].Add(BuildData(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteRockToMud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
            testCases[CreatureConstants.Dragon_Copper_Ancient].Add(BuildData(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfStone, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));

            testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(BuildData(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(BuildData(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(BuildData(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(BuildData(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(BuildData(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
            testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(BuildData(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(BuildData(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.SpellResistance, power: 29));
            testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(BuildData(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneShape, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(BuildData(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteMudToRock, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
            testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(BuildData(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteRockToMud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
            testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(BuildData(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfStone, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));

            testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
            testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, power: 31));
            testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneShape, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteMudToRock, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
            testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteRockToMud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
            testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfStone, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
            testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MoveEarth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Gold_Wyrmling, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Gold_Wyrmling, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Gold_Wyrmling, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Gold_Wyrmling, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Gold_Wyrmling, FeatConstants.SpecialQualities.WaterBreathing));

            testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Gold_VeryYoung, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Gold_VeryYoung, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Gold_VeryYoung, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Gold_VeryYoung, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Gold_VeryYoung, FeatConstants.SpecialQualities.WaterBreathing));

            testCases[CreatureConstants.Dragon_Gold_Young].Add(BuildData(CreatureConstants.Dragon_Gold_Young, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Gold_Young].Add(BuildData(CreatureConstants.Dragon_Gold_Young, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Gold_Young].Add(BuildData(CreatureConstants.Dragon_Gold_Young, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Gold_Young].Add(BuildData(CreatureConstants.Dragon_Gold_Young, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Gold_Young].Add(BuildData(CreatureConstants.Dragon_Gold_Young, FeatConstants.SpecialQualities.WaterBreathing));

            testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(BuildData(CreatureConstants.Dragon_Gold_Juvenile, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(BuildData(CreatureConstants.Dragon_Gold_Juvenile, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(BuildData(CreatureConstants.Dragon_Gold_Juvenile, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(BuildData(CreatureConstants.Dragon_Gold_Juvenile, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(BuildData(CreatureConstants.Dragon_Gold_Juvenile, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(BuildData(CreatureConstants.Dragon_Gold_Juvenile, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Gold_YoungAdult, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Gold_YoungAdult, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Gold_YoungAdult, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Gold_YoungAdult, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Gold_YoungAdult, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Gold_YoungAdult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Gold_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Gold_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, power: 21));

            testCases[CreatureConstants.Dragon_Gold_Adult].Add(BuildData(CreatureConstants.Dragon_Gold_Adult, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Gold_Adult].Add(BuildData(CreatureConstants.Dragon_Gold_Adult, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Gold_Adult].Add(BuildData(CreatureConstants.Dragon_Gold_Adult, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Gold_Adult].Add(BuildData(CreatureConstants.Dragon_Gold_Adult, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Gold_Adult].Add(BuildData(CreatureConstants.Dragon_Gold_Adult, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Gold_Adult].Add(BuildData(CreatureConstants.Dragon_Gold_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Gold_Adult].Add(BuildData(CreatureConstants.Dragon_Gold_Adult, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Gold_Adult].Add(BuildData(CreatureConstants.Dragon_Gold_Adult, FeatConstants.SpecialQualities.SpellResistance, power: 23));
            testCases[CreatureConstants.Dragon_Gold_Adult].Add(BuildData(CreatureConstants.Dragon_Gold_Adult, FeatConstants.SpecialQualities.LuckBonus, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Gold_MatureAdult, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Gold_MatureAdult, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Gold_MatureAdult, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Gold_MatureAdult, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Gold_MatureAdult, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Gold_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Gold_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Gold_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, power: 25));
            testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Gold_MatureAdult, FeatConstants.SpecialQualities.LuckBonus, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Gold_Old].Add(BuildData(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Gold_Old].Add(BuildData(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Gold_Old].Add(BuildData(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Gold_Old].Add(BuildData(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Gold_Old].Add(BuildData(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Gold_Old].Add(BuildData(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Gold_Old].Add(BuildData(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Gold_Old].Add(BuildData(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.SpellResistance, power: 27));
            testCases[CreatureConstants.Dragon_Gold_Old].Add(BuildData(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.LuckBonus, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Gold_Old].Add(BuildData(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GeasQuest, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(BuildData(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(BuildData(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(BuildData(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(BuildData(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(BuildData(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(BuildData(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(BuildData(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(BuildData(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.SpellResistance, power: 28));
            testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(BuildData(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.LuckBonus, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(BuildData(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GeasQuest, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Gold_Ancient].Add(BuildData(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Gold_Ancient].Add(BuildData(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Gold_Ancient].Add(BuildData(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Gold_Ancient].Add(BuildData(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Gold_Ancient].Add(BuildData(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Gold_Ancient].Add(BuildData(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Gold_Ancient].Add(BuildData(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Gold_Ancient].Add(BuildData(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.SpellResistance, power: 30));
            testCases[CreatureConstants.Dragon_Gold_Ancient].Add(BuildData(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.LuckBonus, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Gold_Ancient].Add(BuildData(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GeasQuest, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Gold_Ancient].Add(BuildData(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Sunburst, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 18));

            testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(BuildData(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(BuildData(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(BuildData(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(BuildData(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(BuildData(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(BuildData(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(BuildData(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(BuildData(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.SpellResistance, power: 31));
            testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(BuildData(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.LuckBonus, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(BuildData(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GeasQuest, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(BuildData(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Sunburst, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 18));

            testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, power: 33));
            testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.LuckBonus, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GeasQuest, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Sunburst, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 18));
            testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Foresight, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Silver_Wyrmling, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Silver_Wyrmling, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Silver_Wyrmling, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Silver_Wyrmling, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Silver_Wyrmling, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(BuildData(CreatureConstants.Dragon_Silver_Wyrmling, FeatConstants.SpecialQualities.Cloudwalking));

            testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Silver_VeryYoung, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Silver_VeryYoung, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Silver_VeryYoung, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Silver_VeryYoung, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Silver_VeryYoung, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(BuildData(CreatureConstants.Dragon_Silver_VeryYoung, FeatConstants.SpecialQualities.Cloudwalking));

            testCases[CreatureConstants.Dragon_Silver_Young].Add(BuildData(CreatureConstants.Dragon_Silver_Young, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Silver_Young].Add(BuildData(CreatureConstants.Dragon_Silver_Young, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Silver_Young].Add(BuildData(CreatureConstants.Dragon_Silver_Young, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Silver_Young].Add(BuildData(CreatureConstants.Dragon_Silver_Young, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Silver_Young].Add(BuildData(CreatureConstants.Dragon_Silver_Young, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Silver_Young].Add(BuildData(CreatureConstants.Dragon_Silver_Young, FeatConstants.SpecialQualities.Cloudwalking));

            testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(BuildData(CreatureConstants.Dragon_Silver_Juvenile, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(BuildData(CreatureConstants.Dragon_Silver_Juvenile, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(BuildData(CreatureConstants.Dragon_Silver_Juvenile, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(BuildData(CreatureConstants.Dragon_Silver_Juvenile, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(BuildData(CreatureConstants.Dragon_Silver_Juvenile, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(BuildData(CreatureConstants.Dragon_Silver_Juvenile, FeatConstants.SpecialQualities.Cloudwalking));
            testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(BuildData(CreatureConstants.Dragon_Silver_Juvenile, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Silver_YoungAdult, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Silver_YoungAdult, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Silver_YoungAdult, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Silver_YoungAdult, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Silver_YoungAdult, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Silver_YoungAdult, FeatConstants.SpecialQualities.Cloudwalking));
            testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Silver_YoungAdult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Silver_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(BuildData(CreatureConstants.Dragon_Silver_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, power: 20));

            testCases[CreatureConstants.Dragon_Silver_Adult].Add(BuildData(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Silver_Adult].Add(BuildData(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Silver_Adult].Add(BuildData(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Silver_Adult].Add(BuildData(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Silver_Adult].Add(BuildData(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Silver_Adult].Add(BuildData(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.Cloudwalking));
            testCases[CreatureConstants.Dragon_Silver_Adult].Add(BuildData(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Silver_Adult].Add(BuildData(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Silver_Adult].Add(BuildData(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.SpellResistance, power: 22));
            testCases[CreatureConstants.Dragon_Silver_Adult].Add(BuildData(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.Cloudwalking));
            testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, power: 24));
            testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(BuildData(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Silver_Old].Add(BuildData(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Silver_Old].Add(BuildData(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Silver_Old].Add(BuildData(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Silver_Old].Add(BuildData(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Silver_Old].Add(BuildData(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Silver_Old].Add(BuildData(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.Cloudwalking));
            testCases[CreatureConstants.Dragon_Silver_Old].Add(BuildData(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Silver_Old].Add(BuildData(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Silver_Old].Add(BuildData(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.SpellResistance, power: 26));
            testCases[CreatureConstants.Dragon_Silver_Old].Add(BuildData(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Silver_Old].Add(BuildData(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 15));

            testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(BuildData(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(BuildData(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(BuildData(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(BuildData(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(BuildData(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(BuildData(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.Cloudwalking));
            testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(BuildData(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(BuildData(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(BuildData(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.SpellResistance, power: 27));
            testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(BuildData(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(BuildData(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 15));

            testCases[CreatureConstants.Dragon_Silver_Ancient].Add(BuildData(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Silver_Ancient].Add(BuildData(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Silver_Ancient].Add(BuildData(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Silver_Ancient].Add(BuildData(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Silver_Ancient].Add(BuildData(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Silver_Ancient].Add(BuildData(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.Cloudwalking));
            testCases[CreatureConstants.Dragon_Silver_Ancient].Add(BuildData(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Silver_Ancient].Add(BuildData(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Silver_Ancient].Add(BuildData(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.SpellResistance, power: 29));
            testCases[CreatureConstants.Dragon_Silver_Ancient].Add(BuildData(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Silver_Ancient].Add(BuildData(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 15));
            testCases[CreatureConstants.Dragon_Silver_Ancient].Add(BuildData(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(BuildData(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(BuildData(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(BuildData(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(BuildData(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(BuildData(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(BuildData(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.Cloudwalking));
            testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(BuildData(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(BuildData(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(BuildData(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.SpellResistance, power: 30));
            testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(BuildData(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(BuildData(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 15));
            testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(BuildData(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses));
            testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.Cloudwalking));
            testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, power: 32));
            testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 15));
            testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(BuildData(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ReverseGravity, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 17));

            testCases[CreatureConstants.Dragonne].Add(BuildData(CreatureConstants.Dragonne, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Dretch].Add(BuildData(CreatureConstants.Dretch, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Dretch].Add(BuildData(CreatureConstants.Dretch, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.Dretch].Add(BuildData(CreatureConstants.Dretch, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Dretch].Add(BuildData(CreatureConstants.Dretch, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Dretch].Add(BuildData(CreatureConstants.Dretch, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Dretch].Add(BuildData(CreatureConstants.Dretch, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good or cold iron weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dretch].Add(BuildData(CreatureConstants.Dretch, FeatConstants.SpecialQualities.Telepathy, power: 100));
            testCases[CreatureConstants.Dretch].Add(BuildData(CreatureConstants.Dretch, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Scare, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.Dretch].Add(BuildData(CreatureConstants.Dretch, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StinkingCloud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 13));

            testCases[CreatureConstants.Drider].Add(BuildData(CreatureConstants.Drider, FeatConstants.SpecialQualities.SpellResistance, power: 17));
            testCases[CreatureConstants.Drider].Add(BuildData(CreatureConstants.Drider, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DancingLights, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 10));
            testCases[CreatureConstants.Drider].Add(BuildData(CreatureConstants.Drider, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ClairaudienceClairvoyance, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Drider].Add(BuildData(CreatureConstants.Drider, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Drider].Add(BuildData(CreatureConstants.Drider, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectGood, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Drider].Add(BuildData(CreatureConstants.Drider, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectLaw, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Drider].Add(BuildData(CreatureConstants.Drider, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Drider].Add(BuildData(CreatureConstants.Drider, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Drider].Add(BuildData(CreatureConstants.Drider, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FaerieFire, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Drider].Add(BuildData(CreatureConstants.Drider, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Levitate, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Drider].Add(BuildData(CreatureConstants.Drider, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Drider].Add(BuildData(CreatureConstants.Drider, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Dagger, requiresEquipment: true));
            testCases[CreatureConstants.Drider].Add(BuildData(CreatureConstants.Drider, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));

            testCases[CreatureConstants.Dryad].Add(BuildData(CreatureConstants.Dryad, FeatConstants.SpecialQualities.DamageReduction, power: 5, focus: "Vulnerable to cold iron weapons", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Dryad].Add(BuildData(CreatureConstants.Dryad, FeatConstants.SpecialQualities.TreeDependent));
            testCases[CreatureConstants.Dryad].Add(BuildData(CreatureConstants.Dryad, FeatConstants.SpecialQualities.WildEmpathy));
            testCases[CreatureConstants.Dryad].Add(BuildData(CreatureConstants.Dryad, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Entangle, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 11));
            testCases[CreatureConstants.Dryad].Add(BuildData(CreatureConstants.Dryad, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithPlants, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Dryad].Add(BuildData(CreatureConstants.Dryad, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TreeShape, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Dryad].Add(BuildData(CreatureConstants.Dryad, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmPerson, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
            testCases[CreatureConstants.Dryad].Add(BuildData(CreatureConstants.Dryad, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeepSlumber, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Dryad].Add(BuildData(CreatureConstants.Dryad, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TreeStride, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dryad].Add(BuildData(CreatureConstants.Dryad, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Dryad].Add(BuildData(CreatureConstants.Dryad, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Dagger, requiresEquipment: true));
            testCases[CreatureConstants.Dryad].Add(BuildData(CreatureConstants.Dryad, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));

            testCases[CreatureConstants.Dwarf_Deep].Add(BuildData(CreatureConstants.Dwarf_Deep, FeatConstants.SpecialQualities.Darkvision, power: 90));
            testCases[CreatureConstants.Dwarf_Deep].Add(BuildData(CreatureConstants.Dwarf_Deep, FeatConstants.SpecialQualities.WeaponFamiliarity, focus: WeaponConstants.DwarvenUrgrosh, requiresEquipment: true));
            testCases[CreatureConstants.Dwarf_Deep].Add(BuildData(CreatureConstants.Dwarf_Deep, FeatConstants.SpecialQualities.WeaponFamiliarity, focus: WeaponConstants.DwarvenWaraxe, requiresEquipment: true));
            testCases[CreatureConstants.Dwarf_Deep].Add(BuildData(CreatureConstants.Dwarf_Deep, FeatConstants.SpecialQualities.LightSensitivity));
            testCases[CreatureConstants.Dwarf_Deep].Add(BuildData(CreatureConstants.Dwarf_Deep, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.DwarvenWaraxe, requiresEquipment: true));
            testCases[CreatureConstants.Dwarf_Deep].Add(BuildData(CreatureConstants.Dwarf_Deep, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));

            testCases[CreatureConstants.Dwarf_Duergar].Add(BuildData(CreatureConstants.Dwarf_Duergar, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Dwarf_Duergar].Add(BuildData(CreatureConstants.Dwarf_Duergar, FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
            testCases[CreatureConstants.Dwarf_Duergar].Add(BuildData(CreatureConstants.Dwarf_Duergar, FeatConstants.SpecialQualities.Immunity, focus: "Phantasms"));
            testCases[CreatureConstants.Dwarf_Duergar].Add(BuildData(CreatureConstants.Dwarf_Duergar, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.Dwarf_Duergar].Add(BuildData(CreatureConstants.Dwarf_Duergar, FeatConstants.SpecialQualities.LightSensitivity));
            testCases[CreatureConstants.Dwarf_Duergar].Add(BuildData(CreatureConstants.Dwarf_Duergar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EnlargePerson + ": only self + carried items", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dwarf_Duergar].Add(BuildData(CreatureConstants.Dwarf_Duergar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + ": only self + carried items", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Dwarf_Duergar].Add(BuildData(CreatureConstants.Dwarf_Duergar, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Warhammer, requiresEquipment: true));
            testCases[CreatureConstants.Dwarf_Duergar].Add(BuildData(CreatureConstants.Dwarf_Duergar, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.LightCrossbow, requiresEquipment: true));

            testCases[CreatureConstants.Dwarf_Hill].Add(BuildData(CreatureConstants.Dwarf_Hill, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Dwarf_Hill].Add(BuildData(CreatureConstants.Dwarf_Hill, FeatConstants.SpecialQualities.WeaponFamiliarity, focus: WeaponConstants.DwarvenUrgrosh, requiresEquipment: true));
            testCases[CreatureConstants.Dwarf_Hill].Add(BuildData(CreatureConstants.Dwarf_Hill, FeatConstants.SpecialQualities.WeaponFamiliarity, focus: WeaponConstants.DwarvenWaraxe, requiresEquipment: true));
            testCases[CreatureConstants.Dwarf_Hill].Add(BuildData(CreatureConstants.Dwarf_Hill, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.DwarvenWaraxe, requiresEquipment: true));
            testCases[CreatureConstants.Dwarf_Hill].Add(BuildData(CreatureConstants.Dwarf_Hill, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));

            testCases[CreatureConstants.Dwarf_Mountain].Add(BuildData(CreatureConstants.Dwarf_Mountain, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Dwarf_Mountain].Add(BuildData(CreatureConstants.Dwarf_Mountain, FeatConstants.SpecialQualities.WeaponFamiliarity, focus: WeaponConstants.DwarvenUrgrosh, requiresEquipment: true));
            testCases[CreatureConstants.Dwarf_Mountain].Add(BuildData(CreatureConstants.Dwarf_Mountain, FeatConstants.SpecialQualities.WeaponFamiliarity, focus: WeaponConstants.DwarvenWaraxe, requiresEquipment: true));
            testCases[CreatureConstants.Dwarf_Mountain].Add(BuildData(CreatureConstants.Dwarf_Mountain, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.DwarvenWaraxe, requiresEquipment: true));
            testCases[CreatureConstants.Dwarf_Mountain].Add(BuildData(CreatureConstants.Dwarf_Mountain, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));

            testCases[CreatureConstants.Eagle].Add(BuildData(CreatureConstants.Eagle, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Eagle_Giant].Add(BuildData(CreatureConstants.Eagle_Giant, FeatConstants.SpecialQualities.Evasion));

            testCases[CreatureConstants.Efreeti].Add(BuildData(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.ChangeShape, focus: "Any Small, Medium, or Large Humanoid or Giant", frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Efreeti].Add(BuildData(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.Telepathy, power: 100));
            testCases[CreatureConstants.Efreeti].Add(BuildData(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlaneShift + ": Genie and up to 8 other creatures, provided they all link hands with the genie", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Efreeti].Add(BuildData(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Efreeti].Add(BuildData(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ProduceFlame, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Efreeti].Add(BuildData(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Pyrotechnics, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, saveBaseValue: 12));
            testCases[CreatureConstants.Efreeti].Add(BuildData(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ScorchingRay + ": 1 ray only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Efreeti].Add(BuildData(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Efreeti].Add(BuildData(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfFire, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, saveBaseValue: 14));
            testCases[CreatureConstants.Efreeti].Add(BuildData(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Wish + ": Grant up to 3 wishes to nongenies", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Efreeti].Add(BuildData(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PermanentImage, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 16));
            testCases[CreatureConstants.Efreeti].Add(BuildData(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GaseousForm, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Efreeti].Add(BuildData(CreatureConstants.Efreeti, FeatConstants.Initiative_Improved, power: 4));

            testCases[CreatureConstants.Elasmosaurus].Add(BuildData(CreatureConstants.Elasmosaurus, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Elemental_Air_Small].Add(BuildData(CreatureConstants.Elemental_Air_Small, FeatConstants.WeaponFinesse));
            testCases[CreatureConstants.Elemental_Air_Small].Add(BuildData(CreatureConstants.Elemental_Air_Small, FeatConstants.Initiative_Improved, power: 4));

            testCases[CreatureConstants.Elemental_Air_Medium].Add(BuildData(CreatureConstants.Elemental_Air_Medium, FeatConstants.WeaponFinesse));
            testCases[CreatureConstants.Elemental_Air_Medium].Add(BuildData(CreatureConstants.Elemental_Air_Medium, FeatConstants.Initiative_Improved, power: 4));

            testCases[CreatureConstants.Elemental_Air_Large].Add(BuildData(CreatureConstants.Elemental_Air_Large, FeatConstants.WeaponFinesse));
            testCases[CreatureConstants.Elemental_Air_Large].Add(BuildData(CreatureConstants.Elemental_Air_Large, FeatConstants.Initiative_Improved, power: 4));
            testCases[CreatureConstants.Elemental_Air_Large].Add(BuildData(CreatureConstants.Elemental_Air_Large, FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

            testCases[CreatureConstants.Elemental_Air_Huge].Add(BuildData(CreatureConstants.Elemental_Air_Huge, FeatConstants.WeaponFinesse));
            testCases[CreatureConstants.Elemental_Air_Huge].Add(BuildData(CreatureConstants.Elemental_Air_Huge, FeatConstants.Initiative_Improved, power: 4));
            testCases[CreatureConstants.Elemental_Air_Huge].Add(BuildData(CreatureConstants.Elemental_Air_Huge, FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

            testCases[CreatureConstants.Elemental_Air_Greater].Add(BuildData(CreatureConstants.Elemental_Air_Greater, FeatConstants.WeaponFinesse));
            testCases[CreatureConstants.Elemental_Air_Greater].Add(BuildData(CreatureConstants.Elemental_Air_Greater, FeatConstants.Initiative_Improved, power: 4));
            testCases[CreatureConstants.Elemental_Air_Greater].Add(BuildData(CreatureConstants.Elemental_Air_Greater, FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

            testCases[CreatureConstants.Elemental_Air_Elder].Add(BuildData(CreatureConstants.Elemental_Air_Elder, FeatConstants.WeaponFinesse));
            testCases[CreatureConstants.Elemental_Air_Elder].Add(BuildData(CreatureConstants.Elemental_Air_Elder, FeatConstants.Initiative_Improved, power: 4));
            testCases[CreatureConstants.Elemental_Air_Elder].Add(BuildData(CreatureConstants.Elemental_Air_Elder, FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

            testCases[CreatureConstants.Elemental_Earth_Small].Add(BuildData(CreatureConstants.Elemental_Earth_Small, FeatConstants.SpecialQualities.EarthGlide));

            testCases[CreatureConstants.Elemental_Earth_Medium].Add(BuildData(CreatureConstants.Elemental_Earth_Medium, FeatConstants.SpecialQualities.EarthGlide));

            testCases[CreatureConstants.Elemental_Earth_Large].Add(BuildData(CreatureConstants.Elemental_Earth_Large, FeatConstants.SpecialQualities.EarthGlide));
            testCases[CreatureConstants.Elemental_Earth_Large].Add(BuildData(CreatureConstants.Elemental_Earth_Large, FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

            testCases[CreatureConstants.Elemental_Earth_Huge].Add(BuildData(CreatureConstants.Elemental_Earth_Huge, FeatConstants.SpecialQualities.EarthGlide));
            testCases[CreatureConstants.Elemental_Earth_Huge].Add(BuildData(CreatureConstants.Elemental_Earth_Huge, FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

            testCases[CreatureConstants.Elemental_Earth_Greater].Add(BuildData(CreatureConstants.Elemental_Earth_Greater, FeatConstants.SpecialQualities.EarthGlide));
            testCases[CreatureConstants.Elemental_Earth_Greater].Add(BuildData(CreatureConstants.Elemental_Earth_Greater, FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

            testCases[CreatureConstants.Elemental_Earth_Elder].Add(BuildData(CreatureConstants.Elemental_Earth_Elder, FeatConstants.SpecialQualities.EarthGlide));
            testCases[CreatureConstants.Elemental_Earth_Elder].Add(BuildData(CreatureConstants.Elemental_Earth_Elder, FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

            testCases[CreatureConstants.Elemental_Fire_Small].Add(BuildData(CreatureConstants.Elemental_Fire_Small, FeatConstants.Initiative_Improved, power: 4));
            testCases[CreatureConstants.Elemental_Fire_Small].Add(BuildData(CreatureConstants.Elemental_Fire_Small, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Elemental_Fire_Medium].Add(BuildData(CreatureConstants.Elemental_Fire_Medium, FeatConstants.Initiative_Improved, power: 4));
            testCases[CreatureConstants.Elemental_Fire_Medium].Add(BuildData(CreatureConstants.Elemental_Fire_Medium, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Elemental_Fire_Large].Add(BuildData(CreatureConstants.Elemental_Fire_Large, FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Elemental_Fire_Large].Add(BuildData(CreatureConstants.Elemental_Fire_Large, FeatConstants.Initiative_Improved, power: 4));
            testCases[CreatureConstants.Elemental_Fire_Large].Add(BuildData(CreatureConstants.Elemental_Fire_Large, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Elemental_Fire_Huge].Add(BuildData(CreatureConstants.Elemental_Fire_Huge, FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Elemental_Fire_Huge].Add(BuildData(CreatureConstants.Elemental_Fire_Huge, FeatConstants.Initiative_Improved, power: 4));
            testCases[CreatureConstants.Elemental_Fire_Huge].Add(BuildData(CreatureConstants.Elemental_Fire_Huge, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Elemental_Fire_Greater].Add(BuildData(CreatureConstants.Elemental_Fire_Greater, FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Elemental_Fire_Greater].Add(BuildData(CreatureConstants.Elemental_Fire_Greater, FeatConstants.Initiative_Improved, power: 4));
            testCases[CreatureConstants.Elemental_Fire_Greater].Add(BuildData(CreatureConstants.Elemental_Fire_Greater, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Elemental_Fire_Elder].Add(BuildData(CreatureConstants.Elemental_Fire_Elder, FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Elemental_Fire_Elder].Add(BuildData(CreatureConstants.Elemental_Fire_Elder, FeatConstants.Initiative_Improved, power: 4));
            testCases[CreatureConstants.Elemental_Fire_Elder].Add(BuildData(CreatureConstants.Elemental_Fire_Elder, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Elemental_Water_Small] = [];

            testCases[CreatureConstants.Elemental_Water_Medium] = [];

            testCases[CreatureConstants.Elemental_Water_Large].Add(BuildData(CreatureConstants.Elemental_Water_Large, FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

            testCases[CreatureConstants.Elemental_Water_Huge].Add(BuildData(CreatureConstants.Elemental_Water_Huge, FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

            testCases[CreatureConstants.Elemental_Water_Greater].Add(BuildData(CreatureConstants.Elemental_Water_Greater, FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

            testCases[CreatureConstants.Elemental_Water_Elder].Add(BuildData(CreatureConstants.Elemental_Water_Elder, FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

            testCases[CreatureConstants.Elephant].Add(BuildData(CreatureConstants.Elephant, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Elf_Aquatic].Add(BuildData(CreatureConstants.Elf_Aquatic, FeatConstants.SpecialQualities.Gills));
            testCases[CreatureConstants.Elf_Aquatic].Add(BuildData(CreatureConstants.Elf_Aquatic, FeatConstants.SpecialQualities.LowLightVision_Superior));
            testCases[CreatureConstants.Elf_Aquatic].Add(BuildData(CreatureConstants.Elf_Aquatic, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Spear, requiresEquipment: true));
            testCases[CreatureConstants.Elf_Aquatic].Add(BuildData(CreatureConstants.Elf_Aquatic, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Longspear, requiresEquipment: true));
            testCases[CreatureConstants.Elf_Aquatic].Add(BuildData(CreatureConstants.Elf_Aquatic, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Shortspear, requiresEquipment: true));
            testCases[CreatureConstants.Elf_Aquatic].Add(BuildData(CreatureConstants.Elf_Aquatic, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Trident, requiresEquipment: true));
            testCases[CreatureConstants.Elf_Aquatic].Add(BuildData(CreatureConstants.Elf_Aquatic, FeatConstants.WeaponProficiency_Exotic, focus: WeaponConstants.Net, requiresEquipment: true));

            testCases[CreatureConstants.Elf_Drow].Add(BuildData(CreatureConstants.Elf_Drow, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Elf_Drow].Add(BuildData(CreatureConstants.Elf_Drow, FeatConstants.SpecialQualities.SpellResistance, power: 11));
            testCases[CreatureConstants.Elf_Drow].Add(BuildData(CreatureConstants.Elf_Drow, FeatConstants.SpecialQualities.LightBlindness));
            testCases[CreatureConstants.Elf_Drow].Add(BuildData(CreatureConstants.Elf_Drow, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DancingLights, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Elf_Drow].Add(BuildData(CreatureConstants.Elf_Drow, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Elf_Drow].Add(BuildData(CreatureConstants.Elf_Drow, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FaerieFire, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Elf_Drow].Add(BuildData(CreatureConstants.Elf_Drow, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.ShortSword, requiresEquipment: true));
            testCases[CreatureConstants.Elf_Drow].Add(BuildData(CreatureConstants.Elf_Drow, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Rapier, requiresEquipment: true));
            testCases[CreatureConstants.Elf_Drow].Add(BuildData(CreatureConstants.Elf_Drow, FeatConstants.WeaponProficiency_Exotic, focus: WeaponConstants.HandCrossbow, requiresEquipment: true));

            testCases[CreatureConstants.Elf_Gray].Add(BuildData(CreatureConstants.Elf_Gray, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Elf_Gray].Add(BuildData(CreatureConstants.Elf_Gray, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
            testCases[CreatureConstants.Elf_Gray].Add(BuildData(CreatureConstants.Elf_Gray, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Rapier, requiresEquipment: true));
            testCases[CreatureConstants.Elf_Gray].Add(BuildData(CreatureConstants.Elf_Gray, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));
            testCases[CreatureConstants.Elf_Gray].Add(BuildData(CreatureConstants.Elf_Gray, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
            testCases[CreatureConstants.Elf_Gray].Add(BuildData(CreatureConstants.Elf_Gray, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));
            testCases[CreatureConstants.Elf_Gray].Add(BuildData(CreatureConstants.Elf_Gray, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeShortbow, requiresEquipment: true));

            testCases[CreatureConstants.Elf_Half].Add(BuildData(CreatureConstants.Elf_Half, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Elf_Half].Add(BuildData(CreatureConstants.Elf_Half, FeatConstants.SpecialQualities.ElvenBlood));
            testCases[CreatureConstants.Elf_Half].Add(BuildData(CreatureConstants.Elf_Half, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));

            testCases[CreatureConstants.Elf_High].Add(BuildData(CreatureConstants.Elf_High, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Elf_High].Add(BuildData(CreatureConstants.Elf_High, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
            testCases[CreatureConstants.Elf_High].Add(BuildData(CreatureConstants.Elf_High, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Rapier, requiresEquipment: true));
            testCases[CreatureConstants.Elf_High].Add(BuildData(CreatureConstants.Elf_High, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));
            testCases[CreatureConstants.Elf_High].Add(BuildData(CreatureConstants.Elf_High, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
            testCases[CreatureConstants.Elf_High].Add(BuildData(CreatureConstants.Elf_High, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));
            testCases[CreatureConstants.Elf_High].Add(BuildData(CreatureConstants.Elf_High, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeShortbow, requiresEquipment: true));

            testCases[CreatureConstants.Elf_Wild].Add(BuildData(CreatureConstants.Elf_Wild, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Elf_Wild].Add(BuildData(CreatureConstants.Elf_Wild, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
            testCases[CreatureConstants.Elf_Wild].Add(BuildData(CreatureConstants.Elf_Wild, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Rapier, requiresEquipment: true));
            testCases[CreatureConstants.Elf_Wild].Add(BuildData(CreatureConstants.Elf_Wild, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));
            testCases[CreatureConstants.Elf_Wild].Add(BuildData(CreatureConstants.Elf_Wild, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
            testCases[CreatureConstants.Elf_Wild].Add(BuildData(CreatureConstants.Elf_Wild, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));
            testCases[CreatureConstants.Elf_Wild].Add(BuildData(CreatureConstants.Elf_Wild, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeShortbow, requiresEquipment: true));

            testCases[CreatureConstants.Elf_Wood].Add(BuildData(CreatureConstants.Elf_Wood, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Elf_Wood].Add(BuildData(CreatureConstants.Elf_Wood, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
            testCases[CreatureConstants.Elf_Wood].Add(BuildData(CreatureConstants.Elf_Wood, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Rapier, requiresEquipment: true));
            testCases[CreatureConstants.Elf_Wood].Add(BuildData(CreatureConstants.Elf_Wood, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));
            testCases[CreatureConstants.Elf_Wood].Add(BuildData(CreatureConstants.Elf_Wood, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
            testCases[CreatureConstants.Elf_Wood].Add(BuildData(CreatureConstants.Elf_Wood, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));
            testCases[CreatureConstants.Elf_Wood].Add(BuildData(CreatureConstants.Elf_Wood, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeShortbow, requiresEquipment: true));

            testCases[CreatureConstants.Erinyes].Add(BuildData(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Erinyes].Add(BuildData(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
            testCases[CreatureConstants.Erinyes].Add(BuildData(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.Erinyes].Add(BuildData(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Erinyes].Add(BuildData(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Erinyes].Add(BuildData(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.SeeInDarkness, focus: "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell"));
            testCases[CreatureConstants.Erinyes].Add(BuildData(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.SpellResistance, power: 20));
            testCases[CreatureConstants.Erinyes].Add(BuildData(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.Telepathy, power: 100));
            testCases[CreatureConstants.Erinyes].Add(BuildData(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TrueSeeing, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
            testCases[CreatureConstants.Erinyes].Add(BuildData(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Erinyes].Add(BuildData(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Erinyes].Add(BuildData(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MinorImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.Erinyes].Add(BuildData(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyBlight, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Erinyes].Add(BuildData(CreatureConstants.Erinyes, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
            testCases[CreatureConstants.Erinyes].Add(BuildData(CreatureConstants.Erinyes, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));

            testCases[CreatureConstants.EtherealFilcher].Add(BuildData(CreatureConstants.EtherealFilcher, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.EtherealFilcher].Add(BuildData(CreatureConstants.EtherealFilcher, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EtherealJaunt, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

            testCases[CreatureConstants.EtherealMarauder].Add(BuildData(CreatureConstants.EtherealMarauder, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EtherealJaunt, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

            testCases[CreatureConstants.Ettercap].Add(BuildData(CreatureConstants.Ettercap, FeatConstants.SpecialQualities.LowLightVision));

            testCases[CreatureConstants.Ettin].Add(BuildData(CreatureConstants.Ettin, FeatConstants.TwoWeaponFighting, requiresEquipment: true));
            testCases[CreatureConstants.Ettin].Add(BuildData(CreatureConstants.Ettin, FeatConstants.SpecialQualities.TwoWeaponFighting_Superior, requiresEquipment: true));
            testCases[CreatureConstants.Ettin].Add(BuildData(CreatureConstants.Ettin, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Morningstar, requiresEquipment: true));
            testCases[CreatureConstants.Ettin].Add(BuildData(CreatureConstants.Ettin, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Javelin, requiresEquipment: true));
            testCases[CreatureConstants.Ettin].Add(BuildData(CreatureConstants.Ettin, FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
            testCases[CreatureConstants.Ettin].Add(BuildData(CreatureConstants.Ettin, FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

            testCases[CreatureConstants.FireBeetle_Giant] = [];

            testCases[CreatureConstants.FormianWorker].Add(BuildData(CreatureConstants.FormianWorker, FeatConstants.SpecialQualities.HiveMind));
            testCases[CreatureConstants.FormianWorker].Add(BuildData(CreatureConstants.FormianWorker, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CureSeriousWounds + ": 8 workers work together to cast the spell", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.FormianWorker].Add(BuildData(CreatureConstants.FormianWorker, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MakeWhole + ": 3 workers work together to cast the spell", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.FormianWorker].Add(BuildData(CreatureConstants.FormianWorker, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.FormianWorker].Add(BuildData(CreatureConstants.FormianWorker, FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
            testCases[CreatureConstants.FormianWorker].Add(BuildData(CreatureConstants.FormianWorker, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
            testCases[CreatureConstants.FormianWorker].Add(BuildData(CreatureConstants.FormianWorker, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.FormianWorker].Add(BuildData(CreatureConstants.FormianWorker, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.FormianWorker].Add(BuildData(CreatureConstants.FormianWorker, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Sonic, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

            testCases[CreatureConstants.FormianWarrior].Add(BuildData(CreatureConstants.FormianWarrior, FeatConstants.SpecialQualities.HiveMind));
            testCases[CreatureConstants.FormianWarrior].Add(BuildData(CreatureConstants.FormianWarrior, FeatConstants.SpecialQualities.SpellResistance, power: 18));
            testCases[CreatureConstants.FormianWarrior].Add(BuildData(CreatureConstants.FormianWarrior, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.FormianWarrior].Add(BuildData(CreatureConstants.FormianWarrior, FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
            testCases[CreatureConstants.FormianWarrior].Add(BuildData(CreatureConstants.FormianWarrior, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
            testCases[CreatureConstants.FormianWarrior].Add(BuildData(CreatureConstants.FormianWarrior, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.FormianWarrior].Add(BuildData(CreatureConstants.FormianWarrior, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.FormianWarrior].Add(BuildData(CreatureConstants.FormianWarrior, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Sonic, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

            testCases[CreatureConstants.FormianTaskmaster].Add(BuildData(CreatureConstants.FormianTaskmaster, FeatConstants.SpecialQualities.HiveMind));
            testCases[CreatureConstants.FormianTaskmaster].Add(BuildData(CreatureConstants.FormianTaskmaster, FeatConstants.SpecialQualities.SpellResistance, power: 21));
            testCases[CreatureConstants.FormianTaskmaster].Add(BuildData(CreatureConstants.FormianTaskmaster, FeatConstants.SpecialQualities.Telepathy, power: 100));
            testCases[CreatureConstants.FormianTaskmaster].Add(BuildData(CreatureConstants.FormianTaskmaster, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DominateMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.FormianTaskmaster].Add(BuildData(CreatureConstants.FormianTaskmaster, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.FormianTaskmaster].Add(BuildData(CreatureConstants.FormianTaskmaster, FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
            testCases[CreatureConstants.FormianTaskmaster].Add(BuildData(CreatureConstants.FormianTaskmaster, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
            testCases[CreatureConstants.FormianTaskmaster].Add(BuildData(CreatureConstants.FormianTaskmaster, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.FormianTaskmaster].Add(BuildData(CreatureConstants.FormianTaskmaster, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.FormianTaskmaster].Add(BuildData(CreatureConstants.FormianTaskmaster, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Sonic, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

            testCases[CreatureConstants.FormianMyrmarch].Add(BuildData(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.FastHealing, power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.FormianMyrmarch].Add(BuildData(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.HiveMind));
            testCases[CreatureConstants.FormianMyrmarch].Add(BuildData(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.SpellResistance, power: 25));
            testCases[CreatureConstants.FormianMyrmarch].Add(BuildData(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.FormianMyrmarch].Add(BuildData(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ClairaudienceClairvoyance, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.FormianMyrmarch].Add(BuildData(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectChaos, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.FormianMyrmarch].Add(BuildData(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.FormianMyrmarch].Add(BuildData(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicCircleAgainstChaos, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.FormianMyrmarch].Add(BuildData(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.FormianMyrmarch].Add(BuildData(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Dictum, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));
            testCases[CreatureConstants.FormianMyrmarch].Add(BuildData(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.OrdersWrath, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.FormianMyrmarch].Add(BuildData(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.FormianMyrmarch].Add(BuildData(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
            testCases[CreatureConstants.FormianMyrmarch].Add(BuildData(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
            testCases[CreatureConstants.FormianMyrmarch].Add(BuildData(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.FormianMyrmarch].Add(BuildData(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.FormianMyrmarch].Add(BuildData(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Sonic, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.FormianMyrmarch].Add(BuildData(CreatureConstants.FormianMyrmarch, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Javelin, requiresEquipment: true));

            testCases[CreatureConstants.FormianQueen].Add(BuildData(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.FastHealing, power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.FormianQueen].Add(BuildData(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.HiveMind));
            testCases[CreatureConstants.FormianQueen].Add(BuildData(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellResistance, power: 30));
            testCases[CreatureConstants.FormianQueen].Add(BuildData(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CalmEmotions, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.FormianQueen].Add(BuildData(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.FormianQueen].Add(BuildData(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ClairaudienceClairvoyance, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.FormianQueen].Add(BuildData(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectChaos, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.FormianQueen].Add(BuildData(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.FormianQueen].Add(BuildData(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Dictum, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));
            testCases[CreatureConstants.FormianQueen].Add(BuildData(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Divination, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.FormianQueen].Add(BuildData(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
            testCases[CreatureConstants.FormianQueen].Add(BuildData(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicCircleAgainstChaos, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.FormianQueen].Add(BuildData(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.OrdersWrath, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.FormianQueen].Add(BuildData(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ShieldOfLaw, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 18));
            testCases[CreatureConstants.FormianQueen].Add(BuildData(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TrueSeeing, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.FormianQueen].Add(BuildData(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.FormianQueen].Add(BuildData(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
            testCases[CreatureConstants.FormianQueen].Add(BuildData(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
            testCases[CreatureConstants.FormianQueen].Add(BuildData(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.FormianQueen].Add(BuildData(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.FormianQueen].Add(BuildData(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Sonic, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.FormianQueen].Add(BuildData(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.Telepathy, focus: "Any intelligent creature within 50 miles whose presence she is aware of"));
            testCases[CreatureConstants.FormianQueen].Add(BuildData(CreatureConstants.FormianQueen, FeatConstants.EschewMaterials));

            testCases[CreatureConstants.FrostWorm].Add(BuildData(CreatureConstants.FrostWorm, FeatConstants.SpecialQualities.DeathThroes, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveBaseValue: 17));

            testCases[CreatureConstants.Gargoyle].Add(BuildData(CreatureConstants.Gargoyle, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Gargoyle].Add(BuildData(CreatureConstants.Gargoyle, FeatConstants.SpecialQualities.Freeze));

            testCases[CreatureConstants.Gargoyle_Kapoacinth].Add(BuildData(CreatureConstants.Gargoyle_Kapoacinth, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Gargoyle_Kapoacinth].Add(BuildData(CreatureConstants.Gargoyle_Kapoacinth, FeatConstants.SpecialQualities.Freeze));

            testCases[CreatureConstants.GelatinousCube].Add(BuildData(CreatureConstants.GelatinousCube, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.GelatinousCube].Add(BuildData(CreatureConstants.GelatinousCube, FeatConstants.SpecialQualities.Transparent));

            testCases[CreatureConstants.Ghaele].Add(BuildData(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.AlternateForm, focus: "Humanoid and globe forms", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Ghaele].Add(BuildData(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to evil, cold iron weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Ghaele].Add(BuildData(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellResistance, power: 28));
            testCases[CreatureConstants.Ghaele].Add(BuildData(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Ghaele].Add(BuildData(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.ProtectiveAura));
            testCases[CreatureConstants.Ghaele].Add(BuildData(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
            testCases[CreatureConstants.Ghaele].Add(BuildData(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Ghaele].Add(BuildData(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Ghaele].Add(BuildData(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Ghaele].Add(BuildData(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Tongues, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
            testCases[CreatureConstants.Ghaele].Add(BuildData(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Aid, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Ghaele].Add(BuildData(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Ghaele].Add(BuildData(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ColorSpray, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
            testCases[CreatureConstants.Ghaele].Add(BuildData(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ComprehendLanguages, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Ghaele].Add(BuildData(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ContinualFlame, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Ghaele].Add(BuildData(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CureLightWounds, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
            testCases[CreatureConstants.Ghaele].Add(BuildData(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DancingLights, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Ghaele].Add(BuildData(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectEvil, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Ghaele].Add(BuildData(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.Ghaele].Add(BuildData(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DisguiseSelf, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Ghaele].Add(BuildData(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Ghaele].Add(BuildData(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
            testCases[CreatureConstants.Ghaele].Add(BuildData(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility_Greater + ": self only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Ghaele].Add(BuildData(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MajorImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Ghaele].Add(BuildData(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Ghaele].Add(BuildData(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Ghaele].Add(BuildData(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ChainLightning, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 16));
            testCases[CreatureConstants.Ghaele].Add(BuildData(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PrismaticSpray, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));
            testCases[CreatureConstants.Ghaele].Add(BuildData(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfForce, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Ghaele].Add(BuildData(CreatureConstants.Ghaele, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatsword, requiresEquipment: true));

            testCases[CreatureConstants.Ghoul].Add(BuildData(CreatureConstants.Ghoul, FeatConstants.SpecialQualities.TurnResistance, power: 2));

            testCases[CreatureConstants.Ghoul_Ghast].Add(BuildData(CreatureConstants.Ghoul_Ghast, FeatConstants.SpecialQualities.TurnResistance, power: 2));

            testCases[CreatureConstants.Ghoul_Lacedon].Add(BuildData(CreatureConstants.Ghoul_Lacedon, FeatConstants.SpecialQualities.TurnResistance, power: 2));

            testCases[CreatureConstants.Giant_Cloud].Add(BuildData(CreatureConstants.Giant_Cloud, FeatConstants.SpecialQualities.RockThrowing, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round, power: 1));
            testCases[CreatureConstants.Giant_Cloud].Add(BuildData(CreatureConstants.Giant_Cloud, FeatConstants.SpecialQualities.RockCatching, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Giant_Cloud].Add(BuildData(CreatureConstants.Giant_Cloud, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Giant_Cloud].Add(BuildData(CreatureConstants.Giant_Cloud, FeatConstants.SpecialQualities.OversizedWeapon, focus: SizeConstants.Gargantuan, requiresEquipment: true));
            testCases[CreatureConstants.Giant_Cloud].Add(BuildData(CreatureConstants.Giant_Cloud, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Levitate + ": self plus 2,000 pounds", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Giant_Cloud].Add(BuildData(CreatureConstants.Giant_Cloud, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ObscuringMist, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Giant_Cloud].Add(BuildData(CreatureConstants.Giant_Cloud, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Giant_Cloud].Add(BuildData(CreatureConstants.Giant_Cloud, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Morningstar, requiresEquipment: true));
            testCases[CreatureConstants.Giant_Cloud].Add(BuildData(CreatureConstants.Giant_Cloud, FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

            testCases[CreatureConstants.Giant_Fire].Add(BuildData(CreatureConstants.Giant_Fire, FeatConstants.SpecialQualities.RockThrowing, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round, power: 1));
            testCases[CreatureConstants.Giant_Fire].Add(BuildData(CreatureConstants.Giant_Fire, FeatConstants.SpecialQualities.RockCatching, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Giant_Fire].Add(BuildData(CreatureConstants.Giant_Fire, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatsword, requiresEquipment: true));
            testCases[CreatureConstants.Giant_Fire].Add(BuildData(CreatureConstants.Giant_Fire, FeatConstants.ArmorProficiency_Heavy, requiresEquipment: true));
            testCases[CreatureConstants.Giant_Fire].Add(BuildData(CreatureConstants.Giant_Fire, FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
            testCases[CreatureConstants.Giant_Fire].Add(BuildData(CreatureConstants.Giant_Fire, FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

            testCases[CreatureConstants.Giant_Frost].Add(BuildData(CreatureConstants.Giant_Frost, FeatConstants.SpecialQualities.RockThrowing, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round, power: 1));
            testCases[CreatureConstants.Giant_Frost].Add(BuildData(CreatureConstants.Giant_Frost, FeatConstants.SpecialQualities.RockCatching, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Giant_Frost].Add(BuildData(CreatureConstants.Giant_Frost, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greataxe, requiresEquipment: true));
            testCases[CreatureConstants.Giant_Frost].Add(BuildData(CreatureConstants.Giant_Frost, FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

            testCases[CreatureConstants.Giant_Hill].Add(BuildData(CreatureConstants.Giant_Hill, FeatConstants.SpecialQualities.RockThrowing, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round, power: 1));
            testCases[CreatureConstants.Giant_Hill].Add(BuildData(CreatureConstants.Giant_Hill, FeatConstants.SpecialQualities.RockCatching, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Giant_Hill].Add(BuildData(CreatureConstants.Giant_Hill, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatclub, requiresEquipment: true));
            testCases[CreatureConstants.Giant_Hill].Add(BuildData(CreatureConstants.Giant_Hill, FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
            testCases[CreatureConstants.Giant_Hill].Add(BuildData(CreatureConstants.Giant_Hill, FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

            testCases[CreatureConstants.Giant_Stone].Add(BuildData(CreatureConstants.Giant_Stone, FeatConstants.SpecialQualities.RockThrowing, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round, power: 1));
            testCases[CreatureConstants.Giant_Stone].Add(BuildData(CreatureConstants.Giant_Stone, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Giant_Stone].Add(BuildData(CreatureConstants.Giant_Stone, FeatConstants.SpecialQualities.RockCatching, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Giant_Stone].Add(BuildData(CreatureConstants.Giant_Stone, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatclub, requiresEquipment: true));
            testCases[CreatureConstants.Giant_Stone].Add(BuildData(CreatureConstants.Giant_Stone, FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
            testCases[CreatureConstants.Giant_Stone].Add(BuildData(CreatureConstants.Giant_Stone, FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

            testCases[CreatureConstants.Giant_Stone_Elder].Add(BuildData(CreatureConstants.Giant_Stone_Elder, FeatConstants.SpecialQualities.RockThrowing, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round, power: 1));
            testCases[CreatureConstants.Giant_Stone_Elder].Add(BuildData(CreatureConstants.Giant_Stone_Elder, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Giant_Stone_Elder].Add(BuildData(CreatureConstants.Giant_Stone_Elder, FeatConstants.SpecialQualities.RockCatching, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Giant_Stone_Elder].Add(BuildData(CreatureConstants.Giant_Stone_Elder, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatclub, requiresEquipment: true));
            testCases[CreatureConstants.Giant_Stone_Elder].Add(BuildData(CreatureConstants.Giant_Stone_Elder, FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
            testCases[CreatureConstants.Giant_Stone_Elder].Add(BuildData(CreatureConstants.Giant_Stone_Elder, FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
            testCases[CreatureConstants.Giant_Stone_Elder].Add(BuildData(CreatureConstants.Giant_Stone_Elder, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneShape, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Giant_Stone_Elder].Add(BuildData(CreatureConstants.Giant_Stone_Elder, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneTell, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Giant_Stone_Elder].Add(BuildData(CreatureConstants.Giant_Stone_Elder, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteRockToMud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
            testCases[CreatureConstants.Giant_Stone_Elder].Add(BuildData(CreatureConstants.Giant_Stone_Elder, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteMudToRock, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));

            testCases[CreatureConstants.Giant_Storm].Add(BuildData(CreatureConstants.Giant_Storm, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Giant_Storm].Add(BuildData(CreatureConstants.Giant_Storm, FeatConstants.SpecialQualities.WaterBreathing));
            testCases[CreatureConstants.Giant_Storm].Add(BuildData(CreatureConstants.Giant_Storm, FeatConstants.SpecialQualities.RockCatching, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Giant_Storm].Add(BuildData(CreatureConstants.Giant_Storm, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatsword, requiresEquipment: true));
            testCases[CreatureConstants.Giant_Storm].Add(BuildData(CreatureConstants.Giant_Storm, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
            testCases[CreatureConstants.Giant_Storm].Add(BuildData(CreatureConstants.Giant_Storm, FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
            testCases[CreatureConstants.Giant_Storm].Add(BuildData(CreatureConstants.Giant_Storm, FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
            testCases[CreatureConstants.Giant_Storm].Add(BuildData(CreatureConstants.Giant_Storm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CallLightning, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 13));
            testCases[CreatureConstants.Giant_Storm].Add(BuildData(CreatureConstants.Giant_Storm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ChainLightning, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 16));
            testCases[CreatureConstants.Giant_Storm].Add(BuildData(CreatureConstants.Giant_Storm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Giant_Storm].Add(BuildData(CreatureConstants.Giant_Storm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Levitate, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Giant_Storm].Add(BuildData(CreatureConstants.Giant_Storm, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FreedomOfMovement, frequencyTimePeriod: FeatConstants.Frequencies.Constant));

            testCases[CreatureConstants.GibberingMouther].Add(BuildData(CreatureConstants.GibberingMouther, FeatConstants.SpecialQualities.Amorphous));
            testCases[CreatureConstants.GibberingMouther].Add(BuildData(CreatureConstants.GibberingMouther, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to bludgeoning weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

            testCases[CreatureConstants.Girallon].Add(BuildData(CreatureConstants.Girallon, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Githyanki].Add(BuildData(CreatureConstants.Githyanki, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Githyanki].Add(BuildData(CreatureConstants.Githyanki, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatsword, requiresEquipment: true));
            testCases[CreatureConstants.Githyanki].Add(BuildData(CreatureConstants.Githyanki, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
            testCases[CreatureConstants.Githyanki].Add(BuildData(CreatureConstants.Githyanki, FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
            testCases[CreatureConstants.Githyanki].Add(BuildData(CreatureConstants.Githyanki, FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
            testCases[CreatureConstants.Githyanki].Add(BuildData(CreatureConstants.Githyanki, FeatConstants.SpecialQualities.SpellResistance, power: 6));
            testCases[CreatureConstants.Githyanki].Add(BuildData(CreatureConstants.Githyanki, FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.Daze, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 10));
            testCases[CreatureConstants.Githyanki].Add(BuildData(CreatureConstants.Githyanki, FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.MageHand, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Githzerai].Add(BuildData(CreatureConstants.Githzerai, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Githzerai].Add(BuildData(CreatureConstants.Githzerai, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.ShortSword, requiresEquipment: true));
            testCases[CreatureConstants.Githzerai].Add(BuildData(CreatureConstants.Githzerai, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
            testCases[CreatureConstants.Githzerai].Add(BuildData(CreatureConstants.Githzerai, FeatConstants.SpecialQualities.SpellResistance, power: 6));
            testCases[CreatureConstants.Githzerai].Add(BuildData(CreatureConstants.Githzerai, FeatConstants.SpecialQualities.InertialArmor, power: 4));
            testCases[CreatureConstants.Githzerai].Add(BuildData(CreatureConstants.Githzerai, FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.Daze, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 10));
            testCases[CreatureConstants.Githzerai].Add(BuildData(CreatureConstants.Githzerai, FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.FeatherFall, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Githzerai].Add(BuildData(CreatureConstants.Githzerai, FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.Shatter, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));

            testCases[CreatureConstants.Glabrezu].Add(BuildData(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Glabrezu].Add(BuildData(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.Glabrezu].Add(BuildData(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Glabrezu].Add(BuildData(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Glabrezu].Add(BuildData(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Glabrezu].Add(BuildData(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Glabrezu].Add(BuildData(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.SpellResistance, power: 21));
            testCases[CreatureConstants.Glabrezu].Add(BuildData(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.Telepathy, power: 100));
            testCases[CreatureConstants.Glabrezu].Add(BuildData(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TrueSeeing, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
            testCases[CreatureConstants.Glabrezu].Add(BuildData(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ChaosHammer, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Glabrezu].Add(BuildData(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Confusion, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Glabrezu].Add(BuildData(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Glabrezu].Add(BuildData(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MirrorImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Glabrezu].Add(BuildData(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ReverseGravity, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 17));
            testCases[CreatureConstants.Glabrezu].Add(BuildData(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Glabrezu].Add(BuildData(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyBlight, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Glabrezu].Add(BuildData(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PowerWordStun, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Glabrezu].Add(BuildData(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Wish + ": for a mortal humanoid. The demon can use this ability to offer a mortal whatever he or she desires - but unless the wish is used to create pain and suffering in the world, the glabrezu demands either terrible evil acts or great sacrifice as compensation.", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Month));

            testCases[CreatureConstants.Gnoll].Add(BuildData(CreatureConstants.Gnoll, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Gnoll].Add(BuildData(CreatureConstants.Gnoll, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Battleaxe, requiresEquipment: true));
            testCases[CreatureConstants.Gnoll].Add(BuildData(CreatureConstants.Gnoll, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));
            testCases[CreatureConstants.Gnoll].Add(BuildData(CreatureConstants.Gnoll, FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

            testCases[CreatureConstants.Gnome_Forest].Add(BuildData(CreatureConstants.Gnome_Forest, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
            testCases[CreatureConstants.Gnome_Forest].Add(BuildData(CreatureConstants.Gnome_Forest, FeatConstants.SpecialQualities.AttackBonus, focus: CreatureConstants.Types.Subtypes.Orc, power: 1));
            testCases[CreatureConstants.Gnome_Forest].Add(BuildData(CreatureConstants.Gnome_Forest, FeatConstants.SpecialQualities.AttackBonus, focus: CreatureConstants.Types.Subtypes.Reptilian, power: 1));
            testCases[CreatureConstants.Gnome_Forest].Add(BuildData(CreatureConstants.Gnome_Forest, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals + ": on a very basic level with forest animals", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Gnome_Forest].Add(BuildData(CreatureConstants.Gnome_Forest, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DancingLights, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Gnome_Forest].Add(BuildData(CreatureConstants.Gnome_Forest, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GhostSound, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 10));
            testCases[CreatureConstants.Gnome_Forest].Add(BuildData(CreatureConstants.Gnome_Forest, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Prestidigitation, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Gnome_Forest].Add(BuildData(CreatureConstants.Gnome_Forest, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PassWithoutTrace + ": self only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

            testCases[CreatureConstants.Gnome_Rock].Add(BuildData(CreatureConstants.Gnome_Rock, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
            testCases[CreatureConstants.Gnome_Rock].Add(BuildData(CreatureConstants.Gnome_Rock, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals + ": burrowing mammals only, duration 1 minute", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Gnome_Rock].Add(BuildData(CreatureConstants.Gnome_Rock, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DancingLights, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Gnome_Rock].Add(BuildData(CreatureConstants.Gnome_Rock, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GhostSound, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 10));
            testCases[CreatureConstants.Gnome_Rock].Add(BuildData(CreatureConstants.Gnome_Rock, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Prestidigitation, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Gnome_Svirfneblin].Add(BuildData(CreatureConstants.Gnome_Svirfneblin, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.HeavyPick, requiresEquipment: true));
            testCases[CreatureConstants.Gnome_Svirfneblin].Add(BuildData(CreatureConstants.Gnome_Svirfneblin, FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
            testCases[CreatureConstants.Gnome_Svirfneblin].Add(BuildData(CreatureConstants.Gnome_Svirfneblin, FeatConstants.ArmorProficiency_Heavy, requiresEquipment: true));
            testCases[CreatureConstants.Gnome_Svirfneblin].Add(BuildData(CreatureConstants.Gnome_Svirfneblin, FeatConstants.SpecialQualities.Darkvision, power: 120));
            testCases[CreatureConstants.Gnome_Svirfneblin].Add(BuildData(CreatureConstants.Gnome_Svirfneblin, FeatConstants.SpecialQualities.Stonecunning));
            testCases[CreatureConstants.Gnome_Svirfneblin].Add(BuildData(CreatureConstants.Gnome_Svirfneblin, FeatConstants.SpecialQualities.SpellResistance, power: 11));
            testCases[CreatureConstants.Gnome_Svirfneblin].Add(BuildData(CreatureConstants.Gnome_Svirfneblin, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.BlindnessDeafness, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 14));
            testCases[CreatureConstants.Gnome_Svirfneblin].Add(BuildData(CreatureConstants.Gnome_Svirfneblin, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blur, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Gnome_Svirfneblin].Add(BuildData(CreatureConstants.Gnome_Svirfneblin, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DisguiseSelf, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Goblin].Add(BuildData(CreatureConstants.Goblin, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Morningstar, requiresEquipment: true));
            testCases[CreatureConstants.Goblin].Add(BuildData(CreatureConstants.Goblin, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Javelin, requiresEquipment: true));
            testCases[CreatureConstants.Goblin].Add(BuildData(CreatureConstants.Goblin, FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
            testCases[CreatureConstants.Goblin].Add(BuildData(CreatureConstants.Goblin, FeatConstants.SpecialQualities.Darkvision, power: 60));

            testCases[CreatureConstants.Golem_Clay].Add(BuildData(CreatureConstants.Golem_Clay, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to adamantine, bludgeoning weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Golem_Clay].Add(BuildData(CreatureConstants.Golem_Clay, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Haste + ": after at least 1 round of combat, lasts 3 rounds", frequencyTimePeriod: FeatConstants.Frequencies.Day, frequencyQuantity: 1));
            testCases[CreatureConstants.Golem_Clay].Add(BuildData(CreatureConstants.Golem_Clay, FeatConstants.SpecialQualities.Immunity, focus: "Magic (see creature description)"));

            testCases[CreatureConstants.Golem_Flesh].Add(BuildData(CreatureConstants.Golem_Flesh, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to adamantine weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Golem_Flesh].Add(BuildData(CreatureConstants.Golem_Flesh, FeatConstants.SpecialQualities.Immunity, focus: "Magic (see creature description)"));

            testCases[CreatureConstants.Golem_Iron].Add(BuildData(CreatureConstants.Golem_Iron, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to adamantine weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Golem_Iron].Add(BuildData(CreatureConstants.Golem_Iron, FeatConstants.SpecialQualities.Immunity, focus: "Magic (see creature description)"));

            testCases[CreatureConstants.Golem_Stone].Add(BuildData(CreatureConstants.Golem_Stone, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to adamantine weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Golem_Stone].Add(BuildData(CreatureConstants.Golem_Stone, FeatConstants.SpecialQualities.Immunity, focus: "Magic (see creature description)"));

            testCases[CreatureConstants.Golem_Stone_Greater].Add(BuildData(CreatureConstants.Golem_Stone_Greater, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to adamantine weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Golem_Stone_Greater].Add(BuildData(CreatureConstants.Golem_Stone_Greater, FeatConstants.SpecialQualities.Immunity, focus: "Magic (see creature description)"));

            testCases[CreatureConstants.Gorgon].Add(BuildData(CreatureConstants.Gorgon, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.GrayOoze].Add(BuildData(CreatureConstants.GrayOoze, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
            testCases[CreatureConstants.GrayOoze].Add(BuildData(CreatureConstants.GrayOoze, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
            testCases[CreatureConstants.GrayOoze].Add(BuildData(CreatureConstants.GrayOoze, FeatConstants.SpecialQualities.Transparent));

            testCases[CreatureConstants.GrayRender].Add(BuildData(CreatureConstants.GrayRender, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.GreenHag].Add(BuildData(CreatureConstants.GreenHag, FeatConstants.SpecialQualities.Darkvision, power: 90));
            testCases[CreatureConstants.GreenHag].Add(BuildData(CreatureConstants.GreenHag, FeatConstants.SpecialQualities.SpellResistance, power: 18));
            testCases[CreatureConstants.GreenHag].Add(BuildData(CreatureConstants.GreenHag, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DancingLights, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.GreenHag].Add(BuildData(CreatureConstants.GreenHag, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DisguiseSelf, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.GreenHag].Add(BuildData(CreatureConstants.GreenHag, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GhostSound, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 10));
            testCases[CreatureConstants.GreenHag].Add(BuildData(CreatureConstants.GreenHag, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.GreenHag].Add(BuildData(CreatureConstants.GreenHag, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PassWithoutTrace, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.GreenHag].Add(BuildData(CreatureConstants.GreenHag, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Tongues, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.GreenHag].Add(BuildData(CreatureConstants.GreenHag, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WaterBreathing, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

            testCases[CreatureConstants.Grick].Add(BuildData(CreatureConstants.Grick, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Grick].Add(BuildData(CreatureConstants.Grick, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Grick].Add(BuildData(CreatureConstants.Grick, FeatConstants.Track));

            testCases[CreatureConstants.Griffon].Add(BuildData(CreatureConstants.Griffon, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Grig].Add(BuildData(CreatureConstants.Grig, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Grig].Add(BuildData(CreatureConstants.Grig, FeatConstants.SpecialQualities.SpellResistance, power: 17));
            testCases[CreatureConstants.Grig].Add(BuildData(CreatureConstants.Grig, FeatConstants.Dodge, power: 1));
            testCases[CreatureConstants.Grig].Add(BuildData(CreatureConstants.Grig, FeatConstants.WeaponFinesse));
            testCases[CreatureConstants.Grig].Add(BuildData(CreatureConstants.Grig, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DisguiseSelf, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Grig].Add(BuildData(CreatureConstants.Grig, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Entangle, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 11));
            testCases[CreatureConstants.Grig].Add(BuildData(CreatureConstants.Grig, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + ": self only", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Grig].Add(BuildData(CreatureConstants.Grig, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Pyrotechnics, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, saveBaseValue: 12));
            testCases[CreatureConstants.Grig].Add(BuildData(CreatureConstants.Grig, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
            testCases[CreatureConstants.Grig].Add(BuildData(CreatureConstants.Grig, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.ShortSword, requiresEquipment: true));
            testCases[CreatureConstants.Grig].Add(BuildData(CreatureConstants.Grig, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));

            testCases[CreatureConstants.Grig_WithFiddle].Add(BuildData(CreatureConstants.Grig_WithFiddle, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Grig_WithFiddle].Add(BuildData(CreatureConstants.Grig_WithFiddle, FeatConstants.SpecialQualities.SpellResistance, power: 17));
            testCases[CreatureConstants.Grig_WithFiddle].Add(BuildData(CreatureConstants.Grig_WithFiddle, FeatConstants.Dodge, power: 1));
            testCases[CreatureConstants.Grig_WithFiddle].Add(BuildData(CreatureConstants.Grig_WithFiddle, FeatConstants.WeaponFinesse));
            testCases[CreatureConstants.Grig_WithFiddle].Add(BuildData(CreatureConstants.Grig_WithFiddle, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DisguiseSelf, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Grig_WithFiddle].Add(BuildData(CreatureConstants.Grig_WithFiddle, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Entangle, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 11));
            testCases[CreatureConstants.Grig_WithFiddle].Add(BuildData(CreatureConstants.Grig_WithFiddle, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + ": self only", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Grig_WithFiddle].Add(BuildData(CreatureConstants.Grig_WithFiddle, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Pyrotechnics, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, saveBaseValue: 12));
            testCases[CreatureConstants.Grig_WithFiddle].Add(BuildData(CreatureConstants.Grig_WithFiddle, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
            testCases[CreatureConstants.Grig_WithFiddle].Add(BuildData(CreatureConstants.Grig_WithFiddle, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.ShortSword, requiresEquipment: true));
            testCases[CreatureConstants.Grig_WithFiddle].Add(BuildData(CreatureConstants.Grig_WithFiddle, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));

            testCases[CreatureConstants.Grimlock].Add(BuildData(CreatureConstants.Grimlock, FeatConstants.SpecialQualities.Blind));
            testCases[CreatureConstants.Grimlock].Add(BuildData(CreatureConstants.Grimlock, FeatConstants.SpecialQualities.Blindsight, power: 40));
            testCases[CreatureConstants.Grimlock].Add(BuildData(CreatureConstants.Grimlock, FeatConstants.SpecialQualities.Immunity, focus: "Gaze attacks"));
            testCases[CreatureConstants.Grimlock].Add(BuildData(CreatureConstants.Grimlock, FeatConstants.SpecialQualities.Immunity, focus: "Visual effects"));
            testCases[CreatureConstants.Grimlock].Add(BuildData(CreatureConstants.Grimlock, FeatConstants.SpecialQualities.Immunity, focus: "Illusions"));
            testCases[CreatureConstants.Grimlock].Add(BuildData(CreatureConstants.Grimlock, FeatConstants.SpecialQualities.Immunity, focus: "Attack forms that rely on sight"));
            testCases[CreatureConstants.Grimlock].Add(BuildData(CreatureConstants.Grimlock, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Grimlock].Add(BuildData(CreatureConstants.Grimlock, FeatConstants.Track));

            testCases[CreatureConstants.Gynosphinx].Add(BuildData(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ClairaudienceClairvoyance, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Gynosphinx].Add(BuildData(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Gynosphinx].Add(BuildData(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ReadMagic, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Gynosphinx].Add(BuildData(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Gynosphinx].Add(BuildData(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ComprehendLanguages, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Gynosphinx].Add(BuildData(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Gynosphinx].Add(BuildData(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Gynosphinx].Add(BuildData(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveCurse, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Gynosphinx].Add(BuildData(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LegendLore, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Gynosphinx].Add(BuildData(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SymbolOfDeath, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 18));
            testCases[CreatureConstants.Gynosphinx].Add(BuildData(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SymbolOfFear, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 18));
            testCases[CreatureConstants.Gynosphinx].Add(BuildData(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SymbolOfInsanity, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 18));
            testCases[CreatureConstants.Gynosphinx].Add(BuildData(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SymbolOfPain, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 18));
            testCases[CreatureConstants.Gynosphinx].Add(BuildData(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SymbolOfPersuasion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 18));
            testCases[CreatureConstants.Gynosphinx].Add(BuildData(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SymbolOfSleep, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 18));
            testCases[CreatureConstants.Gynosphinx].Add(BuildData(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SymbolOfStunning, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 18));

            testCases[CreatureConstants.Halfling_Deep].Add(BuildData(CreatureConstants.Halfling_Deep, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Halfling_Deep].Add(BuildData(CreatureConstants.Halfling_Deep, FeatConstants.SpecialQualities.Stonecunning));

            testCases[CreatureConstants.Halfling_Lightfoot] = [];

            testCases[CreatureConstants.Halfling_Tallfellow] = [];

            testCases[CreatureConstants.Harpy].Add(BuildData(CreatureConstants.Harpy, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Club, requiresEquipment: true));

            testCases[CreatureConstants.Hawk].Add(BuildData(CreatureConstants.Hawk, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.HellHound].Add(BuildData(CreatureConstants.HellHound, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.HellHound].Add(BuildData(CreatureConstants.HellHound, FeatConstants.Track));

            testCases[CreatureConstants.HellHound_NessianWarhound].Add(BuildData(CreatureConstants.HellHound_NessianWarhound, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.HellHound_NessianWarhound].Add(BuildData(CreatureConstants.HellHound_NessianWarhound, FeatConstants.Track));

            testCases[CreatureConstants.Hellcat_Bezekira].Add(BuildData(CreatureConstants.Hellcat_Bezekira, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Hellcat_Bezekira].Add(BuildData(CreatureConstants.Hellcat_Bezekira, FeatConstants.SpecialQualities.InvisibleInLight));
            testCases[CreatureConstants.Hellcat_Bezekira].Add(BuildData(CreatureConstants.Hellcat_Bezekira, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Hellcat_Bezekira].Add(BuildData(CreatureConstants.Hellcat_Bezekira, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Hellcat_Bezekira].Add(BuildData(CreatureConstants.Hellcat_Bezekira, FeatConstants.SpecialQualities.SpellResistance, power: 19));
            testCases[CreatureConstants.Hellcat_Bezekira].Add(BuildData(CreatureConstants.Hellcat_Bezekira, FeatConstants.SpecialQualities.Telepathy, power: 100));

            testCases[CreatureConstants.Hellwasp_Swarm].Add(BuildData(CreatureConstants.Hellwasp_Swarm, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Hellwasp_Swarm].Add(BuildData(CreatureConstants.Hellwasp_Swarm, FeatConstants.SpecialQualities.HiveMind));
            testCases[CreatureConstants.Hellwasp_Swarm].Add(BuildData(CreatureConstants.Hellwasp_Swarm, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

            testCases[CreatureConstants.Hezrou].Add(BuildData(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Hezrou].Add(BuildData(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.Hezrou].Add(BuildData(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Hezrou].Add(BuildData(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Hezrou].Add(BuildData(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Hezrou].Add(BuildData(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Hezrou].Add(BuildData(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.SpellResistance, power: 19));
            testCases[CreatureConstants.Hezrou].Add(BuildData(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.Telepathy, power: 100));
            testCases[CreatureConstants.Hezrou].Add(BuildData(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ChaosHammer, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Hezrou].Add(BuildData(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Hezrou].Add(BuildData(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyBlight, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Hezrou].Add(BuildData(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blasphemy, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));
            testCases[CreatureConstants.Hezrou].Add(BuildData(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GaseousForm, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Hieracosphinx] = [];

            testCases[CreatureConstants.Hippogriff].Add(BuildData(CreatureConstants.Hippogriff, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Hobgoblin].Add(BuildData(CreatureConstants.Hobgoblin, FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
            testCases[CreatureConstants.Hobgoblin].Add(BuildData(CreatureConstants.Hobgoblin, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
            testCases[CreatureConstants.Hobgoblin].Add(BuildData(CreatureConstants.Hobgoblin, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Javelin, requiresEquipment: true));
            testCases[CreatureConstants.Hobgoblin].Add(BuildData(CreatureConstants.Hobgoblin, FeatConstants.SpecialQualities.Darkvision, power: 60));

            testCases[CreatureConstants.Homunculus] = [];

            testCases[CreatureConstants.HornedDevil_Cornugon].Add(BuildData(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good, silver weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.HornedDevil_Cornugon].Add(BuildData(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
            testCases[CreatureConstants.HornedDevil_Cornugon].Add(BuildData(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.HornedDevil_Cornugon].Add(BuildData(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.HornedDevil_Cornugon].Add(BuildData(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.HornedDevil_Cornugon].Add(BuildData(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.Regeneration, focus: "Does not regenerate damage from good-aligned, silvered weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.HornedDevil_Cornugon].Add(BuildData(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.SeeInDarkness, focus: "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell"));
            testCases[CreatureConstants.HornedDevil_Cornugon].Add(BuildData(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.SpellResistance, power: 28));
            testCases[CreatureConstants.HornedDevil_Cornugon].Add(BuildData(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.Telepathy, power: 100));
            testCases[CreatureConstants.HornedDevil_Cornugon].Add(BuildData(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelChaos, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
            testCases[CreatureConstants.HornedDevil_Cornugon].Add(BuildData(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelGood, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
            testCases[CreatureConstants.HornedDevil_Cornugon].Add(BuildData(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicCircleAgainstGood, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.HornedDevil_Cornugon].Add(BuildData(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.HornedDevil_Cornugon].Add(BuildData(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PersistentImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
            testCases[CreatureConstants.HornedDevil_Cornugon].Add(BuildData(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fireball, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 13));
            testCases[CreatureConstants.HornedDevil_Cornugon].Add(BuildData(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LightningBolt, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 13));
            testCases[CreatureConstants.HornedDevil_Cornugon].Add(BuildData(CreatureConstants.HornedDevil_Cornugon, FeatConstants.WeaponProficiency_Exotic, focus: WeaponConstants.SpikedChain, requiresEquipment: true));

            testCases[CreatureConstants.Horse_Heavy].Add(BuildData(CreatureConstants.Horse_Heavy, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Horse_Heavy_War].Add(BuildData(CreatureConstants.Horse_Heavy_War, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Horse_Light].Add(BuildData(CreatureConstants.Horse_Light, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Horse_Light_War].Add(BuildData(CreatureConstants.Horse_Light_War, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.HoundArchon].Add(BuildData(CreatureConstants.HoundArchon, FeatConstants.SpecialQualities.AuraOfMenace, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
            testCases[CreatureConstants.HoundArchon].Add(BuildData(CreatureConstants.HoundArchon, FeatConstants.SpecialQualities.ChangeShape, focus: "Any canine form of Small to Large size", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.HoundArchon].Add(BuildData(CreatureConstants.HoundArchon, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to evil weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.HoundArchon].Add(BuildData(CreatureConstants.HoundArchon, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.HoundArchon].Add(BuildData(CreatureConstants.HoundArchon, FeatConstants.SpecialQualities.SpellResistance, power: 16));
            testCases[CreatureConstants.HoundArchon].Add(BuildData(CreatureConstants.HoundArchon, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Aid, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.HoundArchon].Add(BuildData(CreatureConstants.HoundArchon, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ContinualFlame, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.HoundArchon].Add(BuildData(CreatureConstants.HoundArchon, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectEvil, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.HoundArchon].Add(BuildData(CreatureConstants.HoundArchon, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Message, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.HoundArchon].Add(BuildData(CreatureConstants.HoundArchon, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatsword, requiresEquipment: true));

            testCases[CreatureConstants.Howler] = [];

            testCases[CreatureConstants.Human].Add(BuildData(CreatureConstants.Human, FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

            testCases[CreatureConstants.Hydra_5Heads].Add(BuildData(CreatureConstants.Hydra_5Heads, FeatConstants.SpecialQualities.FastHealing, power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Hydra_5Heads].Add(BuildData(CreatureConstants.Hydra_5Heads, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Hydra_5Heads].Add(BuildData(CreatureConstants.Hydra_5Heads, FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

            testCases[CreatureConstants.Hydra_6Heads].Add(BuildData(CreatureConstants.Hydra_6Heads, FeatConstants.SpecialQualities.FastHealing, power: 16, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Hydra_6Heads].Add(BuildData(CreatureConstants.Hydra_6Heads, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Hydra_6Heads].Add(BuildData(CreatureConstants.Hydra_6Heads, FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

            testCases[CreatureConstants.Hydra_7Heads].Add(BuildData(CreatureConstants.Hydra_7Heads, FeatConstants.SpecialQualities.FastHealing, power: 17, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Hydra_7Heads].Add(BuildData(CreatureConstants.Hydra_7Heads, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Hydra_7Heads].Add(BuildData(CreatureConstants.Hydra_7Heads, FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

            testCases[CreatureConstants.Hydra_8Heads].Add(BuildData(CreatureConstants.Hydra_8Heads, FeatConstants.SpecialQualities.FastHealing, power: 18, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Hydra_8Heads].Add(BuildData(CreatureConstants.Hydra_8Heads, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Hydra_8Heads].Add(BuildData(CreatureConstants.Hydra_8Heads, FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

            testCases[CreatureConstants.Hydra_9Heads].Add(BuildData(CreatureConstants.Hydra_9Heads, FeatConstants.SpecialQualities.FastHealing, power: 19, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Hydra_9Heads].Add(BuildData(CreatureConstants.Hydra_9Heads, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Hydra_9Heads].Add(BuildData(CreatureConstants.Hydra_9Heads, FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

            testCases[CreatureConstants.Hydra_10Heads].Add(BuildData(CreatureConstants.Hydra_10Heads, FeatConstants.SpecialQualities.FastHealing, power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Hydra_10Heads].Add(BuildData(CreatureConstants.Hydra_10Heads, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Hydra_10Heads].Add(BuildData(CreatureConstants.Hydra_10Heads, FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

            testCases[CreatureConstants.Hydra_11Heads].Add(BuildData(CreatureConstants.Hydra_11Heads, FeatConstants.SpecialQualities.FastHealing, power: 21, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Hydra_11Heads].Add(BuildData(CreatureConstants.Hydra_11Heads, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Hydra_11Heads].Add(BuildData(CreatureConstants.Hydra_11Heads, FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

            testCases[CreatureConstants.Hydra_12Heads].Add(BuildData(CreatureConstants.Hydra_12Heads, FeatConstants.SpecialQualities.FastHealing, power: 22, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Hydra_12Heads].Add(BuildData(CreatureConstants.Hydra_12Heads, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Hydra_12Heads].Add(BuildData(CreatureConstants.Hydra_12Heads, FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

            testCases[CreatureConstants.Hyena].Add(BuildData(CreatureConstants.Hyena, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.IceDevil_Gelugon].Add(BuildData(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.IceDevil_Gelugon].Add(BuildData(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
            testCases[CreatureConstants.IceDevil_Gelugon].Add(BuildData(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.IceDevil_Gelugon].Add(BuildData(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.IceDevil_Gelugon].Add(BuildData(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.IceDevil_Gelugon].Add(BuildData(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.Regeneration, focus: "Does not regenerate good damage", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.IceDevil_Gelugon].Add(BuildData(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.SeeInDarkness, focus: "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell"));
            testCases[CreatureConstants.IceDevil_Gelugon].Add(BuildData(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.SpellResistance, power: 25));
            testCases[CreatureConstants.IceDevil_Gelugon].Add(BuildData(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.Telepathy, power: 100));
            testCases[CreatureConstants.IceDevil_Gelugon].Add(BuildData(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ConeOfCold, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
            testCases[CreatureConstants.IceDevil_Gelugon].Add(BuildData(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fly, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.IceDevil_Gelugon].Add(BuildData(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.IceStorm, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 14));
            testCases[CreatureConstants.IceDevil_Gelugon].Add(BuildData(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.IceDevil_Gelugon].Add(BuildData(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PersistentImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
            testCases[CreatureConstants.IceDevil_Gelugon].Add(BuildData(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyAura, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 18));
            testCases[CreatureConstants.IceDevil_Gelugon].Add(BuildData(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfIce, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 14));
            testCases[CreatureConstants.IceDevil_Gelugon].Add(BuildData(CreatureConstants.IceDevil_Gelugon, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Longspear, requiresEquipment: true));
            testCases[CreatureConstants.IceDevil_Gelugon].Add(BuildData(CreatureConstants.IceDevil_Gelugon, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Spear, requiresEquipment: true));
            testCases[CreatureConstants.IceDevil_Gelugon].Add(BuildData(CreatureConstants.IceDevil_Gelugon, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Shortspear, requiresEquipment: true));

            testCases[CreatureConstants.Imp].Add(BuildData(CreatureConstants.Imp, FeatConstants.SpecialQualities.AlternateForm, randomFociQuantity: RollHelper.GetRollWithMostEvenDistribution(1, 2), focus: "Imp Alternate Form", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Imp].Add(BuildData(CreatureConstants.Imp, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good or silver weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Imp].Add(BuildData(CreatureConstants.Imp, FeatConstants.SpecialQualities.FastHealing, power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Imp].Add(BuildData(CreatureConstants.Imp, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.Imp].Add(BuildData(CreatureConstants.Imp, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Imp].Add(BuildData(CreatureConstants.Imp, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectGood, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Imp].Add(BuildData(CreatureConstants.Imp, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Imp].Add(BuildData(CreatureConstants.Imp, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + ": self only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Imp].Add(BuildData(CreatureConstants.Imp, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Imp].Add(BuildData(CreatureConstants.Imp, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Commune + ": ask 6 questions", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week));

            testCases[CreatureConstants.InvisibleStalker].Add(BuildData(CreatureConstants.InvisibleStalker, FeatConstants.SpecialQualities.NaturalInvisibility));
            testCases[CreatureConstants.InvisibleStalker].Add(BuildData(CreatureConstants.InvisibleStalker, FeatConstants.SpecialQualities.Tracking_Improved));

            testCases[CreatureConstants.Janni].Add(BuildData(CreatureConstants.Janni, FeatConstants.SpecialQualities.Telepathy, power: 100));
            testCases[CreatureConstants.Janni].Add(BuildData(CreatureConstants.Janni, FeatConstants.SpecialQualities.ElementalEndurance));
            testCases[CreatureConstants.Janni].Add(BuildData(CreatureConstants.Janni, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlaneShift + ": Genie and up to 8 other creatures, provided they all link hands with the genie", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Janni].Add(BuildData(CreatureConstants.Janni, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + ": self only", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Janni].Add(BuildData(CreatureConstants.Janni, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Janni].Add(BuildData(CreatureConstants.Janni, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateFoodAndWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Janni].Add(BuildData(CreatureConstants.Janni, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EtherealJaunt, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Janni].Add(BuildData(CreatureConstants.Janni, FeatConstants.Initiative_Improved, power: 4));
            testCases[CreatureConstants.Janni].Add(BuildData(CreatureConstants.Janni, FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
            testCases[CreatureConstants.Janni].Add(BuildData(CreatureConstants.Janni, FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
            testCases[CreatureConstants.Janni].Add(BuildData(CreatureConstants.Janni, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Scimitar, requiresEquipment: true));
            testCases[CreatureConstants.Janni].Add(BuildData(CreatureConstants.Janni, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));

            testCases[CreatureConstants.Kobold].Add(BuildData(CreatureConstants.Kobold, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Kobold].Add(BuildData(CreatureConstants.Kobold, FeatConstants.SpecialQualities.LightSensitivity));
            testCases[CreatureConstants.Kobold].Add(BuildData(CreatureConstants.Kobold, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Spear, requiresEquipment: true));
            testCases[CreatureConstants.Kobold].Add(BuildData(CreatureConstants.Kobold, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Sling, requiresEquipment: true));

            testCases[CreatureConstants.Kolyarut].Add(BuildData(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to chaotic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Kolyarut].Add(BuildData(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.FastHealing, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Kolyarut].Add(BuildData(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.SpellResistance, power: 22));
            testCases[CreatureConstants.Kolyarut].Add(BuildData(CreatureConstants.Kolyarut, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
            testCases[CreatureConstants.Kolyarut].Add(BuildData(CreatureConstants.Kolyarut, FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
            testCases[CreatureConstants.Kolyarut].Add(BuildData(CreatureConstants.Kolyarut, FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
            testCases[CreatureConstants.Kolyarut].Add(BuildData(CreatureConstants.Kolyarut, FeatConstants.ArmorProficiency_Heavy, requiresEquipment: true));
            testCases[CreatureConstants.Kolyarut].Add(BuildData(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DiscernLies, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Kolyarut].Add(BuildData(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DisguiseSelf, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Kolyarut].Add(BuildData(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fear, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Kolyarut].Add(BuildData(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldPerson, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Kolyarut].Add(BuildData(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Kolyarut].Add(BuildData(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateCreature, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Kolyarut].Add(BuildData(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Kolyarut].Add(BuildData(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldMonster, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Kolyarut].Add(BuildData(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MarkOfJustice, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Kolyarut].Add(BuildData(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GeasQuest, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week));

            testCases[CreatureConstants.Kraken].Add(BuildData(CreatureConstants.Kraken, FeatConstants.SpecialQualities.InkCloud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Minute));
            testCases[CreatureConstants.Kraken].Add(BuildData(CreatureConstants.Kraken, FeatConstants.SpecialQualities.Jet, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Kraken].Add(BuildData(CreatureConstants.Kraken, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Kraken].Add(BuildData(CreatureConstants.Kraken, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Kraken].Add(BuildData(CreatureConstants.Kraken, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DominateAnimal, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Kraken].Add(BuildData(CreatureConstants.Kraken, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ResistEnergy, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Krenshar].Add(BuildData(CreatureConstants.Krenshar, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Krenshar].Add(BuildData(CreatureConstants.Krenshar, FeatConstants.Track));

            testCases[CreatureConstants.KuoToa].Add(BuildData(CreatureConstants.KuoToa, FeatConstants.SpecialQualities.Adhesive));
            testCases[CreatureConstants.KuoToa].Add(BuildData(CreatureConstants.KuoToa, FeatConstants.SpecialQualities.Amphibious));
            testCases[CreatureConstants.KuoToa].Add(BuildData(CreatureConstants.KuoToa, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.KuoToa].Add(BuildData(CreatureConstants.KuoToa, FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
            testCases[CreatureConstants.KuoToa].Add(BuildData(CreatureConstants.KuoToa, FeatConstants.SpecialQualities.KeenSight));
            testCases[CreatureConstants.KuoToa].Add(BuildData(CreatureConstants.KuoToa, FeatConstants.SpecialQualities.LightBlindness));
            testCases[CreatureConstants.KuoToa].Add(BuildData(CreatureConstants.KuoToa, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.KuoToa].Add(BuildData(CreatureConstants.KuoToa, FeatConstants.SpecialQualities.Slippery));
            testCases[CreatureConstants.KuoToa].Add(BuildData(CreatureConstants.KuoToa, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Shortspear, requiresEquipment: true));
            testCases[CreatureConstants.KuoToa].Add(BuildData(CreatureConstants.KuoToa, FeatConstants.WeaponProficiency_Exotic, focus: WeaponConstants.PincerStaff, requiresEquipment: true));
            testCases[CreatureConstants.KuoToa].Add(BuildData(CreatureConstants.KuoToa, FeatConstants.Alertness, power: 2));

            testCases[CreatureConstants.Lamia].Add(BuildData(CreatureConstants.Lamia, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Dagger, requiresEquipment: true));
            testCases[CreatureConstants.Lamia].Add(BuildData(CreatureConstants.Lamia, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DisguiseSelf, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Lamia].Add(BuildData(CreatureConstants.Lamia, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Lamia].Add(BuildData(CreatureConstants.Lamia, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Lamia].Add(BuildData(CreatureConstants.Lamia, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MajorImage, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Lamia].Add(BuildData(CreatureConstants.Lamia, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MirrorImage, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Lamia].Add(BuildData(CreatureConstants.Lamia, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Lamia].Add(BuildData(CreatureConstants.Lamia, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeepSlumber, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));

            testCases[CreatureConstants.Lammasu].Add(BuildData(CreatureConstants.Lammasu, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicCircleAgainstEvil, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
            testCases[CreatureConstants.Lammasu].Add(BuildData(CreatureConstants.Lammasu, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility_Greater + ": self only", frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Lammasu].Add(BuildData(CreatureConstants.Lammasu, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DimensionDoor, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.LanternArchon].Add(BuildData(CreatureConstants.LanternArchon, FeatConstants.SpecialQualities.AuraOfMenace, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.LanternArchon].Add(BuildData(CreatureConstants.LanternArchon, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic, evil weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.LanternArchon].Add(BuildData(CreatureConstants.LanternArchon, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Aid, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.LanternArchon].Add(BuildData(CreatureConstants.LanternArchon, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ContinualFlame, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.LanternArchon].Add(BuildData(CreatureConstants.LanternArchon, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectEvil, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

            testCases[CreatureConstants.Lemure].Add(BuildData(CreatureConstants.Lemure, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good or silver weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Lemure].Add(BuildData(CreatureConstants.Lemure, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
            testCases[CreatureConstants.Lemure].Add(BuildData(CreatureConstants.Lemure, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.Lemure].Add(BuildData(CreatureConstants.Lemure, FeatConstants.SpecialQualities.Immunity, focus: "Mind-Affecting Effects"));
            testCases[CreatureConstants.Lemure].Add(BuildData(CreatureConstants.Lemure, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Lemure].Add(BuildData(CreatureConstants.Lemure, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Lemure].Add(BuildData(CreatureConstants.Lemure, FeatConstants.SpecialQualities.SeeInDarkness, focus: "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell"));

            testCases[CreatureConstants.Leonal].Add(BuildData(CreatureConstants.Leonal, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to evil, silver weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Leonal].Add(BuildData(CreatureConstants.Leonal, FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
            testCases[CreatureConstants.Leonal].Add(BuildData(CreatureConstants.Leonal, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Leonal].Add(BuildData(CreatureConstants.Leonal, FeatConstants.SpecialQualities.LayOnHands));
            testCases[CreatureConstants.Leonal].Add(BuildData(CreatureConstants.Leonal, FeatConstants.SpecialQualities.ProtectiveAura));
            testCases[CreatureConstants.Leonal].Add(BuildData(CreatureConstants.Leonal, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Leonal].Add(BuildData(CreatureConstants.Leonal, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Sonic, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Leonal].Add(BuildData(CreatureConstants.Leonal, FeatConstants.SpecialQualities.SpellResistance, power: 28));
            testCases[CreatureConstants.Leonal].Add(BuildData(CreatureConstants.Leonal, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals + ": does not require sound", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Leonal].Add(BuildData(CreatureConstants.Leonal, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Leonal].Add(BuildData(CreatureConstants.Leonal, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fireball, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 13));
            testCases[CreatureConstants.Leonal].Add(BuildData(CreatureConstants.Leonal, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
            testCases[CreatureConstants.Leonal].Add(BuildData(CreatureConstants.Leonal, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfForce, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Leonal].Add(BuildData(CreatureConstants.Leonal, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CureCriticalWounds, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Leonal].Add(BuildData(CreatureConstants.Leonal, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.NeutralizePoison, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Leonal].Add(BuildData(CreatureConstants.Leonal, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveDisease, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Leonal].Add(BuildData(CreatureConstants.Leonal, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Heal, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 16));

            testCases[CreatureConstants.Leopard].Add(BuildData(CreatureConstants.Leopard, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Lillend].Add(BuildData(CreatureConstants.Lillend, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.Lillend].Add(BuildData(CreatureConstants.Lillend, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Lillend].Add(BuildData(CreatureConstants.Lillend, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Lillend].Add(BuildData(CreatureConstants.Lillend, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HallucinatoryTerrain, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Lillend].Add(BuildData(CreatureConstants.Lillend, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Knock, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Lillend].Add(BuildData(CreatureConstants.Lillend, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Light, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Lillend].Add(BuildData(CreatureConstants.Lillend, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmPerson, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
            testCases[CreatureConstants.Lillend].Add(BuildData(CreatureConstants.Lillend, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Lillend].Add(BuildData(CreatureConstants.Lillend, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithPlants, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Lillend].Add(BuildData(CreatureConstants.Lillend, FeatConstants.SpecialQualities.SpellLikeAbility, focus: "Bardic music ability as a 6th-level Bard", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day)); //HACK: Once this is in a core project and incorporates class features as well, we will add it that way

            testCases[CreatureConstants.Lion].Add(BuildData(CreatureConstants.Lion, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Lion_Dire].Add(BuildData(CreatureConstants.Lion_Dire, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Lizard].Add(BuildData(CreatureConstants.Lizard, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Lizard_Monitor] = [];

            testCases[CreatureConstants.Lizardfolk].Add(BuildData(CreatureConstants.Lizardfolk, FeatConstants.SpecialQualities.HoldBreath));
            testCases[CreatureConstants.Lizardfolk].Add(BuildData(CreatureConstants.Lizardfolk, FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
            testCases[CreatureConstants.Lizardfolk].Add(BuildData(CreatureConstants.Lizardfolk, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Club, requiresEquipment: true));
            testCases[CreatureConstants.Lizardfolk].Add(BuildData(CreatureConstants.Lizardfolk, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Javelin, requiresEquipment: true));

            testCases[CreatureConstants.Locathah].Add(BuildData(CreatureConstants.Locathah, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Longspear, requiresEquipment: true));
            testCases[CreatureConstants.Locathah].Add(BuildData(CreatureConstants.Locathah, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.LightCrossbow, requiresEquipment: true));

            testCases[CreatureConstants.Locust_Swarm] = [];

            testCases[CreatureConstants.Magmin].Add(BuildData(CreatureConstants.Magmin, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Magmin].Add(BuildData(CreatureConstants.Magmin, FeatConstants.SpecialQualities.MeltWeapons, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveBaseValue: 11));

            testCases[CreatureConstants.MantaRay] = [];

            testCases[CreatureConstants.Manticore].Add(BuildData(CreatureConstants.Manticore, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Manticore].Add(BuildData(CreatureConstants.Manticore, FeatConstants.Track));

            testCases[CreatureConstants.Marilith].Add(BuildData(CreatureConstants.Marilith, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Marilith].Add(BuildData(CreatureConstants.Marilith, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.Marilith].Add(BuildData(CreatureConstants.Marilith, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Marilith].Add(BuildData(CreatureConstants.Marilith, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Marilith].Add(BuildData(CreatureConstants.Marilith, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Marilith].Add(BuildData(CreatureConstants.Marilith, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good, cold iron weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Marilith].Add(BuildData(CreatureConstants.Marilith, FeatConstants.SpecialQualities.SpellResistance, power: 25));
            testCases[CreatureConstants.Marilith].Add(BuildData(CreatureConstants.Marilith, FeatConstants.SpecialQualities.Telepathy, power: 100));
            testCases[CreatureConstants.Marilith].Add(BuildData(CreatureConstants.Marilith, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AlignWeapon, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Marilith].Add(BuildData(CreatureConstants.Marilith, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.BladeBarrier, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 16));
            testCases[CreatureConstants.Marilith].Add(BuildData(CreatureConstants.Marilith, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicWeapon, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Marilith].Add(BuildData(CreatureConstants.Marilith, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ProjectImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));
            testCases[CreatureConstants.Marilith].Add(BuildData(CreatureConstants.Marilith, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Marilith].Add(BuildData(CreatureConstants.Marilith, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Telekinesis, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
            testCases[CreatureConstants.Marilith].Add(BuildData(CreatureConstants.Marilith, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Marilith].Add(BuildData(CreatureConstants.Marilith, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyAura, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 18));
            testCases[CreatureConstants.Marilith].Add(BuildData(CreatureConstants.Marilith, FeatConstants.Monster.MultiweaponFighting, requiresEquipment: true));
            testCases[CreatureConstants.Marilith].Add(BuildData(CreatureConstants.Marilith, FeatConstants.SpecialQualities.MultiweaponFighting_Superior, requiresEquipment: true));
            testCases[CreatureConstants.Marilith].Add(BuildData(CreatureConstants.Marilith, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));

            testCases[CreatureConstants.Marut].Add(BuildData(CreatureConstants.Marut, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to chaotic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Marut].Add(BuildData(CreatureConstants.Marut, FeatConstants.SpecialQualities.FastHealing, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Marut].Add(BuildData(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellResistance, power: 25));
            testCases[CreatureConstants.Marut].Add(BuildData(CreatureConstants.Marut, FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
            testCases[CreatureConstants.Marut].Add(BuildData(CreatureConstants.Marut, FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
            testCases[CreatureConstants.Marut].Add(BuildData(CreatureConstants.Marut, FeatConstants.ArmorProficiency_Heavy, requiresEquipment: true));
            testCases[CreatureConstants.Marut].Add(BuildData(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AirWalk, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Marut].Add(BuildData(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DimensionDoor, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Marut].Add(BuildData(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fear, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Marut].Add(BuildData(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Command_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
            testCases[CreatureConstants.Marut].Add(BuildData(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Marut].Add(BuildData(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.InflictLightWounds_Mass, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
            testCases[CreatureConstants.Marut].Add(BuildData(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateCreature, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Marut].Add(BuildData(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TrueSeeing, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Marut].Add(BuildData(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ChainLightning, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 16));
            testCases[CreatureConstants.Marut].Add(BuildData(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CircleOfDeath, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 16));
            testCases[CreatureConstants.Marut].Add(BuildData(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MarkOfJustice, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Marut].Add(BuildData(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfForce, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Marut].Add(BuildData(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Earthquake, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 18));
            testCases[CreatureConstants.Marut].Add(BuildData(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GeasQuest, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week));
            testCases[CreatureConstants.Marut].Add(BuildData(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlaneShift, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));

            testCases[CreatureConstants.Medusa].Add(BuildData(CreatureConstants.Medusa, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));
            testCases[CreatureConstants.Medusa].Add(BuildData(CreatureConstants.Medusa, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Dagger, requiresEquipment: true));

            testCases[CreatureConstants.Megaraptor].Add(BuildData(CreatureConstants.Megaraptor, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Mephit_Air].Add(BuildData(CreatureConstants.Mephit_Air, FeatConstants.SpecialQualities.FastHealing, focus: "Exposed to moving air", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Mephit_Air].Add(BuildData(CreatureConstants.Mephit_Air, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Mephit_Air].Add(BuildData(CreatureConstants.Mephit_Air, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blur, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
            testCases[CreatureConstants.Mephit_Air].Add(BuildData(CreatureConstants.Mephit_Air, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 12));

            testCases[CreatureConstants.Mephit_Dust].Add(BuildData(CreatureConstants.Mephit_Dust, FeatConstants.SpecialQualities.FastHealing, focus: "In arid, dusty environment", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Mephit_Dust].Add(BuildData(CreatureConstants.Mephit_Dust, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Mephit_Dust].Add(BuildData(CreatureConstants.Mephit_Dust, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blur, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
            testCases[CreatureConstants.Mephit_Dust].Add(BuildData(CreatureConstants.Mephit_Dust, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WindWall, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));

            testCases[CreatureConstants.Mephit_Earth].Add(BuildData(CreatureConstants.Mephit_Earth, FeatConstants.SpecialQualities.FastHealing, focus: "Underground or buried up to its waist in earth", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Mephit_Earth].Add(BuildData(CreatureConstants.Mephit_Earth, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Mephit_Earth].Add(BuildData(CreatureConstants.Mephit_Earth, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EnlargePerson + ": self only", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
            testCases[CreatureConstants.Mephit_Earth].Add(BuildData(CreatureConstants.Mephit_Earth, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SoftenEarthAndStone, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Mephit_Fire].Add(BuildData(CreatureConstants.Mephit_Fire, FeatConstants.SpecialQualities.FastHealing, focus: "Touching a flame at least as large as a torch", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Mephit_Fire].Add(BuildData(CreatureConstants.Mephit_Fire, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Mephit_Fire].Add(BuildData(CreatureConstants.Mephit_Fire, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ScorchingRay, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.Mephit_Fire].Add(BuildData(CreatureConstants.Mephit_Fire, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HeatMetal, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));

            testCases[CreatureConstants.Mephit_Ice].Add(BuildData(CreatureConstants.Mephit_Ice, FeatConstants.SpecialQualities.FastHealing, focus: "Touching a piece of ice at least Tiny in size, or ambient temperature is freezing or lower", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Mephit_Ice].Add(BuildData(CreatureConstants.Mephit_Ice, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Mephit_Ice].Add(BuildData(CreatureConstants.Mephit_Ice, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicMissile, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
            testCases[CreatureConstants.Mephit_Ice].Add(BuildData(CreatureConstants.Mephit_Ice, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ChillMetal, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));

            testCases[CreatureConstants.Mephit_Magma].Add(BuildData(CreatureConstants.Mephit_Magma, FeatConstants.SpecialQualities.FastHealing, focus: "Touching magma, lava, or a flame at least as large as a torch", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Mephit_Magma].Add(BuildData(CreatureConstants.Mephit_Magma, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Mephit_Magma].Add(BuildData(CreatureConstants.Mephit_Magma, FeatConstants.SpecialQualities.ChangeShape, focus: "A pool of lava 3 feet in diameter and 6 inches deep", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
            testCases[CreatureConstants.Mephit_Magma].Add(BuildData(CreatureConstants.Mephit_Magma, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Pyrotechnics, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));

            testCases[CreatureConstants.Mephit_Ooze].Add(BuildData(CreatureConstants.Mephit_Ooze, FeatConstants.SpecialQualities.FastHealing, focus: "In a wet or muddy environment", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Mephit_Ooze].Add(BuildData(CreatureConstants.Mephit_Ooze, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Mephit_Ooze].Add(BuildData(CreatureConstants.Mephit_Ooze, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AcidArrow, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
            testCases[CreatureConstants.Mephit_Ooze].Add(BuildData(CreatureConstants.Mephit_Ooze, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StinkingCloud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 13));

            testCases[CreatureConstants.Mephit_Salt].Add(BuildData(CreatureConstants.Mephit_Salt, FeatConstants.SpecialQualities.FastHealing, focus: "In an arid environment", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Mephit_Salt].Add(BuildData(CreatureConstants.Mephit_Salt, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Mephit_Salt].Add(BuildData(CreatureConstants.Mephit_Salt, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Glitterdust, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.Mephit_Salt].Add(BuildData(CreatureConstants.Mephit_Salt, FeatConstants.SpecialQualities.SpellLikeAbility, focus: "Draw moisture from the air", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 12));

            testCases[CreatureConstants.Mephit_Steam].Add(BuildData(CreatureConstants.Mephit_Steam, FeatConstants.SpecialQualities.FastHealing, focus: "Touching boiling water or in a hot, humid area", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Mephit_Steam].Add(BuildData(CreatureConstants.Mephit_Steam, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Mephit_Steam].Add(BuildData(CreatureConstants.Mephit_Steam, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blur, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
            testCases[CreatureConstants.Mephit_Steam].Add(BuildData(CreatureConstants.Mephit_Steam, FeatConstants.SpecialQualities.SpellLikeAbility, focus: "Rainstorm of boiling water", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 12));

            testCases[CreatureConstants.Mephit_Water].Add(BuildData(CreatureConstants.Mephit_Water, FeatConstants.SpecialQualities.FastHealing, focus: "Exposed to rain or submerged up to its waist in water", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Mephit_Water].Add(BuildData(CreatureConstants.Mephit_Water, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Mephit_Water].Add(BuildData(CreatureConstants.Mephit_Water, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AcidArrow, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
            testCases[CreatureConstants.Mephit_Water].Add(BuildData(CreatureConstants.Mephit_Water, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StinkingCloud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 13));

            testCases[CreatureConstants.Merfolk].Add(BuildData(CreatureConstants.Merfolk, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Trident, requiresEquipment: true));
            testCases[CreatureConstants.Merfolk].Add(BuildData(CreatureConstants.Merfolk, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.HeavyCrossbow, requiresEquipment: true));
            testCases[CreatureConstants.Merfolk].Add(BuildData(CreatureConstants.Merfolk, FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
            testCases[CreatureConstants.Merfolk].Add(BuildData(CreatureConstants.Merfolk, FeatConstants.SpecialQualities.Amphibious));
            testCases[CreatureConstants.Merfolk].Add(BuildData(CreatureConstants.Merfolk, FeatConstants.SpecialQualities.LowLightVision));

            testCases[CreatureConstants.Mimic].Add(BuildData(CreatureConstants.Mimic, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Mimic].Add(BuildData(CreatureConstants.Mimic, FeatConstants.SpecialQualities.MimicShape));

            testCases[CreatureConstants.MindFlayer].Add(BuildData(CreatureConstants.MindFlayer, FeatConstants.SpecialQualities.SpellResistance, power: 25));
            testCases[CreatureConstants.MindFlayer].Add(BuildData(CreatureConstants.MindFlayer, FeatConstants.SpecialQualities.Telepathy, power: 100));
            testCases[CreatureConstants.MindFlayer].Add(BuildData(CreatureConstants.MindFlayer, FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.CharmMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.MindFlayer].Add(BuildData(CreatureConstants.MindFlayer, FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.DetectThoughts, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.MindFlayer].Add(BuildData(CreatureConstants.MindFlayer, FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.Levitate, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.MindFlayer].Add(BuildData(CreatureConstants.MindFlayer, FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.PlaneShift, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.MindFlayer].Add(BuildData(CreatureConstants.MindFlayer, FeatConstants.SpecialQualities.Psionic, focus: SpellConstants.Suggestion, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));

            testCases[CreatureConstants.Minotaur].Add(BuildData(CreatureConstants.Minotaur, FeatConstants.SpecialQualities.NaturalCunning));
            testCases[CreatureConstants.Minotaur].Add(BuildData(CreatureConstants.Minotaur, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Minotaur].Add(BuildData(CreatureConstants.Minotaur, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greataxe, requiresEquipment: true));

            testCases[CreatureConstants.Mohrg] = [];

            testCases[CreatureConstants.Monkey].Add(BuildData(CreatureConstants.Monkey, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Mule].Add(BuildData(CreatureConstants.Mule, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Mummy].Add(BuildData(CreatureConstants.Mummy, FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Mummy].Add(BuildData(CreatureConstants.Mummy, FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));

            testCases[CreatureConstants.Naga_Dark].Add(BuildData(CreatureConstants.Naga_Dark, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyTimePeriod: FeatConstants.Frequencies.Constant, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.Naga_Dark].Add(BuildData(CreatureConstants.Naga_Dark, FeatConstants.SpecialQualities.Immunity, focus: "Any form of mind reading"));
            testCases[CreatureConstants.Naga_Dark].Add(BuildData(CreatureConstants.Naga_Dark, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.Naga_Dark].Add(BuildData(CreatureConstants.Naga_Dark, FeatConstants.EschewMaterials));

            testCases[CreatureConstants.Naga_Guardian].Add(BuildData(CreatureConstants.Naga_Guardian, FeatConstants.EschewMaterials));

            testCases[CreatureConstants.Naga_Spirit].Add(BuildData(CreatureConstants.Naga_Spirit, FeatConstants.EschewMaterials));

            testCases[CreatureConstants.Naga_Water].Add(BuildData(CreatureConstants.Naga_Water, FeatConstants.EschewMaterials));

            testCases[CreatureConstants.Nalfeshnee].Add(BuildData(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Nalfeshnee].Add(BuildData(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.Nalfeshnee].Add(BuildData(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Nalfeshnee].Add(BuildData(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Nalfeshnee].Add(BuildData(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Nalfeshnee].Add(BuildData(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Nalfeshnee].Add(BuildData(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.SpellResistance, power: 22));
            testCases[CreatureConstants.Nalfeshnee].Add(BuildData(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.Telepathy, power: 100));
            testCases[CreatureConstants.Nalfeshnee].Add(BuildData(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TrueSeeing, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
            testCases[CreatureConstants.Nalfeshnee].Add(BuildData(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CallLightning, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 13));
            testCases[CreatureConstants.Nalfeshnee].Add(BuildData(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Feeblemind, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
            testCases[CreatureConstants.Nalfeshnee].Add(BuildData(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Nalfeshnee].Add(BuildData(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Slow, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Nalfeshnee].Add(BuildData(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Nalfeshnee].Add(BuildData(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyAura, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 18));

            testCases[CreatureConstants.NightHag].Add(BuildData(CreatureConstants.NightHag, FeatConstants.SpecialQualities.ChangeShape, focus: "Any Small or Medium Humanoid", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.NightHag].Add(BuildData(CreatureConstants.NightHag, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron, magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.NightHag].Add(BuildData(CreatureConstants.NightHag, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
            testCases[CreatureConstants.NightHag].Add(BuildData(CreatureConstants.NightHag, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
            testCases[CreatureConstants.NightHag].Add(BuildData(CreatureConstants.NightHag, FeatConstants.SpecialQualities.Immunity, focus: "Charm"));
            testCases[CreatureConstants.NightHag].Add(BuildData(CreatureConstants.NightHag, FeatConstants.SpecialQualities.Immunity, focus: SpellConstants.Sleep));
            testCases[CreatureConstants.NightHag].Add(BuildData(CreatureConstants.NightHag, FeatConstants.SpecialQualities.Immunity, focus: "Fear"));
            testCases[CreatureConstants.NightHag].Add(BuildData(CreatureConstants.NightHag, FeatConstants.SpecialQualities.SpellResistance, power: 25));
            testCases[CreatureConstants.NightHag].Add(BuildData(CreatureConstants.NightHag, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectChaos, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.NightHag].Add(BuildData(CreatureConstants.NightHag, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectEvil, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.NightHag].Add(BuildData(CreatureConstants.NightHag, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectGood, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.NightHag].Add(BuildData(CreatureConstants.NightHag, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectLaw, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.NightHag].Add(BuildData(CreatureConstants.NightHag, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.NightHag].Add(BuildData(CreatureConstants.NightHag, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicMissile, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.NightHag].Add(BuildData(CreatureConstants.NightHag, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RayOfEnfeeblement, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.NightHag].Add(BuildData(CreatureConstants.NightHag, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Sleep, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
            testCases[CreatureConstants.NightHag].Add(BuildData(CreatureConstants.NightHag, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Etherealness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

            testCases[CreatureConstants.Nightcrawler].Add(BuildData(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.AversionToDaylight));
            testCases[CreatureConstants.Nightcrawler].Add(BuildData(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.DesecratingAura));
            testCases[CreatureConstants.Nightcrawler].Add(BuildData(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to silver, magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Nightcrawler].Add(BuildData(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
            testCases[CreatureConstants.Nightcrawler].Add(BuildData(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellResistance, power: 31));
            testCases[CreatureConstants.Nightcrawler].Add(BuildData(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.Telepathy, power: 100));
            testCases[CreatureConstants.Nightcrawler].Add(BuildData(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.Tremorsense, power: 60));
            testCases[CreatureConstants.Nightcrawler].Add(BuildData(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Contagion, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 14));
            testCases[CreatureConstants.Nightcrawler].Add(BuildData(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeeperDarkness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Nightcrawler].Add(BuildData(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Nightcrawler].Add(BuildData(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Nightcrawler].Add(BuildData(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Haste, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Nightcrawler].Add(BuildData(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Nightcrawler].Add(BuildData(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Nightcrawler].Add(BuildData(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyBlight, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Nightcrawler].Add(BuildData(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ConeOfCold, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
            testCases[CreatureConstants.Nightcrawler].Add(BuildData(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Confusion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Nightcrawler].Add(BuildData(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldMonster, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
            testCases[CreatureConstants.Nightcrawler].Add(BuildData(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FingerOfDeath, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 17));
            testCases[CreatureConstants.Nightcrawler].Add(BuildData(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldMonster_Mass, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 19));
            testCases[CreatureConstants.Nightcrawler].Add(BuildData(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlaneShift, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));

            testCases[CreatureConstants.Nightmare].Add(BuildData(CreatureConstants.Nightmare, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AstralProjection, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Nightmare].Add(BuildData(CreatureConstants.Nightmare, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Etherealness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

            testCases[CreatureConstants.Nightmare_Cauchemar].Add(BuildData(CreatureConstants.Nightmare_Cauchemar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AstralProjection, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Nightmare_Cauchemar].Add(BuildData(CreatureConstants.Nightmare_Cauchemar, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Etherealness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

            testCases[CreatureConstants.Nightwalker].Add(BuildData(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.AversionToDaylight));
            testCases[CreatureConstants.Nightwalker].Add(BuildData(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.DesecratingAura));
            testCases[CreatureConstants.Nightwalker].Add(BuildData(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to silver, magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Nightwalker].Add(BuildData(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
            testCases[CreatureConstants.Nightwalker].Add(BuildData(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellResistance, power: 29));
            testCases[CreatureConstants.Nightwalker].Add(BuildData(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.Telepathy, power: 100));
            testCases[CreatureConstants.Nightwalker].Add(BuildData(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Contagion, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 14));
            testCases[CreatureConstants.Nightwalker].Add(BuildData(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeeperDarkness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Nightwalker].Add(BuildData(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Nightwalker].Add(BuildData(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Nightwalker].Add(BuildData(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Haste, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Nightwalker].Add(BuildData(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Nightwalker].Add(BuildData(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyBlight, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Nightwalker].Add(BuildData(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Confusion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Nightwalker].Add(BuildData(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldMonster, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
            testCases[CreatureConstants.Nightwalker].Add(BuildData(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Nightwalker].Add(BuildData(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ConeOfCold, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
            testCases[CreatureConstants.Nightwalker].Add(BuildData(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FingerOfDeath, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 17));
            testCases[CreatureConstants.Nightwalker].Add(BuildData(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlaneShift, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));

            testCases[CreatureConstants.Nightwing].Add(BuildData(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.AversionToDaylight));
            testCases[CreatureConstants.Nightwing].Add(BuildData(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.DesecratingAura));
            testCases[CreatureConstants.Nightwing].Add(BuildData(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to silver, magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Nightwing].Add(BuildData(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
            testCases[CreatureConstants.Nightwing].Add(BuildData(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellResistance, power: 27));
            testCases[CreatureConstants.Nightwing].Add(BuildData(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.Telepathy, power: 100));
            testCases[CreatureConstants.Nightwing].Add(BuildData(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Contagion, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 14));
            testCases[CreatureConstants.Nightwing].Add(BuildData(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeeperDarkness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Nightwing].Add(BuildData(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Nightwing].Add(BuildData(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Haste, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Nightwing].Add(BuildData(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Nightwing].Add(BuildData(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyBlight, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Nightwing].Add(BuildData(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Confusion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Nightwing].Add(BuildData(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic_Greater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Nightwing].Add(BuildData(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldMonster, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
            testCases[CreatureConstants.Nightwing].Add(BuildData(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Nightwing].Add(BuildData(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ConeOfCold, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
            testCases[CreatureConstants.Nightwing].Add(BuildData(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FingerOfDeath, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 17));
            testCases[CreatureConstants.Nightwing].Add(BuildData(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlaneShift, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));

            testCases[CreatureConstants.Nixie].Add(BuildData(CreatureConstants.Nixie, FeatConstants.SpecialQualities.Amphibious));
            testCases[CreatureConstants.Nixie].Add(BuildData(CreatureConstants.Nixie, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Nixie].Add(BuildData(CreatureConstants.Nixie, FeatConstants.SpecialQualities.SpellResistance, power: 16));
            testCases[CreatureConstants.Nixie].Add(BuildData(CreatureConstants.Nixie, FeatConstants.SpecialQualities.WildEmpathy));
            testCases[CreatureConstants.Nixie].Add(BuildData(CreatureConstants.Nixie, FeatConstants.Dodge, power: 1));
            testCases[CreatureConstants.Nixie].Add(BuildData(CreatureConstants.Nixie, FeatConstants.WeaponFinesse));
            testCases[CreatureConstants.Nixie].Add(BuildData(CreatureConstants.Nixie, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmPerson, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
            testCases[CreatureConstants.Nixie].Add(BuildData(CreatureConstants.Nixie, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WaterBreathing, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Nixie].Add(BuildData(CreatureConstants.Nixie, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.ShortSword, requiresEquipment: true));
            testCases[CreatureConstants.Nixie].Add(BuildData(CreatureConstants.Nixie, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.LightCrossbow, requiresEquipment: true));

            testCases[CreatureConstants.Nymph].Add(BuildData(CreatureConstants.Nymph, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Nymph].Add(BuildData(CreatureConstants.Nymph, FeatConstants.SpecialQualities.UnearthlyGrace));
            testCases[CreatureConstants.Nymph].Add(BuildData(CreatureConstants.Nymph, FeatConstants.SpecialQualities.WildEmpathy));
            testCases[CreatureConstants.Nymph].Add(BuildData(CreatureConstants.Nymph, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DimensionDoor, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Nymph].Add(BuildData(CreatureConstants.Nymph, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Dagger, requiresEquipment: true));

            testCases[CreatureConstants.OchreJelly].Add(BuildData(CreatureConstants.OchreJelly, FeatConstants.SpecialQualities.Split));

            testCases[CreatureConstants.Octopus].Add(BuildData(CreatureConstants.Octopus, FeatConstants.SpecialQualities.InkCloud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Minute));
            testCases[CreatureConstants.Octopus].Add(BuildData(CreatureConstants.Octopus, FeatConstants.SpecialQualities.Jet, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

            testCases[CreatureConstants.Octopus_Giant].Add(BuildData(CreatureConstants.Octopus_Giant, FeatConstants.SpecialQualities.InkCloud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Minute));
            testCases[CreatureConstants.Octopus_Giant].Add(BuildData(CreatureConstants.Octopus_Giant, FeatConstants.SpecialQualities.Jet, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

            testCases[CreatureConstants.Ogre].Add(BuildData(CreatureConstants.Ogre, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Ogre].Add(BuildData(CreatureConstants.Ogre, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Javelin, requiresEquipment: true));
            testCases[CreatureConstants.Ogre].Add(BuildData(CreatureConstants.Ogre, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatclub, requiresEquipment: true));
            testCases[CreatureConstants.Ogre].Add(BuildData(CreatureConstants.Ogre, FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
            testCases[CreatureConstants.Ogre].Add(BuildData(CreatureConstants.Ogre, FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));

            testCases[CreatureConstants.Ogre_Merrow].Add(BuildData(CreatureConstants.Ogre_Merrow, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Ogre_Merrow].Add(BuildData(CreatureConstants.Ogre_Merrow, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Javelin, requiresEquipment: true));
            testCases[CreatureConstants.Ogre_Merrow].Add(BuildData(CreatureConstants.Ogre_Merrow, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Longspear, requiresEquipment: true));
            testCases[CreatureConstants.Ogre_Merrow].Add(BuildData(CreatureConstants.Ogre_Merrow, FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
            testCases[CreatureConstants.Ogre_Merrow].Add(BuildData(CreatureConstants.Ogre_Merrow, FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));

            testCases[CreatureConstants.OgreMage].Add(BuildData(CreatureConstants.OgreMage, FeatConstants.SpecialQualities.ChangeShape, focus: "Any Small, Medium, or Large Humanoid or Giant", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.OgreMage].Add(BuildData(CreatureConstants.OgreMage, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.OgreMage].Add(BuildData(CreatureConstants.OgreMage, FeatConstants.SpecialQualities.Regeneration, focus: "Fire and acid deal normal damage", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.OgreMage].Add(BuildData(CreatureConstants.OgreMage, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatsword, requiresEquipment: true));
            testCases[CreatureConstants.OgreMage].Add(BuildData(CreatureConstants.OgreMage, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));
            testCases[CreatureConstants.OgreMage].Add(BuildData(CreatureConstants.OgreMage, FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
            testCases[CreatureConstants.OgreMage].Add(BuildData(CreatureConstants.OgreMage, FeatConstants.SpecialQualities.Flight));
            testCases[CreatureConstants.OgreMage].Add(BuildData(CreatureConstants.OgreMage, FeatConstants.SpecialQualities.SpellResistance, power: 19));
            testCases[CreatureConstants.OgreMage].Add(BuildData(CreatureConstants.OgreMage, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.OgreMage].Add(BuildData(CreatureConstants.OgreMage, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.OgreMage].Add(BuildData(CreatureConstants.OgreMage, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmPerson, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
            testCases[CreatureConstants.OgreMage].Add(BuildData(CreatureConstants.OgreMage, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ConeOfCold, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 15));
            testCases[CreatureConstants.OgreMage].Add(BuildData(CreatureConstants.OgreMage, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GaseousForm, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.OgreMage].Add(BuildData(CreatureConstants.OgreMage, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Sleep, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));

            testCases[CreatureConstants.Orc].Add(BuildData(CreatureConstants.Orc, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Orc].Add(BuildData(CreatureConstants.Orc, FeatConstants.SpecialQualities.LightSensitivity));
            testCases[CreatureConstants.Orc].Add(BuildData(CreatureConstants.Orc, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Falchion, requiresEquipment: true));
            testCases[CreatureConstants.Orc].Add(BuildData(CreatureConstants.Orc, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greataxe, requiresEquipment: true));
            testCases[CreatureConstants.Orc].Add(BuildData(CreatureConstants.Orc, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Javelin, requiresEquipment: true));
            testCases[CreatureConstants.Orc].Add(BuildData(CreatureConstants.Orc, FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

            testCases[CreatureConstants.Orc_Half].Add(BuildData(CreatureConstants.Orc_Half, FeatConstants.SpecialQualities.OrcBlood));

            testCases[CreatureConstants.Otyugh].Add(BuildData(CreatureConstants.Otyugh, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Owl].Add(BuildData(CreatureConstants.Owl, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Owl_Giant].Add(BuildData(CreatureConstants.Owl_Giant, FeatConstants.SpecialQualities.LowLightVision_Superior));

            testCases[CreatureConstants.Owlbear].Add(BuildData(CreatureConstants.Owlbear, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Pegasus].Add(BuildData(CreatureConstants.Pegasus, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Pegasus].Add(BuildData(CreatureConstants.Pegasus, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectGood + ": within 60-foot radius", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Pegasus].Add(BuildData(CreatureConstants.Pegasus, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectEvil + ": within 60-foot radius", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

            testCases[CreatureConstants.PhantomFungus].Add(BuildData(CreatureConstants.PhantomFungus, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility_Greater, frequencyTimePeriod: FeatConstants.Frequencies.Constant));

            testCases[CreatureConstants.PhaseSpider].Add(BuildData(CreatureConstants.PhaseSpider, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EtherealJaunt, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

            testCases[CreatureConstants.Phasm].Add(BuildData(CreatureConstants.Phasm, FeatConstants.SpecialQualities.AlternateForm, focus: "Any form Large size or smaller, including Humanoid", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Phasm].Add(BuildData(CreatureConstants.Phasm, FeatConstants.SpecialQualities.Amorphous));
            testCases[CreatureConstants.Phasm].Add(BuildData(CreatureConstants.Phasm, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Phasm].Add(BuildData(CreatureConstants.Phasm, FeatConstants.SpecialQualities.Telepathy, power: 100));
            testCases[CreatureConstants.Phasm].Add(BuildData(CreatureConstants.Phasm, FeatConstants.SpecialQualities.Tremorsense, power: 60));

            testCases[CreatureConstants.PitFiend].Add(BuildData(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good, silver weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.PitFiend].Add(BuildData(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
            testCases[CreatureConstants.PitFiend].Add(BuildData(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.PitFiend].Add(BuildData(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.PitFiend].Add(BuildData(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.PitFiend].Add(BuildData(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.Regeneration, focus: "Does not regenerate damage from good spells or effects, or from good-aligned silvered weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.PitFiend].Add(BuildData(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SeeInDarkness, focus: "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell"));
            testCases[CreatureConstants.PitFiend].Add(BuildData(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellResistance, power: 32));
            testCases[CreatureConstants.PitFiend].Add(BuildData(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.Telepathy, power: 100));
            testCases[CreatureConstants.PitFiend].Add(BuildData(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blasphemy, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));
            testCases[CreatureConstants.PitFiend].Add(BuildData(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateUndead, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.PitFiend].Add(BuildData(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fireball, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 13));
            testCases[CreatureConstants.PitFiend].Add(BuildData(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.PitFiend].Add(BuildData(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.PitFiend].Add(BuildData(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.PitFiend].Add(BuildData(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicCircleAgainstGood, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.PitFiend].Add(BuildData(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldMonster_Mass, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 19));
            testCases[CreatureConstants.PitFiend].Add(BuildData(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PersistentImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
            testCases[CreatureConstants.PitFiend].Add(BuildData(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PowerWordStun, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.PitFiend].Add(BuildData(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyAura, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 18));
            testCases[CreatureConstants.PitFiend].Add(BuildData(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MeteorSwarm, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 19));
            testCases[CreatureConstants.PitFiend].Add(BuildData(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Wish, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Year));

            testCases[CreatureConstants.Pixie].Add(BuildData(CreatureConstants.Pixie, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Pixie].Add(BuildData(CreatureConstants.Pixie, FeatConstants.SpecialQualities.SpellResistance, power: 15));
            testCases[CreatureConstants.Pixie].Add(BuildData(CreatureConstants.Pixie, FeatConstants.Dodge, power: 1));
            testCases[CreatureConstants.Pixie].Add(BuildData(CreatureConstants.Pixie, FeatConstants.WeaponFinesse));
            testCases[CreatureConstants.Pixie].Add(BuildData(CreatureConstants.Pixie, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Pixie].Add(BuildData(CreatureConstants.Pixie, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Confusion_Lesser, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
            testCases[CreatureConstants.Pixie].Add(BuildData(CreatureConstants.Pixie, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DancingLights, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Pixie].Add(BuildData(CreatureConstants.Pixie, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectChaos, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Pixie].Add(BuildData(CreatureConstants.Pixie, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectEvil, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Pixie].Add(BuildData(CreatureConstants.Pixie, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectGood, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Pixie].Add(BuildData(CreatureConstants.Pixie, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectLaw, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Pixie].Add(BuildData(CreatureConstants.Pixie, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.Pixie].Add(BuildData(CreatureConstants.Pixie, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Pixie].Add(BuildData(CreatureConstants.Pixie, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Entangle, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 11));
            testCases[CreatureConstants.Pixie].Add(BuildData(CreatureConstants.Pixie, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PermanentImage + ": visual and auditory elements only", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 16));
            testCases[CreatureConstants.Pixie].Add(BuildData(CreatureConstants.Pixie, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.ShortSword, requiresEquipment: true));
            testCases[CreatureConstants.Pixie].Add(BuildData(CreatureConstants.Pixie, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));

            testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(BuildData(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(BuildData(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.SpellResistance, power: 15));
            testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(BuildData(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.Dodge, power: 1));
            testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(BuildData(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.WeaponFinesse));
            testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(BuildData(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(BuildData(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Confusion_Lesser, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
            testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(BuildData(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DancingLights, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(BuildData(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectChaos, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(BuildData(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectEvil, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(BuildData(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectGood, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(BuildData(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectLaw, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(BuildData(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(BuildData(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(BuildData(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Entangle, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 11));
            testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(BuildData(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PermanentImage + ": visual and auditory elements only", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 16));
            testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(BuildData(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.IrresistibleDance, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(BuildData(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.ShortSword, requiresEquipment: true));
            testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(BuildData(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));

            testCases[CreatureConstants.Pony].Add(BuildData(CreatureConstants.Pony, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Pony_War].Add(BuildData(CreatureConstants.Pony_War, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Porpoise].Add(BuildData(CreatureConstants.Porpoise, FeatConstants.SpecialQualities.Blindsight, power: 120));
            testCases[CreatureConstants.Porpoise].Add(BuildData(CreatureConstants.Porpoise, FeatConstants.SpecialQualities.HoldBreath));

            testCases[CreatureConstants.PrayingMantis_Giant] = [];

            testCases[CreatureConstants.Pseudodragon].Add(BuildData(CreatureConstants.Pseudodragon, FeatConstants.SpecialQualities.Blindsense, power: 60));
            testCases[CreatureConstants.Pseudodragon].Add(BuildData(CreatureConstants.Pseudodragon, FeatConstants.SpecialQualities.SpellResistance, power: 19));
            testCases[CreatureConstants.Pseudodragon].Add(BuildData(CreatureConstants.Pseudodragon, FeatConstants.SpecialQualities.Telepathy, power: 60));
            testCases[CreatureConstants.Pseudodragon].Add(BuildData(CreatureConstants.Pseudodragon, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.PurpleWorm].Add(BuildData(CreatureConstants.PurpleWorm, FeatConstants.SpecialQualities.Tremorsense, power: 60));

            testCases[CreatureConstants.Pyrohydra_5Heads].Add(BuildData(CreatureConstants.Pyrohydra_5Heads, FeatConstants.SpecialQualities.FastHealing, power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Pyrohydra_5Heads].Add(BuildData(CreatureConstants.Pyrohydra_5Heads, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Pyrohydra_5Heads].Add(BuildData(CreatureConstants.Pyrohydra_5Heads, FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

            testCases[CreatureConstants.Pyrohydra_6Heads].Add(BuildData(CreatureConstants.Pyrohydra_6Heads, FeatConstants.SpecialQualities.FastHealing, power: 16, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Pyrohydra_6Heads].Add(BuildData(CreatureConstants.Pyrohydra_6Heads, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Pyrohydra_6Heads].Add(BuildData(CreatureConstants.Pyrohydra_6Heads, FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

            testCases[CreatureConstants.Pyrohydra_7Heads].Add(BuildData(CreatureConstants.Pyrohydra_7Heads, FeatConstants.SpecialQualities.FastHealing, power: 17, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Pyrohydra_7Heads].Add(BuildData(CreatureConstants.Pyrohydra_7Heads, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Pyrohydra_7Heads].Add(BuildData(CreatureConstants.Pyrohydra_7Heads, FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

            testCases[CreatureConstants.Pyrohydra_8Heads].Add(BuildData(CreatureConstants.Pyrohydra_8Heads, FeatConstants.SpecialQualities.FastHealing, power: 18, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Pyrohydra_8Heads].Add(BuildData(CreatureConstants.Pyrohydra_8Heads, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Pyrohydra_8Heads].Add(BuildData(CreatureConstants.Pyrohydra_8Heads, FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

            testCases[CreatureConstants.Pyrohydra_9Heads].Add(BuildData(CreatureConstants.Pyrohydra_9Heads, FeatConstants.SpecialQualities.FastHealing, power: 19, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Pyrohydra_9Heads].Add(BuildData(CreatureConstants.Pyrohydra_9Heads, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Pyrohydra_9Heads].Add(BuildData(CreatureConstants.Pyrohydra_9Heads, FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

            testCases[CreatureConstants.Pyrohydra_10Heads].Add(BuildData(CreatureConstants.Pyrohydra_10Heads, FeatConstants.SpecialQualities.FastHealing, power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Pyrohydra_10Heads].Add(BuildData(CreatureConstants.Pyrohydra_10Heads, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Pyrohydra_10Heads].Add(BuildData(CreatureConstants.Pyrohydra_10Heads, FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

            testCases[CreatureConstants.Pyrohydra_11Heads].Add(BuildData(CreatureConstants.Pyrohydra_11Heads, FeatConstants.SpecialQualities.FastHealing, power: 21, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Pyrohydra_11Heads].Add(BuildData(CreatureConstants.Pyrohydra_11Heads, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Pyrohydra_11Heads].Add(BuildData(CreatureConstants.Pyrohydra_11Heads, FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

            testCases[CreatureConstants.Pyrohydra_12Heads].Add(BuildData(CreatureConstants.Pyrohydra_12Heads, FeatConstants.SpecialQualities.FastHealing, power: 22, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Pyrohydra_12Heads].Add(BuildData(CreatureConstants.Pyrohydra_12Heads, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Pyrohydra_12Heads].Add(BuildData(CreatureConstants.Pyrohydra_12Heads, FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

            testCases[CreatureConstants.Quasit].Add(BuildData(CreatureConstants.Quasit, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.Quasit].Add(BuildData(CreatureConstants.Quasit, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Quasit].Add(BuildData(CreatureConstants.Quasit, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good or cold iron weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Quasit].Add(BuildData(CreatureConstants.Quasit, FeatConstants.SpecialQualities.AlternateForm, focus: "Bat, Small or Medium monstrous centipede, toad, and wolf", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Quasit].Add(BuildData(CreatureConstants.Quasit, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectGood, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Quasit].Add(BuildData(CreatureConstants.Quasit, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Quasit].Add(BuildData(CreatureConstants.Quasit, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + ": self only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Quasit].Add(BuildData(CreatureConstants.Quasit, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CauseFear + ": 30-foot radius area from the quasit", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
            testCases[CreatureConstants.Quasit].Add(BuildData(CreatureConstants.Quasit, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Commune + ": can ask 6 questions", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week));

            testCases[CreatureConstants.Rakshasa].Add(BuildData(CreatureConstants.Rakshasa, FeatConstants.SpecialQualities.ChangeShape, focus: "Any Humanoid form", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Rakshasa].Add(BuildData(CreatureConstants.Rakshasa, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good, piercing weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Rakshasa].Add(BuildData(CreatureConstants.Rakshasa, FeatConstants.SpecialQualities.SpellResistance, power: 27));
            testCases[CreatureConstants.Rakshasa].Add(BuildData(CreatureConstants.Rakshasa, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyTimePeriod: FeatConstants.Frequencies.Constant, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));

            testCases[CreatureConstants.Rast].Add(BuildData(CreatureConstants.Rast, FeatConstants.SpecialQualities.Flight));

            testCases[CreatureConstants.Rat].Add(BuildData(CreatureConstants.Rat, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Rat].Add(BuildData(CreatureConstants.Rat, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Rat_Dire].Add(BuildData(CreatureConstants.Rat_Dire, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Rat_Dire].Add(BuildData(CreatureConstants.Rat_Dire, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Rat_Swarm].Add(BuildData(CreatureConstants.Rat_Swarm, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Rat_Swarm].Add(BuildData(CreatureConstants.Rat_Swarm, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Raven].Add(BuildData(CreatureConstants.Raven, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Ravid].Add(BuildData(CreatureConstants.Ravid, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
            testCases[CreatureConstants.Ravid].Add(BuildData(CreatureConstants.Ravid, FeatConstants.SpecialQualities.Flight));
            testCases[CreatureConstants.Ravid].Add(BuildData(CreatureConstants.Ravid, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AnimateObjects, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

            testCases[CreatureConstants.RazorBoar].Add(BuildData(CreatureConstants.RazorBoar, FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.RazorBoar].Add(BuildData(CreatureConstants.RazorBoar, FeatConstants.SpecialQualities.FastHealing, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.RazorBoar].Add(BuildData(CreatureConstants.RazorBoar, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.RazorBoar].Add(BuildData(CreatureConstants.RazorBoar, FeatConstants.SpecialQualities.SpellResistance, power: 21));

            testCases[CreatureConstants.Remorhaz].Add(BuildData(CreatureConstants.Remorhaz, FeatConstants.SpecialQualities.Heat));
            testCases[CreatureConstants.Remorhaz].Add(BuildData(CreatureConstants.Remorhaz, FeatConstants.SpecialQualities.Tremorsense, power: 60));

            testCases[CreatureConstants.Retriever].Add(BuildData(CreatureConstants.Retriever, FeatConstants.SpecialQualities.FastHealing, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

            testCases[CreatureConstants.Rhinoceras] = [];

            testCases[CreatureConstants.Roc] = [];

            testCases[CreatureConstants.Roper].Add(BuildData(CreatureConstants.Roper, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Roper].Add(BuildData(CreatureConstants.Roper, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Roper].Add(BuildData(CreatureConstants.Roper, FeatConstants.SpecialQualities.SpellResistance, power: 30));
            testCases[CreatureConstants.Roper].Add(BuildData(CreatureConstants.Roper, FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));

            testCases[CreatureConstants.RustMonster].Add(BuildData(CreatureConstants.RustMonster, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Sahuagin].Add(BuildData(CreatureConstants.Sahuagin, FeatConstants.SpecialQualities.Blindsense, power: 30));
            testCases[CreatureConstants.Sahuagin].Add(BuildData(CreatureConstants.Sahuagin, FeatConstants.SpecialQualities.FreshwaterSensitivity));
            testCases[CreatureConstants.Sahuagin].Add(BuildData(CreatureConstants.Sahuagin, FeatConstants.SpecialQualities.LightBlindness));
            testCases[CreatureConstants.Sahuagin].Add(BuildData(CreatureConstants.Sahuagin, FeatConstants.SpecialQualities.SpeakWithSharks));
            testCases[CreatureConstants.Sahuagin].Add(BuildData(CreatureConstants.Sahuagin, FeatConstants.SpecialQualities.WaterDependent));
            testCases[CreatureConstants.Sahuagin].Add(BuildData(CreatureConstants.Sahuagin, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.HeavyCrossbow, requiresEquipment: true));
            testCases[CreatureConstants.Sahuagin].Add(BuildData(CreatureConstants.Sahuagin, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Trident, requiresEquipment: true));
            testCases[CreatureConstants.Sahuagin].Add(BuildData(CreatureConstants.Sahuagin, FeatConstants.Monster.Multiattack));

            testCases[CreatureConstants.Sahuagin_Mutant].Add(BuildData(CreatureConstants.Sahuagin_Mutant, FeatConstants.SpecialQualities.Blindsense, power: 30));
            testCases[CreatureConstants.Sahuagin_Mutant].Add(BuildData(CreatureConstants.Sahuagin_Mutant, FeatConstants.SpecialQualities.FreshwaterSensitivity));
            testCases[CreatureConstants.Sahuagin_Mutant].Add(BuildData(CreatureConstants.Sahuagin_Mutant, FeatConstants.SpecialQualities.LightBlindness));
            testCases[CreatureConstants.Sahuagin_Mutant].Add(BuildData(CreatureConstants.Sahuagin_Mutant, FeatConstants.SpecialQualities.SpeakWithSharks));
            testCases[CreatureConstants.Sahuagin_Mutant].Add(BuildData(CreatureConstants.Sahuagin_Mutant, FeatConstants.SpecialQualities.WaterDependent));
            testCases[CreatureConstants.Sahuagin_Mutant].Add(BuildData(CreatureConstants.Sahuagin_Mutant, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.HeavyCrossbow, requiresEquipment: true));
            testCases[CreatureConstants.Sahuagin_Mutant].Add(BuildData(CreatureConstants.Sahuagin_Mutant, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Trident, requiresEquipment: true));
            testCases[CreatureConstants.Sahuagin_Mutant].Add(BuildData(CreatureConstants.Sahuagin_Mutant, FeatConstants.Monster.Multiattack));

            testCases[CreatureConstants.Sahuagin_Malenti].Add(BuildData(CreatureConstants.Sahuagin_Malenti, FeatConstants.SpecialQualities.Blindsense, power: 30));
            testCases[CreatureConstants.Sahuagin_Malenti].Add(BuildData(CreatureConstants.Sahuagin_Malenti, FeatConstants.SpecialQualities.FreshwaterSensitivity));
            testCases[CreatureConstants.Sahuagin_Malenti].Add(BuildData(CreatureConstants.Sahuagin_Malenti, FeatConstants.SpecialQualities.LightSensitivity));
            testCases[CreatureConstants.Sahuagin_Malenti].Add(BuildData(CreatureConstants.Sahuagin_Malenti, FeatConstants.SpecialQualities.SpeakWithSharks));
            testCases[CreatureConstants.Sahuagin_Malenti].Add(BuildData(CreatureConstants.Sahuagin_Malenti, FeatConstants.SpecialQualities.WaterDependent));
            testCases[CreatureConstants.Sahuagin_Malenti].Add(BuildData(CreatureConstants.Sahuagin_Malenti, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.HeavyCrossbow, requiresEquipment: true));
            testCases[CreatureConstants.Sahuagin_Malenti].Add(BuildData(CreatureConstants.Sahuagin_Malenti, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Trident, requiresEquipment: true));
            testCases[CreatureConstants.Sahuagin_Malenti].Add(BuildData(CreatureConstants.Sahuagin_Malenti, FeatConstants.Monster.Multiattack));

            testCases[CreatureConstants.Salamander_Flamebrother].Add(BuildData(CreatureConstants.Salamander_Flamebrother, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Spear, requiresEquipment: true));
            testCases[CreatureConstants.Salamander_Flamebrother].Add(BuildData(CreatureConstants.Salamander_Flamebrother, FeatConstants.Monster.Multiattack));

            testCases[CreatureConstants.Salamander_Average].Add(BuildData(CreatureConstants.Salamander_Average, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Spear, requiresEquipment: true));
            testCases[CreatureConstants.Salamander_Average].Add(BuildData(CreatureConstants.Salamander_Average, FeatConstants.Monster.Multiattack));
            testCases[CreatureConstants.Salamander_Average].Add(BuildData(CreatureConstants.Salamander_Average, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

            testCases[CreatureConstants.Salamander_Noble].Add(BuildData(CreatureConstants.Salamander_Noble, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Longspear, requiresEquipment: true));
            testCases[CreatureConstants.Salamander_Noble].Add(BuildData(CreatureConstants.Salamander_Noble, FeatConstants.Monster.Multiattack));
            testCases[CreatureConstants.Salamander_Noble].Add(BuildData(CreatureConstants.Salamander_Noble, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Salamander_Noble].Add(BuildData(CreatureConstants.Salamander_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.BurningHands, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 11));
            testCases[CreatureConstants.Salamander_Noble].Add(BuildData(CreatureConstants.Salamander_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fireball, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 13));
            testCases[CreatureConstants.Salamander_Noble].Add(BuildData(CreatureConstants.Salamander_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FlamingSphere, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 12));
            testCases[CreatureConstants.Salamander_Noble].Add(BuildData(CreatureConstants.Salamander_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfFire, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 14));
            testCases[CreatureConstants.Salamander_Noble].Add(BuildData(CreatureConstants.Salamander_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Salamander_Noble].Add(BuildData(CreatureConstants.Salamander_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SummonMonsterVII + ": Huge fire elemental", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Satyr].Add(BuildData(CreatureConstants.Satyr, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Satyr].Add(BuildData(CreatureConstants.Satyr, FeatConstants.Alertness, power: 2));
            testCases[CreatureConstants.Satyr].Add(BuildData(CreatureConstants.Satyr, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));
            testCases[CreatureConstants.Satyr].Add(BuildData(CreatureConstants.Satyr, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Dagger, requiresEquipment: true));

            testCases[CreatureConstants.Satyr_WithPipes].Add(BuildData(CreatureConstants.Satyr_WithPipes, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Satyr_WithPipes].Add(BuildData(CreatureConstants.Satyr_WithPipes, FeatConstants.Alertness, power: 2));
            testCases[CreatureConstants.Satyr_WithPipes].Add(BuildData(CreatureConstants.Satyr_WithPipes, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));
            testCases[CreatureConstants.Satyr_WithPipes].Add(BuildData(CreatureConstants.Satyr_WithPipes, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Dagger, requiresEquipment: true));

            testCases[CreatureConstants.SeaCat].Add(BuildData(CreatureConstants.SeaCat, FeatConstants.SpecialQualities.HoldBreath));
            testCases[CreatureConstants.SeaCat].Add(BuildData(CreatureConstants.SeaCat, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.SeaHag].Add(BuildData(CreatureConstants.SeaHag, FeatConstants.SpecialQualities.Amphibious));
            testCases[CreatureConstants.SeaHag].Add(BuildData(CreatureConstants.SeaHag, FeatConstants.SpecialQualities.SpellResistance, power: 14));

            testCases[CreatureConstants.Scorpion_Monstrous_Tiny].Add(BuildData(CreatureConstants.Scorpion_Monstrous_Tiny, FeatConstants.SpecialQualities.Tremorsense, power: 60));
            testCases[CreatureConstants.Scorpion_Monstrous_Tiny].Add(BuildData(CreatureConstants.Scorpion_Monstrous_Tiny, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Scorpion_Monstrous_Small].Add(BuildData(CreatureConstants.Scorpion_Monstrous_Small, FeatConstants.SpecialQualities.Tremorsense, power: 60));
            testCases[CreatureConstants.Scorpion_Monstrous_Small].Add(BuildData(CreatureConstants.Scorpion_Monstrous_Small, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Scorpion_Monstrous_Medium].Add(BuildData(CreatureConstants.Scorpion_Monstrous_Medium, FeatConstants.SpecialQualities.Tremorsense, power: 60));

            testCases[CreatureConstants.Scorpion_Monstrous_Large].Add(BuildData(CreatureConstants.Scorpion_Monstrous_Large, FeatConstants.SpecialQualities.Tremorsense, power: 60));

            testCases[CreatureConstants.Scorpion_Monstrous_Huge].Add(BuildData(CreatureConstants.Scorpion_Monstrous_Huge, FeatConstants.SpecialQualities.Tremorsense, power: 60));

            testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan].Add(BuildData(CreatureConstants.Scorpion_Monstrous_Gargantuan, FeatConstants.SpecialQualities.Tremorsense, power: 60));

            testCases[CreatureConstants.Scorpion_Monstrous_Colossal].Add(BuildData(CreatureConstants.Scorpion_Monstrous_Colossal, FeatConstants.SpecialQualities.Tremorsense, power: 60));

            testCases[CreatureConstants.Scorpionfolk].Add(BuildData(CreatureConstants.Scorpionfolk, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Scorpionfolk].Add(BuildData(CreatureConstants.Scorpionfolk, FeatConstants.SpecialQualities.SpellResistance, power: 18));
            testCases[CreatureConstants.Scorpionfolk].Add(BuildData(CreatureConstants.Scorpionfolk, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MajorImage, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Scorpionfolk].Add(BuildData(CreatureConstants.Scorpionfolk, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MirrorImage, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Scorpionfolk].Add(BuildData(CreatureConstants.Scorpionfolk, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Lance, requiresEquipment: true));
            testCases[CreatureConstants.Scorpionfolk].Add(BuildData(CreatureConstants.Scorpionfolk, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Falchion, requiresEquipment: true));
            testCases[CreatureConstants.Scorpionfolk].Add(BuildData(CreatureConstants.Scorpionfolk, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));
            testCases[CreatureConstants.Scorpionfolk].Add(BuildData(CreatureConstants.Scorpionfolk, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeShortbow, requiresEquipment: true));
            testCases[CreatureConstants.Scorpionfolk].Add(BuildData(CreatureConstants.Scorpionfolk, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Scimitar, requiresEquipment: true));
            testCases[CreatureConstants.Scorpionfolk].Add(BuildData(CreatureConstants.Scorpionfolk, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.LightCrossbow, requiresEquipment: true));
            testCases[CreatureConstants.Scorpionfolk].Add(BuildData(CreatureConstants.Scorpionfolk, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.HeavyCrossbow, requiresEquipment: true));
            testCases[CreatureConstants.Scorpionfolk].Add(BuildData(CreatureConstants.Scorpionfolk, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Spear, requiresEquipment: true));
            testCases[CreatureConstants.Scorpionfolk].Add(BuildData(CreatureConstants.Scorpionfolk, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Longspear, requiresEquipment: true));
            testCases[CreatureConstants.Scorpionfolk].Add(BuildData(CreatureConstants.Scorpionfolk, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Shortspear, requiresEquipment: true));

            testCases[CreatureConstants.Shadow].Add(BuildData(CreatureConstants.Shadow, FeatConstants.SpecialQualities.TurnResistance, power: 2));

            testCases[CreatureConstants.Shadow_Greater].Add(BuildData(CreatureConstants.Shadow_Greater, FeatConstants.SpecialQualities.TurnResistance, power: 2));

            testCases[CreatureConstants.ShadowMastiff].Add(BuildData(CreatureConstants.ShadowMastiff, FeatConstants.SpecialQualities.ShadowBlend));
            testCases[CreatureConstants.ShadowMastiff].Add(BuildData(CreatureConstants.ShadowMastiff, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.ShadowMastiff].Add(BuildData(CreatureConstants.ShadowMastiff, FeatConstants.Track));

            testCases[CreatureConstants.ShamblingMound].Add(BuildData(CreatureConstants.ShamblingMound, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.ShamblingMound].Add(BuildData(CreatureConstants.ShamblingMound, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.ShamblingMound].Add(BuildData(CreatureConstants.ShamblingMound, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

            testCases[CreatureConstants.Shark_Dire].Add(BuildData(CreatureConstants.Shark_Dire, FeatConstants.SpecialQualities.KeenScent, power: 180));

            testCases[CreatureConstants.Shark_Huge].Add(BuildData(CreatureConstants.Shark_Huge, FeatConstants.SpecialQualities.KeenScent, power: 180));
            testCases[CreatureConstants.Shark_Huge].Add(BuildData(CreatureConstants.Shark_Huge, FeatConstants.SpecialQualities.Blindsense, power: 30));

            testCases[CreatureConstants.Shark_Large].Add(BuildData(CreatureConstants.Shark_Large, FeatConstants.SpecialQualities.KeenScent, power: 180));
            testCases[CreatureConstants.Shark_Large].Add(BuildData(CreatureConstants.Shark_Large, FeatConstants.SpecialQualities.Blindsense, power: 30));

            testCases[CreatureConstants.Shark_Medium].Add(BuildData(CreatureConstants.Shark_Medium, FeatConstants.SpecialQualities.KeenScent, power: 180));
            testCases[CreatureConstants.Shark_Medium].Add(BuildData(CreatureConstants.Shark_Medium, FeatConstants.SpecialQualities.Blindsense, power: 30));

            testCases[CreatureConstants.ShieldGuardian].Add(BuildData(CreatureConstants.ShieldGuardian, FeatConstants.SpecialQualities.FastHealing, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.ShieldGuardian].Add(BuildData(CreatureConstants.ShieldGuardian, FeatConstants.SpecialQualities.FindMaster));
            testCases[CreatureConstants.ShieldGuardian].Add(BuildData(CreatureConstants.ShieldGuardian, FeatConstants.SpecialQualities.Guard));
            testCases[CreatureConstants.ShieldGuardian].Add(BuildData(CreatureConstants.ShieldGuardian, FeatConstants.SpecialQualities.SpellStoring));
            testCases[CreatureConstants.ShieldGuardian].Add(BuildData(CreatureConstants.ShieldGuardian, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ShieldOther + ": within 100 feet of the amulet.  Does not provide spell's AC or save bonuses", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

            testCases[CreatureConstants.ShockerLizard].Add(BuildData(CreatureConstants.ShockerLizard, FeatConstants.SpecialQualities.ElectricitySense));
            testCases[CreatureConstants.ShockerLizard].Add(BuildData(CreatureConstants.ShockerLizard, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));

            testCases[CreatureConstants.Shrieker] = [];

            testCases[CreatureConstants.Skum].Add(BuildData(CreatureConstants.Skum, FeatConstants.SpecialQualities.Amphibious));

            testCases[CreatureConstants.Slaad_Red].Add(BuildData(CreatureConstants.Slaad_Red, FeatConstants.SpecialQualities.FastHealing, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Slaad_Red].Add(BuildData(CreatureConstants.Slaad_Red, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Sonic));
            testCases[CreatureConstants.Slaad_Red].Add(BuildData(CreatureConstants.Slaad_Red, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Slaad_Red].Add(BuildData(CreatureConstants.Slaad_Red, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Slaad_Red].Add(BuildData(CreatureConstants.Slaad_Red, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Slaad_Red].Add(BuildData(CreatureConstants.Slaad_Red, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

            testCases[CreatureConstants.Slaad_Blue].Add(BuildData(CreatureConstants.Slaad_Blue, FeatConstants.SpecialQualities.FastHealing, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Slaad_Blue].Add(BuildData(CreatureConstants.Slaad_Blue, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Sonic));
            testCases[CreatureConstants.Slaad_Blue].Add(BuildData(CreatureConstants.Slaad_Blue, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Slaad_Blue].Add(BuildData(CreatureConstants.Slaad_Blue, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Slaad_Blue].Add(BuildData(CreatureConstants.Slaad_Blue, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Slaad_Blue].Add(BuildData(CreatureConstants.Slaad_Blue, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Slaad_Blue].Add(BuildData(CreatureConstants.Slaad_Blue, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldPerson, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Slaad_Blue].Add(BuildData(CreatureConstants.Slaad_Blue, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Passwall, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Slaad_Blue].Add(BuildData(CreatureConstants.Slaad_Blue, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Telekinesis, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
            testCases[CreatureConstants.Slaad_Blue].Add(BuildData(CreatureConstants.Slaad_Blue, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ChaosHammer, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));

            testCases[CreatureConstants.Slaad_Green].Add(BuildData(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.ChangeShape, focus: "Medium or Large humanoid form", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Slaad_Green].Add(BuildData(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.FastHealing, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Slaad_Green].Add(BuildData(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Sonic));
            testCases[CreatureConstants.Slaad_Green].Add(BuildData(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Slaad_Green].Add(BuildData(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Slaad_Green].Add(BuildData(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Slaad_Green].Add(BuildData(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Slaad_Green].Add(BuildData(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ChaosHammer, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Slaad_Green].Add(BuildData(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Slaad_Green].Add(BuildData(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.Slaad_Green].Add(BuildData(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fear, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Slaad_Green].Add(BuildData(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ProtectionFromLaw, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Slaad_Green].Add(BuildData(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Slaad_Green].Add(BuildData(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Shatter, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.Slaad_Green].Add(BuildData(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelLaw, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
            testCases[CreatureConstants.Slaad_Green].Add(BuildData(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeeperDarkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Slaad_Green].Add(BuildData(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fireball, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 13));

            testCases[CreatureConstants.Slaad_Gray].Add(BuildData(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.ChangeShape, focus: "Any humanoid form", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Slaad_Gray].Add(BuildData(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.DamageReduction, power: 10, focus: "Vulnerable to lawful weapons", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Slaad_Gray].Add(BuildData(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.FastHealing, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Slaad_Gray].Add(BuildData(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Sonic));
            testCases[CreatureConstants.Slaad_Gray].Add(BuildData(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Slaad_Gray].Add(BuildData(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Slaad_Gray].Add(BuildData(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Slaad_Gray].Add(BuildData(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Slaad_Gray].Add(BuildData(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ChaosHammer, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Slaad_Gray].Add(BuildData(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeeperDarkness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Slaad_Gray].Add(BuildData(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Slaad_Gray].Add(BuildData(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Identify, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Slaad_Gray].Add(BuildData(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Slaad_Gray].Add(BuildData(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LightningBolt, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 13));
            testCases[CreatureConstants.Slaad_Gray].Add(BuildData(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicCircleAgainstLaw, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Slaad_Gray].Add(BuildData(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Slaad_Gray].Add(BuildData(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Shatter, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.Slaad_Gray].Add(BuildData(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AnimateObjects, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Slaad_Gray].Add(BuildData(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelLaw, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
            testCases[CreatureConstants.Slaad_Gray].Add(BuildData(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fly, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Slaad_Gray].Add(BuildData(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PowerWordStun, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Slaad_Death].Add(BuildData(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.ChangeShape, focus: "Any humanoid form", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Slaad_Death].Add(BuildData(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.DamageReduction, power: 10, focus: "Vulnerable to lawful weapons", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Slaad_Death].Add(BuildData(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.FastHealing, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Slaad_Death].Add(BuildData(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Sonic));
            testCases[CreatureConstants.Slaad_Death].Add(BuildData(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Slaad_Death].Add(BuildData(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Slaad_Death].Add(BuildData(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Slaad_Death].Add(BuildData(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Slaad_Death].Add(BuildData(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.Telepathy, power: 100));
            testCases[CreatureConstants.Slaad_Death].Add(BuildData(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AnimateObjects, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Slaad_Death].Add(BuildData(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ChaosHammer, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Slaad_Death].Add(BuildData(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeeperDarkness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Slaad_Death].Add(BuildData(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Slaad_Death].Add(BuildData(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelLaw, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
            testCases[CreatureConstants.Slaad_Death].Add(BuildData(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fear, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Slaad_Death].Add(BuildData(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FingerOfDeath, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 17));
            testCases[CreatureConstants.Slaad_Death].Add(BuildData(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fireball, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 13));
            testCases[CreatureConstants.Slaad_Death].Add(BuildData(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fly, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Slaad_Death].Add(BuildData(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Identify, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Slaad_Death].Add(BuildData(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Slaad_Death].Add(BuildData(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicCircleAgainstLaw, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Slaad_Death].Add(BuildData(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Slaad_Death].Add(BuildData(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Shatter, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.Slaad_Death].Add(BuildData(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CircleOfDeath, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 16));
            testCases[CreatureConstants.Slaad_Death].Add(BuildData(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CloakOfChaos, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 18));
            testCases[CreatureConstants.Slaad_Death].Add(BuildData(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WordOfChaos, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 17));
            testCases[CreatureConstants.Slaad_Death].Add(BuildData(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Implosion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveBaseValue: 19));
            testCases[CreatureConstants.Slaad_Death].Add(BuildData(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PowerWordBlind, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Snake_Constrictor].Add(BuildData(CreatureConstants.Snake_Constrictor, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Snake_Constrictor_Giant].Add(BuildData(CreatureConstants.Snake_Constrictor_Giant, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Snake_Viper_Tiny].Add(BuildData(CreatureConstants.Snake_Viper_Tiny, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Snake_Viper_Tiny].Add(BuildData(CreatureConstants.Snake_Viper_Tiny, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Snake_Viper_Small].Add(BuildData(CreatureConstants.Snake_Viper_Small, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Snake_Viper_Small].Add(BuildData(CreatureConstants.Snake_Viper_Small, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Snake_Viper_Medium].Add(BuildData(CreatureConstants.Snake_Viper_Medium, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Snake_Viper_Large].Add(BuildData(CreatureConstants.Snake_Viper_Large, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Snake_Viper_Huge].Add(BuildData(CreatureConstants.Snake_Viper_Huge, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Spectre].Add(BuildData(CreatureConstants.Spectre, FeatConstants.SpecialQualities.TurnResistance, power: 2));
            testCases[CreatureConstants.Spectre].Add(BuildData(CreatureConstants.Spectre, FeatConstants.SpecialQualities.SunlightPowerlessness));
            testCases[CreatureConstants.Spectre].Add(BuildData(CreatureConstants.Spectre, FeatConstants.SpecialQualities.UnnaturalAura));

            testCases[CreatureConstants.Spider_Monstrous_Hunter_Tiny].Add(BuildData(CreatureConstants.Spider_Monstrous_Hunter_Tiny, FeatConstants.SpecialQualities.Tremorsense, power: 60));
            testCases[CreatureConstants.Spider_Monstrous_Hunter_Tiny].Add(BuildData(CreatureConstants.Spider_Monstrous_Hunter_Tiny, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Spider_Monstrous_Hunter_Small].Add(BuildData(CreatureConstants.Spider_Monstrous_Hunter_Small, FeatConstants.SpecialQualities.Tremorsense, power: 60));
            testCases[CreatureConstants.Spider_Monstrous_Hunter_Small].Add(BuildData(CreatureConstants.Spider_Monstrous_Hunter_Small, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium].Add(BuildData(CreatureConstants.Spider_Monstrous_Hunter_Medium, FeatConstants.SpecialQualities.Tremorsense, power: 60));
            testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium].Add(BuildData(CreatureConstants.Spider_Monstrous_Hunter_Medium, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Spider_Monstrous_Hunter_Large].Add(BuildData(CreatureConstants.Spider_Monstrous_Hunter_Large, FeatConstants.SpecialQualities.Tremorsense, power: 60));

            testCases[CreatureConstants.Spider_Monstrous_Hunter_Huge].Add(BuildData(CreatureConstants.Spider_Monstrous_Hunter_Huge, FeatConstants.SpecialQualities.Tremorsense, power: 60));

            testCases[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan].Add(BuildData(CreatureConstants.Spider_Monstrous_Hunter_Gargantuan, FeatConstants.SpecialQualities.Tremorsense, power: 60));

            testCases[CreatureConstants.Spider_Monstrous_Hunter_Colossal].Add(BuildData(CreatureConstants.Spider_Monstrous_Hunter_Colossal, FeatConstants.SpecialQualities.Tremorsense, power: 60));

            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny].Add(BuildData(CreatureConstants.Spider_Monstrous_WebSpinner_Tiny, FeatConstants.SpecialQualities.Tremorsense, power: 60));
            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny].Add(BuildData(CreatureConstants.Spider_Monstrous_WebSpinner_Tiny, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small].Add(BuildData(CreatureConstants.Spider_Monstrous_WebSpinner_Small, FeatConstants.SpecialQualities.Tremorsense, power: 60));
            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small].Add(BuildData(CreatureConstants.Spider_Monstrous_WebSpinner_Small, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium].Add(BuildData(CreatureConstants.Spider_Monstrous_WebSpinner_Medium, FeatConstants.SpecialQualities.Tremorsense, power: 60));
            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium].Add(BuildData(CreatureConstants.Spider_Monstrous_WebSpinner_Medium, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large].Add(BuildData(CreatureConstants.Spider_Monstrous_WebSpinner_Large, FeatConstants.SpecialQualities.Tremorsense, power: 60));

            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge].Add(BuildData(CreatureConstants.Spider_Monstrous_WebSpinner_Huge, FeatConstants.SpecialQualities.Tremorsense, power: 60));

            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan].Add(BuildData(CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan, FeatConstants.SpecialQualities.Tremorsense, power: 60));

            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal].Add(BuildData(CreatureConstants.Spider_Monstrous_WebSpinner_Colossal, FeatConstants.SpecialQualities.Tremorsense, power: 60));

            testCases[CreatureConstants.SpiderEater].Add(BuildData(CreatureConstants.SpiderEater, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FreedomOfMovement, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
            testCases[CreatureConstants.SpiderEater].Add(BuildData(CreatureConstants.SpiderEater, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Spider_Swarm].Add(BuildData(CreatureConstants.Spider_Swarm, FeatConstants.SpecialQualities.Tremorsense, power: 30));
            testCases[CreatureConstants.Spider_Swarm].Add(BuildData(CreatureConstants.Spider_Swarm, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Squid].Add(BuildData(CreatureConstants.Squid, FeatConstants.SpecialQualities.InkCloud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Minute));
            testCases[CreatureConstants.Squid].Add(BuildData(CreatureConstants.Squid, FeatConstants.SpecialQualities.Jet, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

            testCases[CreatureConstants.Squid_Giant].Add(BuildData(CreatureConstants.Squid_Giant, FeatConstants.SpecialQualities.InkCloud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Minute));
            testCases[CreatureConstants.Squid_Giant].Add(BuildData(CreatureConstants.Squid_Giant, FeatConstants.SpecialQualities.Jet, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

            testCases[CreatureConstants.StagBeetle_Giant] = [];

            testCases[CreatureConstants.Stirge].Add(BuildData(CreatureConstants.Stirge, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Succubus].Add(BuildData(CreatureConstants.Succubus, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Succubus].Add(BuildData(CreatureConstants.Succubus, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.Succubus].Add(BuildData(CreatureConstants.Succubus, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Succubus].Add(BuildData(CreatureConstants.Succubus, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Succubus].Add(BuildData(CreatureConstants.Succubus, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Succubus].Add(BuildData(CreatureConstants.Succubus, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good or cold iron weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Succubus].Add(BuildData(CreatureConstants.Succubus, FeatConstants.SpecialQualities.SpellResistance, power: 18));
            testCases[CreatureConstants.Succubus].Add(BuildData(CreatureConstants.Succubus, FeatConstants.SpecialQualities.Telepathy, power: 100));
            testCases[CreatureConstants.Succubus].Add(BuildData(CreatureConstants.Succubus, FeatConstants.SpecialQualities.ChangeShape, focus: "Any Small or Medium Humanoid", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Succubus].Add(BuildData(CreatureConstants.Succubus, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Tongues, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
            testCases[CreatureConstants.Succubus].Add(BuildData(CreatureConstants.Succubus, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Succubus].Add(BuildData(CreatureConstants.Succubus, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectGood, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Succubus].Add(BuildData(CreatureConstants.Succubus, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.Succubus].Add(BuildData(CreatureConstants.Succubus, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EtherealJaunt + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Succubus].Add(BuildData(CreatureConstants.Succubus, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Succubus].Add(BuildData(CreatureConstants.Succubus, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

            testCases[CreatureConstants.Tarrasque].Add(BuildData(CreatureConstants.Tarrasque, FeatConstants.SpecialQualities.Carapace));
            testCases[CreatureConstants.Tarrasque].Add(BuildData(CreatureConstants.Tarrasque, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to epic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Tarrasque].Add(BuildData(CreatureConstants.Tarrasque, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
            testCases[CreatureConstants.Tarrasque].Add(BuildData(CreatureConstants.Tarrasque, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.Tarrasque].Add(BuildData(CreatureConstants.Tarrasque, FeatConstants.SpecialQualities.Immunity, focus: "Disease"));
            testCases[CreatureConstants.Tarrasque].Add(BuildData(CreatureConstants.Tarrasque, FeatConstants.SpecialQualities.Immunity, focus: "Energy Drain"));
            testCases[CreatureConstants.Tarrasque].Add(BuildData(CreatureConstants.Tarrasque, FeatConstants.SpecialQualities.Immunity, focus: "Ability Damage"));
            testCases[CreatureConstants.Tarrasque].Add(BuildData(CreatureConstants.Tarrasque, FeatConstants.SpecialQualities.Regeneration, focus: "No form of attack deals lethal damage to the tarrasque", power: 40, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Tarrasque].Add(BuildData(CreatureConstants.Tarrasque, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Tarrasque].Add(BuildData(CreatureConstants.Tarrasque, FeatConstants.SpecialQualities.SpellResistance, power: 32));

            testCases[CreatureConstants.Tendriculos].Add(BuildData(CreatureConstants.Tendriculos, FeatConstants.SpecialQualities.Regeneration, focus: "Bludgeoning weapons and acid deal normal damage", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

            testCases[CreatureConstants.Thoqqua].Add(BuildData(CreatureConstants.Thoqqua, FeatConstants.SpecialQualities.Tremorsense, power: 60));
            testCases[CreatureConstants.Thoqqua].Add(BuildData(CreatureConstants.Thoqqua, FeatConstants.SpecialQualities.Heat));

            testCases[CreatureConstants.Tiefling].Add(BuildData(CreatureConstants.Tiefling, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Tiefling].Add(BuildData(CreatureConstants.Tiefling, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Tiefling].Add(BuildData(CreatureConstants.Tiefling, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Tiefling].Add(BuildData(CreatureConstants.Tiefling, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Tiefling].Add(BuildData(CreatureConstants.Tiefling, FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

            testCases[CreatureConstants.Tiger].Add(BuildData(CreatureConstants.Tiger, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Tiger_Dire].Add(BuildData(CreatureConstants.Tiger_Dire, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.SpecialQualities.OversizedWeapon, focus: SizeConstants.Gargantuan, requiresEquipment: true));
            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.SpecialQualities.OversizedWeapon, focus: SizeConstants.Colossal, requiresEquipment: true));
            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Warhammer, requiresEquipment: true));
            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Javelin, requiresEquipment: true));
            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.ArmorProficiency_Heavy, requiresEquipment: true));
            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to lawful weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellResistance, power: 32));
            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ChainLightning, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 16));
            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CureCriticalWounds, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FireStorm, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 17));
            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.InvisibilityPurge, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Levitate, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PersistentImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Etherealness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WordOfChaos, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SummonNaturesAllyIX, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Gate, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Maze, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MeteorSwarm, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 19));
            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Daylight, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HolySmite, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveCurse, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Restoration_Greater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.BestowCurse, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeeperDarkness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyBlight, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CrushingHand, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Titan].Add(BuildData(CreatureConstants.Titan, FeatConstants.SpecialQualities.ChangeShape, focus: "Any Small or Medium Humanoid", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

            testCases[CreatureConstants.Toad].Add(BuildData(CreatureConstants.Toad, FeatConstants.SpecialQualities.Amphibious));

            testCases[CreatureConstants.Tojanida_Juvenile].Add(BuildData(CreatureConstants.Tojanida_Juvenile, FeatConstants.SpecialQualities.AllAroundVision));
            testCases[CreatureConstants.Tojanida_Juvenile].Add(BuildData(CreatureConstants.Tojanida_Juvenile, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Tojanida_Juvenile].Add(BuildData(CreatureConstants.Tojanida_Juvenile, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
            testCases[CreatureConstants.Tojanida_Juvenile].Add(BuildData(CreatureConstants.Tojanida_Juvenile, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.Tojanida_Juvenile].Add(BuildData(CreatureConstants.Tojanida_Juvenile, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Tojanida_Juvenile].Add(BuildData(CreatureConstants.Tojanida_Juvenile, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

            testCases[CreatureConstants.Tojanida_Adult].Add(BuildData(CreatureConstants.Tojanida_Adult, FeatConstants.SpecialQualities.AllAroundVision));
            testCases[CreatureConstants.Tojanida_Adult].Add(BuildData(CreatureConstants.Tojanida_Adult, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Tojanida_Adult].Add(BuildData(CreatureConstants.Tojanida_Adult, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
            testCases[CreatureConstants.Tojanida_Adult].Add(BuildData(CreatureConstants.Tojanida_Adult, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.Tojanida_Adult].Add(BuildData(CreatureConstants.Tojanida_Adult, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Tojanida_Adult].Add(BuildData(CreatureConstants.Tojanida_Adult, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

            testCases[CreatureConstants.Tojanida_Elder].Add(BuildData(CreatureConstants.Tojanida_Elder, FeatConstants.SpecialQualities.AllAroundVision));
            testCases[CreatureConstants.Tojanida_Elder].Add(BuildData(CreatureConstants.Tojanida_Elder, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Tojanida_Elder].Add(BuildData(CreatureConstants.Tojanida_Elder, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
            testCases[CreatureConstants.Tojanida_Elder].Add(BuildData(CreatureConstants.Tojanida_Elder, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.Tojanida_Elder].Add(BuildData(CreatureConstants.Tojanida_Elder, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Tojanida_Elder].Add(BuildData(CreatureConstants.Tojanida_Elder, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

            testCases[CreatureConstants.Treant].Add(BuildData(CreatureConstants.Treant, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to slashing weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Treant].Add(BuildData(CreatureConstants.Treant, FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));

            testCases[CreatureConstants.Triceratops].Add(BuildData(CreatureConstants.Triceratops, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Triton].Add(BuildData(CreatureConstants.Triton, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SummonNaturesAllyIV, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Troglodyte].Add(BuildData(CreatureConstants.Troglodyte, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Club, requiresEquipment: true));
            testCases[CreatureConstants.Troglodyte].Add(BuildData(CreatureConstants.Troglodyte, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Javelin, requiresEquipment: true));
            testCases[CreatureConstants.Troglodyte].Add(BuildData(CreatureConstants.Troglodyte, FeatConstants.SpecialQualities.Darkvision, power: 90));
            testCases[CreatureConstants.Troglodyte].Add(BuildData(CreatureConstants.Troglodyte, FeatConstants.Monster.Multiattack));

            testCases[CreatureConstants.Troll].Add(BuildData(CreatureConstants.Troll, FeatConstants.SpecialQualities.Darkvision, power: 90));
            testCases[CreatureConstants.Troll].Add(BuildData(CreatureConstants.Troll, FeatConstants.SpecialQualities.Regeneration, focus: "Fire and acid deal normal damage", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Troll].Add(BuildData(CreatureConstants.Troll, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Troll_Scrag].Add(BuildData(CreatureConstants.Troll_Scrag, FeatConstants.SpecialQualities.Darkvision, power: 90));
            testCases[CreatureConstants.Troll_Scrag].Add(BuildData(CreatureConstants.Troll_Scrag, FeatConstants.SpecialQualities.Regeneration, focus: "Fire and acid deal normal damage; only regenerates when immersed in water", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Troll_Scrag].Add(BuildData(CreatureConstants.Troll_Scrag, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.TrumpetArchon].Add(BuildData(CreatureConstants.TrumpetArchon, FeatConstants.SpecialQualities.AuraOfMenace, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 18));
            testCases[CreatureConstants.TrumpetArchon].Add(BuildData(CreatureConstants.TrumpetArchon, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to evil weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.TrumpetArchon].Add(BuildData(CreatureConstants.TrumpetArchon, FeatConstants.SpecialQualities.SpellResistance, power: 29));
            testCases[CreatureConstants.TrumpetArchon].Add(BuildData(CreatureConstants.TrumpetArchon, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ContinualFlame, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.TrumpetArchon].Add(BuildData(CreatureConstants.TrumpetArchon, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectEvil, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.TrumpetArchon].Add(BuildData(CreatureConstants.TrumpetArchon, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Message, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.TrumpetArchon].Add(BuildData(CreatureConstants.TrumpetArchon, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatsword, requiresEquipment: true));

            testCases[CreatureConstants.Tyrannosaurus].Add(BuildData(CreatureConstants.Tyrannosaurus, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.UmberHulk].Add(BuildData(CreatureConstants.UmberHulk, FeatConstants.SpecialQualities.Tremorsense, power: 60));

            testCases[CreatureConstants.UmberHulk_TrulyHorrid].Add(BuildData(CreatureConstants.UmberHulk_TrulyHorrid, FeatConstants.SpecialQualities.Tremorsense, power: 60));

            testCases[CreatureConstants.Unicorn].Add(BuildData(CreatureConstants.Unicorn, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicCircleAgainstEvil, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
            testCases[CreatureConstants.Unicorn].Add(BuildData(CreatureConstants.Unicorn, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectEvil, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Unicorn].Add(BuildData(CreatureConstants.Unicorn, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": within its forest home", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Unicorn].Add(BuildData(CreatureConstants.Unicorn, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CureLightWounds, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Unicorn].Add(BuildData(CreatureConstants.Unicorn, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CureModerateWounds, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Unicorn].Add(BuildData(CreatureConstants.Unicorn, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.NeutralizePoison, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Unicorn].Add(BuildData(CreatureConstants.Unicorn, FeatConstants.SpecialQualities.WildEmpathy));
            testCases[CreatureConstants.Unicorn].Add(BuildData(CreatureConstants.Unicorn, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Unicorn].Add(BuildData(CreatureConstants.Unicorn, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.Unicorn].Add(BuildData(CreatureConstants.Unicorn, FeatConstants.SpecialQualities.Immunity, focus: "Charm"));
            testCases[CreatureConstants.Unicorn].Add(BuildData(CreatureConstants.Unicorn, FeatConstants.SpecialQualities.Immunity, focus: "Compulsion"));

            testCases[CreatureConstants.VampireSpawn].Add(BuildData(CreatureConstants.VampireSpawn, FeatConstants.SpecialQualities.TurnResistance, power: 2));
            testCases[CreatureConstants.VampireSpawn].Add(BuildData(CreatureConstants.VampireSpawn, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to silver weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.VampireSpawn].Add(BuildData(CreatureConstants.VampireSpawn, FeatConstants.SpecialQualities.FastHealing, power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.VampireSpawn].Add(BuildData(CreatureConstants.VampireSpawn, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GaseousForm, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.VampireSpawn].Add(BuildData(CreatureConstants.VampireSpawn, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
            testCases[CreatureConstants.VampireSpawn].Add(BuildData(CreatureConstants.VampireSpawn, FeatConstants.Alertness, power: 2));
            testCases[CreatureConstants.VampireSpawn].Add(BuildData(CreatureConstants.VampireSpawn, FeatConstants.Initiative_Improved, power: 4));
            testCases[CreatureConstants.VampireSpawn].Add(BuildData(CreatureConstants.VampireSpawn, FeatConstants.LightningReflexes, power: 2));
            testCases[CreatureConstants.VampireSpawn].Add(BuildData(CreatureConstants.VampireSpawn, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.VampireSpawn].Add(BuildData(CreatureConstants.VampireSpawn, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

            testCases[CreatureConstants.Vargouille].Add(BuildData(CreatureConstants.Vargouille, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.VioletFungus] = [];

            testCases[CreatureConstants.Vrock].Add(BuildData(CreatureConstants.Vrock, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Vrock].Add(BuildData(CreatureConstants.Vrock, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.Vrock].Add(BuildData(CreatureConstants.Vrock, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Vrock].Add(BuildData(CreatureConstants.Vrock, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Vrock].Add(BuildData(CreatureConstants.Vrock, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Vrock].Add(BuildData(CreatureConstants.Vrock, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Vrock].Add(BuildData(CreatureConstants.Vrock, FeatConstants.SpecialQualities.SpellResistance, power: 17));
            testCases[CreatureConstants.Vrock].Add(BuildData(CreatureConstants.Vrock, FeatConstants.SpecialQualities.Telepathy, power: 100));
            testCases[CreatureConstants.Vrock].Add(BuildData(CreatureConstants.Vrock, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MirrorImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Vrock].Add(BuildData(CreatureConstants.Vrock, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Telekinesis, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
            testCases[CreatureConstants.Vrock].Add(BuildData(CreatureConstants.Vrock, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Vrock].Add(BuildData(CreatureConstants.Vrock, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Heroism, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

            testCases[CreatureConstants.Wasp_Giant] = [];

            testCases[CreatureConstants.Weasel].Add(BuildData(CreatureConstants.Weasel, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Weasel].Add(BuildData(CreatureConstants.Weasel, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Weasel_Dire].Add(BuildData(CreatureConstants.Weasel_Dire, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Weasel_Dire].Add(BuildData(CreatureConstants.Weasel_Dire, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.Whale_Baleen].Add(BuildData(CreatureConstants.Whale_Baleen, FeatConstants.SpecialQualities.Blindsight, power: 120));
            testCases[CreatureConstants.Whale_Baleen].Add(BuildData(CreatureConstants.Whale_Baleen, FeatConstants.SpecialQualities.HoldBreath));

            testCases[CreatureConstants.Whale_Cachalot].Add(BuildData(CreatureConstants.Whale_Cachalot, FeatConstants.SpecialQualities.Blindsight, power: 120));
            testCases[CreatureConstants.Whale_Cachalot].Add(BuildData(CreatureConstants.Whale_Cachalot, FeatConstants.SpecialQualities.HoldBreath));

            testCases[CreatureConstants.Whale_Orca].Add(BuildData(CreatureConstants.Whale_Orca, FeatConstants.SpecialQualities.Blindsight, power: 120));
            testCases[CreatureConstants.Whale_Orca].Add(BuildData(CreatureConstants.Whale_Orca, FeatConstants.SpecialQualities.HoldBreath));

            testCases[CreatureConstants.Wight] = [];

            testCases[CreatureConstants.WillOWisp].Add(BuildData(CreatureConstants.WillOWisp, FeatConstants.SpecialQualities.Immunity, focus: "Spells and spell-like effects that allow spell resistance, except Magic Missile and Maze"));
            testCases[CreatureConstants.WillOWisp].Add(BuildData(CreatureConstants.WillOWisp, FeatConstants.SpecialQualities.NaturalInvisibility));
            testCases[CreatureConstants.WillOWisp].Add(BuildData(CreatureConstants.WillOWisp, FeatConstants.WeaponFinesse));

            testCases[CreatureConstants.WinterWolf].Add(BuildData(CreatureConstants.WinterWolf, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Wolf].Add(BuildData(CreatureConstants.Wolf, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Wolf].Add(BuildData(CreatureConstants.Wolf, FeatConstants.Track));

            testCases[CreatureConstants.Wolf_Dire].Add(BuildData(CreatureConstants.Wolf_Dire, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Wolf_Dire].Add(BuildData(CreatureConstants.Wolf_Dire, FeatConstants.Track));

            testCases[CreatureConstants.Wolverine].Add(BuildData(CreatureConstants.Wolverine, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Wolverine].Add(BuildData(CreatureConstants.Wolverine, FeatConstants.Track));

            testCases[CreatureConstants.Wolverine_Dire].Add(BuildData(CreatureConstants.Wolverine_Dire, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Wolverine_Dire].Add(BuildData(CreatureConstants.Wolverine_Dire, FeatConstants.Track));

            testCases[CreatureConstants.Worg].Add(BuildData(CreatureConstants.Worg, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Wraith].Add(BuildData(CreatureConstants.Wraith, FeatConstants.SpecialQualities.DaylightPowerlessness));
            testCases[CreatureConstants.Wraith].Add(BuildData(CreatureConstants.Wraith, FeatConstants.SpecialQualities.TurnResistance, power: 2));
            testCases[CreatureConstants.Wraith].Add(BuildData(CreatureConstants.Wraith, FeatConstants.SpecialQualities.UnnaturalAura));
            testCases[CreatureConstants.Wraith].Add(BuildData(CreatureConstants.Wraith, FeatConstants.Alertness, power: 2));
            testCases[CreatureConstants.Wraith].Add(BuildData(CreatureConstants.Wraith, FeatConstants.Initiative_Improved, power: 4));

            testCases[CreatureConstants.Wraith_Dread].Add(BuildData(CreatureConstants.Wraith_Dread, FeatConstants.SpecialQualities.Lifesense, power: 60));
            testCases[CreatureConstants.Wraith_Dread].Add(BuildData(CreatureConstants.Wraith_Dread, FeatConstants.SpecialQualities.DaylightPowerlessness));
            testCases[CreatureConstants.Wraith_Dread].Add(BuildData(CreatureConstants.Wraith_Dread, FeatConstants.SpecialQualities.TurnResistance, power: 2));
            testCases[CreatureConstants.Wraith_Dread].Add(BuildData(CreatureConstants.Wraith_Dread, FeatConstants.SpecialQualities.UnnaturalAura));
            testCases[CreatureConstants.Wraith_Dread].Add(BuildData(CreatureConstants.Wraith_Dread, FeatConstants.Alertness, power: 2));
            testCases[CreatureConstants.Wraith_Dread].Add(BuildData(CreatureConstants.Wraith_Dread, FeatConstants.Initiative_Improved, power: 4));

            testCases[CreatureConstants.Wyvern].Add(BuildData(CreatureConstants.Wyvern, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Wyvern].Add(BuildData(CreatureConstants.Wyvern, FeatConstants.Monster.Multiattack));

            testCases[CreatureConstants.Xill].Add(BuildData(CreatureConstants.Xill, FeatConstants.SpecialQualities.SpellResistance, power: 21));
            testCases[CreatureConstants.Xill].Add(BuildData(CreatureConstants.Xill, FeatConstants.SpecialQualities.Planewalk));
            testCases[CreatureConstants.Xill].Add(BuildData(CreatureConstants.Xill, FeatConstants.Monster.Multiattack));

            testCases[CreatureConstants.Xorn_Minor].Add(BuildData(CreatureConstants.Xorn_Minor, FeatConstants.SpecialQualities.AllAroundVision));
            testCases[CreatureConstants.Xorn_Minor].Add(BuildData(CreatureConstants.Xorn_Minor, FeatConstants.SpecialQualities.EarthGlide));
            testCases[CreatureConstants.Xorn_Minor].Add(BuildData(CreatureConstants.Xorn_Minor, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to bludgeoning weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Xorn_Minor].Add(BuildData(CreatureConstants.Xorn_Minor, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
            testCases[CreatureConstants.Xorn_Minor].Add(BuildData(CreatureConstants.Xorn_Minor, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
            testCases[CreatureConstants.Xorn_Minor].Add(BuildData(CreatureConstants.Xorn_Minor, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Xorn_Minor].Add(BuildData(CreatureConstants.Xorn_Minor, FeatConstants.SpecialQualities.Tremorsense, power: 60));

            testCases[CreatureConstants.Xorn_Average].Add(BuildData(CreatureConstants.Xorn_Average, FeatConstants.SpecialQualities.AllAroundVision));
            testCases[CreatureConstants.Xorn_Average].Add(BuildData(CreatureConstants.Xorn_Average, FeatConstants.SpecialQualities.EarthGlide));
            testCases[CreatureConstants.Xorn_Average].Add(BuildData(CreatureConstants.Xorn_Average, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to bludgeoning weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Xorn_Average].Add(BuildData(CreatureConstants.Xorn_Average, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
            testCases[CreatureConstants.Xorn_Average].Add(BuildData(CreatureConstants.Xorn_Average, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
            testCases[CreatureConstants.Xorn_Average].Add(BuildData(CreatureConstants.Xorn_Average, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Xorn_Average].Add(BuildData(CreatureConstants.Xorn_Average, FeatConstants.SpecialQualities.Tremorsense, power: 60));
            testCases[CreatureConstants.Xorn_Average].Add(BuildData(CreatureConstants.Xorn_Average, FeatConstants.Cleave));

            testCases[CreatureConstants.Xorn_Elder].Add(BuildData(CreatureConstants.Xorn_Elder, FeatConstants.SpecialQualities.AllAroundVision));
            testCases[CreatureConstants.Xorn_Elder].Add(BuildData(CreatureConstants.Xorn_Elder, FeatConstants.SpecialQualities.EarthGlide));
            testCases[CreatureConstants.Xorn_Elder].Add(BuildData(CreatureConstants.Xorn_Elder, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to bludgeoning weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Xorn_Elder].Add(BuildData(CreatureConstants.Xorn_Elder, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
            testCases[CreatureConstants.Xorn_Elder].Add(BuildData(CreatureConstants.Xorn_Elder, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
            testCases[CreatureConstants.Xorn_Elder].Add(BuildData(CreatureConstants.Xorn_Elder, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Xorn_Elder].Add(BuildData(CreatureConstants.Xorn_Elder, FeatConstants.SpecialQualities.Tremorsense, power: 60));
            testCases[CreatureConstants.Xorn_Elder].Add(BuildData(CreatureConstants.Xorn_Elder, FeatConstants.Cleave));

            testCases[CreatureConstants.YethHound].Add(BuildData(CreatureConstants.YethHound, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to silver weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.YethHound].Add(BuildData(CreatureConstants.YethHound, FeatConstants.SpecialQualities.Flight));
            testCases[CreatureConstants.YethHound].Add(BuildData(CreatureConstants.YethHound, FeatConstants.SpecialQualities.Scent));

            testCases[CreatureConstants.Yrthak].Add(BuildData(CreatureConstants.Yrthak, FeatConstants.SpecialQualities.Blind));
            testCases[CreatureConstants.Yrthak].Add(BuildData(CreatureConstants.Yrthak, FeatConstants.SpecialQualities.Blindsight, power: 120));
            testCases[CreatureConstants.Yrthak].Add(BuildData(CreatureConstants.Yrthak, FeatConstants.SpecialQualities.Immunity, focus: "Gaze attacks"));
            testCases[CreatureConstants.Yrthak].Add(BuildData(CreatureConstants.Yrthak, FeatConstants.SpecialQualities.Immunity, focus: "Visual effects"));
            testCases[CreatureConstants.Yrthak].Add(BuildData(CreatureConstants.Yrthak, FeatConstants.SpecialQualities.Immunity, focus: "Illusions"));
            testCases[CreatureConstants.Yrthak].Add(BuildData(CreatureConstants.Yrthak, FeatConstants.SpecialQualities.Immunity, focus: "Attacks that rely on sight"));
            testCases[CreatureConstants.Yrthak].Add(BuildData(CreatureConstants.Yrthak, FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Sonic));

            testCases[CreatureConstants.YuanTi_Pureblood].Add(BuildData(CreatureConstants.YuanTi_Pureblood, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Scimitar, requiresEquipment: true));
            testCases[CreatureConstants.YuanTi_Pureblood].Add(BuildData(CreatureConstants.YuanTi_Pureblood, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));
            testCases[CreatureConstants.YuanTi_Pureblood].Add(BuildData(CreatureConstants.YuanTi_Pureblood, FeatConstants.Alertness, power: 2));
            testCases[CreatureConstants.YuanTi_Pureblood].Add(BuildData(CreatureConstants.YuanTi_Pureblood, FeatConstants.BlindFight, power: 2));
            testCases[CreatureConstants.YuanTi_Pureblood].Add(BuildData(CreatureConstants.YuanTi_Pureblood, FeatConstants.SpecialQualities.AlternateForm, focus: "a Tiny to Large viper", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.YuanTi_Pureblood].Add(BuildData(CreatureConstants.YuanTi_Pureblood, FeatConstants.SpecialQualities.SpellResistance, power: 14));
            testCases[CreatureConstants.YuanTi_Pureblood].Add(BuildData(CreatureConstants.YuanTi_Pureblood, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectPoison, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.YuanTi_Pureblood].Add(BuildData(CreatureConstants.YuanTi_Pureblood, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AnimalTrance, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.YuanTi_Pureblood].Add(BuildData(CreatureConstants.YuanTi_Pureblood, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CauseFear, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
            testCases[CreatureConstants.YuanTi_Pureblood].Add(BuildData(CreatureConstants.YuanTi_Pureblood, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmPerson, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
            testCases[CreatureConstants.YuanTi_Pureblood].Add(BuildData(CreatureConstants.YuanTi_Pureblood, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.YuanTi_Pureblood].Add(BuildData(CreatureConstants.YuanTi_Pureblood, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Entangle, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 11));

            testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeArms, FeatConstants.Alertness, power: 2));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeArms, FeatConstants.BlindFight, power: 2));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeArms, FeatConstants.SpecialQualities.AlternateForm, focus: "a Tiny to Large viper", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeArms, FeatConstants.SpecialQualities.ChameleonPower));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeArms, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeArms, FeatConstants.SpecialQualities.SpellResistance, power: 16));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeArms, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectPoison, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeArms, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AnimalTrance, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeArms, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CauseFear, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeArms, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeArms, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.NeutralizePoison, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeArms, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeeperDarkness, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeArms, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Entangle, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 11));

            testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeHead, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Scimitar, requiresEquipment: true));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeHead, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeHead, FeatConstants.Alertness, power: 2));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeHead, FeatConstants.BlindFight, power: 2));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeHead, FeatConstants.SpecialQualities.AlternateForm, focus: "a Tiny to Large viper", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeHead, FeatConstants.SpecialQualities.ChameleonPower));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeHead, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeHead, FeatConstants.SpecialQualities.SpellResistance, power: 16));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeHead, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectPoison, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeHead, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AnimalTrance, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeHead, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CauseFear, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeHead, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeHead, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.NeutralizePoison, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeHead, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeeperDarkness, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeHead, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Entangle, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 11));

            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTail, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Scimitar, requiresEquipment: true));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTail, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTail, FeatConstants.Alertness, power: 2));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTail, FeatConstants.BlindFight, power: 2));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTail, FeatConstants.SpecialQualities.AlternateForm, focus: "a Tiny to Large viper", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTail, FeatConstants.SpecialQualities.ChameleonPower));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTail, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTail, FeatConstants.SpecialQualities.SpellResistance, power: 16));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTail, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectPoison, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTail, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AnimalTrance, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTail, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CauseFear, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTail, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTail, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.NeutralizePoison, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTail, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeeperDarkness, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTail, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Entangle, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 11));

            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Scimitar, requiresEquipment: true));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, FeatConstants.Alertness, power: 2));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, FeatConstants.BlindFight, power: 2));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, FeatConstants.SpecialQualities.AlternateForm, focus: "a Tiny to Large viper", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, FeatConstants.SpecialQualities.ChameleonPower));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, FeatConstants.SpecialQualities.SpellResistance, power: 16));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectPoison, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AnimalTrance, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CauseFear, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 11));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.NeutralizePoison, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeeperDarkness, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Entangle, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 11));

            testCases[CreatureConstants.YuanTi_Abomination].Add(BuildData(CreatureConstants.YuanTi_Abomination, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Scimitar, requiresEquipment: true));
            testCases[CreatureConstants.YuanTi_Abomination].Add(BuildData(CreatureConstants.YuanTi_Abomination, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
            testCases[CreatureConstants.YuanTi_Abomination].Add(BuildData(CreatureConstants.YuanTi_Abomination, FeatConstants.Alertness, power: 2));
            testCases[CreatureConstants.YuanTi_Abomination].Add(BuildData(CreatureConstants.YuanTi_Abomination, FeatConstants.BlindFight, power: 2));
            testCases[CreatureConstants.YuanTi_Abomination].Add(BuildData(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.AlternateForm, focus: "a Tiny to Large viper", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.YuanTi_Abomination].Add(BuildData(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.ChameleonPower));
            testCases[CreatureConstants.YuanTi_Abomination].Add(BuildData(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.YuanTi_Abomination].Add(BuildData(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.SpellResistance, power: 18));
            testCases[CreatureConstants.YuanTi_Abomination].Add(BuildData(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectPoison, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.YuanTi_Abomination].Add(BuildData(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AnimalTrance, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 12));
            testCases[CreatureConstants.YuanTi_Abomination].Add(BuildData(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.YuanTi_Abomination].Add(BuildData(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.NeutralizePoison, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.YuanTi_Abomination].Add(BuildData(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeeperDarkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.YuanTi_Abomination].Add(BuildData(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.BalefulPolymorph + ": into snake form only", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
            testCases[CreatureConstants.YuanTi_Abomination].Add(BuildData(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fear, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.YuanTi_Abomination].Add(BuildData(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Entangle, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Reflex, saveBaseValue: 11));

            testCases[CreatureConstants.Zelekhut].Add(BuildData(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to chaotic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Zelekhut].Add(BuildData(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.FastHealing, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Zelekhut].Add(BuildData(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellResistance, power: 20));
            testCases[CreatureConstants.Zelekhut].Add(BuildData(CreatureConstants.Zelekhut, FeatConstants.WeaponProficiency_Exotic, focus: WeaponConstants.SpikedChain, requiresEquipment: true));
            testCases[CreatureConstants.Zelekhut].Add(BuildData(CreatureConstants.Zelekhut, FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
            testCases[CreatureConstants.Zelekhut].Add(BuildData(CreatureConstants.Zelekhut, FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
            testCases[CreatureConstants.Zelekhut].Add(BuildData(CreatureConstants.Zelekhut, FeatConstants.ArmorProficiency_Heavy, requiresEquipment: true));
            testCases[CreatureConstants.Zelekhut].Add(BuildData(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ClairaudienceClairvoyance, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Zelekhut].Add(BuildData(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DimensionalAnchor, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Zelekhut].Add(BuildData(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Zelekhut].Add(BuildData(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fear, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Zelekhut].Add(BuildData(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldPerson, frequencyTimePeriod: FeatConstants.Frequencies.AtWill, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 13));
            testCases[CreatureConstants.Zelekhut].Add(BuildData(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateCreature, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Zelekhut].Add(BuildData(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TrueSeeing, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Zelekhut].Add(BuildData(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldMonster, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 15));
            testCases[CreatureConstants.Zelekhut].Add(BuildData(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MarkOfJustice, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
            testCases[CreatureConstants.Zelekhut].Add(BuildData(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Earthquake, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week));
            testCases[CreatureConstants.Zelekhut].Add(BuildData(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Geas_Lesser, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveBaseValue: 14));
            testCases[CreatureConstants.Zelekhut].Add(BuildData(CreatureConstants.Zelekhut, FeatConstants.MountedCombat));

            return testCases;
        }

        public static Dictionary<string, List<string>> GetTypeData()
        {
            var testCases = new Dictionary<string, List<string>>();
            foreach (var type in CreatureConstants.Types.GetAll())
            {
                testCases[type] = [];
            }

            testCases[CreatureConstants.Types.Aberration].Add(BuildData(CreatureConstants.Types.Aberration, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Types.Aberration].Add(BuildData(CreatureConstants.Types.Aberration, FeatConstants.WeaponProficiency_Simple, focus: GroupConstants.All, requiresEquipment: true));
            testCases[CreatureConstants.Types.Aberration].Add(BuildData(CreatureConstants.Types.Aberration, FeatConstants.ShieldProficiency, requiresEquipment: true));

            testCases[CreatureConstants.Types.Animal].Add(BuildData(CreatureConstants.Types.Animal, FeatConstants.SpecialQualities.LowLightVision));

            testCases[CreatureConstants.Types.Construct].Add(BuildData(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Types.Construct].Add(BuildData(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Types.Construct].Add(BuildData(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, focus: "Mind-Affecting Effects"));
            testCases[CreatureConstants.Types.Construct].Add(BuildData(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.Types.Construct].Add(BuildData(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
            testCases[CreatureConstants.Types.Construct].Add(BuildData(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
            testCases[CreatureConstants.Types.Construct].Add(BuildData(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
            testCases[CreatureConstants.Types.Construct].Add(BuildData(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, focus: "Disease"));
            testCases[CreatureConstants.Types.Construct].Add(BuildData(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, focus: "Death"));
            testCases[CreatureConstants.Types.Construct].Add(BuildData(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, focus: "Necromancy"));
            testCases[CreatureConstants.Types.Construct].Add(BuildData(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
            testCases[CreatureConstants.Types.Construct].Add(BuildData(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, focus: "Nonlethal damage"));
            testCases[CreatureConstants.Types.Construct].Add(BuildData(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, focus: "Ability Damage"));
            testCases[CreatureConstants.Types.Construct].Add(BuildData(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, focus: "Ability Drain"));
            testCases[CreatureConstants.Types.Construct].Add(BuildData(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, focus: "Energy Drain"));
            testCases[CreatureConstants.Types.Construct].Add(BuildData(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, focus: "Fatigue"));
            testCases[CreatureConstants.Types.Construct].Add(BuildData(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, focus: "Exhaustion"));
            testCases[CreatureConstants.Types.Construct].Add(BuildData(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, focus: "Effect that requires a Fortitude save"));
            testCases[CreatureConstants.Types.Construct].Add(BuildData(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, focus: "Death from massive damage"));
            testCases[CreatureConstants.Types.Construct].Add(BuildData(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, focus: "Being raised or resurrected"));

            testCases[CreatureConstants.Types.Dragon].Add(BuildData(CreatureConstants.Types.Dragon, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Types.Dragon].Add(BuildData(CreatureConstants.Types.Dragon, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Types.Dragon].Add(BuildData(CreatureConstants.Types.Dragon, FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
            testCases[CreatureConstants.Types.Dragon].Add(BuildData(CreatureConstants.Types.Dragon, FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
            testCases[CreatureConstants.Types.Dragon].Add(BuildData(CreatureConstants.Types.Dragon, FeatConstants.WeaponProficiency_Simple, focus: GroupConstants.All, requiresEquipment: true));

            testCases[CreatureConstants.Types.Elemental].Add(BuildData(CreatureConstants.Types.Elemental, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Types.Elemental].Add(BuildData(CreatureConstants.Types.Elemental, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.Types.Elemental].Add(BuildData(CreatureConstants.Types.Elemental, FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
            testCases[CreatureConstants.Types.Elemental].Add(BuildData(CreatureConstants.Types.Elemental, FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
            testCases[CreatureConstants.Types.Elemental].Add(BuildData(CreatureConstants.Types.Elemental, FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
            testCases[CreatureConstants.Types.Elemental].Add(BuildData(CreatureConstants.Types.Elemental, FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
            testCases[CreatureConstants.Types.Elemental].Add(BuildData(CreatureConstants.Types.Elemental, FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));
            testCases[CreatureConstants.Types.Elemental].Add(BuildData(CreatureConstants.Types.Elemental, FeatConstants.WeaponProficiency_Simple, focus: GroupConstants.All, requiresEquipment: true));
            testCases[CreatureConstants.Types.Elemental].Add(BuildData(CreatureConstants.Types.Elemental, FeatConstants.ShieldProficiency, requiresEquipment: true));

            testCases[CreatureConstants.Types.Fey].Add(BuildData(CreatureConstants.Types.Fey, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Types.Fey].Add(BuildData(CreatureConstants.Types.Fey, FeatConstants.WeaponProficiency_Simple, focus: GroupConstants.All, requiresEquipment: true));
            testCases[CreatureConstants.Types.Fey].Add(BuildData(CreatureConstants.Types.Fey, FeatConstants.ShieldProficiency, requiresEquipment: true));

            testCases[CreatureConstants.Types.Giant].Add(BuildData(CreatureConstants.Types.Giant, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Types.Giant].Add(BuildData(CreatureConstants.Types.Giant, FeatConstants.WeaponProficiency_Simple, focus: GroupConstants.All, requiresEquipment: true));
            testCases[CreatureConstants.Types.Giant].Add(BuildData(CreatureConstants.Types.Giant, FeatConstants.WeaponProficiency_Martial, focus: GroupConstants.All, requiresEquipment: true));
            testCases[CreatureConstants.Types.Giant].Add(BuildData(CreatureConstants.Types.Giant, FeatConstants.ShieldProficiency, requiresEquipment: true));

            testCases[CreatureConstants.Types.Humanoid].Add(BuildData(CreatureConstants.Types.Humanoid, FeatConstants.WeaponProficiency_Simple, focus: GroupConstants.All, requiresEquipment: true));
            testCases[CreatureConstants.Types.Humanoid].Add(BuildData(CreatureConstants.Types.Humanoid, FeatConstants.ShieldProficiency, requiresEquipment: true));

            testCases[CreatureConstants.Types.MagicalBeast].Add(BuildData(CreatureConstants.Types.MagicalBeast, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Types.MagicalBeast].Add(BuildData(CreatureConstants.Types.MagicalBeast, FeatConstants.SpecialQualities.LowLightVision));

            testCases[CreatureConstants.Types.MonstrousHumanoid].Add(BuildData(CreatureConstants.Types.MonstrousHumanoid, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Types.MonstrousHumanoid].Add(BuildData(CreatureConstants.Types.MonstrousHumanoid, FeatConstants.WeaponProficiency_Simple, focus: GroupConstants.All, requiresEquipment: true));
            testCases[CreatureConstants.Types.MonstrousHumanoid].Add(BuildData(CreatureConstants.Types.MonstrousHumanoid, FeatConstants.ShieldProficiency, requiresEquipment: true));

            testCases[CreatureConstants.Types.Ooze].Add(BuildData(CreatureConstants.Types.Ooze, FeatConstants.SpecialQualities.Immunity, focus: "Mind-Affecting Effects"));
            testCases[CreatureConstants.Types.Ooze].Add(BuildData(CreatureConstants.Types.Ooze, FeatConstants.SpecialQualities.Blind));
            testCases[CreatureConstants.Types.Ooze].Add(BuildData(CreatureConstants.Types.Ooze, FeatConstants.SpecialQualities.Blindsight, power: 60));
            testCases[CreatureConstants.Types.Ooze].Add(BuildData(CreatureConstants.Types.Ooze, FeatConstants.SpecialQualities.Immunity, focus: "Gaze attacks, visual effects, illusions, and other attack forms that rely on sight"));
            testCases[CreatureConstants.Types.Ooze].Add(BuildData(CreatureConstants.Types.Ooze, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.Types.Ooze].Add(BuildData(CreatureConstants.Types.Ooze, FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
            testCases[CreatureConstants.Types.Ooze].Add(BuildData(CreatureConstants.Types.Ooze, FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
            testCases[CreatureConstants.Types.Ooze].Add(BuildData(CreatureConstants.Types.Ooze, FeatConstants.SpecialQualities.Immunity, focus: "Polymorph"));
            testCases[CreatureConstants.Types.Ooze].Add(BuildData(CreatureConstants.Types.Ooze, FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
            testCases[CreatureConstants.Types.Ooze].Add(BuildData(CreatureConstants.Types.Ooze, FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
            testCases[CreatureConstants.Types.Ooze].Add(BuildData(CreatureConstants.Types.Ooze, FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));

            testCases[CreatureConstants.Types.Outsider].Add(BuildData(CreatureConstants.Types.Outsider, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Types.Outsider].Add(BuildData(CreatureConstants.Types.Outsider, FeatConstants.WeaponProficiency_Simple, focus: GroupConstants.All, requiresEquipment: true));
            testCases[CreatureConstants.Types.Outsider].Add(BuildData(CreatureConstants.Types.Outsider, FeatConstants.WeaponProficiency_Martial, focus: GroupConstants.All, requiresEquipment: true));
            testCases[CreatureConstants.Types.Outsider].Add(BuildData(CreatureConstants.Types.Outsider, FeatConstants.ShieldProficiency, requiresEquipment: true));

            testCases[CreatureConstants.Types.Plant].Add(BuildData(CreatureConstants.Types.Plant, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Types.Plant].Add(BuildData(CreatureConstants.Types.Plant, FeatConstants.SpecialQualities.Immunity, focus: "Mind-Affecting Effects"));
            testCases[CreatureConstants.Types.Plant].Add(BuildData(CreatureConstants.Types.Plant, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.Types.Plant].Add(BuildData(CreatureConstants.Types.Plant, FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
            testCases[CreatureConstants.Types.Plant].Add(BuildData(CreatureConstants.Types.Plant, FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
            testCases[CreatureConstants.Types.Plant].Add(BuildData(CreatureConstants.Types.Plant, FeatConstants.SpecialQualities.Immunity, focus: "Polymorph"));
            testCases[CreatureConstants.Types.Plant].Add(BuildData(CreatureConstants.Types.Plant, FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
            testCases[CreatureConstants.Types.Plant].Add(BuildData(CreatureConstants.Types.Plant, FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));

            testCases[CreatureConstants.Types.Undead].Add(BuildData(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Types.Undead].Add(BuildData(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, focus: "Mind-Affecting Effects"));
            testCases[CreatureConstants.Types.Undead].Add(BuildData(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
            testCases[CreatureConstants.Types.Undead].Add(BuildData(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
            testCases[CreatureConstants.Types.Undead].Add(BuildData(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
            testCases[CreatureConstants.Types.Undead].Add(BuildData(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, focus: "Stunning"));
            testCases[CreatureConstants.Types.Undead].Add(BuildData(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, focus: "Disease"));
            testCases[CreatureConstants.Types.Undead].Add(BuildData(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, focus: "Death"));
            testCases[CreatureConstants.Types.Undead].Add(BuildData(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
            testCases[CreatureConstants.Types.Undead].Add(BuildData(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, focus: "Nonlethal damage"));
            testCases[CreatureConstants.Types.Undead].Add(BuildData(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, focus: "Ability Damage to Strength, Dexterity, or Constitution"));
            testCases[CreatureConstants.Types.Undead].Add(BuildData(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, focus: "Ability Drain"));
            testCases[CreatureConstants.Types.Undead].Add(BuildData(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, focus: "Energy Drain"));
            testCases[CreatureConstants.Types.Undead].Add(BuildData(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, focus: "Fatigue"));
            testCases[CreatureConstants.Types.Undead].Add(BuildData(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, focus: "Exhaustion"));
            testCases[CreatureConstants.Types.Undead].Add(BuildData(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, focus: "Effect that requires a Fortitude save"));
            testCases[CreatureConstants.Types.Undead].Add(BuildData(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, focus: "Death from massive damage"));
            testCases[CreatureConstants.Types.Undead].Add(BuildData(CreatureConstants.Types.Undead, FeatConstants.WeaponProficiency_Simple, focus: GroupConstants.All, requiresEquipment: true));
            testCases[CreatureConstants.Types.Undead].Add(BuildData(CreatureConstants.Types.Undead, FeatConstants.ShieldProficiency, requiresEquipment: true));

            testCases[CreatureConstants.Types.Vermin].Add(BuildData(CreatureConstants.Types.Vermin, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Types.Vermin].Add(BuildData(CreatureConstants.Types.Vermin, FeatConstants.SpecialQualities.Immunity, focus: "Mind-Affecting Effects"));

            return testCases;
        }

        public static Dictionary<string, List<string>> GetSubtypeData()
        {
            var testCases = new Dictionary<string, List<string>>();

            var subtypes = CreatureConstants.Types.Subtypes.GetAll()
                .Except(
                [
                        CreatureConstants.Types.Subtypes.Gnoll,
                        CreatureConstants.Types.Subtypes.Human,
                        CreatureConstants.Types.Subtypes.Orc,
                ]); //INFO: This is duplicated from the creature entry

            foreach (var subtype in subtypes)
            {
                testCases[subtype] = [];
            }

            testCases[CreatureConstants.Types.Subtypes.Air] = [];

            testCases[CreatureConstants.Types.Subtypes.Angel].Add(BuildData(CreatureConstants.Types.Subtypes.Angel, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Types.Subtypes.Angel].Add(BuildData(CreatureConstants.Types.Subtypes.Angel, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Types.Subtypes.Angel].Add(BuildData(CreatureConstants.Types.Subtypes.Angel, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
            testCases[CreatureConstants.Types.Subtypes.Angel].Add(BuildData(CreatureConstants.Types.Subtypes.Angel, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
            testCases[CreatureConstants.Types.Subtypes.Angel].Add(BuildData(CreatureConstants.Types.Subtypes.Angel, FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
            testCases[CreatureConstants.Types.Subtypes.Angel].Add(BuildData(CreatureConstants.Types.Subtypes.Angel, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Types.Subtypes.Angel].Add(BuildData(CreatureConstants.Types.Subtypes.Angel, FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
            testCases[CreatureConstants.Types.Subtypes.Angel].Add(BuildData(CreatureConstants.Types.Subtypes.Angel, FeatConstants.SpecialQualities.ProtectiveAura));
            testCases[CreatureConstants.Types.Subtypes.Angel].Add(BuildData(CreatureConstants.Types.Subtypes.Angel, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Tongues, frequencyTimePeriod: FeatConstants.Frequencies.Constant));

            testCases[CreatureConstants.Types.Subtypes.Aquatic] = [];

            testCases[CreatureConstants.Types.Subtypes.Archon].Add(BuildData(CreatureConstants.Types.Subtypes.Archon, FeatConstants.SpecialQualities.Darkvision, power: 60));
            testCases[CreatureConstants.Types.Subtypes.Archon].Add(BuildData(CreatureConstants.Types.Subtypes.Archon, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Types.Subtypes.Archon].Add(BuildData(CreatureConstants.Types.Subtypes.Archon, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
            testCases[CreatureConstants.Types.Subtypes.Archon].Add(BuildData(CreatureConstants.Types.Subtypes.Archon, FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
            testCases[CreatureConstants.Types.Subtypes.Archon].Add(BuildData(CreatureConstants.Types.Subtypes.Archon, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicCircleAgainstEvil, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
            testCases[CreatureConstants.Types.Subtypes.Archon].Add(BuildData(CreatureConstants.Types.Subtypes.Archon, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
            testCases[CreatureConstants.Types.Subtypes.Archon].Add(BuildData(CreatureConstants.Types.Subtypes.Archon, FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Tongues, frequencyTimePeriod: FeatConstants.Frequencies.Constant));

            testCases[CreatureConstants.Types.Subtypes.Augmented] = [];

            testCases[CreatureConstants.Types.Subtypes.Chaotic] = [];

            testCases[CreatureConstants.Types.Subtypes.Cold].Add(BuildData(CreatureConstants.Types.Subtypes.Cold, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
            testCases[CreatureConstants.Types.Subtypes.Cold].Add(BuildData(CreatureConstants.Types.Subtypes.Cold, FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));

            testCases[CreatureConstants.Types.Subtypes.Dwarf].Add(BuildData(CreatureConstants.Types.Subtypes.Dwarf, FeatConstants.SpecialQualities.Stonecunning));
            testCases[CreatureConstants.Types.Subtypes.Dwarf].Add(BuildData(CreatureConstants.Types.Subtypes.Dwarf, FeatConstants.SpecialQualities.Stability));
            testCases[CreatureConstants.Types.Subtypes.Dwarf].Add(BuildData(CreatureConstants.Types.Subtypes.Dwarf, FeatConstants.SpecialQualities.AttackBonus, focus: CreatureConstants.Types.Subtypes.Orc, power: 1));
            testCases[CreatureConstants.Types.Subtypes.Dwarf].Add(BuildData(CreatureConstants.Types.Subtypes.Dwarf, FeatConstants.SpecialQualities.AttackBonus, focus: CreatureConstants.Types.Subtypes.Goblinoid, power: 1));
            testCases[CreatureConstants.Types.Subtypes.Dwarf].Add(BuildData(CreatureConstants.Types.Subtypes.Dwarf, FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
            testCases[CreatureConstants.Types.Subtypes.Dwarf].Add(BuildData(CreatureConstants.Types.Subtypes.Dwarf, FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

            testCases[CreatureConstants.Types.Subtypes.Earth] = [];

            testCases[CreatureConstants.Types.Subtypes.Elf].Add(BuildData(CreatureConstants.Types.Subtypes.Elf, FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
            testCases[CreatureConstants.Types.Subtypes.Elf].Add(BuildData(CreatureConstants.Types.Subtypes.Elf, FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

            testCases[CreatureConstants.Types.Subtypes.Evil] = [];

            testCases[CreatureConstants.Types.Subtypes.Extraplanar] = [];

            testCases[CreatureConstants.Types.Subtypes.Fire].Add(BuildData(CreatureConstants.Types.Subtypes.Fire, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
            testCases[CreatureConstants.Types.Subtypes.Fire].Add(BuildData(CreatureConstants.Types.Subtypes.Fire, FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Cold));

            testCases[CreatureConstants.Types.Subtypes.Gnome].Add(BuildData(CreatureConstants.Types.Subtypes.Gnome, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.LightCrossbow, requiresEquipment: true));
            testCases[CreatureConstants.Types.Subtypes.Gnome].Add(BuildData(CreatureConstants.Types.Subtypes.Gnome, FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
            testCases[CreatureConstants.Types.Subtypes.Gnome].Add(BuildData(CreatureConstants.Types.Subtypes.Gnome, FeatConstants.SpecialQualities.LowLightVision));
            testCases[CreatureConstants.Types.Subtypes.Gnome].Add(BuildData(CreatureConstants.Types.Subtypes.Gnome, FeatConstants.SpecialQualities.WeaponFamiliarity, focus: WeaponConstants.GnomeHookedHammer, requiresEquipment: true));
            testCases[CreatureConstants.Types.Subtypes.Gnome].Add(BuildData(CreatureConstants.Types.Subtypes.Gnome, FeatConstants.SpecialQualities.AttackBonus, focus: CreatureConstants.Kobold, power: 1));
            testCases[CreatureConstants.Types.Subtypes.Gnome].Add(BuildData(CreatureConstants.Types.Subtypes.Gnome, FeatConstants.SpecialQualities.AttackBonus, focus: CreatureConstants.Types.Subtypes.Goblinoid, power: 1));

            testCases[CreatureConstants.Types.Subtypes.Goblinoid] = [];

            testCases[CreatureConstants.Types.Subtypes.Good] = [];

            testCases[CreatureConstants.Types.Subtypes.Halfling].Add(BuildData(CreatureConstants.Types.Subtypes.Halfling, FeatConstants.SpecialQualities.AttackBonus, focus: "thrown weapons and slings", power: 1));
            testCases[CreatureConstants.Types.Subtypes.Halfling].Add(BuildData(CreatureConstants.Types.Subtypes.Halfling, FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
            testCases[CreatureConstants.Types.Subtypes.Halfling].Add(BuildData(CreatureConstants.Types.Subtypes.Halfling, FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
            testCases[CreatureConstants.Types.Subtypes.Halfling].Add(BuildData(CreatureConstants.Types.Subtypes.Halfling, FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.LightCrossbow, requiresEquipment: true));

            testCases[CreatureConstants.Types.Subtypes.Incorporeal].Add(BuildData(CreatureConstants.Types.Subtypes.Incorporeal, FeatConstants.SpecialQualities.Immunity, focus: "Nonmagical attacks"));
            testCases[CreatureConstants.Types.Subtypes.Incorporeal].Add(BuildData(CreatureConstants.Types.Subtypes.Incorporeal, FeatConstants.SpecialQualities.Immunity, focus: "50% chance to ignore any damage from a corporeal source (except for positive energy, negative energy, force effects such as magic missiles, or attacks made with ghost touch weapons)"));
            testCases[CreatureConstants.Types.Subtypes.Incorporeal].Add(BuildData(CreatureConstants.Types.Subtypes.Incorporeal, FeatConstants.SpecialQualities.Immunity, focus: "Trip"));
            testCases[CreatureConstants.Types.Subtypes.Incorporeal].Add(BuildData(CreatureConstants.Types.Subtypes.Incorporeal, FeatConstants.SpecialQualities.Immunity, focus: "Grapple"));
            testCases[CreatureConstants.Types.Subtypes.Incorporeal].Add(BuildData(CreatureConstants.Types.Subtypes.Incorporeal, FeatConstants.SpecialQualities.Immunity, focus: "Falling or falling damage"));
            testCases[CreatureConstants.Types.Subtypes.Incorporeal].Add(BuildData(CreatureConstants.Types.Subtypes.Incorporeal, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.SpecialQualities.Scent));
            testCases[CreatureConstants.Types.Subtypes.Incorporeal].Add(BuildData(CreatureConstants.Types.Subtypes.Incorporeal, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.SpecialQualities.Blindsense));
            testCases[CreatureConstants.Types.Subtypes.Incorporeal].Add(BuildData(CreatureConstants.Types.Subtypes.Incorporeal, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.SpecialQualities.Blindsight));
            testCases[CreatureConstants.Types.Subtypes.Incorporeal].Add(BuildData(CreatureConstants.Types.Subtypes.Incorporeal, FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.SpecialQualities.Tremorsense));

            testCases[CreatureConstants.Types.Subtypes.Lawful] = [];

            testCases[CreatureConstants.Types.Subtypes.Native] = [];

            testCases[CreatureConstants.Types.Subtypes.Reptilian] = [];

            testCases[CreatureConstants.Types.Subtypes.Shapechanger].Add(BuildData(CreatureConstants.Types.Subtypes.Shapechanger, FeatConstants.WeaponProficiency_Simple, focus: GroupConstants.All, requiresEquipment: true));
            testCases[CreatureConstants.Types.Subtypes.Shapechanger].Add(BuildData(CreatureConstants.Types.Subtypes.Shapechanger, FeatConstants.ShieldProficiency, requiresEquipment: true));

            testCases[CreatureConstants.Types.Subtypes.Swarm].Add(BuildData(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.HalfDamage, focus: AttributeConstants.DamageTypes.Piercing, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Types.Subtypes.Swarm].Add(BuildData(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.HalfDamage, focus: AttributeConstants.DamageTypes.Slashing, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
            testCases[CreatureConstants.Types.Subtypes.Swarm].Add(BuildData(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Immunity, focus: "Weapon damage"));
            testCases[CreatureConstants.Types.Subtypes.Swarm].Add(BuildData(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
            testCases[CreatureConstants.Types.Subtypes.Swarm].Add(BuildData(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Immunity, focus: "Flanking"));
            testCases[CreatureConstants.Types.Subtypes.Swarm].Add(BuildData(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Immunity, focus: "Staggering"));
            testCases[CreatureConstants.Types.Subtypes.Swarm].Add(BuildData(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Immunity, focus: "Dying state"));
            testCases[CreatureConstants.Types.Subtypes.Swarm].Add(BuildData(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Immunity, focus: "Trip"));
            testCases[CreatureConstants.Types.Subtypes.Swarm].Add(BuildData(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Immunity, focus: "Grapple"));
            testCases[CreatureConstants.Types.Subtypes.Swarm].Add(BuildData(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Immunity, focus: "Bull Rush"));
            testCases[CreatureConstants.Types.Subtypes.Swarm].Add(BuildData(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Immunity, focus: "Any spell that targets a specific number of creatures, including single-target spells"));
            testCases[CreatureConstants.Types.Subtypes.Swarm].Add(BuildData(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Vulnerability, focus: "Area-of-effect spells"));
            testCases[CreatureConstants.Types.Subtypes.Swarm].Add(BuildData(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Vulnerability, focus: "Splash damage"));
            testCases[CreatureConstants.Types.Subtypes.Swarm].Add(BuildData(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Vulnerability, focus: "High winds"));

            testCases[CreatureConstants.Types.Subtypes.Water] = [];

            return testCases;
        }
    }
}
