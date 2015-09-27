using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;


namespace Bioinformatics
{
   public class BKTree
   {
      private Node _root;

      public void Add(Dna word)
      {
         if (_root == null)
         {
            _root = new Node(word);
            return;
         }

         var curNode = _root;

         var dist = LevenshteinDistance(curNode.Word, word);
         while (curNode.ContainsKey(dist))
         {
            if (dist == 0) return;

            curNode = curNode[dist];
            dist = LevenshteinDistance(curNode.Word, word);
         }

         curNode.AddChild(dist, word);
      }

      public IEnumerable<Dna> Search(Dna word, uint d)
      {
         var rtn = new List<Dna>();

         RecursiveSearch(_root, rtn, word, d);

         return rtn;
      }

      private void RecursiveSearch(Node node, List<Dna> rtn, Dna word, uint d)
      {
         var curDist = LevenshteinDistance(node.Word, word);
         var minDist = Math.Max(0,(int)curDist - (int)d);
         var maxDist = curDist + d;

         if (curDist <= d)
            rtn.Add(node.Word);

         foreach (var key in node.Keys.Cast<uint>().Where(key => minDist <= key && key <= maxDist))
         {
            RecursiveSearch(node[key], rtn, word, d);
         }
      }

      public static uint LevenshteinDistance(Dna first, Dna second)
      {
         if (first.Length == 0) return second.Length;
         if (second.Length == 0) return first.Length;

         var lenFirst = first.Length;
         var lenSecond = second.Length;

         var d = new uint[lenFirst + 1, lenSecond + 1];

         for (uint i = 0; i <= lenFirst; i++)
            d[i, 0] = i;

         for (uint i = 0; i <= lenSecond; i++)
            d[0, i] = i;

         for (uint i = 1; i <= lenFirst; i++)
         {
            for (uint j = 1; j <= lenSecond; j++)
            {
               var match = (first[i - 1] == second[j - 1]) ? 0 : 1;

               d[i, j] = (uint)Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + match);
            }
         }

         return d[lenFirst, lenSecond];
      }
   }

   public class Node
   {
      public Dna Word { get; set; }
      public HybridDictionary Children { get; set; }

      public Node() { }

      public Node(Dna word)
      {
         Word = word;
      }

      public Node this[uint key]
      {
         get { return (Node)Children[key]; }
      }

      public ICollection Keys
      {
         get
         {
            if (Children == null)
               return new ArrayList();
            return Children.Keys;
         }
      }

      public bool ContainsKey(uint key)
      {
         return Children != null && Children.Contains(key);
      }

      public void AddChild(uint key, Dna word)
      {
         if (Children == null)
            Children = new HybridDictionary();
         Children[key] = new Node(word);
      }
   }
}
