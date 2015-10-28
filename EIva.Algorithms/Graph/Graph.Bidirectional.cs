using System;
using System.Collections.Generic;
using EIva.Algorithms.Utils;

namespace Graph
{
    public static partial class Graph
    {
        #region Bidirectional

        private class BidirectionalGraph<TV, TE> : IGraph<TV, TE> where TV : IEquatable<TV>
        {
            private readonly IDictionary<TV, Dictionary<TV, TE>> _graph = new SortedDictionary<TV, Dictionary<TV, TE>>();

            public void AddEdge(TV v, TV w, TE e)
            {
                if (v.Equals(w))
                {
                    _graph.GetOrCreate(w);
                    return;
                }
                _graph.GetOrCreate(v)[w] = e;
                _graph.GetOrCreate(w)[v] = e;
            }

            public IReadOnlyCollection<KeyValuePair<TV, TE>> Adjasent(TV v)
            {
                return _graph[v];
            }

            public IEnumerable<TV> Vertexes
            {
                get { return _graph.Keys; }
            }
        }

        public static IGraph<TV, TE> Bidirectional<TV, TE>() where TV : IEquatable<TV>
        {
            return new BidirectionalGraph<TV, TE>();
        }

        #endregion
    }
}