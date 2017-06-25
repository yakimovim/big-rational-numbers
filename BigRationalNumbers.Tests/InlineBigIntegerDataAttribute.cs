using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using Xunit.Sdk;

namespace EdlinSoftware.BigRationalNumbers.Tests
{
    /// <summary>
    /// Provides a data source for a data theory, with the data coming from inline values.
    /// Tries to convert values to BigInteger type if possible.
    /// </summary>
    [DataDiscoverer("Xunit.Sdk.InlineDataDiscoverer", "xunit.core")]
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

        private object Convert(object arg)
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