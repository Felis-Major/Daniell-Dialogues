using System;
using System.Collections.Generic;
using UnityEngine;

namespace FM.Runtime.Systems.DialogueNodes
{
    /// <summary>
    /// A node that can be used in Runtime Applications
    /// </summary>
    public abstract class RuntimeNode : ScriptableObject
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

        /// <summary>
        /// Dialogue file for this node
        /// </summary>
        public DialogueFile DialogueFile { get => _dialogueFile; set => _dialogueFile = value; }


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

        [SerializeField]
        private DialogueFile _dialogueFile;


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

        /// <summary>
        /// Get the next node
        /// </summary>
        public abstract RuntimeNode GetNextNode();


        /// <summary>
        /// Find a pot with matching name and direction
        /// </summary>
        /// <param name="portName">Name of the node to look for</param>
        /// <param name="direction">Direction of the node to look for</param>
        protected RuntimeNode[] GetConnectedNodesToPort(string portName, PortID.Direction direction)
        {
            List<RuntimeNode> connectedNodes = new List<RuntimeNode>();

            for (int i = 0; i < _connections.Count; i++)
            {
                // Find the port of origin in the connections
                PortConnection connection = _connections[i];
                var originPortName = connection.OriginPort.PortName;
                var originPortDirection = connection.OriginPort.PortDirection;

                // If name and direction matches
                if (originPortName == portName && originPortDirection == direction)
                {
                    // Find the Runtime node for the target node GUID
                    var targetNodeGUID = connection.TargetPort.NodeGUID;
                    var targetNode = _dialogueFile.FindNodeByGUID(targetNodeGUID);
                    if (targetNode != null)
                    {
                        connectedNodes.Add(targetNode);
                    }
                }
            }

            return connectedNodes.ToArray();
        }
    }
}