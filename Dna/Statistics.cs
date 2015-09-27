using System.Collections.Generic;

namespace Bioinformatics
{
   public static class Statistics
   {
      public static int[] Skew(Dna dna)
      {
         var skew = new int[dna.Length+1];
         int g = 0;
         int c = 0;
         skew[0] = 0;
         for (uint i = 1; i < dna.Length+1; i++)
         {
            var nuc = dna[i-1];
            if (nuc == Nucleotid.G)
            {
               g++;
            }
            else if (nuc == Nucleotid.C)
            {
               c++;
            }
            skew[i] = g - c;
         }
         return skew;
      }

      public static uint[] GetSkewMin(Dna dna)
      {
         var minimums = new List<uint>();
         int g = 0;
         int c = 0;
         int min = int.MaxValue;
         for (uint i = 1; i < dna.Length + 1; i++)
         {
            var nuc = dna[i - 1];
            if (nuc == Nucleotid.G)
            {
               g++;
            }
            else if (nuc == Nucleotid.C)
            {
               c++;
            }
            var skew = g - c;
            if (skew < min)
            {
               min = skew;
               minimums = new List<uint>();
            }
            if (skew == min)
            {
               minimums.Add(i);
            }
         }
         return minimums.ToArray();
      }
   }
}
