namespace String_Extensions;

using System;
using System.Collections.Generic;

/// <summary>
/// Extensions for parsing common data types
/// </summary>
public static class ParsingExtensions
{
    // ReSharper disable once CommentTypo
    /// <summary>
    /// Converts a Hex-string into a byte array.
    /// <para>Take a string like "BADF00BA" into a byte array. String length should be a multiple of two.</para>
    /// Remember to check for network order issues!
    /// </summary>
    public static byte[] HexToByteArray(this string hexString)
    {
        var outp = new List<byte>(hexString.Length / 2); // initial guess at length.

        int i = 0;
        while (i < hexString.Length)
        {
            while (!char.IsLetterOrDigit(hexString[i]))
            {
                i++;
                if (i >= hexString.Length) throw new Exception("hex string contains an uneven number of characters");
            }
            char a = hexString[i];
            i++;

            while (!char.IsLetterOrDigit(hexString[i]))
            {
                i++;
                if (i >= hexString.Length) throw new Exception("hex string contains an uneven number of characters");
            }
            char b = hexString[i];
            i++;

            outp.Add(Convert.ToByte(string.Concat(a, b), 16));
        }
        return outp.ToArray();
    }

    /// <summary>
    /// Output a human readable difference between two dates
    /// </summary>
    /// <param name="target">The earlier time</param>
    /// <param name="actual">[Optional] The later reference time. If <c>null</c>, DateTime.UtcNow is used</param>
    public static string TimeLag(this DateTime target, DateTime? actual = null)
    {
        return new DateTime?(target).TimeLag(actual);
    }

    /// <summary>
    /// Output a human readable difference between two dates
    /// </summary>
    /// <param name="target">The earlier time</param>
    /// <param name="actual">[Optional] The later reference time. If <c>null</c>, DateTime.UtcNow is used</param>
    public static string TimeLag(this DateTime? target, DateTime? actual = null)
    {
        if (target is null) return "-";

        var span = target.Value - (actual ?? DateTime.UtcNow);
        return span < TimeSpan.Zero ? "0 s" : span.Approximate();
    }

    /// <summary>
    /// Return a human readable approximation of the absolute value of the time span
    /// </summary>
    public static string Approximate(this TimeSpan span)
    {
        if (span < TimeSpan.Zero) span = -span;

        if (span < TimeSpan.FromSeconds(1)) return "0 s";
        if (span < TimeSpan.FromMinutes(1))
        {
            var sec = span.TotalSeconds;
            if (sec < 10) return span.TotalSeconds.ToString("0.0") + " s";
            return span.TotalSeconds.ToString("0") + " s";
        }

        if (span < TimeSpan.FromHours(1))
        {
            var min = span.TotalMinutes;
            if (min < 10) return span.TotalMinutes.ToString("0.0") + " min";
            return span.TotalMinutes.ToString("0") + " min";
        }

        if (span < TimeSpan.FromDays(2))
        {
            var hours = span.TotalHours;
            if (hours < 10) return span.TotalHours.ToString("0.0") + " h";
            return span.TotalHours.ToString("0") + " h";
        }

        if (span < TimeSpan.FromDays(366))
        {
            var days = span.TotalDays;
            if (days < 10) return span.TotalDays.ToString("0.0") + " days";
            return span.TotalDays.ToString("0") + " days";
        }

        var years = span.TotalDays / 365.2425;

        if (years < 10) return years.ToString("0.0") + " years";
        if (years < 100) return years.ToString("0") + " years";
        return "centuries";
    }
}