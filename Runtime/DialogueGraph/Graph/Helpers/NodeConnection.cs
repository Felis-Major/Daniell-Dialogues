using System;
using UnityEngine;

namespace Daniell.Runtime.Systems.DialogueNodes
{
    /// <summary>
    /// Holds infos about a connection between two nodes
    /// </summary>
    [Serializable]
    public struct NodeConnection
    {
        /* ==========================
         * > Properties
         * -------------------------- */

        /// <summary>
        /// Origin of the connection
        /// </summary>
        public NodePortIdentifier ConnectionOrigin => _connectionOrigin;

        /// <summary>
        /// Target of the connection
        /// </summary>
        public NodePortIdentifier ConnectionTarget => _connectionTarget;


        /* ==========================
         * > Private Fields
         * -------------------------- */

        [SerializeField]
        private NodePortIdentifier _connectionOrigin;

        [SerializeField]
        private NodePortIdentifier _connectionTarget;


        /* ==========================
         * > Constructors
         * -------------------------- */

        public NodeConnection(NodePortIdentifier connectionOrigin, NodePortIdentifier connectionTarget)
        {
            _connectionOrigin = connectionOrigin;
            _connectionTarget = connectionTarget;
        }

        public NodeConnection(string originNodeGUID, string originPortID, string targetNodeGUID, string targetPortID)
        {
            _connectionOrigin = new NodePortIdentifier(originNodeGUID, originPortID);
            _connectionTarget = new NodePortIdentifier(targetNodeGUID, targetPortID);
        }
    }
}