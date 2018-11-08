using System.Collections.Generic;

namespace CreatureGen.Feats
{
    public static class FeatConstants
    {
        public const string Acrobatic = "Acrobatic";
        public const string Agile = "Agile";
        public const string Alertness = "Alertness";
        public const string AnimalAffinity = "Animal Affinity";
        public const string ArmorProficiency_Light = "Armor Proficiency (Light)";
        public const string ArmorProficiency_Medium = "Armor Proficiency (Medium)";
        public const string ArmorProficiency_Heavy = "Armor Proficiency (Heavy)";
        public const string Athletic = "Athletic";
        public const string AugmentSummoning = "Augment Summoning";
        public const string BlindFight = "Blind-Fight";
        public const string BullRush_Improved = "Improved Bull Rush";
        public const string Cleave = "Cleave";
        public const string Cleave_Great = "Great Cleave";
        public const string CombatCasting = "Combat Casting";
        public const string CombatExpertise = "Combat Expertise";
        public const string CombatReflexes = "Combat Reflexes";
        public const string Counterspell_Improved = "Improved Counterspell";
        public const string Critical_Improved = "Improved Critical";
        public const string Deceitful = "Deceitful";
        public const string DeftHands = "Deft Hands";
        public const string DeflectArrows = "Deflect Arrows";
        public const string Diehard = "Diehard";
        public const string Diligent = "Diligent";
        public const string Disarm_Improved = "Improved Disarm";
        public const string Dodge = "Dodge";
        public const string Endurance = "Endurance";
        public const string EschewMaterials = "Eschew Materials";
        //INFO: Creatures cannot get familiars without being a character class
        //public const string Familiar_Improved = "Improved Familiar";
        public const string FarShot = "Far Shot";
        public const string Feint_Improved = "Improved Feint";
        public const string GreatFortitude = "Great Fortitude";
        public const string Grapple_Improved = "Improved Grapple";
        public const string Initiative_Improved = "Improved Initiative";
        public const string Investigator = "Investigator";
        public const string IronWill = "Iron Will";
        //INFO: Requires character level 6
        //public const string Leadership = "Leadership";
        public const string LightningReflexes = "Lightning Reflexes";
        public const string MagicalAptitude = "Magical Aptitude";
        public const string Manyshot = "Manyshot";
        public const string Mobility = "Mobility";
        public const string MountedArchery = "Mounted Archery";
        public const string MountedCombat = "Mounted Combat";
        //INFO: Wild Shape, a prerequisite, is only had by Druid classes
        //public const string NaturalSpell = "Natural Spell";
        public const string Negotiator = "Negotiator";
        public const string NimbleFingers = "Nimble Fingers";
        public const string Overrun_Improved = "Improved Overrun";
        public const string Persuasive = "Persuasive";
        public const string PointBlankShot = "Point Blank Shot";
        public const string PowerAttack = "Power Attack";
        public const string PreciseShot = "Precise Shot";
        public const string PreciseShot_Improved = "Improved Precise Shot";
        public const string QuickDraw = "Quick Draw";
        public const string RapidReload = "Rapid Reload";
        public const string RapidShot = "Rapid Shot";
        public const string RideByAttack = "Ride-By Attack";
        public const string Run = "Run";
        public const string SelfSufficient = "Self-Sufficient";
        public const string ShieldProficiency = "Shield Proficiency";
        public const string ShieldBash_Improved = "Improved Shield Bash";
        public const string ShieldProficiency_Tower = "Tower Shield Proficiency";
        public const string ShotOnTheRun = "Shot On The Run";
        public const string SkillFocus = "Skill Focus";
        public const string SnatchArrows = "Snatch Arrows";
        public const string SpellFocus = "Spell Focus";
        public const string SpellFocus_Greater = "Greater Spell Focus";
        //INFO: Requirement is being a Wizard level 1
        //public const string SpellMastery = "Spell Mastery";
        public const string SpellPenetration = "Spell Penetration";
        public const string SpellPenetration_Greater = "Greater Spell Penetration";
        public const string SpiritedCharge = "Spirited Charge";
        public const string SpringAttack = "Spring Attack";
        public const string Stealthy = "Stealthy";
        public const string StunningFist = "Stunning Fist";
        public const string Sunder_Improved = "Improved Sunder";
        public const string Toughness = "Toughness";
        public const string Track = "Track";
        public const string Trample = "Trample";
        public const string Trip_Improved = "Improved Trip";
        //INFO: No monsters can natively turn or rebuke
        //public const string Turning_Extra = "Extra Turning";
        //public const string Turning_Improved = "Improved Turning";
        public const string TwoWeaponDefense = "Two-Weapon Defense";
        public const string TwoWeaponFighting = "Two-Weapon Fighting";
        public const string TwoWeaponFighting_Greater = "Greater Two-Weapon Fighting";
        public const string TwoWeaponFighting_Improved = "Improved Two-Weapon Fighting";
        public const string UnarmedStrike_Improved = "Improved Unarmed Strike";
        public const string WeaponFinesse = "Weapon Finesse";
        //INFO: Being a Fighter is a requirement for these feats
        //public const string WeaponFocus = "Weapon Focus";
        //public const string WeaponFocus_Greater = "Greater Weapon Focus";
        public const string WeaponProficiency_Exotic = "Exotic Weapon Proficiency";
        public const string WeaponProficiency_Martial = "Martial Weapon Proficiency";
        public const string WeaponProficiency_Simple = "Simple Weapon Proficiency";
        //INFO: Being a Fighter is a requirement for these feats
        //public const string WeaponSpecialization = "Weapon Specialization";
        //public const string WeaponSpecialization_Greater = "Greater Weapon Specialization";
        public const string WhirlwindAttack = "Whirlwind Attack";

