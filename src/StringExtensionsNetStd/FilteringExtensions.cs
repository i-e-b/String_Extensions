using System;
using System.Linq;
using System.Text;

namespace String_Extensions;

/// <summary>
/// Extensions to filter or remove partial contents of strings
/// </summary>
public static class FilteringExtensions
{
    /// <summary>
    /// Return a string with all instances of all <paramref name="unwanted"/> strings removed.
    /// </summary>
    public static string Remove(this string src, params string[] unwanted)
    {
        return unwanted.Aggregate(src, (current, target) => current.Replace(target, ""));
    }

    /// <summary>
    /// Return a string with all instances of all <paramref name="unwanted"/> strings removed.
    /// Matching between input and unwanted is done case-insensitive
    /// </summary>
    public static string RemoveCaseInvariant(this string src, params string[] unwanted)
    {
        return unwanted.Aggregate(src, (current, target) => current.ReplaceCaseInvariant(target, ""));
    }

    /// <summary>
    /// Remove a string omitting all decimal digit characters.
    /// </summary>
    public static string RemoveNumbers(this string src) => src.FilterCharacters(char.IsDigit);

    /// <summary>
    /// Keep only ASCII alphabetic and decimal characters in the string
    /// </summary>
    public static string RemoveNonAlphaNumeric(this string src) => src.FilterCharacters(c=>!char.IsLetterOrDigit(c));

    /// <summary>
    /// Remove a string omitting characters where the function returns <c>true</c>
    /// </summary>
    public static string FilterCharacters(this string src, Func<char, bool> remove)
    {
        var sb = new StringBuilder();

        foreach (var c in src)
        {
            if (remove(c)) continue;
            sb.Append(c);
        }

        return sb.ToString();
    }

    /// <summary>
    /// Replace all runs of whitespace with a single space character
    /// </summary>
    public static string NormaliseWhitespace(this string src)
    {
        var sb = new StringBuilder();

        var inSpace = false;
        foreach (var c in src)
        {
            if (char.IsWhiteSpace(c))
            {
                if (inSpace) continue;
                sb.Append(' ');
                inSpace = true;
                continue;
            }

            inSpace = false;
            sb.Append(c);
        }

        return sb.ToString();
    }
}