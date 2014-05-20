namespace StringClusterScanTests.Replace
{
    using System;
    using NUnit.Framework;
    using String_Extensions;

    [TestFixture]
    public class ReplaceExtensionTests
    {
        [Test]
        [TestCase("not found", "fish", "goat", "not found")]
        [TestCase("short", "very very long", "goat", "short")]
        public void output_is_not_changed_if_no_match (string haystack, string find, string replace, string expected)
        {
            Assert.That(haystack.ReplaceCaseInvariant(find, replace), Is.EqualTo(expected));
        }

        [Test]
        [TestCase("not found", null, "goat", "not found")]
        public void null_target_throws_exception (string haystack, string find, string replace, string expected)
        {
            Assert.Throws<ArgumentNullException>(() => haystack.ReplaceCaseInvariant(find, replace));
        }

        [Test]
        [TestCase(null, "", "goat", "not found")]
        public void null_src_throws_exception(string haystack, string find, string replace, string expected)
        {
            Assert.Throws<ArgumentNullException>(() => haystack.ReplaceCaseInvariant(find, replace));
        }

        [Test]
        [TestCase("not found", "fish", null, "not found")]
        [TestCase("not fish", "fish", null, "not found")]
        public void null_replacement_throws_exception(string haystack, string find, string replace, string expected)
        {
            Assert.Throws<ArgumentNullException>(() => haystack.ReplaceCaseInvariant(find, replace));
        }

        [Test]
        [TestCase("not fish", "fish", "goat", "not goat")]
        [TestCase("not FISH", "fish", "goat", "not goat")]
        [TestCase("not fish", "FISH", "goat", "not goat")]
        [TestCase("not FiSh", "fiSH", "goat", "not goat")]
        public void finds_matching_and_mismatching_case (string haystack, string find, string replace, string expected)
        {
            Assert.That(haystack.ReplaceCaseInvariant(find, replace), Is.EqualTo(expected));
        }

        [Test]
        [TestCase("not FiSh", "fIsH", "gOaT", "not gOaT")]
        public void preserves_case_of_replacement (string haystack, string find, string replace, string expected)
        {
            Assert.That(haystack.ReplaceCaseInvariant(find, replace), Is.EqualTo(expected));
        }

        [Test]
        [TestCase("not FiSh", "", "gOaT", "not FiSh")]
        public void empty_target_replaces_nothing (string haystack, string find, string replace, string expected)
        {
            Assert.That(haystack.ReplaceCaseInvariant(find, replace), Is.EqualTo(expected));
        }

        [Test]
        [TestCase("not fish", "fish", "", "not ")]
        [TestCase("not FISH", "fish", "", "not ")]
        [TestCase("not fish", "FISH", "", "not ")]
        [TestCase("not FiSh", "fiSH", "", "not ")]
        public void empty_replacement_deletes_target(string haystack, string find, string replace, string expected)
        {
            Assert.That(haystack.ReplaceCaseInvariant(find, replace), Is.EqualTo(expected));
        }

        [Test]
        [TestCase("one and two and three", "AND", "OR", "one OR two OR three")] // multiple replace
        public void replaces_multiple_instances (string haystack, string find, string replace, string expected)
        {
            Assert.That(haystack.ReplaceCaseInvariant(find, replace), Is.EqualTo(expected));
        }
    }
}