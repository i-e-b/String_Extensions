﻿using System.Text;

namespace String_Extensions;

/// <summary>
/// A C# version of https://github.com/apankrat/notes/tree/master/fast-case-conversion
/// </summary>
public static class CaseSwitchExtensions
{
    /// <summary>
    /// Set the character at the given index to upper case.
    /// If the index is out of range or the character is already upper case,
    /// the original string is returned.
    /// </summary>
    public static string? UpperCaseIndex(this string? input, int index)
    {
        if (input is null) return input;
        if (index >= input.Length) return input;
        if (index < 0) index += input.Length;
        if (index < 0) return input;
            
        if (char.IsUpper(input[index])) return input;
            
        // simple cases done, now actually transform the string
        var sb = new StringBuilder();
        for (var i = 0; i < input.Length; i++)
        {
            var c = input[i];
            if (i == index) c = char.ToUpper(c);
            sb.Append(c);
        }

        return sb.ToString();
    }

    /// <summary>
    /// A fast case conversion based on table-lookups.
    /// This ignores all 'complex' cases, so the output will always be the
    /// same length as the input
    /// </summary>
    public static string? ToUpperSimple(this string? input)
    {
        if (input is null) return null;
        var sb = new StringBuilder();
        foreach (var c in input)
        {
            var x = c + _caseMapUpper2258[ _caseMapUpper2258[ _caseMapUpper2258[c >> 8] + ((c >> 3) & 0x1f) ] + (c & 0x07) ];
            sb.Append((char)x);
        }
        return sb.ToString();
    }

    /// <summary>
    /// A fast case conversion based on table-lookups.
    /// This ignores all 'complex' cases, so the output will always be the
    /// same length as the input
    /// </summary>
    public static char ToUpperSimple(this char c)
    {
        return (char) (c + _caseMapUpper2258[ _caseMapUpper2258[ _caseMapUpper2258[c >> 8] + ((c >> 3) & 0x1f) ] + (c & 0x07) ]);
    }

    /// <summary>
    /// A fast case conversion based on table-lookups.
    /// This ignores all 'complex' cases, so the output will always be the
    /// same length as the input
    /// </summary>
    public static string? ToLowerSimple(this string? input)
    {
        if (input is null) return null;
        var sb = new StringBuilder();
        foreach (var c in input)
        {
            var x = c + _caseMapLower2258[ _caseMapLower2258[ _caseMapLower2258[c >> 8] + ((c >> 3) & 0x1f) ] + (c & 0x07) ];
            sb.Append((char)x);
        }
        return sb.ToString();
    }
        
    /// <summary>
    /// A fast case conversion based on table-lookups.
    /// This ignores all 'complex' cases, so the output will always be the
    /// same length as the input
    /// </summary>
    public static char ToLowerSimple(this char c)
    {
        return (char) (c + _caseMapLower2258[ _caseMapLower2258[ _caseMapLower2258[c >> 8] + ((c >> 3) & 0x1f) ] + (c & 0x07) ]);
    }

