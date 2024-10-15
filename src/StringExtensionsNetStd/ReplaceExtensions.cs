using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using String_Extensions.support;

namespace String_Extensions;

using System;

/// <summary>
/// Extensions for replacing parts of strings
/// </summary>
[SuppressMessage("ReSharper", "CommentTypo")]
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
        if (targetPattern == null) throw new ArgumentNullException(nameof(targetPattern));
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        if (src == null) throw new ArgumentNullException(nameof(src));

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

    /// <summary>
    /// Split a <c>camelCase</c> or <c>PascalCase</c> fused string into
    /// space separated words.
    /// </summary>
    /// <example><code>"myNumber1CamelCasePhrase".SplitCamelCase(); // "my Number 1 Camel Case Phrase"</code></example>
    public static string CamelCaseToWords(this string? src)
    {
        if (string.IsNullOrWhiteSpace(src)) return src ?? "";

        // Derived from https://stackoverflow.com/a/35953318/423033 by Chris Kline

        // Sample initial input:
        // "__ToGetYourGEDInTimeASongAboutThe26ABCsIsOfTheEssenceButAPersonalIDCardForUser_456InRoom26AContainingABC26TimesIsNotAsEasyAs123ForC3POOrR2D2Or2R2D"

        // => " ToGetYourGEDInTimeASongAboutThe26ABCsIsOfTheEssenceButAPersonalIDCardForUser 456InRoom26AContainingABC26TimesIsNotAsEasyAs123ForC3POOrR2D2Or2R2D"
        var step1 = StaticRegexes.Underscores.Replace(src, " ");

        // => " To Get YourGEDIn TimeASong About The26ABCs IsOf The Essence ButAPersonalIDCard For User456In Room26AContainingABC26Times IsNot AsEasy As123ForC3POOrR2D2Or2R2D"
        var step2 = StaticRegexes.LowerToUpper.Replace(step1, StaticRegexes.JoinWithSpace);

        // => " To Get YourGEDIn TimeASong About The26ABCs Is Of The Essence ButAPersonalIDCard For User456In Room26AContainingABC26Times Is Not As Easy As123ForC3POOr R2D2Or2R2D"
        var step3 = StaticRegexes.UpperLowerUpper.Replace(step2, StaticRegexes.JoinWithSpace);

        // => " To Get Your GEDIn Time ASong About The26ABCs Is Of The Essence But APersonal IDCard For User456In Room26AContainingABC26Times Is Not As Easy As123ForC3POOr R2D2Or2R2D"
        var step4 = StaticRegexes.LowerToManyUpper.Replace(step3, StaticRegexes.JoinWithSpace);

        // => " To Get Your GEDIn Time A Song About The26ABCs Is Of The Essence But A Personal ID Card For User456In Room26A ContainingABC26Times Is Not As Easy As123ForC3POOr R2D2Or2R2D"
        var step5 = StaticRegexes.ManyUpperToSentence.Replace(step4, StaticRegexes.JoinWithSpace);

        // => " To Get Your GEDIn Time A Song About The 26ABCs Is Of The Essence But A Personal ID Card For User 456In Room 26A Containing ABC26Times Is Not As Easy As 123For C3POOr R2D2Or 2R2D"
        var step6 = StaticRegexes.ManyLowerToManyUpperNumber.Replace(step5, StaticRegexes.JoinWithSpace);

        // => " To Get Your GED In Time A Song About The 26ABCs Is Of The Essence But A Personal ID Card For User 456In Room 26A Containing ABC26Times Is Not As Easy As 123For C3PO Or R2D2Or 2R2D"
        var step7 = StaticRegexes.ManyUpperToUpperLower.Replace(step6, StaticRegexes.JoinWithSpace);

        // => " To Get Your GED In Time A Song About The 26ABCs Is Of The Essence But A Personal ID Card For User 456In Room 26A Containing ABC 26Times Is Not As Easy As 123For C3PO Or R2D2Or 2R2D"
        var step8 = StaticRegexes.NumberToUpperLower.Replace(step7, StaticRegexes.JoinWithSpace);

        // => " To Get Your GED In Time A Song About The 26ABCs Is Of The Essence But A Personal ID Card For User 456 In Room 26A Containing ABC 26 Times Is Not As Easy As 123 For C3PO Or R2D2 Or 2R2D"
        var step9 = StaticRegexes.AcronymToNumber.Replace(step8, StaticRegexes.JoinWithSpace);

        // => " To Get Your GED In Time A Song About The 26 ABCs Is Of The Essence But A Personal ID Card For User 456 In Room 26A Containing ABC 26 Times Is Not As Easy As 123 For C3PO Or R2D2 Or 2R2D"
        var step10 = StaticRegexes.NumberToAcronym.Replace(step9, StaticRegexes.JoinWithSpace);

        return step10.Trim();
    }

    /// <summary>
    /// Create a new string that is as close to the original as possible, using only low-ascii characters.
    /// This will replace accented forms with ones that *look* similar, but it will often destroy or
    /// change meaning.
    /// Do NOT use this to present output to users. It is intended to use for stored search targets.
    /// This is not exhaustive, and does not handle characters that are not latin-like (e.g. CJK, Arabic)
    /// </summary>
    /// <example><code>"HÉLLO, Åbjørn!".ReplaceAsciiCompatible() == "HELLO, Abjorn!"</code></example>
    public static string ReplaceAsciiCompatible(this string src)
    {
        if (string.IsNullOrWhiteSpace(src)) return src;

        var charEnum = StringInfo.GetTextElementEnumerator(src);

        var outp  = new StringBuilder();
        while (charEnum.MoveNext()) {
            var longChar = char.ConvertToUtf32(charEnum.GetTextElement(), 0);
            outp.Append(CharacterConverter.ConvertToAscii(longChar));
        }

        return outp.ToString();
    }
}