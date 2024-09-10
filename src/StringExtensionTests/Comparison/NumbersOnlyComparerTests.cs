using System;
using System.Linq;
using NUnit.Framework;
using String_Extensions;

namespace StringClusterScanTests.Comparison;

[TestFixture]
public class NumbersOnlyComparerTests
{
    [Test]
    public void numbers_sort_before_letters()
    {
        var input = new[] { "File", "Users 2020-01-01", "Accounts 2020-02-03", "Accounts 2020-02-03_v2" };
        var expected = new[] { "Users 2020-01-01", "Accounts 2020-02-03", "Accounts 2020-02-03_v2", "File" };

        var actual = input!.OrderBy(a=>a, new NumbersOnlyComparer()).ToList();

        Console.WriteLine(string.Join(", ", actual));
        Assert.That(actual.SequenceEqual(expected));
    }

    [Test]
    public void decimals_and_commas_are_separators()
    {
        var input = new[] { "One.1.2.3", "Two_2_3_4", "Three, 3, 4", "Four-1-2-3" };
        var expected = new[] { "Four-1-2-3", "One.1.2.3", "Two_2_3_4", "Three, 3, 4" };

        var actual = input!.OrderBy(a=>a, new NumbersOnlyComparer()).ToList();

        Console.WriteLine(string.Join(", ", actual));
        Assert.That(actual.SequenceEqual(expected));
    }
}