using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Creatures
{
    public static class CreatureConstants
    {
        public static class Types
        {
            public const string Aberration = "Aberration";
            public const string Animal = "Animal";
            public const string Construct = "Construct";
            public const string Dragon = "Dragon";
            public const string Elemental = "Elemental";
            public const string Fey = "Fey";
            public const string Giant = "Giant";
            public const string Humanoid = "Humanoid";
            public const string MagicalBeast = "Magical Beast";
            public const string MonstrousHumanoid = "Monstrous Humanoid";
            public const string Ooze = "Ooze";
            public const string Outsider = "Outsider";
            public const string Plant = "Plant";
            public const string Undead = "Undead";
            public const string Vermin = "Vermin";

            public static IEnumerable<string> GetAll()
            {
                return new[]
                {
                    Aberration,
                    Animal,
                    Construct,
                    Dragon,
                    Elemental,
                    Fey,
                    Giant,
                    Humanoid,
                    MagicalBeast,
                    MonstrousHumanoid,
                    Ooze,
                    Outsider,
                    Plant,
                    Undead,
                    Vermin,
                };
            }

            public static class Subtypes
            {
                public const string Air = "Air";
                public const string Angel = "Angel";
                public const string Aquatic = "Aquatic";
                public const string Archon = "Archon";
                public const string Augmented = "Augmented";
                public const string Chaotic = "Chaotic";
                public const string Cold = "Cold";
                public const string Dwarf = "Dwarf";
                public const string Earth = "Earth";
                public const string Elf = "Elf";
                public const string Evil = "Evil";
                public const string Extraplanar = "Extraplanar";
                public const string Fire = "Fire";
                public const string Gnoll = "Gnoll";
                public const string Gnome = "Gnome";
                public const string Goblinoid = "Goblinoid";
                public const string Good = "Good";
                public const string Halfling = "Halfling";
                public const string Human = "Human";
                public const string Incorporeal = "Incorporeal";
                public const string Lawful = "Lawful";
                public const string Native = "Native";
                public const string Orc = "Orc";
                public const string Reptilian = "Reptilian";
                public const string Shapechanger = "Shapechanger";
                public const string Swarm = "Swarm";
                public const string Water = "Water";

                public static IEnumerable<string> GetAll()
                {
                    return new[]
                    {
                        Air,
                        Angel,
                        Aquatic,
                        Archon,
                        Augmented,
                        Chaotic,
                        Cold,
                        Dwarf,
                        Earth,
                        Elf,
                        Evil,
                        Extraplanar,
                        Fire,
                        Gnoll,
                        Gnome,
                        Goblinoid,
                        Good,
                        Halfling,
                        Human,
                        Incorporeal,
                        Lawful,
                        Native,
                        Orc,
                        Reptilian,
                        Shapechanger,
                        Swarm,
                        Water,
                    };
                }
            }
        }

        public static class Templates
        {
            public const string CelestialCreature = "Celestial Creature";
            public const string FiendishCreature = "Fiendish Creature";
            public const string Ghost = "Ghost";
            public const string HalfCelestial = "Half-Celestial";
            public const string HalfDragon_Black = "Half-Dragon (Black)";
            public const string HalfDragon_Blue = "Half-Dragon (Blue)";
            public const string HalfDragon_Green = "Half-Dragon (Green)";
            public const string HalfDragon_Red = "Half-Dragon (Red)";
            public const string HalfDragon_White = "Half-Dragon (White)";
            public const string HalfDragon_Brass = "Half-Dragon (Brass)";
            public const string HalfDragon_Bronze = "Half-Dragon (Bronze)";
            public const string HalfDragon_Copper = "Half-Dragon (Copper)";
            public const string HalfDragon_Gold = "Half-Dragon (Gold)";
            public const string HalfDragon_Silver = "Half-Dragon (Silver)";
            public const string HalfFiend = "Half-Fiend";
            public const string Lich = "Lich";
            public const string Lycanthrope_Bear_Black_Afflicted = "Lycanthrope, Black Bear (Werebear, Afflicted)";
            public const string Lycanthrope_Bear_Black_Natural = "Lycanthrope, Black Bear (Werebear, Natural)";
            public const string Lycanthrope_Bear_Brown_Afflicted = "Lycanthrope, Brown Bear (Werebear, Afflicted)";
            public const string Lycanthrope_Bear_Brown_Natural = "Lycanthrope, Brown Bear (Werebear, Natural)";
            public const string Lycanthrope_Bear_Dire_Afflicted = "Lycanthrope, Dire Bear (Dire Werebear, Afflicted)";
            public const string Lycanthrope_Bear_Dire_Natural = "Lycanthrope, Dire Bear (Dire Werebear, Natural)";
            public const string Lycanthrope_Bear_Polar_Afflicted = "Lycanthrope, Polar Bear (Werebear, Afflicted)";
            public const string Lycanthrope_Bear_Polar_Natural = "Lycanthrope, Polar Bear (Werebear, Natural)";
            public const string Lycanthrope_Boar_Afflicted = "Lycanthrope, Boar (Wereboar, Afflicted)";
            public const string Lycanthrope_Boar_Natural = "Lycanthrope, Boar (Wereboar, Natural)";
            public const string Lycanthrope_Boar_Dire_Afflicted = "Lycanthrope, Dire Boar (Dire Wereboar, Afflicted)";
            public const string Lycanthrope_Boar_Dire_Natural = "Lycanthrope, Dire Boar (Dire Wereboar, Natural)";
            public const string Lycanthrope_Rat_Afflicted = "Lycanthrope, Rat (Wererat, Afflicted)";
            public const string Lycanthrope_Rat_Natural = "Lycanthrope, Rat (Wererat, Natural)";
            public const string Lycanthrope_Rat_Dire_Afflicted = "Lycanthrope, Dire Rat (Wererat, Afflicted)";
            public const string Lycanthrope_Rat_Dire_Natural = "Lycanthrope, Dire Rat (Wererat, Natural)";
            public const string Lycanthrope_Tiger_Afflicted = "Lycanthrope, Tiger (Weretiger, Afflicted)";
            public const string Lycanthrope_Tiger_Natural = "Lycanthrope, Tiger (Weretiger, Natural)";
            public const string Lycanthrope_Tiger_Dire_Afflicted = "Lycanthrope, Dire Tiger (Dire Weretiger, Afflicted)";
            public const string Lycanthrope_Tiger_Dire_Natural = "Lycanthrope, Dire Tiger (Dire Weretiger, Natural)";
            public const string Lycanthrope_Wolf_Afflicted = "Lycanthrope, Wolf (Werewolf, Afflicted)";
            public const string Lycanthrope_Wolf_Natural = "Lycanthrope, Wolf (Werewolf, Natural)";
            public const string Lycanthrope_Wolf_Dire_Afflicted = "Lycanthrope, Dire Wolf (Dire Werewolf, Afflicted)";
            public const string Lycanthrope_Wolf_Dire_Natural = "Lycanthrope, Dire Wolf (Dire Werewolf, Natural)";
            public const string None = "None";
            public const string Skeleton = "Skeleton";
            public const string Vampire = "Vampire";
            public const string Zombie = "Zombie";

            public static IEnumerable<string> GetAll()
            {
                return new[]
                {
                    CelestialCreature,
                    FiendishCreature,
                    Ghost,
                    HalfCelestial,
                    HalfDragon_Black,
                    HalfDragon_Blue,
                    HalfDragon_Brass,
                    HalfDragon_Bronze,
                    HalfDragon_Copper,
                    HalfDragon_Gold,
                    HalfDragon_Green,
                    HalfDragon_Red,
                    HalfDragon_Silver,
                    HalfDragon_White,
                    HalfFiend,
                    Lich,
                    None,
                    Skeleton,
                    Vampire,
                    Lycanthrope_Bear_Black_Afflicted,
                    Lycanthrope_Bear_Black_Natural,
                    Lycanthrope_Bear_Brown_Afflicted,
                    Lycanthrope_Bear_Brown_Natural,
                    Lycanthrope_Bear_Dire_Afflicted,
                    Lycanthrope_Bear_Dire_Natural,
                    Lycanthrope_Bear_Polar_Afflicted,
                    Lycanthrope_Bear_Polar_Natural,
                    Lycanthrope_Boar_Afflicted,
                    Lycanthrope_Boar_Natural,
                    Lycanthrope_Boar_Dire_Afflicted,
                    Lycanthrope_Boar_Dire_Natural,
                    Lycanthrope_Rat_Afflicted,
                    Lycanthrope_Rat_Natural,
                    Lycanthrope_Rat_Dire_Afflicted,
                    Lycanthrope_Rat_Dire_Natural,
                    Lycanthrope_Tiger_Afflicted,
                    Lycanthrope_Tiger_Natural,
                    Lycanthrope_Tiger_Dire_Afflicted,
                    Lycanthrope_Tiger_Dire_Natural,
                    Lycanthrope_Wolf_Afflicted,
                    Lycanthrope_Wolf_Natural,
                    Lycanthrope_Wolf_Dire_Afflicted,
                    Lycanthrope_Wolf_Dire_Natural,
                    Zombie,
                };
            }
        }

        public static class Groups
        {
            public const string Angel = "Angel";
            public const string AnimatedObject = "Animated Object";
            public const string Ant_Giant = "Giant Ant";
            public const string Archon = "Archon";
            public const string Arrowhawk = "Arrowhawk";
            public const string Bear = "Bear";
            public const string Centipede_Monstrous = "Monstrous Centipede";
            public const string Chimera = "Chimera";
            public const string Cryohydra = "Cryohydra";
            public const string Demon = "Demon";
            public const string Devil = "Devil";
            public const string Dinosaur = "Dinosaur";
            public const string Dragon_Black = "Black Dragon";
            public const string Dragon_Blue = "Blue Dragon";
            public const string Dragon_Brass = "Brass Dragon";
            public const string Dragon_Bronze = "Bronze Dragon";
            public const string Dragon_Copper = "Copper Dragon";
            public const string Dragon_Gold = "Gold Dragon";
            public const string Dragon_Green = "Green Dragon";
            public const string Dragon_Red = "Red Dragon";
            public const string Dragon_Silver = "Silver Dragon";
            public const string Dragon_White = "White Dragon";
            public const string Dwarf = "Dwarf";
            public const string Elemental = "Elemental";
            public const string Elemental_Air = "Air Elemental";
            public const string Elemental_Earth = "Earth Elemental";
            public const string Elemental_Fire = "Fire Elemental";
            public const string Elemental_Water = "Water Elemental";
            public const string Elf = "Elf";
            public const string Formian = "Formian";
            public const string Fungus = "Fungus";
            public const string Genie = "Genie";
            public const string Gnome = "Gnome";
            public const string Golem = "Golem";
            public const string Hag = "Hag";
            public const string HalfDragon = "Half-Dragon";
            public const string Halfling = "Halfling";
            public const string Horse = "Horse";
            public const string Hydra = "Hydra";
            public const string Inevitable = "Inevitable";
            public const string Lycanthrope = "Lycanthrope";
            public const string Mephit = "Mephit";
            public const string Naga = "Naga";
            public const string Nightshade = "Nightshade";
            public const string Orc = "Orc";
            public const string Planetouched = "Planetouched";
            public const string Pyrohydra = "Pyrohydra";
            public const string Salamander = "Salamander";
            public const string Scorpion_Monstrous = "Monstrous Scorpion";
            public const string Shark = "Shark";
            public const string Slaad = "Slaad";
            public const string Snake_Viper = "Viper Snake";
            public const string Sphinx = "Sphinx";
            public const string Spider_Monstrous = "Monstrous Spider";
            public const string Sprite = "Sprite";
            public const string Tojanida = "Tojanida";
            public const string Whale = "Whale";
            public const string Xorn = "Xorn";
            public const string YuanTi = "Yuan-ti";
        }

        public const string Aasimar = "Aasimar";
        public const string Aboleth = "Aboleth";
        public const string Achaierai = "Achaierai";
        public const string Allip = "Allip";
        public const string Androsphinx = "Androsphinx";
        public const string Angel_AstralDeva = "Angel, Astral Deva";

        //block, anvil, candle, crowbar, caltrop, grappling hook, hammer, jug, lamp, lantern, lock, spyglass, vial, clock
        public const string AnimatedObject_Colossal = "Animated Object, Colossal";
        //Sled, block, box, ram, torch
        public const string AnimatedObject_Colossal_Wooden = "Animated Object, Colossal (Wooden)";
        //Carpet, rug, tapestry, clothes
        public const string AnimatedObject_Colossal_Sheetlike = "Animated Object, Colossal (Sheetlike)";
        //rope, vine, chain, manacles, necklace
        public const string AnimatedObject_Colossal_Flexible = "Animated Object, Colossal (Flexible)";
        //ladder, humanoid statue
        public const string AnimatedObject_Colossal_TwoLegs_Wooden = "Animated Object, Colossal (Two Legs, Wooden)";
        //ladder, humanoid statue
        public const string AnimatedObject_Colossal_TwoLegs = "Animated Object, Colossal (Two Legs)";
        //table, chair, stool, animal statue, desk, bed
        public const string AnimatedObject_Colossal_MultipleLegs_Wooden = "Animated Object, Colossal (More Than Two Legs, Wooden)";
        //table, chair, stool, animal statue, desk, bed
        public const string AnimatedObject_Colossal_MultipleLegs = "Animated Object, Colossal (More Than Two Legs)";
        //wagon, carriage, cart
        public const string AnimatedObject_Colossal_Wheels_Wooden = "Animated Object, Colossal (Wheels, Wooden)";
        public const string AnimatedObject_Gargantuan = "Animated Object, Gargantuan";
        public const string AnimatedObject_Gargantuan_Wooden = "Animated Object, Gargantuan (Wooden)";
        public const string AnimatedObject_Gargantuan_Sheetlike = "Animated Object, Gargantuan (Sheetlike)";
        public const string AnimatedObject_Gargantuan_Flexible = "Animated Object, Gargantuan (Flexible)";
        public const string AnimatedObject_Gargantuan_TwoLegs_Wooden = "Animated Object, Gargantuan (Two Legs, Wooden)";
        public const string AnimatedObject_Gargantuan_TwoLegs = "Animated Object, Gargantuan (Two Legs)";
        public const string AnimatedObject_Gargantuan_MultipleLegs_Wooden = "Animated Object, Gargantuan (More Than Two Legs, Wooden)";
        public const string AnimatedObject_Gargantuan_MultipleLegs = "Animated Object, Gargantuan (More Than Two Legs)";
        public const string AnimatedObject_Gargantuan_Wheels_Wooden = "Animated Object, Gargantuan (Wheels, Wooden)";
        public const string AnimatedObject_Huge = "Animated Object, Huge";
        public const string AnimatedObject_Huge_Wooden = "Animated Object, Huge (Wooden)";
        public const string AnimatedObject_Huge_Sheetlike = "Animated Object, Huge (Sheetlike)";
        public const string AnimatedObject_Huge_Flexible = "Animated Object, Huge (Flexible)";
        public const string AnimatedObject_Huge_TwoLegs_Wooden = "Animated Object, Huge (Two Legs, Wooden)";
        public const string AnimatedObject_Huge_TwoLegs = "Animated Object, Huge (Two Legs)";
        public const string AnimatedObject_Huge_MultipleLegs_Wooden = "Animated Object, Huge (More Than Two Legs, Wooden)";
        public const string AnimatedObject_Huge_MultipleLegs = "Animated Object, Huge (More Than Two Legs)";
        public const string AnimatedObject_Huge_Wheels_Wooden = "Animated Object, Huge (Wheels, Wooden)";
        public const string AnimatedObject_Large = "Animated Object, Large";
        public const string AnimatedObject_Large_Wooden = "Animated Object, Large (Wooden)";
        public const string AnimatedObject_Large_Sheetlike = "Animated Object, Large (Sheetlike)";
        public const string AnimatedObject_Large_Flexible = "Animated Object, Large (Flexible)";
        public const string AnimatedObject_Large_TwoLegs_Wooden = "Animated Object, Large (Two Legs, Wooden)";
        public const string AnimatedObject_Large_TwoLegs = "Animated Object, Large (Two Legs)";
        public const string AnimatedObject_Large_MultipleLegs_Wooden = "Animated Object, Large (More Than Two Legs, Wooden)";
        public const string AnimatedObject_Large_MultipleLegs = "Animated Object, Large (More Than Two Legs)";
        public const string AnimatedObject_Large_Wheels_Wooden = "Animated Object, Large (Wheels, Wooden)";
        public const string AnimatedObject_Medium = "Animated Object, Medium";
        public const string AnimatedObject_Medium_Wooden = "Animated Object, Medium (Wooden)";
        public const string AnimatedObject_Medium_Sheetlike = "Animated Object, Medium (Sheetlike)";
        public const string AnimatedObject_Medium_Flexible = "Animated Object, Medium (Flexible)";
        public const string AnimatedObject_Medium_TwoLegs_Wooden = "Animated Object, Medium (Two Legs, Wooden)";
        public const string AnimatedObject_Medium_TwoLegs = "Animated Object, Medium (Two Legs)";
        public const string AnimatedObject_Medium_MultipleLegs_Wooden = "Animated Object, Medium (More Than Two Legs, Wooden)";
        public const string AnimatedObject_Medium_MultipleLegs = "Animated Object, Medium (More Than Two Legs)";
        public const string AnimatedObject_Medium_Wheels_Wooden = "Animated Object, Medium (Wheels, Wooden)";
        public const string AnimatedObject_Small = "Animated Object, Small";
        public const string AnimatedObject_Small_Wooden = "Animated Object, Small (Wooden)";
        public const string AnimatedObject_Small_Sheetlike = "Animated Object, Small (Sheetlike)";
        public const string AnimatedObject_Small_Flexible = "Animated Object, Small (Flexible)";
        public const string AnimatedObject_Small_TwoLegs_Wooden = "Animated Object, Small (Two Legs, Wooden)";
        public const string AnimatedObject_Small_TwoLegs = "Animated Object, Small (Two Legs)";
        public const string AnimatedObject_Small_MultipleLegs_Wooden = "Animated Object, Small (More Than Two Legs, Wooden)";
        public const string AnimatedObject_Small_MultipleLegs = "Animated Object, Small (More Than Two Legs)";
        public const string AnimatedObject_Small_Wheels_Wooden = "Animated Object, Small (Wheels, Wooden)";
        public const string AnimatedObject_Tiny = "Animated Object, Tiny";
        public const string AnimatedObject_Tiny_Wooden = "Animated Object, Tiny (Wooden)";
        public const string AnimatedObject_Tiny_Sheetlike = "Animated Object, Tiny (Sheetlike)";
        public const string AnimatedObject_Tiny_Flexible = "Animated Object, Tiny (Flexible)";
        public const string AnimatedObject_Tiny_TwoLegs_Wooden = "Animated Object, Tiny (Two Legs, Wooden)";
        public const string AnimatedObject_Tiny_TwoLegs = "Animated Object, Tiny (Two Legs)";
        public const string AnimatedObject_Tiny_MultipleLegs_Wooden = "Animated Object, Tiny (More Than Two Legs, Wooden)";
        public const string AnimatedObject_Tiny_MultipleLegs = "Animated Object, Tiny (More Than Two Legs)";
        public const string AnimatedObject_Tiny_Wheels_Wooden = "Animated Object, Tiny (Wheels, Wooden)";
        public const string Ankheg = "Ankheg";
        public const string Annis = "Annis";
        public const string Ant_Giant_Queen = "Giant Ant, Queen";
        public const string Ant_Giant_Soldier = "Giant Ant, Soldier";
        public const string Ant_Giant_Worker = "Giant Ant, Worker";
        public const string Ape = "Ape";
        public const string Ape_Dire = "Dire Ape";
        public const string Aranea = "Aranea";
        public const string Arrowhawk_Adult = "Adult Arrowhawk";
        public const string Arrowhawk_Elder = "Elder Arrowhawk";
        public const string Arrowhawk_Juvenile = "Juvenile Arrowhawk";
        public const string AssassinVine = "Assassin Vine";
        public const string Athach = "Athach";
        public const string Avoral = "Avoral";
        public const string Azer = "Azer";
        public const string Babau = "Babau";
        public const string Baboon = "Baboon";
        public const string Badger = "Badger";
        public const string Badger_Dire = "Dire Badger";
        public const string Balor = "Balor";
        public const string BarbedDevil_Hamatula = "Barbed Devil (Hamatula)";
        public const string Barghest = "Barghest";
        public const string Barghest_Greater = "Greater Barghest";
        public const string Basilisk = "Basilisk";
        public const string Basilisk_Greater = "Greater Basilisk";
        public const string Bat = "Bat";
        public const string Bat_Dire = "Dire Bat";
        public const string Bat_Swarm = "Bat Swarm";
        public const string Bear_Black = "Black Bear";
        public const string Bear_Brown = "Brown Bear";
        public const string Bear_Dire = "Dire Bear";
        public const string Bear_Polar = "Polar Bear";
        public const string BeardedDevil_Barbazu = "Bearded Devil (Barbazu)";
        public const string Bebilith = "Bebilith";
        public const string Bee_Giant = "Giant Bee";
        public const string Behir = "Behir";
        public const string Beholder = "Beholder";
        public const string Beholder_Gauth = "Gauth (Lesser Beholder)";
        public const string Belker = "Belker";
        public const string Bison = "Bison";
        public const string BlackPudding = "Black Pudding";
        public const string BlackPudding_Elder = "Elder Black Pudding";
        public const string BlinkDog = "Blink Dog";
        public const string Boar = "Boar";
        public const string Boar_Dire = "Dire Boar";
        public const string Bodak = "Bodak";
        public const string BombardierBeetle_Giant = "Giant Bombardier Beetle";
        public const string BoneDevil_Osyluth = "Bone Devil (Osyluth)";
        public const string Bralani = "Bralani";
        public const string Bugbear = "Bugbear";
        public const string Bulette = "Bulette";
        public const string Camel_Dromedary = "Camel, Dromedary (One-Humped)";
        public const string Camel_Bactrian = "Camel, Bactrian (Two-Humped)";
        public const string CarrionCrawler = "Carrion Crawler";
        public const string Cat = "Cat";
        public const string Centaur = "Centaur";
        public const string Centipede_Monstrous_Colossal = "Monstrous Centipede, Colossal";
        public const string Centipede_Monstrous_Gargantuan = "Monstrous Centipede, Gargantuan";
        public const string Centipede_Monstrous_Huge = "Monstrous Centipede, Huge";
        public const string Centipede_Monstrous_Large = "Monstrous Centipede, Large";
        public const string Centipede_Monstrous_Medium = "Monstrous Centipede, Medium";
        public const string Centipede_Monstrous_Small = "Monstrous Centipede, Small";
        public const string Centipede_Monstrous_Tiny = "Monstrous Centipede, Tiny";
        public const string Centipede_Swarm = "Centipede Swarm";
        public const string ChainDevil_Kyton = "Chain Devil (Kyton)";
        public const string ChaosBeast = "Chaos Beast";
        public const string Cheetah = "Cheetah";
        public const string Chimera_Black = "Chimera (Black Dragon head)";
        public const string Chimera_Blue = "Chimera (Blue Dragon head)";
        public const string Chimera_Green = "Chimera (Green Dragon head)";
        public const string Chimera_Red = "Chimera (Red Dragon head)";
        public const string Chimera_White = "Chimera (White Dragon head)";
        public const string Choker = "Choker";
        public const string Chuul = "Chuul";
        public const string Cloaker = "Cloaker";
        public const string Cockatrice = "Cockatrice";
        public const string Couatl = "Couatl";
        public const string Criosphinx = "Criosphinx";
        public const string Crocodile = "Crocodile";
        public const string Crocodile_Giant = "Giant Crocodile";
        public const string Cryohydra_5Heads = "Five-Headed Cryohydra";
        public const string Cryohydra_6Heads = "Six-Headed Cryohydra";
        public const string Cryohydra_7Heads = "Seven-Headed Cryohydra";
        public const string Cryohydra_8Heads = "Eight-Headed Cryohydra";
        public const string Cryohydra_9Heads = "Nine-Headed Cryohydra";
        public const string Cryohydra_10Heads = "Ten-Headed Cryohydra";
        public const string Cryohydra_11Heads = "Eleven-Headed Cryohydra";
        public const string Cryohydra_12Heads = "Twelve-Headed Cryohydra";
        public const string Darkmantle = "Darkmantle";
        public const string Deinonychus = "Deinonychus";
        public const string Delver = "Delver";
        public const string Derro = "Derro";
        public const string Derro_Sane = "Derro (Sane)";
        public const string Destrachan = "Destrachan";
        public const string Devourer = "Devourer";
        public const string Digester = "Digester";
        public const string DisplacerBeast = "Displacer Beast";
        public const string DisplacerBeast_PackLord = "Displacer Beast Pack Lord";
        public const string Djinni = "Djinni";
        public const string Djinni_Noble = "Noble Djinni";
        public const string Dog = "Dog";
        public const string Dog_Riding = "Dog, Riding";
        public const string Hyena = "Hyena";
        public const string Donkey = "Donkey";
        public const string Doppelganger = "Doppelganger";
        public const string Dragon_Black_Wyrmling = "Black Dragon, Wyrmling";
        public const string Dragon_Black_VeryYoung = "Black Dragon, Very Young";
        public const string Dragon_Black_Young = "Black Dragon, Young";
        public const string Dragon_Black_Juvenile = "Black Dragon, Juvenile";
        public const string Dragon_Black_YoungAdult = "Black Dragon, Young Adult";
        public const string Dragon_Black_Adult = "Black Dragon, Adult";
        public const string Dragon_Black_MatureAdult = "Black Dragon, Mature Adult";
        public const string Dragon_Black_Old = "Black Dragon, Old";
        public const string Dragon_Black_VeryOld = "Black Dragon, Very Old";
        public const string Dragon_Black_Ancient = "Black Dragon, Ancient";
        public const string Dragon_Black_Wyrm = "Black Dragon, Wyrm";
        public const string Dragon_Black_GreatWyrm = "Black Dragon, Great Wyrm";
        public const string Dragon_Blue_Wyrmling = "Blue Dragon, Wyrmling";
        public const string Dragon_Blue_VeryYoung = "Blue Dragon, Very Young";
        public const string Dragon_Blue_Young = "Blue Dragon, Young";
        public const string Dragon_Blue_Juvenile = "Blue Dragon, Juvenile";
        public const string Dragon_Blue_YoungAdult = "Blue Dragon, Young Adult";
        public const string Dragon_Blue_Adult = "Blue Dragon, Adult";
        public const string Dragon_Blue_MatureAdult = "Blue Dragon, Mature Adult";
        public const string Dragon_Blue_Old = "Blue Dragon, Old";
        public const string Dragon_Blue_VeryOld = "Blue Dragon, Very Old";
        public const string Dragon_Blue_Ancient = "Blue Dragon, Ancient";
        public const string Dragon_Blue_Wyrm = "Blue Dragon, Wyrm";
        public const string Dragon_Blue_GreatWyrm = "Blue Dragon, Great Wyrm";
        public const string Dragon_Brass_Wyrmling = "Brass Dragon, Wyrmling";
        public const string Dragon_Brass_VeryYoung = "Brass Dragon, Very Young";
        public const string Dragon_Brass_Young = "Brass Dragon, Young";
        public const string Dragon_Brass_Juvenile = "Brass Dragon, Juvenile";
        public const string Dragon_Brass_YoungAdult = "Brass Dragon, Young Adult";
        public const string Dragon_Brass_Adult = "Brass Dragon, Adult";
        public const string Dragon_Brass_MatureAdult = "Brass Dragon, Mature Adult";
        public const string Dragon_Brass_Old = "Brass Dragon, Old";
        public const string Dragon_Brass_VeryOld = "Brass Dragon, Very Old";
        public const string Dragon_Brass_Ancient = "Brass Dragon, Ancient";
        public const string Dragon_Brass_Wyrm = "Brass Dragon, Wyrm";
        public const string Dragon_Brass_GreatWyrm = "Brass Dragon, Great Wyrm";
        public const string Dragon_Bronze_Wyrmling = "Bronze Dragon, Wyrmling";
        public const string Dragon_Bronze_VeryYoung = "Bronze Dragon, Very Young";
        public const string Dragon_Bronze_Young = "Bronze Dragon, Young";
        public const string Dragon_Bronze_Juvenile = "Bronze Dragon, Juvenile";
        public const string Dragon_Bronze_YoungAdult = "Bronze Dragon, Young Adult";
        public const string Dragon_Bronze_Adult = "Bronze Dragon, Adult";
        public const string Dragon_Bronze_MatureAdult = "Bronze Dragon, Mature Adult";
        public const string Dragon_Bronze_Old = "Bronze Dragon, Old";
        public const string Dragon_Bronze_VeryOld = "Bronze Dragon, Very Old";
        public const string Dragon_Bronze_Ancient = "Bronze Dragon, Ancient";
        public const string Dragon_Bronze_Wyrm = "Bronze Dragon, Wyrm";
        public const string Dragon_Bronze_GreatWyrm = "Bronze Dragon, Great Wyrm";
        public const string Dragon_Copper_Wyrmling = "Copper Dragon, Wyrmling";
        public const string Dragon_Copper_VeryYoung = "Copper Dragon, Very Young";
        public const string Dragon_Copper_Young = "Copper Dragon, Young";
        public const string Dragon_Copper_Juvenile = "Copper Dragon, Juvenile";
        public const string Dragon_Copper_YoungAdult = "Copper Dragon, Young Adult";
        public const string Dragon_Copper_Adult = "Copper Dragon, Adult";
        public const string Dragon_Copper_MatureAdult = "Copper Dragon, Mature Adult";
        public const string Dragon_Copper_Old = "Copper Dragon, Old";
        public const string Dragon_Copper_VeryOld = "Copper Dragon, Very Old";
        public const string Dragon_Copper_Ancient = "Copper Dragon, Ancient";
        public const string Dragon_Copper_Wyrm = "Copper Dragon, Wyrm";
        public const string Dragon_Copper_GreatWyrm = "Copper Dragon, Great Wyrm";
        public const string Dragon_Gold_Wyrmling = "Gold Dragon, Wyrmling";
        public const string Dragon_Gold_VeryYoung = "Gold Dragon, Very Young";
        public const string Dragon_Gold_Young = "Gold Dragon, Young";
        public const string Dragon_Gold_Juvenile = "Gold Dragon, Juvenile";
        public const string Dragon_Gold_YoungAdult = "Gold Dragon, Young Adult";
        public const string Dragon_Gold_Adult = "Gold Dragon, Adult";
        public const string Dragon_Gold_MatureAdult = "Gold Dragon, Mature Adult";
        public const string Dragon_Gold_Old = "Gold Dragon, Old";
        public const string Dragon_Gold_VeryOld = "Gold Dragon, Very Old";
        public const string Dragon_Gold_Ancient = "Gold Dragon, Ancient";
        public const string Dragon_Gold_Wyrm = "Gold Dragon, Wyrm";
        public const string Dragon_Gold_GreatWyrm = "Gold Dragon, Great Wyrm";
        public const string Dragon_Green_Wyrmling = "Green Dragon, Wyrmling";
        public const string Dragon_Green_VeryYoung = "Green Dragon, Very Young";
        public const string Dragon_Green_Young = "Green Dragon, Young";
        public const string Dragon_Green_Juvenile = "Green Dragon, Juvenile";
        public const string Dragon_Green_YoungAdult = "Green Dragon, Young Adult";
        public const string Dragon_Green_Adult = "Green Dragon, Adult";
        public const string Dragon_Green_MatureAdult = "Green Dragon, Mature Adult";
        public const string Dragon_Green_Old = "Green Dragon, Old";
        public const string Dragon_Green_VeryOld = "Green Dragon, Very Old";
        public const string Dragon_Green_Ancient = "Green Dragon, Ancient";
        public const string Dragon_Green_Wyrm = "Green Dragon, Wyrm";
        public const string Dragon_Green_GreatWyrm = "Green Dragon, Great Wyrm";
        public const string Dragon_Red_Wyrmling = "Red Dragon, Wyrmling";
        public const string Dragon_Red_VeryYoung = "Red Dragon, Very Young";
        public const string Dragon_Red_Young = "Red Dragon, Young";
        public const string Dragon_Red_Juvenile = "Red Dragon, Juvenile";
        public const string Dragon_Red_YoungAdult = "Red Dragon, Young Adult";
        public const string Dragon_Red_Adult = "Red Dragon, Adult";
        public const string Dragon_Red_MatureAdult = "Red Dragon, Mature Adult";
        public const string Dragon_Red_Old = "Red Dragon, Old";
        public const string Dragon_Red_VeryOld = "Red Dragon, Very Old";
        public const string Dragon_Red_Ancient = "Red Dragon, Ancient";
        public const string Dragon_Red_Wyrm = "Red Dragon, Wyrm";
        public const string Dragon_Red_GreatWyrm = "Red Dragon, Great Wyrm";
        public const string Dragon_Silver_Wyrmling = "Silver Dragon, Wyrmling";
        public const string Dragon_Silver_VeryYoung = "Silver Dragon, Very Young";
        public const string Dragon_Silver_Young = "Silver Dragon, Young";
        public const string Dragon_Silver_Juvenile = "Silver Dragon, Juvenile";
        public const string Dragon_Silver_YoungAdult = "Silver Dragon, Young Adult";
        public const string Dragon_Silver_Adult = "Silver Dragon, Adult";
        public const string Dragon_Silver_MatureAdult = "Silver Dragon, Mature Adult";
        public const string Dragon_Silver_Old = "Silver Dragon, Old";
        public const string Dragon_Silver_VeryOld = "Silver Dragon, Very Old";
        public const string Dragon_Silver_Ancient = "Silver Dragon, Ancient";
        public const string Dragon_Silver_Wyrm = "Silver Dragon, Wyrm";
        public const string Dragon_Silver_GreatWyrm = "Silver Dragon, Great Wyrm";
        public const string Dragon_White_Wyrmling = "White Dragon, Wyrmling";
        public const string Dragon_White_VeryYoung = "White Dragon, Very Young";
        public const string Dragon_White_Young = "White Dragon, Young";
        public const string Dragon_White_Juvenile = "White Dragon, Juvenile";
        public const string Dragon_White_YoungAdult = "White Dragon, Young Adult";
        public const string Dragon_White_Adult = "White Dragon, Adult";
        public const string Dragon_White_MatureAdult = "White Dragon, Mature Adult";
        public const string Dragon_White_Old = "White Dragon, Old";
        public const string Dragon_White_VeryOld = "White Dragon, Very Old";
        public const string Dragon_White_Ancient = "White Dragon, Ancient";
        public const string Dragon_White_Wyrm = "White Dragon, Wyrm";
        public const string Dragon_White_GreatWyrm = "White Dragon, Great Wyrm";
        public const string DragonTurtle = "Dragon Turtle";
        public const string Dragonne = "Dragonne";
        public const string Dretch = "Dretch";
        public const string Drider = "Drider";
        public const string Elf_Drow = "Drow";
        public const string Dryad = "Dryad";
        public const string Dwarf_Duergar = "Duergar";
        public const string Dwarf_Deep = "Deep Dwarf";
        public const string Dwarf_Hill = "Hill Dwarf";
        public const string Dwarf_Mountain = "Mountain Dwarf";
        public const string Eagle = "Eagle";
        public const string Eagle_Giant = "Giant Eagle";
        public const string Efreeti = "Efreeti";
        public const string Elasmosaurus = "Elasmosaurus";
        public const string Elemental_Air_Elder = "Air Elemental, Elder";
        public const string Elemental_Air_Greater = "Air Elemental, Greater";
        public const string Elemental_Air_Huge = "Air Elemental, Huge";
        public const string Elemental_Air_Large = "Air Elemental, Large";
        public const string Elemental_Air_Medium = "Air Elemental, Medium";
        public const string Elemental_Air_Small = "Air Elemental, Small";
        public const string Elemental_Earth_Elder = "Earth Elemental, Elder";
        public const string Elemental_Earth_Greater = "Earth Elemental, Greater";
        public const string Elemental_Earth_Huge = "Earth Elemental, Huge";
        public const string Elemental_Earth_Large = "Earth Elemental, Large";
        public const string Elemental_Earth_Medium = "Earth Elemental, Medium";
        public const string Elemental_Earth_Small = "Earth Elemental, Small";
        public const string Elemental_Fire_Elder = "Fire Elemental, Elder";
        public const string Elemental_Fire_Greater = "Fire Elemental, Greater";
        public const string Elemental_Fire_Huge = "Fire Elemental, Huge";
        public const string Elemental_Fire_Large = "Fire Elemental, Large";
        public const string Elemental_Fire_Medium = "Fire Elemental, Medium";
        public const string Elemental_Fire_Small = "Fire Elemental, Small";
        public const string Elemental_Water_Elder = "Water Elemental, Elder";
        public const string Elemental_Water_Greater = "Water Elemental, Greater";
        public const string Elemental_Water_Huge = "Water Elemental, Huge";
        public const string Elemental_Water_Large = "Water Elemental, Large";
        public const string Elemental_Water_Medium = "Water Elemental, Medium";
        public const string Elemental_Water_Small = "Water Elemental, Small";
        public const string Elephant = "Elephant";
        public const string Elf_Aquatic = "Aquatic Elf";
        public const string Elf_Gray = "Gray Elf";
        public const string Elf_Half = "Half-Elf";
        public const string Elf_High = "High Elf";
        public const string Elf_Wild = "Wild Elf";
        public const string Elf_Wood = "Wood Elf";
        public const string Erinyes = "Erinyes";
        public const string EtherealFilcher = "Ethereal Filcher";
        public const string EtherealMarauder = "Ethereal Marauder";
        public const string Ettercap = "Ettercap";
        public const string Ettin = "Ettin";
        public const string FireBeetle_Giant = "Giant Fire Beetle";
        public const string FormianMyrmarch = "Formian Myrmarch";
        public const string FormianQueen = "Formian Queen";
        public const string FormianTaskmaster = "Formian Taskmaster";
        public const string FormianWarrior = "Formian Warrior";
        public const string FormianWorker = "Formian Worker";
        public const string Kolyarut = "Kolyarut";
        public const string Marut = "Marut";
        public const string Zelekhut = "Zelekhut";
        public const string FrostWorm = "Frost Worm";
        public const string Gargoyle = "Gargoyle";
        public const string Gargoyle_Kapoacinth = "Kapoacinth";
        public const string GelatinousCube = "Gelatinous Cube";
        public const string Ghaele = "Ghaele";
        public const string Ghoul = "Ghoul";
        public const string Ghoul_Ghast = "Ghast";
        public const string Ghoul_Lacedon = "Lacedon";
        public const string Giant_Cloud = "Cloud Giant";
        public const string Giant_Fire = "Fire Giant";
        public const string Giant_Frost = "Frost Giant";
        public const string Giant_Hill = "Hill Giant";
        public const string Giant_Stone = "Stone Giant";
        public const string Giant_Stone_Elder = "Stone Giant Elder";
        public const string Giant_Storm = "Storm Giant";
        public const string GibberingMouther = "Gibbering Mouther";
        public const string Girallon = "Girallon";
        public const string Githyanki = "Githyanki";
        public const string Githzerai = "Githzerai";
        public const string Glabrezu = "Glabrezu";
        public const string Gnoll = "Gnoll";
        public const string Gnome_Forest = "Forest Gnome";
        public const string Gnome_Rock = "Rock Gnome";
        public const string Goblin = "Goblin";
        public const string Golem_Clay = "Clay Golem";
        public const string Golem_Flesh = "Flesh Golem";
        public const string Golem_Iron = "Iron Golem";
        public const string Golem_Stone = "Stone Golem";
        public const string Golem_Stone_Greater = "Greater Stone Golem";
        public const string Gorgon = "Gorgon";
        public const string GrayRender = "Gray Render";
        public const string GreenHag = "Green Hag";
        public const string Grick = "Grick";
        public const string Griffon = "Griffon";
        public const string Grig = "Grig";
        public const string Grig_WithFiddle = "Grig with fiddle";
        public const string Grimlock = "Grimlock";
        public const string Gynosphinx = "Gynosphinx";
        public const string Halfling_Deep = "Deep Halfling";
        public const string Halfling_Lightfoot = "Lightfoot Halfling";
        public const string Halfling_Tallfellow = "Tallfellow Halfling";
        public const string Harpy = "Harpy";
        public const string Hawk = "Hawk";
        public const string Hellcat_Bezekira = "Hellcat (Bezekira)";
        public const string HellHound = "Hell Hound";
        public const string Hellwasp_Swarm = "Hellwasp Swarm";
        public const string Hezrou = "Hezrou";
        public const string Hieracosphinx = "Hieracosphinx";
        public const string Hippogriff = "Hippogriff";
        public const string Hobgoblin = "Hobgoblin";
        public const string Homunculus = "Homunculus";
        public const string HornedDevil_Cornugon = "Horned Devil (Cornugon)";
        public const string Horse_Heavy = "Heavy Horse";
        public const string Horse_Heavy_War = "Heavy Warhorse";
        public const string Horse_Light = "Light Horse";
        public const string Horse_Light_War = "Light Warhorse";
        public const string HoundArchon = "Hound Archon";
        public const string Howler = "Howler";
        public const string Human = "Human";
        public const string Hydra_5Heads = "Five-Headed Hydra";
        public const string Hydra_6Heads = "Six-Headed Hydra";
        public const string Hydra_7Heads = "Seven-Headed Hydra";
        public const string Hydra_8Heads = "Eight-Headed Hydra";
        public const string Hydra_9Heads = "Nine-Headed Hydra";
        public const string Hydra_10Heads = "Ten-Headed Hydra";
        public const string Hydra_11Heads = "Eleven-Headed Hydra";
        public const string Hydra_12Heads = "Twelve-Headed Hydra";
        public const string IceDevil_Gelugon = "Ice Devil (Gelugon)";
        public const string Imp = "Imp";
        public const string InvisibleStalker = "Invisible Stalker";
        public const string Janni = "Janni";
        public const string Kobold = "Kobold";
        public const string Kraken = "Kraken";
        public const string Krenshar = "Krenshar";
        public const string KuoToa = "Kuo-toa";
        public const string Lamia = "Lamia";
        public const string Lammasu = "Lammasu";
        public const string LanternArchon = "Lantern Archon";
        public const string Lemure = "Lemure";
        public const string Leonal = "Leonal";
        public const string Leopard = "Leopard";
        public const string Lillend = "Lillend";
        public const string Lion = "Lion";
        public const string Lion_Dire = "Dire Lion";
        public const string Lizard = "Lizard";
        public const string Lizard_Monitor = "Monitor Lizard";
        public const string Lizardfolk = "Lizardfolk";
        public const string Locathah = "Locathah";
        public const string Locust_Swarm = "Locust Swarm";
        public const string Magmin = "Magmin";
        public const string MantaRay = "Manta Ray";
        public const string Manticore = "Manticore";
        public const string Marilith = "Marilith";
        public const string Medusa = "Medusa";
        public const string Megaraptor = "Megaraptor";
        public const string Mephit_Air = "Air Mephit";
        public const string Mephit_Dust = "Dust Mephit";
        public const string Mephit_Earth = "Earth Mephit";
        public const string Mephit_Fire = "Fire Mephit";
        public const string Mephit_Ice = "Ice Mephit";
        public const string Mephit_Magma = "Magma Mephit";
        public const string Mephit_Ooze = "Ooze Mephit";
        public const string Mephit_Salt = "Salt Mephit";
        public const string Mephit_Steam = "Steam Mephit";
        public const string Mephit_Water = "Water Mephit";
        public const string Merfolk = "Merfolk";
        public const string Mimic = "Mimic";
        public const string MindFlayer = "Mind Flayer (Illithid)";
        public const string Minotaur = "Minotaur";
        public const string Mohrg = "Mohrg";
        public const string Monkey = "Monkey";
        public const string Mule = "Mule";
        public const string Mummy = "Mummy";
        public const string Naga_Dark = "Dark Naga";
        public const string Naga_Guardian = "Guardian Naga";
        public const string Naga_Spirit = "Spirit Naga";
        public const string Naga_Water = "Water Naga";
        public const string Nalfeshnee = "Nalfeshnee";
        public const string HellHound_NessianWarhound = "Nessian Warhound";
        public const string Nightcrawler = "Nightcrawler";
        public const string NightHag = "Night Hag";
        public const string Nightmare = "Nightmare";
        public const string Nightmare_Cauchemar = "Nightmare, Cauchemar";
        public const string Nightwalker = "Nightwalker";
        public const string Nightwing = "Nightwing";
        public const string Nixie = "Nixie";
        public const string Nymph = "Nymph";
        public const string Octopus = "Octopus";
        public const string Octopus_Giant = "Giant Octopus";
        public const string Ogre = "Ogre";
        public const string Ogre_Merrow = "Merrow";
        public const string OgreMage = "Ogre Mage";
        public const string GrayOoze = "Gray Ooze";
        public const string OchreJelly = "Ochre Jelly";
        public const string Orc = "Orc";
        public const string Orc_Half = "Half-Orc";
        public const string Otyugh = "Otyugh";
        public const string Owl = "Owl";
        public const string Owl_Giant = "Giant Owl";
        public const string Owlbear = "Owlbear";
        public const string Pegasus = "Pegasus";
        public const string PhantomFungus = "Phantom Fungus";
        public const string PhaseSpider = "Phase Spider";
        public const string Phasm = "Phasm";
        public const string PitFiend = "Pit Fiend";
        public const string Pixie = "Pixie";
        public const string Pixie_WithIrresistibleDance = "Pixie with Irresistible dance";
        public const string Angel_Planetar = "Angel, Planetar";
        public const string Pony = "Pony";
        public const string Pony_War = "Warpony";
        public const string Porpoise = "Porpoise";
        public const string PrayingMantis_Giant = "Giant Praying Mantis";
        public const string Pseudodragon = "Pseudodragon";
        public const string PurpleWorm = "Purple Worm";
        public const string Pyrohydra_5Heads = "Five-Headed Pyrohydra";
        public const string Pyrohydra_6Heads = "Six-Headed Pyrohydra";
        public const string Pyrohydra_7Heads = "Seven-Headed Pyrohydra";
        public const string Pyrohydra_8Heads = "Eight-Headed Pyrohydra";
        public const string Pyrohydra_9Heads = "Nine-Headed Pyrohydra";
        public const string Pyrohydra_10Heads = "Ten-Headed Pyrohydra";
        public const string Pyrohydra_11Heads = "Eleven-Headed Pyrohydra";
        public const string Pyrohydra_12Heads = "Twelve-Headed Pyrohydra";
        public const string Quasit = "Quasit";
        public const string Rakshasa = "Rakshasa";
        public const string Rast = "Rast";
        public const string Rat = "Rat";
        public const string Rat_Dire = "Dire Rat";
        public const string Rat_Swarm = "Rat Swarm";
        public const string Raven = "Raven";
        public const string Ravid = "Ravid";
        public const string RazorBoar = "Razor Boar";
        public const string Remorhaz = "Remorhaz";
        public const string Retriever = "Retriever";
        public const string Rhinoceras = "Rhinoceras";
        public const string Roc = "Roc";
        public const string Roper = "Roper";
        public const string RustMonster = "Rust Monster";
        public const string Sahuagin = "Sahuagin";
        public const string Sahuagin_Mutant = "Sahuagin Mutant";
        public const string Sahuagin_Malenti = "Malenti";
        public const string Satyr = "Satyr";
        public const string Satyr_WithPipes = "Satyr with pipes";
        public const string Salamander_Average = "Average Salamander";
        public const string Salamander_Flamebrother = "Flamebrother Salamander";
        public const string Salamander_Noble = "Noble Salamander";
        public const string Scorpion_Monstrous_Colossal = "Monstrous Scorpion, Colossal";
        public const string Scorpion_Monstrous_Gargantuan = "Monstrous Scorpion, Gargantuan";
        public const string Scorpion_Monstrous_Huge = "Monstrous Scorpion, Huge";
        public const string Scorpion_Monstrous_Large = "Monstrous Scorpion, Large";
        public const string Scorpion_Monstrous_Medium = "Monstrous Scorpion, Medium";
        public const string Scorpion_Monstrous_Small = "Monstrous Scorpion, Small";
        public const string Scorpion_Monstrous_Tiny = "Monstrous Scorpion, Tiny";
        public const string Scorpionfolk = "Scorpionfolk";
        public const string SeaCat = "Sea Cat";
        public const string SeaHag = "Sea Hag";
        public const string Shadow = "Shadow";
        public const string Shadow_Greater = "Greater Shadow";
        public const string ShadowMastiff = "Shadow Mastiff";
        public const string Shark_Dire = "Dire Shark";
        public const string Shark_Medium = "Shark, Medium";
        public const string Shark_Large = "Shark, Large";
        public const string Shark_Huge = "Shark, Huge";
        public const string ShamblingMound = "Shambling Mound";
        public const string ShieldGuardian = "Shield Guardian";
        public const string ShockerLizard = "Shocker Lizard";
        public const string Shrieker = "Shrieker";
        public const string Skum = "Skum";
        public const string Slaad_Blue = "Blue Slaad";
        public const string Slaad_Death = "Death Slaad";
        public const string Slaad_Gray = "Gray Slaad";
        public const string Slaad_Green = "Green Slaad";
        public const string Slaad_Red = "Red Slaad";
        public const string Snake_Constrictor = "Constrictor Snake";
        public const string Snake_Constrictor_Giant = "Giant Constrictor Snake";
        public const string Snake_Viper_Huge = "Viper Snake, Huge";
        public const string Snake_Viper_Large = "Viper Snake, Large";
        public const string Snake_Viper_Medium = "Viper Snake, Medium";
        public const string Snake_Viper_Small = "Viper Snake, Small";
        public const string Snake_Viper_Tiny = "Viper Snake, Tiny";
        public const string Angel_Solar = "Angel, Solar";
        public const string Spectre = "Spectre";
        public const string Spider_Monstrous_Hunter_Colossal = "Monstrous Spider, Hunter, Colossal";
        public const string Spider_Monstrous_Hunter_Gargantuan = "Monstrous Spider, Hunter, Gargantuan";
        public const string Spider_Monstrous_Hunter_Huge = "Monstrous Spider, Hunter, Huge";
        public const string Spider_Monstrous_Hunter_Large = "Monstrous Spider, Hunter, Large";
        public const string Spider_Monstrous_Hunter_Medium = "Monstrous Spider, Hunter, Medium";
        public const string Spider_Monstrous_Hunter_Small = "Monstrous Spider, Hunter, Small";
        public const string Spider_Monstrous_Hunter_Tiny = "Monstrous Spider, Hunter, Tiny";
        public const string Spider_Monstrous_WebSpinner_Colossal = "Monstrous Spider, Web-Spinner, Colossal";
        public const string Spider_Monstrous_WebSpinner_Gargantuan = "Monstrous Spider, Web-Spinner, Gargantuan";
        public const string Spider_Monstrous_WebSpinner_Huge = "Monstrous Spider, Web-Spinner, Huge";
        public const string Spider_Monstrous_WebSpinner_Large = "Monstrous Spider, Web-Spinner, Large";
        public const string Spider_Monstrous_WebSpinner_Medium = "Monstrous Spider, Web-Spinner, Medium";
        public const string Spider_Monstrous_WebSpinner_Small = "Monstrous Spider, Web-Spinner, Small";
        public const string Spider_Monstrous_WebSpinner_Tiny = "Monstrous Spider, Web-Spinner, Tiny";
        public const string Spider_Swarm = "Spider Swarm";
        public const string SpiderEater = "Spider Eater";
        public const string Squid = "Squid";
        public const string Squid_Giant = "Giant Squid";
        public const string StagBeetle_Giant = "Giant Stag Beetle";
        public const string Stirge = "Stirge";
        public const string Succubus = "Succubus";
        public const string Gnome_Svirfneblin = "Svirfneblin";
        public const string Tarrasque = "Tarrasque";
        public const string Tendriculos = "Tendriculos";
        public const string Thoqqua = "Thoqqua";
        public const string Tiefling = "Tiefling";
        public const string Tiger = "Tiger";
        public const string Tiger_Dire = "Dire Tiger";
        public const string Titan = "Titan";
        public const string Toad = "Toad";
        public const string Tojanida_Adult = "Adult Tojanida";
        public const string Tojanida_Elder = "Elder Tojanida";
        public const string Tojanida_Juvenile = "Juvenile Tojanida";
        public const string Treant = "Treant";
        public const string Triceratops = "Triceratops";
        public const string Triton = "Triton";
        public const string Troglodyte = "Troglodyte";
        public const string Troll = "Troll";
        public const string Troll_Scrag = "Scrag";
        public const string TrumpetArchon = "Trumpet Archon";
        public const string Tyrannosaurus = "Tyrannosaurus";
        public const string UmberHulk = "Umber Hulk";
        public const string UmberHulk_TrulyHorrid = "Truly Horrid Umber Hulk";
        public const string Unicorn = "Unicorn";
        public const string VampireSpawn = "Vampire Spawn";
        public const string Vargouille = "Vargouille";
        public const string VioletFungus = "Violet Fungus";
        public const string Vrock = "Vrock";
        public const string Wasp_Giant = "Giant Wasp";
        public const string Weasel = "Weasel";
        public const string Weasel_Dire = "Dire Weasel";
        public const string Whale_Baleen = "Baleen Whale";
        public const string Whale_Cachalot = "Cachalot Whale";
        public const string Whale_Orca = "Orca Whale";
        public const string Wight = "Wight";
        public const string WillOWisp = "Will-O'-Wisp";
        public const string WinterWolf = "Winter Wolf";
        public const string Wolverine = "Wolverine";
        public const string Wolverine_Dire = "Dire Wolverine";
        public const string Wolf = "Wolf";
        public const string Wolf_Dire = "Dire Wolf";
        public const string Worg = "Worg";
        public const string Wraith = "Wraith";
        public const string Wraith_Dread = "Dread Wraith";
        public const string Wyvern = "Wyvern";
        public const string Xill = "Xill";
        public const string Xorn_Average = "Average Xorn";
        public const string Xorn_Elder = "Elder Xorn";
        public const string Xorn_Minor = "Minor Xorn";
        public const string YethHound = "Yeth Hound";
        public const string Yrthak = "Yrthak";
        public const string YuanTi_Abomination = "Yuan-ti Abomination";
        public const string YuanTi_Halfblood_SnakeHead = "Yuan-ti Halfblood (Snake Head)";
        public const string YuanTi_Halfblood_SnakeArms = "Yuan-ti Halfblood (Human Head, Arms are Snakes)";
        public const string YuanTi_Halfblood_SnakeTailAndHumanLegs = "Yuan-ti Halfblood (Snake Head, Snake Tail and Human Legs)";
        public const string YuanTi_Halfblood_SnakeTail = "Yuan-ti Halfblood (Snake Head, Snake Tail instead of Human Legs)";
        public const string YuanTi_Pureblood = "Yuan-ti Pureblood";

        public static IEnumerable<string> GetAll()
        {
            return new[]
            {
                Aasimar,
                Aboleth,
                Achaierai,
                Allip,
                Androsphinx,
                AnimatedObject_Colossal,
                AnimatedObject_Colossal_Flexible,
                AnimatedObject_Colossal_MultipleLegs,
                AnimatedObject_Colossal_MultipleLegs_Wooden,
                AnimatedObject_Colossal_Sheetlike,
                AnimatedObject_Colossal_TwoLegs,
                AnimatedObject_Colossal_TwoLegs_Wooden,
                AnimatedObject_Colossal_Wheels_Wooden,
                AnimatedObject_Colossal_Wooden,
                AnimatedObject_Gargantuan,
                AnimatedObject_Gargantuan_Flexible,
                AnimatedObject_Gargantuan_MultipleLegs,
                AnimatedObject_Gargantuan_MultipleLegs_Wooden,
                AnimatedObject_Gargantuan_Sheetlike,
                AnimatedObject_Gargantuan_TwoLegs,
                AnimatedObject_Gargantuan_TwoLegs_Wooden,
                AnimatedObject_Gargantuan_Wheels_Wooden,
                AnimatedObject_Gargantuan_Wooden,
                AnimatedObject_Huge,
                AnimatedObject_Huge_Flexible,
                AnimatedObject_Huge_MultipleLegs,
                AnimatedObject_Huge_MultipleLegs_Wooden,
                AnimatedObject_Huge_Sheetlike,
                AnimatedObject_Huge_TwoLegs,
                AnimatedObject_Huge_TwoLegs_Wooden,
                AnimatedObject_Huge_Wheels_Wooden,
                AnimatedObject_Huge_Wooden,
                AnimatedObject_Large,
                AnimatedObject_Large_Flexible,
                AnimatedObject_Large_MultipleLegs,
                AnimatedObject_Large_MultipleLegs_Wooden,
                AnimatedObject_Large_Sheetlike,
                AnimatedObject_Large_TwoLegs,
                AnimatedObject_Large_TwoLegs_Wooden,
                AnimatedObject_Large_Wheels_Wooden,
                AnimatedObject_Large_Wooden,
                AnimatedObject_Medium,
                AnimatedObject_Medium_Flexible,
                AnimatedObject_Medium_MultipleLegs,
                AnimatedObject_Medium_MultipleLegs_Wooden,
                AnimatedObject_Medium_Sheetlike,
                AnimatedObject_Medium_TwoLegs,
                AnimatedObject_Medium_TwoLegs_Wooden,
                AnimatedObject_Medium_Wheels_Wooden,
                AnimatedObject_Medium_Wooden,
                AnimatedObject_Small,
                AnimatedObject_Small_Flexible,
                AnimatedObject_Small_MultipleLegs,
                AnimatedObject_Small_MultipleLegs_Wooden,
                AnimatedObject_Small_Sheetlike,
                AnimatedObject_Small_TwoLegs,
                AnimatedObject_Small_TwoLegs_Wooden,
                AnimatedObject_Small_Wheels_Wooden,
                AnimatedObject_Small_Wooden,
                AnimatedObject_Tiny,
                AnimatedObject_Tiny_Flexible,
                AnimatedObject_Tiny_MultipleLegs,
                AnimatedObject_Tiny_MultipleLegs_Wooden,
                AnimatedObject_Tiny_Sheetlike,
                AnimatedObject_Tiny_TwoLegs,
                AnimatedObject_Tiny_TwoLegs_Wooden,
                AnimatedObject_Tiny_Wheels_Wooden,
                AnimatedObject_Tiny_Wooden,
                Ankheg,
                Annis,
                Ant_Giant_Queen,
                Ant_Giant_Soldier,
                Ant_Giant_Worker,
                Ape,
                Ape_Dire,
                Aranea,
                Arrowhawk_Adult,
                Arrowhawk_Elder,
                Arrowhawk_Juvenile,
                AssassinVine,
                Angel_AstralDeva,
                Athach,
                Avoral,
                Azer,
                Babau,
                Baboon,
                Badger,
                Badger_Dire,
                Balor,
                BarbedDevil_Hamatula,
                Barghest,
                Barghest_Greater,
                Basilisk,
                Basilisk_Greater,
                Bat,
                Bat_Dire,
                Bat_Swarm,
                Bear_Black,
                Bear_Brown,
                Bear_Dire,
                Bear_Polar,
                BeardedDevil_Barbazu,
                Bebilith,
                Bee_Giant,
                Behir,
                Beholder,
                Beholder_Gauth,
                Belker,
                Bison,
                BlackPudding,
                BlackPudding_Elder,
                BlinkDog,
                Boar,
                Boar_Dire,
                Bodak,
                BombardierBeetle_Giant,
                BoneDevil_Osyluth,
                Bralani,
                Bugbear,
                Bulette,
                Camel_Bactrian,
                Camel_Dromedary,
                CarrionCrawler,
                Cat,
                Centaur,
                Centipede_Monstrous_Colossal,
                Centipede_Monstrous_Gargantuan,
                Centipede_Monstrous_Huge,
                Centipede_Monstrous_Large,
                Centipede_Monstrous_Medium,
                Centipede_Monstrous_Small,
                Centipede_Monstrous_Tiny,
                Centipede_Swarm,
                ChainDevil_Kyton,
                ChaosBeast,
                Cheetah,
                Chimera_Black,
                Chimera_Blue,
                Chimera_Green,
                Chimera_Red,
                Chimera_White,
                Choker,
                Chuul,
                Cloaker,
                Cockatrice,
                Couatl,
                Criosphinx,
                Crocodile,
                Crocodile_Giant,
                Cryohydra_5Heads,
                Cryohydra_6Heads,
                Cryohydra_7Heads,
                Cryohydra_8Heads,
                Cryohydra_9Heads,
                Cryohydra_10Heads,
                Cryohydra_11Heads,
                Cryohydra_12Heads,
                Darkmantle,
                Deinonychus,
                Delver,
                Derro,
                Derro_Sane,
                Destrachan,
                Devourer,
                Digester,
                DisplacerBeast,
                DisplacerBeast_PackLord,
                Djinni,
                Djinni_Noble,
                Dog,
                Dog_Riding,
                Hyena,
                Donkey,
                Doppelganger,
                Dragon_Black_Wyrmling,
                Dragon_Black_VeryYoung,
                Dragon_Black_Young,
                Dragon_Black_Juvenile,
                Dragon_Black_YoungAdult,
                Dragon_Black_Adult,
                Dragon_Black_MatureAdult,
                Dragon_Black_Old,
                Dragon_Black_VeryOld,
                Dragon_Black_Ancient,
                Dragon_Black_Wyrm,
                Dragon_Black_GreatWyrm,
                Dragon_Blue_Wyrmling,
                Dragon_Blue_VeryYoung,
                Dragon_Blue_Young,
                Dragon_Blue_Juvenile,
                Dragon_Blue_YoungAdult,
                Dragon_Blue_Adult,
                Dragon_Blue_MatureAdult,
                Dragon_Blue_Old,
                Dragon_Blue_VeryOld,
                Dragon_Blue_Ancient,
                Dragon_Blue_Wyrm,
                Dragon_Blue_GreatWyrm,
                Dragon_Brass_Wyrmling,
                Dragon_Brass_VeryYoung,
                Dragon_Brass_Young,
                Dragon_Brass_Juvenile,
                Dragon_Brass_YoungAdult,
                Dragon_Brass_Adult,
                Dragon_Brass_MatureAdult,
                Dragon_Brass_Old,
                Dragon_Brass_VeryOld,
                Dragon_Brass_Ancient,
                Dragon_Brass_Wyrm,
                Dragon_Brass_GreatWyrm,
                Dragon_Bronze_Wyrmling,
                Dragon_Bronze_VeryYoung,
                Dragon_Bronze_Young,
                Dragon_Bronze_Juvenile,
                Dragon_Bronze_YoungAdult,
                Dragon_Bronze_Adult,
                Dragon_Bronze_MatureAdult,
                Dragon_Bronze_Old,
                Dragon_Bronze_VeryOld,
                Dragon_Bronze_Ancient,
                Dragon_Bronze_Wyrm,
                Dragon_Bronze_GreatWyrm,
                Dragon_Copper_Wyrmling,
                Dragon_Copper_VeryYoung,
                Dragon_Copper_Young,
                Dragon_Copper_Juvenile,
                Dragon_Copper_YoungAdult,
                Dragon_Copper_Adult,
                Dragon_Copper_MatureAdult,
                Dragon_Copper_Old,
                Dragon_Copper_VeryOld,
                Dragon_Copper_Ancient,
                Dragon_Copper_Wyrm,
                Dragon_Copper_GreatWyrm,
                Dragon_Gold_Wyrmling,
                Dragon_Gold_VeryYoung,
                Dragon_Gold_Young,
                Dragon_Gold_Juvenile,
                Dragon_Gold_YoungAdult,
                Dragon_Gold_Adult,
                Dragon_Gold_MatureAdult,
                Dragon_Gold_Old,
                Dragon_Gold_VeryOld,
                Dragon_Gold_Ancient,
                Dragon_Gold_Wyrm,
                Dragon_Gold_GreatWyrm,
                Dragon_Green_Wyrmling,
                Dragon_Green_VeryYoung,
                Dragon_Green_Young,
                Dragon_Green_Juvenile,
                Dragon_Green_YoungAdult,
                Dragon_Green_Adult,
                Dragon_Green_MatureAdult,
                Dragon_Green_Old,
                Dragon_Green_VeryOld,
                Dragon_Green_Ancient,
                Dragon_Green_Wyrm,
                Dragon_Green_GreatWyrm,
                Dragon_Red_Wyrmling,
                Dragon_Red_VeryYoung,
                Dragon_Red_Young,
                Dragon_Red_Juvenile,
                Dragon_Red_YoungAdult,
                Dragon_Red_Adult,
                Dragon_Red_MatureAdult,
                Dragon_Red_Old,
                Dragon_Red_VeryOld,
                Dragon_Red_Ancient,
                Dragon_Red_Wyrm,
                Dragon_Red_GreatWyrm,
                Dragon_Silver_Wyrmling,
                Dragon_Silver_VeryYoung,
                Dragon_Silver_Young,
                Dragon_Silver_Juvenile,
                Dragon_Silver_YoungAdult,
                Dragon_Silver_Adult,
                Dragon_Silver_MatureAdult,
                Dragon_Silver_Old,
                Dragon_Silver_VeryOld,
                Dragon_Silver_Ancient,
                Dragon_Silver_Wyrm,
                Dragon_Silver_GreatWyrm,
                Dragon_White_Wyrmling,
                Dragon_White_VeryYoung,
                Dragon_White_Young,
                Dragon_White_Juvenile,
                Dragon_White_YoungAdult,
                Dragon_White_Adult,
                Dragon_White_MatureAdult,
                Dragon_White_Old,
                Dragon_White_VeryOld,
                Dragon_White_Ancient,
                Dragon_White_Wyrm,
                Dragon_White_GreatWyrm,
                DragonTurtle,
                Dragonne,
                Dretch,
                Drider,
                Elf_Drow,
                Dryad,
                Dwarf_Duergar,
                Dwarf_Deep,
                Dwarf_Hill,
                Dwarf_Mountain,
                Eagle,
                Eagle_Giant,
                Efreeti,
                Elasmosaurus,
                Elemental_Air_Elder,
                Elemental_Air_Greater,
                Elemental_Air_Huge,
                Elemental_Air_Large,
                Elemental_Air_Medium,
                Elemental_Air_Small,
                Elemental_Earth_Elder,
                Elemental_Earth_Greater,
                Elemental_Earth_Huge,
                Elemental_Earth_Large,
                Elemental_Earth_Medium,
                Elemental_Earth_Small,
                Elemental_Fire_Elder,
                Elemental_Fire_Greater,
                Elemental_Fire_Huge,
                Elemental_Fire_Large,
                Elemental_Fire_Medium,
                Elemental_Fire_Small,
                Elemental_Water_Elder,
                Elemental_Water_Greater,
                Elemental_Water_Huge,
                Elemental_Water_Large,
                Elemental_Water_Medium,
                Elemental_Water_Small,
                Elephant,
                Elf_Aquatic,
                Elf_Gray,
                Elf_Half,
                Elf_High,
                Elf_Wild,
                Elf_Wood,
                Erinyes,
                EtherealFilcher,
                EtherealMarauder,
                Ettercap,
                Ettin,
                FireBeetle_Giant,
                FormianMyrmarch,
                FormianQueen,
                FormianTaskmaster,
                FormianWarrior,
                FormianWorker,
                Kolyarut,
                Marut,
                Zelekhut,
                FrostWorm,
                Gargoyle,
                Gargoyle_Kapoacinth,
                GelatinousCube,
                Ghaele,
                Ghoul,
                Ghoul_Ghast,
                Ghoul_Lacedon,
                Giant_Cloud,
                Giant_Fire,
                Giant_Frost,
                Giant_Hill,
                Giant_Stone,
                Giant_Stone_Elder,
                Giant_Storm,
                GibberingMouther,
                Girallon,
                Githyanki,
                Githzerai,
                Glabrezu,
                Gnoll,
                Gnome_Forest,
                Gnome_Rock,
                Goblin,
                Golem_Clay,
                Golem_Flesh,
                Golem_Iron,
                Golem_Stone,
                Golem_Stone_Greater,
                Gorgon,
                GrayRender,
                GreenHag,
                Grick,
                Griffon,
                Grig,
                Grig_WithFiddle,
                Grimlock,
                Gynosphinx,
                Halfling_Deep,
                Halfling_Lightfoot,
                Halfling_Tallfellow,
                Harpy,
                Hawk,
                Hellcat_Bezekira,
                HellHound,
                Hellwasp_Swarm,
                Hezrou,
                Hieracosphinx,
                Hippogriff,
                Hobgoblin,
                Homunculus,
                HornedDevil_Cornugon,
                Horse_Heavy,
                Horse_Heavy_War,
                Horse_Light,
                Horse_Light_War,
                HoundArchon,
                Howler,
                Human,
                Hydra_5Heads,
                Hydra_6Heads,
                Hydra_7Heads,
                Hydra_8Heads,
                Hydra_9Heads,
                Hydra_10Heads,
                Hydra_11Heads,
                Hydra_12Heads,
                IceDevil_Gelugon,
                Imp,
                InvisibleStalker,
                Janni,
                Kobold,
                Kraken,
                Krenshar,
                KuoToa,
                Lamia,
                Lammasu,
                LanternArchon,
                Lemure,
                Leonal,
                Leopard,
                Lillend,
                Lion,
                Lion_Dire,
                Lizard,
                Lizard_Monitor,
                Lizardfolk,
                Locathah,
                Locust_Swarm,
                Magmin,
                MantaRay,
                Manticore,
                Marilith,
                Medusa,
                Megaraptor,
                Mephit_Air,
                Mephit_Dust,
                Mephit_Earth,
                Mephit_Fire,
                Mephit_Ice,
                Mephit_Magma,
                Mephit_Ooze,
                Mephit_Salt,
                Mephit_Steam,
                Mephit_Water,
                Merfolk,
                Mimic,
                MindFlayer,
                Minotaur,
                Mohrg,
                Monkey,
                Mule,
                Mummy,
                Naga_Dark,
                Naga_Guardian,
                Naga_Spirit,
                Naga_Water,
                Nalfeshnee,
                HellHound_NessianWarhound,
                Nightcrawler,
                NightHag,
                Nightmare,
                Nightmare_Cauchemar,
                Nightwalker,
                Nightwing,
                Nixie,
                Nymph,
                Octopus,
                Octopus_Giant,
                Ogre,
                Ogre_Merrow,
                OgreMage,
                GrayOoze,
                OchreJelly,
                Orc,
                Orc_Half,
                Otyugh,
                Owl,
                Owl_Giant,
                Owlbear,
                Pegasus,
                PhantomFungus,
                PhaseSpider,
                Phasm,
                PitFiend,
                Pixie,
                Pixie_WithIrresistibleDance,
                Angel_Planetar,
                Pony,
                Pony_War,
                Porpoise,
                PrayingMantis_Giant,
                Pseudodragon,
                PurpleWorm,
                Pyrohydra_5Heads,
                Pyrohydra_6Heads,
                Pyrohydra_7Heads,
                Pyrohydra_8Heads,
                Pyrohydra_9Heads,
                Pyrohydra_10Heads,
                Pyrohydra_11Heads,
                Pyrohydra_12Heads,
                Quasit,
                Rakshasa,
                Rast,
                Rat,
                Rat_Dire,
                Rat_Swarm,
                Raven,
                Ravid,
                RazorBoar,
                Remorhaz,
                Retriever,
                Rhinoceras,
                Roc,
                Roper,
                RustMonster,
                Sahuagin,
                Sahuagin_Malenti,
                Sahuagin_Mutant,
                Satyr,
                Satyr_WithPipes,
                Salamander_Average,
                Salamander_Flamebrother,
                Salamander_Noble,
                Scorpion_Monstrous_Colossal,
                Scorpion_Monstrous_Gargantuan,
                Scorpion_Monstrous_Huge,
                Scorpion_Monstrous_Large,
                Scorpion_Monstrous_Medium,
                Scorpion_Monstrous_Small,
                Scorpion_Monstrous_Tiny,
                Scorpionfolk,
                SeaCat,
                SeaHag,
                Shadow,
                Shadow_Greater,
                ShadowMastiff,
                Shark_Dire,
                Shark_Medium,
                Shark_Large,
                Shark_Huge,
                ShamblingMound,
                ShieldGuardian,
                ShockerLizard,
                Shrieker,
                Skum,
                Slaad_Blue,
                Slaad_Death,
                Slaad_Gray,
                Slaad_Green,
                Slaad_Red,
                Snake_Constrictor,
                Snake_Constrictor_Giant,
                Snake_Viper_Huge,
                Snake_Viper_Large,
                Snake_Viper_Medium,
                Snake_Viper_Small,
                Snake_Viper_Tiny,
                Angel_Solar,
                Spectre,
                Spider_Monstrous_Hunter_Colossal,
                Spider_Monstrous_Hunter_Gargantuan,
                Spider_Monstrous_Hunter_Huge,
                Spider_Monstrous_Hunter_Large,
                Spider_Monstrous_Hunter_Medium,
                Spider_Monstrous_Hunter_Small,
                Spider_Monstrous_Hunter_Tiny,
                Spider_Monstrous_WebSpinner_Colossal,
                Spider_Monstrous_WebSpinner_Gargantuan,
                Spider_Monstrous_WebSpinner_Huge,
                Spider_Monstrous_WebSpinner_Large,
                Spider_Monstrous_WebSpinner_Medium,
                Spider_Monstrous_WebSpinner_Small,
                Spider_Monstrous_WebSpinner_Tiny,
                Spider_Swarm,
                SpiderEater,
                Squid,
                Squid_Giant,
                StagBeetle_Giant,
                Stirge,
                Succubus,
                Gnome_Svirfneblin,
                Tarrasque,
                Tendriculos,
                Thoqqua,
                Tiefling,
                Tiger,
                Tiger_Dire,
                Titan,
                Toad,
                Tojanida_Adult,
                Tojanida_Elder,
                Tojanida_Juvenile,
                Treant,
                Triceratops,
                Triton,
                Troglodyte,
                Troll,
                Troll_Scrag,
                TrumpetArchon,
                Tyrannosaurus,
                UmberHulk,
                UmberHulk_TrulyHorrid,
                Unicorn,
                VampireSpawn,
                Vargouille,
                VioletFungus,
                Vrock,
                Wasp_Giant,
                Weasel,
                Weasel_Dire,
                Whale_Baleen,
                Whale_Cachalot,
                Whale_Orca,
                Wight,
                WillOWisp,
                WinterWolf,
                Wolverine,
                Wolverine_Dire,
                Wolf,
                Wolf_Dire,
                Worg,
                Wraith,
                Wraith_Dread,
                Wyvern,
                Xill,
                Xorn_Average,
                Xorn_Elder,
                Xorn_Minor,
                YethHound,
                Yrthak,
                YuanTi_Abomination,
                YuanTi_Halfblood_SnakeArms,
                YuanTi_Halfblood_SnakeHead,
                YuanTi_Halfblood_SnakeTail,
                YuanTi_Halfblood_SnakeTailAndHumanLegs,
                YuanTi_Pureblood,
            };
        }

        public static IEnumerable<string> GetAllNonCharacters() => GetAll().Except(GetAllCharacters());

        public static IEnumerable<string> GetAllCharacters()
        {
            return
            [
                Aasimar,
                Aboleth,
                Androsphinx,
                Angel_AstralDeva,
                Annis,
                Aranea,
                Athach,
                Azer,
                BeardedDevil_Barbazu,
                BlinkDog,
                Bralani,
                Bugbear,
                Centaur,
                ChainDevil_Kyton,
                Chimera_Black,
                Chimera_Blue,
                Chimera_Green,
                Chimera_Red,
                Chimera_White,
                Couatl,
                Criosphinx,
                Derro,
                Derro_Sane,
                DisplacerBeast,
                Djinni,
                Doppelganger,
                Dragon_Black_Wyrmling,
                Dragon_Black_VeryYoung,
                Dragon_Black_Young,
                Dragon_Black_Juvenile,
                Dragon_Blue_Wyrmling,
                Dragon_Blue_VeryYoung,
                Dragon_Blue_Young,
                Dragon_Green_Wyrmling,
                Dragon_Green_VeryYoung,
                Dragon_Green_Young,
                Dragon_Green_Juvenile,
                Dragon_Red_Wyrmling,
                Dragon_Red_VeryYoung,
                Dragon_Red_Young,
                Dragon_White_Wyrmling,
                Dragon_White_VeryYoung,
                Dragon_White_Young,
                Dragon_White_Juvenile,
                Dragon_Brass_Wyrmling,
                Dragon_Brass_VeryYoung,
                Dragon_Brass_Young,
                Dragon_Brass_Juvenile,
                Dragon_Bronze_Wyrmling,
                Dragon_Bronze_VeryYoung,
                Dragon_Bronze_Young,
                Dragon_Copper_Wyrmling,
                Dragon_Copper_VeryYoung,
                Dragon_Copper_Young,
                Dragon_Copper_Juvenile,
                Dragon_Gold_Wyrmling,
                Dragon_Gold_VeryYoung,
                Dragon_Gold_Young,
                Dragon_Silver_Wyrmling,
                Dragon_Silver_VeryYoung,
                Dragon_Silver_Young,
                Dragonne,
                Dretch,
                Drider,
                Dryad,
                Dwarf_Deep,
                Dwarf_Duergar,
                Dwarf_Hill,
                Dwarf_Mountain,
                Eagle_Giant,
                Elf_Aquatic,
                Elf_Drow,
                Elf_Gray,
                Elf_Half,
                Elf_High,
                Elf_Wild,
                Elf_Wood,
                Erinyes,
                Ettercap,
                Ettin,
                Gargoyle,
                Gargoyle_Kapoacinth,
                Giant_Cloud,
                Giant_Fire,
                Giant_Frost,
                Giant_Hill,
                Giant_Stone,
                Giant_Stone_Elder,
                Giant_Storm,
                Githyanki,
                Githzerai,
                Gnoll,
                Gnome_Forest,
                Gnome_Rock,
                Gnome_Svirfneblin,
                Goblin,
                GrayRender,
                GreenHag,
                Griffon,
                Grig,
                Grig_WithFiddle,
                Grimlock,
                Gynosphinx,
                Halfling_Deep,
                Halfling_Lightfoot,
                Halfling_Tallfellow,
                Harpy,
                HellHound,
                HellHound_NessianWarhound,
                Hezrou,
                Hieracosphinx,
                Hobgoblin,
                HoundArchon,
                Howler,
                Human,
                Janni,
                Kobold,
                Krenshar,
                KuoToa,
                Lamia,
                Lammasu,
                Lillend,
                Lizardfolk,
                Locathah,
                Manticore,
                Medusa,
                Mephit_Air,
                Mephit_Dust,
                Mephit_Earth,
                Mephit_Fire,
                Mephit_Ice,
                Mephit_Magma,
                Mephit_Ooze,
                Mephit_Salt,
                Mephit_Steam,
                Mephit_Water,
                Merfolk,
                MindFlayer,
                Minotaur,
                Mummy,
                Nightmare,
                Nightmare_Cauchemar,
                Nixie,
                Nymph,
                Ogre,
                OgreMage,
                Ogre_Merrow,
                Orc,
                Orc_Half,
                Owl_Giant,
                Pegasus,
                Pixie,
                Pixie_WithIrresistibleDance,
                Pseudodragon,
                Rakshasa,
                Sahuagin,
                Sahuagin_Malenti,
                Sahuagin_Mutant,
                Salamander_Average,
                Salamander_Flamebrother,
                Satyr,
                Satyr_WithPipes,
                Scorpionfolk,
                SeaHag,
                ShadowMastiff,
                ShamblingMound,
                Skum,
                Slaad_Blue,
                Slaad_Gray,
                Slaad_Green,
                Slaad_Red,
                Succubus,
                Tiefling,
                Treant,
                Triton,
                Troglodyte,
                Troll,
                Troll_Scrag,
                TrumpetArchon,
                Unicorn,
                Vrock,
                WinterWolf,
                Worg,
                Xill,
                YethHound,
                YuanTi_Abomination,
                YuanTi_Halfblood_SnakeArms,
                YuanTi_Halfblood_SnakeHead,
                YuanTi_Halfblood_SnakeTail,
                YuanTi_Halfblood_SnakeTailAndHumanLegs,
                YuanTi_Pureblood,
                Zelekhut,
            ];
        }
    }
}
