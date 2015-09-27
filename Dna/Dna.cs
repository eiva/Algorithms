using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Bioinformatics
{
   /// <summary>
   /// Base class for Dna representation.
   /// </summary>
   public sealed class Dna : Sequence<Nucleotid>
   {
      public Dna(string text) : base(text)
      {
      }

      public Dna(Nucleotid[] nucleotids) : base(nucleotids)
      {
      }

      public Dna(IEnumerable<Nucleotid> nucleotids) : base(nucleotids)
      {
      }

      protected override Sequence<Nucleotid> Create(Nucleotid[] sequence)
      {
         return new Dna(sequence);
      }

      protected override Nucleotid Factory(byte b)
      {
         return (Nucleotid) b;
      }

      protected override Nucleotid Factory(char c)
      {
         return Convert.FromChar(c);
      }

      protected override char ToChar(Nucleotid t)
      {
         return Convert.ToChar(t);
      }

      [DebuggerStepThrough]
      public bool Equals(Dna dna)
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
            if (_nucleotides[i] != dna._nucleotides[i])
            {
               return false;
            }
         }
         return true;
      }

      [DebuggerStepThrough]
      public override bool Equals(object obj)
      {
         var dna = obj as Dna;
         if (dna == null)
         {
            return false;
         }
         return Equals(dna);
      }

      public object Clone()
      {
         return new Dna(_nucleotides);
      }

      public static Dna LoadFromTextFile(string fName)
      {
         var text = File.ReadAllText(fName);
         text = text.Trim();
         return new Dna(text);
      }

      public static Dna operator +(Nucleotid n, Dna d)
      {
         var sequence = new Nucleotid[d.Length + 1];
         for (int i = 0; i < d.Length; i++)
         {
            sequence[1 + i] = d._nucleotides[i];
         }
         sequence[0] = n;
         return new Dna(sequence);
      }

      public bool Contains(Dna pattern)
      {
         return ContainsImpl(pattern);
      }

      public Dna Replace(uint position, Nucleotid nucleotid)
      {
         return (Dna)ReplaceImpl(position, nucleotid);
      }

      [DebuggerStepThrough]
      public Dna Substring(uint start, uint length)
      {
         return (Dna)SubstringImpl(start, length);
      }
   }
}
