using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Bioinformatics
{
   public static class DnaPattern
   {
      /// <summary>
      /// Counts patterns match witin sequence.
      /// </summary>
      /// <param name="dna">Dna.</param>
      /// <param name="pattern">Pattern fro luckup.</param>
      /// <returns>Counts pattern mutch.</returns>
      public static uint FindMostFrequent(Dna dna, Dna pattern)
      {
         if (pattern.Length > dna.Length)
         {
            return 0;
         }
         uint count = 0;
         uint start = 0;
         uint offset = 0;
         while (start+offset <= dna.Length)
         {
            if (offset >= pattern.Length)
            {
               ++count;
               start += offset;
               offset = 0;
               continue;
            }
            if (start + offset >= dna.Length)
            {
               break;
            }
            if (dna[start + offset] != pattern[offset])
            {
               ++start;
               offset = 0;
               continue;
            }
            ++offset;
         }
         return count;
      }

      /// <summary>
      /// Находит наиболее часто встречаемые ка-меры. 
      /// </summary>
      /// <param name="dna">ДНК для поиска.</param>
      /// <param name="kMer">Длинна ка-мера.</param>
      /// <returns>Список камеров и частота встречаемости.</returns>
      public static Tuple<IEnumerable<Dna>, uint> FindMostFrequent(Dna dna, uint kMer)
      {
         var histogram = new Dictionary<Dna, uint>();

         uint index = 0;

         uint max = 0;

         while (index + kMer <= dna.Length)
         {
            var sub = dna.Substring(index, kMer);

            uint ap;
            if (!histogram.TryGetValue(sub, out ap))
            {
               ap = 0;
            }
            histogram[sub] = ap + 1;
            max = Math.Max(ap + 1, max);
            ++index;
         }

         var frequent = histogram.Where(d => d.Value == max).Select(d=>d.Key);
         return new Tuple<IEnumerable<Dna>, uint>(frequent, max);
      }

      /// <summary>
      /// Поиск наиболее часто встречаемых камеров с учетом возможных ошибок.
      /// </summary>
      public static Tuple<IEnumerable<Dna>, uint> FindMostFrequent(Dna dna, uint kMer, uint error)
      {
         var histogram = BuildHistogram(dna, kMer, error);

         var histogramErr = GetMostFrequent(histogram, kMer, error);
         uint max = histogramErr.Max(e=>e.Value);
         
         var frequent = histogramErr.Where(d => d.Value == max).Select(d => d.Key);
         return new Tuple<IEnumerable<Dna>, uint>(frequent, max);
      }

      public static Tuple<IEnumerable<Dna>, uint> FindMostFrequentWithReverse(Dna dna, uint kMer, uint error)
      {
         var allCombinations = GenerateKMer(kMer).ToArray();
         Console.WriteLine("All generated: {0}", allCombinations.Length);

         var kTree = BuildTree(allCombinations);
         Console.WriteLine("Tree builded");

         var histogram = BuildHistogram(dna, kMer, error);
         Console.WriteLine("Histogram builded: {0}", histogram.Count);

         var result = new Dictionary<Dna, uint>();

         foreach (var histItem in histogram)
         {
            var c = histItem.Value;
            var forwardHist = histItem.Key;
            var reverseHist = forwardHist.Complimentary();

            var forwardSames = kTree.Search(forwardHist, error);
            var reverseSames = kTree.Search(reverseHist, error);
            var sames = forwardSames.Concat(reverseSames).Distinct();

            foreach (var same in sames)
            {
               if (!result.ContainsKey(same))
               {
                  result[same] = c;
               }
               else
               {
                  result[same] += c;
               }
            }
         }

         uint max = result.Max(e => e.Value);

         var frequent = result.Where(d => d.Value == max).Select(d => d.Key);
         return new Tuple<IEnumerable<Dna>, uint>(frequent, max);
      }

      internal static BKTree BuildTree(IEnumerable<Dna> patterns)
      {
         var kTree = new BKTree();

         foreach (var u in patterns)
         {
            kTree.Add(u);
         }

         return kTree;
      }

      
      internal static IDictionary<Dna, uint> GetMostFrequent(IDictionary<Dna, uint> histogram, uint kMer, uint error)
      {
         var kTree = BuildTree(histogram.Keys);

         var histogramErr = new Dictionary<Dna, uint>();
         foreach (var el in histogram.Where(el => el.Key.Length == kMer))
         {
            var similar = kTree.Search(el.Key, error);
            uint den = similar.Where(dna => !dna.Contains(el.Key)).Aggregate(el.Value, (current, dna) => current + histogram[dna]);
            histogramErr[el.Key] = den;
         }

         return histogramErr;
      }

      /*
       * I have 2 functions: 
(1) generating all similar strings (pass in a k-mer and d, it spits out all combo) (ex. GGGG, 1 -> GGGG, GGGA, GGGT, GGGC) 
(2) generate revc (ex. AAAC -> GTTT)
in main:
(a) do a quick frequency analysis of k-mer -> store in dic_1
(b) for each k-mer in frequency analysis, I use function 1 to find all similar k-mers and store/update dic_2 (dic_2[similark] += dic_1[k-mer]) and for each similar k-mer, I use function 2 to find it’s rev-c and update dic_2 as well
(c) loop through dic_2 and find the one with max frequency If you still have no idea… try to find me in Coursera or pass me an email joannetychow[at]yahoo[dot]ca
       */

      internal static IDictionary<Dna, uint> GetMostFrequentReverse(IDictionary<Dna, uint> histogram, uint kMer, uint error)
      {
         var kTree = BuildTree(histogram.Keys);

         var histogramErr = new Dictionary<Dna, uint>();
         foreach (var el in histogram.Where(el => el.Key.Length == kMer))
         {
            // Forward
            var similar = kTree.Search(el.Key, error);
            uint den = similar.Where(dna => !dna.Contains(el.Key)).Aggregate(el.Value, (current, dna) => current + histogram[dna]);

            // Reverse

            var reverse = el.Key.Complimentary();

            if (el.Key.Equals(reverse))
            {
               histogramErr[el.Key] = den;
               continue;
            }

            var similarRev = kTree.Search(reverse, error);

            uint initial;
            if(!histogram.TryGetValue(reverse, out initial))
            {
               initial = 0;
            }
            
            uint denRev = similarRev.Where(dna => !dna.Contains(reverse)).Aggregate(initial, (current, dna) => current + histogram[dna]);

            histogramErr[el.Key] = den + denRev;
         }

         return histogramErr;
      }

      internal static IDictionary<Dna, uint> BuildHistogram(Dna dna, uint kMer, uint error)
      {
         var histogram = new Dictionary<Dna, uint>();

         for (uint i = 0; i <= dna.Length - kMer; i++)
         {
            for (uint j = 0; j <= error; j++)
            {
               if (i + j + kMer > dna.Length)
               {
                  break;
               }
               var sub = dna.Substring(i, kMer + j);

               uint ap;
               if (!histogram.TryGetValue(sub, out ap))
               {
                  ap = 0;
               }
               histogram[sub] = ap + 1;
            }
         }

         return histogram;
      }

      internal static IEnumerable<Dna> GenerateKMer(uint kmer)
      {
         if (kmer == 0)
         {
            yield break;
         }
         if (kmer == 1)
         {
            yield return new Dna("A");
            yield return new Dna("C");
            yield return new Dna("G");
            yield return new Dna("T");
            yield break;
         }
         var postfix = GenerateKMer(kmer - 1);
         foreach (var dna in postfix)
         {
            yield return Nucleotid.A + dna;
            yield return Nucleotid.C + dna;
            yield return Nucleotid.G + dna;
            yield return Nucleotid.T + dna;
         }
      }

      public static IEnumerable<Dna> CountWindowed(Dna dna, uint kMer, uint windowSize, uint t)
      {
         uint index = 0;
         var result = new List<Dna>();
         bool cont = true;
         do
         {
            var start = index;
            if (index >= dna.Length - 1)
            {
               break;
            }
            if (index + windowSize > dna.Length)
            {
               start = dna.Length - windowSize;
               cont = false;
            }
            var window = dna.Substring(start, windowSize);
            var res = FindMostFrequent(window, kMer);
            if (res.Item2 >= t)
            {
               result.AddRange(res.Item1);
            }
            ++index;
         } while (cont);
         return result.Distinct();
      }

      /// <summary>
      /// Поиск всех подпоследовательностей в заданной последовательности.
      /// </summary>
      /// <typeparam name="T">Тип нуклеотида.</typeparam>
      /// <param name="sequence">Последовательность.</param>
      /// <param name="pattern">Подпоследовательность.</param>
      /// <returns>Список позиций подпоследовательности в последовательности.</returns>
      public static IEnumerable<uint> FindAllMatches<T>(Sequence<T> sequence, Sequence<T> pattern) where T:struct
      {
         if (pattern.Length > sequence.Length)
         {
            return new uint[0];
         }
         if (pattern.Length == 0 || sequence.Length == 0)
         {
            return new uint[0];
         }
         uint index = 0;
         uint mutchPosition = 0;
         IList<uint> matches = new List<uint>();
         while (index + pattern.Length <= sequence.Length )
         {
            if (mutchPosition >= pattern.Length)
            {
               matches.Add(index);
               ++index;
               mutchPosition = 0;
               continue;
            }
            if (Equals(sequence[index + mutchPosition], pattern[mutchPosition]))
            {
               ++mutchPosition;
               continue;
            }
            ++index;
            mutchPosition = 0;
         }
         return matches;
      }

      public static IEnumerable<uint> FindAllMatches(Dna dna, Dna pattern, byte mistmatch)
      {
         var res = new List<uint>();
         for (uint i = 0; i <= dna.Length - pattern.Length; ++i)
         {
            var substr = dna.Substring(i, pattern.Length);
            var dist = Distance(substr, pattern);
            if (dist <= mistmatch)
            {
               res.Add(i);
            }
         }
         return res;
      }

      /// <summary>
      /// Find Levinshtain distance between 2 sequence samples.
      /// </summary>
      /// <param name="pattern1"></param>
      /// <param name="pattern2"></param>
      /// <returns></returns>
      internal static byte Distance(Dna pattern1, Dna pattern2)
      {
         if (pattern1.Length != pattern2.Length)
         {
            return byte.MaxValue;
         }
         byte res = 0;
         for (uint i = 0; i < pattern1.Length; i++)
         {
            if (pattern1[i] != pattern2[i])
            {
               ++res;
            }
         }
         return res;
      }
   }
}