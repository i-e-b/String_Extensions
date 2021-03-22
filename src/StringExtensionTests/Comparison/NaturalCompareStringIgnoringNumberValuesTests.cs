using NUnit.Framework;
using String_Extensions;

namespace StringClusterScanTests.Comparison
{
    [TestFixture]
    public class NaturalCompareStringIgnoringNumberValuesTests
    {
        [Test]
        [TestCase("1 is less than 10", "007 is less than 10000")]
        [TestCase("the 10th day is Tuesday", "the 4th day is Tuesday")]
        [TestCase("2", "7")]
        public void strings_are_considered_the_same_if_they_vary_only_by_number_value(string a, string b)
        {
            Assert.That(NaturalComparer.EqualsIgnoreNumbers(a,b), Is.True, $"'{a}' was considered not to match '{b}', but should have");
        }

        [Test]
        [TestCase("1 is less than one hundred","1 is less than 100")]
        [TestCase("the 10th day is Tuesday","the day is Tuesday")]
        [TestCase("very totally","different")]
        public void strings_are_considered_not_equal_if_numbers_are_missing_or_in_different_places(string a, string b)
        {
            Assert.That(NaturalComparer.EqualsIgnoreNumbers(a,b), Is.False, $"'{a}' was considered to match '{b}', but should not have");
        }

        [Test]
        public void null_is_not_equal_to_anything()
        {
            Assert.That(NaturalComparer.EqualsIgnoreNumbers(null, "a string"), Is.False, "null left was wrong");
            Assert.That(NaturalComparer.EqualsIgnoreNumbers("a string", null), Is.False, "null right was wrong");
            Assert.That(NaturalComparer.EqualsIgnoreNumbers(null, null), Is.False, "null both was wrong");
        }

        [Test]
        public void exactly_equal_strings_are_considered_equal()
        {
            Assert.That(NaturalComparer.EqualsIgnoreNumbers("they are the same picture", "they are the same picture"), Is.True, "same string was not matched correctly");
        }
    }
}