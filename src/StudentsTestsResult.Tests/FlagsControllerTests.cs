﻿using System;
using NUnit.Framework;

namespace StudentsTestsResult.Tests
{
    [TestFixture]
    internal class FlagsControllerSpanTests
    {
        [Test]
        public void FlagsControllerSpan_GivenString_ReturnsValidFlags()
        {
            var input = "-name Robin -soname Scherbatsky -test Math -minmark 3 -maxmark 5 -datefrom 20/01/2020 -dateto 20/12/2020 -sort date asc";

            var flagsConrtoller = new FlagsControllerSpan(input);
            var actual = flagsConrtoller.FlagsForSearch;

            var expected = new Flags
            {
                Name = "Robin",
                Soname = "Scherbatsky",
                Test = "Math",
                DateFrom = new DateTime(2020, 1, 20),
                DateTo = new DateTime(2020, 12, 20),
                MinMark = 3,
                MaxMark = 5,
                SortProperty = "date",
                SortWay = "asc"
            };

            Assert.AreEqual(expected, actual);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void FlagsControllerSpan_GivenNullOrWhitespace_ArgumentNullException(string input)
        {
            Assert.That(() => new FlagsControllerSpan(input), Throws.TypeOf<ArgumentNullException>());
        }

        [TestCase("-datefrom 01/011/2020")]
        [TestCase("-dateto 01.01.2020")]
        [TestCase("-sort name")]
        [TestCase("-sort sort asc")]
        [TestCase("-sort")]
        [TestCase("-sort name asc desc")]
        [TestCase("-sort what? asc")]
        [TestCase("-sort date how?")]
        [TestCase("-name Max -no way!")]
        [TestCase("-name -soname")]
        public void FlagsControllerSpan_GivenNotValidFlags_FormatException(string input)
        {
            Assert.That(() => new FlagsControllerSpan(input), Throws.TypeOf<FormatException>());
        }
    }
}