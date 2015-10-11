using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public static partial class Graph
    {
        #region BFS

        public static IPath<TV, TE> BreadthFirstSearch<TV, TE>(this IGraph<TV, TE> graph, TV start) where TV : IEquatable<TV>
        {
            return new BreadthFirstPath<TV, TE>(graph, start);
        }

        private class BreadthFirstPath<TV, TE> : IPath<TV, TE> where TV : IEquatable<TV>
        {
            private readonly TV _start;
            private readonly IDictionary<TV, KeyValuePair<TV, TE>> _edge = new Dictionary<TV, KeyValuePair<TV, TE>>();
            public BreadthFirstPath(IGraph<TV, TE> graph, TV start)
            {
                _start = start;

                var queue = new Queue<TV>();
                queue.Enqueue(start);

                while (queue.Count != 0)
                {
                    var e = queue.Dequeue();
                    foreach (var pair in graph.Adjasent(e))
                    {
                        if (!_edge.ContainsKey(pair.Key))
                        {
                            queue.Enqueue(pair.Key);
                            _edge.Add(pair.Key, new KeyValuePair<TV, TE>(e, pair.Value));
                        }
                    }
                }
            }
            public bool HasPath(TV v)
            {
                return _edge.ContainsKey(v);
            }

            public IEnumerable<KeyValuePair<TV, TE>> PathTo(TV v)
            {
                return reversePath(v).Reverse();
            }

            private IEnumerable<KeyValuePair<TV, TE>> reversePath(TV v)
            {
                if (!HasPath(v))
                    yield break;
                var t = _edge[v];
                while (!Equals(t.Key, _start))
                {
                    yield return t;
                    t = _edge[t.Key];
                }
            }
        }
        #endregion
    }
}
