using System;

namespace Bloqboard.Ethereum.Common
{
    public static class StringExtensions
    {
        public static bool AddressEquals(this string a, string b)
        {
            return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
        }
    }
}