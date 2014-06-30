String_Extensions
=================
[![Build status](https://ci.appveyor.com/api/projects/status/f5bm61wvueo79beq)](https://ci.appveyor.com/project/i-e-b/string-extensions)

A big pile of string manipulation methods for C#, to fill in some of the gaps of the default string library.

##Comparison
###CompareWildcard

Match strings to patterns using `*` and `?` placeholders. For when Regex is too much.

###NaturalComparer

A comparer for use in Linq `Sort(...)` that respects numerical ordering.

##Parsing
###ToByteArray

Convert hex strings to byte arrays.

##Replacement
###ReplaceCaseInvariant

Replace string fragments using a case-insensitive search.

##Searching
###FindCluster

A very simple O(n+m) algorithm for finding clusters of characters in a string.
(i.e. it will find `wxyz` in `abc zwyx abc`, but not in `ab xy ab wz`)

This algorithm will give some false positives where the sum of character values match.

##Substrings

A range of substring before/after first/last instances of a character or string.