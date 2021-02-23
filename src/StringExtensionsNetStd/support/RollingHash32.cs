namespace String_Extensions.support
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A simple 32-bit rolling hash for strings.
    /// </summary>
    public class RollingHash32
    {
        readonly int _windowWidth;
        readonly int _windowRemainder;
        readonly LinkedList<char> _patch;

        UInt32 _value;
        const UInt32 HashConst = 21474836u;

        /// <summary>
        /// Create a new empty Rolling hash of a given window size
        /// </summary>
        public RollingHash32(int windowWidth)
        {
            _windowWidth = windowWidth;
            _windowRemainder = 32 - windowWidth;
            _patch = new LinkedList<char>();
            _value = 0;
        }


        /// <summary>
        /// Create a new Rolling hash with a given window size,
        /// and pre-fill it with a string until the window is full.
        /// </summary>
        public RollingHash32(int windowWidth, string prefillSource)
        {
            _windowWidth = windowWidth;
            _windowRemainder = 32 - windowWidth;
            _patch = new LinkedList<char>();
            _value = 0;

            int i = 0;
            foreach (var c in prefillSource)
            {
                if (i >= windowWidth) break;
                Fill(c);
                i++;
            }
        }

        /// <summary>
        /// Last updated hash value
        /// </summary>
        public uint Value => _value;

        /// <summary>
        /// Return the has of a string, given a window equal to its length
        /// </summary>
        public static UInt32 HashOfString(string str)
        {
            var rh = new RollingHash32(str.Length);
            foreach (var c in str)
            {
                rh.Fill(c);
            }
            return rh._value;
        }

        /// <summary>
        /// Add a character to the rolling hash. Most recent hash value is returned
        /// </summary>
        public UInt32 AddChar(char c)
        {
            if (_patch.Count >= _windowWidth) return Update(c);
            return Fill(c);
        }

        /// <summary>
        /// Roll patch and update hash value
        /// </summary>
        uint Update(char c)
        {
            var tail = _patch.First?.Value ?? (char)0;
            _patch.RemoveFirst();
            _patch.AddLast(c);
            var x = tail * HashConst;
            var y = (_value << 1) | (_value >> 31);
            var z = (x << _windowWidth) | (x >> _windowRemainder);
            _value = y ^ z ^ (c * HashConst);
            return _value;
        }

        /// <summary>
        /// Fill patch and update hash, without rolling
        /// </summary>
        uint Fill(char c)
        {
            _patch.AddLast(c);
            
            _value = (_value << 1) | (_value >> 31);
            _value ^= c * HashConst;
            return _value;
        }
    }
}