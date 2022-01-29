namespace Daniell.Runtime.Systems.DialogueNodes
{
    /// <summary>
    /// Holds infos about a connection to a node
    /// </summary>
    public struct NodeConnection
    {
        /* ==========================
         * > Properties
         * -------------------------- */

        /// <summary>
        /// GUID of the node targeted by this connection
        /// </summary>
        public string TargetNodeGUID { get; }

        /// <summary>
        /// ID of the port on the node targeted by this connection
        /// </summary>
        public string TargetPortID { get; }

        /// <summary>
        /// ID of the port this connection originates from
        /// </summary>
        public string StartPortID { get; }


        /* ==========================
         * > Constructors
         * -------------------------- */

        public NodeConnection(string targetNodeGUID, string targetPortID, string startPortID)
        {
            TargetNodeGUID = targetNodeGUID;
            TargetPortID = targetPortID;
            StartPortID = startPortID;
        }
    }
}