    private static readonly int[] _caseMapLower2258 =
    {
        /* index sequence offsets */
        0x0239, 0x01f6, 0x0215, 0x01bc, 0x013f, 0x0159, 0x025d, 0x025d,
        0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d,
        0x0165, 0x025d, 0x025d, 0x0269, 0x025d, 0x025d, 0x025d, 0x025d,
        0x025d, 0x025d, 0x025d, 0x025d, 0x0190, 0x025d, 0x01dc, 0x0288,
        0x025d, 0x017f, 0x025d, 0x025d, 0x021f, 0x025d, 0x025d, 0x025d,
        0x025d, 0x025d, 0x025d, 0x025d, 0x0100, 0x025d, 0x025d, 0x025d,
        0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d,
        0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d,
        0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d,
        0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d,
        0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d,
        0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d,
        0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d,
        0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d,
        0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d,
        0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d,
        0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d,
        0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d,
        0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d,
        0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d,
        0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x01a8, 0x011f,
        0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d,
        0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d,
        0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d,
        0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d,
        0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d,
        0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d,
        0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d,
        0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d,
        0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d,
        0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d,
        0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x025d, 0x0255,
        /* index sequence values */
        0x02ef, 0x02ef, 0x02ef, 0x02ef, 0x02ef, 0x02f1, 0x02ca, 0x02ca,
        0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02fe, 0x02de, 0x0385, 0x037d,
        0x03bc, 0x03bc, 0x03bc, 0x03bc, 0x03bc, 0x03bc, 0x03bc, 0x03bc,
        0x03bc, 0x03bc, 0x03bc, 0x03bc, 0x031c, 0x039f, 0x0388, 0x02ca,
        0x02ca, 0x02ca, 0x02ca, 0x0303, 0x03bc, 0x0303, 0x03bc, 0x03bc,
        0x03bc, 0x03bc, 0x03bc, 0x03bc, 0x03bc, 0x02ca, 0x02f8, 0x03bc,
        0x03d5, 0x0307, 0x03bc, 0x03bc, 0x03ac, 0x03fc, 0x03bc, 0x03a5,
        0x037b, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x0377, 0x02ca, 0x03ec,
        0x03ec, 0x0338, 0x0338, 0x0338, 0x0338, 0x02ca, 0x02ca, 0x02ca,
        0x02ca, 0x02ca, 0x02ca, 0x03bc, 0x03bc, 0x03bc, 0x03bc, 0x038a,
        0x0303, 0x03bc, 0x03bc, 0x03bc, 0x03bc, 0x03bc, 0x03bc, 0x0315,
        0x0319, 0x03bc, 0x03bc, 0x03bc, 0x03bc, 0x03bc, 0x03bc, 0x02ed,
        0x02ef, 0x02ef, 0x02ef, 0x02f1, 0x02ca, 0x02ca, 0x02ca, 0x02ca,
        0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca,
        0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca,
        0x02ca, 0x03c6, 0x03c6, 0x03c6, 0x03c6, 0x03c8, 0x031f, 0x02ca,
        0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x0358, 0x02ca, 0x02ca,
        0x02ca, 0x02ca, 0x02ca, 0x0418, 0x0418, 0x02ca, 0x02ca, 0x0379,
        0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca,
        0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca,
        0x02ca, 0x02ca, 0x02ad, 0x02ad, 0x02ad, 0x02ad, 0x02ad, 0x02a8,
        0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca,
        0x03bc, 0x03bc, 0x03bc, 0x03bc, 0x03bc, 0x0305, 0x02ca, 0x02ca,
        0x03bc, 0x03bc, 0x03bc, 0x031c, 0x02ca, 0x02ca, 0x02ca, 0x02ca,
        0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca,
        0x02ca, 0x02ca, 0x0307, 0x02cc, 0x0341, 0x03f4, 0x0336, 0x0338,
        0x0334, 0x033d, 0x02ca, 0x02ca, 0x02ca, 0x035b, 0x02ca, 0x03bc,
        0x03bc, 0x03bc, 0x0375, 0x03b3, 0x03bc, 0x03bc, 0x03bc, 0x03bc,
        0x03bc, 0x03bc, 0x03bc, 0x03bc, 0x03bc, 0x03bc, 0x03bc, 0x03bc,
        0x03bc, 0x03bc, 0x03bc, 0x03bc, 0x03bc, 0x03bc, 0x0305, 0x02ca,
        0x03bc, 0x03bc, 0x03bc, 0x03bc, 0x03bc, 0x03bc, 0x03bc, 0x03bc,
        0x03bc, 0x03bc, 0x03bc, 0x03bc, 0x0303, 0x0317, 0x0317, 0x0303,
        0x03bc, 0x03bc, 0x03bc, 0x03bc, 0x03bc, 0x02e6, 0x0348, 0x03dc,
        0x0410, 0x030d, 0x03be, 0x0408, 0x032c, 0x0309, 0x02b9, 0x02da,
        0x0317, 0x03a1, 0x03bc, 0x03bc, 0x02bf, 0x03bc, 0x03bc, 0x03bc,
        0x03bc, 0x03ba, 0x03bc, 0x031c, 0x0325, 0x0401, 0x03bc, 0x02ca,
        0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca,
        0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca,
        0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x038c, 0x0392, 0x0392,
        0x0392, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca,
        0x02ca, 0x0336, 0x0338, 0x0338, 0x02c7, 0x02ca, 0x02ca, 0x02ca,
        0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca,
        0x02ca, 0x0338, 0x0338, 0x033a, 0x033a, 0x02ca, 0x02ca, 0x02ca,
        0x02ca, 0x0336, 0x0338, 0x0338, 0x02c7, 0x02ca, 0x02ca, 0x02ca,
        0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca,
        0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca,
        0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca,
        0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x02ca, 0x03e4, 0x03e4, 0x03e4,
        0x03e4, 0x03e4, 0x03e4, 0x03e4, 0x03e4, 0x03e4, 0x03e4, 0x0362,
        0x02ca, 0x0350, 0x02ca, 0x0352, 0x02ca, 0x0350, 0x02ca, 0x0350,
        0x02ca, 0x0352, 0x02ca, 0x0369, 0x02ca, 0x0350, 0x02ca, 0x02ca,
        0x02ca, 0x0350, 0x02ca, 0x0350, 0x02ca, 0x0350, 0x02ca, 0x0370,
        0x02ca, 0x039a, 0x02ca, 0x02b5, 0x02ca, 0x02d4, 0x02ca, 0x03d0,
        /* case map */
        0xf440, 0xf440, 0xf440, 0x0000, 0x0000, 0xf440, 0xf440, 0xf440,
        0xf440, 0xf440, 0xf440, 0xf440, 0xf440, 0xfff8, 0xfff8, 0xff9c,
        0xff9c, 0x0000, 0x0000, 0x0000, 0x0000, 0x0002, 0x0000, 0x0000,
        0x0002, 0x0000, 0x0000, 0x0001, 0x0000, 0xff9f, 0xffc8, 0x0020,
        0x0020, 0x0020, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000,
        0x0000, 0x0000, 0x0000, 0x0074, 0xfff8, 0xfff8, 0xff90, 0xff90,
        0xfff9, 0x0000, 0x0000, 0x0000, 0x0002, 0x0000, 0x0000, 0x0001,
        0x0000, 0x0001, 0x0000, 0xd5e4, 0xd603, 0xd5e1, 0xff87, 0x0001,
        0x0000, 0x0001, 0x0000, 0x0001, 0x0000, 0x0000, 0x0030, 0x0030,
        0x0030, 0x0030, 0x0030, 0x0030, 0x0030, 0x0030, 0x0030, 0x0030,
        0x0000, 0x0001, 0x0000, 0x0001, 0x0000, 0x75fc, 0x0001, 0x0000,
        0xd609, 0xf11a, 0xd619, 0x0000, 0x0000, 0x0001, 0x0000, 0x0001,
        0x0000, 0x0001, 0x0000, 0x0000, 0x0000, 0x0001, 0x0000, 0x0000,
        0x0000, 0x00d3, 0x00d5, 0x0000, 0x00d6, 0x000f, 0x0001, 0x0000,
        0x0001, 0x0000, 0x0001, 0x0000, 0x0001, 0x0000, 0x0001, 0x0000,
        0x0000, 0x0000, 0x0000, 0x0000, 0x1c60, 0x0000, 0x0000, 0x2a2b,
        0x0001, 0x0000, 0xff5d, 0x2a28, 0x0000, 0x00d9, 0x00d9, 0x0001,
        0x0000, 0x0001, 0x0000, 0x00db, 0x0020, 0x0020, 0x0000, 0x0020,
        0x0020, 0x0020, 0x0020, 0x0020, 0x0020, 0x0020, 0x0020, 0x0020,
        0x0020, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0026,
        0x0000, 0x00d2, 0x0001, 0x0000, 0x0001, 0x0000, 0x00ce, 0x0001,
        0xfff8, 0xfff8, 0xfff8, 0xfff8, 0xfff8, 0xfff8, 0xfff8, 0xfff8,
        0x0000, 0x0000, 0x001c, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000,
        0x0000, 0x0000, 0x0008, 0x0008, 0x0008, 0x0008, 0x0008, 0x0008,
        0x0000, 0x0000, 0xfff8, 0x0000, 0xfff8, 0x0000, 0xfff8, 0x0000,
        0xfff8, 0xfff8, 0xffb6, 0xffb6, 0xfff7, 0x0000, 0x0000, 0x0000,
        0x0000, 0x0000, 0x0000, 0x0000, 0x0001, 0x0000, 0x0000, 0x0000,
        0x0000, 0x0000, 0x0000, 0xd5c1, 0xd5c1, 0xd5e2, 0x0000, 0x0001,
        0x0000, 0x0000, 0x0001, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000,
        0x0000, 0x0000, 0x001a, 0x001a, 0x001a, 0x001a, 0x001a, 0x001a,
        0x001a, 0x001a, 0xffaa, 0xffaa, 0xffaa, 0xffaa, 0xfff7, 0x0000,
        0x0000, 0x0000, 0x0001, 0x0000, 0x0001, 0x0000, 0x0000, 0x0001,
        0x0000, 0xffd0, 0x5abd, 0x75c8, 0x0001, 0x0000, 0x5abc, 0x5ab1,
        0x5ab5, 0x5abf, 0x5abc, 0x0000, 0xfff9, 0x0001, 0x0000, 0x0000,
        0xff7e, 0xff7e, 0xff7e, 0x0000, 0x0001, 0x0000, 0x0001, 0x0000,
        0x0001, 0x0000, 0x0001, 0x0000, 0x00da, 0x0001, 0x1c60, 0x1c60,
        0x1c60, 0x1c60, 0x1c60, 0x1c60, 0x1c60, 0x1c60, 0x0000, 0x1c60,
        0xff80, 0xff80, 0xff82, 0xff82, 0xfff7, 0x0000, 0x0000, 0x0000,
        0x0001, 0x0000, 0x5ad8, 0x0000, 0x0000, 0x00cd, 0x00cd, 0x0001,
        0x0000, 0x0000, 0x004f, 0x00ca, 0x97d0, 0x97d0, 0x97d0, 0x97d0,
        0x97d0, 0x97d0, 0x97d0, 0x97d0, 0x0050, 0x0050, 0x0050, 0x0050,
        0x0050, 0x0050, 0x0050, 0x0050, 0x0025, 0x0025, 0x0025, 0x0000,
        0x0040, 0x0000, 0x003f, 0x003f, 0x5aee, 0x5ad6, 0x5aeb, 0x03a0,
        0x0001, 0x0000, 0x0001, 0x0000, 0xff3d, 0x0045, 0x0047, 0x0001,
        0x0000, 0x00da, 0x0000, 0x0000, 0x0001, 0x0000, 0x00da, 0x0001,
        0x00cb, 0x0001, 0x0000, 0x00cd, 0x00cf, 0x0000, 0x00d3, 0x00d1,
        0x0010, 0x0010, 0x0010, 0x0010, 0x0010, 0x0010, 0x0010, 0x0010
    };