        public static IEnumerable<string> All()
        {
            return new[]
            {
                Acrobatic,
                Agile,
                Alertness,
                AnimalAffinity,
                ArmorProficiency_Light,
                ArmorProficiency_Medium,
                ArmorProficiency_Heavy,
                Athletic,
                AugmentSummoning,
                BlindFight,
                BullRush_Improved,
                Cleave,
                Cleave_Great,
                CombatCasting,
                CombatExpertise,
                CombatReflexes,
                Counterspell_Improved,
                Critical_Improved,
                Deceitful,
                DeftHands,
                DeflectArrows,
                Diehard,
                Diligent,
                Disarm_Improved,
                Dodge,
                Endurance,
                EschewMaterials,
                //Familiar_Improved,
                FarShot,
                Feint_Improved,
                GreatFortitude,
                Grapple_Improved,
                Initiative_Improved,
                Investigator,
                IronWill,
                //Leadership,
                LightningReflexes,
                MagicalAptitude,
                Manyshot,
                Mobility,
                MountedArchery,
                MountedCombat,
                //NaturalSpell,
                Negotiator,
                NimbleFingers,
                Overrun_Improved,
                Persuasive,
                PointBlankShot,
                PowerAttack,
                PreciseShot,
                PreciseShot_Improved,
                QuickDraw,
                RapidReload,
                RapidShot,
                RideByAttack,
                Run,
                SelfSufficient,
                ShieldProficiency,
                ShieldBash_Improved,
                ShieldProficiency_Tower,
                ShotOnTheRun,
                SkillFocus,
                SnatchArrows,
                SpellFocus,
                SpellFocus_Greater,
                //SpellMastery,
                SpellPenetration,
                SpellPenetration_Greater,
                SpiritedCharge,
                SpringAttack,
                Stealthy,
                StunningFist,
                Sunder_Improved,
                Toughness,
                Track,
                Trample,
                Trip_Improved,
                //Turning_Extra,
                //Turning_Improved,
                TwoWeaponDefense,
                TwoWeaponFighting,
                TwoWeaponFighting_Greater,
                TwoWeaponFighting_Improved,
                UnarmedStrike_Improved,
                WeaponFinesse,
                //WeaponFocus,
                //WeaponFocus_Greater,
                WeaponProficiency_Exotic,
                WeaponProficiency_Martial,
                WeaponProficiency_Simple,
                //WeaponSpecialization,
                //WeaponSpecialization_Greater,
                WhirlwindAttack,
            };
        }

