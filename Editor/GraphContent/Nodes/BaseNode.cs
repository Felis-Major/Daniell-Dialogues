using Daniell.Runtime.Helpers.Reflection;
using Daniell.Runtime.Systems.DialogueNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static Daniell.Runtime.Systems.DialogueNodes.RuntimeNode;

namespace Daniell.Editor.Systems.DialogueNodes
{
    /// <summary>
    /// Base Node for a dialogue node system
    /// </summary>
    [NodeName("Node")]
    [NodeColor(255, 255, 255)]
    [NodeWidth(13)]
    public abstract class BaseNode : Node
    {
        /* ==========================
         * > Constants
         * -------------------------- */

        /// <summary>
        /// X Position of the start node
        /// </summary>
        public const int DEFAULT_X_POS = 50;

        /// <summary>
        /// Y Position of the start node
        /// </summary>
        public const int DEFAULT_Y_POS = 50;

        /// <summary>
        /// Distance in pixels at which the node will snap in place
        /// </summary>
        public const int NODE_SNAP_SIZE = 25;


        /* ==========================
         * > Properties
         * -------------------------- */

        /// <summary>
        /// Display name of the node
        /// </summary>
        public string NodeName { get; private set; }

        /// <summary>
        /// Color of the node
        /// </summary>
        public Color NodeColor { get; private set; }

        /// <summary>
        /// Unique ID of the node
        /// </summary>
        public string GUID { get; private set; }

        /// <summary>
        /// Position of the node
        /// </summary>
        public Vector2 GraphPosition { get; private set; }

        /// <summary>
        /// Type of the runtime node
        /// </summary>
        public virtual Type RuntimeNodeType => null;


        /* ==========================
         * > Private Fields
         * -------------------------- */

        protected readonly NodePortHandler _nodePortHandler;
        protected readonly NodeFieldHandler _nodeFieldHandler;


        /* ==========================
         * > Constructors
         * -------------------------- */

        public BaseNode()
        {
            // Assign new GUID
            GUID = Guid.NewGuid().ToString();

            SetDefaultNodeStyleAndPosition();
            SetNodeAttributes();

            // Create handlers
            _nodePortHandler = new NodePortHandler(this);
            _nodeFieldHandler = new NodeFieldHandler(this);
        }


        /* ==========================
         * > Methods
         * -------------------------- */

        #region Node Styling

        /// <summary>
        /// Set default node position, width and padding
        /// </summary>
        protected virtual void SetDefaultNodeStyleAndPosition()
        {
            // Set node default position & size
            SetPosition(DEFAULT_X_POS, DEFAULT_Y_POS);

            // Set padding
            extensionContainer.style.paddingTop =
                extensionContainer.style.paddingLeft =
                extensionContainer.style.paddingRight = 5;
        }

        /// <summary>
        /// Set node attribute data
        /// </summary>
        protected virtual void SetNodeAttributes()
        {
            // Get Color attribute
            var colorAttribute = ReflectionHelpers.GetAttributeForType<NodeColorAttribute>(GetType());
            NodeColor = colorAttribute.Color;

            // Set the colors of the node
            titleContainer.style.backgroundColor = NodeColor;
            extensionContainer.style.backgroundColor = NodeHelpers.GetRelativeColor(NodeColor);

            // Get Name attribute
            var nameAttribute = ReflectionHelpers.GetAttributeForType<NodeNameAttribute>(GetType());
            NodeName = nameAttribute.Name;

            // Set node name
            title = NodeName;

            // Get Width attribute
            var widthAttribute = ReflectionHelpers.GetAttributeForType<NodeWidthAttribute>(GetType());
            style.width = widthAttribute.Width * NODE_SNAP_SIZE;
        }

        #endregion

        #region Node Properties override

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override bool IsCopiable()
        {
            return true;
        }

        #endregion

        #region Size & Position

        /// <summary>
        /// Set the position of the node using XY coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public void SetPosition(int x, int y)
        {
            SetPosition(new Rect(x, y, 0, 0));
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override void SetPosition(Rect newPos)
        {
            // Ensure that the position is locked to the grid
            newPos.x -= newPos.x % NODE_SNAP_SIZE;
            newPos.y -= newPos.y % NODE_SNAP_SIZE;

            GraphPosition = newPos.position;

            base.SetPosition(newPos);
        }

        #endregion

        #region Save & Load

        /// <summary>
        /// Save data to the save data bundle
        /// </summary>
        /// <param name="runtimeNode">Runtime node to write to</param>
        public virtual void SaveNodeData(RuntimeNode runtimeNode)
        {
            runtimeNode.GraphPosition = GraphPosition;
            runtimeNode.NodeTypeName = GetType().AssemblyQualifiedName;
            runtimeNode.GUID = GUID;
        }

        /// <summary>
        /// Load data from the save data bundle
        /// </summary>
        /// <param name="runtimeNode">Runtime Node to read from</param>
        public virtual void LoadNodeData(RuntimeNode runtimeNode)
        {
            var position = runtimeNode.GraphPosition;
            SetPosition((int)position.x, (int)position.y);
            GUID = runtimeNode.GUID;
        }

        #endregion

        /// <summary>
        /// Get all ports connected to this node's ports
        /// </summary>
        /// <returns>List of ports connected to this node's ports</returns>
        public PortConnection[] GetConnections()
        {
            List<PortConnection> nodeConnections = new List<PortConnection>();

            foreach (var port in _nodePortHandler.Ports)
            {
                var nodePortConnections = _nodePortHandler.GetPortConnections(port.portName, port.direction);

                for (int j = 0; j < nodePortConnections.Length; j++)
                {
                    nodeConnections.Add(nodePortConnections[j]);
                }
            }

            return nodeConnections.ToArray();
        }

        /// <summary>
        /// Find a port on the node
        /// </summary>
        /// <param name="portID">ID of the port</param>
        /// <returns>Port found by ID</returns>
        public Port FindPort(PortID portID)
        {
            var direction = portID.PortDirection == PortID.Direction.Input ? Direction.Input : Direction.Output;
            return _nodePortHandler.FindPort(portID.PortName, direction);
        }
    }
}