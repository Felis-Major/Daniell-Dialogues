using Daniell.Runtime.Systems.Events;
using UnityEngine;

namespace Daniell.Runtime.Systems.DialogueNodes
{
    /// <summary>
    /// Runtime Generic version of call event nodes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CallEventRuntimeNode<T> : RuntimeNode where T : ScriptableEvent
    {
        /* ==========================
         * > Constants
         * -------------------------- */

        /// <summary>
        /// Next node port
        /// </summary>
        public const string NEXT_NODE_PORT = "Next";


        /* ==========================
         * > Properties
         * -------------------------- */

        /// <summary>
        /// Actor for this dialogue node
        /// </summary>
        public T Event { get => _event; set => _event = value; }


        /* ==========================
         * > Private fields
         * -------------------------- */

        [SerializeField]
        private T _event;


        /* ==========================
         * > Methods
         * -------------------------- */

        /// <summary>
        /// Raise the event associated with this node
        /// </summary>
        public void RaiseEvent()
        {
            if (_event != null)
            {
                _event.Raise();
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override RuntimeNode GetNextNode()
        {
            // Find the first node connected to the next port
            var nodes = GetConnectedNodesToPort(NEXT_NODE_PORT, PortID.Direction.Output);

            // Return the first node found if there is one
            if (nodes.Length > 0)
            {
                return nodes[0];
            }
            else
            {
                return null;
            }
        }
    }
}