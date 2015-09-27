using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bioinformatics
{
   public static class MassSpectre
   {
      private static readonly IDictionary<AminoAcid, uint> _mass;
      static MassSpectre()
      {
         _mass = new Dictionary<AminoAcid, uint>();
         _mass[AminoAcid.G] = 57;
         _mass[AminoAcid.A] = 71;
         _mass[AminoAcid.S] = 87;
         _mass[AminoAcid.P] = 97;
         _mass[AminoAcid.V] = 99;
         _mass[AminoAcid.T] = 101;
         _mass[AminoAcid.C] = 103;
         _mass[AminoAcid.I] = 113;
         _mass[AminoAcid.L] = 113;
         _mass[AminoAcid.N] = 114;
         _mass[AminoAcid.D] = 115;
         _mass[AminoAcid.K] = 128;
         _mass[AminoAcid.Q] = 128;
         _mass[AminoAcid.E] = 129;
         _mass[AminoAcid.M] = 131;
         _mass[AminoAcid.H] = 137;
         _mass[AminoAcid.F] = 147;
         _mass[AminoAcid.R] = 156;
         _mass[AminoAcid.Y] = 163;
         _mass[AminoAcid.W] = 186;
      }
      public static uint Mass(this Peptide peptide)
      {
         unchecked
         {
            return (uint)peptide.Where(aminoAcid => aminoAcid != AminoAcid.Stop).Sum(aminoAcid => _mass[aminoAcid]);
         }
      }

      public static IEnumerable<Peptide> BuildSpectre(this Peptide peptide)
      {
         var res = new HashSet<Peptide>();
         var doubledPeptide = peptide.Concat(peptide);
         for (uint l = 1; l < peptide.Length; l++)
         {
            for (uint i = 0; i < peptide.Length; i++)
            {
               var pep = doubledPeptide.Substring(i, l);
               res.Add(pep);
            }
         }
         return res;
      }

      public static IEnumerable<uint> BuildMassSpectre(this Peptide peptide)
      {
         var spectre = BuildSpectre(peptide);

         var masses = spectre.Select(Mass);

         return masses.Concat(new uint[] {0, Mass(peptide)}).OrderBy(m => m);

      }
   }
}
