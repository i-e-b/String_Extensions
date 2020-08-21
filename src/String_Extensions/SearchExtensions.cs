namespace String_Extensions
{
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
        public static int FindCluster(this string haystack, string needle)
        {
            if (haystack == null) return -1;
            if (needle == null) return -1;

            if (haystack.Length < needle.Length) return -1;

            long sum = needle.ToCharArray().Sum(c => c);
            long rolling = haystack.ToCharArray().Take(needle.Length).Sum(c => c);

            var idx = 0;
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
        /// Find the first offset in a string using a rolling hash. This is just an experiment -- use string.IndexOf().
        /// </summary>
        public static int Find(this string haystack, string needle)
        {
            if (haystack == null) return -1;
            if (needle == null) return -1;

            if (haystack.Length < needle.Length) return -1;

            var match = RollingHash32.HashOfString(needle);
            var rolling = new RollingHash32(needle.Length, haystack);

            var idx = 0;
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
    }
}