        public static class MagicItemCreation
        {
            public const string BrewPotion = "Brew Potion";
            public const string CraftMagicArmsAndArmor = "Craft Magic Arms and Armor";
            public const string CraftRod = "Craft Rod";
            public const string CraftStaff = "Craft Staff";
            public const string CraftWand = "Craft Wand";
            public const string CraftWondrousItem = "Craft Wondrous Item";
            public const string ForgeRing = "Forge Ring";
            public const string ScribeScroll = "Scribe Scroll";

            public static IEnumerable<string> All()
            {
                return new[]
                {
                    BrewPotion,
                    CraftMagicArmsAndArmor,
                    CraftRod,
                    CraftStaff,
                    CraftWand,
                    CraftWondrousItem,
                    ForgeRing,
                    ScribeScroll
                };
            }
        }

        public static class SpecialQualities
        {
            public const string Adhesive = "Adhesive";
            public const string AllAroundVision = "All-Around Vision";
            public const string AlternateForm = "Alternate Form";
            public const string Amorphous = "Amorphous";
            public const string Amphibious = "Amphibious";
            public const string AntimagicCone = "Antimagic Cone";
            public const string AttackBonus = "Attack Bonus";
            public const string AuraOfMenace = "Aura of Menace";
            public const string BarbedDefense = "Barbed Defense";
            public const string Blindsense = "Blindsense";
            public const string Blindsight = "Blindsight";
            public const string Camouflage = "Camouflage";
            public const string ChangeShape = "Change Shape";
            public const string CharmReptiles = "Charm Reptiles";
            public const string Cloudwalking = "Cloudwalking";
            public const string CorruptWater = "Corrupt Water";
            public const string CreateDestroyWater = "Create/Destroy Water";
            public const string DamageReduction = "Damage Reduction";
            public const string Darkvision = "Darkvision";
            public const string DaylightPowerlessness = "Daylight Powerlessness";
            public const string DeathThroes = "Death Throes";
            public const string Displacement = "Displacement";
            public const string DodgeBonus = "Dodge Bonus";
            public const string EarthGlide = "Earth Glide";
            public const string ElementalEndurance = "Elemental Endurance";
            public const string ElvenBlood = "Elven Blood";
            public const string EnergyResistance = "Energy Resistance";
            public const string Evasion = "Evasion";
            public const string FastHealing = "Fast Healing";
            public const string FlamingBody = "Flaming Body";
            public const string Flight = "Flight";
            public const string Freeze = "Freeze";
            public const string FreezingFog = "Freezing Fog";
            public const string FreshwaterSensitivity = "Freshwater Sensitivity";
            public const string Gills = "Gills";
            public const string HalfDamage = "Half Damage";
            public const string HiveMind = "Hive Mind";
            public const string HoldBreath = "Hold Breath";
            public const string Icewalking = "Icewalking";
            public const string Immunity = "Immunity";
            public const string InertialArmor = "Inertial Armor";
            public const string InkCloud = "Ink Cloud";
            public const string InvisibleInLight = "Invisible in Light";
            public const string Jet = "Jet";
            public const string KeenScent = "Keen Scent";
            public const string KeenSenses = "Keen Senses";
            public const string KeenSight = "Keen Sight";
            public const string LayOnHands = "Lay on Hands";
            public const string Lifesense = "Lifesense";
            public const string LightBlindness = "Light Blindness";
            public const string LightSensitivity = "Light Sensitivity";
            public const string LowLightVision = "Low-Light Vision";
            public const string LowLightVision_Superior = "Superior Low-Light Vision";
            public const string LuckBonus = "Luck Bonus";
            public const string Madness = "Madness";
            public const string MeltWeapons = "Melt Weapons";
            public const string MucusCloud = "Mucus Cloud";
            public const string NaturalInvisibility = "Natural Invisibility";
            public const string OrcBlood = "Orc Blood";
            public const string OversizedWeapon = "Oversized Weapon";
            public const string ProtectiveAura = "Protective Aura";
            public const string ProtectiveSlime = "Protective Slime";
            public const string Quickness = "Quickness";
            public const string Regeneration = "Regeneration";
            public const string RockCatching = "Rock Catching";
            public const string Scent = "Scent";
            public const string SeeInDarkness = "See in Darkness";
            public const string ShadowShift = "Shadow Shift";
            public const string Slippery = "Slippery";
            public const string SmokeForm = "Smoke Form";
            public const string SoundImitation = "Sound Imitation";
            public const string SpeakWithSharks = "Speak With Sharks";
            public const string SpellDeflection = "Spell Deflection";
            public const string SpellLikeAbility = "Spell-Like Ability";
            public const string SpellResistance = "Spell Resistance";
            public const string Split = "Split";
            public const string Sprint = "Sprint";
            public const string Stability = "Stability";
            public const string Stonecunning = "Stonecunning";
            public const string Telepathy = "Telepathy";
            public const string Tracking_Improved = "Improved Tracking";
            public const string Transparent = "Transparent";
            public const string TreeDependent = "Tree Dependent";
            public const string Tremorsense = "Tremorsense";
            public const string TurnResistance = "Turn Resistance";
            public const string TwoWeaponFighting_Superior = "Superior Two-Weapon Fighting";
            public const string UncannyDodge = "Uncanny Dodge";
            public const string UnnaturalAura = "Unnatural Aura";
            public const string Vulnerability = "Vulnerability";
            public const string WaterBreathing = "Water Breathing";
            public const string WaterDependent = "Water Dependent";
            public const string WeaponFamiliarity = "Weapon Familiarity";
            public const string WildEmpathy = "Wild Empathy";

