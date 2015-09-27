using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;


namespace Bioinformatics
{
   public sealed class Peptide: Sequence<AminoAcid>
   {
      public Peptide(string text) : base(text)
      {
      }

      public Peptide(AminoAcid[] nucleotids)
         : base(nucleotids)
      {
      }

      public Peptide(IEnumerable<AminoAcid> nucleotids)
         : base(nucleotids)
      {
      }

      public static Peptide FromString(string rna)
      {
         var res = new AminoAcid[rna.Length];
         for (int i = 0; i < rna.Length; i++)
         {
            res[i] = Convert.AminoAcidFromChar(rna[i]);
         }
         return new Peptide(res);
      }

      public static Peptide LoadFromTextFile(string fileName)
      {
         var text = File.ReadAllText(fileName);
         text = text.Trim();
         return FromString(text);
      }

      public static string ToString(IEnumerable<AminoAcid> peptid)
      {
         var sb = new StringBuilder();
         foreach (var aminoAcid in peptid)
         {
            sb.Append(Convert.ToChar(aminoAcid));
         }
         return sb.ToString();
      }

      protected override Sequence<AminoAcid> Create(AminoAcid[] sequence)
      {
         return new Peptide(sequence);
      }

      protected override AminoAcid Factory(byte b)
      {
         return (AminoAcid)b;
      }

      protected override AminoAcid Factory(char c)
      {
         return Convert.AminoAcidFromChar(c);
      }

      protected override char ToChar(AminoAcid t)
      {
         return Convert.ToChar(t);
      }

      public bool Contains(Peptide pattern)
      {
         return ContainsImpl(pattern);
      }

      public Peptide Concat(Peptide tail)
      {
         return new Peptide(ConcatImpl(tail));
      }

      public Peptide Replace(uint position, AminoAcid nucleotid)
      {
         return (Peptide)ReplaceImpl(position, nucleotid);
      }

      [DebuggerStepThrough]
      public Peptide Substring(uint start, uint length)
      {
         return (Peptide)SubstringImpl(start, length);
      }
   }
}
