using System;
using System.Collections.Generic;
using System.Linq;

namespace IslandOfMisfitTypes.Collections
{
    /// <summary>
    /// Generates a series of terms using zero or more initial terms, then calculating the
    /// subsequent values using a supplied function. The first N calls to <see cref="Next"/> 
    /// (where N is the length of the initial values) will return the initial values in the order 
    /// supplied. Subsequent calls to <see cref="Next"/> will return a value calculated by passing 
    /// the last last N return values (from oldest to newest) to the given function.
    /// </summary>
    /// <remarks>
    /// This type is not threadsafe.
    /// </remarks>
    public class Series<T>
    {
        private readonly T[] _initialValues;
        private readonly Queue<T> _pendingArguments;
        private readonly Func<T[], T> _nextValue;

        /// <summary>
        /// Creates a new series that begins with <paramref name="initialValues"/> then calculates
        /// subsequent values using <paramref name="func"/>.
        /// </summary>
        /// <param name="initialValues">
        /// The first values, in order, to return from the series.
        /// </param>
        /// <param name="func">The function to calculate subsequent values from.</param>
        /// <remarks>
        /// The array of <typeparamref name="T"/> passed to <paramref name="func"/> will always be 
        /// the same size as <paramref name="initialValues"/>. If <paramref name="func"/> attempts 
        /// to access elements that are ouf of range then an exception will be thrown during
        /// enumeration. The non-array constructers are preferred as they provide index safety.
        /// </remarks>
        public Series(T[] initialValues, Func<T[], T> func)
        {
            _initialValues =
                initialValues ?? throw new ArgumentNullException(nameof(initialValues));
            _nextValue = func ?? throw new ArgumentNullException(nameof(func));
            _pendingArguments = new Queue<T>(initialValues.Length);
        }

        /// <summary>
        /// Creates a copy of a <see cref="Series{T}"/>.
        /// </summary>
        /// <param name="source">The series to copy.</param>
        /// <param name="preservePosition">
        /// If <c>false</c> the new <see cref="Series{T}"/> will start with the initial values 
        /// specified when <paramref name="source"/> was created, if <c>true</c> it will begin at
        /// the same position as <paramref name="source"/> at the time of the copy.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// WARNING:
        /// If the function provided for <paramref name="source"/> was impure then the new instance
        /// will tied to <paramref name="source"/> via the functions shared state.
        /// </remarks>
        public Series(Series<T> source, bool preservePosition = false)
        {
            if (source == null) { throw new ArgumentNullException(nameof(source)); }
            _initialValues = source._initialValues.ToArray();
            _nextValue = source._nextValue;
            _pendingArguments = new Queue<T>(source._pendingArguments);
            if (!preservePosition)
            {
                Reset();
            }
        }

        /// <summary>
        /// Initializes a new <see cref="Series{T}"/>.
        /// </summary>
        /// <param name="a">The first term in the series.</param>
        /// <param name="func">
        /// The function used to calculate subsequent terms in the series.
        /// </param>
        public Series(T a, Func<T, T> func) : this(new[] { a }, t => func(t[0]))
        {
            if (func == null) throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new <see cref="Series{T}"/>.
        /// </summary>
        /// <param name="a">The first term in the series.</param>
        /// <param name="b">The second term in the series.</param>
        /// <param name="func">
        /// The function used to calculate subsequent terms in the series.
        /// </param>
        public Series(T a, T b, Func<T, T, T> func) : this(new[] { a, b }, t => func(t[0], t[1]))
        {
            if (func == null) throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new <see cref="Series{T}"/>.
        /// </summary>
        /// <param name="a">The first term in the series.</param>
        /// <param name="b">The second term in the series.</param>
        /// <param name="c">The third term in the series.</param>
        /// <param name="func">
        /// The function used to calculate subsequent terms in the series.
        /// </param>
        public Series(T a, T b, T c, Func<T, T, T, T> func)
            : this(new[] { a, b, c }, t => func(t[0], t[1], t[2]))
        {
            if (func == null) throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new <see cref="Series{T}"/>.
        /// </summary>
        /// <param name="a">The first term in the series.</param>
        /// <param name="b">The second term in the series.</param>
        /// <param name="c">The third term in the series.</param>
        /// <param name="d">The fourth term in the series.</param>
        /// <param name="func">
        /// The function used to calculate subsequent terms in the series.
        /// </param>
        public Series(T a, T b, T c, T d, Func<T, T, T, T, T> func)
            : this(new[] { a, b, c, d }, t => func(t[0], t[1], t[2], t[3]))
        {
            if (func == null) throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new <see cref="Series{T}"/>.
        /// </summary>
        /// <param name="a">The first term in the series.</param>
        /// <param name="b">The second term in the series.</param>
        /// <param name="c">The third term in the series.</param>
        /// <param name="d">The fourth term in the series.</param>
        /// <param name="e">The fifth term in the series.</param>
        /// <param name="func">
        /// The function used to calculate subsequent terms in the series.
        /// </param>
        public Series(T a, T b, T c, T d, T e, Func<T, T, T, T, T, T> func)
            : this(new[] { a, b, c, d, e }, t => func(t[0], t[1], t[2], t[3], t[4]))
        {
            if (func == null) throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Gets the next term in the series.
        /// </summary>
        /// <returns>The next term in the series.</returns>
        public T Next()
        {
            var useFunction = _pendingArguments.Count == _initialValues.Length;
            var term =
                useFunction ?
                 _nextValue(_pendingArguments.ToArray()) : _initialValues[_pendingArguments.Count];
            _pendingArguments.Enqueue(term);
            if (useFunction)
            {
                _pendingArguments.Dequeue();
            }
            return term;
        }

        /// <summary>
        /// Resets the series.
        /// </summary>
        public void Reset()
        {
            while (_pendingArguments.Count > 0)
            {
                _pendingArguments.Dequeue();
            }
        }
    }
}