            public static IEnumerable<string> All()
            {
                return new[]
                {
                    Adhesive,
                    AllAroundVision,
                    AlternateForm,
                    Amorphous,
                    Amphibious,
                    AntimagicCone,
                    AttackBonus,
                    AuraOfMenace,
                    BarbedDefense,
                    Blindsense,
                    Blindsight,
                    Camouflage,
                    ChangeShape,
                    CharmReptiles,
                    Cloudwalking,
                    CorruptWater,
                    CreateDestroyWater,
                    DamageReduction,
                    Darkvision,
                    DaylightPowerlessness,
                    DeathThroes,
                    Displacement,
                    DodgeBonus,
                    EarthGlide,
                    ElementalEndurance,
                    ElvenBlood,
                    EnergyResistance,
                    Evasion,
                    FastHealing,
                    FlamingBody,
                    Flight,
                    Freeze,
                    FreezingFog,
                    FreshwaterSensitivity,
                    Gills,
                    HalfDamage,
                    HiveMind,
                    HoldBreath,
                    Icewalking,
                    Immunity,
                    InertialArmor,
                    InkCloud,
                    InvisibleInLight,
                    Jet,
                    KeenScent,
                    KeenSenses,
                    KeenSight,
                    LayOnHands,
                    Lifesense,
                    LightBlindness,
                    LightSensitivity,
                    LowLightVision,
                    LowLightVision_Superior,
                    LuckBonus,
                    Madness,
                    MeltWeapons,
                    MucusCloud,
                    NaturalInvisibility,
                    OrcBlood,
                    OversizedWeapon,
                    ProtectiveAura,
                    ProtectiveSlime,
                    Quickness,
                    Regeneration,
                    RockCatching,
                    Scent,
                    SeeInDarkness,
                    ShadowShift,
                    Slippery,
                    SmokeForm,
                    SoundImitation,
                    SpeakWithSharks,
                    SpellDeflection,
                    SpellLikeAbility,
                    SpellResistance,
                    Split,
                    Sprint,
                    Stability,
                    Stonecunning,
                    Telepathy,
                    Tracking_Improved,
                    Transparent,
                    TreeDependent,
                    Tremorsense,
                    TurnResistance,
                    TwoWeaponFighting_Superior,
                    UncannyDodge,
                    UnnaturalAura,
                    Vulnerability,
                    WaterBreathing,
                    WaterDependent,
                    WeaponFamiliarity,
                    WildEmpathy,
                };
            }
        }

