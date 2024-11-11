namespace String_Extensions;

using System.Linq;
using support;

/// <summary>
/// Extensions for searching inside strings
/// </summary>
public static class SearchExtensions
{
    /// <summary>
    /// Find the first offset in the string that might contain the characters
    /// in `needle`, in any order. Returns -1 if not found.
    /// <para>This function can return false positives</para>
    /// </summary>
    /// <param name="haystack">String to search</param>
    /// <param name="needle">Sub-string to find inside <paramref name="haystack"/></param>
    public static int FindCluster(this string? haystack, string? needle)
    {
        if (haystack == null) return -1;
        if (needle == null) return -1;

        if (haystack.Length < needle.Length) return -1;

        long sum     = needle.ToCharArray().Sum(c => c);
        long rolling = haystack.ToCharArray().Take(needle.Length).Sum(c => c);

        var idx  = 0;
        var head = needle.Length;
        while (rolling != sum)
        {
            if (head >= haystack.Length) return -1;
            rolling -= haystack[idx];
            rolling += haystack[head];
            head++;
            idx++;
        }

        return idx;
    }

    /// <summary>
    /// Find the first offset in a string using a rolling hash search.
    /// </summary>
    /// <param name="haystack">String to search</param>
    /// <param name="needle">Sub-string to find inside <paramref name="haystack"/></param>
    public static int FindRollingHash(this string? haystack, string? needle)
    {
        if (haystack == null) return -1;
        if (needle == null) return -1;

        if (haystack.Length < needle.Length) return -1;

        var match   = RollingHash32.HashOfString(needle);
        var rolling = new RollingHash32(needle.Length, haystack);

        var idx  = 0;
        var head = needle.Length;
        while (rolling.Value != match)
        {
            if (head >= haystack.Length) return -1;
            rolling.AddChar(haystack[head]);
            head++;
            idx++;
        }

        return idx;
    }

    /// <summary>
    /// Find the first offset in a string using the Boyer-Moor algorithm
    /// </summary>
    /// <param name="haystack">String to search</param>
    /// <param name="needle">Sub-string to find inside <paramref name="haystack"/></param>
    public static int FindBoyerMoor(this string? haystack, string? needle)
    {
        if (haystack == null) return -1;
        if (needle == null) return -1;
        if (needle == "") return 0;

        var indexStart = 0;

        while (indexStart <= haystack.Length - needle.Length)
        {
            var  j  = indexStart + needle.Length - 1;
            var  i  = needle.Length - 1;
            var cj = haystack[j];

            // Try to find a match
            while (i >= 0)
            {
                if (needle[i] != cj) break;
                if (i == 0) return indexStart;

                i--;
                j--;
                cj = haystack[j];
            }

            // No match, find the next safe offset.
            // This doesn't bother building a table, it just checks every time.
            // Will tend to be faster on short pairs of strings.
            var shiftIndex = 0;

            for (var k = needle.Length - 1; k >= 0; k--)
            {
                if (needle[k] == cj)
                {
                    shiftIndex = shiftIndex == 0 ? k : shiftIndex;
                    break;
                }

                shiftIndex++;
            }

            indexStart += shiftIndex;
        }

        return -1;
    }
}