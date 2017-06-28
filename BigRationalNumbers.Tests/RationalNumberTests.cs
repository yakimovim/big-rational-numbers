using System;
using System.Numerics;
using Xunit;

namespace EdlinSoftware.BigRationalNumbers.Tests
{
    public class RationalNumberTests
    {
        [Fact]
        public void DefaultValueIsOne()
        {
            var rationalNumber = default(RationalNumber);

            Assert.Equal(0, rationalNumber.Numerator);
            Assert.Equal(1, rationalNumber.Denominator);
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentOutOfRangeException_IfDenominatorIsNull()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new RationalNumber(1, 0));
            Assert.Equal("denominator", exception.ParamName);
        }

        [Theory]
        [InlineBigIntegerData(4, 2, 2, 1)]
        [InlineBigIntegerData(8, 6, 4, 3)]
        [InlineBigIntegerData(5, 3, 5, 3)]
        [InlineBigIntegerData("333333333333333333333333", 3, "111111111111111111111111", 1)]
        public void Constructor_ShouldSimplifyValue(BigInteger inputNumerator, BigInteger inputDenominator, BigInteger expectedNumerator, BigInteger expectedDenominator)
        {
            var rationalNumber = new RationalNumber(inputNumerator, inputDenominator);

            Assert.Equal(expectedNumerator, rationalNumber.Numerator);
            Assert.Equal(expectedDenominator, rationalNumber.Denominator);
        }

        [Fact]
        public void Denominator_IsAlwaysPositive()
        {
            var rationalNumber = new RationalNumber(1, -1);

            Assert.True(rationalNumber.Denominator > 0);
        }

        [Fact]
        public void Denominator_ShouldBeOne_ForZero()
        {
            var rationalNumber = new RationalNumber(0, -10);

            Assert.Equal(1, rationalNumber.Denominator);
        }

        [Theory]
        [InlineData(4, 2, "2")]
        [InlineData(8, 6, "4/3")]
        [InlineData(5, 3, "5/3")]
        [InlineData(0, 3, "0")]
        [InlineData(-1, 3, "-1/3")]
        [InlineData(-4, -3, "4/3")]
        [InlineData(long.MaxValue, long.MaxValue, "1")]
        [InlineData(long.MaxValue, long.MinValue + 1, "-1")]
        [InlineData(long.MinValue + 1, long.MaxValue, "-1")]
        public void ToString_Values(long inputNumerator, long inputDenominator, string expectedString)
        {
            var rationalNumber = new RationalNumber(inputNumerator, inputDenominator);

            Assert.Equal(expectedString, rationalNumber.ToString());
        }

        [Theory]
        [InlineBigIntegerData(4, 2, 4, 2, true)]
        [InlineBigIntegerData(4, 2, 2, 1, true)]
        [InlineBigIntegerData(4, 2, -2, -1, true)]
        [InlineBigIntegerData(4, 2, -2, 1, false)]
        [InlineBigIntegerData(5, 3, 7, 2, false)]
        [InlineBigIntegerData("333333333333333333333333", 3, "111111111111111111111111", 1, true)]
        [InlineBigIntegerData("333333333333333333333333", 3, "111111111111111111111112", 1, false)]
        public void Equal_Values(BigInteger numerator1, BigInteger denominator1, BigInteger numerator2, BigInteger denominator2, bool areEqual)
        {
            var rationalNumber1 = new RationalNumber(numerator1, denominator1);
            var rationalNumber2 = new RationalNumber(numerator2, denominator2);

            Assert.Equal(areEqual, rationalNumber1.Equals(rationalNumber2));
        }

        [Theory]
        [InlineBigIntegerData(4, 2, 4, 2, true)]
        [InlineBigIntegerData(4, 2, 2, 1, true)]
        [InlineBigIntegerData(4, 2, -2, -1, true)]
        [InlineBigIntegerData(4, 2, -2, 1, false)]
        [InlineBigIntegerData(5, 3, 7, 2, false)]
        [InlineBigIntegerData("333333333333333333333333", 3, "111111111111111111111111", 1, true)]
        [InlineBigIntegerData("333333333333333333333333", 3, "111111111111111111111112", 1, false)]
        public void EqualityOperator_Values(BigInteger numerator1, BigInteger denominator1, BigInteger numerator2, BigInteger denominator2, bool areEqual)
        {
            var rationalNumber1 = new RationalNumber(numerator1, denominator1);
            var rationalNumber2 = new RationalNumber(numerator2, denominator2);

            Assert.Equal(areEqual, rationalNumber1 == rationalNumber2);
        }

