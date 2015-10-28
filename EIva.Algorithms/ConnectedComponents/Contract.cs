using System;

namespace EIva.Algorithms
{
    /// <summary>
    ///     Allows to detect whether 2 vertices of a graph are connected.
    /// </summary>
    public interface IConnectedComponents<in TV> where TV : IEquatable<TV>
    {
        /// <summary>
        ///     Returns the number of connected components.
        /// </summary>
        /// <value>The count.</value>
        int Count { get; }

        /// <summary>
        ///     Checks that two vertexes are connected.
        /// </summary>
        /// <returns><c>true</c> if vertexes v1 and v2 are connected; otherwise, <c>false</c>.</returns>
        /// <param name="v1">V1.</param>
        /// <param name="v2">V2.</param>
        bool IsConnected(TV v1, TV v2);

        /// <summary>
        ///     Returns the id of component to which vertex is belong.
        /// </summary>
        /// <returns>The component id.</returns>
        /// <param name="v">Vertex.</param>
        int ComponentId(TV v);
    }

    /// <summary>
    ///     Connected components with dynamic connectivities.
    /// </summary>
    public interface IDynamicConnectedComponents<in TV> : IConnectedComponents<TV>
        where TV : IEquatable<TV>
    {
        /// <summary>
        ///     Adds connection between to specified elements.
        /// </summary>
        /// <param name="i">First element.</param>
        /// <param name="j">Second Element.</param>
        void AddConnection(TV i, TV j);

        /// <summary>
        ///     Removes connection between specified elements.
        /// </summary>
        /// <param name="i">First element.</param>
        /// <param name="j">Second Element.</param>
        void RemoveConnection(TV i, TV j);
    }
}