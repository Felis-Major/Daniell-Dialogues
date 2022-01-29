using System;
using UnityEngine;

namespace Daniell.Runtime.Systems.DialogueNodes
{
    /// <summary>
    /// Identifies a port associated with a node
    /// </summary>
    [Serializable]
    public struct NodePortIdentifier
    {
        /* ==========================
         * > Properties
         * -------------------------- */

        /// <summary>
        /// GUID of the node
        /// </summary>
        public string NodeGUID => _nodeGUID;
        /// <summary>
        /// ID of the node's port
        /// </summary>
        public string PortID => _portID;


        /* ==========================
         * > Private Fields
         * -------------------------- */

        [SerializeField]
        private string _nodeGUID;

        [SerializeField]
        private string _portID;


        /* ==========================
         * > Constructors
         * -------------------------- */

        public NodePortIdentifier(string nodeGUID, string portID)
        {
            _nodeGUID = nodeGUID;
            _portID = portID;
        }
    }
}