namespace String_Extensions
{
    using System.Linq;

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

            long sum = needle.Sum(c => (int)c);
            long rolling = haystack.Take(needle.Length).Sum(c => (int)c);

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
    }
}
