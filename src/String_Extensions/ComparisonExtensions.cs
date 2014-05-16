namespace String_Extensions
{
    using System;

    public static class ComparisonExtensions
    {
        /// <summary>
        /// Compare this string to a path-style wildcard mask.
        /// Mask may contain literal characters, '*' or '?'
        /// There is no way to escape wildcard characters.
        /// </summary>
        /// <param name="Mask">Pattern to match</param>
        /// <param name="IgnoreCase">If true, case differences between this string and the mask will be ignored</param>
        /// <param name="ToMatch">String to test</param>
        /// <returns>True if this string fits the mask string. False otherwise.</returns>
        public static bool CompareWildcard(this string ToMatch, string Mask, bool IgnoreCase)
        {
            int i = 0, k = 0;

            while (k != ToMatch.Length)
            {
                switch (Mask[i])
                {
                    case '*':
                        if ((i + 1) == Mask.Length) return true;

                        while (k != ToMatch.Length)
                        {
                            if (CompareWildcard(ToMatch.Substring(k + 1), Mask.Substring(i + 1), IgnoreCase)) return true;
                            k += 1;
                        }
                        return false;

                    case '?':
                        break;

                    default:
                        if (IgnoreCase == false && ToMatch[k] != Mask[i]) return false;
                        if (IgnoreCase && Char.ToLower(ToMatch[k]) != Char.ToLower(Mask[i])) return false;
                        break;
                }
                i += 1;
                k += 1;
            }

            if ((k == ToMatch.Length) && (i == Mask.Length || Mask[i] == ';' || Mask[i] == '*')) return true;

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
