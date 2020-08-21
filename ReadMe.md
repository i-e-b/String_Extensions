String_Extensions
=================

A big pile of string manipulation methods for C#, to fill in some of the gaps of the default string library.

## Comparison

### CompareWildcard

Match strings to patterns using `*` and `?` placeholders. For when Regex is too much.

### NaturalComparer

A comparer for use in Linq `Sort(...)` that respects numerical ordering.

You can drop this anywhere an IComparer is used on strings. To sort files by name with numbers in the right place:
`new DirectoryInfo(@"C:\temp").GetFiles().Select(f=>f.Name).ToList().Sort(new NaturalComparer());`
Then, given these files (as ordered by default .Net sort)

> File 10, File 11, File 8, File 9

Will be sorted correctly, as would be expected:

> File 8, File 9, File 10, File 11

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

## Searching

### FindCluster

A very simple O(n+m) algorithm for finding clusters of characters in a string.
(i.e. it will find `wxyz` in `abc zwyx abc`, but not in `ab xy ab wz`)

This algorithm will give some false positives where the sum of character values match.

## Substrings

A range of substring before/after first/last instances of a character or string.

SubstringBefore, SubstringBeforeLast, SubstringAfter, ExtendedIndexOf, SubstringAfterLast