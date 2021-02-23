namespace String_Extensions
{
    using System;

    /// <summary>
    /// Extensions to compare strings
    /// </summary>
    public static class ComparisonExtensions
    {
        /// <summary>
        /// Compare this string to a path-style wildcard mask.
        /// Mask may contain literal characters, '*' or '?'
        /// There is no way to escape wildcard characters.
        /// </summary>
        /// <param name="mask">Pattern to match</param>
        /// <param name="ignoreCase">If true, case differences between this string and the mask will be ignored</param>
        /// <param name="toMatch">String to test</param>
        /// <returns>True if this string fits the mask string. False otherwise.</returns>
        public static bool CompareWildcard(this string toMatch, string mask, bool ignoreCase)
        {
            int i = 0, k = 0;

            while (k != toMatch.Length)
            {
                switch (mask[i])
                {
                    case '*':
                        if ((i + 1) == mask.Length) return true;

                        while (k != toMatch.Length)
                        {
                            if (CompareWildcard(toMatch.Substring(k + 1), mask.Substring(i + 1), ignoreCase)) return true;
                            k += 1;
                        }
                        return false;

                    case '?':
                        break;

                    default:
                        if (ignoreCase == false && toMatch[k] != mask[i]) return false;
                        if (ignoreCase && Char.ToLower(toMatch[k]) != Char.ToLower(mask[i])) return false;
                        break;
                }
                i += 1;
                k += 1;
            }

            if ((k == toMatch.Length) && (i == mask.Length || mask[i] == ';' || mask[i] == '*')) return true;

            return false;
        }

        /// <summary>
        /// Compute the edit distance (number of single character substitutions, inserts or deletes) between this and another string.
        /// Memory usage and processor time increase greatly with long strings.
        /// </summary>
        /// <returns>Number of edits (single character substitutions, inserts or deletes) between the two strings</returns>
        public static int EditDistance(this string s, string other)
        {
            // http://en.wikipedia.org/wiki/Levenshtein_distance
            int n = s.Length;
            int m = other.Length;
            var d = new int[n + 1, m + 1];

            if (n == 0) return m;
            if (m == 0) return n;

            for (int i = 0; i <= n; d[i, 0] = i++) { }
            for (int j = 0; j <= m; d[0, j] = j++) { }

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (other[j - 1] == s[i - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(
                      Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                      d[i - 1, j - 1] + cost);
                }
            }
            return d[n, m];
        }
    }
}
