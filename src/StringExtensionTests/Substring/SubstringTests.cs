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


        [Test]
        [TestCase("once upon a time", 'o', "")]
        [TestCase("once upon a time", 'n', "o")]
        [TestCase("once upon a time", 'u', "once ")]
        [TestCase("once upon a time", 'z', "once upon a time")]
        [TestCase("once upon a timez", 'z', "once upon a time")]
        [TestCase("once upon a timze", 'z', "once upon a tim")]
        public void substring_before_char(string src, char c, string expected)
        {
            Assert.That(src.SubstringBefore(c), Is.EqualTo(expected));
        }

        [Test]
        [TestCase("once upon a time", 'o', "once up")]
        [TestCase("once upon a time", 'n', "once upo")]
        [TestCase("once upon a time", 'u', "once ")]
        [TestCase("once upon a time", 'z', "once upon a time")]
        [TestCase("once upon a timez", 'z', "once upon a time")]
        [TestCase("once upon a timze", 'z', "once upon a tim")]
        public void substring_before_last_char(string src, char c, string expected)
        {
            Assert.That(src.SubstringBeforeLast(c), Is.EqualTo(expected));
        }

        [Test]
        [TestCase("once upon a time", "once", "")]
        [TestCase("once upon a time", "upon", "once ")]
        [TestCase("once upon a time", "time", "once upon a ")]
        [TestCase("once upon a time", "z", "once upon a time")]
        [TestCase("once upon a timez", "z", "once upon a time")]
        [TestCase("once upon a timze", "z", "once upon a tim")]
        public void substring_before_string_default(string src, string s, string expected)
        {
            Assert.That(src.SubstringBefore(s), Is.EqualTo(expected));
        }

        [Test]
        [TestCase("once upon a time", "ONCE", "")]
        [TestCase("once UPON a time", "upon", "once ")]
        [TestCase("once upon a tIMe", "TiMe", "once upon a ")]
        [TestCase("once upon a time", "z", "once upon a time")]
        [TestCase("once upon a timez", "Z", "once upon a time")]
        [TestCase("once upon a timZe", "z", "once upon a tim")]
        public void substring_before_string_case_insensitive(string src, string s, string expected)
        {
            Assert.That(src.SubstringBefore(s, StringComparison.OrdinalIgnoreCase), Is.EqualTo(expected));
        }
         
    }
}