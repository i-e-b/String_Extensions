using System.Collections.Generic;
using System.Linq;

namespace String_Extensions
{
    public static class AggregateExtensions
    {
        /// <summary>
        /// Aggregate a dictionary into a single string given two combiners.
        /// <para>For example `MyParams.Join("=", "&")` could be used to make URL query parameters</para>
        /// </summary>
        /// <param name="source">Dictionary to combine</param>
        /// <param name="keyValueCombiner">String to join keys to values</param>
        /// <param name="entryCombiner">String to join the resulting key-value pairs</param>
        public static string? Join<TKey, TValue>(this Dictionary<TKey, TValue>? source, string keyValueCombiner, string entryCombiner) {
            if (source == null) return null;
            return string.Join(entryCombiner, source.Select(x => x.Key + keyValueCombiner + x.Value));
        }
    }
}