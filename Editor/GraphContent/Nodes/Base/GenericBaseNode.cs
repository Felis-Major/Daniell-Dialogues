using FM.Runtime.Systems.DialogueNodes;
using System;

namespace FM.Editor.Systems.DialogueNodes
{
    /// <summary>
    /// Generic version of base node
    /// </summary>
    /// <typeparam name="T">Type of the matching Runtime Node</typeparam>
    public abstract class GenericBaseNode<T> : BaseNode where T : RuntimeNode
    {
        /* ==========================
         * > Methods
         * -------------------------- */

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public sealed override void SaveNodeData(RuntimeNode runtimeNode)
        {
            base.SaveNodeData(runtimeNode);
            SaveNode((T)runtimeNode);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public sealed override void LoadNodeData(RuntimeNode runtimeNode)
        {
            base.LoadNodeData(runtimeNode);
            LoadNode((T)runtimeNode);
        }

        /// <summary>
        /// Save to a Runtime Node 
        /// </summary>
        /// <param name="node">Runtime Node instance</param>
        protected abstract void SaveNode(T node);

        /// <summary>
        /// Load from a Runtime Node
        /// </summary>
        /// <param name="node">Runtime Node instance</param>
        protected abstract void LoadNode(T node);
    }
}