namespace String_Extensions
{
    using System;
    using System.Collections.Generic;

    public static class ParsingExtensions
    {
        /// <summary>
        /// Converts a Hex-string into a byte array.
        /// <para>Take a string like "BADF00BA" into a byte array. String length should be a multiple of two.</para>
        /// Remember to check for network order issues!
        /// </summary>
        public static byte[] ToByteArray(this string hexString)
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
    }
}
