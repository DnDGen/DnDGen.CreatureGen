using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures
{
    [TestFixture]
    internal class GendersTests : CollectionTests
    {
        private ICollectionSelector collectionSelector;

        protected override string tableName => TableNameConstants.Collection.Genders;

        [SetUp]
        public void Setup()
        {
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
        }

        [Test]
        public void SaveGroupsNames()
        {
            var creatures = CreatureConstants.GetAll();
            AssertCollectionNames(creatures);
        }

        [TestCase(CreatureConstants.Aasimar, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Aboleth, GenderConstants.Hermaphrodite)]
        [TestCase(CreatureConstants.Achaierai, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Allip, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Androsphinx, GenderConstants.Male)]
        [TestCase(CreatureConstants.Angel_AstralDeva, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Angel_Planetar, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Angel_Solar, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Ankheg, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Annis, GenderConstants.Female)]
        [TestCase(CreatureConstants.Ant_Giant_Queen, GenderConstants.Female)]
        [TestCase(CreatureConstants.Ant_Giant_Soldier, GenderConstants.Male)]
        [TestCase(CreatureConstants.Ant_Giant_Worker, GenderConstants.Male)]
        [TestCase(CreatureConstants.Aranea, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Arrowhawk_Adult, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Arrowhawk_Elder, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Arrowhawk_Juvenile, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.AssassinVine, GenderConstants.Agender)]
        [TestCase(CreatureConstants.Athach, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Avoral, GenderConstants.Agender, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Azer, GenderConstants.Agender, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Babau, GenderConstants.Agender)]
        [TestCase(CreatureConstants.Balor, GenderConstants.Agender)]
        [TestCase(CreatureConstants.BarbedDevil_Hamatula, GenderConstants.Agender)]
        [TestCase(CreatureConstants.Barghest, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Barghest_Greater, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Basilisk, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Basilisk_Greater, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.BeardedDevil_Barbazu, GenderConstants.Agender)]
        [TestCase(CreatureConstants.Bebilith, GenderConstants.Agender)]
        [TestCase(CreatureConstants.Bee_Giant, GenderConstants.Male)]
        [TestCase(CreatureConstants.Behir, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Beholder, GenderConstants.Agender)]
        [TestCase(CreatureConstants.Beholder_Gauth, GenderConstants.Agender)]
        [TestCase(CreatureConstants.Belker, GenderConstants.Agender)]
        [TestCase(CreatureConstants.BlackPudding, GenderConstants.Agender)]
        [TestCase(CreatureConstants.BlackPudding_Elder, GenderConstants.Agender)]
        [TestCase(CreatureConstants.BlinkDog, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Bodak, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.BombardierBeetle_Giant, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.BoneDevil_Osyluth, GenderConstants.Agender)]
        [TestCase(CreatureConstants.Bralani, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Bugbear, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Bulette, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.CarrionCrawler, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Centaur, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Colossal, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Gargantuan, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Huge, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Large, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Medium, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Small, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Tiny, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.ChainDevil_Kyton, GenderConstants.Agender, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.ChaosBeast, GenderConstants.Agender)]
        [TestCase(CreatureConstants.Chimera_Black, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Chimera_Blue, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Chimera_Green, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Chimera_Red, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Chimera_White, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Choker, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Chuul, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Cloaker, GenderConstants.Agender)]
        [TestCase(CreatureConstants.Cockatrice,
            GenderConstants.Male,
            GenderConstants.Male,
            GenderConstants.Male,
            GenderConstants.Male,
            GenderConstants.Male,
            GenderConstants.Male,
            GenderConstants.Male,
            GenderConstants.Male,
            GenderConstants.Male,
            GenderConstants.Female)]
        [TestCase(CreatureConstants.Couatl, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Criosphinx, GenderConstants.Male)]
        [TestCase(CreatureConstants.Cryohydra_5Heads, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Cryohydra_6Heads, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Cryohydra_7Heads, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Cryohydra_8Heads, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Cryohydra_9Heads, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Cryohydra_10Heads, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Cryohydra_11Heads, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Cryohydra_12Heads, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Darkmantle, GenderConstants.Hermaphrodite)]
        [TestCase(CreatureConstants.Delver, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Derro, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Derro_Sane, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Destrachan, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Devourer, GenderConstants.Agender)]
        [TestCase(CreatureConstants.Digester, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.DisplacerBeast, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.DisplacerBeast_PackLord, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Djinni, GenderConstants.Agender, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Djinni_Noble, GenderConstants.Agender, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Dretch, GenderConstants.Agender, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Efreeti, GenderConstants.Agender, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Erinyes, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.GelatinousCube, GenderConstants.Agender)]
        [TestCase(CreatureConstants.Glabrezu, GenderConstants.Agender)]
        [TestCase(CreatureConstants.GrayOoze, GenderConstants.Agender)]
        [TestCase(CreatureConstants.GreenHag, GenderConstants.Female)]
        [TestCase(CreatureConstants.Gynosphinx, GenderConstants.Female)]
        [TestCase(CreatureConstants.Hellcat_Bezekira, GenderConstants.Agender)]
        [TestCase(CreatureConstants.Hezrou, GenderConstants.Agender)]
        [TestCase(CreatureConstants.Hieracosphinx, GenderConstants.Male)]
        [TestCase(CreatureConstants.HornedDevil_Cornugon, GenderConstants.Agender)]
        [TestCase(CreatureConstants.Hydra_5Heads, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Hydra_6Heads, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Hydra_7Heads, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Hydra_8Heads, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Hydra_9Heads, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Hydra_10Heads, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Hydra_11Heads, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Hydra_12Heads, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.IceDevil_Gelugon, GenderConstants.Agender)]
        [TestCase(CreatureConstants.Imp, GenderConstants.Agender, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Janni, GenderConstants.Agender, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Lemure, GenderConstants.Agender)]
        [TestCase(CreatureConstants.Marilith, GenderConstants.Female)]
        [TestCase(CreatureConstants.Nalfeshnee, GenderConstants.Agender)]
        [TestCase(CreatureConstants.NightHag, GenderConstants.Female)]
        [TestCase(CreatureConstants.OchreJelly, GenderConstants.Agender)]
        [TestCase(CreatureConstants.PitFiend, GenderConstants.Agender)]
        [TestCase(CreatureConstants.Pyrohydra_5Heads, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Pyrohydra_6Heads, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Pyrohydra_7Heads, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Pyrohydra_8Heads, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Pyrohydra_9Heads, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Pyrohydra_10Heads, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Pyrohydra_11Heads, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Pyrohydra_12Heads, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Quasit, GenderConstants.Agender)]
        [TestCase(CreatureConstants.SeaHag, GenderConstants.Female)]
        [TestCase(CreatureConstants.Succubus, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Tiefling, GenderConstants.Female, GenderConstants.Male)]
        [TestCase(CreatureConstants.Vrock, GenderConstants.Agender)]
        public void Genders(string creature, params string[] genders)
        {
            Assert.Fail("Doppelganger is next");
            AssertDistinctCollection(creature, genders);
        }

        [TestCase(CreatureConstants.Types.Construct)]
        [TestCase(CreatureConstants.Types.Subtypes.Swarm)]
        public void CreaturesOfTypeAreAgender(string type)
        {
            var creatures = collectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, type);
            Assert.That(table.Keys, Is.SupersetOf(creatures));

            foreach (var creature in creatures)
            {
                AssertDistinctCollection(creature, GenderConstants.Agender);
            }
        }

        [Test]
        public void AnimalsAreMaleAndFemale()
        {
            var constructs = collectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Types.Animal);
            Assert.That(table.Keys, Is.SupersetOf(constructs));

            foreach (var creature in constructs)
            {
                AssertDistinctCollection(creature, GenderConstants.Female, GenderConstants.Male);
            }
        }
    }
}
