using System;
using UnityEngine;

namespace FM.Runtime.Systems.DialogueNodes
{
    /// <summary>
    /// Identifies a port on a node
    /// </summary>
    [Serializable]
    public struct PortID
    {
        /* ==========================
         * > Enums
         * -------------------------- */

        /// <summary>
        /// Direction of a port
        /// </summary>
        public enum Direction
        {
            /// <summary>
            /// Input port of a node
            /// </summary>
            Input,

            /// <summary>
            /// Output port of a node
            /// </summary>
            Output
        }


        /* ==========================
         * > Properties
         * -------------------------- */

        /// <summary>
        /// Name of the port
        /// </summary>
        public string PortName => _portName;

        /// <summary>
        /// Direction of the port
        /// </summary>
        public Direction PortDirection => _portDirection;

        /// <summary>
        /// GUID of the node hosting the port
        /// </summary>
        public string NodeGUID => _nodeGUID;


        /* ==========================
         * > Private Fields
         * -------------------------- */

        [SerializeField]
        private string _portName;

        [SerializeField]
        private Direction _portDirection;

        [SerializeField]
        private string _nodeGUID;


        /* ==========================
         * > Constructors
         * -------------------------- */

        public PortID(string portName, string nodeGUID, Direction portDirection)
        {
            _portName = portName;
            _nodeGUID = nodeGUID;
            _portDirection = portDirection;
        }
    }
}