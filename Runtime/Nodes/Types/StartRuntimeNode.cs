namespace FM.Runtime.Systems.DialogueNodes
{
    /// <summary>
    /// The first node in a graph
    /// </summary>
    public class StartRuntimeNode : RuntimeNode
    {
        /* ==========================
         * > Constants
         * -------------------------- */

        /// <summary>
        /// Next node port
        /// </summary>
        public const string NEXT_NODE_PORT = "Next";


        /* ==========================
         * > Methods
         * -------------------------- */

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