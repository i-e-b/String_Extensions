namespace String_Extensions
{
    using System;

    public static class ReplaceExtensions
    {
        /// <summary>
        /// Replace all occurrences of 'targetPattern' in the source string with 'replacement'.
        /// Matching is case invariant, replacement is done preserving case.
        /// <para></para>
        /// For example: "OnCe UpOn A tImE".ReplaceCaseInvariant("TiMe", "Moon") == "OnCe UpOn A Moon"
        /// </summary>
        /// <example><code>"OnCe UpOn A tImE".ReplaceCaseInvariant("TiMe", "Moon"); // "OnCe UpOn A Moon"</code></example>
        public static string ReplaceCaseInvariant(this string src, string targetPattern, string replacement)
        {
            if (targetPattern == null) throw new ArgumentNullException("targetPattern");
            if (replacement == null) throw new ArgumentNullException("replacement");
            if (src == null) throw new ArgumentNullException("src");

            if (targetPattern == "") return src;

            var idx = 0;
            var tmp = src;
            while (0 <= (idx = tmp.IndexOf(targetPattern, idx, StringComparison.OrdinalIgnoreCase)))
            {
                tmp = tmp.Remove(idx, targetPattern.Length).Insert(idx, replacement);
                idx += replacement.Length;
            }
            return tmp;
        }
    }
}
