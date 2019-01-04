namespace System.Math.LinearAlgebra
{
    internal static class Guard
    {
        /// <summary>
        /// Throws an exception if the given argument is null.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <c>null</c>.</exception>
        public static void ThrowIfArgumentNull(object value, string paramName)
        {
            if (object.ReferenceEquals(value, null))
            {
                throw new ArgumentNullException(paramName);
            }
        }

        /// <summary>
        /// Throws an exception if the given value is greater than the maximum, optionally inclusive.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="inclusive">if set to <c>true</c>, a greater than or equal to operation is performed.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/> fails the test.</exception>
        public static void ThrowIfGreaterThan(int value, int maxValue, string paramName, bool inclusive = false)
        {
            var isTooBig = inclusive ? value >= maxValue : value > maxValue;

            if (isTooBig)
            {
                throw new ArgumentOutOfRangeException(paramName);
            }
        }

        /// <summary>
        /// Throws an exception if the given value is greater than the maximum, optionally inclusive.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <param name="inclusive">if set to <c>true</c>, a greater than or equal to operation is performed.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if <paramref name="value"/> fails the test.</exception>
        public static void ThrowIfIndexGreaterThan(int value, int maxValue, bool inclusive = false)
        {
            var isTooBig = inclusive ? value >= maxValue : value > maxValue;

            if (isTooBig)
            {
                throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Throws an exception if the given value is less than the minimum, optionally inclusive.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="inclusive">if set to <c>true</c>, a less than or equal to operation is performed.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if <paramref name="value"/> fails the test.</exception>
        public static void ThrowIfIndexLessThan(int value, int minValue, bool inclusive = false)
        {
            var isTooSmall = inclusive ? value <= minValue : value < minValue;

            if (isTooSmall)
            {
                throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Throws an exception if the given value is less than zero.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if <paramref name="value"/> fails the test.</exception>
        public static void ThrowIfIndexLessThanZero(int value) => Guard.ThrowIfIndexLessThan(value, 0);

        /// <summary>
        /// Throws an exception if the given value is less than the minimum, optionally inclusive.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="inclusive">if set to <c>true</c>, a less than or equal to operation is performed.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/> fails the test.</exception>
        public static void ThrowIfLessThan(int value, int minValue, string paramName, bool inclusive = false)
        {
            var isTooSmall = inclusive ? value <= minValue : value < minValue;

            if (isTooSmall)
            {
                throw new ArgumentOutOfRangeException(paramName);
            }
        }

        /// <summary>
        /// Throws an exception if the given value is less than zero.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/> fails the test.</exception>
        public static void ThrowIfLessThanZero(int value, string paramName) => Guard.ThrowIfLessThan(value, 0, paramName);
    }
}