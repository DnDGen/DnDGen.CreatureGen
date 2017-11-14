using CreatureGen.Creatures;
using NUnit.Framework;
using System.Linq;

namespace CreatureGen.Tests.Unit.Creatures
{
    [TestFixture]
    public class CreatureConstantsTests
    {
        [TestCase(CreatureConstants.Aasimar, "Aasimar")]
        [TestCase(CreatureConstants.Aboleth, "Aboleth")]
        [TestCase(CreatureConstants.Achaierai, "Achaierai")]
        [TestCase(CreatureConstants.Allip, "Allip")]
        [TestCase(CreatureConstants.Androsphinx, "Androsphinx")]
        [TestCase(CreatureConstants.Ankheg, "Ankheg")]
        [TestCase(CreatureConstants.Annis, "Annis")]
        [TestCase(CreatureConstants.Ape, "Ape")]
        [TestCase(CreatureConstants.Ape_Dire, "Dire Ape")]
        [TestCase(CreatureConstants.Aranea, "Aranea")]
        [TestCase(CreatureConstants.AssassinVine, "Assassin Vine")]
        [TestCase(CreatureConstants.AstralDeva, "Astral Deva")]
        [TestCase(CreatureConstants.Athach, "Athach")]
        [TestCase(CreatureConstants.Avoral, "Avoral")]
        [TestCase(CreatureConstants.Azer, "Azer")]
        [TestCase(CreatureConstants.Babau, "Babau")]
        [TestCase(CreatureConstants.Baboon, "Baboon")]
        [TestCase(CreatureConstants.Badger, "Badger")]
        [TestCase(CreatureConstants.Badger_Dire, "Dire Badger")]
        [TestCase(CreatureConstants.Balor, "Balor")]
        [TestCase(CreatureConstants.Barghest, "Barghest")]
        [TestCase(CreatureConstants.Basilisk, "Basilisk")]
        [TestCase(CreatureConstants.Bat, "Bat")]
        [TestCase(CreatureConstants.Bat_Dire, "Dire Bat")]
        [TestCase(CreatureConstants.Bat_Swarm, "Bat Swarm")]
        [TestCase(CreatureConstants.Bear_Black, "Black Bear")]
        [TestCase(CreatureConstants.Bear_Brown, "Brown Bear")]
        [TestCase(CreatureConstants.Bear_Dire, "Dire Bear")]
        [TestCase(CreatureConstants.Bear_Polar, "Polar Bear")]
        [TestCase(CreatureConstants.Bebilith, "Bebilith")]
        [TestCase(CreatureConstants.Bee_Giant, "Giant Bee")]
        [TestCase(CreatureConstants.Behir, "Behir")]
        [TestCase(CreatureConstants.Beholder, "Beholder")]
        [TestCase(CreatureConstants.Belker, "Belker")]
        [TestCase(CreatureConstants.Bison, "Bison")]
        [TestCase(CreatureConstants.BlackPudding, "Black Pudding")]
        [TestCase(CreatureConstants.BlinkDog, "Blink Dog")]
        [TestCase(CreatureConstants.Boar, "Boar")]
        [TestCase(CreatureConstants.Boar_Dire, "Dire Boar")]
        [TestCase(CreatureConstants.Bodak, "Bodak")]
        [TestCase(CreatureConstants.BombardierBeetle_Giant, "Giant Bombardier Beetle")]
        [TestCase(CreatureConstants.Bralani, "Bralani")]
        [TestCase(CreatureConstants.Bugbear, "Bugbear")]
        [TestCase(CreatureConstants.Bulette, "Bulette")]
        [TestCase(CreatureConstants.Camel, "Camel")]
        [TestCase(CreatureConstants.CarrionCrawler, "Carrion Crawler")]
        [TestCase(CreatureConstants.Cat, "Cat")]
        [TestCase(CreatureConstants.Centaur, "Centaur")]
        [TestCase(CreatureConstants.Centipede_Swarm, "Centipede Swarm")]
        [TestCase(CreatureConstants.ChaosBeast, "Chaos Beast")]
        [TestCase(CreatureConstants.Cheetah, "Cheetah")]
        [TestCase(CreatureConstants.Chimera, "Chimera")]
        [TestCase(CreatureConstants.Choker, "Choker")]
        [TestCase(CreatureConstants.Chuul, "Chuul")]
        [TestCase(CreatureConstants.Cloaker, "Cloaker")]
        [TestCase(CreatureConstants.Cockatrice, "Cockatrice")]
        [TestCase(CreatureConstants.Couatl, "Couatl")]
        [TestCase(CreatureConstants.Criosphinx, "Criosphinx")]
        [TestCase(CreatureConstants.Crocodile, "Crocodile")]
        [TestCase(CreatureConstants.Crocodile_Giant, "Giant Crocodile")]
        [TestCase(CreatureConstants.Darkmantle, "Darkmantle")]
        [TestCase(CreatureConstants.Deinonychus, "Deinonychus")]
        [TestCase(CreatureConstants.Derro, "Derro")]
        [TestCase(CreatureConstants.Destrachan, "Destrachan")]
        [TestCase(CreatureConstants.Devourer, "Devourer")]
        [TestCase(CreatureConstants.Delver, "Delver")]
        [TestCase(CreatureConstants.Digester, "Digester")]
        [TestCase(CreatureConstants.DisplacerBeast, "Displacer Beast")]
        [TestCase(CreatureConstants.Djinni, "Djinni")]
        [TestCase(CreatureConstants.Dog, "Dog")]
        [TestCase(CreatureConstants.Donkey, "Donkey")]
        [TestCase(CreatureConstants.Doppelganger, "Doppelganger")]
        [TestCase(CreatureConstants.DragonTurtle, "Dragon Turtle")]
        [TestCase(CreatureConstants.Dragonne, "Dragonne")]
        [TestCase(CreatureConstants.Dretch, "Dretch")]
        [TestCase(CreatureConstants.Drider, "Drider")]
        [TestCase(CreatureConstants.Dryad, "Dryad")]
        [TestCase(CreatureConstants.Duergar, "Duergar")]
        [TestCase(CreatureConstants.Dwarf_Deep, "Deep Dwarf")]
        [TestCase(CreatureConstants.Dwarf_Hill, "Hill Dwarf")]
        [TestCase(CreatureConstants.Dwarf_Mountain, "Mountain Dwarf")]
        [TestCase(CreatureConstants.Eagle, "Eagle")]
        [TestCase(CreatureConstants.Eagle_Giant, "Giant Eagle")]
        [TestCase(CreatureConstants.Efreeti, "Efreeti")]
        [TestCase(CreatureConstants.Elasmosaurus, "Elasmosaurus")]
        [TestCase(CreatureConstants.Elephant, "Elephant")]
        [TestCase(CreatureConstants.Elf_Aquatic, "Aquatic Elf")]
        [TestCase(CreatureConstants.Elf_Drow, "Drow")]
        [TestCase(CreatureConstants.Elf_Gray, "Gray Elf")]
        [TestCase(CreatureConstants.Elf_High, "High Elf")]
        [TestCase(CreatureConstants.Elf_Half, "Half-Elf")]
        [TestCase(CreatureConstants.Elf_Wood, "Wood Elf")]
        [TestCase(CreatureConstants.Elf_Wild, "Wild Elf")]
        [TestCase(CreatureConstants.Erinyes, "Erinyes")]
        [TestCase(CreatureConstants.EtherealFilcher, "Ethereal Filcher")]
        [TestCase(CreatureConstants.EtherealMarauder, "Ethereal Marauder")]
        [TestCase(CreatureConstants.Ettercap, "Ettercap")]
        [TestCase(CreatureConstants.Ettin, "Ettin")]
        [TestCase(CreatureConstants.FireBeetle_Giant, "Giant Fire Beetle")]
        [TestCase(CreatureConstants.FrostWorm, "Frost Worm")]
        [TestCase(CreatureConstants.Gargoyle, "Gargoyle")]
        [TestCase(CreatureConstants.GelatinousCube, "Gelatinous Cube")]
        [TestCase(CreatureConstants.Ghaele, "Ghaele")]
        [TestCase(CreatureConstants.Ghoul, "Ghoul")]
        [TestCase(CreatureConstants.Giant_Cloud, "Cloud Giant")]
        [TestCase(CreatureConstants.Giant_Fire, "Fire Giant")]
        [TestCase(CreatureConstants.Giant_Frost, "Frost Giant")]
        [TestCase(CreatureConstants.Giant_Hill, "Hill Giant")]
        [TestCase(CreatureConstants.Giant_Stone, "Stone Giant")]
        [TestCase(CreatureConstants.Giant_Storm, "Storm Giant")]
        [TestCase(CreatureConstants.GibberingMouther, "Gibbering Mouther")]
        [TestCase(CreatureConstants.Girallon, "Girallon")]
        [TestCase(CreatureConstants.Githyanki, "Githyanki")]
        [TestCase(CreatureConstants.Githzerai, "Githzerai")]
        [TestCase(CreatureConstants.Glabrezu, "Glabrezu")]
        [TestCase(CreatureConstants.Gnoll, "Gnoll")]
        [TestCase(CreatureConstants.Gnome_Forest, "Forest Gnome")]
        [TestCase(CreatureConstants.Gnome_Rock, "Rock Gnome")]
        [TestCase(CreatureConstants.Goblin, "Goblin")]
        [TestCase(CreatureConstants.Gorgon, "Gorgon")]
        [TestCase(CreatureConstants.GrayRender, "Gray Render")]
        [TestCase(CreatureConstants.GreenHag, "Green Hag")]
        [TestCase(CreatureConstants.Grick, "Grick")]
        [TestCase(CreatureConstants.Griffon, "Griffon")]
        [TestCase(CreatureConstants.Grig, "Grig")]
        [TestCase(CreatureConstants.Grimlock, "Grimlock")]
        [TestCase(CreatureConstants.Gynosphinx, "Gynosphinx")]
        [TestCase(CreatureConstants.Halfling_Deep, "Deep Halfling")]
        [TestCase(CreatureConstants.Halfling_Lightfoot, "Lightfoot Halfling")]
        [TestCase(CreatureConstants.Halfling_Tallfellow, "Tallfellow Halfling")]
        [TestCase(CreatureConstants.Harpy, "Harpy")]
        [TestCase(CreatureConstants.Hawk, "Hawk")]
        [TestCase(CreatureConstants.HellHound, "Hell Hound")]
        [TestCase(CreatureConstants.Hellwasp_Swarm, "Hellwasp Swarm")]
        [TestCase(CreatureConstants.Hezrou, "Hezrou")]
        [TestCase(CreatureConstants.Hieracosphinx, "Hieracosphinx")]
        [TestCase(CreatureConstants.Hippogriff, "Hippogriff")]
        [TestCase(CreatureConstants.Hobgoblin, "Hobgoblin")]
        [TestCase(CreatureConstants.Homunculus, "Homunculus")]
        [TestCase(CreatureConstants.Horse_Heavy, "Heavy Horse")]
        [TestCase(CreatureConstants.Horse_Heavy_War, "Heavy Warhorse")]
        [TestCase(CreatureConstants.Horse_Light, "Light Horse")]
        [TestCase(CreatureConstants.Horse_Light_War, "Light Warhorse")]
        [TestCase(CreatureConstants.HoundArchon, "Hound Archon")]
        [TestCase(CreatureConstants.Howler, "Howler")]
        [TestCase(CreatureConstants.Human, "Human")]
        [TestCase(CreatureConstants.Hyena, "Hyena")]
        [TestCase(CreatureConstants.Imp, "Imp")]
        [TestCase(CreatureConstants.InvisibleStalker, "Invisible Stalker")]
        [TestCase(CreatureConstants.Janni, "Janni")]
        [TestCase(CreatureConstants.Kobold, "Kobold")]
        [TestCase(CreatureConstants.Kolyarut, "Kolyarut")]
        [TestCase(CreatureConstants.Kraken, "Kraken")]
        [TestCase(CreatureConstants.Krenshar, "Krenshar")]
        [TestCase(CreatureConstants.KuoToa, "Kuo-toa")]
        [TestCase(CreatureConstants.Lamia, "Lamia")]
        [TestCase(CreatureConstants.Lammasu, "Lammasu")]
        [TestCase(CreatureConstants.LanternArchon, "Lantern Archon")]
        [TestCase(CreatureConstants.Lemure, "Lemure")]
        [TestCase(CreatureConstants.Leonal, "Leonal")]
        [TestCase(CreatureConstants.Leopard, "Leopard")]
        [TestCase(CreatureConstants.Lillend, "Lillend")]
        [TestCase(CreatureConstants.Lion, "Lion")]
        [TestCase(CreatureConstants.Lion_Dire, "Dire Lion")]
        [TestCase(CreatureConstants.Lizard, "Lizard")]
        [TestCase(CreatureConstants.Lizard_Monitor, "Monitor Lizard")]
        [TestCase(CreatureConstants.Lizardfolk, "Lizardfolk")]
        [TestCase(CreatureConstants.Locathah, "Locathah")]
        [TestCase(CreatureConstants.Locust_Swarm, "Locust Swarm")]
        [TestCase(CreatureConstants.Magmin, "Magmin")]
        [TestCase(CreatureConstants.MantaRay, "Manta Ray")]
        [TestCase(CreatureConstants.Manticore, "Manticore")]
        [TestCase(CreatureConstants.Marilith, "Marilith")]
        [TestCase(CreatureConstants.Marut, "Marut")]
        [TestCase(CreatureConstants.Medusa, "Medusa")]
        [TestCase(CreatureConstants.Megaraptor, "Megaraptor")]
        [TestCase(CreatureConstants.Merfolk, "Merfolk")]
        [TestCase(CreatureConstants.Mimic, "Mimic")]
        [TestCase(CreatureConstants.MindFlayer, "Mind Flayer")]
        [TestCase(CreatureConstants.Minotaur, "Minotaur")]
        [TestCase(CreatureConstants.Mohrg, "Mohrg")]
        [TestCase(CreatureConstants.Monkey, "Monkey")]
        [TestCase(CreatureConstants.Mule, "Mule")]
        [TestCase(CreatureConstants.Mummy, "Mummy")]
        [TestCase(CreatureConstants.Nalfeshnee, "Nalfeshnee")]
        [TestCase(CreatureConstants.NessianWarhound, "Nessian Warhound")]
        [TestCase(CreatureConstants.Nightcrawler, "Nightcrawler")]
        [TestCase(CreatureConstants.NightHag, "Night Hag")]
        [TestCase(CreatureConstants.Nightmare, "Nightmare")]
        [TestCase(CreatureConstants.Nightwalker, "Nightwalker")]
        [TestCase(CreatureConstants.Nightwing, "Nightwing")]
        [TestCase(CreatureConstants.Nixie, "Nixie")]
        [TestCase(CreatureConstants.Nymph, "Nymph")]
        [TestCase(CreatureConstants.Octopus, "Octopus")]
        [TestCase(CreatureConstants.Octopus_Giant, "Giant Octopus")]
        [TestCase(CreatureConstants.Ogre, "Ogre")]
        [TestCase(CreatureConstants.OgreMage, "Ogre Mage")]
        [TestCase(CreatureConstants.Ooze_Gray, "Gray Ooze")]
        [TestCase(CreatureConstants.Ooze_OchreJelly, "Ochre jelly")]
        [TestCase(CreatureConstants.Orc, "Orc")]
        [TestCase(CreatureConstants.Orc_Half, "Half-Orc")]
        [TestCase(CreatureConstants.Otyugh, "Otyugh")]
        [TestCase(CreatureConstants.Owl, "Owl")]
        [TestCase(CreatureConstants.Owl_Giant, "Giant Owl")]
        [TestCase(CreatureConstants.Owlbear, "Owlbear")]
        [TestCase(CreatureConstants.Pegasus, "Pegasus")]
        [TestCase(CreatureConstants.PhantomFungus, "Phantom Fungus")]
        [TestCase(CreatureConstants.PhaseSpider, "Phase Spider")]
        [TestCase(CreatureConstants.Phasm, "Phasm")]
        [TestCase(CreatureConstants.PitFiend, "Pit Fiend")]
        [TestCase(CreatureConstants.Pixie, "Pixie")]
        [TestCase(CreatureConstants.Planetar, "Planetar")]
        [TestCase(CreatureConstants.Pony, "Pony")]
        [TestCase(CreatureConstants.Pony_War, "Warpony")]
        [TestCase(CreatureConstants.Porpoise, "Porpoise")]
        [TestCase(CreatureConstants.PrayingMantis_Giant, "Giant Praying Mantis")]
        [TestCase(CreatureConstants.Pseudodragon, "Pseudodragon")]
        [TestCase(CreatureConstants.PurpleWorm, "Purple Worm")]
        [TestCase(CreatureConstants.Quasit, "Quasit")]
        [TestCase(CreatureConstants.Rakshasa, "Rakshasa")]
        [TestCase(CreatureConstants.Rast, "Rast")]
        [TestCase(CreatureConstants.Rat, "Rat")]
        [TestCase(CreatureConstants.Rat_Dire, "Dire Rat")]
        [TestCase(CreatureConstants.Rat_Swarm, "Rat Swarm")]
        [TestCase(CreatureConstants.Raven, "Raven")]
        [TestCase(CreatureConstants.Ravid, "Ravid")]
        [TestCase(CreatureConstants.RazorBoar, "Razor Boar")]
        [TestCase(CreatureConstants.Remorhaz, "Remorhaz")]
        [TestCase(CreatureConstants.Retriever, "Retriever")]
        [TestCase(CreatureConstants.Rhinoceras, "Rhinoceras")]
        [TestCase(CreatureConstants.Roc, "Roc")]
        [TestCase(CreatureConstants.Roper, "Roper")]
        [TestCase(CreatureConstants.RustMonster, "Rust Monster")]
        [TestCase(CreatureConstants.Sahuagin, "Sahuagin")]
        [TestCase(CreatureConstants.Satyr, "Satyr")]
        [TestCase(CreatureConstants.Scorpionfolk, "Scorpionfolk")]
        [TestCase(CreatureConstants.SeaCat, "Sea Cat")]
        [TestCase(CreatureConstants.SeaHag, "Sea Hag")]
        [TestCase(CreatureConstants.Shadow, "Shadow")]
        [TestCase(CreatureConstants.ShadowMastiff, "Shadow Mastiff")]
        [TestCase(CreatureConstants.Shark_Dire, "Dire Shark")]
        [TestCase(CreatureConstants.ShamblingMound, "Shambling Mound")]
        [TestCase(CreatureConstants.ShieldGuardian, "Shield Guardian")]
        [TestCase(CreatureConstants.ShockerLizard, "Shocker Lizard")]
        [TestCase(CreatureConstants.Shrieker, "Shrieker")]
        [TestCase(CreatureConstants.Skum, "Skum")]
        [TestCase(CreatureConstants.Snake_Constrictor, "Constrictor Snake")]
        [TestCase(CreatureConstants.Snake_Constrictor_Giant, "Giant Constrictor Snake")]
        [TestCase(CreatureConstants.Solar, "Solar")]
        [TestCase(CreatureConstants.Spectre, "Spectre")]
        [TestCase(CreatureConstants.Spider_Swarm, "Spider Swarm")]
        [TestCase(CreatureConstants.SpiderEater, "Spider Eater")]
        [TestCase(CreatureConstants.Squid, "Squid")]
        [TestCase(CreatureConstants.Squid_Giant, "Giant Squid")]
        [TestCase(CreatureConstants.StagBeetle_Giant, "Giant Stag Beetle")]
        [TestCase(CreatureConstants.Stirge, "Stirge")]
        [TestCase(CreatureConstants.Succubus, "Succubus")]
        [TestCase(CreatureConstants.Gnome_Svirfneblin, "Svirfneblin")]
        [TestCase(CreatureConstants.Tarrasque, "Tarrasque")]
        [TestCase(CreatureConstants.Tendriculos, "Tendriculos")]
        [TestCase(CreatureConstants.Thoqqua, "Thoqqua")]
        [TestCase(CreatureConstants.Tiefling, "Tiefling")]
        [TestCase(CreatureConstants.Tiger, "Tiger")]
        [TestCase(CreatureConstants.Tiger_Dire, "Dire Tiger")]
        [TestCase(CreatureConstants.Titan, "Titan")]
        [TestCase(CreatureConstants.Toad, "Toad")]
        [TestCase(CreatureConstants.Treant, "Treant")]
        [TestCase(CreatureConstants.Triceratops, "Triceratops")]
        [TestCase(CreatureConstants.Triton, "Triton")]
        [TestCase(CreatureConstants.Troglodyte, "Troglodyte")]
        [TestCase(CreatureConstants.Troll, "Troll")]
        [TestCase(CreatureConstants.TrumpetArchon, "Trumpet Archon")]
        [TestCase(CreatureConstants.Tyrannosaurus, "Tyrannosaurus")]
        [TestCase(CreatureConstants.UmberHulk, "Umber Hulk")]
        [TestCase(CreatureConstants.Unicorn, "Unicorn")]
        [TestCase(CreatureConstants.VampireSpawn, "Vampire Spawn")]
        [TestCase(CreatureConstants.Vargouille, "Vargouille")]
        [TestCase(CreatureConstants.VioletFungus, "Violet Fungus")]
        [TestCase(CreatureConstants.Vrock, "Vrock")]
        [TestCase(CreatureConstants.Wasp_Giant, "Giant Wasp")]
        [TestCase(CreatureConstants.Weasel, "Weasel")]
        [TestCase(CreatureConstants.Weasel_Dire, "Dire Weasel")]
        [TestCase(CreatureConstants.Werebear, "Werebear")]
        [TestCase(CreatureConstants.Wereboar, "Wereboar")]
        [TestCase(CreatureConstants.Wereboar_HillGiantDire, "Hill Giant Dire Wereboar")]
        [TestCase(CreatureConstants.Wererat, "Wererat")]
        [TestCase(CreatureConstants.Weretiger, "Weretiger")]
        [TestCase(CreatureConstants.Werewolf, "Werewolf")]
        [TestCase(CreatureConstants.Wight, "Wight")]
        [TestCase(CreatureConstants.WillOWisp, "Will-O'-Wisp")]
        [TestCase(CreatureConstants.WinterWolf, "Winter Wolf")]
        [TestCase(CreatureConstants.Wolf, "Wolf")]
        [TestCase(CreatureConstants.Wolf_Dire, "Dire Wolf")]
        [TestCase(CreatureConstants.Wolverine, "Wolverine")]
        [TestCase(CreatureConstants.Wolverine_Dire, "Dire Wolverine")]
        [TestCase(CreatureConstants.Worg, "Worg")]
        [TestCase(CreatureConstants.Wraith, "Wraith")]
        [TestCase(CreatureConstants.Wyvern, "Wyvern")]
        [TestCase(CreatureConstants.Xill, "Xill")]
        [TestCase(CreatureConstants.YethHound, "Yeth Hound")]
        [TestCase(CreatureConstants.Yrthak, "Yrthak")]
        [TestCase(CreatureConstants.Zelekhut, "Zelekhut")]
        [TestCase(CreatureConstants.Aboleth_Mage, CreatureConstants.Aboleth + " Mage")]
        [TestCase(CreatureConstants.AnimatedObject_Colossal, CreatureConstants.Groups.AnimatedObject + ", " + SizeConstants.Colossal)]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan, CreatureConstants.Groups.AnimatedObject + ", " + SizeConstants.Gargantuan)]
        [TestCase(CreatureConstants.AnimatedObject_Huge, CreatureConstants.Groups.AnimatedObject + ", " + SizeConstants.Huge)]
        [TestCase(CreatureConstants.AnimatedObject_Large, CreatureConstants.Groups.AnimatedObject + ", " + SizeConstants.Large)]
        [TestCase(CreatureConstants.AnimatedObject_Medium, CreatureConstants.Groups.AnimatedObject + ", " + SizeConstants.Medium)]
        [TestCase(CreatureConstants.AnimatedObject_Small, CreatureConstants.Groups.AnimatedObject + ", " + SizeConstants.Small)]
        [TestCase(CreatureConstants.AnimatedObject_Tiny, CreatureConstants.Groups.AnimatedObject + ", " + SizeConstants.Tiny)]
        [TestCase(CreatureConstants.Ant_Giant_Soldier, CreatureConstants.Groups.Ant_Giant + ", Soldier")]
        [TestCase(CreatureConstants.Ant_Giant_Worker, CreatureConstants.Groups.Ant_Giant + ", Worker")]
        [TestCase(CreatureConstants.Ant_Giant_Queen, CreatureConstants.Groups.Ant_Giant + ", Queen")]
        [TestCase(CreatureConstants.Arrowhawk_Juvenile, "Juvenile " + CreatureConstants.Groups.Arrowhawk)]
        [TestCase(CreatureConstants.Arrowhawk_Adult, "Adult " + CreatureConstants.Groups.Arrowhawk)]
        [TestCase(CreatureConstants.Arrowhawk_Elder, "Elder " + CreatureConstants.Groups.Arrowhawk)]
        [TestCase(CreatureConstants.BarbedDevil_Hamatula, "Barbed Devil (Hamatula)")]
        [TestCase(CreatureConstants.BeardedDevil_Barbazu, "Bearded Devil (Barbazu)")]
        [TestCase(CreatureConstants.Barghest_Greater, "Greater " + CreatureConstants.Barghest)]
        [TestCase(CreatureConstants.Basilisk_AbyssalGreater, "Abyssal Greater " + CreatureConstants.Basilisk)]
        [TestCase(CreatureConstants.BlackPudding_Elder, "Elder " + CreatureConstants.BlackPudding)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Colossal, CreatureConstants.Groups.Centipede_Monstrous + ", " + SizeConstants.Colossal)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Gargantuan, CreatureConstants.Groups.Centipede_Monstrous + ", " + SizeConstants.Gargantuan)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Huge, CreatureConstants.Groups.Centipede_Monstrous + ", " + SizeConstants.Huge)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Large, CreatureConstants.Groups.Centipede_Monstrous + ", " + SizeConstants.Large)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Medium, CreatureConstants.Groups.Centipede_Monstrous + ", " + SizeConstants.Medium)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Small, CreatureConstants.Groups.Centipede_Monstrous + ", " + SizeConstants.Small)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Tiny, CreatureConstants.Groups.Centipede_Monstrous + ", " + SizeConstants.Tiny)]
        [TestCase(CreatureConstants.ChainDevil_Kyton, "Chain Devil (Kyton)")]
        [TestCase(CreatureConstants.HornedDevil_Cornugon, "Horned Devil (Cornugon)")]
        [TestCase(CreatureConstants.Cryohydra_5Heads, "Five-Headed " + CreatureConstants.Groups.Cryohydra)]
        [TestCase(CreatureConstants.Cryohydra_6Heads, "Six-Headed " + CreatureConstants.Groups.Cryohydra)]
        [TestCase(CreatureConstants.Cryohydra_7Heads, "Seven-Headed " + CreatureConstants.Groups.Cryohydra)]
        [TestCase(CreatureConstants.Cryohydra_8Heads, "Eight-Headed " + CreatureConstants.Groups.Cryohydra)]
        [TestCase(CreatureConstants.Cryohydra_9Heads, "Nine-Headed " + CreatureConstants.Groups.Cryohydra)]
        [TestCase(CreatureConstants.Cryohydra_10Heads, "Ten-Headed " + CreatureConstants.Groups.Cryohydra)]
        [TestCase(CreatureConstants.Cryohydra_11Heads, "Eleven-Headed " + CreatureConstants.Groups.Cryohydra)]
        [TestCase(CreatureConstants.Cryohydra_12Heads, "Twelve-Headed " + CreatureConstants.Groups.Cryohydra)]
        [TestCase(CreatureConstants.DisplacerBeast_PackLord, CreatureConstants.DisplacerBeast + " Pack Lord")]
        [TestCase(CreatureConstants.Djinni_Noble, "Noble " + CreatureConstants.Djinni)]
        [TestCase(CreatureConstants.Dog_Riding, CreatureConstants.Dog + ", Riding")]
        [TestCase(CreatureConstants.Dragon_Black_Wyrmling, CreatureConstants.Groups.Dragon_Black + ", Wyrmling")]
        [TestCase(CreatureConstants.Dragon_Black_VeryYoung, CreatureConstants.Groups.Dragon_Black + ", Very Young")]
        [TestCase(CreatureConstants.Dragon_Black_Young, CreatureConstants.Groups.Dragon_Black + ", Young")]
        [TestCase(CreatureConstants.Dragon_Black_Juvenile, CreatureConstants.Groups.Dragon_Black + ", Juvenile")]
        [TestCase(CreatureConstants.Dragon_Black_YoungAdult, CreatureConstants.Groups.Dragon_Black + ", Young Adult")]
        [TestCase(CreatureConstants.Dragon_Black_Adult, CreatureConstants.Groups.Dragon_Black + ", Adult")]
        [TestCase(CreatureConstants.Dragon_Black_MatureAdult, CreatureConstants.Groups.Dragon_Black + ", Mature Adult")]
        [TestCase(CreatureConstants.Dragon_Black_Old, CreatureConstants.Groups.Dragon_Black + ", Old")]
        [TestCase(CreatureConstants.Dragon_Black_VeryOld, CreatureConstants.Groups.Dragon_Black + ", Very Old")]
        [TestCase(CreatureConstants.Dragon_Black_Ancient, CreatureConstants.Groups.Dragon_Black + ", Ancient")]
        [TestCase(CreatureConstants.Dragon_Black_Wyrm, CreatureConstants.Groups.Dragon_Black + ", Wyrm")]
        [TestCase(CreatureConstants.Dragon_Black_GreatWyrm, CreatureConstants.Groups.Dragon_Black + ", Great Wyrm")]
        [TestCase(CreatureConstants.Dragon_Blue_Wyrmling, CreatureConstants.Groups.Dragon_Blue + ", Wyrmling")]
        [TestCase(CreatureConstants.Dragon_Blue_VeryYoung, CreatureConstants.Groups.Dragon_Blue + ", Very Young")]
        [TestCase(CreatureConstants.Dragon_Blue_Young, CreatureConstants.Groups.Dragon_Blue + ", Young")]
        [TestCase(CreatureConstants.Dragon_Blue_Juvenile, CreatureConstants.Groups.Dragon_Blue + ", Juvenile")]
        [TestCase(CreatureConstants.Dragon_Blue_YoungAdult, CreatureConstants.Groups.Dragon_Blue + ", Young Adult")]
        [TestCase(CreatureConstants.Dragon_Blue_Adult, CreatureConstants.Groups.Dragon_Blue + ", Adult")]
        [TestCase(CreatureConstants.Dragon_Blue_MatureAdult, CreatureConstants.Groups.Dragon_Blue + ", Mature Adult")]
        [TestCase(CreatureConstants.Dragon_Blue_Old, CreatureConstants.Groups.Dragon_Blue + ", Old")]
        [TestCase(CreatureConstants.Dragon_Blue_VeryOld, CreatureConstants.Groups.Dragon_Blue + ", Very Old")]
        [TestCase(CreatureConstants.Dragon_Blue_Ancient, CreatureConstants.Groups.Dragon_Blue + ", Ancient")]
        [TestCase(CreatureConstants.Dragon_Blue_Wyrm, CreatureConstants.Groups.Dragon_Blue + ", Wyrm")]
        [TestCase(CreatureConstants.Dragon_Blue_GreatWyrm, CreatureConstants.Groups.Dragon_Blue + ", Great Wyrm")]
        [TestCase(CreatureConstants.Dragon_Brass_Wyrmling, CreatureConstants.Groups.Dragon_Brass + ", Wyrmling")]
        [TestCase(CreatureConstants.Dragon_Brass_VeryYoung, CreatureConstants.Groups.Dragon_Brass + ", Very Young")]
        [TestCase(CreatureConstants.Dragon_Brass_Young, CreatureConstants.Groups.Dragon_Brass + ", Young")]
        [TestCase(CreatureConstants.Dragon_Brass_Juvenile, CreatureConstants.Groups.Dragon_Brass + ", Juvenile")]
        [TestCase(CreatureConstants.Dragon_Brass_YoungAdult, CreatureConstants.Groups.Dragon_Brass + ", Young Adult")]
        [TestCase(CreatureConstants.Dragon_Brass_Adult, CreatureConstants.Groups.Dragon_Brass + ", Adult")]
        [TestCase(CreatureConstants.Dragon_Brass_MatureAdult, CreatureConstants.Groups.Dragon_Brass + ", Mature Adult")]
        [TestCase(CreatureConstants.Dragon_Brass_Old, CreatureConstants.Groups.Dragon_Brass + ", Old")]
        [TestCase(CreatureConstants.Dragon_Brass_VeryOld, CreatureConstants.Groups.Dragon_Brass + ", Very Old")]
        [TestCase(CreatureConstants.Dragon_Brass_Ancient, CreatureConstants.Groups.Dragon_Brass + ", Ancient")]
        [TestCase(CreatureConstants.Dragon_Brass_Wyrm, CreatureConstants.Groups.Dragon_Brass + ", Wyrm")]
        [TestCase(CreatureConstants.Dragon_Brass_GreatWyrm, CreatureConstants.Groups.Dragon_Brass + ", Great Wyrm")]
        [TestCase(CreatureConstants.Dragon_Bronze_Wyrmling, CreatureConstants.Groups.Dragon_Bronze + ", Wyrmling")]
        [TestCase(CreatureConstants.Dragon_Bronze_VeryYoung, CreatureConstants.Groups.Dragon_Bronze + ", Very Young")]
        [TestCase(CreatureConstants.Dragon_Bronze_Young, CreatureConstants.Groups.Dragon_Bronze + ", Young")]
        [TestCase(CreatureConstants.Dragon_Bronze_Juvenile, CreatureConstants.Groups.Dragon_Bronze + ", Juvenile")]
        [TestCase(CreatureConstants.Dragon_Bronze_YoungAdult, CreatureConstants.Groups.Dragon_Bronze + ", Young Adult")]
        [TestCase(CreatureConstants.Dragon_Bronze_Adult, CreatureConstants.Groups.Dragon_Bronze + ", Adult")]
        [TestCase(CreatureConstants.Dragon_Bronze_MatureAdult, CreatureConstants.Groups.Dragon_Bronze + ", Mature Adult")]
        [TestCase(CreatureConstants.Dragon_Bronze_Old, CreatureConstants.Groups.Dragon_Bronze + ", Old")]
        [TestCase(CreatureConstants.Dragon_Bronze_VeryOld, CreatureConstants.Groups.Dragon_Bronze + ", Very Old")]
        [TestCase(CreatureConstants.Dragon_Bronze_Ancient, CreatureConstants.Groups.Dragon_Bronze + ", Ancient")]
        [TestCase(CreatureConstants.Dragon_Bronze_Wyrm, CreatureConstants.Groups.Dragon_Bronze + ", Wyrm")]
        [TestCase(CreatureConstants.Dragon_Bronze_GreatWyrm, CreatureConstants.Groups.Dragon_Bronze + ", Great Wyrm")]
        [TestCase(CreatureConstants.Dragon_Copper_Wyrmling, CreatureConstants.Groups.Dragon_Copper + ", Wyrmling")]
        [TestCase(CreatureConstants.Dragon_Copper_VeryYoung, CreatureConstants.Groups.Dragon_Copper + ", Very Young")]
        [TestCase(CreatureConstants.Dragon_Copper_Young, CreatureConstants.Groups.Dragon_Copper + ", Young")]
        [TestCase(CreatureConstants.Dragon_Copper_Juvenile, CreatureConstants.Groups.Dragon_Copper + ", Juvenile")]
        [TestCase(CreatureConstants.Dragon_Copper_YoungAdult, CreatureConstants.Groups.Dragon_Copper + ", Young Adult")]
        [TestCase(CreatureConstants.Dragon_Copper_Adult, CreatureConstants.Groups.Dragon_Copper + ", Adult")]
        [TestCase(CreatureConstants.Dragon_Copper_MatureAdult, CreatureConstants.Groups.Dragon_Copper + ", Mature Adult")]
        [TestCase(CreatureConstants.Dragon_Copper_Old, CreatureConstants.Groups.Dragon_Copper + ", Old")]
        [TestCase(CreatureConstants.Dragon_Copper_VeryOld, CreatureConstants.Groups.Dragon_Copper + ", Very Old")]
        [TestCase(CreatureConstants.Dragon_Copper_Ancient, CreatureConstants.Groups.Dragon_Copper + ", Ancient")]
        [TestCase(CreatureConstants.Dragon_Copper_Wyrm, CreatureConstants.Groups.Dragon_Copper + ", Wyrm")]
        [TestCase(CreatureConstants.Dragon_Copper_GreatWyrm, CreatureConstants.Groups.Dragon_Copper + ", Great Wyrm")]
        [TestCase(CreatureConstants.Dragon_Gold_Wyrmling, CreatureConstants.Groups.Dragon_Gold + ", Wyrmling")]
        [TestCase(CreatureConstants.Dragon_Gold_VeryYoung, CreatureConstants.Groups.Dragon_Gold + ", Very Young")]
        [TestCase(CreatureConstants.Dragon_Gold_Young, CreatureConstants.Groups.Dragon_Gold + ", Young")]
        [TestCase(CreatureConstants.Dragon_Gold_Juvenile, CreatureConstants.Groups.Dragon_Gold + ", Juvenile")]
        [TestCase(CreatureConstants.Dragon_Gold_YoungAdult, CreatureConstants.Groups.Dragon_Gold + ", Young Adult")]
        [TestCase(CreatureConstants.Dragon_Gold_Adult, CreatureConstants.Groups.Dragon_Gold + ", Adult")]
        [TestCase(CreatureConstants.Dragon_Gold_MatureAdult, CreatureConstants.Groups.Dragon_Gold + ", Mature Adult")]
        [TestCase(CreatureConstants.Dragon_Gold_Old, CreatureConstants.Groups.Dragon_Gold + ", Old")]
        [TestCase(CreatureConstants.Dragon_Gold_VeryOld, CreatureConstants.Groups.Dragon_Gold + ", Very Old")]
        [TestCase(CreatureConstants.Dragon_Gold_Ancient, CreatureConstants.Groups.Dragon_Gold + ", Ancient")]
        [TestCase(CreatureConstants.Dragon_Gold_Wyrm, CreatureConstants.Groups.Dragon_Gold + ", Wyrm")]
        [TestCase(CreatureConstants.Dragon_Gold_GreatWyrm, CreatureConstants.Groups.Dragon_Gold + ", Great Wyrm")]
        [TestCase(CreatureConstants.Dragon_Green_Wyrmling, CreatureConstants.Groups.Dragon_Green + ", Wyrmling")]
        [TestCase(CreatureConstants.Dragon_Green_VeryYoung, CreatureConstants.Groups.Dragon_Green + ", Very Young")]
        [TestCase(CreatureConstants.Dragon_Green_Young, CreatureConstants.Groups.Dragon_Green + ", Young")]
        [TestCase(CreatureConstants.Dragon_Green_Juvenile, CreatureConstants.Groups.Dragon_Green + ", Juvenile")]
        [TestCase(CreatureConstants.Dragon_Green_YoungAdult, CreatureConstants.Groups.Dragon_Green + ", Young Adult")]
        [TestCase(CreatureConstants.Dragon_Green_Adult, CreatureConstants.Groups.Dragon_Green + ", Adult")]
        [TestCase(CreatureConstants.Dragon_Green_MatureAdult, CreatureConstants.Groups.Dragon_Green + ", Mature Adult")]
        [TestCase(CreatureConstants.Dragon_Green_Old, CreatureConstants.Groups.Dragon_Green + ", Old")]
        [TestCase(CreatureConstants.Dragon_Green_VeryOld, CreatureConstants.Groups.Dragon_Green + ", Very Old")]
        [TestCase(CreatureConstants.Dragon_Green_Ancient, CreatureConstants.Groups.Dragon_Green + ", Ancient")]
        [TestCase(CreatureConstants.Dragon_Green_Wyrm, CreatureConstants.Groups.Dragon_Green + ", Wyrm")]
        [TestCase(CreatureConstants.Dragon_Green_GreatWyrm, CreatureConstants.Groups.Dragon_Green + ", Great Wyrm")]
        [TestCase(CreatureConstants.Dragon_Red_Wyrmling, CreatureConstants.Groups.Dragon_Red + ", Wyrmling")]
        [TestCase(CreatureConstants.Dragon_Red_VeryYoung, CreatureConstants.Groups.Dragon_Red + ", Very Young")]
        [TestCase(CreatureConstants.Dragon_Red_Young, CreatureConstants.Groups.Dragon_Red + ", Young")]
        [TestCase(CreatureConstants.Dragon_Red_Juvenile, CreatureConstants.Groups.Dragon_Red + ", Juvenile")]
        [TestCase(CreatureConstants.Dragon_Red_YoungAdult, CreatureConstants.Groups.Dragon_Red + ", Young Adult")]
        [TestCase(CreatureConstants.Dragon_Red_Adult, CreatureConstants.Groups.Dragon_Red + ", Adult")]
        [TestCase(CreatureConstants.Dragon_Red_MatureAdult, CreatureConstants.Groups.Dragon_Red + ", Mature Adult")]
        [TestCase(CreatureConstants.Dragon_Red_Old, CreatureConstants.Groups.Dragon_Red + ", Old")]
        [TestCase(CreatureConstants.Dragon_Red_VeryOld, CreatureConstants.Groups.Dragon_Red + ", Very Old")]
        [TestCase(CreatureConstants.Dragon_Red_Ancient, CreatureConstants.Groups.Dragon_Red + ", Ancient")]
        [TestCase(CreatureConstants.Dragon_Red_Wyrm, CreatureConstants.Groups.Dragon_Red + ", Wyrm")]
        [TestCase(CreatureConstants.Dragon_Red_GreatWyrm, CreatureConstants.Groups.Dragon_Red + ", Great Wyrm")]
        [TestCase(CreatureConstants.Dragon_Silver_Wyrmling, CreatureConstants.Groups.Dragon_Silver + ", Wyrmling")]
        [TestCase(CreatureConstants.Dragon_Silver_VeryYoung, CreatureConstants.Groups.Dragon_Silver + ", Very Young")]
        [TestCase(CreatureConstants.Dragon_Silver_Young, CreatureConstants.Groups.Dragon_Silver + ", Young")]
        [TestCase(CreatureConstants.Dragon_Silver_Juvenile, CreatureConstants.Groups.Dragon_Silver + ", Juvenile")]
        [TestCase(CreatureConstants.Dragon_Silver_YoungAdult, CreatureConstants.Groups.Dragon_Silver + ", Young Adult")]
        [TestCase(CreatureConstants.Dragon_Silver_Adult, CreatureConstants.Groups.Dragon_Silver + ", Adult")]
        [TestCase(CreatureConstants.Dragon_Silver_MatureAdult, CreatureConstants.Groups.Dragon_Silver + ", Mature Adult")]
        [TestCase(CreatureConstants.Dragon_Silver_Old, CreatureConstants.Groups.Dragon_Silver + ", Old")]
        [TestCase(CreatureConstants.Dragon_Silver_VeryOld, CreatureConstants.Groups.Dragon_Silver + ", Very Old")]
        [TestCase(CreatureConstants.Dragon_Silver_Ancient, CreatureConstants.Groups.Dragon_Silver + ", Ancient")]
        [TestCase(CreatureConstants.Dragon_Silver_Wyrm, CreatureConstants.Groups.Dragon_Silver + ", Wyrm")]
        [TestCase(CreatureConstants.Dragon_Silver_GreatWyrm, CreatureConstants.Groups.Dragon_Silver + ", Great Wyrm")]
        [TestCase(CreatureConstants.Dragon_White_Wyrmling, CreatureConstants.Groups.Dragon_White + ", Wyrmling")]
        [TestCase(CreatureConstants.Dragon_White_VeryYoung, CreatureConstants.Groups.Dragon_White + ", Very Young")]
        [TestCase(CreatureConstants.Dragon_White_Young, CreatureConstants.Groups.Dragon_White + ", Young")]
        [TestCase(CreatureConstants.Dragon_White_Juvenile, CreatureConstants.Groups.Dragon_White + ", Juvenile")]
        [TestCase(CreatureConstants.Dragon_White_YoungAdult, CreatureConstants.Groups.Dragon_White + ", Young Adult")]
        [TestCase(CreatureConstants.Dragon_White_Adult, CreatureConstants.Groups.Dragon_White + ", Adult")]
        [TestCase(CreatureConstants.Dragon_White_MatureAdult, CreatureConstants.Groups.Dragon_White + ", Mature Adult")]
        [TestCase(CreatureConstants.Dragon_White_Old, CreatureConstants.Groups.Dragon_White + ", Old")]
        [TestCase(CreatureConstants.Dragon_White_VeryOld, CreatureConstants.Groups.Dragon_White + ", Very Old")]
        [TestCase(CreatureConstants.Dragon_White_Ancient, CreatureConstants.Groups.Dragon_White + ", Ancient")]
        [TestCase(CreatureConstants.Dragon_White_Wyrm, CreatureConstants.Groups.Dragon_White + ", Wyrm")]
        [TestCase(CreatureConstants.Dragon_White_GreatWyrm, CreatureConstants.Groups.Dragon_White + ", Great Wyrm")]
        [TestCase(CreatureConstants.Elemental_Air_Elder, CreatureConstants.Groups.Elemental_Air + ", Elder")]
        [TestCase(CreatureConstants.Elemental_Air_Greater, CreatureConstants.Groups.Elemental_Air + ", Greater")]
        [TestCase(CreatureConstants.Elemental_Air_Huge, CreatureConstants.Groups.Elemental_Air + ", Huge")]
        [TestCase(CreatureConstants.Elemental_Air_Large, CreatureConstants.Groups.Elemental_Air + ", Large")]
        [TestCase(CreatureConstants.Elemental_Air_Medium, CreatureConstants.Groups.Elemental_Air + ", Medium")]
        [TestCase(CreatureConstants.Elemental_Air_Small, CreatureConstants.Groups.Elemental_Air + ", Small")]
        [TestCase(CreatureConstants.Elemental_Earth_Elder, CreatureConstants.Groups.Elemental_Earth + ", Elder")]
        [TestCase(CreatureConstants.Elemental_Earth_Greater, CreatureConstants.Groups.Elemental_Earth + ", Greater")]
        [TestCase(CreatureConstants.Elemental_Earth_Huge, CreatureConstants.Groups.Elemental_Earth + ", Huge")]
        [TestCase(CreatureConstants.Elemental_Earth_Large, CreatureConstants.Groups.Elemental_Earth + ", Large")]
        [TestCase(CreatureConstants.Elemental_Earth_Medium, CreatureConstants.Groups.Elemental_Earth + ", Medium")]
        [TestCase(CreatureConstants.Elemental_Earth_Small, CreatureConstants.Groups.Elemental_Earth + ", Small")]
        [TestCase(CreatureConstants.Elemental_Fire_Elder, CreatureConstants.Groups.Elemental_Fire + ", Elder")]
        [TestCase(CreatureConstants.Elemental_Fire_Greater, CreatureConstants.Groups.Elemental_Fire + ", Greater")]
        [TestCase(CreatureConstants.Elemental_Fire_Huge, CreatureConstants.Groups.Elemental_Fire + ", Huge")]
        [TestCase(CreatureConstants.Elemental_Fire_Large, CreatureConstants.Groups.Elemental_Fire + ", Large")]
        [TestCase(CreatureConstants.Elemental_Fire_Medium, CreatureConstants.Groups.Elemental_Fire + ", Medium")]
        [TestCase(CreatureConstants.Elemental_Fire_Small, CreatureConstants.Groups.Elemental_Fire + ", Small")]
        [TestCase(CreatureConstants.Elemental_Water_Elder, CreatureConstants.Groups.Elemental_Water + ", Elder")]
        [TestCase(CreatureConstants.Elemental_Water_Greater, CreatureConstants.Groups.Elemental_Water + ", Greater")]
        [TestCase(CreatureConstants.Elemental_Water_Huge, CreatureConstants.Groups.Elemental_Water + ", Huge")]
        [TestCase(CreatureConstants.Elemental_Water_Large, CreatureConstants.Groups.Elemental_Water + ", Large")]
        [TestCase(CreatureConstants.Elemental_Water_Medium, CreatureConstants.Groups.Elemental_Water + ", Medium")]
        [TestCase(CreatureConstants.Elemental_Water_Small, CreatureConstants.Groups.Elemental_Water + ", Small")]
        [TestCase(CreatureConstants.FormianMyrmarch, CreatureConstants.Groups.Formian + " Myrmarch")]
        [TestCase(CreatureConstants.FormianQueen, CreatureConstants.Groups.Formian + " Queen")]
        [TestCase(CreatureConstants.FormianTaskmaster, CreatureConstants.Groups.Formian + " Taskmaster")]
        [TestCase(CreatureConstants.FormianWarrior, CreatureConstants.Groups.Formian + " Warrior")]
        [TestCase(CreatureConstants.FormianWorker, CreatureConstants.Groups.Formian + " Worker")]
        [TestCase(CreatureConstants.IceDevil_Gelugon, "Ice Devil (Gelugon)")]
        [TestCase(CreatureConstants.Gargoyle_Kapoacinth, "Kapoacinth")]
        [TestCase(CreatureConstants.Ghoul_Ghast, "Ghast")]
        [TestCase(CreatureConstants.Ghoul_Lacedon, "Lacedon")]
        [TestCase(CreatureConstants.Giant_Stone_Elder, CreatureConstants.Giant_Stone + " Elder")]
        [TestCase(CreatureConstants.Golem_Clay, "Clay " + CreatureConstants.Groups.Golem)]
        [TestCase(CreatureConstants.Golem_Flesh, "Flesh " + CreatureConstants.Groups.Golem)]
        [TestCase(CreatureConstants.Golem_Iron, "Iron " + CreatureConstants.Groups.Golem)]
        [TestCase(CreatureConstants.Golem_Stone, "Stone " + CreatureConstants.Groups.Golem)]
        [TestCase(CreatureConstants.Golem_Stone_Greater, "Greater Stone " + CreatureConstants.Groups.Golem)]
        [TestCase(CreatureConstants.Hellcat_Bezekira, "Hellcat (Bezekira)")]
        [TestCase(CreatureConstants.Hydra_5Heads, "Five-Headed " + CreatureConstants.Groups.Hydra)]
        [TestCase(CreatureConstants.Hydra_6Heads, "Six-Headed " + CreatureConstants.Groups.Hydra)]
        [TestCase(CreatureConstants.Hydra_7Heads, "Seven-Headed " + CreatureConstants.Groups.Hydra)]
        [TestCase(CreatureConstants.Hydra_8Heads, "Eight-Headed " + CreatureConstants.Groups.Hydra)]
        [TestCase(CreatureConstants.Hydra_9Heads, "Nine-Headed " + CreatureConstants.Groups.Hydra)]
        [TestCase(CreatureConstants.Hydra_10Heads, "Ten-Headed " + CreatureConstants.Groups.Hydra)]
        [TestCase(CreatureConstants.Hydra_11Heads, "Eleven-Headed " + CreatureConstants.Groups.Hydra)]
        [TestCase(CreatureConstants.Hydra_12Heads, "Twelve-Headed " + CreatureConstants.Groups.Hydra)]
        [TestCase(CreatureConstants.Lammasu_GoldenProtector, "Golden Protector (Celestial Half-Dragon Lammasu)")]
        [TestCase(CreatureConstants.Mephit_Air, "Air " + CreatureConstants.Groups.Mephit)]
        [TestCase(CreatureConstants.Mephit_Dust, "Dust " + CreatureConstants.Groups.Mephit)]
        [TestCase(CreatureConstants.Mephit_Earth, "Earth " + CreatureConstants.Groups.Mephit)]
        [TestCase(CreatureConstants.Mephit_Fire, "Fire " + CreatureConstants.Groups.Mephit)]
        [TestCase(CreatureConstants.Mephit_Ice, "Ice " + CreatureConstants.Groups.Mephit)]
        [TestCase(CreatureConstants.Mephit_Magma, "Magma " + CreatureConstants.Groups.Mephit)]
        [TestCase(CreatureConstants.Mephit_Ooze, "Ooze " + CreatureConstants.Groups.Mephit)]
        [TestCase(CreatureConstants.Mephit_Salt, "Salt " + CreatureConstants.Groups.Mephit)]
        [TestCase(CreatureConstants.Mephit_Steam, "Steam " + CreatureConstants.Groups.Mephit)]
        [TestCase(CreatureConstants.Mephit_Water, "Water " + CreatureConstants.Groups.Mephit)]
        [TestCase(CreatureConstants.Naga_Dark, "Dark " + CreatureConstants.Groups.Naga)]
        [TestCase(CreatureConstants.Naga_Guardian, "Guardian " + CreatureConstants.Groups.Naga)]
        [TestCase(CreatureConstants.Naga_Spirit, "Spirit " + CreatureConstants.Groups.Naga)]
        [TestCase(CreatureConstants.Naga_Water, "Water " + CreatureConstants.Groups.Naga)]
        [TestCase(CreatureConstants.Nightmare_Cauchemar, CreatureConstants.Nightmare + ", Cauchemar")]
        [TestCase(CreatureConstants.Ogre_Merrow, "Merrow")]
        [TestCase(CreatureConstants.BoneDevil_Osyluth, "Bone Devil (Osyluth)")]
        [TestCase(CreatureConstants.Pixie_WithIrresistableDance, CreatureConstants.Pixie + " with Irresistable Dance")]
        [TestCase(CreatureConstants.Pyrohydra_5Heads, "Five-Headed " + CreatureConstants.Groups.Pyrohydra)]
        [TestCase(CreatureConstants.Pyrohydra_6Heads, "Six-Headed " + CreatureConstants.Groups.Pyrohydra)]
        [TestCase(CreatureConstants.Pyrohydra_7Heads, "Seven-Headed " + CreatureConstants.Groups.Pyrohydra)]
        [TestCase(CreatureConstants.Pyrohydra_8Heads, "Eight-Headed " + CreatureConstants.Groups.Pyrohydra)]
        [TestCase(CreatureConstants.Pyrohydra_9Heads, "Nine-Headed " + CreatureConstants.Groups.Pyrohydra)]
        [TestCase(CreatureConstants.Pyrohydra_10Heads, "Ten-Headed " + CreatureConstants.Groups.Pyrohydra)]
        [TestCase(CreatureConstants.Pyrohydra_11Heads, "Eleven-Headed " + CreatureConstants.Groups.Pyrohydra)]
        [TestCase(CreatureConstants.Pyrohydra_12Heads, "Twelve-Headed " + CreatureConstants.Groups.Pyrohydra)]
        [TestCase(CreatureConstants.Salamander_Average, "Average " + CreatureConstants.Groups.Salamander)]
        [TestCase(CreatureConstants.Salamander_Flamebrother, "Flamebrother " + CreatureConstants.Groups.Salamander)]
        [TestCase(CreatureConstants.Salamander_Noble, "Noble " + CreatureConstants.Groups.Salamander)]
        [TestCase(CreatureConstants.Satyr_WithPipes, CreatureConstants.Satyr + " with pipes")]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Colossal, CreatureConstants.Groups.Scorpion_Monstrous + ", " + SizeConstants.Colossal)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Gargantuan, CreatureConstants.Groups.Scorpion_Monstrous + ", " + SizeConstants.Gargantuan)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Huge, CreatureConstants.Groups.Scorpion_Monstrous + ", " + SizeConstants.Huge)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Large, CreatureConstants.Groups.Scorpion_Monstrous + ", " + SizeConstants.Large)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Medium, CreatureConstants.Groups.Scorpion_Monstrous + ", " + SizeConstants.Medium)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Small, CreatureConstants.Groups.Scorpion_Monstrous + ", " + SizeConstants.Small)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Tiny, CreatureConstants.Groups.Scorpion_Monstrous + ", " + SizeConstants.Tiny)]
        [TestCase(CreatureConstants.Shadow_Greater, "Greater " + CreatureConstants.Shadow)]
        [TestCase(CreatureConstants.Shark_Medium, CreatureConstants.Groups.Shark + ", " + SizeConstants.Medium)]
        [TestCase(CreatureConstants.Shark_Large, CreatureConstants.Groups.Shark + ", " + SizeConstants.Large)]
        [TestCase(CreatureConstants.Shark_Huge, CreatureConstants.Groups.Shark + ", " + SizeConstants.Huge)]
        [TestCase(CreatureConstants.Slaad_Blue, "Blue " + CreatureConstants.Groups.Slaad)]
        [TestCase(CreatureConstants.Slaad_Death, "Death " + CreatureConstants.Groups.Slaad)]
        [TestCase(CreatureConstants.Slaad_Gray, "Gray " + CreatureConstants.Groups.Slaad)]
        [TestCase(CreatureConstants.Slaad_Green, "Green " + CreatureConstants.Groups.Slaad)]
        [TestCase(CreatureConstants.Slaad_Red, "Red " + CreatureConstants.Groups.Slaad)]
        [TestCase(CreatureConstants.Spider_Monstrous_Colossal, CreatureConstants.Groups.Spider_Monstrous + ", " + SizeConstants.Colossal)]
        [TestCase(CreatureConstants.Spider_Monstrous_Gargantuan, CreatureConstants.Groups.Spider_Monstrous + ", " + SizeConstants.Gargantuan)]
        [TestCase(CreatureConstants.Spider_Monstrous_Huge, CreatureConstants.Groups.Spider_Monstrous + ", " + SizeConstants.Huge)]
        [TestCase(CreatureConstants.Spider_Monstrous_Large, CreatureConstants.Groups.Spider_Monstrous + ", " + SizeConstants.Large)]
        [TestCase(CreatureConstants.Spider_Monstrous_Medium, CreatureConstants.Groups.Spider_Monstrous + ", " + SizeConstants.Medium)]
        [TestCase(CreatureConstants.Spider_Monstrous_Small, CreatureConstants.Groups.Spider_Monstrous + ", " + SizeConstants.Small)]
        [TestCase(CreatureConstants.Spider_Monstrous_Tiny, CreatureConstants.Groups.Spider_Monstrous + ", " + SizeConstants.Tiny)]
        [TestCase(CreatureConstants.Tojanida_Adult, "Adult " + CreatureConstants.Groups.Tojanida)]
        [TestCase(CreatureConstants.Tojanida_Elder, "Elder " + CreatureConstants.Groups.Tojanida)]
        [TestCase(CreatureConstants.Tojanida_Juvenile, "Juvenile " + CreatureConstants.Groups.Tojanida)]
        [TestCase(CreatureConstants.Troll_Scrag, "Scrag")]
        [TestCase(CreatureConstants.UmberHulk_TrulyHorrid, "Truly Horrid " + CreatureConstants.UmberHulk)]
        [TestCase(CreatureConstants.Unicorn_CelestialCharger, "Celestial Charger")]
        [TestCase(CreatureConstants.Whale_Baleen, "Baleen " + CreatureConstants.Groups.Whale)]
        [TestCase(CreatureConstants.Whale_Cachalot, "Cachalot " + CreatureConstants.Groups.Whale)]
        [TestCase(CreatureConstants.Whale_Orca, "Orca " + CreatureConstants.Groups.Whale)]
        [TestCase(CreatureConstants.Wraith_Dread, "Dread " + CreatureConstants.Wraith)]
        [TestCase(CreatureConstants.Xorn_Average, "Average " + CreatureConstants.Groups.Xorn)]
        [TestCase(CreatureConstants.Xorn_Elder, "Elder " + CreatureConstants.Groups.Xorn)]
        [TestCase(CreatureConstants.Xorn_Minor, "Minor " + CreatureConstants.Groups.Xorn)]
        [TestCase(CreatureConstants.YuanTi_Abomination, CreatureConstants.Groups.YuanTi + " Abomination")]
        [TestCase(CreatureConstants.YuanTi_Halfblood, CreatureConstants.Groups.YuanTi + " Halfblood")]
        [TestCase(CreatureConstants.YuanTi_Pureblood, CreatureConstants.Groups.YuanTi + " Pureblood")]
        public void CreatureConstant(string constant, string value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [TestCase(CreatureConstants.Templates.CelestialCreature, "Celestial Creature")]
        [TestCase(CreatureConstants.Templates.FiendishCreature, "Fiendish Creature")]
        [TestCase(CreatureConstants.Templates.Ghost, "Ghost")]
        [TestCase(CreatureConstants.Templates.HalfCelestial, "Half-Celestial")]
        [TestCase(CreatureConstants.Templates.HalfDragon, "Half-Dragon")]
        [TestCase(CreatureConstants.Templates.HalfFiend, "Half-Fiend")]
        [TestCase(CreatureConstants.Templates.Lich, "Lich")]
        [TestCase(CreatureConstants.Templates.None, "")]
        [TestCase(CreatureConstants.Templates.Skeleton, "Skeleton")]
        [TestCase(CreatureConstants.Templates.Vampire, "Vampire")]
        [TestCase(CreatureConstants.Templates.Werebear, "Werebear")]
        [TestCase(CreatureConstants.Templates.Wereboar, "Wereboar")]
        [TestCase(CreatureConstants.Templates.Wererat, "Wererat")]
        [TestCase(CreatureConstants.Templates.Weretiger, "Weretiger")]
        [TestCase(CreatureConstants.Templates.Werewolf, "Werewolf")]
        [TestCase(CreatureConstants.Templates.Zombie, "Zombie")]
        public void CreatureTemplateConstant(string constant, string value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [TestCase(CreatureConstants.Groups.Angel, "Angel")]
        [TestCase(CreatureConstants.Groups.AnimatedObject, "Animated Object")]
        [TestCase(CreatureConstants.Groups.Ant_Giant, "Giant Ant")]
        [TestCase(CreatureConstants.Groups.Archon, "Archon")]
        [TestCase(CreatureConstants.Groups.Arrowhawk, "Arrowhawk")]
        [TestCase(CreatureConstants.Groups.Bear, "Bear")]
        [TestCase(CreatureConstants.Groups.Centipede_Monstrous, "Monstrous Centipede")]
        [TestCase(CreatureConstants.Groups.Cryohydra, "Cryohydra")]
        [TestCase(CreatureConstants.Groups.Demon, "Demon")]
        [TestCase(CreatureConstants.Groups.Devil, "Devil")]
        [TestCase(CreatureConstants.Groups.Dinosaur, "Dinosaur")]
        [TestCase(CreatureConstants.Groups.Dragon_Black, "Black Dragon")]
        [TestCase(CreatureConstants.Groups.Dragon_Blue, "Blue Dragon")]
        [TestCase(CreatureConstants.Groups.Dragon_Brass, "Brass Dragon")]
        [TestCase(CreatureConstants.Groups.Dragon_Bronze, "Bronze Dragon")]
        [TestCase(CreatureConstants.Groups.Dragon_Copper, "Copper Dragon")]
        [TestCase(CreatureConstants.Groups.Dragon_Gold, "Gold Dragon")]
        [TestCase(CreatureConstants.Groups.Dragon_Green, "Green Dragon")]
        [TestCase(CreatureConstants.Groups.Dragon_Red, "Red Dragon")]
        [TestCase(CreatureConstants.Groups.Dragon_Silver, "Silver Dragon")]
        [TestCase(CreatureConstants.Groups.Dragon_White, "White Dragon")]
        [TestCase(CreatureConstants.Groups.Dwarf, "Dwarf")]
        [TestCase(CreatureConstants.Groups.Elemental, "Elemental")]
        [TestCase(CreatureConstants.Groups.Elemental_Air, "Air " + CreatureConstants.Groups.Elemental)]
        [TestCase(CreatureConstants.Groups.Elemental_Earth, "Earth " + CreatureConstants.Groups.Elemental)]
        [TestCase(CreatureConstants.Groups.Elemental_Fire, "Fire " + CreatureConstants.Groups.Elemental)]
        [TestCase(CreatureConstants.Groups.Elemental_Water, "Water " + CreatureConstants.Groups.Elemental)]
        [TestCase(CreatureConstants.Groups.Formian, "Formian")]
        [TestCase(CreatureConstants.Groups.Fungus, "Fungus")]
        [TestCase(CreatureConstants.Groups.Genie, "Genie")]
        [TestCase(CreatureConstants.Groups.Gnome, "Gnome")]
        [TestCase(CreatureConstants.Groups.Golem, "Golem")]
        [TestCase(CreatureConstants.Groups.Hag, "Hag")]
        [TestCase(CreatureConstants.Groups.Halfling, "Halfling")]
        [TestCase(CreatureConstants.Groups.HasSkeleton, "Has Skeleton")]
        [TestCase(CreatureConstants.Groups.Horse, "Horse")]
        [TestCase(CreatureConstants.Groups.Hydra, "Hydra")]
        [TestCase(CreatureConstants.Groups.Inevitable, "Inevitable")]
        [TestCase(CreatureConstants.Groups.Mephit, "Mephit")]
        [TestCase(CreatureConstants.Groups.Naga, "Naga")]
        [TestCase(CreatureConstants.Groups.Nightshade, "Nightshade")]
        [TestCase(CreatureConstants.Groups.Pyrohydra, "Pyrohydra")]
        [TestCase(CreatureConstants.Groups.Salamander, "Salamander")]
        [TestCase(CreatureConstants.Groups.Scorpion_Monstrous, "Monstrous Scorpion")]
        [TestCase(CreatureConstants.Groups.Spider_Monstrous, "Monstrous Spider")]
        [TestCase(CreatureConstants.Groups.Shark, "Shark")]
        [TestCase(CreatureConstants.Groups.Slaad, "Slaad")]
        [TestCase(CreatureConstants.Groups.Snake_Viper, "Viper Snake")]
        [TestCase(CreatureConstants.Groups.Sprite, "Sprite")]
        [TestCase(CreatureConstants.Groups.Tojanida, "Tojanida")]
        [TestCase(CreatureConstants.Groups.Whale, "Whale")]
        [TestCase(CreatureConstants.Groups.Xorn, "Xorn")]
        [TestCase(CreatureConstants.Groups.YuanTi, "Yuan-ti")]
        public void CreatureGroupConstant(string constant, string value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [TestCase(CreatureConstants.Types.Aberration, "Aberration")]
        [TestCase(CreatureConstants.Types.Animal, "Animal")]
        [TestCase(CreatureConstants.Types.Construct, "Construct")]
        [TestCase(CreatureConstants.Types.Dragon, "Dragon")]
        [TestCase(CreatureConstants.Types.Elemental, "Elemental")]
        [TestCase(CreatureConstants.Types.Fey, "Fey")]
        [TestCase(CreatureConstants.Types.Giant, "Giant")]
        [TestCase(CreatureConstants.Types.Humanoid, "Humanoid")]
        [TestCase(CreatureConstants.Types.MagicalBeast, "Magical Beast")]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid, "Monstrous Humanoid")]
        [TestCase(CreatureConstants.Types.Ooze, "Ooze")]
        [TestCase(CreatureConstants.Types.Outsider, "Outsider")]
        [TestCase(CreatureConstants.Types.Plant, "Plant")]
        [TestCase(CreatureConstants.Types.Undead, "Undead")]
        [TestCase(CreatureConstants.Types.Vermin, "Vermin")]
        public void CreatureTypeConstant(string constant, string value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [Test]
        public void AllCreatureConstants()
        {
            var creatures = CreatureConstants.All();

            Assert.That(creatures, Contains.Item(CreatureConstants.Aasimar));
            Assert.That(creatures, Contains.Item(CreatureConstants.Aboleth));
            Assert.That(creatures, Contains.Item(CreatureConstants.Aboleth_Mage));
            Assert.That(creatures, Contains.Item(CreatureConstants.Achaierai));
            Assert.That(creatures, Contains.Item(CreatureConstants.Allip));
            Assert.That(creatures, Contains.Item(CreatureConstants.Androsphinx));
            Assert.That(creatures, Contains.Item(CreatureConstants.AnimatedObject_Colossal));
            Assert.That(creatures, Contains.Item(CreatureConstants.AnimatedObject_Gargantuan));
            Assert.That(creatures, Contains.Item(CreatureConstants.AnimatedObject_Huge));
            Assert.That(creatures, Contains.Item(CreatureConstants.AnimatedObject_Large));
            Assert.That(creatures, Contains.Item(CreatureConstants.AnimatedObject_Medium));
            Assert.That(creatures, Contains.Item(CreatureConstants.AnimatedObject_Small));
            Assert.That(creatures, Contains.Item(CreatureConstants.AnimatedObject_Tiny));
            Assert.That(creatures, Contains.Item(CreatureConstants.Ankheg));
            Assert.That(creatures, Contains.Item(CreatureConstants.Annis));
            Assert.That(creatures, Contains.Item(CreatureConstants.Ant_Giant_Queen));
            Assert.That(creatures, Contains.Item(CreatureConstants.Ant_Giant_Soldier));
            Assert.That(creatures, Contains.Item(CreatureConstants.Ant_Giant_Worker));
            Assert.That(creatures, Contains.Item(CreatureConstants.Ape));
            Assert.That(creatures, Contains.Item(CreatureConstants.Ape_Dire));
            Assert.That(creatures, Contains.Item(CreatureConstants.Aranea));
            Assert.That(creatures, Contains.Item(CreatureConstants.Arrowhawk_Adult));
            Assert.That(creatures, Contains.Item(CreatureConstants.Arrowhawk_Elder));
            Assert.That(creatures, Contains.Item(CreatureConstants.Arrowhawk_Juvenile));
            Assert.That(creatures, Contains.Item(CreatureConstants.AssassinVine));
            Assert.That(creatures, Contains.Item(CreatureConstants.AstralDeva));
            Assert.That(creatures, Contains.Item(CreatureConstants.Athach));
            Assert.That(creatures, Contains.Item(CreatureConstants.Avoral));
            Assert.That(creatures, Contains.Item(CreatureConstants.Azer));
            Assert.That(creatures, Contains.Item(CreatureConstants.Babau));
            Assert.That(creatures, Contains.Item(CreatureConstants.Baboon));
            Assert.That(creatures, Contains.Item(CreatureConstants.Badger));
            Assert.That(creatures, Contains.Item(CreatureConstants.Badger_Dire));
            Assert.That(creatures, Contains.Item(CreatureConstants.Balor));
            Assert.That(creatures, Contains.Item(CreatureConstants.BarbedDevil_Hamatula));
            Assert.That(creatures, Contains.Item(CreatureConstants.Barghest));
            Assert.That(creatures, Contains.Item(CreatureConstants.Barghest_Greater));
            Assert.That(creatures, Contains.Item(CreatureConstants.Basilisk));
            Assert.That(creatures, Contains.Item(CreatureConstants.Basilisk_AbyssalGreater));
            Assert.That(creatures, Contains.Item(CreatureConstants.Bat));
            Assert.That(creatures, Contains.Item(CreatureConstants.Bat_Dire));
            Assert.That(creatures, Contains.Item(CreatureConstants.Bat_Swarm));
            Assert.That(creatures, Contains.Item(CreatureConstants.BeardedDevil_Barbazu));
            Assert.That(creatures, Contains.Item(CreatureConstants.Bear_Black));
            Assert.That(creatures, Contains.Item(CreatureConstants.Bear_Brown));
            Assert.That(creatures, Contains.Item(CreatureConstants.Bear_Dire));
            Assert.That(creatures, Contains.Item(CreatureConstants.Bear_Polar));
            Assert.That(creatures, Contains.Item(CreatureConstants.Bebilith));
            Assert.That(creatures, Contains.Item(CreatureConstants.Bee_Giant));
            Assert.That(creatures, Contains.Item(CreatureConstants.Behir));
            Assert.That(creatures, Contains.Item(CreatureConstants.Beholder));
            Assert.That(creatures, Contains.Item(CreatureConstants.Belker));
            Assert.That(creatures, Contains.Item(CreatureConstants.Bison));
            Assert.That(creatures, Contains.Item(CreatureConstants.BlackPudding));
            Assert.That(creatures, Contains.Item(CreatureConstants.BlackPudding_Elder));
            Assert.That(creatures, Contains.Item(CreatureConstants.BlinkDog));
            Assert.That(creatures, Contains.Item(CreatureConstants.Boar));
            Assert.That(creatures, Contains.Item(CreatureConstants.Boar_Dire));
            Assert.That(creatures, Contains.Item(CreatureConstants.Bodak));
            Assert.That(creatures, Contains.Item(CreatureConstants.BombardierBeetle_Giant));
            Assert.That(creatures, Contains.Item(CreatureConstants.BoneDevil_Osyluth));
            Assert.That(creatures, Contains.Item(CreatureConstants.Bralani));
            Assert.That(creatures, Contains.Item(CreatureConstants.Bugbear));
            Assert.That(creatures, Contains.Item(CreatureConstants.Bulette));
            Assert.That(creatures, Contains.Item(CreatureConstants.Camel));
            Assert.That(creatures, Contains.Item(CreatureConstants.CarrionCrawler));
            Assert.That(creatures, Contains.Item(CreatureConstants.Cat));
            Assert.That(creatures, Contains.Item(CreatureConstants.Centaur));
            Assert.That(creatures, Contains.Item(CreatureConstants.Centipede_Monstrous_Colossal));
            Assert.That(creatures, Contains.Item(CreatureConstants.Centipede_Monstrous_Gargantuan));
            Assert.That(creatures, Contains.Item(CreatureConstants.Centipede_Monstrous_Huge));
            Assert.That(creatures, Contains.Item(CreatureConstants.Centipede_Monstrous_Large));
            Assert.That(creatures, Contains.Item(CreatureConstants.Centipede_Monstrous_Medium));
            Assert.That(creatures, Contains.Item(CreatureConstants.Centipede_Monstrous_Small));
            Assert.That(creatures, Contains.Item(CreatureConstants.Centipede_Monstrous_Tiny));
            Assert.That(creatures, Contains.Item(CreatureConstants.Centipede_Swarm));
            Assert.That(creatures, Contains.Item(CreatureConstants.ChainDevil_Kyton));
            Assert.That(creatures, Contains.Item(CreatureConstants.ChaosBeast));
            Assert.That(creatures, Contains.Item(CreatureConstants.Cheetah));
            Assert.That(creatures, Contains.Item(CreatureConstants.Chimera));
            Assert.That(creatures, Contains.Item(CreatureConstants.Choker));
            Assert.That(creatures, Contains.Item(CreatureConstants.Chuul));
            Assert.That(creatures, Contains.Item(CreatureConstants.Cloaker));
            Assert.That(creatures, Contains.Item(CreatureConstants.Cockatrice));
            Assert.That(creatures, Contains.Item(CreatureConstants.Couatl));
            Assert.That(creatures, Contains.Item(CreatureConstants.Criosphinx));
            Assert.That(creatures, Contains.Item(CreatureConstants.Crocodile));
            Assert.That(creatures, Contains.Item(CreatureConstants.Crocodile_Giant));
            Assert.That(creatures, Contains.Item(CreatureConstants.Cryohydra_10Heads));
            Assert.That(creatures, Contains.Item(CreatureConstants.Cryohydra_11Heads));
            Assert.That(creatures, Contains.Item(CreatureConstants.Cryohydra_12Heads));
            Assert.That(creatures, Contains.Item(CreatureConstants.Cryohydra_5Heads));
            Assert.That(creatures, Contains.Item(CreatureConstants.Cryohydra_6Heads));
            Assert.That(creatures, Contains.Item(CreatureConstants.Cryohydra_7Heads));
            Assert.That(creatures, Contains.Item(CreatureConstants.Cryohydra_8Heads));
            Assert.That(creatures, Contains.Item(CreatureConstants.Cryohydra_9Heads));
            Assert.That(creatures, Contains.Item(CreatureConstants.Darkmantle));
            Assert.That(creatures, Contains.Item(CreatureConstants.Deinonychus));
            Assert.That(creatures, Contains.Item(CreatureConstants.Delver));
            Assert.That(creatures, Contains.Item(CreatureConstants.Derro));
            Assert.That(creatures, Contains.Item(CreatureConstants.Destrachan));
            Assert.That(creatures, Contains.Item(CreatureConstants.Devourer));
            Assert.That(creatures, Contains.Item(CreatureConstants.Digester));
            Assert.That(creatures, Contains.Item(CreatureConstants.DisplacerBeast));
            Assert.That(creatures, Contains.Item(CreatureConstants.DisplacerBeast_PackLord));
            Assert.That(creatures, Contains.Item(CreatureConstants.Djinni));
            Assert.That(creatures, Contains.Item(CreatureConstants.Djinni_Noble));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dog));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dog_Riding));
            Assert.That(creatures, Contains.Item(CreatureConstants.Donkey));
            Assert.That(creatures, Contains.Item(CreatureConstants.Doppelganger));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragonne));
            Assert.That(creatures, Contains.Item(CreatureConstants.DragonTurtle));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Black_Adult));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Black_Ancient));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Black_GreatWyrm));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Black_Juvenile));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Black_MatureAdult));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Black_Old));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Black_VeryOld));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Black_VeryYoung));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Black_Wyrm));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Black_Wyrmling));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Black_Young));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Black_YoungAdult));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Blue_Adult));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Blue_Ancient));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Blue_GreatWyrm));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Blue_Juvenile));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Blue_MatureAdult));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Blue_Old));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Blue_VeryOld));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Blue_VeryYoung));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Blue_Wyrm));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Blue_Wyrmling));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Blue_Young));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Blue_YoungAdult));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Brass_Adult));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Brass_Ancient));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Brass_GreatWyrm));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Brass_Juvenile));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Brass_MatureAdult));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Brass_Old));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Brass_VeryOld));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Brass_VeryYoung));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Brass_Wyrm));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Brass_Wyrmling));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Brass_Young));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Brass_YoungAdult));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Bronze_Adult));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Bronze_Ancient));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Bronze_GreatWyrm));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Bronze_Juvenile));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Bronze_MatureAdult));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Bronze_Old));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Bronze_VeryOld));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Bronze_VeryYoung));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Bronze_Wyrm));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Bronze_Wyrmling));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Bronze_Young));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Bronze_YoungAdult));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Copper_Adult));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Copper_Ancient));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Copper_GreatWyrm));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Copper_Juvenile));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Copper_MatureAdult));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Copper_Old));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Copper_VeryOld));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Copper_VeryYoung));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Copper_Wyrm));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Copper_Wyrmling));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Copper_Young));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Copper_YoungAdult));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Green_Adult));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Green_Ancient));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Green_GreatWyrm));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Green_Juvenile));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Green_MatureAdult));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Green_Old));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Green_VeryOld));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Green_VeryYoung));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Green_Wyrm));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Green_Wyrmling));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Green_Young));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Green_YoungAdult));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Gold_Adult));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Gold_Ancient));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Gold_GreatWyrm));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Gold_Juvenile));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Gold_MatureAdult));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Gold_Old));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Gold_VeryOld));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Gold_VeryYoung));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Gold_Wyrm));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Gold_Wyrmling));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Gold_Young));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Gold_YoungAdult));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Red_Adult));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Red_Ancient));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Red_GreatWyrm));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Red_Juvenile));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Red_MatureAdult));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Red_Old));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Red_VeryOld));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Red_VeryYoung));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Red_Wyrm));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Red_Wyrmling));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Red_Young));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Red_YoungAdult));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Silver_Adult));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Silver_Ancient));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Silver_GreatWyrm));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Silver_Juvenile));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Silver_MatureAdult));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Silver_Old));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Silver_VeryOld));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Silver_VeryYoung));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Silver_Wyrm));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Silver_Wyrmling));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Silver_Young));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_Silver_YoungAdult));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_White_Adult));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_White_Ancient));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_White_GreatWyrm));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_White_Juvenile));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_White_MatureAdult));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_White_Old));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_White_VeryOld));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_White_VeryYoung));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_White_Wyrm));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_White_Wyrmling));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_White_Young));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dragon_White_YoungAdult));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dretch));
            Assert.That(creatures, Contains.Item(CreatureConstants.Drider));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dryad));
            Assert.That(creatures, Contains.Item(CreatureConstants.Duergar));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dwarf_Deep));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dwarf_Hill));
            Assert.That(creatures, Contains.Item(CreatureConstants.Dwarf_Mountain));
            Assert.That(creatures, Contains.Item(CreatureConstants.Eagle));
            Assert.That(creatures, Contains.Item(CreatureConstants.Eagle_Giant));
            Assert.That(creatures, Contains.Item(CreatureConstants.Efreeti));
            Assert.That(creatures, Contains.Item(CreatureConstants.Elasmosaurus));
            Assert.That(creatures, Contains.Item(CreatureConstants.Elemental_Air_Elder));
            Assert.That(creatures, Contains.Item(CreatureConstants.Elemental_Air_Greater));
            Assert.That(creatures, Contains.Item(CreatureConstants.Elemental_Air_Huge));
            Assert.That(creatures, Contains.Item(CreatureConstants.Elemental_Air_Large));
            Assert.That(creatures, Contains.Item(CreatureConstants.Elemental_Air_Medium));
            Assert.That(creatures, Contains.Item(CreatureConstants.Elemental_Air_Small));
            Assert.That(creatures, Contains.Item(CreatureConstants.Elemental_Earth_Elder));
            Assert.That(creatures, Contains.Item(CreatureConstants.Elemental_Earth_Greater));
            Assert.That(creatures, Contains.Item(CreatureConstants.Elemental_Earth_Huge));
            Assert.That(creatures, Contains.Item(CreatureConstants.Elemental_Earth_Large));
            Assert.That(creatures, Contains.Item(CreatureConstants.Elemental_Earth_Medium));
            Assert.That(creatures, Contains.Item(CreatureConstants.Elemental_Earth_Small));
            Assert.That(creatures, Contains.Item(CreatureConstants.Elemental_Fire_Elder));
            Assert.That(creatures, Contains.Item(CreatureConstants.Elemental_Fire_Greater));
            Assert.That(creatures, Contains.Item(CreatureConstants.Elemental_Fire_Huge));
            Assert.That(creatures, Contains.Item(CreatureConstants.Elemental_Fire_Large));
            Assert.That(creatures, Contains.Item(CreatureConstants.Elemental_Fire_Medium));
            Assert.That(creatures, Contains.Item(CreatureConstants.Elemental_Fire_Small));
            Assert.That(creatures, Contains.Item(CreatureConstants.Elemental_Water_Elder));
            Assert.That(creatures, Contains.Item(CreatureConstants.Elemental_Water_Greater));
            Assert.That(creatures, Contains.Item(CreatureConstants.Elemental_Water_Huge));
            Assert.That(creatures, Contains.Item(CreatureConstants.Elemental_Water_Large));
            Assert.That(creatures, Contains.Item(CreatureConstants.Elemental_Water_Medium));
            Assert.That(creatures, Contains.Item(CreatureConstants.Elemental_Water_Small));
            Assert.That(creatures, Contains.Item(CreatureConstants.Elephant));
            Assert.That(creatures, Contains.Item(CreatureConstants.Elf_Aquatic));
            Assert.That(creatures, Contains.Item(CreatureConstants.Elf_Drow));
            Assert.That(creatures, Contains.Item(CreatureConstants.Elf_Gray));
            Assert.That(creatures, Contains.Item(CreatureConstants.Elf_Half));
            Assert.That(creatures, Contains.Item(CreatureConstants.Elf_High));
            Assert.That(creatures, Contains.Item(CreatureConstants.Elf_Wild));
            Assert.That(creatures, Contains.Item(CreatureConstants.Elf_Wood));
            Assert.That(creatures, Contains.Item(CreatureConstants.Erinyes));
            Assert.That(creatures, Contains.Item(CreatureConstants.EtherealFilcher));
            Assert.That(creatures, Contains.Item(CreatureConstants.EtherealMarauder));
            Assert.That(creatures, Contains.Item(CreatureConstants.Ettercap));
            Assert.That(creatures, Contains.Item(CreatureConstants.Ettin));
            Assert.That(creatures, Contains.Item(CreatureConstants.FireBeetle_Giant));
            Assert.That(creatures, Contains.Item(CreatureConstants.FormianMyrmarch));
            Assert.That(creatures, Contains.Item(CreatureConstants.FormianQueen));
            Assert.That(creatures, Contains.Item(CreatureConstants.FormianTaskmaster));
            Assert.That(creatures, Contains.Item(CreatureConstants.FormianWarrior));
            Assert.That(creatures, Contains.Item(CreatureConstants.FormianWorker));
            Assert.That(creatures, Contains.Item(CreatureConstants.FrostWorm));
            Assert.That(creatures, Contains.Item(CreatureConstants.Gargoyle));
            Assert.That(creatures, Contains.Item(CreatureConstants.Gargoyle_Kapoacinth));
            Assert.That(creatures, Contains.Item(CreatureConstants.GelatinousCube));
            Assert.That(creatures, Contains.Item(CreatureConstants.Ghaele));
            Assert.That(creatures, Contains.Item(CreatureConstants.Ghoul));
            Assert.That(creatures, Contains.Item(CreatureConstants.Ghoul_Ghast));
            Assert.That(creatures, Contains.Item(CreatureConstants.Ghoul_Lacedon));
            Assert.That(creatures, Contains.Item(CreatureConstants.Giant_Cloud));
            Assert.That(creatures, Contains.Item(CreatureConstants.Giant_Fire));
            Assert.That(creatures, Contains.Item(CreatureConstants.Giant_Frost));
            Assert.That(creatures, Contains.Item(CreatureConstants.Giant_Hill));
            Assert.That(creatures, Contains.Item(CreatureConstants.Giant_Stone));
            Assert.That(creatures, Contains.Item(CreatureConstants.Giant_Stone_Elder));
            Assert.That(creatures, Contains.Item(CreatureConstants.Giant_Storm));
            Assert.That(creatures, Contains.Item(CreatureConstants.GibberingMouther));
            Assert.That(creatures, Contains.Item(CreatureConstants.Girallon));
            Assert.That(creatures, Contains.Item(CreatureConstants.Githyanki));
            Assert.That(creatures, Contains.Item(CreatureConstants.Githzerai));
            Assert.That(creatures, Contains.Item(CreatureConstants.Glabrezu));
            Assert.That(creatures, Contains.Item(CreatureConstants.Gnoll));
            Assert.That(creatures, Contains.Item(CreatureConstants.Gnome_Forest));
            Assert.That(creatures, Contains.Item(CreatureConstants.Gnome_Rock));
            Assert.That(creatures, Contains.Item(CreatureConstants.Gnome_Svirfneblin));
            Assert.That(creatures, Contains.Item(CreatureConstants.Goblin));
            Assert.That(creatures, Contains.Item(CreatureConstants.Golem_Clay));
            Assert.That(creatures, Contains.Item(CreatureConstants.Golem_Flesh));
            Assert.That(creatures, Contains.Item(CreatureConstants.Golem_Iron));
            Assert.That(creatures, Contains.Item(CreatureConstants.Golem_Stone));
            Assert.That(creatures, Contains.Item(CreatureConstants.Golem_Stone_Greater));
            Assert.That(creatures, Contains.Item(CreatureConstants.Gorgon));
            Assert.That(creatures, Contains.Item(CreatureConstants.GrayRender));
            Assert.That(creatures, Contains.Item(CreatureConstants.GreenHag));
            Assert.That(creatures, Contains.Item(CreatureConstants.Grick));
            Assert.That(creatures, Contains.Item(CreatureConstants.Griffon));
            Assert.That(creatures, Contains.Item(CreatureConstants.Grig));
            Assert.That(creatures, Contains.Item(CreatureConstants.Grimlock));
            Assert.That(creatures, Contains.Item(CreatureConstants.Gynosphinx));
            Assert.That(creatures, Contains.Item(CreatureConstants.Halfling_Deep));
            Assert.That(creatures, Contains.Item(CreatureConstants.Halfling_Lightfoot));
            Assert.That(creatures, Contains.Item(CreatureConstants.Halfling_Tallfellow));
            Assert.That(creatures, Contains.Item(CreatureConstants.Harpy));
            Assert.That(creatures, Contains.Item(CreatureConstants.Hawk));
            Assert.That(creatures, Contains.Item(CreatureConstants.Hellcat_Bezekira));
            Assert.That(creatures, Contains.Item(CreatureConstants.HellHound));
            Assert.That(creatures, Contains.Item(CreatureConstants.Hellwasp_Swarm));
            Assert.That(creatures, Contains.Item(CreatureConstants.Hezrou));
            Assert.That(creatures, Contains.Item(CreatureConstants.Hieracosphinx));
            Assert.That(creatures, Contains.Item(CreatureConstants.Hippogriff));
            Assert.That(creatures, Contains.Item(CreatureConstants.Hobgoblin));
            Assert.That(creatures, Contains.Item(CreatureConstants.Homunculus));
            Assert.That(creatures, Contains.Item(CreatureConstants.HornedDevil_Cornugon));
            Assert.That(creatures, Contains.Item(CreatureConstants.Horse_Heavy));
            Assert.That(creatures, Contains.Item(CreatureConstants.Horse_Heavy_War));
            Assert.That(creatures, Contains.Item(CreatureConstants.Horse_Light));
            Assert.That(creatures, Contains.Item(CreatureConstants.Horse_Light_War));
            Assert.That(creatures, Contains.Item(CreatureConstants.HoundArchon));
            Assert.That(creatures, Contains.Item(CreatureConstants.Howler));
            Assert.That(creatures, Contains.Item(CreatureConstants.Human));
            Assert.That(creatures, Contains.Item(CreatureConstants.Hydra_10Heads));
            Assert.That(creatures, Contains.Item(CreatureConstants.Hydra_11Heads));
            Assert.That(creatures, Contains.Item(CreatureConstants.Hydra_12Heads));
            Assert.That(creatures, Contains.Item(CreatureConstants.Hydra_5Heads));
            Assert.That(creatures, Contains.Item(CreatureConstants.Hydra_6Heads));
            Assert.That(creatures, Contains.Item(CreatureConstants.Hydra_7Heads));
            Assert.That(creatures, Contains.Item(CreatureConstants.Hydra_8Heads));
            Assert.That(creatures, Contains.Item(CreatureConstants.Hydra_9Heads));
            Assert.That(creatures, Contains.Item(CreatureConstants.Hyena));
            Assert.That(creatures, Contains.Item(CreatureConstants.IceDevil_Gelugon));
            Assert.That(creatures, Contains.Item(CreatureConstants.Imp));
            Assert.That(creatures, Contains.Item(CreatureConstants.InvisibleStalker));
            Assert.That(creatures, Contains.Item(CreatureConstants.Janni));
            Assert.That(creatures, Contains.Item(CreatureConstants.Kobold));
            Assert.That(creatures, Contains.Item(CreatureConstants.Kolyarut));
            Assert.That(creatures, Contains.Item(CreatureConstants.Kraken));
            Assert.That(creatures, Contains.Item(CreatureConstants.Krenshar));
            Assert.That(creatures, Contains.Item(CreatureConstants.KuoToa));
            Assert.That(creatures, Contains.Item(CreatureConstants.Lamia));
            Assert.That(creatures, Contains.Item(CreatureConstants.Lammasu));
            Assert.That(creatures, Contains.Item(CreatureConstants.Lammasu_GoldenProtector));
            Assert.That(creatures, Contains.Item(CreatureConstants.LanternArchon));
            Assert.That(creatures, Contains.Item(CreatureConstants.Lemure));
            Assert.That(creatures, Contains.Item(CreatureConstants.Leonal));
            Assert.That(creatures, Contains.Item(CreatureConstants.Leopard));
            Assert.That(creatures, Contains.Item(CreatureConstants.Lillend));
            Assert.That(creatures, Contains.Item(CreatureConstants.Lion));
            Assert.That(creatures, Contains.Item(CreatureConstants.Lion_Dire));
            Assert.That(creatures, Contains.Item(CreatureConstants.Lizard));
            Assert.That(creatures, Contains.Item(CreatureConstants.Lizardfolk));
            Assert.That(creatures, Contains.Item(CreatureConstants.Lizard_Monitor));
            Assert.That(creatures, Contains.Item(CreatureConstants.Locathah));
            Assert.That(creatures, Contains.Item(CreatureConstants.Locust_Swarm));
            Assert.That(creatures, Contains.Item(CreatureConstants.Magmin));
            Assert.That(creatures, Contains.Item(CreatureConstants.MantaRay));
            Assert.That(creatures, Contains.Item(CreatureConstants.Manticore));
            Assert.That(creatures, Contains.Item(CreatureConstants.Marilith));
            Assert.That(creatures, Contains.Item(CreatureConstants.Marut));
            Assert.That(creatures, Contains.Item(CreatureConstants.Medusa));
            Assert.That(creatures, Contains.Item(CreatureConstants.Megaraptor));
            Assert.That(creatures, Contains.Item(CreatureConstants.Mephit_Air));
            Assert.That(creatures, Contains.Item(CreatureConstants.Mephit_Dust));
            Assert.That(creatures, Contains.Item(CreatureConstants.Mephit_Earth));
            Assert.That(creatures, Contains.Item(CreatureConstants.Mephit_Fire));
            Assert.That(creatures, Contains.Item(CreatureConstants.Mephit_Ice));
            Assert.That(creatures, Contains.Item(CreatureConstants.Mephit_Magma));
            Assert.That(creatures, Contains.Item(CreatureConstants.Mephit_Ooze));
            Assert.That(creatures, Contains.Item(CreatureConstants.Mephit_Salt));
            Assert.That(creatures, Contains.Item(CreatureConstants.Mephit_Steam));
            Assert.That(creatures, Contains.Item(CreatureConstants.Mephit_Water));
            Assert.That(creatures, Contains.Item(CreatureConstants.Merfolk));
            Assert.That(creatures, Contains.Item(CreatureConstants.Mimic));
            Assert.That(creatures, Contains.Item(CreatureConstants.MindFlayer));
            Assert.That(creatures, Contains.Item(CreatureConstants.Minotaur));
            Assert.That(creatures, Contains.Item(CreatureConstants.Mohrg));
            Assert.That(creatures, Contains.Item(CreatureConstants.Monkey));
            Assert.That(creatures, Contains.Item(CreatureConstants.Mule));
            Assert.That(creatures, Contains.Item(CreatureConstants.Mummy));
            Assert.That(creatures, Contains.Item(CreatureConstants.Naga_Dark));
            Assert.That(creatures, Contains.Item(CreatureConstants.Naga_Guardian));
            Assert.That(creatures, Contains.Item(CreatureConstants.Naga_Spirit));
            Assert.That(creatures, Contains.Item(CreatureConstants.Naga_Water));
            Assert.That(creatures, Contains.Item(CreatureConstants.Nalfeshnee));
            Assert.That(creatures, Contains.Item(CreatureConstants.NessianWarhound));
            Assert.That(creatures, Contains.Item(CreatureConstants.Nightcrawler));
            Assert.That(creatures, Contains.Item(CreatureConstants.NightHag));
            Assert.That(creatures, Contains.Item(CreatureConstants.Nightmare));
            Assert.That(creatures, Contains.Item(CreatureConstants.Nightmare_Cauchemar));
            Assert.That(creatures, Contains.Item(CreatureConstants.Nightwalker));
            Assert.That(creatures, Contains.Item(CreatureConstants.Nightwing));
            Assert.That(creatures, Contains.Item(CreatureConstants.Nixie));
            Assert.That(creatures, Contains.Item(CreatureConstants.Nymph));
            Assert.That(creatures, Contains.Item(CreatureConstants.Octopus));
            Assert.That(creatures, Contains.Item(CreatureConstants.Octopus_Giant));
            Assert.That(creatures, Contains.Item(CreatureConstants.Ogre));
            Assert.That(creatures, Contains.Item(CreatureConstants.OgreMage));
            Assert.That(creatures, Contains.Item(CreatureConstants.Ogre_Merrow));
            Assert.That(creatures, Contains.Item(CreatureConstants.Ooze_Gray));
            Assert.That(creatures, Contains.Item(CreatureConstants.Ooze_OchreJelly));
            Assert.That(creatures, Contains.Item(CreatureConstants.Orc));
            Assert.That(creatures, Contains.Item(CreatureConstants.Orc_Half));
            Assert.That(creatures, Contains.Item(CreatureConstants.Otyugh));
            Assert.That(creatures, Contains.Item(CreatureConstants.Owl));
            Assert.That(creatures, Contains.Item(CreatureConstants.Owlbear));
            Assert.That(creatures, Contains.Item(CreatureConstants.Owl_Giant));
            Assert.That(creatures, Contains.Item(CreatureConstants.Pegasus));
            Assert.That(creatures, Contains.Item(CreatureConstants.PhantomFungus));
            Assert.That(creatures, Contains.Item(CreatureConstants.PhaseSpider));
            Assert.That(creatures, Contains.Item(CreatureConstants.Phasm));
            Assert.That(creatures, Contains.Item(CreatureConstants.PitFiend));
            Assert.That(creatures, Contains.Item(CreatureConstants.Pixie));
            Assert.That(creatures, Contains.Item(CreatureConstants.Pixie_WithIrresistableDance));
            Assert.That(creatures, Contains.Item(CreatureConstants.Planetar));
            Assert.That(creatures, Contains.Item(CreatureConstants.Pony));
            Assert.That(creatures, Contains.Item(CreatureConstants.Pony_War));
            Assert.That(creatures, Contains.Item(CreatureConstants.Porpoise));
            Assert.That(creatures, Contains.Item(CreatureConstants.PrayingMantis_Giant));
            Assert.That(creatures, Contains.Item(CreatureConstants.Pseudodragon));
            Assert.That(creatures, Contains.Item(CreatureConstants.PurpleWorm));
            Assert.That(creatures, Contains.Item(CreatureConstants.Pyrohydra_10Heads));
            Assert.That(creatures, Contains.Item(CreatureConstants.Pyrohydra_11Heads));
            Assert.That(creatures, Contains.Item(CreatureConstants.Pyrohydra_12Heads));
            Assert.That(creatures, Contains.Item(CreatureConstants.Pyrohydra_5Heads));
            Assert.That(creatures, Contains.Item(CreatureConstants.Pyrohydra_6Heads));
            Assert.That(creatures, Contains.Item(CreatureConstants.Pyrohydra_7Heads));
            Assert.That(creatures, Contains.Item(CreatureConstants.Pyrohydra_8Heads));
            Assert.That(creatures, Contains.Item(CreatureConstants.Pyrohydra_9Heads));
            Assert.That(creatures, Contains.Item(CreatureConstants.Quasit));
            Assert.That(creatures, Contains.Item(CreatureConstants.Rakshasa));
            Assert.That(creatures, Contains.Item(CreatureConstants.Rast));
            Assert.That(creatures, Contains.Item(CreatureConstants.Rat));
            Assert.That(creatures, Contains.Item(CreatureConstants.Rat_Dire));
            Assert.That(creatures, Contains.Item(CreatureConstants.Rat_Swarm));
            Assert.That(creatures, Contains.Item(CreatureConstants.Raven));
            Assert.That(creatures, Contains.Item(CreatureConstants.Ravid));
            Assert.That(creatures, Contains.Item(CreatureConstants.RazorBoar));
            Assert.That(creatures, Contains.Item(CreatureConstants.Remorhaz));
            Assert.That(creatures, Contains.Item(CreatureConstants.Retriever));
            Assert.That(creatures, Contains.Item(CreatureConstants.Rhinoceras));
            Assert.That(creatures, Contains.Item(CreatureConstants.Roc));
            Assert.That(creatures, Contains.Item(CreatureConstants.Roper));
            Assert.That(creatures, Contains.Item(CreatureConstants.RustMonster));
            Assert.That(creatures, Contains.Item(CreatureConstants.Sahuagin));
            Assert.That(creatures, Contains.Item(CreatureConstants.Salamander_Average));
            Assert.That(creatures, Contains.Item(CreatureConstants.Salamander_Flamebrother));
            Assert.That(creatures, Contains.Item(CreatureConstants.Salamander_Noble));
            Assert.That(creatures, Contains.Item(CreatureConstants.Satyr));
            Assert.That(creatures, Contains.Item(CreatureConstants.Satyr_WithPipes));
            Assert.That(creatures, Contains.Item(CreatureConstants.Scorpionfolk));
            Assert.That(creatures, Contains.Item(CreatureConstants.Scorpion_Monstrous_Colossal));
            Assert.That(creatures, Contains.Item(CreatureConstants.Scorpion_Monstrous_Gargantuan));
            Assert.That(creatures, Contains.Item(CreatureConstants.Scorpion_Monstrous_Huge));
            Assert.That(creatures, Contains.Item(CreatureConstants.Scorpion_Monstrous_Large));
            Assert.That(creatures, Contains.Item(CreatureConstants.Scorpion_Monstrous_Medium));
            Assert.That(creatures, Contains.Item(CreatureConstants.Scorpion_Monstrous_Small));
            Assert.That(creatures, Contains.Item(CreatureConstants.Scorpion_Monstrous_Tiny));
            Assert.That(creatures, Contains.Item(CreatureConstants.SeaCat));
            Assert.That(creatures, Contains.Item(CreatureConstants.SeaHag));
            Assert.That(creatures, Contains.Item(CreatureConstants.Shadow));
            Assert.That(creatures, Contains.Item(CreatureConstants.ShadowMastiff));
            Assert.That(creatures, Contains.Item(CreatureConstants.Shadow_Greater));
            Assert.That(creatures, Contains.Item(CreatureConstants.ShamblingMound));
            Assert.That(creatures, Contains.Item(CreatureConstants.Shark_Dire));
            Assert.That(creatures, Contains.Item(CreatureConstants.Shark_Huge));
            Assert.That(creatures, Contains.Item(CreatureConstants.Shark_Large));
            Assert.That(creatures, Contains.Item(CreatureConstants.Shark_Medium));
            Assert.That(creatures, Contains.Item(CreatureConstants.ShieldGuardian));
            Assert.That(creatures, Contains.Item(CreatureConstants.ShockerLizard));
            Assert.That(creatures, Contains.Item(CreatureConstants.Shrieker));
            Assert.That(creatures, Contains.Item(CreatureConstants.Skum));
            Assert.That(creatures, Contains.Item(CreatureConstants.Slaad_Blue));
            Assert.That(creatures, Contains.Item(CreatureConstants.Slaad_Death));
            Assert.That(creatures, Contains.Item(CreatureConstants.Slaad_Gray));
            Assert.That(creatures, Contains.Item(CreatureConstants.Slaad_Green));
            Assert.That(creatures, Contains.Item(CreatureConstants.Slaad_Red));
            Assert.That(creatures, Contains.Item(CreatureConstants.Snake_Constrictor));
            Assert.That(creatures, Contains.Item(CreatureConstants.Snake_Constrictor_Giant));
            Assert.That(creatures, Contains.Item(CreatureConstants.Snake_Viper_Huge));
            Assert.That(creatures, Contains.Item(CreatureConstants.Snake_Viper_Large));
            Assert.That(creatures, Contains.Item(CreatureConstants.Snake_Viper_Medium));
            Assert.That(creatures, Contains.Item(CreatureConstants.Snake_Viper_Small));
            Assert.That(creatures, Contains.Item(CreatureConstants.Snake_Viper_Tiny));
            Assert.That(creatures, Contains.Item(CreatureConstants.Solar));
            Assert.That(creatures, Contains.Item(CreatureConstants.Spectre));
            Assert.That(creatures, Contains.Item(CreatureConstants.SpiderEater));
            Assert.That(creatures, Contains.Item(CreatureConstants.Spider_Monstrous_Colossal));
            Assert.That(creatures, Contains.Item(CreatureConstants.Spider_Monstrous_Gargantuan));
            Assert.That(creatures, Contains.Item(CreatureConstants.Spider_Monstrous_Huge));
            Assert.That(creatures, Contains.Item(CreatureConstants.Spider_Monstrous_Large));
            Assert.That(creatures, Contains.Item(CreatureConstants.Spider_Monstrous_Medium));
            Assert.That(creatures, Contains.Item(CreatureConstants.Spider_Monstrous_Small));
            Assert.That(creatures, Contains.Item(CreatureConstants.Spider_Monstrous_Tiny));
            Assert.That(creatures, Contains.Item(CreatureConstants.Spider_Swarm));
            Assert.That(creatures, Contains.Item(CreatureConstants.Squid));
            Assert.That(creatures, Contains.Item(CreatureConstants.Squid_Giant));
            Assert.That(creatures, Contains.Item(CreatureConstants.StagBeetle_Giant));
            Assert.That(creatures, Contains.Item(CreatureConstants.Stirge));
            Assert.That(creatures, Contains.Item(CreatureConstants.Succubus));
            Assert.That(creatures, Contains.Item(CreatureConstants.Tarrasque));
            Assert.That(creatures, Contains.Item(CreatureConstants.Tendriculos));
            Assert.That(creatures, Contains.Item(CreatureConstants.Thoqqua));
            Assert.That(creatures, Contains.Item(CreatureConstants.Tiefling));
            Assert.That(creatures, Contains.Item(CreatureConstants.Tiger));
            Assert.That(creatures, Contains.Item(CreatureConstants.Tiger_Dire));
            Assert.That(creatures, Contains.Item(CreatureConstants.Titan));
            Assert.That(creatures, Contains.Item(CreatureConstants.Toad));
            Assert.That(creatures, Contains.Item(CreatureConstants.Tojanida_Adult));
            Assert.That(creatures, Contains.Item(CreatureConstants.Tojanida_Elder));
            Assert.That(creatures, Contains.Item(CreatureConstants.Tojanida_Juvenile));
            Assert.That(creatures, Contains.Item(CreatureConstants.Treant));
            Assert.That(creatures, Contains.Item(CreatureConstants.Triceratops));
            Assert.That(creatures, Contains.Item(CreatureConstants.Triton));
            Assert.That(creatures, Contains.Item(CreatureConstants.Troglodyte));
            Assert.That(creatures, Contains.Item(CreatureConstants.Troll));
            Assert.That(creatures, Contains.Item(CreatureConstants.Troll_Scrag));
            Assert.That(creatures, Contains.Item(CreatureConstants.TrumpetArchon));
            Assert.That(creatures, Contains.Item(CreatureConstants.Tyrannosaurus));
            Assert.That(creatures, Contains.Item(CreatureConstants.UmberHulk));
            Assert.That(creatures, Contains.Item(CreatureConstants.UmberHulk_TrulyHorrid));
            Assert.That(creatures, Contains.Item(CreatureConstants.Unicorn));
            Assert.That(creatures, Contains.Item(CreatureConstants.Unicorn_CelestialCharger));
            Assert.That(creatures, Contains.Item(CreatureConstants.VampireSpawn));
            Assert.That(creatures, Contains.Item(CreatureConstants.Vargouille));
            Assert.That(creatures, Contains.Item(CreatureConstants.VioletFungus));
            Assert.That(creatures, Contains.Item(CreatureConstants.Vrock));
            Assert.That(creatures, Contains.Item(CreatureConstants.Wasp_Giant));
            Assert.That(creatures, Contains.Item(CreatureConstants.Weasel));
            Assert.That(creatures, Contains.Item(CreatureConstants.Weasel_Dire));
            Assert.That(creatures, Contains.Item(CreatureConstants.Werebear));
            Assert.That(creatures, Contains.Item(CreatureConstants.Wereboar));
            Assert.That(creatures, Contains.Item(CreatureConstants.Wereboar_HillGiantDire));
            Assert.That(creatures, Contains.Item(CreatureConstants.Wererat));
            Assert.That(creatures, Contains.Item(CreatureConstants.Weretiger));
            Assert.That(creatures, Contains.Item(CreatureConstants.Werewolf));
            Assert.That(creatures, Contains.Item(CreatureConstants.Whale_Baleen));
            Assert.That(creatures, Contains.Item(CreatureConstants.Whale_Cachalot));
            Assert.That(creatures, Contains.Item(CreatureConstants.Whale_Orca));
            Assert.That(creatures, Contains.Item(CreatureConstants.Wight));
            Assert.That(creatures, Contains.Item(CreatureConstants.WillOWisp));
            Assert.That(creatures, Contains.Item(CreatureConstants.WinterWolf));
            Assert.That(creatures, Contains.Item(CreatureConstants.Wolf));
            Assert.That(creatures, Contains.Item(CreatureConstants.Wolf_Dire));
            Assert.That(creatures, Contains.Item(CreatureConstants.Wolverine));
            Assert.That(creatures, Contains.Item(CreatureConstants.Wolverine_Dire));
            Assert.That(creatures, Contains.Item(CreatureConstants.Worg));
            Assert.That(creatures, Contains.Item(CreatureConstants.Wraith));
            Assert.That(creatures, Contains.Item(CreatureConstants.Wraith_Dread));
            Assert.That(creatures, Contains.Item(CreatureConstants.Wyvern));
            Assert.That(creatures, Contains.Item(CreatureConstants.Xill));
            Assert.That(creatures, Contains.Item(CreatureConstants.Xorn_Average));
            Assert.That(creatures, Contains.Item(CreatureConstants.Xorn_Elder));
            Assert.That(creatures, Contains.Item(CreatureConstants.Xorn_Minor));
            Assert.That(creatures, Contains.Item(CreatureConstants.YethHound));
            Assert.That(creatures, Contains.Item(CreatureConstants.Yrthak));
            Assert.That(creatures, Contains.Item(CreatureConstants.YuanTi_Abomination));
            Assert.That(creatures, Contains.Item(CreatureConstants.YuanTi_Halfblood));
            Assert.That(creatures, Contains.Item(CreatureConstants.YuanTi_Pureblood));
            Assert.That(creatures, Contains.Item(CreatureConstants.Zelekhut));
            Assert.That(creatures.Count(), Is.EqualTo(584));
        }

        [Test]
        public void AllTemplates()
        {
            var templates = CreatureConstants.Templates.All();

            Assert.That(templates, Contains.Item(CreatureConstants.Templates.CelestialCreature));
            Assert.That(templates, Contains.Item(CreatureConstants.Templates.FiendishCreature));
            Assert.That(templates, Contains.Item(CreatureConstants.Templates.Ghost));
            Assert.That(templates, Contains.Item(CreatureConstants.Templates.HalfCelestial));
            Assert.That(templates, Contains.Item(CreatureConstants.Templates.HalfDragon));
            Assert.That(templates, Contains.Item(CreatureConstants.Templates.HalfFiend));
            Assert.That(templates, Contains.Item(CreatureConstants.Templates.Lich));
            Assert.That(templates, Contains.Item(CreatureConstants.Templates.None));
            Assert.That(templates, Contains.Item(CreatureConstants.Templates.Skeleton));
            Assert.That(templates, Contains.Item(CreatureConstants.Templates.Vampire));
            Assert.That(templates, Contains.Item(CreatureConstants.Templates.Werebear));
            Assert.That(templates, Contains.Item(CreatureConstants.Templates.Wereboar));
            Assert.That(templates, Contains.Item(CreatureConstants.Templates.Wererat));
            Assert.That(templates, Contains.Item(CreatureConstants.Templates.Weretiger));
            Assert.That(templates, Contains.Item(CreatureConstants.Templates.Werewolf));
            Assert.That(templates, Contains.Item(CreatureConstants.Templates.Zombie));
            Assert.That(templates.Count(), Is.EqualTo(16));
        }
    }
}
