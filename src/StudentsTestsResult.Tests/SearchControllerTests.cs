using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace StudentsTestsResult.Tests
{
    [TestFixture]
    internal class SearchControllerTests
    {
        private IReadOnlyCollection<StudentTest> _tests;

        [OneTimeSetUp]
        public void SetUp()
        {
            _tests = TestsController.GetTestsResultsFromFile();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _tests = null;
        }

        [TestCase(null, null)]
        public void SearchController_GivenNull_ArgumentNullException(IEnumerable<StudentTest> tests2, Flags flagsForSearch2)
        {
            // arrange
            var test1 = new List<StudentTest>();
            var flag = new Flags();

            // assert
            Assert.That(() => new SearchController(tests2, flagsForSearch2), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => new SearchController(test1, null), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => new SearchController(null, flag), Throws.TypeOf<ArgumentNullException>());
        }

        [TestCaseSource(nameof(flagsAndExpectedResult))]
        public void GetResults_GivenTestsAndFlags_ReturnExpected(Flags flags, StudentTest[] expected)
        {
            // arrange
            var searchController = new SearchController(_tests, flags);

            // act
            var results = searchController.GetResults().ToArray();

            // assert
            Assert.That(results, Is.EquivalentTo(expected));
        }

        [Test]
        public void GetResults_GivenTestsAndFlags_ReturnExpectedOrderByAsc()
        {
            // arrange
            var searchController = new SearchController(_tests, _flags4);

            // act
            var results = searchController.GetResults().ToArray();

            // assert
            Assert.That(results, Is.EquivalentTo(_expected3.Reverse().ToArray()).And.Ordered.Ascending.By("Mark"));
        }

        [Test]
        public void GetResults_GivenTestsAndFlags_ReturnExpectedOrderByDesc()
        {
            // arrange
            var searchController = new SearchController(_tests, _flags3);

            // act
            var results = searchController.GetResults().ToArray();

            // assert
            Assert.That(results, Is.EquivalentTo(_expected3).And.Ordered.Descending.By("Mark"));
        }

        private static Flags _flags1 = new() { Name = "Max", DateFrom = default, DateTo = DateTime.Now, MinMark = 4, MaxMark = 4 };
        private static Flags _flags2 = new() { Soname = "Greenfield" };
        private static Flags _flags3 = new() { Name = "Max", SortProperty = "Mark", SortWay = "desc" };
        private static Flags _flags4 = new() { Name = "Max", SortProperty = "Mark", SortWay = "asc" };

        private static StudentTest[] _expected1 = new StudentTest[] { new() { Name = "Max", Soname = "Lebowski", Test = "Geography", Date = new DateTime(2020, 6, 1), Mark = 4 } };
        private static StudentTest[] _expected2 = new StudentTest[] { new() { Name = "Max", Soname = "Greenfield", Test = "Math", Date = new DateTime(2020, 6, 15), Mark = 5 } };
        private static StudentTest[] _expected3 = new StudentTest[]
        {
            new() { Name = "Max", Soname = "Greenfield", Test = "Math", Date = new DateTime(2020, 6, 15), Mark = 5 },
            new() { Name = "Max", Soname = "Lebowski", Test = "Math", Date = new DateTime(2020, 6, 10), Mark = 3 },
            new() { Name = "Max", Soname = "Lebowski", Test = "Geography", Date = new DateTime(2020, 6, 1), Mark = 4 },
        };

        private static object[] flagsAndExpectedResult = new object[]
        {
            new object[] { _flags1, _expected1 },
            new object[] { _flags2, _expected2 },
        };
    }
}