        [Theory]
        [InlineBigIntegerData(4, 2, 4, 2, false)]
        [InlineBigIntegerData(-4, 2, -4, 2, false)]
        [InlineBigIntegerData(4, 2, 2, 1, false)]
        [InlineBigIntegerData(4, 2, -2, -1, false)]
        [InlineBigIntegerData(4, 2, -2, 1, false)]
        [InlineBigIntegerData(4, 2, 6, 1, true)]
        [InlineBigIntegerData(5, 3, 7, 2, true)]
        [InlineBigIntegerData(long.MaxValue - 1, long.MaxValue, long.MaxValue - 1, long.MaxValue, false)]
        [InlineBigIntegerData(long.MaxValue - 2, long.MaxValue, long.MaxValue - 1, long.MaxValue, true)]
        [InlineBigIntegerData(long.MinValue + 2, long.MaxValue, long.MinValue + 1, long.MaxValue, false)]
        [InlineBigIntegerData(100000000L, 1, 10000000000L, 101, false)]
        [InlineBigIntegerData(-100000000L, 1, -10000000000L, 101, true)]
        [InlineBigIntegerData("333333333333333333333333", 3, "111111111111111111111111", 1, false)]
        [InlineBigIntegerData("333333333333333333333333", 3, "111111111111111111111112", 1, true)]
        public void LessOperator_Values(BigInteger numerator1, BigInteger denominator1, BigInteger numerator2, BigInteger denominator2, bool isTrue)
        {
            var rationalNumber1 = new RationalNumber(numerator1, denominator1);
            var rationalNumber2 = new RationalNumber(numerator2, denominator2);

            Assert.Equal(isTrue, rationalNumber1 < rationalNumber2);
        }

        [Theory]
        [InlineBigIntegerData(4, 2, 4, 2, false)]
        [InlineBigIntegerData(-4, 2, -4, 2, false)]
        [InlineBigIntegerData(4, 2, 2, 1, false)]
        [InlineBigIntegerData(4, 2, -2, -1, false)]
        [InlineBigIntegerData(4, 2, -2, 1, true)]
        [InlineBigIntegerData(4, 2, 6, 1, false)]
        [InlineBigIntegerData(5, 3, 7, 2, false)]
        [InlineBigIntegerData(7, 2, 5, 3, true)]
        [InlineBigIntegerData(long.MaxValue - 1, long.MaxValue, long.MaxValue - 1, long.MaxValue, false)]
        [InlineBigIntegerData(long.MaxValue - 2, long.MaxValue, long.MaxValue - 1, long.MaxValue, false)]
        [InlineBigIntegerData(long.MinValue + 2, long.MaxValue, long.MinValue + 1, long.MaxValue, true)]
        [InlineBigIntegerData(100000000L, 1, 10000000000L, 101, true)]
        [InlineBigIntegerData(-100000000L, 1, -10000000000L, 101, false)]
        [InlineBigIntegerData("333333333333333333333333", 3, "111111111111111111111111", 1, false)]
        [InlineBigIntegerData("333333333333333333333333", 3, "111111111111111111111112", 1, false)]
        [InlineBigIntegerData("333333333333333333333333", 3, "111111111111111111111110", 1, true)]
        public void GreaterOperator_Values(BigInteger numerator1, BigInteger denominator1, BigInteger numerator2, BigInteger denominator2, bool isTrue)
        {
            var rationalNumber1 = new RationalNumber(numerator1, denominator1);
            var rationalNumber2 = new RationalNumber(numerator2, denominator2);

            Assert.Equal(isTrue, rationalNumber1 > rationalNumber2);
        }

        [Theory]
        [InlineBigIntegerData(1, 2, 4, 2)]
        [InlineBigIntegerData(-2, 1, 4, 2)]
        [InlineBigIntegerData(long.MaxValue - 2, long.MaxValue, long.MaxValue - 1, long.MaxValue)]
        [InlineBigIntegerData(long.MinValue + 1, long.MaxValue, long.MinValue + 2, long.MaxValue)]
        [InlineBigIntegerData("333333333333333333333333", 3, "111111111111111111111112", 1)]
        public void CompareTo_FirstIsLess_Values(BigInteger numerator1, BigInteger denominator1, BigInteger numerator2, BigInteger denominator2)
        {
            var rationalNumber1 = new RationalNumber(numerator1, denominator1);
            var rationalNumber2 = new RationalNumber(numerator2, denominator2);

            Assert.True(rationalNumber1.CompareTo(rationalNumber2) < 0);
        }

        [Theory]
        [InlineBigIntegerData(4, 2, 1, 2)]
        [InlineBigIntegerData(2, 1, -2, 1)]
        [InlineBigIntegerData(long.MaxValue - 1, long.MaxValue, long.MaxValue - 2, long.MaxValue)]
        [InlineBigIntegerData(long.MinValue + 2, long.MaxValue, long.MinValue + 1, long.MaxValue)]
        [InlineBigIntegerData("333333333333333333333333", 3, "111111111111111111111110", 1)]
        public void CompareTo_SecondIsLess_Values(BigInteger numerator1, BigInteger denominator1, BigInteger numerator2, BigInteger denominator2)
        {
            var rationalNumber1 = new RationalNumber(numerator1, denominator1);
            var rationalNumber2 = new RationalNumber(numerator2, denominator2);

            Assert.True(rationalNumber1.CompareTo(rationalNumber2) > 0);
        }

