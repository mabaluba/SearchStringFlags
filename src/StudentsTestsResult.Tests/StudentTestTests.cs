using NUnit.Framework;

namespace StudentsTestsResult.Tests
{
    [TestFixture]
    internal class StudentTestTests
    {
        [Test]
        public void Equals_GivenNull_ArgumentNullException()
        {
            Assert.That(() => new StudentTest().Equals(null), Throws.ArgumentNullException);
        }
    }
}