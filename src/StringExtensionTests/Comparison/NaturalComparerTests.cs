namespace StringClusterScanTests.Comparison
{
    using System.Linq;
    using NUnit.Framework;
    using String_Extensions;

    [TestFixture]
    public class NaturalComparerTests
    {
        string[] _inputSimple, _expectedSimple, _inputMixedStarts, _expectedMixedStarts, _inputsLeadingZeros, _expectedLeadingZeros, _inputsMultiPart, _expectedMultiPart;

        [SetUp]
        public void setup()
        {
            _inputSimple = new[] { "A", "B", "C", "1", "2", "3" };
            _expectedSimple = new[] { "1", "2", "3", "A", "B", "C" };

            _inputMixedStarts = new[] { "A10", "B1", "C002", "C01", "1" };
            _expectedMixedStarts = new[] { "1", "A10", "B1", "C01", "C002" };

            _inputsLeadingZeros = new[] { "003", "00005", "0004", "1", "02" };
            _expectedLeadingZeros = new[] { "1", "02", "003", "0004", "00005" };

            _inputsMultiPart = new[] { "Word 1 A 1", "Word 1 B 2", "Word 2 A 10", "Word 1 A 10", "Word 1 B 20" };
            _expectedMultiPart = new[] { "Word 1 A 1", "Word 1 A 10", "Word 1 B 2", "Word 1 B 20", "Word 2 A 10" };
        }

        [Test]
        public void numbers_sort_before_letters()
        {
            var actual = _inputSimple.ToList();
            actual.Sort(new NaturalComparer());

            Assert.That(actual.SequenceEqual(_expectedSimple));
        }

        [Test]
        public void lexical_prefixes_supercede_numerical_value()
        {
            var actual = _inputMixedStarts.ToList();
            actual.Sort(new NaturalComparer());

            Assert.That(actual.SequenceEqual(_expectedMixedStarts));
        }

        [Test]
        public void leading_zeros_are_ignored_in_sort_order()
        {
            var actual = _inputsLeadingZeros.ToList();
            actual.Sort(new NaturalComparer());

            Assert.That(actual.SequenceEqual(_expectedLeadingZeros));
        }

        [Test]
        public void fragments_are_ordered_left_to_right()
        {
            var actual = _inputsMultiPart.ToList();
            actual.Sort(new NaturalComparer());

            Assert.That(actual.SequenceEqual(_expectedMultiPart));
        }

         
    }
}