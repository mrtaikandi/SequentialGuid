namespace Taikandi
{
    using System;
    using System.Threading;

    using JetBrains.Annotations;

    /// <summary>
    /// Represents a sequential globally unique identifier (GUID).
    /// </summary>
    public sealed class SequentialGuid
    {
        #region Constants and Fields

        /// <summary>
        /// A read-only instance of the <see cref="T:System.Guid" /> structure whose value is all zeros.
        /// </summary>
        public static readonly Guid Empty = Guid.Empty;

        private static long _counter;

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Guid" /> structure filled with sequential values.
        /// </summary>
        /// <returns>A new GUID object.</returns>
        public static Guid NewGuid()
        {
            var guid = Guid.NewGuid().ToByteArray();
            var ticks = BitConverter.GetBytes(GetTicks());

            if( !BitConverter.IsLittleEndian )
                Array.Reverse(ticks);

            return new Guid(new[]
                {
                    guid[0], guid[1], guid[2], guid[3],
                    guid[4], guid[5], guid[6], guid[7],
                    ticks[1], ticks[0], ticks[7], ticks[6],
                    ticks[5], ticks[4], ticks[3], ticks[2]
                });
        }

        /// <summary>
        /// Converts the string representation of a GUID to the equivalent <see cref="T:System.Guid"/> structure.
        /// </summary>
        /// <returns>
        /// A structure that contains the value that was parsed.
        /// </returns>
        /// <param name="input">
        /// The GUID to convert.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="input"/> is null.
        /// </exception>
        /// <exception cref="T:System.FormatException">
        /// <paramref name="input"/> is not in a recognized format.
        /// </exception>
        public static Guid Parse([NotNull] string input)
        {
            return Guid.Parse(input);
        }

        /// <summary>
        /// Converts the string representation of a GUID to the equivalent <see cref="T:System.Guid"/> structure, provided that the string is in the specified format.
        /// </summary>
        /// <returns>
        /// A structure that contains the value that was parsed.
        /// </returns>
        /// <param name="input">
        /// The GUID to convert.
        /// </param>
        /// <param name="format">
        /// One of the following specifiers that indicates the exact format to use when interpreting <paramref name="input"/>: "N", "D", "B", "P", or "X".
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="input"/> or <paramref name="format"/> is null.
        /// </exception>
        /// <exception cref="T:System.FormatException">
        /// <paramref name="input"/> is not in a recognized format.
        /// </exception>
        public static Guid ParseExact([NotNull] string input, [NotNull] string format)
        {
            return Guid.ParseExact(input, format);
        }

        /// <summary>
        /// Converts the string representation of a GUID to the equivalent <see cref="T:System.Guid"/> structure.
        /// </summary>
        /// <returns>
        /// true if the parse operation was successful; otherwise, false.
        /// </returns>
        /// <param name="input">
        /// The GUID to convert.
        /// </param>
        /// <param name="result">
        /// The structure that will contain the parsed value. If the method returns true, <paramref name="result"/> contains a valid <see cref="T:System.Guid"/>. If the
        /// method returns false, <paramref name="result"/> equals <see cref="F:System.Guid.Empty"/>.
        /// </param>
        public static bool TryParse(string input, out Guid result)
        {
            return Guid.TryParse(input, out result);
        }

        /// <summary>
        /// Converts the string representation of a GUID to the equivalent <see cref="T:System.Guid"/> structure, provided that the string is in the specified format.
        /// </summary>
        /// <returns>
        /// true if the parse operation was successful; otherwise, false.
        /// </returns>
        /// <param name="input">
        /// The GUID to convert.
        /// </param>
        /// <param name="format">
        /// One of the following specifiers that indicates the exact format to use when interpreting <paramref name="input"/>: "N", "D", "B", "P", or "X".
        /// </param>
        /// <param name="result">
        /// The structure that will contain the parsed value. If the method returns true, <paramref name="result"/> contains a valid <see cref="T:System.Guid"/>. If the
        /// method returns false, <paramref name="result"/> equals <see cref="F:System.Guid.Empty"/>.
        /// </param>
        public static bool TryParseExact(string input, string format, out Guid result)
        {
            return Guid.TryParseExact(input, format, out result);
        }

        #endregion

        #region Methods

        private static long GetTicks()
        {
            if( _counter == 0 )
                _counter = DateTime.UtcNow.Ticks;

            return Interlocked.Increment(ref _counter);            
        }

        #endregion
    }
}