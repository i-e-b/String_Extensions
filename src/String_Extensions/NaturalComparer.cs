namespace String_Extensions
{
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
        private static readonly Regex sRegex;
        static NaturalComparer()
        {
#if NETSTANDARD
            sRegex = new Regex(@"[\W\.]*([\w-[\d]]+|[\d]+)", RegexOptions.None);
#else
            sRegex = new Regex(@"[\W\.]*([\w-[\d]]+|[\d]+)", RegexOptions.Compiled);
#endif
        }

        int IComparer.Compare(object left, object right)
        {
            if (!(left is string)) throw new ArgumentException("Parameter type is not string", "left");
            if (!(right is string)) throw new ArgumentException("Parameter type is not string", "right");
            return Compare((string) left, (string) right);
        }

        /// <summary>
        /// Compare two strings for relative sort order
        /// </summary>
        public int Compare(string left, string right)
        {
            if (left == right) return 0;
            if (left == null) return -1;
            if (right == null) return 1;

            var leftmatches = sRegex.Matches(left);
            var rightmatches = sRegex.Matches(right);

            var enrm = rightmatches.GetEnumerator();
            foreach (Match lm in leftmatches)
            {
                if (!enrm.MoveNext())
                {
                    // the right-hand string ran out first, so is considered "less-than" the left
                    return 1;
                }
                var rm = enrm.Current as Match;
                if (rm == null) continue;

                var tokenresult = CompareTokens(CapturedStringFromMatch(lm), CapturedStringFromMatch(rm));
                if (tokenresult != 0)
                {
                    return tokenresult;
                }
            }

            // the lefthand matches are exhausted;
            // if there is more, then left was shorter, ie, lessthan
            // if there's no more left in the righthand, then they were all equal
            return enrm.MoveNext() ? -1 : 0;
        }

        private static string CapturedStringFromMatch(Match match)
        {
            if (match.Captures.Count != 1) throw new Exception("Match captures failed");
            return match.Captures[0].Value;
        }

        private static int CompareTokens(string left, string right)
        {
            double leftval, rightval;

            var leftisnum = double.TryParse(left, out leftval);
            var rightisnum = double.TryParse(right, out rightval);

            // numbers always sort in front of text
            if (leftisnum)
            {
                if (!rightisnum) return -1;

                // they're both numeric
                if (leftval < rightval) return -1;
                if (rightval < leftval) return 1;

                // if values are same, this might be due to leading 0s.
                // Assuming this, the longest string would indicate more leading 0s
                // which should be considered to have lower value
                return Math.Sign(right.Length - left.Length);
            }

            return String.Compare(left, right, StringComparison.Ordinal);
        }

    }
}
