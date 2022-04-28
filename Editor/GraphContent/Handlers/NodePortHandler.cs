using FM.Runtime.Systems.DialogueNodes;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace FM.Editor.Systems.DialogueNodes
{
    /// <summary>
    /// Handles creating ports for a node
    /// </summary>
    public class NodePortHandler : NodeElementHandler
    {
        /* ==========================
         * > Properties
         * -------------------------- */

        public IEnumerable<Port> Ports => _ports;


        /* ==========================
         * > Private fields
         * -------------------------- */

        private readonly VisualElement _inputContainer;
        private readonly VisualElement _outputContainer;
        private readonly List<Port> _ports = new List<Port>();


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
            _ports.Add(port);

            RefreshNodeState();
        }

        /// <summary>
        /// Remove a port from the node
        /// </summary>
        /// <param name="name">Name of the port</param>
        /// <param name="direction">Direction of the port (input or output)</param>
        public virtual void RemovePort(string name, Direction direction)
        {
            var port = FindPort(name, direction);

            if (port != null)
            {
                // Disconnect all edges connected to the port
                if (port.connected)
                {
                    foreach (var connection in port.connections)
                    {
                        Port targetPort = port.direction == Direction.Input ? connection.output : connection.input;

                        targetPort.Disconnect(connection);
                        connection.RemoveFromHierarchy();
                    }

                    port.DisconnectAll();
                }

                port.RemoveFromHierarchy();
                _ports.Remove(port);
            }

            RefreshNodeState();
        }

        #endregion

        #region Node Connections

        /// <summary>
        /// Return all the ports connected to a port
        /// </summary>
        /// <param name="portName">Name of the port</param>
        /// <returns>List ports connected to the port</returns>
        public PortConnection[] GetPortConnections(string name, Direction direction)
        {
            var portConnections = new List<PortConnection>();
            var port = FindPort(name, direction);

            // If the port exists
            if (port != null && port.connected)
            {
                foreach (Edge connection in port.connections)
                {
                    // If the port is an input, get the output port, else reverse this
                    Port targetPort = port.direction == Direction.Input ? connection.output : connection.input;

                    var originPortID = GetPortID(port);
                    var targetPortID = GetPortID(targetPort);

                    var portIdentifier = new PortConnection(originPortID, targetPortID);
                    portConnections.Add(portIdentifier);
                }
            }

            return portConnections.ToArray();
        }

        /// <summary>
        /// Find a port using its name and direction
        /// </summary>
        /// <param name="name">Name of the port</param>
        /// <param name="direction">Direction of the port</param>
        /// <returns>Port matching name and direction, null if no port is found</returns>
        public Port FindPort(string name, Direction direction)
        {
            for (int i = 0; i < _ports.Count; i++)
            {
                Port port = _ports[i];
                if (port.portName == name && port.direction == direction)
                {
                    return port;
                }
            }

            return null;
        }

        #endregion

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override void RefreshNodeState()
        {
            base.RefreshNodeState();
            Node.RefreshPorts();
        }

        /// <summary>
        /// Get PortID from port
        /// </summary>
        /// <param name="port">Port to convert to PortID</param>
        /// <returns>PortID using port values</returns>
        public static PortID GetPortID(Port port)
        {
            var nodeGUID = ((BaseNode)port.node).GUID;
            var portName = port.portName;
            var portDirection = port.direction == Direction.Input ? PortID.Direction.Input : PortID.Direction.Output;

            var portID = new PortID(portName, nodeGUID, portDirection);
            return portID;
        }
    }
}