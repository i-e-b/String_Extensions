// ReSharper disable InconsistentNaming
// ReSharper disable PossibleNullReferenceException
namespace StringClusterScanTests.Search
{
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
            Assert.That(haystack.Find(needle), Is.EqualTo(expectedOffset));
        }

        // ReSharper disable StringLiteralTypo
        [Test]
        [TestCase("contains simple match", "gross", -1)]
        [TestCase("contains scattered match", "actetrdse", -1)]
        [TestCase("short", "very very long", -1)]
        // ReSharper restore StringLiteralTypo
        public void non_matches(string haystack, string needle, int expectedOffset)
        {
            Assert.That(haystack.Find(needle), Is.EqualTo(expectedOffset));
        }

        [Test, Description("The hashed find is resilient to collisions")]
        [TestCase("contains abc match", "bbb", -1)] // equal sum and length
        [TestCase("contains abc match", "Ã", -1)]  // different length
        public void collisions(string haystack, string needle, int expectedOffset)
        {
            Assert.That(haystack.Find(needle), Is.EqualTo(expectedOffset));
        }

        [Test]
        public void speed_test()
        {
            // ReSharper disable ReturnValueOfPureMethodIsNotUsed
            var huge_string = GenerateBigString();
            const string small_needle = "needle";

            var Boyer_Moore = Time(() => huge_string.IndexOf(small_needle, StringComparison.Ordinal)); // I think .Net uses Boyer-Moore.
            var hash_scan = Time(() => huge_string.FindCluster(small_needle));

            Console.WriteLine("Small needle can be found -> Optimised native: " + Boyer_Moore + ", hash scan: " + hash_scan);

            var big_needle = new String('x', 1000);
            Boyer_Moore = Time(() => huge_string.IndexOf(big_needle, StringComparison.Ordinal));
            hash_scan = Time(() => huge_string.Find(big_needle));

            Console.WriteLine("Large needle can't found  -> Optimised native: " + Boyer_Moore + ", hash scan: " + hash_scan);
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
}