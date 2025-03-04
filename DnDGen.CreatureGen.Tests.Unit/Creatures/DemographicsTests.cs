﻿using DnDGen.CreatureGen.Creatures;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Creatures
{
    [TestFixture]
    internal class DemographicsTests
    {
        private Demographics demographics;

        [SetUp]
        public void Setup()
        {
            demographics = new Demographics();
        }

        [Test]
        public void DemographicsInitialized()
        {
            Assert.That(demographics.Gender, Is.Empty);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Unit, Is.EqualTo("years"));
            Assert.That(demographics.MaximumAge, Is.Not.Null);
            Assert.That(demographics.MaximumAge.Unit, Is.EqualTo("years"));
            Assert.That(demographics.Height, Is.Not.Null);
            Assert.That(demographics.Height.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Length, Is.Not.Null);
            Assert.That(demographics.Length.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Weight, Is.Not.Null);
            Assert.That(demographics.Weight.Unit, Is.EqualTo("pounds"));
            Assert.That(demographics.Wingspan, Is.Not.Null);
            Assert.That(demographics.Wingspan.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Skin, Is.Empty);
            Assert.That(demographics.Hair, Is.Empty);
            Assert.That(demographics.Eyes, Is.Empty);
            Assert.That(demographics.Other, Is.Empty);
        }
    }
}
