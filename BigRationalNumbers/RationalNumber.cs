using System;
using System.Diagnostics;
using System.Numerics;

namespace EdlinSoftware.BigRationalNumbers
{
    /// <summary>
    /// Represents rational number.
    /// </summary>
    public struct RationalNumber : IEquatable<RationalNumber>, IComparable<RationalNumber>
    {
        /// <summary>
        /// Zero value.
        /// </summary>
        public static readonly RationalNumber Zero = new RationalNumber(0, 1);
        /// <summary>
        /// One value.
        /// </summary>
        public static readonly RationalNumber One = new RationalNumber(1, 1);
        /// <summary>
        /// Minus one value.
        /// </summary>
        public static readonly RationalNumber MinusOne = new RationalNumber(-1, 1);

        /// <summary>
        /// Numerator of value.
        /// </summary>
        public readonly BigInteger Numerator;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private BigInteger _denominator;

        /// <summary>
        /// Denominator of value.
        /// </summary>
        /// <remarks>This is always positive value.</remarks>
        public BigInteger Denominator
        {
            [DebuggerStepThrough]
            get
            {
                // protection from default constructor.
                if (_denominator == 0)
                { _denominator = 1; }

                return _denominator;
            }
        }

        /// <summary>
        /// Initializes instance of <see cref="RationalNumber"/>
        /// </summary>
        /// <param name="numerator">Value of numerator.</param>
        /// <param name="denominator">Value of denominator.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Denominator is zero.</exception>
        public RationalNumber(BigInteger numerator, BigInteger denominator)
        {
            if (denominator == BigInteger.Zero) throw new ArgumentOutOfRangeException(nameof(denominator));

            if (numerator == 0)
            {
                Numerator = 0;
                _denominator = 1;
            }
            else
            {
                var greatestCommonDenominator = GreatestCommonDenominator(numerator, denominator);

                Numerator = denominator > 0 ? numerator / greatestCommonDenominator : -numerator / greatestCommonDenominator;
                _denominator = BigInteger.Abs(denominator / greatestCommonDenominator);
            }
        }

        /// <summary>
        /// Returns greatest common denominator of two integers.
        /// </summary>
        /// <param name="a">First intreger.</param>
        /// <param name="b">Second integer.</param>
        private static BigInteger GreatestCommonDenominator(BigInteger a, BigInteger b)
        {
            if (a < 0)
            { a = -a; }
            if (b < 0)
            { b = -b; }

            return BigInteger.GreatestCommonDivisor(a, b);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is RationalNumber))
                return false;

