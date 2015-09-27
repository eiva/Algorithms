using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Bioinformatics
{
   public class Rna : Sequence<NucleotidRna>
   {
      public Rna(string text) : base(text)
      {
      }

      public Rna(NucleotidRna[] nucleotids) : base(nucleotids)
      {
      }

      public Rna(IEnumerable<NucleotidRna> nucleotids)
         : base(nucleotids)
      {
      }

      /// <summary>
      /// Преобразует строкове представление РНК в массив.
      /// </summary>
      public static Rna FromString(string rna)
      {
         var res = new NucleotidRna[rna.Length];
         for (int i = 0; i < rna.Length; i++)
         {
            res[i] = Convert.RnaFromChar(rna[i]);
         }
         return new Rna(res);
      }

      public static Rna LoadFromTextFile(string fileName)
      {
         var text = File.ReadAllText(fileName);
         text = text.Trim();
         return FromString(text);
      }

      /// <summary>
      /// Преобразует аминокислотное представление пептида в строку сокращений.
      /// </summary>
      /// <param name="peptid">Пептид.</param>
      /// <returns>Строкове представление.</returns>
      public static string ToString(IEnumerable<AminoAcid> peptid)
      {
         var sb = new StringBuilder();
         foreach (var aminoAcid in peptid)
         {
            sb.Append(Convert.ToChar(aminoAcid));
         }
         return sb.ToString();
      }

      protected override Sequence<NucleotidRna> Create(NucleotidRna[] sequence)
      {
         return new Rna(sequence);
      }

      protected override NucleotidRna Factory(byte b)
      {
         return (NucleotidRna) b;
      }

      protected override NucleotidRna Factory(char c)
      {
         return Convert.RnaFromChar(c);
      }

      protected override char ToChar(NucleotidRna t)
      {
         return Convert.ToChar(t);
      }

      public bool Contains(Rna pattern)
      {
         return ContainsImpl(pattern);
      }

      public Rna Replace(uint position, NucleotidRna nucleotid)
      {
         return (Rna)ReplaceImpl(position, nucleotid);
      }

      [DebuggerStepThrough]
      public Rna Substring(uint start, uint length)
      {
         return (Rna)SubstringImpl(start, length);
      }
   }
}
