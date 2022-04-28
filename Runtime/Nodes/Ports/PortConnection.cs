using System;
using UnityEngine;

namespace FM.Runtime.Systems.DialogueNodes
{
    /// <summary>
    /// A connection between two ports
    /// </summary>
    [Serializable]
    public struct PortConnection
    {
        /* ==========================
         * > Properties
         * -------------------------- */

        /// <summary>
        /// Origin Port ID
        /// </summary>
        public PortID OriginPort => _originPort;

        /// <summary>
        /// Target Port ID
        /// </summary>
        public PortID TargetPort => _targetPort;


        /* ==========================
         * > Private Fields
         * -------------------------- */

        [SerializeField]
        private PortID _originPort;

        [SerializeField]
        private PortID _targetPort;


        /* ==========================
         * > Constructors
         * -------------------------- */

        public PortConnection(PortID originPort, PortID targetPort)
        {
            _originPort = originPort;
            _targetPort = targetPort;
        }
    }
}