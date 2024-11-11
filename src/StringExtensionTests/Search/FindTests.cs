// ReSharper disable InconsistentNaming
// ReSharper disable PossibleNullReferenceException
namespace StringClusterScanTests.Search;

using System;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;
using String_Extensions;

[TestFixture]
public class FindTests
{
    [Test]
    [TestCase("contains simple match", "simple", 9)]
    [TestCase("at start", "at", 0)]
    [TestCase("near end", "end", 5)]
    [TestCase("last", "t", 3)]
    [TestCase("", "", 0)]
    public void matches(string haystack, string needle, int expectedOffset)
    {
        Assert.That(haystack.FindRollingHash(needle), Is.EqualTo(expectedOffset), "Rolling hash method");
        Assert.That(haystack.FindBoyerMoor(needle), Is.EqualTo(expectedOffset), "Boyer-Moor method");
    }

    // ReSharper disable StringLiteralTypo
    [Test]
    [TestCase("contains simple match", "gross", -1)]
    [TestCase("contains scattered match", "actetrdse", -1)]
    [TestCase("short", "very very long", -1)]
    // ReSharper restore StringLiteralTypo
    public void non_matches(string haystack, string needle, int expectedOffset)
    {
        Assert.That(haystack.FindRollingHash(needle), Is.EqualTo(expectedOffset), "Rolling hash method");
        Assert.That(haystack.FindBoyerMoor(needle), Is.EqualTo(expectedOffset), "Boyer-Moor method");
    }

    [Test, Description("The hashed find is resilient to collisions")]
    [TestCase("contains abc match", "bbb", -1)] // equal sum and length
    [TestCase("contains abc match", "Ã", -1)]  // different length
    public void collisions(string haystack, string needle, int expectedOffset)
    {
        Assert.That(haystack.FindRollingHash(needle), Is.EqualTo(expectedOffset));
    }

    [Test]
    public void speed_test()
    {
        // ReSharper disable ReturnValueOfPureMethodIsNotUsed
        var huge_string = GenerateBigString();
        const string small_needle = "needle";

        var native      = Time(() => huge_string.IndexOf(small_needle, StringComparison.Ordinal));
        var Boyer_Moore = Time(() => huge_string.FindBoyerMoor(small_needle));
        var hash_scan   = Time(() => huge_string.FindCluster(small_needle));

        Console.WriteLine($"Small needle can be found -> Optimised native: {native}, hash scan: {hash_scan}, Boyer-Moor: {Boyer_Moore}");

        var big_needle  = new String('x', 1000);
        native      = Time(() => huge_string.IndexOf(big_needle, StringComparison.Ordinal));
        Boyer_Moore = Time(() => huge_string.FindBoyerMoor(big_needle));
        hash_scan = Time(() => huge_string.FindRollingHash(big_needle));

        Console.WriteLine($"Large needle can't be found -> Optimised native: {native}, hash scan: {hash_scan}, Boyer-Moor: {Boyer_Moore}");
        // ReSharper restore ReturnValueOfPureMethodIsNotUsed
    }

    static string Time(Action act)
    {
        var sw = new Stopwatch();
        sw.Start();
        act();
        sw.Stop();
        return sw.Elapsed.TotalMilliseconds.ToString("0.000");
    }

    static string GenerateBigString()
    {
        var sb = new StringBuilder();
        for (var i = 0; i < 1000000; i++)
        {
            sb.Append((char)(33 + i % 93));
        }
        sb.Append("needle");
        for (var i = 0; i < 1000000; i++)
        {
            sb.Append((char)(33 + i % 93));
        }
        return sb.ToString();
    }
}