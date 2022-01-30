using System.Collections.Generic;
using UnityEngine;

namespace Daniell.Runtime.Systems.DialogueNodes
{
    /// <summary>
    /// A node that can be used in Runtime Applications
    /// </summary>
    public class RuntimeNode : ScriptableObject
    {
        /* ==========================
         * > Properties
         * -------------------------- */

#if UNITY_EDITOR

        /// <summary>
        /// Node type assembly qualified name
        /// </summary>
        public string NodeTypeName { get => _nodeTypeName; set => _nodeTypeName = value; }

        /// <summary>
        /// Position of the node on the graph
        /// </summary>
        public Vector2 GraphPosition { get => _nodePosition; set => _nodePosition = value; }

#endif

        /// <summary>
        /// GUID of the node
        /// </summary>
        public string GUID { get => _guid; set => _guid = value; }

        /// <summary>
        /// Input connections for this node
        /// </summary>
        public IEnumerable<PortConnection> Connections => _connections;


        /* ==========================
         * > Private Fields
         * -------------------------- */

#if UNITY_EDITOR

        [SerializeField]
        private string _nodeTypeName;

        [SerializeField]
        private Vector2 _nodePosition;

#endif

        [SerializeField]
        private string _guid;

        [SerializeField]
        private List<PortConnection> _connections = new List<PortConnection>();


        /* ==========================
         * > Methods
         * -------------------------- */

        /// <summary>
        /// Clear all connections to this node
        /// </summary>
        public void ClearConnections()
        {
            _connections.Clear();
        }

        /// <summary>
        /// Add a new connection to this node
        /// </summary>
        /// <param name="nodePortID">Connection data</param>
        public void AddConnection(PortConnection nodePortID)
        {
            _connections.Add(nodePortID);
        }
    }
}