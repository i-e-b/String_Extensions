using System.Text;
using System.Text.RegularExpressions;

namespace String_Extensions.support;

/// <summary>
/// Regular expressions with fixed values
/// </summary>
internal static class StaticRegexes
{
    /// <summary>
    /// Pattern for splitting word-like fragments from number-like fragments
    /// </summary>
    public static readonly Regex WordAndNumberSplit = new(@"[\W\.]*([\w-[\d]]+|[\d]+)", RegexOptions.None);

    /// <summary>
    /// Find underscores, with a capture
    /// </summary>
    public static readonly Regex Underscores = new(@"(_)+", RegexOptions.ECMAScript);

    /// <summary>
    /// Capture 'lowe<b>r̕Up</b>per' changes
    /// </summary>
    public static readonly Regex LowerToUpper = new(@"(?<left>[a-z])(?<right>[A-Z][a-z])", RegexOptions.ECMAScript);

    /// <summary>
    /// Capture 'UPPE<b>Ra̕U</b>PPER' changes
    /// </summary>
    public static readonly Regex UpperLowerUpper = new(@"(?<left>[A-Z][a-z])(?<right>[A-Z])", RegexOptions.ECMAScript);

    /// <summary>
    /// Capture 'lowe<b>r̕UPPERc</b>ase' changes
    /// </summary>
    public static readonly Regex LowerToManyUpper = new(@"(?<left>[a-z])(?<right>[A-Z]+[a-z])", RegexOptions.ECMAScript);

    /// <summary>
    /// Capture '<b>UPPER̕Tol</b>ower' changes
    /// </summary>
    public static readonly Regex ManyUpperToSentence = new(@"(?<left>[A-Z]+)(?<right>[A-Z][a-z][a-z])", RegexOptions.ECMAScript);

    /// <summary>
    /// Capture '<b>lower̕TO123</b>' changes
    /// </summary>
    public static readonly Regex ManyLowerToManyUpperNumber = new(@"(?<left>[a-z]+)(?<right>[A-Z0-9]+)", RegexOptions.ECMAScript);

    /// <summary>
    /// Capture '<b>UPPER̕Lower</b>' changes, with a special case to avoid splitting 'ABCs'
    /// </summary>
    public static readonly Regex ManyUpperToUpperLower = new(@"(?<left>[A-Z]+)(?<right>[A-Z][a-rt-z][a-z]*)", RegexOptions.ECMAScript);

    /// <summary>
    /// Capture '12<b>3̕Word</b>' changes, with a special case to avoid splitting 'ABCs'
    /// </summary>
    public static readonly Regex NumberToUpperLower = new(@"(?<left>[0-9])(?<right>[A-Z][a-z]+)", RegexOptions.ECMAScript);

    /// <summary>
    /// Capture 'RO<b>OM̕26</b>A' changes
    /// <p/>
    /// Note: this uses <c>{2,}</c> instead of <c>+</c> to add space on phrases like
    /// <c>Room26A</c> and <c>26ABCs</c> but not on phrases like <c>R2D2</c> and <c>C3PO</c>
    /// </summary>
    public static readonly Regex AcronymToNumber = new(@"(?<left>[A-Z]{2,})(?<right>[0-9]{2,})", RegexOptions.ECMAScript);

    /// <summary>
    /// Capture '1<b>23̕AB</b>Cs' changes
    /// <p/>
    /// Note: this uses <c>{2,}</c> instead of <c>+</c> to add space on phrases like
    /// <c>Room26A</c> and <c>26ABCs</c> but not on phrases like <c>R2D2</c> and <c>C3PO</c>
    /// </summary>
    public static readonly Regex NumberToAcronym = new(@"(?<left>[0-9]{2,})(?<right>[A-Z]{2,})", RegexOptions.ECMAScript);

    /// <summary>
    /// Match evaluator to join groups with a space
    /// </summary>
    public static string JoinWithSpace(Match match)
    {
        var sb = new StringBuilder();

        var first = true;
        foreach (var group in match.Groups)
        {
            if (group is Match) continue;

            if (!first) sb.Append(" ");
            sb.Append(group);
            first = false;
        }

        return sb.ToString();
    }
}