    private static readonly int[] _caseMapUpper2258 /*[1129]*/ =
    {
        /* index sequence offsets */
        0x0290, 0x025d, 0x027c, 0x01da, 0x0243, 0x0214, 0x02b6, 0x02b6,
        0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6,
        0x012b, 0x02b6, 0x02b6, 0x02b8, 0x02b6, 0x02b6, 0x02b6, 0x02b6,
        0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x01c8, 0x01fa, 0x014b,
        0x02b6, 0x01b7, 0x02b6, 0x02b6, 0x0225, 0x02b6, 0x02b6, 0x02b6,
        0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x016a, 0x02b0, 0x02b6, 0x02b6,
        0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6,
        0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6,
        0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6,
        0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6,
        0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6,
        0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6,
        0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6,
        0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6,
        0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6,
        0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6,
        0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6,
        0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6,
        0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6,
        0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6,
        0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x0189, 0x0100,
        0x02b6, 0x02b6, 0x02b6, 0x019f, 0x02b6, 0x02b6, 0x02b6, 0x02b6,
        0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6,
        0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6,
        0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6,
        0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6,
        0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6,
        0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6,
        0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6,
        0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6,
        0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6,
        0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x02b6, 0x011f,
        /* index sequence values */
        0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03d4, 0x03d6, 0x03d4, 0x03d6,
        0x03d6, 0x03d6, 0x03d6, 0x03d6, 0x03d6, 0x03d6, 0x03ba, 0x03f0,
        0x03d6, 0x03a5, 0x03eb, 0x03d6, 0x03d6, 0x03b8, 0x03d2, 0x03d6,
        0x032c, 0x03f9, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x0451, 0x03ba,
        0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x036a,
        0x036c, 0x036c, 0x03cf, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba,
        0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba,
        0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba,
        0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x02f7, 0x02f7, 0x02f7,
        0x02f7, 0x02f7, 0x02fc, 0x0449, 0x03ba, 0x044b, 0x03ba, 0x0449,
        0x03ba, 0x0449, 0x03ba, 0x044b, 0x03ba, 0x0442, 0x03ba, 0x0449,
        0x03ba, 0x0404, 0x0316, 0x0449, 0x03ba, 0x0449, 0x03ba, 0x0449,
        0x03ba, 0x0355, 0x03ba, 0x0391, 0x03ba, 0x044f, 0x03ba, 0x02ef,
        0x03ba, 0x0391, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba,
        0x034b, 0x034b, 0x034b, 0x034b, 0x034b, 0x034d, 0x03ac, 0x03f7,
        0x033b, 0x03ba, 0x03d6, 0x03d6, 0x03d6, 0x03d6, 0x03d6, 0x03d6,
        0x03d6, 0x03d6, 0x03d6, 0x03d6, 0x03d6, 0x03d6, 0x03b6, 0x0453,
        0x032c, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba,
        0x03ba, 0x03d6, 0x03d6, 0x03d6, 0x03d6, 0x03d6, 0x0456, 0x03ba,
        0x03ba, 0x03d6, 0x03d6, 0x03d6, 0x03b6, 0x03ba, 0x03ba, 0x03ba,
        0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba,
        0x03ba, 0x03a1, 0x03ba, 0x03ba, 0x03ba, 0x0304, 0x0304, 0x0304,
        0x0304, 0x0304, 0x0304, 0x0304, 0x0304, 0x0304, 0x0304, 0x03ba,
        0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba,
        0x03fc, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x042c, 0x042c, 0x03a5,
        0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba,
        0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x0360,
        0x03ba, 0x0342, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba,
        0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba,
        0x0458, 0x02dd, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x02e7,
        0x036a, 0x036c, 0x0368, 0x0414, 0x037e, 0x03d6, 0x03d6, 0x03d6,
        0x02e3, 0x033e, 0x03d6, 0x03d6, 0x03d6, 0x03d6, 0x03d6, 0x03d6,
        0x03d6, 0x03d6, 0x03d6, 0x03d6, 0x03d6, 0x03d6, 0x03d6, 0x03d6,
        0x03d6, 0x03d6, 0x03d6, 0x03d6, 0x0456, 0x03ba, 0x03d6, 0x03d6,
        0x03d6, 0x03d6, 0x03d6, 0x03d6, 0x03d6, 0x03d6, 0x03d6, 0x03d6,
        0x03d6, 0x03d6, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba,
        0x0349, 0x034b, 0x034b, 0x034b, 0x034d, 0x03ba, 0x03ba, 0x03ba,
        0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba,
        0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba,
        0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x0376,
        0x0376, 0x0376, 0x037c, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba,
        0x03ba, 0x036c, 0x036c, 0x036c, 0x036c, 0x040c, 0x040c, 0x03d6,
        0x03d6, 0x03d6, 0x03d6, 0x03b8, 0x03d4, 0x03d6, 0x03d6, 0x03d6,
        0x03d6, 0x03d6, 0x03d6, 0x045d, 0x0461, 0x03d6, 0x03d6, 0x03d6,
        0x03d6, 0x03d6, 0x03d6, 0x03d4, 0x045d, 0x045f, 0x03f4, 0x03d6,
        0x03d6, 0x03d6, 0x03d6, 0x03d6, 0x045d, 0x03b4, 0x03a9, 0x039b,
        0x03df, 0x0456, 0x0339, 0x045b, 0x03c3, 0x03bc, 0x03e6, 0x045f,
        0x03d9, 0x03d6, 0x03d6, 0x03bf, 0x03d6, 0x03d6, 0x03d6, 0x03d6,
        0x03d4, 0x03d6, 0x03b6, 0x0330, 0x0337, 0x03d6, 0x0434, 0x038c,
        0x043b, 0x0424, 0x031d, 0x0326, 0x041c, 0x02d8, 0x0323, 0x0359,
        0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba,
        0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x036a, 0x036c, 0x036c, 0x03cf,
        0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba,
        0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x036c, 0x036c, 0x03cb, 0x036e,
        0x030c, 0x030c, 0x030c, 0x030c, 0x030e, 0x0395, 0x03ba, 0x03ba,
        0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba,
        0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba,
        0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba,
        0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x03ba, 0x0385,
        /* case map */
        0xff26, 0xffbb, 0xff27, 0xff27, 0xffb9, 0x0000, 0x0000, 0x0000,
        0x0082, 0x0082, 0x0082, 0x0000, 0x0000, 0x0007, 0xff8c, 0x0000,
        0x0000, 0x0000, 0x0000, 0xffda, 0xffdb, 0xffdb, 0xffdb, 0x0008,
        0x0008, 0x0000, 0x0000, 0x0000, 0x0007, 0x0000, 0x0000, 0x0bc0,
        0x0bc0, 0x0bc0, 0x0bc0, 0x0bc0, 0x0bc0, 0x0bc0, 0x0bc0, 0x0000,
        0x0000, 0x0bc0, 0x0bc0, 0x0bc0, 0x6830, 0x6830, 0x6830, 0x6830,
        0x6830, 0x6830, 0x6830, 0x6830, 0xe3a0, 0xe3a0, 0xe3a0, 0xe3a0,
        0xe3a0, 0xe3a0, 0xe3a0, 0xe3a0, 0x0000, 0xe3a0, 0x0080, 0x0080,
        0x0070, 0x0070, 0x007e, 0x007e, 0x0000, 0x0000, 0x29fd, 0xff2b,
        0x0000, 0x0000, 0xff2a, 0x0000, 0x0000, 0xff25, 0x0000, 0x0000,
        0x0000, 0x0000, 0x0000, 0x29e7, 0x0000, 0x0000, 0x0000, 0xffff,
        0x0000, 0x0000, 0x0000, 0x0000, 0xffff, 0x0000, 0x0000, 0x2a3f,
        0x0000, 0xffff, 0x0000, 0x0000, 0x0000, 0x0000, 0xffff, 0x0000,
        0x0000, 0xffff, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000,
        0x8a38, 0x0000, 0xffd0, 0xffd0, 0xffd0, 0xffd0, 0xffd0, 0xffd0,
        0xffd0, 0xffd0, 0xffd0, 0xffd0, 0x0000, 0x0008, 0x0008, 0x0000,
        0x0009, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0xa515, 0xa512,
        0x0000, 0x8a04, 0x0000, 0x0000, 0x0000, 0x0ee6, 0x0000, 0x0000,
        0xffe0, 0xffe0, 0x0000, 0xffe0, 0xffe0, 0xffe0, 0xffe0, 0xffe0,
        0xffe0, 0xffe0, 0xffe0, 0xffe0, 0xffe0, 0x0079, 0xffe6, 0xffe6,
        0xffe6, 0xffe6, 0xffe6, 0xffe6, 0xffe6, 0xffe6, 0x0000, 0x0000,
        0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0xfff8, 0xfff8, 0xfff8,
        0xfff8, 0xfff8, 0xfff8, 0x0000, 0x0000, 0xff36, 0x0000, 0xff35,
        0xa54f, 0x0000, 0x0000, 0x0000, 0x0009, 0x0000, 0x0000, 0x0000,
        0x0000, 0x0000, 0xe3a0, 0x0000, 0x0000, 0xffff, 0x0000, 0x0000,
        0x0061, 0x0000, 0x0000, 0x0000, 0xfc60, 0x0000, 0x0000, 0x0000,
        0x0000, 0xffff, 0x0000, 0x0000, 0x0000, 0xffff, 0x0000, 0x0000,
        0x0000, 0xd5d5, 0xd5d8, 0x0000, 0x00c3, 0x0000, 0x0000, 0xffff,
        0x0000, 0xffff, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000,
        0x0000, 0x0000, 0xfffe, 0x0000, 0xffff, 0x0000, 0x0000, 0x0000,
        0xffff, 0x0000, 0x0038, 0xffe0, 0xffe0, 0xffe0, 0xffe0, 0xffe0,
        0xffe0, 0xffe0, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0xffff,
        0x0000, 0xffff, 0x0000, 0xffff, 0x0000, 0xffff, 0xffb1, 0x0000,
        0xffff, 0x00a3, 0x0000, 0x0000, 0x0000, 0x0082, 0x0000, 0xfffe,
        0x0000, 0x0000, 0xfffe, 0x0000, 0xffff, 0x0000, 0xffff, 0x0030,
        0x0000, 0x0000, 0xffff, 0x0000, 0xffff, 0x0000, 0x0000, 0xffff,
        0x0000, 0xffff, 0x0000, 0xffff, 0x0000, 0x0000, 0x0000, 0x0000,
        0x0000, 0x0000, 0xffe4, 0x0000, 0x004a, 0x004a, 0x0056, 0x0056,
        0x0056, 0x0056, 0x0064, 0x0064, 0xffb0, 0xffb0, 0xffb0, 0xffb0,
        0xffb0, 0xffb0, 0xffb0, 0xffb0, 0xffe0, 0xffe0, 0xffe0, 0xffe0,
        0xffc0, 0xffc1, 0xffc1, 0x0000, 0xff26, 0x0000, 0xa543, 0xff26,
        0x0000, 0x0000, 0x0000, 0xa52a, 0xff2f, 0xff2d, 0xa544, 0x29f7,
        0xa541, 0x0000, 0x0000, 0xff2d, 0xfff0, 0xfff0, 0xfff0, 0xfff0,
        0xfff0, 0xfff0, 0xfff0, 0xfff0, 0x2a1f, 0x2a1c, 0x2a1e, 0xff2e,
        0xff32, 0x0000, 0xff33, 0xff33, 0xa54b, 0x0000, 0xff31, 0x0000,
        0xa528, 0xa544, 0x0000, 0x0008, 0x0000, 0x0008, 0x0000, 0x0008,
        0x0000, 0x0008, 0x0008, 0x0008, 0x0008, 0x0008, 0x0008, 0x0008,
        0x0008, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0xffff,
        0x0000, 0xffff, 0x0000, 0xffff, 0x0000, 0x0000, 0x0000, 0xffff,
        0x0000, 0xffff, 0x0000, 0xffff, 0x0000, 0xffff, 0x0000, 0xffff,
        0xfff1
    };
}