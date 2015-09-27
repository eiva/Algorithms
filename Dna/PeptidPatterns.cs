using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bioinformatics
{
   public static class PeptidPatterns
   {
      public static IEnumerable<Dna> FindPatterns(Dna input, Peptide peptide)
      {
         var dnaComp = input.Complimentary();
         
         var forward = match(input, peptide);

         var backWard = match(dnaComp, peptide);

         var resultMatches = forward.Concat(backWard.Select(d=>d.Complimentary()));
         
         return resultMatches;
      }

      private static IEnumerable<Dna> match(Dna input, Peptide peptide)
      {
         var resultMatches = new List<Dna>();
         var rna1 = input.Translate();
         for (uint offset = 0; offset < 3; offset++)
         {
            var pep = rna1.Transcribe(offset, true);
            var matches = DnaPattern.FindAllMatches(pep, peptide);
            foreach (uint match in matches)
            {
               var of = match * 3 + offset;
               var transDna = input.Substring(of, 3 * peptide.Length);
               resultMatches.Add(transDna);
            }
         }
         return resultMatches;
      }
   }
}
