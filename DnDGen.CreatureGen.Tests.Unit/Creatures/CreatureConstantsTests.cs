using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Magics;
using NUnit.Framework;
using System.Linq;
using System.Reflection;

namespace DnDGen.CreatureGen.Tests.Unit.Creatures
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
        [TestCase(CreatureConstants.Angel_AstralDeva, "Angel, Astral Deva")]
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
        [TestCase(CreatureConstants.Beholder_Gauth, "Gauth (Lesser Beholder)")]
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
        [TestCase(CreatureConstants.Camel_Bactrian, "Camel, Bactrian (Two-Humped)")]
        [TestCase(CreatureConstants.Camel_Dromedary, "Camel, Dromedary (One-Humped)")]
        [TestCase(CreatureConstants.CarrionCrawler, "Carrion Crawler")]
        [TestCase(CreatureConstants.Cat, "Cat")]
        [TestCase(CreatureConstants.Centaur, "Centaur")]
        [TestCase(CreatureConstants.Centipede_Swarm, "Centipede Swarm")]
        [TestCase(CreatureConstants.ChaosBeast, "Chaos Beast")]
        [TestCase(CreatureConstants.Cheetah, "Cheetah")]
        [TestCase(CreatureConstants.Chimera_Black, "Chimera (Black Dragon head)")]
        [TestCase(CreatureConstants.Chimera_Blue, "Chimera (Blue Dragon head)")]
        [TestCase(CreatureConstants.Chimera_Green, "Chimera (Green Dragon head)")]
        [TestCase(CreatureConstants.Chimera_Red, "Chimera (Red Dragon head)")]
        [TestCase(CreatureConstants.Chimera_White, "Chimera (White Dragon head)")]
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
        [TestCase(CreatureConstants.Dwarf_Duergar, "Duergar")]
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
        [TestCase(CreatureConstants.Grig_WithFiddle, "Grig with fiddle")]
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
        [TestCase(CreatureConstants.MindFlayer, "Mind Flayer (Illithid)")]
        [TestCase(CreatureConstants.Minotaur, "Minotaur")]
        [TestCase(CreatureConstants.Mohrg, "Mohrg")]
        [TestCase(CreatureConstants.Monkey, "Monkey")]
        [TestCase(CreatureConstants.Mule, "Mule")]
        [TestCase(CreatureConstants.Mummy, "Mummy")]
        [TestCase(CreatureConstants.Nalfeshnee, "Nalfeshnee")]
        [TestCase(CreatureConstants.HellHound_NessianWarhound, "Nessian Warhound")]
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
        [TestCase(CreatureConstants.GrayOoze, "Gray Ooze")]
        [TestCase(CreatureConstants.OchreJelly, "Ochre Jelly")]
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
        [TestCase(CreatureConstants.Angel_Planetar, "Angel, Planetar")]
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
        [TestCase(CreatureConstants.Sahuagin_Malenti, "Malenti")]
        [TestCase(CreatureConstants.Sahuagin_Mutant, "Sahuagin Mutant")]
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
        [TestCase(CreatureConstants.Angel_Solar, "Angel, Solar")]
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
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Colossal, CreatureConstants.Groups.AnimatedObject + ", Anvil, " + SizeConstants.Colossal)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Gargantuan, CreatureConstants.Groups.AnimatedObject + ", Anvil, " + SizeConstants.Gargantuan)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Huge, CreatureConstants.Groups.AnimatedObject + ", Anvil, " + SizeConstants.Huge)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Large, CreatureConstants.Groups.AnimatedObject + ", Anvil, " + SizeConstants.Large)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Medium, CreatureConstants.Groups.AnimatedObject + ", Anvil, " + SizeConstants.Medium)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Small, CreatureConstants.Groups.AnimatedObject + ", Anvil, " + SizeConstants.Small)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Tiny, CreatureConstants.Groups.AnimatedObject + ", Anvil, " + SizeConstants.Tiny)]
        [TestCase(CreatureConstants.AnimatedObject_Block_Stone_Colossal, CreatureConstants.Groups.AnimatedObject + ", Block (Stone), " + SizeConstants.Colossal)]
        [TestCase(CreatureConstants.AnimatedObject_Block_Stone_Gargantuan, CreatureConstants.Groups.AnimatedObject + ", Block (Stone), " + SizeConstants.Gargantuan)]
        [TestCase(CreatureConstants.AnimatedObject_Block_Stone_Huge, CreatureConstants.Groups.AnimatedObject + ", Block (Stone), " + SizeConstants.Huge)]
        [TestCase(CreatureConstants.AnimatedObject_Block_Stone_Large, CreatureConstants.Groups.AnimatedObject + ", Block (Stone), " + SizeConstants.Large)]
        [TestCase(CreatureConstants.AnimatedObject_Block_Stone_Medium, CreatureConstants.Groups.AnimatedObject + ", Block (Stone), " + SizeConstants.Medium)]
        [TestCase(CreatureConstants.AnimatedObject_Block_Stone_Small, CreatureConstants.Groups.AnimatedObject + ", Block (Stone), " + SizeConstants.Small)]
        [TestCase(CreatureConstants.AnimatedObject_Block_Stone_Tiny, CreatureConstants.Groups.AnimatedObject + ", Block (Stone), " + SizeConstants.Tiny)]
        [TestCase(CreatureConstants.AnimatedObject_Block_Wood_Colossal, CreatureConstants.Groups.AnimatedObject + ", Block (Wood), " + SizeConstants.Colossal)]
        [TestCase(CreatureConstants.AnimatedObject_Block_Wood_Gargantuan, CreatureConstants.Groups.AnimatedObject + ", Block (Wood), " + SizeConstants.Gargantuan)]
        [TestCase(CreatureConstants.AnimatedObject_Block_Wood_Huge, CreatureConstants.Groups.AnimatedObject + ", Block (Wood), " + SizeConstants.Huge)]
        [TestCase(CreatureConstants.AnimatedObject_Block_Wood_Large, CreatureConstants.Groups.AnimatedObject + ", Block (Wood), " + SizeConstants.Large)]
        [TestCase(CreatureConstants.AnimatedObject_Block_Wood_Medium, CreatureConstants.Groups.AnimatedObject + ", Block (Wood), " + SizeConstants.Medium)]
        [TestCase(CreatureConstants.AnimatedObject_Block_Wood_Small, CreatureConstants.Groups.AnimatedObject + ", Block (Wood), " + SizeConstants.Small)]
        [TestCase(CreatureConstants.AnimatedObject_Block_Wood_Tiny, CreatureConstants.Groups.AnimatedObject + ", Block (Wood), " + SizeConstants.Tiny)]
        [TestCase(CreatureConstants.AnimatedObject_Box_Colossal, CreatureConstants.Groups.AnimatedObject + ", Box, " + SizeConstants.Colossal)]
        [TestCase(CreatureConstants.AnimatedObject_Box_Gargantuan, CreatureConstants.Groups.AnimatedObject + ", Box, " + SizeConstants.Gargantuan)]
        [TestCase(CreatureConstants.AnimatedObject_Box_Huge, CreatureConstants.Groups.AnimatedObject + ", Box, " + SizeConstants.Huge)]
        [TestCase(CreatureConstants.AnimatedObject_Box_Large, CreatureConstants.Groups.AnimatedObject + ", Box, " + SizeConstants.Large)]
        [TestCase(CreatureConstants.AnimatedObject_Box_Medium, CreatureConstants.Groups.AnimatedObject + ", Box, " + SizeConstants.Medium)]
        [TestCase(CreatureConstants.AnimatedObject_Box_Small, CreatureConstants.Groups.AnimatedObject + ", Box, " + SizeConstants.Small)]
        [TestCase(CreatureConstants.AnimatedObject_Box_Tiny, CreatureConstants.Groups.AnimatedObject + ", Box, " + SizeConstants.Tiny)]
        [TestCase(CreatureConstants.AnimatedObject_Carpet_Colossal, CreatureConstants.Groups.AnimatedObject + ", Carpet, " + SizeConstants.Colossal)]
        [TestCase(CreatureConstants.AnimatedObject_Carpet_Gargantuan, CreatureConstants.Groups.AnimatedObject + ", Carpet, " + SizeConstants.Gargantuan)]
        [TestCase(CreatureConstants.AnimatedObject_Carpet_Huge, CreatureConstants.Groups.AnimatedObject + ", Carpet, " + SizeConstants.Huge)]
        [TestCase(CreatureConstants.AnimatedObject_Carpet_Large, CreatureConstants.Groups.AnimatedObject + ", Carpet, " + SizeConstants.Large)]
        [TestCase(CreatureConstants.AnimatedObject_Carpet_Medium, CreatureConstants.Groups.AnimatedObject + ", Carpet, " + SizeConstants.Medium)]
        [TestCase(CreatureConstants.AnimatedObject_Carpet_Small, CreatureConstants.Groups.AnimatedObject + ", Carpet, " + SizeConstants.Small)]
        [TestCase(CreatureConstants.AnimatedObject_Carpet_Tiny, CreatureConstants.Groups.AnimatedObject + ", Carpet, " + SizeConstants.Tiny)]
        [TestCase(CreatureConstants.AnimatedObject_Carriage_Colossal, CreatureConstants.Groups.AnimatedObject + ", Carriage, " + SizeConstants.Colossal)]
        [TestCase(CreatureConstants.AnimatedObject_Carriage_Gargantuan, CreatureConstants.Groups.AnimatedObject + ", Carriage, " + SizeConstants.Gargantuan)]
        [TestCase(CreatureConstants.AnimatedObject_Carriage_Huge, CreatureConstants.Groups.AnimatedObject + ", Carriage, " + SizeConstants.Huge)]
        [TestCase(CreatureConstants.AnimatedObject_Carriage_Large, CreatureConstants.Groups.AnimatedObject + ", Carriage, " + SizeConstants.Large)]
        [TestCase(CreatureConstants.AnimatedObject_Carriage_Medium, CreatureConstants.Groups.AnimatedObject + ", Carriage, " + SizeConstants.Medium)]
        [TestCase(CreatureConstants.AnimatedObject_Carriage_Small, CreatureConstants.Groups.AnimatedObject + ", Carriage, " + SizeConstants.Small)]
        [TestCase(CreatureConstants.AnimatedObject_Carriage_Tiny, CreatureConstants.Groups.AnimatedObject + ", Carriage, " + SizeConstants.Tiny)]
        [TestCase(CreatureConstants.AnimatedObject_Chain_Colossal, CreatureConstants.Groups.AnimatedObject + ", Chain, " + SizeConstants.Colossal)]
        [TestCase(CreatureConstants.AnimatedObject_Chain_Gargantuan, CreatureConstants.Groups.AnimatedObject + ", Chain, " + SizeConstants.Gargantuan)]
        [TestCase(CreatureConstants.AnimatedObject_Chain_Huge, CreatureConstants.Groups.AnimatedObject + ", Chain, " + SizeConstants.Huge)]
        [TestCase(CreatureConstants.AnimatedObject_Chain_Large, CreatureConstants.Groups.AnimatedObject + ", Chain, " + SizeConstants.Large)]
        [TestCase(CreatureConstants.AnimatedObject_Chain_Medium, CreatureConstants.Groups.AnimatedObject + ", Chain, " + SizeConstants.Medium)]
        [TestCase(CreatureConstants.AnimatedObject_Chain_Small, CreatureConstants.Groups.AnimatedObject + ", Chain, " + SizeConstants.Small)]
        [TestCase(CreatureConstants.AnimatedObject_Chain_Tiny, CreatureConstants.Groups.AnimatedObject + ", Chain, " + SizeConstants.Tiny)]
        [TestCase(CreatureConstants.AnimatedObject_Chair_Colossal, CreatureConstants.Groups.AnimatedObject + ", Chair, " + SizeConstants.Colossal)]
        [TestCase(CreatureConstants.AnimatedObject_Chair_Gargantuan, CreatureConstants.Groups.AnimatedObject + ", Chair, " + SizeConstants.Gargantuan)]
        [TestCase(CreatureConstants.AnimatedObject_Chair_Huge, CreatureConstants.Groups.AnimatedObject + ", Chair, " + SizeConstants.Huge)]
        [TestCase(CreatureConstants.AnimatedObject_Chair_Large, CreatureConstants.Groups.AnimatedObject + ", Chair, " + SizeConstants.Large)]
        [TestCase(CreatureConstants.AnimatedObject_Chair_Medium, CreatureConstants.Groups.AnimatedObject + ", Chair, " + SizeConstants.Medium)]
        [TestCase(CreatureConstants.AnimatedObject_Chair_Small, CreatureConstants.Groups.AnimatedObject + ", Chair, " + SizeConstants.Small)]
        [TestCase(CreatureConstants.AnimatedObject_Chair_Tiny, CreatureConstants.Groups.AnimatedObject + ", Chair, " + SizeConstants.Tiny)]
        [TestCase(CreatureConstants.AnimatedObject_Clothes_Colossal, CreatureConstants.Groups.AnimatedObject + ", Clothes, " + SizeConstants.Colossal)]
        [TestCase(CreatureConstants.AnimatedObject_Clothes_Gargantuan, CreatureConstants.Groups.AnimatedObject + ", Clothes, " + SizeConstants.Gargantuan)]
        [TestCase(CreatureConstants.AnimatedObject_Clothes_Huge, CreatureConstants.Groups.AnimatedObject + ", Clothes, " + SizeConstants.Huge)]
        [TestCase(CreatureConstants.AnimatedObject_Clothes_Large, CreatureConstants.Groups.AnimatedObject + ", Clothes, " + SizeConstants.Large)]
        [TestCase(CreatureConstants.AnimatedObject_Clothes_Medium, CreatureConstants.Groups.AnimatedObject + ", Clothes, " + SizeConstants.Medium)]
        [TestCase(CreatureConstants.AnimatedObject_Clothes_Small, CreatureConstants.Groups.AnimatedObject + ", Clothes, " + SizeConstants.Small)]
        [TestCase(CreatureConstants.AnimatedObject_Clothes_Tiny, CreatureConstants.Groups.AnimatedObject + ", Clothes, " + SizeConstants.Tiny)]
        [TestCase(CreatureConstants.AnimatedObject_Ladder_Colossal, CreatureConstants.Groups.AnimatedObject + ", Ladder, " + SizeConstants.Colossal)]
        [TestCase(CreatureConstants.AnimatedObject_Ladder_Gargantuan, CreatureConstants.Groups.AnimatedObject + ", Ladder, " + SizeConstants.Gargantuan)]
        [TestCase(CreatureConstants.AnimatedObject_Ladder_Huge, CreatureConstants.Groups.AnimatedObject + ", Ladder, " + SizeConstants.Huge)]
        [TestCase(CreatureConstants.AnimatedObject_Ladder_Large, CreatureConstants.Groups.AnimatedObject + ", Ladder, " + SizeConstants.Large)]
        [TestCase(CreatureConstants.AnimatedObject_Ladder_Medium, CreatureConstants.Groups.AnimatedObject + ", Ladder, " + SizeConstants.Medium)]
        [TestCase(CreatureConstants.AnimatedObject_Ladder_Small, CreatureConstants.Groups.AnimatedObject + ", Ladder, " + SizeConstants.Small)]
        [TestCase(CreatureConstants.AnimatedObject_Ladder_Tiny, CreatureConstants.Groups.AnimatedObject + ", Ladder, " + SizeConstants.Tiny)]
        [TestCase(CreatureConstants.AnimatedObject_Rope_Colossal, CreatureConstants.Groups.AnimatedObject + ", Rope, " + SizeConstants.Colossal)]
        [TestCase(CreatureConstants.AnimatedObject_Rope_Gargantuan, CreatureConstants.Groups.AnimatedObject + ", Rope, " + SizeConstants.Gargantuan)]
        [TestCase(CreatureConstants.AnimatedObject_Rope_Huge, CreatureConstants.Groups.AnimatedObject + ", Rope, " + SizeConstants.Huge)]
        [TestCase(CreatureConstants.AnimatedObject_Rope_Large, CreatureConstants.Groups.AnimatedObject + ", Rope, " + SizeConstants.Large)]
        [TestCase(CreatureConstants.AnimatedObject_Rope_Medium, CreatureConstants.Groups.AnimatedObject + ", Rope, " + SizeConstants.Medium)]
        [TestCase(CreatureConstants.AnimatedObject_Rope_Small, CreatureConstants.Groups.AnimatedObject + ", Rope, " + SizeConstants.Small)]
        [TestCase(CreatureConstants.AnimatedObject_Rope_Tiny, CreatureConstants.Groups.AnimatedObject + ", Rope, " + SizeConstants.Tiny)]
        [TestCase(CreatureConstants.AnimatedObject_Rug_Colossal, CreatureConstants.Groups.AnimatedObject + ", Rug, " + SizeConstants.Colossal)]
        [TestCase(CreatureConstants.AnimatedObject_Rug_Gargantuan, CreatureConstants.Groups.AnimatedObject + ", Rug, " + SizeConstants.Gargantuan)]
        [TestCase(CreatureConstants.AnimatedObject_Rug_Huge, CreatureConstants.Groups.AnimatedObject + ", Rug, " + SizeConstants.Huge)]
        [TestCase(CreatureConstants.AnimatedObject_Rug_Large, CreatureConstants.Groups.AnimatedObject + ", Rug, " + SizeConstants.Large)]
        [TestCase(CreatureConstants.AnimatedObject_Rug_Medium, CreatureConstants.Groups.AnimatedObject + ", Rug, " + SizeConstants.Medium)]
        [TestCase(CreatureConstants.AnimatedObject_Rug_Small, CreatureConstants.Groups.AnimatedObject + ", Rug, " + SizeConstants.Small)]
        [TestCase(CreatureConstants.AnimatedObject_Rug_Tiny, CreatureConstants.Groups.AnimatedObject + ", Rug, " + SizeConstants.Tiny)]
        [TestCase(CreatureConstants.AnimatedObject_Sled_Colossal, CreatureConstants.Groups.AnimatedObject + ", Sled, " + SizeConstants.Colossal)]
        [TestCase(CreatureConstants.AnimatedObject_Sled_Gargantuan, CreatureConstants.Groups.AnimatedObject + ", Sled, " + SizeConstants.Gargantuan)]
        [TestCase(CreatureConstants.AnimatedObject_Sled_Huge, CreatureConstants.Groups.AnimatedObject + ", Sled, " + SizeConstants.Huge)]
        [TestCase(CreatureConstants.AnimatedObject_Sled_Large, CreatureConstants.Groups.AnimatedObject + ", Sled, " + SizeConstants.Large)]
        [TestCase(CreatureConstants.AnimatedObject_Sled_Medium, CreatureConstants.Groups.AnimatedObject + ", Sled, " + SizeConstants.Medium)]
        [TestCase(CreatureConstants.AnimatedObject_Sled_Small, CreatureConstants.Groups.AnimatedObject + ", Sled, " + SizeConstants.Small)]
        [TestCase(CreatureConstants.AnimatedObject_Sled_Tiny, CreatureConstants.Groups.AnimatedObject + ", Sled, " + SizeConstants.Tiny)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Animal_Colossal, CreatureConstants.Groups.AnimatedObject + ", Statue (Animal), " + SizeConstants.Colossal)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Animal_Gargantuan, CreatureConstants.Groups.AnimatedObject + ", Statue (Animal), " + SizeConstants.Gargantuan)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Animal_Huge, CreatureConstants.Groups.AnimatedObject + ", Statue (Animal), " + SizeConstants.Huge)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Animal_Large, CreatureConstants.Groups.AnimatedObject + ", Statue (Animal), " + SizeConstants.Large)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Animal_Medium, CreatureConstants.Groups.AnimatedObject + ", Statue (Animal), " + SizeConstants.Medium)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Animal_Small, CreatureConstants.Groups.AnimatedObject + ", Statue (Animal), " + SizeConstants.Small)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Animal_Tiny, CreatureConstants.Groups.AnimatedObject + ", Statue (Animal), " + SizeConstants.Tiny)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Humanoid_Colossal, CreatureConstants.Groups.AnimatedObject + ", Statue (Humanoid), " + SizeConstants.Colossal)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Humanoid_Gargantuan, CreatureConstants.Groups.AnimatedObject + ", Statue (Humanoid), " + SizeConstants.Gargantuan)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Humanoid_Huge, CreatureConstants.Groups.AnimatedObject + ", Statue (Humanoid), " + SizeConstants.Huge)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Humanoid_Large, CreatureConstants.Groups.AnimatedObject + ", Statue (Humanoid), " + SizeConstants.Large)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Humanoid_Medium, CreatureConstants.Groups.AnimatedObject + ", Statue (Humanoid), " + SizeConstants.Medium)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Humanoid_Small, CreatureConstants.Groups.AnimatedObject + ", Statue (Humanoid), " + SizeConstants.Small)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Humanoid_Tiny, CreatureConstants.Groups.AnimatedObject + ", Statue (Humanoid), " + SizeConstants.Tiny)]
        [TestCase(CreatureConstants.AnimatedObject_Stool_Colossal, CreatureConstants.Groups.AnimatedObject + ", Stool, " + SizeConstants.Colossal)]
        [TestCase(CreatureConstants.AnimatedObject_Stool_Gargantuan, CreatureConstants.Groups.AnimatedObject + ", Stool, " + SizeConstants.Gargantuan)]
        [TestCase(CreatureConstants.AnimatedObject_Stool_Huge, CreatureConstants.Groups.AnimatedObject + ", Stool, " + SizeConstants.Huge)]
        [TestCase(CreatureConstants.AnimatedObject_Stool_Large, CreatureConstants.Groups.AnimatedObject + ", Stool, " + SizeConstants.Large)]
        [TestCase(CreatureConstants.AnimatedObject_Stool_Medium, CreatureConstants.Groups.AnimatedObject + ", Stool, " + SizeConstants.Medium)]
        [TestCase(CreatureConstants.AnimatedObject_Stool_Small, CreatureConstants.Groups.AnimatedObject + ", Stool, " + SizeConstants.Small)]
        [TestCase(CreatureConstants.AnimatedObject_Stool_Tiny, CreatureConstants.Groups.AnimatedObject + ", Stool, " + SizeConstants.Tiny)]
        [TestCase(CreatureConstants.AnimatedObject_Table_Colossal, CreatureConstants.Groups.AnimatedObject + ", Table, " + SizeConstants.Colossal)]
        [TestCase(CreatureConstants.AnimatedObject_Table_Gargantuan, CreatureConstants.Groups.AnimatedObject + ", Table, " + SizeConstants.Gargantuan)]
        [TestCase(CreatureConstants.AnimatedObject_Table_Huge, CreatureConstants.Groups.AnimatedObject + ", Table, " + SizeConstants.Huge)]
        [TestCase(CreatureConstants.AnimatedObject_Table_Large, CreatureConstants.Groups.AnimatedObject + ", Table, " + SizeConstants.Large)]
        [TestCase(CreatureConstants.AnimatedObject_Table_Medium, CreatureConstants.Groups.AnimatedObject + ", Table, " + SizeConstants.Medium)]
        [TestCase(CreatureConstants.AnimatedObject_Table_Small, CreatureConstants.Groups.AnimatedObject + ", Table, " + SizeConstants.Small)]
        [TestCase(CreatureConstants.AnimatedObject_Table_Tiny, CreatureConstants.Groups.AnimatedObject + ", Table, " + SizeConstants.Tiny)]
        [TestCase(CreatureConstants.AnimatedObject_Tapestry_Colossal, CreatureConstants.Groups.AnimatedObject + ", Tapestry, " + SizeConstants.Colossal)]
        [TestCase(CreatureConstants.AnimatedObject_Tapestry_Gargantuan, CreatureConstants.Groups.AnimatedObject + ", Tapestry, " + SizeConstants.Gargantuan)]
        [TestCase(CreatureConstants.AnimatedObject_Tapestry_Huge, CreatureConstants.Groups.AnimatedObject + ", Tapestry, " + SizeConstants.Huge)]
        [TestCase(CreatureConstants.AnimatedObject_Tapestry_Large, CreatureConstants.Groups.AnimatedObject + ", Tapestry, " + SizeConstants.Large)]
        [TestCase(CreatureConstants.AnimatedObject_Tapestry_Medium, CreatureConstants.Groups.AnimatedObject + ", Tapestry, " + SizeConstants.Medium)]
        [TestCase(CreatureConstants.AnimatedObject_Tapestry_Small, CreatureConstants.Groups.AnimatedObject + ", Tapestry, " + SizeConstants.Small)]
        [TestCase(CreatureConstants.AnimatedObject_Tapestry_Tiny, CreatureConstants.Groups.AnimatedObject + ", Tapestry, " + SizeConstants.Tiny)]
        [TestCase(CreatureConstants.AnimatedObject_Wagon_Colossal, CreatureConstants.Groups.AnimatedObject + ", Wagon, " + SizeConstants.Colossal)]
        [TestCase(CreatureConstants.AnimatedObject_Wagon_Gargantuan, CreatureConstants.Groups.AnimatedObject + ", Wagon, " + SizeConstants.Gargantuan)]
        [TestCase(CreatureConstants.AnimatedObject_Wagon_Huge, CreatureConstants.Groups.AnimatedObject + ", Wagon, " + SizeConstants.Huge)]
        [TestCase(CreatureConstants.AnimatedObject_Wagon_Large, CreatureConstants.Groups.AnimatedObject + ", Wagon, " + SizeConstants.Large)]
        [TestCase(CreatureConstants.AnimatedObject_Wagon_Medium, CreatureConstants.Groups.AnimatedObject + ", Wagon, " + SizeConstants.Medium)]
        [TestCase(CreatureConstants.AnimatedObject_Wagon_Small, CreatureConstants.Groups.AnimatedObject + ", Wagon, " + SizeConstants.Small)]
        [TestCase(CreatureConstants.AnimatedObject_Wagon_Tiny, CreatureConstants.Groups.AnimatedObject + ", Wagon, " + SizeConstants.Tiny)]
        [TestCase(CreatureConstants.Ant_Giant_Soldier, CreatureConstants.Groups.Ant_Giant + ", Soldier")]
        [TestCase(CreatureConstants.Ant_Giant_Worker, CreatureConstants.Groups.Ant_Giant + ", Worker")]
        [TestCase(CreatureConstants.Ant_Giant_Queen, CreatureConstants.Groups.Ant_Giant + ", Queen")]
        [TestCase(CreatureConstants.Arrowhawk_Juvenile, "Juvenile " + CreatureConstants.Groups.Arrowhawk)]
        [TestCase(CreatureConstants.Arrowhawk_Adult, "Adult " + CreatureConstants.Groups.Arrowhawk)]
        [TestCase(CreatureConstants.Arrowhawk_Elder, "Elder " + CreatureConstants.Groups.Arrowhawk)]
        [TestCase(CreatureConstants.BarbedDevil_Hamatula, "Barbed Devil (Hamatula)")]
        [TestCase(CreatureConstants.BeardedDevil_Barbazu, "Bearded Devil (Barbazu)")]
        [TestCase(CreatureConstants.Barghest_Greater, "Greater " + CreatureConstants.Barghest)]
        [TestCase(CreatureConstants.Basilisk_Greater, "Greater " + CreatureConstants.Basilisk)]
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
        [TestCase(CreatureConstants.Pixie_WithIrresistibleDance, CreatureConstants.Pixie + " with " + SpellConstants.IrresistibleDance)]
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
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Colossal, CreatureConstants.Groups.Spider_Monstrous + ", Hunter, " + SizeConstants.Colossal)]
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Gargantuan, CreatureConstants.Groups.Spider_Monstrous + ", Hunter, " + SizeConstants.Gargantuan)]
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Huge, CreatureConstants.Groups.Spider_Monstrous + ", Hunter, " + SizeConstants.Huge)]
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Large, CreatureConstants.Groups.Spider_Monstrous + ", Hunter, " + SizeConstants.Large)]
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Medium, CreatureConstants.Groups.Spider_Monstrous + ", Hunter, " + SizeConstants.Medium)]
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Small, CreatureConstants.Groups.Spider_Monstrous + ", Hunter, " + SizeConstants.Small)]
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Tiny, CreatureConstants.Groups.Spider_Monstrous + ", Hunter, " + SizeConstants.Tiny)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Colossal, CreatureConstants.Groups.Spider_Monstrous + ", Web-Spinner, " + SizeConstants.Colossal)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan, CreatureConstants.Groups.Spider_Monstrous + ", Web-Spinner, " + SizeConstants.Gargantuan)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Huge, CreatureConstants.Groups.Spider_Monstrous + ", Web-Spinner, " + SizeConstants.Huge)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Large, CreatureConstants.Groups.Spider_Monstrous + ", Web-Spinner, " + SizeConstants.Large)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Medium, CreatureConstants.Groups.Spider_Monstrous + ", Web-Spinner, " + SizeConstants.Medium)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Small, CreatureConstants.Groups.Spider_Monstrous + ", Web-Spinner, " + SizeConstants.Small)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Tiny, CreatureConstants.Groups.Spider_Monstrous + ", Web-Spinner, " + SizeConstants.Tiny)]
        [TestCase(CreatureConstants.Tojanida_Adult, "Adult " + CreatureConstants.Groups.Tojanida)]
        [TestCase(CreatureConstants.Tojanida_Elder, "Elder " + CreatureConstants.Groups.Tojanida)]
        [TestCase(CreatureConstants.Tojanida_Juvenile, "Juvenile " + CreatureConstants.Groups.Tojanida)]
        [TestCase(CreatureConstants.Troll_Scrag, "Scrag")]
        [TestCase(CreatureConstants.UmberHulk_TrulyHorrid, "Truly Horrid " + CreatureConstants.UmberHulk)]
        [TestCase(CreatureConstants.Whale_Baleen, "Baleen " + CreatureConstants.Groups.Whale)]
        [TestCase(CreatureConstants.Whale_Cachalot, "Cachalot " + CreatureConstants.Groups.Whale)]
        [TestCase(CreatureConstants.Whale_Orca, "Orca " + CreatureConstants.Groups.Whale)]
        [TestCase(CreatureConstants.Wraith_Dread, "Dread " + CreatureConstants.Wraith)]
        [TestCase(CreatureConstants.Xorn_Average, "Average " + CreatureConstants.Groups.Xorn)]
        [TestCase(CreatureConstants.Xorn_Elder, "Elder " + CreatureConstants.Groups.Xorn)]
        [TestCase(CreatureConstants.Xorn_Minor, "Minor " + CreatureConstants.Groups.Xorn)]
        [TestCase(CreatureConstants.YuanTi_Abomination, CreatureConstants.Groups.YuanTi + " Abomination")]
        [TestCase(CreatureConstants.YuanTi_Halfblood_SnakeArms, CreatureConstants.Groups.YuanTi + " Halfblood (Human Head, Arms are Snakes)")]
        [TestCase(CreatureConstants.YuanTi_Halfblood_SnakeHead, CreatureConstants.Groups.YuanTi + " Halfblood (Snake Head)")]
        [TestCase(CreatureConstants.YuanTi_Halfblood_SnakeTail, CreatureConstants.Groups.YuanTi + " Halfblood (Snake Head, Snake Tail instead of Human Legs)")]
        [TestCase(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, CreatureConstants.Groups.YuanTi + " Halfblood (Snake Head, Snake Tail and Human Legs)")]
        [TestCase(CreatureConstants.YuanTi_Pureblood, CreatureConstants.Groups.YuanTi + " Pureblood")]
        public void CreatureConstant(string constant, string value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [TestCase(CreatureConstants.Templates.CelestialCreature, "Celestial Creature")]
        [TestCase(CreatureConstants.Templates.FiendishCreature, "Fiendish Creature")]
        [TestCase(CreatureConstants.Templates.Ghost, "Ghost")]
        [TestCase(CreatureConstants.Templates.HalfCelestial, "Half-Celestial")]
        [TestCase(CreatureConstants.Templates.HalfDragon_Black, "Half-Dragon (Black)")]
        [TestCase(CreatureConstants.Templates.HalfDragon_Blue, "Half-Dragon (Blue)")]
        [TestCase(CreatureConstants.Templates.HalfDragon_Green, "Half-Dragon (Green)")]
        [TestCase(CreatureConstants.Templates.HalfDragon_Red, "Half-Dragon (Red)")]
        [TestCase(CreatureConstants.Templates.HalfDragon_White, "Half-Dragon (White)")]
        [TestCase(CreatureConstants.Templates.HalfDragon_Brass, "Half-Dragon (Brass)")]
        [TestCase(CreatureConstants.Templates.HalfDragon_Bronze, "Half-Dragon (Bronze)")]
        [TestCase(CreatureConstants.Templates.HalfDragon_Copper, "Half-Dragon (Copper)")]
        [TestCase(CreatureConstants.Templates.HalfDragon_Gold, "Half-Dragon (Gold)")]
        [TestCase(CreatureConstants.Templates.HalfDragon_Silver, "Half-Dragon (Silver)")]
        [TestCase(CreatureConstants.Templates.HalfFiend, "Half-Fiend")]
        [TestCase(CreatureConstants.Templates.Lich, "Lich")]
        [TestCase(CreatureConstants.Templates.None, "None")]
        [TestCase(CreatureConstants.Templates.Skeleton, "Skeleton")]
        [TestCase(CreatureConstants.Templates.Vampire, "Vampire")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Bear_Black_Afflicted, "Lycanthrope, Black Bear (Werebear, Afflicted)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Bear_Black_Natural, "Lycanthrope, Black Bear (Werebear, Natural)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted, "Lycanthrope, Brown Bear (Werebear, Afflicted)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural, "Lycanthrope, Brown Bear (Werebear, Natural)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Bear_Dire_Afflicted, "Lycanthrope, Dire Bear (Dire Werebear, Afflicted)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Bear_Dire_Natural, "Lycanthrope, Dire Bear (Dire Werebear, Natural)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Bear_Polar_Afflicted, "Lycanthrope, Polar Bear (Werebear, Afflicted)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Bear_Polar_Natural, "Lycanthrope, Polar Bear (Werebear, Natural)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Boar_Afflicted, "Lycanthrope, Boar (Wereboar, Afflicted)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Boar_Natural, "Lycanthrope, Boar (Wereboar, Natural)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted, "Lycanthrope, Dire Boar (Dire Wereboar, Afflicted)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural, "Lycanthrope, Dire Boar (Dire Wereboar, Natural)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted, "Lycanthrope, Dire Rat (Wererat, Afflicted)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural, "Lycanthrope, Dire Rat (Wererat, Natural)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted, "Lycanthrope, Tiger (Weretiger, Afflicted)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Tiger_Natural, "Lycanthrope, Tiger (Weretiger, Natural)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Afflicted, "Lycanthrope, Dire Tiger (Dire Weretiger, Afflicted)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Natural, "Lycanthrope, Dire Tiger (Dire Weretiger, Natural)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted, "Lycanthrope, Wolf (Werewolf, Afflicted)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Wolf_Natural, "Lycanthrope, Wolf (Werewolf, Natural)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted, "Lycanthrope, Dire Wolf (Dire Werewolf, Afflicted)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural, "Lycanthrope, Dire Wolf (Dire Werewolf, Natural)")]
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
        [TestCase(CreatureConstants.Groups.Chimera, "Chimera")]
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
        [TestCase(CreatureConstants.Groups.HalfDragon, "Half-Dragon")]
        [TestCase(CreatureConstants.Groups.Halfling, "Halfling")]
        [TestCase(CreatureConstants.Groups.HasSkeleton, "Has Skeleton")]
        [TestCase(CreatureConstants.Groups.Horse, "Horse")]
        [TestCase(CreatureConstants.Groups.Hydra, "Hydra")]
        [TestCase(CreatureConstants.Groups.Inevitable, "Inevitable")]
        [TestCase(CreatureConstants.Groups.Mephit, "Mephit")]
        [TestCase(CreatureConstants.Groups.Naga, "Naga")]
        [TestCase(CreatureConstants.Groups.Nightshade, "Nightshade")]
        [TestCase(CreatureConstants.Groups.Orc, "Orc")]
        [TestCase(CreatureConstants.Groups.Planetouched, "Planetouched")]
        [TestCase(CreatureConstants.Groups.Pyrohydra, "Pyrohydra")]
        [TestCase(CreatureConstants.Groups.Salamander, "Salamander")]
        [TestCase(CreatureConstants.Groups.Scorpion_Monstrous, "Monstrous Scorpion")]
        [TestCase(CreatureConstants.Groups.Sphinx, "Sphinx")]
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
        [TestCase(CreatureConstants.Types.Subtypes.Air, "Air")]
        [TestCase(CreatureConstants.Types.Subtypes.Angel, "Angel")]
        [TestCase(CreatureConstants.Types.Subtypes.Aquatic, "Aquatic")]
        [TestCase(CreatureConstants.Types.Subtypes.Archon, "Archon")]
        [TestCase(CreatureConstants.Types.Subtypes.Augmented, "Augmented")]
        [TestCase(CreatureConstants.Types.Subtypes.Chaotic, "Chaotic")]
        [TestCase(CreatureConstants.Types.Subtypes.Cold, "Cold")]
        [TestCase(CreatureConstants.Types.Subtypes.Dwarf, "Dwarf")]
        [TestCase(CreatureConstants.Types.Subtypes.Earth, "Earth")]
        [TestCase(CreatureConstants.Types.Subtypes.Elf, "Elf")]
        [TestCase(CreatureConstants.Types.Subtypes.Evil, "Evil")]
        [TestCase(CreatureConstants.Types.Subtypes.Extraplanar, "Extraplanar")]
        [TestCase(CreatureConstants.Types.Subtypes.Fire, "Fire")]
        [TestCase(CreatureConstants.Types.Subtypes.Gnoll, "Gnoll")]
        [TestCase(CreatureConstants.Types.Subtypes.Gnome, "Gnome")]
        [TestCase(CreatureConstants.Types.Subtypes.Goblinoid, "Goblinoid")]
        [TestCase(CreatureConstants.Types.Subtypes.Good, "Good")]
        [TestCase(CreatureConstants.Types.Subtypes.Halfling, "Halfling")]
        [TestCase(CreatureConstants.Types.Subtypes.Human, "Human")]
        [TestCase(CreatureConstants.Types.Subtypes.Incorporeal, "Incorporeal")]
        [TestCase(CreatureConstants.Types.Subtypes.Lawful, "Lawful")]
        [TestCase(CreatureConstants.Types.Subtypes.Native, "Native")]
        [TestCase(CreatureConstants.Types.Subtypes.Orc, "Orc")]
        [TestCase(CreatureConstants.Types.Subtypes.Reptilian, "Reptilian")]
        [TestCase(CreatureConstants.Types.Subtypes.Shapechanger, "Shapechanger")]
        [TestCase(CreatureConstants.Types.Subtypes.Swarm, "Swarm")]
        [TestCase(CreatureConstants.Types.Subtypes.Water, "Water")]
        public void CreatureTypeConstant(string constant, string value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [Test]
        public void AllCreatureConstants()
        {
            var creatures = CreatureConstants.GetAll();
            var creatureConstants = typeof(CreatureConstants);
            var fields = creatureConstants.GetFields(BindingFlags.Public | BindingFlags.Static);
            var constantFields = fields.Where(f => f.IsLiteral && !f.IsInitOnly);
            var constants = constantFields.Select(f => f.GetValue(null) as string);

            //HACK: The failure message for Is.Equivalent is truncated because of the size of the collection
            //So, we will alter the assertions
            Assert.That(creatures, Is.Unique, "From GetAll");
            Assert.That(constants, Is.Unique, "From Reflection");
            Assert.That(creatures.Except(constants), Is.Empty, "From GetAll");
            Assert.That(constants.Except(creatures), Is.Empty, "From Reflection");
        }

        [Test]
        public void AllTemplates()
        {
            var templates = CreatureConstants.Templates.GetAll();
            var templateConstants = typeof(CreatureConstants.Templates);
            var fields = templateConstants.GetFields(BindingFlags.Public | BindingFlags.Static);
            var constantFields = fields.Where(f => f.IsLiteral && !f.IsInitOnly);
            var constants = constantFields.Select(f => f.GetValue(null) as string);

            Assert.That(templates, Is.EquivalentTo(constants));
        }

        [Test]
        public void AllTypes()
        {
            var types = CreatureConstants.Types.GetAll();
            var typeConstants = typeof(CreatureConstants.Types);
            var fields = typeConstants.GetFields(BindingFlags.Public | BindingFlags.Static);
            var constantFields = fields.Where(f => f.IsLiteral && !f.IsInitOnly);
            var constants = constantFields.Select(f => f.GetValue(null) as string);

            Assert.That(types, Is.EquivalentTo(constants));
        }

        [Test]
        public void AllSubtypes()
        {
            var subtypes = CreatureConstants.Types.Subtypes.GetAll();
            var subtypeConstants = typeof(CreatureConstants.Types.Subtypes);
            var fields = subtypeConstants.GetFields(BindingFlags.Public | BindingFlags.Static);
            var constantFields = fields.Where(f => f.IsLiteral && !f.IsInitOnly);
            var constants = constantFields.Select(f => f.GetValue(null) as string);

            Assert.That(subtypes, Is.EquivalentTo(constants));
        }

        [Test]
        public void AllCharacters()
        {
            var characters = CreatureConstants.GetAllCharacters();
            Assert.That(characters, Is.EquivalentTo(new[]
            {
                CreatureConstants.Aasimar,
                CreatureConstants.Aboleth,
                CreatureConstants.Androsphinx,
                CreatureConstants.Angel_AstralDeva,
                CreatureConstants.Annis,
                CreatureConstants.Aranea,
                CreatureConstants.Athach,
                CreatureConstants.Azer,
                CreatureConstants.BeardedDevil_Barbazu,
                CreatureConstants.BlinkDog,
                CreatureConstants.Bralani,
                CreatureConstants.Bugbear,
                CreatureConstants.Centaur,
                CreatureConstants.ChainDevil_Kyton,
                CreatureConstants.Chimera_Black,
                CreatureConstants.Chimera_Blue,
                CreatureConstants.Chimera_Green,
                CreatureConstants.Chimera_Red,
                CreatureConstants.Chimera_White,
                CreatureConstants.Couatl,
                CreatureConstants.Criosphinx,
                CreatureConstants.Derro,
                CreatureConstants.Derro_Sane,
                CreatureConstants.DisplacerBeast,
                CreatureConstants.Djinni,
                CreatureConstants.Doppelganger,
                CreatureConstants.Dragon_Black_Wyrmling,
                CreatureConstants.Dragon_Black_VeryYoung,
                CreatureConstants.Dragon_Black_Young,
                CreatureConstants.Dragon_Black_Juvenile,
                CreatureConstants.Dragon_Blue_Wyrmling,
                CreatureConstants.Dragon_Blue_VeryYoung,
                CreatureConstants.Dragon_Blue_Young,
                CreatureConstants.Dragon_Green_Wyrmling,
                CreatureConstants.Dragon_Green_VeryYoung,
                CreatureConstants.Dragon_Green_Young,
                CreatureConstants.Dragon_Green_Juvenile,
                CreatureConstants.Dragon_Red_Wyrmling,
                CreatureConstants.Dragon_Red_VeryYoung,
                CreatureConstants.Dragon_Red_Young,
                CreatureConstants.Dragon_White_Wyrmling,
                CreatureConstants.Dragon_White_VeryYoung,
                CreatureConstants.Dragon_White_Young,
                CreatureConstants.Dragon_White_Juvenile,
                CreatureConstants.Dragon_Brass_Wyrmling,
                CreatureConstants.Dragon_Brass_VeryYoung,
                CreatureConstants.Dragon_Brass_Young,
                CreatureConstants.Dragon_Brass_Juvenile,
                CreatureConstants.Dragon_Bronze_Wyrmling,
                CreatureConstants.Dragon_Bronze_VeryYoung,
                CreatureConstants.Dragon_Bronze_Young,
                CreatureConstants.Dragon_Copper_Wyrmling,
                CreatureConstants.Dragon_Copper_VeryYoung,
                CreatureConstants.Dragon_Copper_Young,
                CreatureConstants.Dragon_Copper_Juvenile,
                CreatureConstants.Dragon_Gold_Wyrmling,
                CreatureConstants.Dragon_Gold_VeryYoung,
                CreatureConstants.Dragon_Gold_Young,
                CreatureConstants.Dragon_Silver_Wyrmling,
                CreatureConstants.Dragon_Silver_VeryYoung,
                CreatureConstants.Dragon_Silver_Young,
                CreatureConstants.Dragonne,
                CreatureConstants.Dretch,
                CreatureConstants.Drider,
                CreatureConstants.Dryad,
                CreatureConstants.Dwarf_Deep,
                CreatureConstants.Dwarf_Duergar,
                CreatureConstants.Dwarf_Hill,
                CreatureConstants.Dwarf_Mountain,
                CreatureConstants.Eagle_Giant,
                CreatureConstants.Elf_Aquatic,
                CreatureConstants.Elf_Drow,
                CreatureConstants.Elf_Gray,
                CreatureConstants.Elf_Half,
                CreatureConstants.Elf_High,
                CreatureConstants.Elf_Wild,
                CreatureConstants.Elf_Wood,
                CreatureConstants.Erinyes,
                CreatureConstants.Ettercap,
                CreatureConstants.Ettin,
                CreatureConstants.Gargoyle,
                CreatureConstants.Gargoyle_Kapoacinth,
                CreatureConstants.Giant_Cloud,
                CreatureConstants.Giant_Fire,
                CreatureConstants.Giant_Frost,
                CreatureConstants.Giant_Hill,
                CreatureConstants.Giant_Stone,
                CreatureConstants.Giant_Stone_Elder,
                CreatureConstants.Giant_Storm,
                CreatureConstants.Githyanki,
                CreatureConstants.Githzerai,
                CreatureConstants.Gnoll,
                CreatureConstants.Gnome_Forest,
                CreatureConstants.Gnome_Rock,
                CreatureConstants.Gnome_Svirfneblin,
                CreatureConstants.Goblin,
                CreatureConstants.GrayRender,
                CreatureConstants.GreenHag,
                CreatureConstants.Griffon,
                CreatureConstants.Grig,
                CreatureConstants.Grig_WithFiddle,
                CreatureConstants.Grimlock,
                CreatureConstants.Gynosphinx,
                CreatureConstants.Halfling_Deep,
                CreatureConstants.Halfling_Lightfoot,
                CreatureConstants.Halfling_Tallfellow,
                CreatureConstants.Harpy,
                CreatureConstants.HellHound,
                CreatureConstants.HellHound_NessianWarhound,
                CreatureConstants.Hezrou,
                CreatureConstants.Hieracosphinx,
                CreatureConstants.Hobgoblin,
                CreatureConstants.HoundArchon,
                CreatureConstants.Howler,
                CreatureConstants.Human,
                CreatureConstants.Janni,
                CreatureConstants.Kobold,
                CreatureConstants.Krenshar,
                CreatureConstants.KuoToa,
                CreatureConstants.Lamia,
                CreatureConstants.Lammasu,
                CreatureConstants.Lillend,
                CreatureConstants.Lizardfolk,
                CreatureConstants.Locathah,
                CreatureConstants.Manticore,
                CreatureConstants.Medusa,
                CreatureConstants.Mephit_Air,
                CreatureConstants.Mephit_Dust,
                CreatureConstants.Mephit_Earth,
                CreatureConstants.Mephit_Fire,
                CreatureConstants.Mephit_Ice,
                CreatureConstants.Mephit_Magma,
                CreatureConstants.Mephit_Ooze,
                CreatureConstants.Mephit_Salt,
                CreatureConstants.Mephit_Steam,
                CreatureConstants.Mephit_Water,
                CreatureConstants.Merfolk,
                CreatureConstants.MindFlayer,
                CreatureConstants.Minotaur,
                CreatureConstants.Mummy,
                CreatureConstants.Nightmare,
                CreatureConstants.Nightmare_Cauchemar,
                CreatureConstants.Nixie,
                CreatureConstants.Nymph,
                CreatureConstants.Ogre,
                CreatureConstants.OgreMage,
                CreatureConstants.Ogre_Merrow,
                CreatureConstants.Orc,
                CreatureConstants.Orc_Half,
                CreatureConstants.Owl_Giant,
                CreatureConstants.Pegasus,
                CreatureConstants.Pixie,
                CreatureConstants.Pixie_WithIrresistibleDance,
                CreatureConstants.Pseudodragon,
                CreatureConstants.Rakshasa,
                CreatureConstants.Sahuagin,
                CreatureConstants.Sahuagin_Malenti,
                CreatureConstants.Sahuagin_Mutant,
                CreatureConstants.Salamander_Average,
                CreatureConstants.Salamander_Flamebrother,
                CreatureConstants.Satyr,
                CreatureConstants.Satyr_WithPipes,
                CreatureConstants.Scorpionfolk,
                CreatureConstants.SeaHag,
                CreatureConstants.ShadowMastiff,
                CreatureConstants.ShamblingMound,
                CreatureConstants.Skum,
                CreatureConstants.Slaad_Blue,
                CreatureConstants.Slaad_Gray,
                CreatureConstants.Slaad_Green,
                CreatureConstants.Slaad_Red,
                CreatureConstants.Succubus,
                CreatureConstants.Tiefling,
                CreatureConstants.Treant,
                CreatureConstants.Triton,
                CreatureConstants.Troglodyte,
                CreatureConstants.Troll,
                CreatureConstants.Troll_Scrag,
                CreatureConstants.TrumpetArchon,
                CreatureConstants.Unicorn,
                CreatureConstants.Vrock,
                CreatureConstants.WinterWolf,
                CreatureConstants.Worg,
                CreatureConstants.Xill,
                CreatureConstants.YethHound,
                CreatureConstants.YuanTi_Abomination,
                CreatureConstants.YuanTi_Halfblood_SnakeArms,
                CreatureConstants.YuanTi_Halfblood_SnakeHead,
                CreatureConstants.YuanTi_Halfblood_SnakeTail,
                CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs,
                CreatureConstants.YuanTi_Pureblood,
                CreatureConstants.Zelekhut,
            }));
        }

        [Test]
        public void GetAllNonCharacters_ReturnsNonCharacters()
        {
            var nonCharacters = CreatureConstants.GetAllNonCharacters();
            var creatures = CreatureConstants.GetAll();
            var characters = CreatureConstants.GetAllCharacters();

            Assert.That(nonCharacters, Is.EqualTo(creatures.Except(characters)));
        }
    }
}
