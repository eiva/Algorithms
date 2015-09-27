using System;
using System.Collections.Generic;

namespace Bioinformatics
{
   internal class TranslationTree
   {
      private static readonly IDictionary<uint, AminoAcid>  _translationTable = new Dictionary<uint, AminoAcid>();

      private static uint GetHashCode(NucleotidRna _1, NucleotidRna _2, NucleotidRna _3)
      {
         var a = (uint) _1;
         var b = (uint) _2;
         var c = (uint) _3;
         return (a + 1) | ((b + 1) << 8) | ((c + 1) << 16);
      }
      
      static void Add(NucleotidRna _1, NucleotidRna _2, NucleotidRna _3, AminoAcid acid)
      {
         var code = GetHashCode(_1, _2, _3);
         _translationTable.Add(code, acid);
      }

      public static AminoAcid Translate(NucleotidRna _1, NucleotidRna _2, NucleotidRna _3)
      {
         var code = GetHashCode(_1, _2, _3);
         AminoAcid res;
         if (!_translationTable.TryGetValue(code, out res))
         {
            throw new ArgumentOutOfRangeException();
         }
         return res;
      }
      static TranslationTree()
      {
         Add(NucleotidRna.A, NucleotidRna.A, NucleotidRna.A, AminoAcid.K);
         Add(NucleotidRna.A, NucleotidRna.A, NucleotidRna.C, AminoAcid.N);
         Add(NucleotidRna.A, NucleotidRna.A, NucleotidRna.G, AminoAcid.K);
         Add(NucleotidRna.A, NucleotidRna.A, NucleotidRna.U, AminoAcid.N);
         Add(NucleotidRna.A, NucleotidRna.C, NucleotidRna.A, AminoAcid.T);
         Add(NucleotidRna.A, NucleotidRna.C, NucleotidRna.C, AminoAcid.T);
         Add(NucleotidRna.A, NucleotidRna.C, NucleotidRna.G, AminoAcid.T);
         Add(NucleotidRna.A, NucleotidRna.C, NucleotidRna.U, AminoAcid.T);
         Add(NucleotidRna.A, NucleotidRna.G, NucleotidRna.A, AminoAcid.R);
         Add(NucleotidRna.A, NucleotidRna.G, NucleotidRna.C, AminoAcid.S);
         Add(NucleotidRna.A, NucleotidRna.G, NucleotidRna.G, AminoAcid.R);
         Add(NucleotidRna.A, NucleotidRna.G, NucleotidRna.U, AminoAcid.S);
         Add(NucleotidRna.A, NucleotidRna.U, NucleotidRna.A, AminoAcid.I);
         Add(NucleotidRna.A, NucleotidRna.U, NucleotidRna.C, AminoAcid.I);
         Add(NucleotidRna.A, NucleotidRna.U, NucleotidRna.G, AminoAcid.M);
         Add(NucleotidRna.A, NucleotidRna.U, NucleotidRna.U, AminoAcid.I);
         Add(NucleotidRna.C, NucleotidRna.A, NucleotidRna.A, AminoAcid.Q);
         Add(NucleotidRna.C, NucleotidRna.A, NucleotidRna.C, AminoAcid.H);
         Add(NucleotidRna.C, NucleotidRna.A, NucleotidRna.G, AminoAcid.Q);
         Add(NucleotidRna.C, NucleotidRna.A, NucleotidRna.U, AminoAcid.H);
         Add(NucleotidRna.C, NucleotidRna.C, NucleotidRna.A, AminoAcid.P);
         Add(NucleotidRna.C, NucleotidRna.C, NucleotidRna.C, AminoAcid.P);
         Add(NucleotidRna.C, NucleotidRna.C, NucleotidRna.G, AminoAcid.P);
         Add(NucleotidRna.C, NucleotidRna.C, NucleotidRna.U, AminoAcid.P);
         Add(NucleotidRna.C, NucleotidRna.G, NucleotidRna.A, AminoAcid.R);
         Add(NucleotidRna.C, NucleotidRna.G, NucleotidRna.C, AminoAcid.R);
         Add(NucleotidRna.C, NucleotidRna.G, NucleotidRna.G, AminoAcid.R);
         Add(NucleotidRna.C, NucleotidRna.G, NucleotidRna.U, AminoAcid.R);
         Add(NucleotidRna.C, NucleotidRna.U, NucleotidRna.A, AminoAcid.L);
         Add(NucleotidRna.C, NucleotidRna.U, NucleotidRna.C, AminoAcid.L);
         Add(NucleotidRna.C, NucleotidRna.U, NucleotidRna.G, AminoAcid.L);
         Add(NucleotidRna.C, NucleotidRna.U, NucleotidRna.U, AminoAcid.L);
         Add(NucleotidRna.G, NucleotidRna.A, NucleotidRna.A, AminoAcid.E);
         Add(NucleotidRna.G, NucleotidRna.A, NucleotidRna.C, AminoAcid.D);
         Add(NucleotidRna.G, NucleotidRna.A, NucleotidRna.G, AminoAcid.E);
         Add(NucleotidRna.G, NucleotidRna.A, NucleotidRna.U, AminoAcid.D);
         Add(NucleotidRna.G, NucleotidRna.C, NucleotidRna.A, AminoAcid.A);
         Add(NucleotidRna.G, NucleotidRna.C, NucleotidRna.C, AminoAcid.A);
         Add(NucleotidRna.G, NucleotidRna.C, NucleotidRna.G, AminoAcid.A);
         Add(NucleotidRna.G, NucleotidRna.C, NucleotidRna.U, AminoAcid.A);
         Add(NucleotidRna.G, NucleotidRna.G, NucleotidRna.A, AminoAcid.G);
         Add(NucleotidRna.G, NucleotidRna.G, NucleotidRna.C, AminoAcid.G);
         Add(NucleotidRna.G, NucleotidRna.G, NucleotidRna.G, AminoAcid.G);
         Add(NucleotidRna.G, NucleotidRna.G, NucleotidRna.U, AminoAcid.G);
         Add(NucleotidRna.G, NucleotidRna.U, NucleotidRna.A, AminoAcid.V);
         Add(NucleotidRna.G, NucleotidRna.U, NucleotidRna.C, AminoAcid.V);
         Add(NucleotidRna.G, NucleotidRna.U, NucleotidRna.G, AminoAcid.V);
         Add(NucleotidRna.G, NucleotidRna.U, NucleotidRna.U, AminoAcid.V);
         Add(NucleotidRna.U, NucleotidRna.A, NucleotidRna.A, AminoAcid.Stop);
         Add(NucleotidRna.U, NucleotidRna.A, NucleotidRna.C, AminoAcid.Y);
         Add(NucleotidRna.U, NucleotidRna.A, NucleotidRna.G, AminoAcid.Stop);
         Add(NucleotidRna.U, NucleotidRna.A, NucleotidRna.U, AminoAcid.Y);
         Add(NucleotidRna.U, NucleotidRna.C, NucleotidRna.A, AminoAcid.S);
         Add(NucleotidRna.U, NucleotidRna.C, NucleotidRna.C, AminoAcid.S);
         Add(NucleotidRna.U, NucleotidRna.C, NucleotidRna.G, AminoAcid.S);
         Add(NucleotidRna.U, NucleotidRna.C, NucleotidRna.U, AminoAcid.S);
         Add(NucleotidRna.U, NucleotidRna.G, NucleotidRna.A, AminoAcid.Stop);
         Add(NucleotidRna.U, NucleotidRna.G, NucleotidRna.C, AminoAcid.C);
         Add(NucleotidRna.U, NucleotidRna.G, NucleotidRna.G, AminoAcid.W);
         Add(NucleotidRna.U, NucleotidRna.G, NucleotidRna.U, AminoAcid.C);
         Add(NucleotidRna.U, NucleotidRna.U, NucleotidRna.A, AminoAcid.L);
         Add(NucleotidRna.U, NucleotidRna.U, NucleotidRna.C, AminoAcid.F);
         Add(NucleotidRna.U, NucleotidRna.U, NucleotidRna.G, AminoAcid.L);
         Add(NucleotidRna.U, NucleotidRna.U, NucleotidRna.U, AminoAcid.F);
      }
   }
   public static class Transcription
   {
      private static IEnumerable<AminoAcid> transcribe(this IEnumerable<NucleotidRna> rna)
      {
         var enumerator = rna.GetEnumerator();
         enumerator.Reset();
         while (true)
         {
            if (!enumerator.MoveNext())
            {
               yield break;
            }
            var _1 = enumerator.Current;
            if (!enumerator.MoveNext())
            {
               yield break;
            }
            var _2 = enumerator.Current;
            if (!enumerator.MoveNext())
            {
               yield break;
            }
            var _3 = enumerator.Current;
            var res = TranslationTree.Translate(_1, _2, _3);
            if (res == AminoAcid.Stop)
            {
               yield break;
            }
            yield return res;
         }
      }

