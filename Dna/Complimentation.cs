using System;
using System.Linq;

namespace Bioinformatics
{
   public static class Complimentation
   {
      public static Nucleotid Complimentary(Nucleotid nucleotid)
      {
         switch (nucleotid)
         {
            case Nucleotid.A:
               return Nucleotid.T;
            case Nucleotid.G:
               return Nucleotid.C;
            case Nucleotid.C:
               return Nucleotid.G;
            case Nucleotid.T:
               return Nucleotid.A;
            default:
               throw new ArgumentOutOfRangeException("nucleotid");
         }
      }

      public static Dna Complimentary(this Dna dna)
      {
         return new Dna(dna.Reverse().Select(Complimentary).ToArray());
      }
   }
}
