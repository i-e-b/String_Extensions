using System;
using System.Globalization;
using NUnit.Framework;
using String_Extensions;

namespace StringClusterScanTests.Parsing;

[TestFixture]
public class ParseDoubleExactTests
{
    [Test]
    [TestCase("0", 0.0)]
    [TestCase("-0", -0.0)]
    [TestCase("1", 1.0)]
    [TestCase("-1", -1.0)]
    [TestCase("001", 1.0)]
    [TestCase("-001", -1.0)]
    [TestCase("0.00000000000000212312312", 2.12312312E-15)]
    [TestCase("-0.00000000000000212312312", -2.12312312E-15)]
    [TestCase("4.56E+2", 456)]
    [TestCase("-4.56E-2", -0.0456)]
    [TestCase("-4424.5644E-223", -4.4245644E-220)]
    [TestCase("4424.5644E+223", 4.4245644E+226)]
    // These special values are case sensitive
    [TestCase("+inf", double.PositiveInfinity)]
    [TestCase("-inf", double.NegativeInfinity)]
    [TestCase("NaN", double.NaN)]
    public void parse_double_examples(string input, double expected)
    {
        var actual = DoubleExact.Parse(input).ToDouble();
        Assert.That(actual, Is.EqualTo(expected), $"{input} -> {actual}");
    }

    [Test]
    public void parse_double_fuzz()
    {
        var rnd = new Random(123456789);
        var buf = new byte[8];
        for (int i = 0; i < 1000; i++)
        {
            rnd.NextBytes(buf);
            var source = BitConverter.ToDouble(buf, 0);
                
            var expected = source.ToString(CultureInfo.InvariantCulture);
            var parsed = DoubleExact.Parse(expected).ToDouble();
            var actual = parsed.ToString(CultureInfo.InvariantCulture);
                
            Console.WriteLine($"{expected} -> {actual}");
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}