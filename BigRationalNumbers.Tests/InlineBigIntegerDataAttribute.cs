using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace EdlinSoftware.BigRationalNumbers.Tests
{
    /// <summary>
    /// Implementation of <see cref="IDataDiscoverer"/> used to discover the data
    /// provided by <see cref="InlineBigIntegerDataAttribute"/>.
    /// </summary>
    public class InlineBigIntegerDataDiscoverer : IDataDiscoverer
    {
        /// <inheritdoc/>
        public IEnumerable<object[]> GetData(IAttributeInfo dataAttribute, IMethodInfo testMethod)
        {
            // The data from GetConstructorArguments does not maintain its original form (in particular, collections
            // end up as generic IEnumerable<T>). So we end up needing to call .ToArray() on the enumerable so that
            // we can restore the correct argument type from InlineDataAttribute.
            //
            // In addition, [InlineData(null)] gets translated into passing a null array, not a single array with a null
            // value in it, which is why the null coalesce operator is required (this is covered by the acceptance test
            // in Xunit2TheoryAcceptanceTests.InlineDataTests.SingleNullValuesWork).

            var args = (IEnumerable<object>)dataAttribute.GetConstructorArguments().Single() ?? new object[] { null };
            return new[] { args.Select(InlineBigIntegerDataAttribute.Convert).ToArray() };
        }

        /// <inheritdoc/>
        public bool SupportsDiscoveryEnumeration(IAttributeInfo dataAttribute, IMethodInfo testMethod)
        {
            return true;
        }
    }

    /// <summary>
    /// Provides a data source for a data theory, with the data coming from inline values.
    /// Tries to convert values to BigInteger type if possible.
    /// </summary>
    [DataDiscoverer("EdlinSoftware.BigRationalNumbers.Tests.InlineBigIntegerDataDiscoverer", "EdlinSoftware.BigRationalNumbers.Tests")]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class InlineBigIntegerDataAttribute : DataAttribute
    {
        private readonly object[] _data;

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineBigIntegerDataAttribute"/> class.
        /// </summary>
        /// <param name="data">The data values to pass to the theory.</param>
        public InlineBigIntegerDataAttribute(params object[] data)
        {
            _data = data.Select(Convert).ToArray();
        }

        public static object Convert(object arg)
        {
            if (arg == null)
                return null;

            if (arg is string)
            {
                var value = (string) arg;
                if (BigInteger.TryParse(value, out BigInteger result))
                { return result; }

                return value;
            }

            if (arg is byte)
            {
                var value = (byte)arg;
                return new BigInteger(value);
            }

            if (arg is sbyte)
            {
                var value = (sbyte)arg;
                return new BigInteger(value);
            }

            if (arg is Int16)
            {
                var value = (Int16)arg;
                return new BigInteger(value);
            }

            if (arg is UInt16)
            {
                var value = (UInt16)arg;
                return new BigInteger(value);
            }

            if (arg is Int32)
            {
                var value = (Int32)arg;
                return new BigInteger(value);
            }

            if (arg is UInt32)
            {
                var value = (UInt32)arg;
                return new BigInteger(value);
            }

            if (arg is Int64)
            {
                var value = (Int64)arg;
                return new BigInteger(value);
            }

            if (arg is UInt64)
            {
                var value = (UInt64)arg;
                return new BigInteger(value);
            }

            return arg;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            return new [] { _data };
        }
    }
}