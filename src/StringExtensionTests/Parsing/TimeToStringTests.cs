using System;
using NUnit.Framework;
using String_Extensions;

namespace StringClusterScanTests.Parsing;

[TestFixture]
public class TimeToStringTests
{
    [Test]
    public void can_convert_two_dates_into_a_difference_string()
    {
        var target = new DateTime(2025, 3, 2, 9, 45, 47, DateTimeKind.Utc);
        var actual = new DateTime(2025, 3, 2, 9, 44, 12, DateTimeKind.Utc);

        var result = target.TimeLag(actual);

        Console.WriteLine(result);
        Assert.That(result, Is.EqualTo("1.6 min"));

        Assert.That(actual.TimeLag(target), Is.EqualTo("0 s"), "Over target is zero seconds");
    }

    [Test]
    public void can_convert_time_span_to_string()
    {
        var result = TimeSpan.FromSeconds(12.3456).Approximate();

        Assert.That(result, Is.EqualTo("12 s"));
    }
}