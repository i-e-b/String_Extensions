String_Extensions
=================
[![Build status](https://ci.appveyor.com/api/projects/status/f5bm61wvueo79beq)](https://ci.appveyor.com/project/i-e-b/string-extensions)

A big pile of string manipulation methods for C#

Todo
----
Add unit tests for all the old stuff. Document here.

Highlights
----------

###FindCluster

A very simple O(n+m) algorithm for finding clusters of characters in a string.
(i.e. it will find `wxyz` in `abc zwyx abc`, but not in `ab xy ab wz`)

This algorithm will give some false positives where the sum of character values match.