        [Theory]
        [InlineBigIntegerData(4, 2, 2, 1)]
        [InlineBigIntegerData(-2, 1, -2, 1)]
        [InlineBigIntegerData(long.MaxValue - 1, long.MaxValue, long.MaxValue - 1, long.MaxValue)]
        [InlineBigIntegerData(long.MinValue + 1, long.MaxValue, long.MinValue + 1, long.MaxValue)]
        [InlineBigIntegerData("333333333333333333333333", 3, "111111111111111111111111", 1)]
        public void CompareTo_BothEqual_Values(BigInteger numerator1, BigInteger denominator1, BigInteger numerator2, BigInteger denominator2)
        {
            var rationalNumber1 = new RationalNumber(numerator1, denominator1);
            var rationalNumber2 = new RationalNumber(numerator2, denominator2);

            Assert.True(rationalNumber1.CompareTo(rationalNumber2) == 0);
        }

        [Theory]
        [InlineBigIntegerData(2, 1, 2, 1, 4, 1)]
        [InlineBigIntegerData(2, 1, -2, 1, 0, 1)]
        [InlineBigIntegerData(2, 3, 3, 4, 17, 12)]
        [InlineBigIntegerData(5, 6, 7, 8, 41, 24)]
        [InlineBigIntegerData("333333333333333333333333", 3, "111111111111111111111111", 1, "222222222222222222222222", 1)]
        public void AddOperator_Values(BigInteger numerator1, BigInteger denominator1, BigInteger numerator2, BigInteger denominator2,
            BigInteger resultNumerator, BigInteger resultDenominator)
        {
            Assert.Equal(new RationalNumber(resultNumerator, resultDenominator), new RationalNumber(numerator1, denominator1) + new RationalNumber(numerator2, denominator2));
        }

        [Theory]
        [InlineBigIntegerData(2, 1, 2, 1, 0, 1)]
        [InlineBigIntegerData(2, 1, -2, 1, 4, 1)]
        [InlineBigIntegerData(2, 3, 3, 4, -1, 12)]
        [InlineBigIntegerData(5, 6, 7, 8, -1, 24)]
        [InlineBigIntegerData("333333333333333333333333", 3, "111111111111111111111111", 1, 0, 1)]
        public void SubOperator_Values(BigInteger numerator1, BigInteger denominator1, BigInteger numerator2, BigInteger denominator2,
            BigInteger resultNumerator, BigInteger resultDenominator)
        {
            Assert.Equal(new RationalNumber(resultNumerator, resultDenominator), new RationalNumber(numerator1, denominator1) - new RationalNumber(numerator2, denominator2));
        }

        [Theory]
        [InlineBigIntegerData(2, 1, 2, 1, 4, 1)]
        [InlineBigIntegerData(2, 1, -2, 1, -4, 1)]
        [InlineBigIntegerData(2, 3, 3, 4, 1, 2)]
        [InlineBigIntegerData(5, 6, 7, 8, 35, 48)]
        [InlineBigIntegerData("1000000000000000", 1, "1000000000000000", 1, "1000000000000000000000000000000", 1)]
        public void MulOperator_Values(BigInteger numerator1, BigInteger denominator1, BigInteger numerator2, BigInteger denominator2,
            BigInteger resultNumerator, BigInteger resultDenominator)
        {
            Assert.Equal(new RationalNumber(resultNumerator, resultDenominator), new RationalNumber(numerator1, denominator1) * new RationalNumber(numerator2, denominator2));
        }

        [Theory]
        [InlineBigIntegerData(2, 1, 2, 1, 1, 1)]
        [InlineBigIntegerData(2, 1, -2, 1, -1, 1)]
        [InlineBigIntegerData(2, 3, 3, 4, 8, 9)]
        [InlineBigIntegerData(5, 6, 7, 8, 20, 21)]
        [InlineBigIntegerData("1000000000000000000000000000000", 1, "2000000000000000", 2, "1000000000000000", 1)]
        public void DivOperator_Values(BigInteger numerator1, BigInteger denominator1, BigInteger numerator2, BigInteger denominator2,
            BigInteger resultNumerator, BigInteger resultDenominator)
        {
            Assert.Equal(new RationalNumber(resultNumerator, resultDenominator), new RationalNumber(numerator1, denominator1) / new RationalNumber(numerator2, denominator2));
        }

        [Fact]
        public void DivOperator_DivisionByZero()
        {
            Assert.Throws<DivideByZeroException>(() => new RationalNumber(1, 1) / new RationalNumber(0, 1));
        }

        [Theory]
        [InlineBigIntegerData(2, 1, true)]
        [InlineBigIntegerData(-2, 1, false)]
        [InlineBigIntegerData(long.MaxValue, 1, true)]
        [InlineBigIntegerData(long.MinValue + 1, 1, false)]
        [InlineBigIntegerData("333333333333333333333333", 3, true)]
        [InlineBigIntegerData("-333333333333333333333333", 3, false)]
        public void Abs_Values(BigInteger numerator1, BigInteger denominator1, bool isSame)
        {
            if (isSame)
            {
                Assert.Equal(new RationalNumber(numerator1, denominator1), new RationalNumber(numerator1, denominator1).Abs());
            }
            else
            {
                Assert.Equal(new RationalNumber(-numerator1, denominator1), new RationalNumber(numerator1, denominator1).Abs());

            }
        }
    }
}
