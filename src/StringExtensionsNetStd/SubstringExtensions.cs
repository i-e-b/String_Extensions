namespace String_Extensions;

using System;

/// <summary>
/// Extensions for searching and partitioning strings
/// </summary>
public static class SubstringExtensions
{
    /// <summary>
    /// Return the substring up to but not including the first instance of character 'c'.
    /// If 'c' is not found, the entire string is returned.
    /// </summary>
    public static string SubstringBefore(this string? src, char c)
    {
        if (string.IsNullOrEmpty(src)) return "";

        var idx = Math.Min(src!.Length, src.IndexOf(c));
        return idx < 0 ? src : src.Substring(0, idx);
    }


    /// <summary>
    /// Return the substring up to but not including the first instance of string 's'.
    /// If 's' is not found, the entire string is returned.
    /// </summary>
    public static string SubstringBefore(this string? src, string s, StringComparison stringComparison = StringComparison.Ordinal)
    {
        if (string.IsNullOrEmpty(src)) return "";

        var idx = Math.Min(src!.Length, src.IndexOf(s, stringComparison));
        return idx < 0 ? src : src.Substring(0, idx);
    }


    /// <summary>
    /// Return the substring up to but not including the last instance of character 'c'.
    /// If 'c' is not found, the entire string is returned.
    /// </summary>
    public static string SubstringBeforeLast(this string? src, char c)
    {
        if (string.IsNullOrEmpty(src)) return "";

        var idx = Math.Min(src!.Length, src.LastIndexOf(c));
        return idx < 0 ? src : src.Substring(0, idx);
    }


    /// <summary>
    /// Return the substring up to but not including the last instance of string 's'.
    /// If 's' is not found, the entire string is returned.
    /// </summary>
    public static string SubstringBeforeLast(this string? src, string s, StringComparison stringComparison = StringComparison.Ordinal)
    {
        if (string.IsNullOrEmpty(src)) return "";

        var idx = Math.Min(src!.Length, src.LastIndexOf(s, stringComparison));
        return idx < 0 ? src : src.Substring(0, idx);
    }

    /// <summary>
    /// Return the substring after to but not including the first instance of character 'c'.
    /// If 'c' is not found, the entire string is returned.
    /// </summary>
    public static string SubstringAfter(this string? src, char c)
    {
        if (string.IsNullOrEmpty(src)) return "";

        var idx = Math.Min(src!.Length, src.IndexOf(c) + 1);
        return idx < 0 ? src : src.Substring(idx);
    }

    /// <summary>
    /// Return the substring after to but not including the first instance of string 's'.
    /// If 's' is not found, the entire string is returned.
    /// </summary>
    public static string SubstringAfter(this string? src, string s, StringComparison stringComparison = StringComparison.Ordinal)
    {
        if (string.IsNullOrEmpty(src)) return "";

        var idx = Math.Min(src!.Length, src.ExtendedIndexOf(s, 0, s.Length, stringComparison));
        return idx < 0 ? src : src.Substring(idx);
    }

    /// <summary>
    /// Return index of target + offset, or -1 if not found.
    /// </summary>
    private static int ExtendedIndexOf(this string? src, string target, int start, int offset, StringComparison stringComparison)
    {
        if (src is null) return -1;
        var idx = src.IndexOf(target, start, stringComparison);
        return idx < 0 ? idx : idx + offset;
    }

    /// <summary>
    /// Return last index of target + offset, or -1 if not found.
    /// </summary>
    private static int ExtendedLastIndexOf(this string? src, string target, int end, int offset, StringComparison stringComparison)
    {
        if (src is null) return -1;
        var idx = src.LastIndexOf(target, end, stringComparison);
        return idx < 0 ? idx : idx + offset;
    }

    /// <summary>
    /// Return the index of the first character after a match.
    /// If match is not found, `-1` is returned.
    /// </summary>
    public static int IndexAfter(this string? src, string target, StringComparison stringComparison = StringComparison.CurrentCulture)
    {
        return ExtendedIndexOf(src, target, 0, target.Length, stringComparison);
    }

    /// <summary>
    /// Return the index of the first character after a match.
    /// If match is not found, `-1` is returned.
    /// </summary>
    public static int IndexAfter(this string? src, string target, int start, StringComparison stringComparison = StringComparison.CurrentCulture)
    {
        return ExtendedIndexOf(src, target, start, target.Length, stringComparison);
    }

    /// <summary>
    /// Return the index of the first character after the last match.
    /// If match is not found, `-1` is returned.
    /// </summary>
    public static int LastIndexAfter(this string? src, string target, StringComparison stringComparison = StringComparison.CurrentCulture)
    {
        if (src is null) return -1;
        return ExtendedLastIndexOf(src, target, src.Length - 1, target.Length, stringComparison);
    }

    /// <summary>
    /// Return the index of the first character after the last match.
    /// If match is not found, `-1` is returned.
    /// </summary>
    public static int LastIndexAfter(this string? src, string target, int end, StringComparison stringComparison = StringComparison.CurrentCulture)
    {
        return ExtendedLastIndexOf(src, target, end, target.Length, stringComparison);
    }

    /// <summary>
    /// Return the substring after to but not including the last instance of character 'c'.
    /// If 'c' is not found, the entire string is returned.
    /// </summary>
    public static string SubstringAfterLast(this string? src, char c)
    {
        if (string.IsNullOrEmpty(src)) return "";

        var idx = Math.Min(src!.Length, src.LastIndexOf(c) + 1);
        return idx < 0 ? src : src.Substring(idx);
    }

    /// <summary>
    /// Return the substring after to but not including the last instance of string 's'.
    /// If 's' is not found, the entire string is returned.
    /// </summary>
    public static string SubstringAfterLast(this string? src, string s, StringComparison stringComparison = StringComparison.Ordinal)
    {
        if (src is null) return "";

        var idx = Math.Min(src.Length, src.ExtendedLastIndexOf(s, src.Length - 1, s.Length, stringComparison));
        return idx < 0 ? src : src.Substring(idx);
    }
}