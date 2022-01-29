using System;

namespace Daniell.Runtime.Systems.DialogueNodes
{
    /// <summary>
    /// Base class for interpreting nodes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class NodeInterpreter<T> where T : BaseNode
    {
        /* ==========================
         * > Protected Fields
         * -------------------------- */

        protected readonly T _node;


        /* ==========================
         * > Events
         * -------------------------- */

        /// <summary>
        /// Event called when the next node was found during processing
        /// </summary>
        public event Action<BaseNode> OnNextNodeFound;


        /* ==========================
         * > Constructors
         * -------------------------- */

        public NodeInterpreter(T node)
        {
            _node = node;
        }


        /* ==========================
         * > Methods
         * -------------------------- */

        /// <summary>
        /// Use this node and return the next
        /// </summary>
        public abstract void Process();

        /// <summary>
        /// Call this to end the node processing
        /// </summary>
        /// <param name="nextNode">Node to be processed next</param>
        protected virtual void Finish(BaseNode nextNode)
        {
            OnNextNodeFound?.Invoke(nextNode);
        }
    }
}