      private static IEnumerable<AminoAcid> transcribe(this IEnumerable<NucleotidRna> rna, uint offset, bool processStop)
      {
         var enumerator = rna.GetEnumerator();
         enumerator.Reset();
         for (uint i = 0; i < offset && enumerator.MoveNext(); i++){}

         while (true)
         {
            if (!enumerator.MoveNext())
            {
               yield break;
            }
            var _1 = enumerator.Current;
            if (!enumerator.MoveNext())
            {
               yield break;
            }
            var _2 = enumerator.Current;
            if (!enumerator.MoveNext())
            {
               yield break;
            }
            var _3 = enumerator.Current;
            var res = TranslationTree.Translate(_1, _2, _3);
            if (res == AminoAcid.Stop)
            {
               if (!processStop)
               {
                  yield break;
               }
            }
            yield return res;
         }
      }

      /// <summary>
      /// Производит транскрипцию строки РНК в последовательность Аминокислот (Пептид).
      /// </summary>
      /// <param name="rna">РНК строка на вход.</param>
      /// <returns>Пептид.</returns>
      public static Peptide Transcribe(this Rna rna)
      {
         return new Peptide(rna.transcribe());
      }

      /// <summary>
      /// Производит транскрипцию строки РНК в последовательность Аминокислот (Пептид).
      /// </summary>
      /// <param name="rna">РНК строка на вход.</param>
      /// <param name="offset">Смещение отностительно начала РНК цепочки.</param>
      /// <param name="processStop">Если тру - то стоп сигналы попадают в пептид, а последовательность не разрывается.</param>
      /// <returns>Пептид.</returns>
      public static Peptide Transcribe(this Rna rna, uint offset, bool processStop)
      {
         return new Peptide(rna.transcribe(offset, processStop));
      }


   }
}
