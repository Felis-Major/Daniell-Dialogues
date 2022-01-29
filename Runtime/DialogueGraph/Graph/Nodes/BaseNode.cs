using Daniell.Runtime.Helpers.Reflection;
using Daniell.Runtime.Systems.SimpleSave;
using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Daniell.Runtime.Systems.DialogueNodes
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
        public Vector2 Position { get; private set; }


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

            Position = newPos.position;

            base.SetPosition(newPos);
        }

        #endregion

        #region Save & Load

        /// <summary>
        /// Save data to the save data bundle
        /// </summary>
        /// <param name="nodeSaveData">Data bundle to write to</param>
        public virtual void SaveNodeData(SaveDataBundle nodeSaveData)
        {
            nodeSaveData.Set("GUID", GUID);
            nodeSaveData.Set("Position", Position);
            nodeSaveData.Set("Type", GetType().AssemblyQualifiedName);
        }

        /// <summary>
        /// Load data from the save data bundle
        /// </summary>
        /// <param name="nodeSaveData">Data bundle to read from</param>
        public virtual void LoadNodeData(SaveDataBundle nodeSaveData)
        {
            GUID = nodeSaveData.Get<string>("GUID");
            var position = nodeSaveData.Get<Vector2>("Position");
            SetPosition((int)position.x, (int)position.y);
        }

        #endregion

        /// <summary>
        /// Get all connections to this node
        /// </summary>
        /// <returns>List of Node Connections originating from this node</returns>
        public NodeConnection[] GetNodeConnections()
        {
            List<NodeConnection> nodeConnections = new List<NodeConnection>();

            // Find and create connections to all port ids
            string[] portIDs = _nodePortHandler.GetPortIDs();

            for (int i = 0; i < portIDs.Length; i++)
            {
                var portID = portIDs[i];
                var nodePortIdentifiers = _nodePortHandler.GetPortConnections(portID);

                for (int j = 0; j < nodePortIdentifiers.Length; j++)
                {
                    var targetPortIdentifier = nodePortIdentifiers[j];
                    var originPortIdentifier = new NodePortIdentifier(GUID, portID);
                    var nodeConnection = new NodeConnection(originPortIdentifier, targetPortIdentifier);
                    nodeConnections.Add(nodeConnection);
                }
            }

            return nodeConnections.ToArray();
        }

        /// <summary>
        /// Return a port by ID from this node
        /// </summary>
        /// <param name="portID">ID of the port to look for</param>
        /// <returns>Port with matching ID or null if not found</returns>
        public Port GetPortByID(string portID)
        {
            if (_nodePortHandler.HasPort(portID))
            {
                return _nodePortHandler[portID];
            }

            return null;
        }
    }
}