namespace StringClusterScanTests.Substring
{
    using System;
    using NUnit.Framework;
    using String_Extensions;

    [TestFixture]
    public class SubstringTests
    {
        [Test]
        [TestCase("once upon a time", 'o', "nce upon a time")]
        [TestCase("once upon a time", 'e', " upon a time")]
        [TestCase("once upon a time", 'z', "once upon a time")]
        [TestCase("once upon a timez", 'z', "")]
        [TestCase("once upon a timze", 'z', "e")]
        public void substring_after_char(string src, char c, string expected)
        {
            Assert.That(src.SubstringAfter(c), Is.EqualTo(expected));
        }

        [Test]
        [TestCase("once upon a time", "once", " upon a time")]
        [TestCase("once upon a time", "e", " upon a time")]
        [TestCase("once upon a time", "foo", "once upon a time")]
        [TestCase("once upon a time", "Once", "once upon a time")]
        [TestCase("once upon a time", "time", "")]
        [TestCase("once upon a time", "tim", "e")]
        public void substring_after_string_default(string src, string s, string expected)
        {
            Assert.That(src.SubstringAfter(s), Is.EqualTo(expected));
        }

        [Test]
        [TestCase("once upon a time", "Once", " upon a time")]
        [TestCase("ONCE upon a time", "e", " upon a time")]
        [TestCase("once upon a time", "TIME", "")]
        public void substring_after_string_case_insensitive(string src, string s, string expected)
        {
            Assert.That(src.SubstringAfter(s, StringComparison.OrdinalIgnoreCase), Is.EqualTo(expected));
        }
         
    }
}