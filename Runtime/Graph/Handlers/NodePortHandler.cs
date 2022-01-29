using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Daniell.Runtime.Systems.DialogueNodes
{
    /// <summary>
    /// Handles creating ports for a node
    /// </summary>
    public class NodePortHandler : NodeElementHandler
    {
        /* ==========================
         * > Indexer
         * -------------------------- */

        public Port this[string portID] => _ports[portID];


        /* ==========================
         * > Private fields
         * -------------------------- */

        private readonly VisualElement _inputContainer;
        private readonly VisualElement _outputContainer;
        private Dictionary<string, Port> _ports = new Dictionary<string, Port>();


        /* ==========================
         * > Constructors
         * -------------------------- */

        public NodePortHandler(BaseNode node) : base(node)
        {
            _inputContainer = node.inputContainer;
            _outputContainer = node.outputContainer;
        }


        /* ==========================
         * > Methods
         * -------------------------- */

        #region Add / Remove Ports

        /// <summary>
        /// Add an input port
        /// </summary>
        /// <param name="name">Name of the port</param>
        /// <param name="port">Port Created</param>
        /// <returns>Created port</returns>
        public Port AddInputPort(string name)
        {
            AddPort(name, Direction.Input, Port.Capacity.Multi, out Port port);
            return port;
        }

        /// <summary>
        /// Add an output port
        /// </summary>
        /// <param name="name">Name of the port</param>
        /// <param name="port">Port Created</param>
        /// <returns>Created port</returns>
        public Port AddOutputPort(string name)
        {
            AddPort(name, Direction.Output, Port.Capacity.Single, out Port port);
            return port;
        }

        /// <summary>
        /// Add a new port to the node
        /// </summary>
        /// <param name="name">Name of the port</param>
        /// <param name="direction">Direction of the port (input or output)</param>
        /// <param name="capacity">Capacity of the node (single or multiple)</param>
        public void AddPort(string name, Direction direction, Port.Capacity capacity, out Port port)
        {
            string portID = GetPortUniqueID(name, direction);

            // Ensure ports have unique names
            if (_ports.ContainsKey(portID))
            {
                Debug.Log($"Port with the name '{name}' already exists in {direction} container.");
                port = null;
                return;
            }

            // Instantiate port and set properties
            port = Node.InstantiatePort(Orientation.Horizontal, direction, capacity, typeof(object));
            port.portName = name;
            port.portColor = Node.NodeColor;

            // Add the node to the proper container
            switch (direction)
            {
                case Direction.Input:
                    _inputContainer.Add(port);
                    break;

                case Direction.Output:
                    _outputContainer.Add(port);
                    break;
            }

            // Add port to the list
            _ports.Add(portID, port);

            RefreshNodeState();
        }

        /// <summary>
        /// Remove a port from the node
        /// </summary>
        /// <param name="name">Name of the port</param>
        /// <param name="direction">Direction of the port (input or output)</param>
        public virtual void RemovePort(string name, Direction direction)
        {
            string portID = GetPortUniqueID(name, direction);

            // Remove port if it exists
            if (_ports.ContainsKey(portID))
            {
                // Remove the port from the view
                _ports[portID].RemoveFromHierarchy();

                // Remove port from the list
                _ports.Remove(portID);
            }
            else
            {
                Debug.Log($"Port with the name '{name}' doesn't exist in {direction} container.");
            }

            RefreshNodeState();
        }

        #endregion

        #region Node Connections

        /// <summary>
        /// Return all the nodes connected to a port
        /// </summary>
        /// <param name="portName">ID of the port</param>
        /// <returns>List of nodes and ports connected to the port</returns>
        public NodePortIdentifier[] GetPortConnections(string portID)
        {
            List<NodePortIdentifier> portConnections = new List<NodePortIdentifier>();

            // If the port exists
            if (_ports.ContainsKey(portID))
            {
                Port port = _ports[portID];

                // Ensure the port is connected
                if (port.connected)
                {
                    foreach (Edge connection in port.connections)
                    {
                        // If the port is an input, get the output port, else reverse this
                        Port targetPort = port.direction == Direction.Input ? connection.output : connection.input;
                        string targetPortID = GetPortUniqueID(targetPort.portName, targetPort.direction);

                        BaseNode targetNode = (BaseNode)targetPort.node;
                        string targetNodeGUID = targetNode.GUID;

                        var portIdentifier = new NodePortIdentifier(targetNodeGUID, targetPortID);
                        portConnections.Add(portIdentifier);
                    }
                }
            }

            return portConnections.ToArray();
        }

        #endregion

        /// <summary>
        /// Does this port ID exist?
        /// </summary>
        /// <param name="portID">Port ID to look for</param>
        /// <returns>True if the port ID exists</returns>
        public bool HasPort(string portID)
        {
            return _ports.ContainsKey(portID);
        }

        /// <summary>
        /// Get node ports IDs
        /// </summary>
        /// <returns>Array of port IDs</returns>
        public string[] GetPortIDs()
        {
            return _ports.Keys.ToArray();
        }

        /// <summary>
        /// Get a unique ID using the name and direction of the port
        /// </summary>
        /// <param name="portName">Name of the port</param>
        /// <param name="portDirection">Direction of the port</param>
        /// <returns>Unique port ID</returns>
        private string GetPortUniqueID(string portName, Direction portDirection)
        {
            return $"{portDirection}>{portName}";
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override void RefreshNodeState()
        {
            base.RefreshNodeState();
            Node.RefreshPorts();
        }
    }
}