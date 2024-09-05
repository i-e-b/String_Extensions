using NUnit.Framework;
using String_Extensions;
// ReSharper disable PossibleNullReferenceException

namespace StringClusterScanTests.Replace;

[TestFixture]
public class CaseSwitchTests
{
    [Test]
    [TestCase("lower case", 0, "Lower case")]
    [TestCase("lower case", 6, "lower Case")]
    [TestCase("lower case", 9, "lower casE")]
    [TestCase("lower case", -10, "Lower case")]
    [TestCase("lower case", -1, "lower casE")]
    [TestCase("lower case", -11, "lower case")]
    [TestCase("lower case", -100, "lower case")]
    [TestCase("lower case", 10, "lower case")]
    [TestCase("lower case", 100, "lower case")]
    [TestCase(" ", 0, " ")]
    [TestCase("", 0, "")]
    [TestCase("+++", 1, "+++")]
    [TestCase("Sentence case", 0, "Sentence case")]
    public void upper_case_single_character_on_a_string(string input, int index, string expected)
    {
        var actual = input.UpperCaseIndex(index);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    [TestCase("Mixed case String", "MIXED CASE STRING")]
    [TestCase("MiXeD cAsE StRiNg", "MIXED CASE STRING")]
    [TestCase("ß", "ß")] // we specifically DON'T convert to "SS"
    [TestCase("123 £%$: AAA aaa ǉ ǆ", "123 £%$: AAA AAA Ǉ Ǆ")]
    public void upper_case_on_a_string(string input, string expected)
    {
        var actual = input.ToUpperSimple();
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    [TestCase("Mixed case String", "mixed case string")]
    [TestCase("MiXeD cAsE StRiNg", "mixed case string")]
    [TestCase("SS", "ss")] // we specifically DON'T convert to "ß"
    [TestCase("123 £%$: AAA aaa Ǉ Ǆ", "123 £%$: aaa aaa ǉ ǆ")]
    public void lower_case_on_a_string(string input, string expected)
    {
        var actual = input.ToLowerSimple();
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    [TestCase(new[]{'a','A','Ě','ě','Ű','ű','Ǟ','ǟ'},
        new[]{'A','A','Ě','Ě','Ű','Ű','Ǟ','Ǟ'},
        new[]{'a','a','ě','ě','ű','ű','ǟ','ǟ'})]
    public void case_switching_on_single_char(char[] input, char[] upper, char[] lower)
    {
        for (int i = 0; i < input.Length; i++)
        {
            Assert.That(input[i].ToLowerSimple(), Is.EqualTo(lower[i]));
            Assert.That(input[i].ToUpperSimple(), Is.EqualTo(upper[i]));
        }
    }

}