namespace StringClusterScanTests.Comparison
{
    using NUnit.Framework;
    using String_Extensions;

    [TestFixture]
    public class CompareWildcardTests
    {

        [Test]
        [TestCase("haystack", "needle", /*match*/ false)]
        [TestCase("simple match", "simple match", true)]
        [TestCase("simple star", "*", true)]
        [TestCase("", "*", true)] // star will match empty
        [TestCase("case sensitive", "Case Sensitive", false)]
        [TestCase("prefix", "pre*", true)]
        [TestCase("postfix", "*fix", true)]
        [TestCase("emptystar", "empty*star", false)] // star can't be an empty infix (acts like regex `.+?`)
        [TestCase("emptyprefix", "emptyprefix*", true)] // star may match nothing at end of input
        [TestCase("emptypostfix", "*emptypostfix", false)] // start can't match nothing an start of input
        [TestCase("intercedents", "*c*", true)]
        [TestCase("intercedents", "*X*", false)] // must match entire patten.
        [TestCase("special \r\n chars", "special*chars", true)]
        public void case_sensitive_kleene_stars(string haystack, string needle, bool expected)
        {
            Assert.That(haystack.CompareWildcard(needle, false), Is.EqualTo(expected));
        }

        [Test]
        [TestCase("haystack", "h?yst??k", true)]
        [TestCase("haystack", "h?yst?k", false)]
        [TestCase("prefix", "prefix?", false)] // wildcard can never be empty
        [TestCase("postfix", "?postfix", false)] // wildcard can never be empty
        [TestCase("special\r\nchars", "special??chars", true)]
        public void case_sensitive_wildcards(string haystack, string needle, bool expected)
        {
            Assert.That(haystack.CompareWildcard(needle, false), Is.EqualTo(expected));
        }

        [Test]
        [TestCase("haystack", "h?yst*k", true)]
        [TestCase("haystack", "h?yst?*k", true)]
        [TestCase("haystack", "h?yst*?k", true)] // star is non-greedy, so wildcard still matches
        [TestCase("haystack", "h?yst??*k", false)] // would cause an empty infix star
        [TestCase("haystack", "h?yst**k", true)] // two stars are meaningless, but happen to pass
        [TestCase("special\r\nchars", "special*?chars", true)]
        public void mixing_stars_and_wildcards (string haystack, string needle, bool expected)
        {
            Assert.That(haystack.CompareWildcard(needle, false), Is.EqualTo(expected));
        }

        [Test]
        [TestCase("Case Insensitive", "cAsE In??NS*E", true)]
        [TestCase("cASE iNSENSITIVE", "CaSe iN??ns*e", true)]
        public void case_insensitive_matching(string haystack, string needle, bool expected)
        {
            Assert.That(haystack.CompareWildcard(needle, true), Is.EqualTo(expected));
        }

    }
}