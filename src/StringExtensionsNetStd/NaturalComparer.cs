using String_Extensions.support;

namespace String_Extensions;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/// <summary>
/// An IComparer implementation that sorts only strings. Strings are sorted in a natural number-aware way (i.e. abc2d comes before abc10)
/// </summary>
/// <remarks>
/// You can drop this anywhere an IComparer is used on strings. To sort files by name with numbers in the right place:
/// <para><code>new DirectoryInfo(@"C:\temp").GetFiles().Select(f=>f.Name).ToList().Sort(new NaturalComparer());</code></para>
/// Then, given these files (as ordered by default .Net sort)
/// <para>File 10, File 11, File 8, File 9</para>
/// Will be sorted correctly, as would be expected:
/// <para>File 8, File 9, File 10, File 11</para>
/// </remarks>
public class NaturalComparer : IComparer, IComparer<string>
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

        var leftMatches = StaticRegexes.WordAndNumberSplit.Matches(left);
        var rightMatches = StaticRegexes.WordAndNumberSplit.Matches(right);

        var rightEnum = rightMatches.GetEnumerator();
        using var disposable = rightEnum as IDisposable;
        foreach (Match lm in leftMatches)
        {
            if (!rightEnum.MoveNext())
            {
                // the right-hand string ran out first, so is considered "less-than" the left
                return 1;
            }

            if (rightEnum.Current is not Match rm) continue;

            var tokenResult = CompareTokens(CapturedStringFromMatch(lm), CapturedStringFromMatch(rm));
            if (tokenResult != 0)
            {
                return tokenResult;
            }
        }

        // the left hand matches are exhausted;
        // if there is more, then left was shorter, ie, less than
        // if there's no more left in the right hand, then they were all equal
        return rightEnum.MoveNext() ? -1 : 0;
    }

    /// <summary>
    /// Compare two strings by ordinal rules (case sensitive, by exact character).
    /// Strings will be considered equal if they have decimal numbers in the same places, but the numbers are different.
    /// Null is considered not equal to anything (including another null)
    /// </summary>
    public static bool EqualsIgnoreNumbers(string? left, string? right)
    {
        if (left == null || right == null) return false;
        if (left == right) return true;
            
            
        var leftMatches = StaticRegexes.WordAndNumberSplit.Matches(left);
        var rightMatches = StaticRegexes.WordAndNumberSplit.Matches(right);

        var rightEnum = rightMatches.GetEnumerator();
        using var disposable = rightEnum as IDisposable;
        foreach (Match lm in leftMatches)
        {
            if (!rightEnum.MoveNext())
            {
                // the right-hand string ran out, so the strings are not equal
                return false;
            }
            var rm = rightEnum.Current as Match;
            if (rm == null) continue;

            var areEqualEnough = CompareTokensIgnoreNumber(CapturedStringFromMatch(lm), CapturedStringFromMatch(rm));
            if (!areEqualEnough) return false;
        }

        // the left hand matches are exhausted;
        // if there is more, then left was shorter and the strings are not equal
        var anythingLeft = rightEnum.MoveNext();
            
        return !anythingLeft;
    }

    private static string CapturedStringFromMatch(Match match)
    {
        if (match.Captures.Count != 1) throw new Exception("Match captures failed");
        return match.Captures[0].Value;
    }

    private static int CompareTokens(string left, string right)
    {
        var leftIsNum = double.TryParse(left, out var leftVal);
        var rightIsNum = double.TryParse(right, out var rightVal);

        // numbers always sort in front of text
        if (leftIsNum)
        {
            if (!rightIsNum) return -1;

            // they're both numeric
            if (leftVal < rightVal) return -1;
            if (rightVal < leftVal) return 1;

            // if values are same, this might be due to leading 0s.
            // Assuming this, the longest string would indicate more leading 0s
            // which should be considered to have lower value
            return Math.Sign(right.Length - left.Length);
        }

        return String.Compare(left, right, StringComparison.Ordinal);
    }
        
    private static bool CompareTokensIgnoreNumber(string left, string right)
    {
        var leftIsNum = double.TryParse(left, out _);
        var rightIsNum = double.TryParse(right, out _);

        // for a match, either they are both numbers, or are equal strings
        return leftIsNum 
            ? rightIsNum
            : string.Equals(left, right, StringComparison.Ordinal);
    }
}