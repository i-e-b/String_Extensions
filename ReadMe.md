String_Extensions
=================

A big pile of string manipulation methods for C#, to fill in some of the gaps of the default string library.

https://www.nuget.org/packages/StringExtensionsNetStdIeb/

## Comparison

### CompareWildcard

Match strings to patterns using `*` and `?` placeholders. For when Regex is too much.

### NaturalComparer

A comparer for use in Linq `Sort(...)` that respects numerical ordering.

You can drop this anywhere an IComparer is used on strings. To sort files by name with numbers in the right place:
```csharp
var list = new DirectoryInfo(@"C:\temp")
    .GetFiles()
    .OrderBy(f=>f.Name, new NaturalComparer())
    .ToList();
```
.OrderBy(f=>f.Name, new NaturalComparer()).ToList()

Then, given these files (as ordered by default .Net sort)

> File 10, File 11, File 8, File 9

Will be sorted correctly, as would be expected:

> File 8, File 9, File 10, File 11

### NumbersOnlyComparer

Similar to `NaturalComparer`, but sorts only on number parts of strings. Strings without numbers are sorted last.

All number values are treated as positive integers, with the `-`, `.`, `,` characters interpreted as separators.

Given 

`File`, `Users 2020-01-01`, `Accounts 2020-02-03`, `Accounts 2020-02-03_v2`

this will output

`Users 2020-01-01`, `Accounts 2020-02-03`, `Accounts 2020-02-03_v2`, `File`

### EqualsIgnoreNumbers

Compare strings for equality, ignoring the actual values of decimal numbers

`NaturalComparer.EqualsIgnoreNumbers("Added ID 5 to DB", "Added ID 201 to DB")` == `true`

`NaturalComparer.EqualsIgnoreNumbers("Added ID 5 to DB", "ID 201 Failed")` == `false`

## Parsing

### ToByteArray

Convert hex strings to byte arrays.

Take a string like `BADF00BA` into a byte array. String length should be a multiple of two.

## Replacement

### ReplaceCaseInvariant

Replace string fragments using a case-insensitive search.

Matching is case invariant, replacement is done preserving case.
For example: `"OnCe UpOn A tImE".ReplaceCaseInvariant("TiMe", "Moon") == "OnCe UpOn A Moon"`

### ReplaceAsciiCompatible

Replace latin-like letters and numbers with their ASCII equivalents.

This will replace accented forms with ones that *look* similar, but it
will often destroy or change meaning. Do **NOT** use this to present output
to users. It is intended to use for stored search targets.
This is not exhaustive, and does not handle characters that are not latin-like (e.g. CJK)

`HÉLLO, Åbjørn!".ReplaceAsciiCompatible() == "HELLO, Abjorn!"`

### CamelCaseToWords

Convert `camelCase` or `PascalCase` fused-word strings into space separated words.

`MyABCsDoCamelCasePhrase".CamelCaseToWords() == "My ABCs Do Camel Case Phrase"`

### Switch case simple

Fast case switching on strings that only makes changes that don't change the number
of characters in the string.

`"123 £%$: AAA aaa ǉ ǆ ß".ToUpperSimple() == "123 £%$: AAA AAA Ǉ Ǆ ß"`

`"123 £%$: AAA aaa Ǉ Ǆ SS".ToLowerSimple() == "123 £%$: aaa aaa ǉ ǆ ss"`

### Switch case of single character

Set casing of characters in the string

`"hello, world".UpperCaseIndex(0) == "Hello, world"`

`"hello, world".UpperCaseIndex(-5) == "hello, World"`

## Searching

### FindCluster

A very simple O(n+m) algorithm for finding clusters of characters in a string.
(i.e. it will find `wxyz` in `abc zwyx abc`, but not in `ab xy ab wz`)

This algorithm will give some false positives where the sum of character values match.

## Substrings

A range of substring before/after first/last instances of a character or string.

### SubstringBefore

Return the substring up to but not including the first instance of string 's'.
If 's' is not found, the entire string is returned.

`"once upon a time".SubstringBefore("upon") == "once "`

### SubstringBeforeLast

Return the substring up to but not including the last instance of string 's'.
If 's' is not found, the entire string is returned.

`"once upon a time up high".SubstringBeforeLast("up") == "once upon a time "`

### SubstringAfter

Return the substring after to but not including the first instance of string 's'.
If 's' is not found, the entire string is returned.

`"it happened once upon a time".SubstringAfter("once") == " upon a time"`

### SubstringAfterLast

Return the substring after to but not including the last instance of string 's'.
If 's' is not found, the entire string is returned.

`"once upon a time".SubstringAfterLast("on") == " a time"`

### IndexAfter / LastIndexAfter

Return the index of the first character after a match.
If match is not found, `-1` is returned.

`"once upon a time".IndexAfter("on") == 2`
`"once upon a time".LastIndexAfter("on") == 9`

## Filtering

### NormaliseWhitespace

Replace all runs of whitespace with a single space character

```csharp
var output = "This\tis\u00a0a  story about \r\n a \rperson\n\t\r\n named Not, who was    tall."
        .NormaliseWhitespace(char.IsLower);
```

results in `This is a story about a person named Not, who was tall.`

### FilterCharacters

Remove a string omitting characters where the function returns `true`

```csharp
var output = "Hello, World! How are you?"
        .FilterCharacters(char.IsLower);
```

results in `H, W! H  ?`


### Remove

Return a string with all instances of all given unwanted strings removed,
matched case sensitive.

```csharp
var output = "This is not a short story about a person named Not, who was not short but tall."
        .Remove(new[]{"not", "short", "but"})
```

results in `This is  a  story about a person named Not, who was    tall.`

### RemoveCaseInvariant

Return a string with all instances of all given unwanted strings removed,
matched case insensitive.

```csharp
var output = "This is not a short story about a person named Not, who was not short but tall."
        .RemoveCaseInvariant(new[]{"not", "short", "but"})
```

results in `This is  a  story about a person named , who was    tall.`


### RemoveNumbers

Remove a string omitting all decimal digit characters.

```csharp
var output = "In 2024 at 12.15pm, we found 7 stone lions in a cave 250 miles from 9th Avenue"
        .RemoveNumbers()
```

results in `In  at .pm, we found  stone lions in a cave  miles from th Avenue`

### RemoveNonAlphaNumeric

Keep only ASCII alphabetic and decimal characters in the string

```csharp
var output = "In 2024, we found 7 stone lions in a cave 250 miles from 9th Avenue."
        .RemoveNonAlphaNumeric()
```

results in `In2024wefound7stonelionsinacave250milesfrom9thAvenue`