            return Equals((RationalNumber)obj);
        }

        /// <inheritdoc />
        public bool Equals(RationalNumber other)
        {
            return Numerator == other.Numerator && Denominator == other.Denominator;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (Numerator.GetHashCode() * 397) ^ Denominator.GetHashCode();
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            if (Denominator == 1)
            { return Numerator.ToString(); }

            return Numerator + "/" + Denominator;
        }

        /// <inheritdoc />
        public static bool operator ==(RationalNumber rn1, RationalNumber rn2)
        {
            return rn1.Equals(rn2);
        }

        /// <inheritdoc />
        public static bool operator !=(RationalNumber rn1, RationalNumber rn2)
        {
            return !(rn1 == rn2);
        }

        /// <inheritdoc />
        public static bool operator <(RationalNumber rn1, RationalNumber rn2)
        {
            var n1 = rn1.Numerator;
            var d1 = rn1.Denominator;
            var n2 = rn2.Numerator;
            var d2 = rn2.Denominator;

            return n1 * d2 < n2 * d1;
        }

        /// <inheritdoc />
        public static bool operator >(RationalNumber rn1, RationalNumber rn2)
        {
            return !(rn1 == rn2) && !(rn1 < rn2);
        }

        /// <inheritdoc />
        public static bool operator <=(RationalNumber rn1, RationalNumber rn2)
        {
            return (rn1 == rn2) || (rn1 < rn2);
        }

        /// <inheritdoc />
        public static bool operator >=(RationalNumber rn1, RationalNumber rn2)
        {
            return (rn1 == rn2) || (rn1 > rn2);
        }

        /// <inheritdoc />
        public int CompareTo(RationalNumber other)
        {
            if (this < other)
                return -1;
            if (this == other)
                return 0;
            return 1;
        }

        /// <inheritdoc />
        public static RationalNumber operator -(RationalNumber rn)
        {
            return new RationalNumber(-rn.Numerator, rn.Denominator);
        }

        /// <inheritdoc />
        public static RationalNumber operator +(RationalNumber rn1, RationalNumber rn2)
        {
            var n1 = rn1.Numerator;
            var d1 = rn1.Denominator;
            var n2 = rn2.Numerator;
            var d2 = rn2.Denominator;

            return new RationalNumber(n1 * d2 + n2 * d1, d1 * d2);
        }

        /// <inheritdoc />
        public static RationalNumber operator -(RationalNumber rn1, RationalNumber rn2)
        {
            return rn1 + (-rn2);
        }

        /// <inheritdoc />
        public static RationalNumber operator *(RationalNumber rn1, RationalNumber rn2)
        {
            var n1 = rn1.Numerator;
            var d1 = rn1.Denominator;
            var n2 = rn2.Numerator;
            var d2 = rn2.Denominator;

            return new RationalNumber(n1 * n2, d1 * d2);
        }

        /// <inheritdoc />
        public static RationalNumber operator /(RationalNumber rn1, RationalNumber rn2)
        {
            if(rn2.Numerator == 0)
                throw new DivideByZeroException();

            var n1 = rn1.Numerator;
            var d1 = rn1.Denominator;
            var n2 = rn2.Numerator;
            var d2 = rn2.Denominator;

            return new RationalNumber(n1 * d2, d1 * n2);
        }

        /// <inheritdoc />
        public static implicit operator RationalNumber(byte value)
        {
            return new RationalNumber(value, 1);
        }

        /// <inheritdoc />
        public static implicit operator RationalNumber(sbyte value)
        {
            return new RationalNumber(value, 1);
        }

        /// <inheritdoc />
        public static implicit operator RationalNumber(Int16 value)
        {
            return new RationalNumber(value, 1);
        }

        /// <inheritdoc />
        public static implicit operator RationalNumber(UInt16 value)
        {
            return new RationalNumber(value, 1);
        }

        /// <inheritdoc />
        public static implicit operator RationalNumber(Int32 value)
        {
            return new RationalNumber(value, 1);
        }

        /// <inheritdoc />
        public static implicit operator RationalNumber(UInt32 value)
        {
            return new RationalNumber(value, 1);
        }

        /// <inheritdoc />
        public static implicit operator RationalNumber(Int64 value)
        {
            return new RationalNumber(value, 1);
        }

        /// <inheritdoc />
        public static implicit operator RationalNumber(UInt64 value)
        {
            return new RationalNumber(value, 1);
        }

        /// <inheritdoc />
        public static implicit operator RationalNumber(BigInteger value)
        {
            return new RationalNumber(value, 1);
        }

        /// <inheritdoc />
        public static explicit operator double(RationalNumber value)
        {
            var n = (double)value.Numerator;
            var d = (double)value.Denominator;

            return n / d;
        }

        /// <inheritdoc />
        public static explicit operator decimal(RationalNumber value)
        {
            var n = (decimal)value.Numerator;
            var d = (decimal)value.Denominator;

            return n / d;
        }

        /// <inheritdoc />
        public static explicit operator float(RationalNumber value)
        {
            var n = (float)value.Numerator;
            var d = (float)value.Denominator;

            return n / d;
        }

        /// <summary>
        /// Returns absolute value of this number.
        /// </summary>
        public RationalNumber Abs()
        {
            if (Numerator >= 0)
                return this;

            return -this;
        }

        /// <summary>
        /// Returns absolute value of given number.
        /// </summary>
        /// <param name="rationalNumber">Rational number.</param>
        public static RationalNumber Abs(RationalNumber rationalNumber)
        {
            return rationalNumber.Abs();
        }

        /// <summary>
        /// Raises the rational number into integer power.
        /// </summary>
        /// <param name="exponent">Power.</param>
        public RationalNumber Pow(int exponent)
        {
            if (Numerator == BigInteger.Zero)
            {
                if (exponent < 0)
                {
                    throw new DivideByZeroException("Unable to raise zero value to negative power.");
                }
                if (exponent == 0)
                {
                    return One;
                }

                return Zero;
            }

            if (exponent == 0)
                return One;

            if (exponent > 0)
            {
                var numerator = BigInteger.Pow(Numerator, exponent);
                var denominator = BigInteger.Pow(Denominator, exponent);
                return new RationalNumber(numerator, denominator);
            }
            else
            {
                exponent = -exponent;
                var numerator = BigInteger.Pow(Denominator, exponent);
                var denominator = BigInteger.Pow(Numerator, exponent);
                return new RationalNumber(numerator, denominator);
            }
        }

        /// <summary>
        /// Raises the rational number into integer power.
        /// </summary>
        /// <param name="rationalNumber">Rational number.</param>
        /// <param name="exponent">Power.</param>
        public static RationalNumber Pow(RationalNumber rationalNumber, int exponent)
        {
            return rationalNumber.Pow(exponent);
        }

        /// <summary>
        /// Computes natural logarithm of the rational number.
        /// </summary>
        /// <returns>
        /// double.NegativeInfinity, if number is zero.
        /// double.NaN, if number is negative.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public double Log()
        {
            return BigInteger.Log(Numerator) - BigInteger.Log(Denominator);
        }

        /// <summary>
        /// Computes natural logarithm of the rational number.
        /// </summary>
        /// <param name="rationalNumber">Rational number.</param>
        /// <returns>
        /// double.NegativeInfinity, if number is zero.
        /// double.NaN, if number is negative.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static double Log(RationalNumber rationalNumber)
        {
            return rationalNumber.Log();
        }

        /// <summary>
        /// Computes logarithm with given base of the rational number.
        /// </summary>
        /// <param name="baseValue">Base of the logarithm.</param>
        /// <returns>
        /// double.NegativeInfinity, if number is zero.
        /// double.NaN, if number is negative.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public double Log(double baseValue)
        {
            return BigInteger.Log(Numerator, baseValue) - BigInteger.Log(Denominator, baseValue);
        }

        /// <summary>
        /// Computes logarithm with given base of the rational number.
        /// </summary>
        /// <param name="rationalNumber">Rational number.</param>
        /// <param name="baseValue">Base of the logarithm.</param>
        /// <returns>
        /// double.NegativeInfinity, if number is zero.
        /// double.NaN, if number is negative.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static double Log(RationalNumber rationalNumber, double baseValue)
        {
            return rationalNumber.Log(baseValue);
        }

        /// <summary>
        /// Computes base 10 logarithm of the rational number.
        /// </summary>
        /// <returns>
        /// double.NegativeInfinity, if number is zero.
        /// double.NaN, if number is negative.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public double Log10()
        {
            return BigInteger.Log10(Numerator) - BigInteger.Log10(Denominator);
        }

        /// <summary>
        /// Computes base 10 logarithm of the rational number.
        /// </summary>
        /// <param name="rationalNumber">Rational number.</param>
        /// <returns>
        /// double.NegativeInfinity, if number is zero.
        /// double.NaN, if number is negative.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static double Log10(RationalNumber rationalNumber)
        {
            return rationalNumber.Log10();
        }

        /// <summary>
        /// Returns minimum of two numbers.
        /// </summary>
        /// <param name="rationalNumber1">First rational number.</param>
        /// <param name="rationalNumber2">Second rational number.</param>
        public static RationalNumber Min(RationalNumber rationalNumber1, RationalNumber rationalNumber2)
        {
            return rationalNumber1 <= rationalNumber2
                ? rationalNumber1
                : rationalNumber2;
        }

        /// <summary>
        /// Returns maximum of two numbers.
        /// </summary>
        /// <param name="rationalNumber1">First rational number.</param>
        /// <param name="rationalNumber2">Second rational number.</param>
        public static RationalNumber Max(RationalNumber rationalNumber1, RationalNumber rationalNumber2)
        {
            return rationalNumber1 >= rationalNumber2
                ? rationalNumber1
                : rationalNumber2;
        }
    }
}
