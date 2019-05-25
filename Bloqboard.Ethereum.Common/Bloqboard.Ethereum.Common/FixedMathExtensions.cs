using System;
using System.Numerics;

namespace Bloqboard.Ethereum.Common
{
    public static class FixedMathExtensions
    {
        public static decimal ToDecimal(this BigInteger amount, int decimals = 0)
        {
            var decimalsBigInteger = BigInteger.Pow(10, decimals);
            var result = BigInteger.DivRem(amount, decimalsBigInteger, out var remainder);
            var resultDecimal = (decimal) result + ((decimal) remainder / (decimal) decimalsBigInteger);
            return resultDecimal;
        }
        
        public static decimal ToDecimal(this BigInteger amount, uint decimals) => amount.ToDecimal((int) decimals);

        // basically stealing methods from here https://github.com/dapphub/ds-math/blob/49b38937c0c0b8af73b05f767a0af9d5e85a1e6c/src/math.sol
        // wad has 18 precision points
        // ray has 27 precision points

        public static readonly BigInteger WadPrecision = new BigInteger(Math.Pow(10, 18));
        public static readonly BigInteger RayPrecision = new BigInteger(Math.Pow(10, 27));

        public static BigInteger WadMultiply(this BigInteger x, BigInteger y)
        {
            return BigInteger.Divide(
                BigInteger.Add(
                    BigInteger.Multiply(x, y),
                    BigInteger.Divide(WadPrecision, new BigInteger(2))),
                WadPrecision);
        }
        
        public static BigInteger WadDivide(this BigInteger x, BigInteger y)
        {
            return BigInteger.Divide(
                BigInteger.Add(
                    BigInteger.Multiply(x, WadPrecision),
                    BigInteger.Divide(y, new BigInteger(2))),
                y);
        }

        public static BigInteger RayMultiply(this BigInteger x, BigInteger y)
        {
            return BigInteger.Divide(
                BigInteger.Add(
                    BigInteger.Multiply(x, y),
                    BigInteger.Divide(RayPrecision, new BigInteger(2))),
                RayPrecision);
        }

        // TODO: cover with unit tests
        public static BigInteger RayPower(this BigInteger x, BigInteger n)
        {
            var result = !BigInteger.Remainder(n, new BigInteger(2)).IsZero ? x : RayPrecision;

            for (var i = BigInteger.Divide(n, new BigInteger(2)); i != 0; i = BigInteger.Divide(i, new BigInteger(2)))
            {
                x = RayMultiply(x, x);

                if (BigInteger.Remainder(i, new BigInteger(2)) != 0)
                {
                    result = RayMultiply(result, x);
                }
            }

            return result;
        }
    }
}