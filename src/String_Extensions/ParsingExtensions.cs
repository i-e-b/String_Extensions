namespace String_Extensions
{
    using System;

    public static class ParsingExtensions
    {
        /// <summary>
        /// Converts a Hex-string into a byte array.
        /// <para>Take a string like "BADF00BA" into a byte array. String length should be a multiple of two.</para>
        /// Remember to check for network order issues!
        /// </summary>
        public static byte[] ToByteArray(this string hexString)
        {
            var numberChars = hexString.Length;
            var bytes = new byte[numberChars / 2];
            for (var i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            }
            return bytes;
        }
    }
}
