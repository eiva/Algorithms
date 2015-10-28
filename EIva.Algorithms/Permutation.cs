using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Permutation
{
    public static class Extensions
    {
        public static IEnumerable<T> InsertAt<T>(
            this IEnumerable<T> items, int position, T newItem)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            if (position < 0)
                throw new ArgumentOutOfRangeException(nameof(position));
            return InsertAtIterator(items, position, newItem);
        }

        private static IEnumerable<T> InsertAtIterator<T>(
            this IEnumerable<T> items, int position, T newItem)
        {
            var index = 0;
            var yieldedNew = false;
            foreach (var item in items)
            {
                if (index == position)
                {
                    yield return newItem;
                    yieldedNew = true;
                }
                yield return item;
                index += 1;
            }
            if (index == position)
            {
                yield return newItem;
                yieldedNew = true;
            }
            if (!yieldedNew)
                throw new ArgumentOutOfRangeException(nameof(position));
        }
    }

    internal struct Permutation : IEnumerable<int>
    {
        public static Permutation Empty { get; } = new Permutation(new int[] {});
        private readonly int[] _permutation;

        private Permutation(int[] permutation)
        {
            _permutation = permutation;
        }

        private Permutation(IEnumerable<int> permutation)
            : this(permutation.ToArray())
        {
        }

        private static BigInteger Factorial(int x)
        {
            BigInteger result = 1;
            for (var i = 2; i <= x; ++i)
                result *= i;
            return result;
        }

        public static Permutation NthPermutation(int size, BigInteger index)
        {
            if (index < 0 || index >= Factorial(size))
                throw new ArgumentOutOfRangeException(nameof(index));
            if (size == 0)
                return Empty;
            BigInteger group = index/size;
            var forwards = group%2 != 0;
            var permutation = NthPermutation(size - 1, group);
            var i = (int) (index%size);
            return new Permutation(
                permutation.InsertAt(forwards ? i : size - i - 1, size - 1));
        }

        // Produce a random number between 0 and n!-1
        private static BigInteger RandomFactoradic(int n, Random random)
        {
            BigInteger result = 0;
            BigInteger radix = 1;
            for (var i = 1; i < n; ++i)
            {
                // We need a digit between 0 and i.
                result += radix*random.Next(i + 1);
                radix *= (i + 1);
            }
            return result;
        }

        public static Permutation RandomPermutation(int size, Random random)
        {
            return NthPermutation(size, RandomFactoradic(size, random));
        }

        public int this[int index] => _permutation[index];

        public IEnumerator<int> GetEnumerator()
        {
            return ((IEnumerable<int>) _permutation).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => _permutation.Length;

        public override string ToString()
        {
            return string.Join(",", _permutation);
        }
    }
}