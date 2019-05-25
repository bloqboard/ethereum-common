using System;
using System.Numerics;

namespace Bloqboard.Ethereum.Common
{
    public static class DateTimeExtensions
    {
        public static DateTime ToDateTime(this BigInteger unixTimestamp)
        {
            return DateTimeOffset.FromUnixTimeSeconds((long) unixTimestamp).UtcDateTime;
        }
        
        public static DateTime ToDateTime(this long timestamp)
        {
            return DateTimeOffset.FromUnixTimeSeconds(timestamp).UtcDateTime;
        }
    }
}