using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using String_Extensions.support;

namespace String_Extensions;

/// <summary>
/// An IComparer implementation that sorts only strings.
/// Strings are sorted only by numerical value; strings without numbers are sorted last.
/// <p/>
/// All number values are treated as positive, with the <c>-</c> character interpreted
/// as a separator.
/// </summary>
/// <example>
/// Given <c>File, Users 2020-01-01, Accounts 2020-02-03, Accounts 2020-02-03_v2</c>
/// this will output
/// <c>Users 2020-01-01, Accounts 2020-02-03, Accounts 2020-02-03_v2, File</c>
/// </example>
/// <seealso cref="NaturalComparer"/>
public class NumbersOnlyComparer : IComparer, IComparer<string>
{
    int IComparer.Compare(object? left, object? right)
    {
        if (left is not string leftStr) throw new ArgumentException("Parameter type is not string", nameof(left));
        if (right is not string rightStr) throw new ArgumentException("Parameter type is not string", nameof(right));
        return Compare(leftStr, rightStr);
    }

    /// <summary>
    /// Compare two strings for relative sort order
    /// </summary>
    public int Compare(string? left, string? right)
    {
        if (left == right) return 0;
        if (left == null) return -1;
        if (right == null) return 1;

        var leftMatches = OnlyNumbers(StaticRegexes.WordAndNumberSplit.Matches(left)).ToList();
        var rightMatches = OnlyNumbers(StaticRegexes.WordAndNumberSplit.Matches(right)).ToList();

        var rightEnum = rightMatches.GetEnumerator();
        using var disposable = rightEnum as IDisposable;
        foreach (var leftPart in leftMatches)
        {
            if (!rightEnum.MoveNext())
            {
                // the right-hand string ran out first, so is considered "greater-than" the left
                return -1;
            }

            var rightPart = rightEnum.Current;
            if (leftPart < rightPart) return -1;
            if (leftPart > rightPart) return 1;
        }

        // Here, all matched numbers are equal.

        // If ONLY one part has no numbers, it goes to the right
        if (leftMatches.Count < 1 && rightMatches.Count > 0) return 1;
        if (rightMatches.Count < 1 && leftMatches.Count > 0) return -1;

        // Otherwise we sort the one with the least number parts on the left
        if (leftMatches.Count < rightMatches.Count) return -1;
        if (leftMatches.Count > rightMatches.Count) return 1;

        // Otherwise, sort them as plain strings
        return string.Compare(left, right, StringComparison.Ordinal);
    }

    private static IEnumerable<double> OnlyNumbers(MatchCollection matches)
    {
        foreach (Match match in matches)
        {
            if (match.Captures.Count != 1) continue;
            if (double.TryParse(match.Captures[0].Value.Trim('-', '.', ','), out var number))
                yield return number;
        }
    }
}