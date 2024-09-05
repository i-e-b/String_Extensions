namespace StringClusterScanTests.Substring;

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
    [TestCase("path/like/string", '/', "like/string")]
    [TestCase("path\\like\\string", '\\', "like\\string")]
    public void substring_after_char(string src, char c, string expected)
    {
        Assert.That(src.SubstringAfter(c), Is.EqualTo(expected));
    }

    [Test]
    [TestCase("once upon a time", "once", " upon a time")]
    [TestCase("it happened once upon a time", "once", " upon a time")]
    [TestCase("once upon a time", "e", " upon a time")]
    [TestCase("once upon a time", "foo", "once upon a time")]
    [TestCase("once upon a time", "Once", "once upon a time")]
    [TestCase("once upon a time", "time", "")]
    [TestCase("once upon a time", "tim", "e")]
    [TestCase("path/like/string", "/", "like/string")]
    [TestCase("path\\like\\string", "\\", "like\\string")]
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
    [TestCase("once upon a time", "on", "once up")]
    [TestCase("once upon a time", "n", "once upo")]
    [TestCase("once upon a time up high", "up", "once upon a time ")]
    [TestCase("once upon a time", "zorro", "once upon a time")]
    [TestCase("once upon a timez", "z", "once upon a time")]
    [TestCase("once upon a timze", "z", "once upon a tim")]
    public void substring_before_last_string(string src, string s, string expected)
    {
        Assert.That(src.SubstringBeforeLast(s), Is.EqualTo(expected));
    }


    [Test]
    [TestCase("once upon a time", 'o', "n a time")]
    [TestCase("once upon a time", 'n', " a time")]
    [TestCase("once upon a time", 'u', "pon a time")]
    [TestCase("once upon a time", 'z', "once upon a time")]
    [TestCase("once upon a timez", 'z', "")]
    [TestCase("once upon a timze", 'z', "e")]
    [TestCase("path/like/string", '/', "string")]
    [TestCase("path\\like\\string", '\\', "string")]
    public void substring_after_last_char(string src, char c, string expected)
    {
        Assert.That(src.SubstringAfterLast(c), Is.EqualTo(expected));
    }

    [Test]
    [TestCase("once upon a time", "on", " a time")]
    [TestCase("once upon a time", "n", " a time")]
    [TestCase("once upon a time", "u", "pon a time")]
    [TestCase("once upon a time", "z", "once upon a time")]
    [TestCase("once upon a timez", "ez", "")]
    [TestCase("once upon a timze", "mz", "e")]
    [TestCase("path/like/string", "/", "string")]
    [TestCase("path\\like\\string", "\\", "string")]
    public void substring_after_last_string(string src, string pattern, string expected)
    {
        Assert.That(src.SubstringAfterLast(pattern), Is.EqualTo(expected));
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

    [Test]
    [TestCase("once upon a time", "once", 4)]
    [TestCase("once upon a time", "on", 2)]
    [TestCase("once upon a time", "upon", 9)]
    [TestCase("once upon a time", "time", 16)]
    [TestCase("once upon a time", "z", -1)]
    [TestCase("once upon a timez", "z", 17)]
    [TestCase("once upon a timze", "z", 16)]
    public void index_after_first_match(string src, string s, int expected)
    {
        Assert.That(src.IndexAfter(s), Is.EqualTo(expected));
    }

    [Test]
    [TestCase("onCe upOn a tiMe", "once", -1)]
    [TestCase("onCe upOn a tiMe", "on", 9)]
    [TestCase("onCe upOn a tiMe", "upon", 9)]
    [TestCase("onCe upOn a tiMe", "time", 16)]
    public void index_after_first_match_with_offset(string src, string s, int expected)
    {
        Assert.That(src.IndexAfter(s, 4, StringComparison.OrdinalIgnoreCase), Is.EqualTo(expected));
    }

    [Test]
    [TestCase("once upon a time", "once", 4)]
    [TestCase("once upon a time", "on", 9)]
    [TestCase("once upon a time", "upon", 9)]
    [TestCase("once upon a time", "time", 16)]
    [TestCase("once upon a time", "z", -1)]
    [TestCase("once upon a timez", "z", 17)]
    [TestCase("once upon a timze", "z", 16)]
    public void index_after_last_match(string src, string s, int expected)
    {
        Assert.That(src.LastIndexAfter(s), Is.EqualTo(expected));
    }

    [Test]
    [TestCase("oNce uPoN a time", "once", 4)]
    [TestCase("oNce uPoN a time", "on", 9)]
    [TestCase("oNce uPoN a time", "upon", 9)]
    [TestCase("oNce uPoN a time", "time", -1)]
    public void index_after_last_match_with_offset(string src, string s, int expected)
    {
        Assert.That(src.LastIndexAfter(s, 10, StringComparison.OrdinalIgnoreCase), Is.EqualTo(expected));
    }
}