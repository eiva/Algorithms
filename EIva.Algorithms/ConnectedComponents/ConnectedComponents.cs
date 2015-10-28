using System;
using System.Collections.Generic;
using Graph;

namespace EIva.Algorithms.ConnectedComponents
{
    /// <summary>
    ///     Simplest implementation of building connected components using DFS and Hash.
    /// </summary>
    /// <remarks>
    ///     Build takes O(V+E).
    ///     IsConnected takes O(1).
    /// </remarks>
    public sealed class ConnectedComponents<TV, TE> : IConnectedComponents<TV>
        where TV : IEquatable<TV>
    {
        private readonly IDictionary<TV, int> _components = new Dictionary<TV, int>();

        public ConnectedComponents(IGraph<TV, TE> graph)
        {
            Count = 0;
            foreach (var vertex in graph.Vertexes)
            {
                if (!_components.ContainsKey(vertex))
                {
                    dfs(vertex, graph);
                    ++Count;
                }
            }
        }

        public bool IsConnected(TV v1, TV v2)
        {
            var c1 = _components[v1];
            var c2 = _components[v2];
            return c1 == c2;
        }

        public int ComponentId(TV v)
        {
            return _components[v];
        }

        public int Count { get; }

        private void dfs(TV vertex, IGraph<TV, TE> graph)
        {
            _components[vertex] = Count;
            foreach (var adj in graph.Adjasent(vertex))
            {
                if (!_components.ContainsKey(adj.Key))
                {
                    dfs(adj.Key, graph);
                }
            }
        }
    }
}