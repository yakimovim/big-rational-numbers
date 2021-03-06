[![Build status](https://ci.appveyor.com/api/projects/status/dc2oh8flsubft2r0/branch/master?svg=true)](https://ci.appveyor.com/project/IvanIakimov/big-rational-numbers/branch/master)

# Rational Numbers library

This library implements structure for presentation of rational numbers in .NET. The implementation is based on numerator and denomination which are presented by two 'BigInteger' values. Denominator is always positive and numerator can have any sign. Rational numbers in the library are presented in the form of irreducible fractions. Algorithm for calculation of greatest common divisor is used to reduce numbers into this form.

 The following operations are supported by the library:

* \+
* \-
* \*
* /
* \>
* \<
* \>=
* \<=
* ==
* \!=
* absolute value
* minimum and maximum of two values
* power
* different logarithms
* implicit convertion from integer values (byte, int, long, ...)
* explicit convertion to double, decimal and float

Be aware, that conversion to decimal and float types can lead to OverflowException exception. Conversion to double type can result in infinity.

RationalNumber structure implements IEquatable and IComparable interfaces.