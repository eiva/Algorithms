using System;
using System.Collections.Generic;

namespace Bioinformatics
{
   public static class Translation
   {

      private static IEnumerable<NucleotidRna> translate(this IEnumerable<Nucleotid> dna)
      {
         foreach (var nucleotid in dna)
         {
            switch (nucleotid)
            {
               case Nucleotid.A:
                  yield return NucleotidRna.A;
                  break;
               case Nucleotid.G:
                  yield return NucleotidRna.G;
                  break;
               case Nucleotid.C:
                  yield return NucleotidRna.C;
                  break;
               case Nucleotid.T:
                  yield return NucleotidRna.U;
                  break;
               default:
                  throw new ArgumentOutOfRangeException();
            }
         }
      }

      /// <summary>
      /// Транслирует ДНК в РНК.
      /// </summary>
      /// <param name="dna">Вводная ДНК.</param>
      /// <returns>Последовательность РНК нуклеотидов.</returns>
      public static Rna Translate(this Dna dna)
      {
         return new Rna(dna.translate());
      }
   }
}
