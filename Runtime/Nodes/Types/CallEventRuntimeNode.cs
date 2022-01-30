using Daniell.Runtime.Systems.Events;
using UnityEngine;

namespace Daniell.Runtime.Systems.DialogueNodes
{
    public class CallEventRuntimeNode : RuntimeNode
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
        public VoidEvent Event { get => _event; set => _event = value; }


        /* ==========================
         * > Private fields
         * -------------------------- */

        [SerializeField]
        private VoidEvent _event;


        /* ==========================
         * > Methods
         * -------------------------- */

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