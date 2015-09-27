using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Bioinformatics
{
   [DebuggerStepThrough]
   internal static class Convert
   {
      // A-0 G-1 C-2 T-3
      public static char ToChar(Nucleotid nucleotid)
      {
         switch (nucleotid)
         {
            case Nucleotid.A:
               return 'A';
            case Nucleotid.G:
               return 'G';
            case Nucleotid.C:
               return 'C';
            case Nucleotid.T:
               return 'T';
            default:
               throw new ArgumentOutOfRangeException("nucleotid");
         }
      }

      public static char ToChar(NucleotidRna nucleotid)
      {
         switch (nucleotid)
         {
            case NucleotidRna.A:
               return 'A';
            case NucleotidRna.G:
               return 'G';
            case NucleotidRna.C:
               return 'C';
            case NucleotidRna.U:
               return 'U';
            default:
               throw new ArgumentOutOfRangeException("nucleotid");
         }
      }

      public static Nucleotid FromChar(char nucleotide)
      {
         if (nucleotide == 'A' || nucleotide == 'a')
            return Nucleotid.A;
         if (nucleotide == 'G' || nucleotide == 'g')
            return Nucleotid.G;
         if (nucleotide == 'C' || nucleotide == 'c')
            return Nucleotid.C;
         if (nucleotide == 'T' || nucleotide == 't')
            return Nucleotid.T;
         throw new ApplicationException(String.Format("Wrong nucleotide:'{0}'", nucleotide));
      }

      public static NucleotidRna RnaFromChar(char nucleotide)
      {
         if (nucleotide == 'A' || nucleotide == 'a')
            return NucleotidRna.A;
         if (nucleotide == 'G' || nucleotide == 'g')
            return NucleotidRna.G;
         if (nucleotide == 'C' || nucleotide == 'c')
            return NucleotidRna.C;
         if (nucleotide == 'U' || nucleotide == 'u')
            return NucleotidRna.U;
         throw new ApplicationException(String.Format("Wrong nucleotide:'{0}'", nucleotide));
      }

      public static AminoAcid AminoAcidFromChar(char aminoAcid)
      {
         switch (aminoAcid)
         {
            case 'G':
               return AminoAcid.G;
            case 'A':
               return AminoAcid.A;
            case 'V':
               return AminoAcid.V;
            case 'L':
               return AminoAcid.L;
            case 'I':
               return AminoAcid.I;
            case 'P':
               return AminoAcid.P;
            case 'F':
               return AminoAcid.F;
            case 'Y':
               return AminoAcid.Y;
            case 'W':
               return AminoAcid.W;
            case 'S':
               return AminoAcid.S;
            case 'T':
               return AminoAcid.T;
            case 'D':
               return AminoAcid.D;
            case 'E':
               return AminoAcid.E;
            case 'N':
               return AminoAcid.N;
            case 'Q':
               return AminoAcid.Q;
            case 'C':
               return AminoAcid.C;
            case 'M':
               return AminoAcid.M;
            case 'H':
               return AminoAcid.H;
            case 'K':
               return AminoAcid.K;
            case 'R':
               return AminoAcid.R;
            default:
               throw new ArgumentOutOfRangeException("aminoAcid");
         }
      }

      public static char ToChar(AminoAcid aminoAcid)
      {
         switch (aminoAcid)
         {
            case AminoAcid.G:
               return 'G';
            case AminoAcid.A:
               return 'A';
            case AminoAcid.V:
               return 'V';
            case AminoAcid.L:
               return 'L';
            case AminoAcid.I:
               return 'I';
            case AminoAcid.P:
               return 'P';
            case AminoAcid.F:
               return 'F';
            case AminoAcid.Y:
               return 'Y';
            case AminoAcid.W:
               return 'W';
            case AminoAcid.S:
               return 'S';
            case AminoAcid.T:
               return 'T';
            case AminoAcid.D:
               return 'D';
            case AminoAcid.E:
               return 'E';
            case AminoAcid.N:
               return 'N';
            case AminoAcid.Q:
               return 'Q';
            case AminoAcid.C:
               return 'C';
            case AminoAcid.M:
               return 'M';
            case AminoAcid.H:
               return 'H';
            case AminoAcid.K:
               return 'K';
            case AminoAcid.R:
               return 'R';
            case AminoAcid.Stop:
               return '*';
            default:
               throw new ArgumentOutOfRangeException("aminoAcid");
         }
      }

      
   }
}