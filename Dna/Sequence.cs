using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;


namespace Bioinformatics
{
   public abstract class Sequence<T> : IReadOnlyCollection<T>
      where T : struct
   {
      #region Private members
      
      protected readonly T[] _nucleotides;

      private readonly int _hashCode;

      #endregion

      [Pure]
      protected abstract Sequence<T> Create(T[] sequence);
      
      [Pure]
      protected abstract T Factory(byte b);
      [Pure]
      protected abstract T Factory(char c);

      protected abstract char ToChar(T t);

      [DebuggerStepThrough]
      protected Sequence(string text)
      {
         _nucleotides = new T[text.Length];
         for (int i = 0; i < text.Length; ++i)
         {
            _nucleotides[i] = Factory(text[i]);
         }
         _hashCode = text.GetHashCode();
      }
      
      protected Sequence(T[] nucleotids)
      {
         _nucleotides = nucleotids;
         _hashCode = ToString().GetHashCode();
      }

      protected Sequence(IEnumerable<T> nucleotids)
      {
         _nucleotides = nucleotids.ToArray();
         _hashCode = ToString().GetHashCode();
      }

      [Pure]
      protected Sequence<T> SubstringImpl(uint start, uint length)
      {
         if (start + length > Length)
            throw new ArgumentOutOfRangeException();

         if (length == 0)
            return Create(new T[0]);

         var substring = new T[length];
         for (uint i = start, j = 0; j < length; ++i, ++j)
         {
            substring[j] = _nucleotides[i];
         }

         return Create(substring);
      }

      [DebuggerStepThrough]
      public IEnumerator<T> GetEnumerator()
      {
         return ((IEnumerable<T>) _nucleotides).GetEnumerator();
      }

      [DebuggerStepThrough]
      [Pure]
      public virtual bool Equals(Sequence<T> dna)
      {
         if (dna.Length != Length)
         {
            return false;
         }
         if (dna.GetHashCode() != GetHashCode())
         {
            return false;
         }
         for (int i = 0; i < Length; ++i)
         {
            if (!Equals(_nucleotides[i], dna._nucleotides[i]))
            {
               return false;
            }
         }
         return true;
      }

      [Pure]
      protected Sequence<T> ReplaceImpl(uint position, T nucleotid)
      {
         var n = new T[Length];
         for (int i = 0; i < Length; i++)
         {
            n[i] = _nucleotides[i];
         }
         n[position] = nucleotid;
         return Create(n);
      }

      public uint Length
      {
         [DebuggerStepThrough]
         get { return (uint)_nucleotides.Length; }
      }

      public T this[uint index]
      {
         [DebuggerStepThrough]
         get { return _nucleotides[index]; }
      }

      public T[] Nucleotids
      {
         [DebuggerStepThrough]
         get { return _nucleotides; }
      }

      public override string ToString()
      {
         return string.Concat(_nucleotides.Select(ToChar));
      }

      [DebuggerStepThrough]
      public override int GetHashCode()
      {
         return _hashCode;
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return GetEnumerator();
      }

      protected bool ContainsImpl(Sequence<T> pattern)
      {
         if (pattern.Length > Length)
         {
            return false;
         }
         uint start = 0;
         uint offset = 0;
         while (start + offset <= Length)
         {
            if (offset >= pattern.Length)
            {
               return true;
            }
            if (start + offset >= Length)
            {
               break;
            }
            if (!Equals(this[start + offset], pattern[offset]))
            {
               ++start;
               offset = 0;
               continue;
            }
            ++offset;
         }
         return false;
      }

      protected IEnumerable<T> ConcatImpl(Sequence<T> tail)
      {
         foreach (var nucleotide in _nucleotides)
         {
            yield return nucleotide;
         }
         foreach (var nukleotide in tail)
         {
            yield return nukleotide;
         }
      }

      #region Implementation of IReadOnlyCollection<out Nucleotid>

      public int Count
      {
         [DebuggerStepThrough]
         get { return _nucleotides.Length; }
      }

      #endregion
   }
}
