using System;
using System.Collections.Generic;
using System.Text;

namespace IslandOfMisfitTypes.Collections
{
    /// <summary>
    /// Generates a series of values using one or more initial values then calculating the
    /// subsequent values using a supplied function.
    /// </summary>
    /// <remarks>
    /// This type is not threadsafe.
    /// </remarks>
    public class Series<T>
    {
        private readonly T[] _initialValues;
        private readonly Queue<T> _pendingArguments;
        private Func<T[], T> _nextValue;

        /// <summary>
        /// Initializes a new <see cref="Series{T}"/>.
        /// </summary>
        /// <param name="a">The first term in the series.</param>
        /// <param name="func">
        /// The function used to calculate subsequent terms in the series.
        /// </param>
        public Series(T a, Func<T, T> func)
        {
            _initialValues = new[] { a };
            if (func == null) throw new ArgumentNullException(nameof(func));
            _nextValue = v => func(v[0]);
            _pendingArguments = new Queue<T>(_initialValues.Length);
        }

        /// <summary>
        /// Initializes a new <see cref="Series{T}"/>.
        /// </summary>
        /// <param name="a">The first term in the series.</param>
        /// <param name="b">The second term in the series.</param>
        /// <param name="func">
        /// The function used to calculate subsequent terms in the series.
        /// </param>
        public Series(T a, T b, Func<T,T,T> func)
        {
            _initialValues = new[] { a, b };
            if (func == null) throw new ArgumentNullException(nameof(func));
            _nextValue = v => func(v[0], v[1]);
            _pendingArguments = new Queue<T>(_initialValues.Length);
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
        {
            _initialValues = new[] { a, b };
            if (func == null) throw new ArgumentNullException(nameof(func));
            _nextValue = v => func(v[0], v[1], v[2]);
            _pendingArguments = new Queue<T>(_initialValues.Length);
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
        {
            _initialValues = new[] { a, b };
            if (func == null) throw new ArgumentNullException(nameof(func));
            _nextValue = v => func(v[0], v[1], v[2], v[3]);
            _pendingArguments = new Queue<T>(_initialValues.Length);
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
        {
            _initialValues = new[] { a, b };
            if (func == null) throw new ArgumentNullException(nameof(func));
            _nextValue = v => func(v[0], v[1], v[2], v[3], v[4]);
            _pendingArguments = new Queue<T>(_initialValues.Length);
        }

        /// <summary>
        /// Initializes a new <see cref="Series{T}"/>.
        /// </summary>
        /// <param name="a">The first term in the series.</param>
        /// <param name="b">The second term in the series.</param>
        /// <param name="c">The third term in the series.</param>
        /// <param name="d">The fourth term in the series.</param>
        /// <param name="e">The fifth term in the series.</param>
        /// <param name="f">The sixth term in the series.</param>
        /// <param name="func">
        /// The function used to calculate subsequent terms in the series.
        /// </param>
        public Series(T a, T b, T c, T d, T e, T f, Func<T, T, T, T, T, T, T> func)
        {
            _initialValues = new[] { a, b };
            if (func == null) throw new ArgumentNullException(nameof(func));
            _nextValue = v => func(v[0], v[1], v[2], v[3], v[4], v[5]);
            _pendingArguments = new Queue<T>(_initialValues.Length);
        }

        /// <summary>
        /// Initializes a new <see cref="Series{T}"/>.
        /// </summary>
        /// <param name="a">The first term in the series.</param>
        /// <param name="b">The second term in the series.</param>
        /// <param name="c">The third term in the series.</param>
        /// <param name="d">The fourth term in the series.</param>
        /// <param name="e">The fifth term in the series.</param>
        /// <param name="f">The sixth term in the series.</param>
        /// <param name="g">The seventh term in the series.</param>
        /// <param name="func">
        /// The function used to calculate subsequent terms in the series.
        /// </param>
        public Series(T a, T b, T c, T d, T e, T f, T g, Func<T, T, T, T, T, T, T, T> func)
        {
            _initialValues = new[] { a, b };
            if (func == null) throw new ArgumentNullException(nameof(func));
            _nextValue = v => func(v[0], v[1], v[2], v[3], v[4], v[5], v[6]);
            _pendingArguments = new Queue<T>(_initialValues.Length);
        }

        /// <summary>
        /// Initializes a new <see cref="Series{T}"/>.
        /// </summary>
        /// <param name="a">The first term in the series.</param>
        /// <param name="b">The second term in the series.</param>
        /// <param name="c">The third term in the series.</param>
        /// <param name="d">The fourth term in the series.</param>
        /// <param name="e">The fifth term in the series.</param>
        /// <param name="f">The sixth term in the series.</param>
        /// <param name="g">The seventh term in the series.</param>
        /// <param name="h">The eighth term in the series.</param>
        /// <param name="func">
        /// The function used to calculate subsequent terms in the series.
        /// </param>
        public Series(T a, T b, T c, T d, T e, T f, T g, T h, Func<T, T, T, T, T, T, T, T, T> func)
        {
            _initialValues = new[] { a, b };
            if (func == null) throw new ArgumentNullException(nameof(func));
            _nextValue = v => func(v[0], v[1], v[2], v[3], v[4], v[5], v[6], v[7]);
            _pendingArguments = new Queue<T>(_initialValues.Length);
        }

        /// <summary>
        /// Initializes a new <see cref="Series{T}"/>.
        /// </summary>
        /// <param name="a">The first term in the series.</param>
        /// <param name="b">The second term in the series.</param>
        /// <param name="c">The third term in the series.</param>
        /// <param name="d">The fourth term in the series.</param>
        /// <param name="e">The fifth term in the series.</param>
        /// <param name="f">The sixth term in the series.</param>
        /// <param name="g">The seventh term in the series.</param>
        /// <param name="h">The eighth term in the series.</param>
        /// <param name="i">The ninth term in the series.</param>
        /// <param name="func">
        /// The function used to calculate subsequent terms in the series.
        /// </param>
        public Series(
            T a, T b, T c, T d, T e, T f, T g, T h, T i, Func<T, T, T, T, T, T, T, T, T, T> func)
        {
            _initialValues = new[] { a, b };
            if (func == null) throw new ArgumentNullException(nameof(func));
            _nextValue = v => func(v[0], v[1], v[2], v[3], v[4], v[5], v[6], v[7], v[8]);
            _pendingArguments = new Queue<T>(_initialValues.Length);
        }

        /// <summary>
        /// Initializes a new <see cref="Series{T}"/>.
        /// </summary>
        /// <param name="a">The first term in the series.</param>
        /// <param name="b">The second term in the series.</param>
        /// <param name="c">The third term in the series.</param>
        /// <param name="d">The fourth term in the series.</param>
        /// <param name="e">The fifth term in the series.</param>
        /// <param name="f">The sixth term in the series.</param>
        /// <param name="g">The seventh term in the series.</param>
        /// <param name="h">The eighth term in the series.</param>
        /// <param name="i">The ninth term in the series.</param>
        /// <param name="j">The tenth term in the series.</param>
        /// <param name="func">
        /// The function used to calculate subsequent terms in the series.
        /// </param>
        public Series(
            T a, T b, T c, T d, T e, T f, T g, T h, T i, T j, 
            Func<T, T, T, T, T, T, T, T, T, T, T> func)
        {
            _initialValues = new[] { a, b };
            if (func == null) throw new ArgumentNullException(nameof(func));
            _nextValue = v => func(v[0], v[1], v[2], v[3], v[4], v[5], v[6], v[7], v[8], v[9]);
            _pendingArguments = new Queue<T>(_initialValues.Length);
        }

        /// <summary>
        /// Initializes a new <see cref="Series{T}"/>.
        /// </summary>
        /// <param name="a">The first term in the series.</param>
        /// <param name="b">The second term in the series.</param>
        /// <param name="c">The third term in the series.</param>
        /// <param name="d">The fourth term in the series.</param>
        /// <param name="e">The fifth term in the series.</param>
        /// <param name="f">The sixth term in the series.</param>
        /// <param name="g">The seventh term in the series.</param>
        /// <param name="h">The eighth term in the series.</param>
        /// <param name="i">The ninth term in the series.</param>
        /// <param name="j">The tenth term in the series.</param>
        /// <param name="k">The eleventh term in the series.</param>
        /// <param name="func">
        /// The function used to calculate subsequent terms in the series.
        /// </param>
        public Series(
            T a, T b, T c, T d, T e, T f, T g, T h, T i, T j, T k,
            Func<T, T, T, T, T, T, T, T, T, T, T, T> func)
        {
            _initialValues = new[] { a, b };
            if (func == null) throw new ArgumentNullException(nameof(func));
            _nextValue = 
                v => func(v[0], v[1], v[2], v[3], v[4], v[5], v[6], v[7], v[8], v[9], v[10]);
            _pendingArguments = new Queue<T>(_initialValues.Length);
        }

        /// <summary>
        /// Initializes a new <see cref="Series{T}"/>.
        /// </summary>
        /// <param name="a">The first term in the series.</param>
        /// <param name="b">The second term in the series.</param>
        /// <param name="c">The third term in the series.</param>
        /// <param name="d">The fourth term in the series.</param>
        /// <param name="e">The fifth term in the series.</param>
        /// <param name="f">The sixth term in the series.</param>
        /// <param name="g">The seventh term in the series.</param>
        /// <param name="h">The eighth term in the series.</param>
        /// <param name="i">The ninth term in the series.</param>
        /// <param name="j">The tenth term in the series.</param>
        /// <param name="k">The eleventh term in the series.</param>
        /// <param name="l">The twelfth term in the series.</param>
        /// <param name="func">
        /// The function used to calculate subsequent terms in the series.
        /// </param>
        public Series(
            T a, T b, T c, T d, T e, T f, T g, T h, T i, T j, T k, T l,
            Func<T, T, T, T, T, T, T, T, T, T, T, T, T> func)
        {
            _initialValues = new[] { a, b };
            if (func == null) throw new ArgumentNullException(nameof(func));
            _nextValue =
                v => func(v[0], v[1], v[2], v[3], v[4], v[5], v[6], v[7], v[8], v[9], v[10], v[11]);
            _pendingArguments = new Queue<T>(_initialValues.Length);
        }

        /// <summary>
        /// Initializes a new <see cref="Series{T}"/>.
        /// </summary>
        /// <param name="a">The first term in the series.</param>
        /// <param name="b">The second term in the series.</param>
        /// <param name="c">The third term in the series.</param>
        /// <param name="d">The fourth term in the series.</param>
        /// <param name="e">The fifth term in the series.</param>
        /// <param name="f">The sixth term in the series.</param>
        /// <param name="g">The seventh term in the series.</param>
        /// <param name="h">The eighth term in the series.</param>
        /// <param name="i">The ninth term in the series.</param>
        /// <param name="j">The tenth term in the series.</param>
        /// <param name="k">The eleventh term in the series.</param>
        /// <param name="l">The twelfth term in the series.</param>
        /// <param name="m">The thirteenth term in the series.</param>
        /// <param name="func">
        /// The function used to calculate subsequent terms in the series.
        /// </param>
        public Series(
            T a, T b, T c, T d, T e, T f, T g, T h, T i, T j, T k, T l, T m,
            Func<T, T, T, T, T, T, T, T, T, T, T, T, T, T> func)
        {
            _initialValues = new[] { a, b };
            if (func == null) throw new ArgumentNullException(nameof(func));
            _nextValue =
                v => func(v[0], v[1], v[2], v[3], v[4], v[5], v[6], v[7], v[8], v[9], v[10], v[11],
                    v[12]);
            _pendingArguments = new Queue<T>(_initialValues.Length);
        }

        /// <summary>
        /// Initializes a new <see cref="Series{T}"/>.
        /// </summary>
        /// <param name="a">The first term in the series.</param>
        /// <param name="b">The second term in the series.</param>
        /// <param name="c">The third term in the series.</param>
        /// <param name="d">The fourth term in the series.</param>
        /// <param name="e">The fifth term in the series.</param>
        /// <param name="f">The sixth term in the series.</param>
        /// <param name="g">The seventh term in the series.</param>
        /// <param name="h">The eighth term in the series.</param>
        /// <param name="i">The ninth term in the series.</param>
        /// <param name="j">The tenth term in the series.</param>
        /// <param name="k">The eleventh term in the series.</param>
        /// <param name="l">The twelfth term in the series.</param>
        /// <param name="m">The thirteenth term in the series.</param>
        /// <param name="n">The fourteenth term in the series.</param>
        /// <param name="func">
        /// The function used to calculate subsequent terms in the series.
        /// </param>
        public Series(
            T a, T b, T c, T d, T e, T f, T g, T h, T i, T j, T k, T l, T m, T n,
            Func<T, T, T, T, T, T, T, T, T, T, T, T, T, T, T> func)
        {
            _initialValues = new[] { a, b };
            if (func == null) throw new ArgumentNullException(nameof(func));
            _nextValue =
                v => func(v[0], v[1], v[2], v[3], v[4], v[5], v[6], v[7], v[8], v[9], v[10], v[11],
                    v[12], v[13]);
            _pendingArguments = new Queue<T>(_initialValues.Length);
        }

        /// <summary>
        /// Initializes a new <see cref="Series{T}"/>.
        /// </summary>
        /// <param name="a">The first term in the series.</param>
        /// <param name="b">The second term in the series.</param>
        /// <param name="c">The third term in the series.</param>
        /// <param name="d">The fourth term in the series.</param>
        /// <param name="e">The fifth term in the series.</param>
        /// <param name="f">The sixth term in the series.</param>
        /// <param name="g">The seventh term in the series.</param>
        /// <param name="h">The eighth term in the series.</param>
        /// <param name="i">The ninth term in the series.</param>
        /// <param name="j">The tenth term in the series.</param>
        /// <param name="k">The eleventh term in the series.</param>
        /// <param name="l">The twelfth term in the series.</param>
        /// <param name="m">The thirteenth term in the series.</param>
        /// <param name="n">The fourteenth term in the series.</param>
        /// <param name="o">The fifteenth term in the series.</param>
        /// <param name="func">
        /// The function used to calculate subsequent terms in the series.
        /// </param>
        public Series(
            T a, T b, T c, T d, T e, T f, T g, T h, T i, T j, T k, T l, T m, T n, T o,
            Func<T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T> func)
        {
            _initialValues = new[] { a, b };
            if (func == null) throw new ArgumentNullException(nameof(func));
            _nextValue =
                v => func(v[0], v[1], v[2], v[3], v[4], v[5], v[6], v[7], v[8], v[9], v[10], v[11],
                    v[12], v[13], v[14]);
            _pendingArguments = new Queue<T>(_initialValues.Length);
        }

        /// <summary>
        /// Initializes a new <see cref="Series{T}"/>.
        /// </summary>
        /// <param name="a">The first term in the series.</param>
        /// <param name="b">The second term in the series.</param>
        /// <param name="c">The third term in the series.</param>
        /// <param name="d">The fourth term in the series.</param>
        /// <param name="e">The fifth term in the series.</param>
        /// <param name="f">The sixth term in the series.</param>
        /// <param name="g">The seventh term in the series.</param>
        /// <param name="h">The eighth term in the series.</param>
        /// <param name="i">The ninth term in the series.</param>
        /// <param name="j">The tenth term in the series.</param>
        /// <param name="k">The eleventh term in the series.</param>
        /// <param name="l">The twelfth term in the series.</param>
        /// <param name="m">The thirteenth term in the series.</param>
        /// <param name="n">The fourteenth term in the series.</param>
        /// <param name="o">The fifteenth term in the series.</param>
        /// <param name="p">The sixteenth term in the series.</param>
        /// <param name="func">
        /// The function used to calculate subsequent terms in the series.
        /// </param>
        public Series(
            T a, T b, T c, T d, T e, T f, T g, T h, T i, T j, T k, T l, T m, T n, T o, T p,
            Func<T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T> func)
        {
            _initialValues = new[] { a, b };
            if (func == null) throw new ArgumentNullException(nameof(func));
            _nextValue = 
                v => func(v[0], v[1], v[2], v[3], v[4], v[5], v[6], v[7], v[8], v[9], v[10], v[11],
                    v[12], v[13], v[14], v[15]);
            _pendingArguments = new Queue<T>(_initialValues.Length);
        }

        /// <summary>
        /// Creates a new series that begins with <paramref name="initialValues"/> then calculates
        /// subsequent values using <paramref name="f"/>.
        /// </summary>
        /// <param name="initialValues">
        /// The first values, in order, to return from the series.
        /// </param>
        /// <param name="f">The function to calculate subsequent values from.</param>
        /// <remarks>
        /// The array of <typeparamref name="T"/> passed to <paramref name="f"/> will always be the
        /// same size as <paramref name="initialValues"/>. If <paramref name="f"/> attempts to
        /// access elements that are ouf of range then an exception will be thrown during
        /// enumeration. The non-array constructers are preferred as they provide index safety.
        /// </remarks>
        public Series(T[] initialValues, Func<T[], T> f)
        {
            _initialValues = initialValues ?? throw new ArgumentNullException(nameof(initialValues));
            _nextValue = f ?? throw new ArgumentNullException(nameof(f));
        }

        /// <summary>
        /// Gets the next term in the series.
        /// </summary>
        /// <returns>The next term in the series.</returns>
        public T Next()
        {
            T term;

            if (_pendingArguments.Count < _initialValues.Length)
            {
                term = _initialValues[_pendingArguments.Count];
            }
            else
            {
                term = _nextValue(_pendingArguments.ToArray());
                _pendingArguments.Dequeue();
            }
            _pendingArguments.Enqueue(term);
            return term;
        }
    }
}