        public static class Metamagic
        {
            public const string EmpowerSpell = "Empower Spell";
            public const string EnlargeSpell = "Enlarge Spell";
            public const string ExtendSpell = "Extend Spell";
            public const string HeightenSpell = "Heighten Spell";
            public const string MaximizeSpell = "Maximize Spell";
            public const string QuickenSpell = "Quicken Spell";
            public const string SilentSpell = "Silent Spell";
            public const string StillSpell = "Still Spell";
            public const string WidenSpell = "Widen Spell";

            public static IEnumerable<string> All()
            {
                return new[]
                {
                    EmpowerSpell,
                    EnlargeSpell,
                    ExtendSpell,
                    HeightenSpell,
                    MaximizeSpell,
                    QuickenSpell,
                    SilentSpell,
                    StillSpell,
                    WidenSpell
                };
            }
        }

        public static class Monster
        {
            public const string AbilityFocus = "Ability Focus";
            public const string AwesomeBlow = "Awesome Blow";
            public const string CraftConstruct = "Craft Construct";
            public const string EmpowerSpellLikeAbility = "Empower Spell-Like Ability";
            public const string FlybyAttack = "Flyby Attack";
            public const string FlybyAttack_Improved = "Improved Flyby Attack";
            public const string Hover = "Hover";
            public const string Multiattack = "Multiattack";
            public const string Multiattack_Improved = "Improved Multiattack";
            public const string NaturalArmor_Improved = "Improved Natural Armor";
            public const string NaturalAttack_Improved = "Improved Natural Attack";
            public const string MultiweaponFighting = "Multiweapon Fighting";
            public const string MultiweaponFighting_Improved = "Improved Multiweapon Fighting";
            public const string MultiweaponFighting_Greater = "Greater Multiweapon Fighting";
            public const string QuickenSpellLikeAbility = "Quicken Spell-Like Ability";
            public const string Snatch = "Snatch";
            public const string Wingover = "Wingover";

            public static IEnumerable<string> All()
            {
                return new[]
                {
                    AbilityFocus,
                    AwesomeBlow,
                    CraftConstruct,
                    EmpowerSpellLikeAbility,
                    FlybyAttack,
                    FlybyAttack_Improved,
                    Hover,
                    Multiattack,
                    Multiattack_Improved,
                    NaturalArmor_Improved,
                    NaturalAttack_Improved,
                    MultiweaponFighting,
                    MultiweaponFighting_Improved,
                    MultiweaponFighting_Greater,
                    QuickenSpellLikeAbility,
                    Snatch,
                    Wingover,
                };
            }
        }

        public static class Frequencies
        {
            public const string Constant = "Constant";
            public const string AtWill = "At Will";
            public const string Hit = "Hit";
            public const string Round = "Round";
            public const string Turn = "Turn";
            public const string Hour = "Hour";
            public const string Day = "Day";
            public const string Week = "Week";
            public const string Month = "Month";
            public const string Year = "Year";
        }

        public static class Foci
        {
            public const string NoValidFociAvailable = "No Valid Foci Available";
            public const string Weapon = "Weapon";
            public const string WeaponWithUnarmed = "Weapon with Unarmed";
            public const string WeaponWithUnarmedAndGrapple = "Weapon with Unarmed and Grapple";
            public const string WeaponWithUnarmedAndGrappleAndRay = "Weapon with Unarmed, Grapple, and Ray";
            public const string Element = "Element";
            public const string School = "School";

            public static class Weapons
            {
                public const string Grapple = "Grapple";
                public const string UnarmedStrike = "Unarmed Strike";
                public const string Ray = "Ray";
            }

            public static class Elements
            {
                public const string Acid = "Acid";
                public const string Cold = "Cold";
                public const string Electricity = "Electricity";
                public const string Fire = "Fire";
                public const string Sonic = "Sonic";
            }

            public static class Schools
            {
                public const string Abjuration = "Abjuration";
                public const string Conjuration = "Conjuration";
                public const string Divination = "Divination";
                public const string Enchantment = "Enchantment";
                public const string Evocation = "Evocation";
                public const string Illusion = "Illusion";
                public const string Necromancy = "Necromancy";
                public const string Transmutation = "Transmutation";
            }
        }
    }
}