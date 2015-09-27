using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public static partial class Graph
    {
        #region DFS
        public static IPath<TV, TE> DepthFirstSearch<TV, TE>(this IGraph<TV, TE> g, TV start) where TV : IEquatable<TV>
        {
            return new DepthFirstSearchPath<TV, TE>(g, start);
        }

        private class DepthFirstSearchPath<TV, TE> : IPath<TV, TE> where TV : IEquatable<TV>
        { 

            private readonly IDictionary<TV, KeyValuePair<TV, TE>> _edgeTo = new Dictionary<TV, KeyValuePair<TV, TE>>();

            private readonly TV _start;

            public DepthFirstSearchPath(IGraph<TV, TE> graph, TV start)
            {
                _start = start;
                var marked = new HashSet<TV>();
                var stack = new Stack<TV>();
                stack.Push(start);
                while (stack.Count != 0)
                {
                    var v = stack.Pop();
                    marked.Add(v);
                    foreach (var adj in graph.Adjasent(v))
                    {
                        if (!marked.Contains(adj.Key))
                        {
                            stack.Push(adj.Key);
                            _edgeTo[adj.Key] = new KeyValuePair<TV, TE>(v, adj.Value);
                        }
                    }
                }
            }

            public bool HasPath(TV v)
            {
                return _edgeTo.ContainsKey(v);
            }

            public IEnumerable<KeyValuePair<TV, TE>> PathTo(TV v)
            {
                return reversePath(v).Reverse();
            }

            private IEnumerable<KeyValuePair<TV, TE>> reversePath(TV v)
            {
                if (!HasPath(v))
                    yield break;
                var t = _edgeTo[v];
                while (!Equals(t.Key, _start))
                {
                    yield return t;
                    t = _edgeTo[t.Key];
                }
            }
        }
        #endregion
    }
}
