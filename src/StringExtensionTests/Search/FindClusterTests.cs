// ReSharper disable InconsistentNaming
// ReSharper disable PossibleNullReferenceException
namespace StringClusterScanTests.Search;

using System;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;
using String_Extensions;

[TestFixture]
public class FindClusterTests
{
    // ReSharper disable StringLiteralTypo
    [Test]
    [TestCase("contains simple match", "simple", 9)]
    [TestCase("contains scattered match", "actetrdse", 9)]
    [TestCase("at start", "at", 0)]
    [TestCase("near end", "end", 5)]
    [TestCase("last", "t", 3)]
    [TestCase("", "", 0)]
    // ReSharper restore StringLiteralTypo
    public void matches(string haystack, string needle, int expectedOffset)
    {
        Assert.That(haystack.FindCluster(needle), Is.EqualTo(expectedOffset));
    }

    [Test]
    [TestCase("contains simple match", "gross", -1)]
    [TestCase("short", "very very long", -1)]
    public void non_matches(string haystack, string needle, int expectedOffset)
    {
        Assert.That(haystack.FindCluster(needle), Is.EqualTo(expectedOffset));
    }

    [Test, Description("The algorithm is approximate, and can find false positives")]
    [TestCase("contains abc match", "bbb", 9)] // equal sum and length
    [TestCase("contains abc match", "Ã", -1)]  // different length
    public void collisions(string haystack, string needle, int expectedOffset)
    {
        Assert.That(haystack.FindCluster(needle), Is.EqualTo(expectedOffset));
    }

    [Test]
    public void speed_test()
    {
        // ReSharper disable ReturnValueOfPureMethodIsNotUsed
        var huge_string = GenerateBigString();
        const string small_needle = "needle";

        var Boyer_Moore = Time(() => huge_string.Contains(small_needle)); // I think .Net uses Boyer-Moore.
        var cluster_scan = Time(() => huge_string.FindCluster(small_needle));

        Console.WriteLine("Small needle can be found -> Optimised non-cluster: " + Boyer_Moore + ", cluster scan: " + cluster_scan);

        var big_needle = new String('x', 1000);
        Boyer_Moore = Time(() => huge_string.Contains(big_needle));
        cluster_scan = Time(() => huge_string.FindCluster(big_needle));
            
        Console.WriteLine("Large needle can't found  -> Optimised non-cluster: " + Boyer_Moore + ", cluster scan: " + cluster_scan);
        // ReSharper restore ReturnValueOfPureMethodIsNotUsed
    }

    static TimeSpan Time(Action act)
    {
        var sw = new Stopwatch();
        sw.Start();
        act();
        sw.Stop();
        return sw.Elapsed;
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