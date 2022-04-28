using UnityEngine;

namespace FM.Runtime.Systems.DialogueNodes
{
    /// <summary>
    /// Runtime version of a Dialogue Line Node
    /// </summary>
    public class DialogueLineRuntimeNode : RuntimeNode
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
        public DialogueActor Actor { get => _actor; set => _actor = value; }

        /// <summary>
        /// Line for this dialogue node
        /// </summary>
        public string Line { get => _text; set => _text = value; }


        /* ==========================
         * > Private fields
         * -------------------------- */

        [SerializeField]
        private DialogueActor _actor;

        [SerializeField]
        private string _text;


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
            if(nodes.Length > 0)
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