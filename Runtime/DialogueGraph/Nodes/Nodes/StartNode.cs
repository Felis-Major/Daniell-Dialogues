using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Daniell.Runtime.Systems.DialogueNodes
{
    /// <summary>
    /// Node used as a starting point of every dialogue graph
    /// </summary>
    [NodeName("Start")]
    [NodeColor(38, 109, 30)]
    [NodeWidth(3)]
    public class StartNode : BaseNode
    {
        /* ==========================
         * > Constructors
         * -------------------------- */

        public StartNode()
        {
            _nodePortHandler.AddOutputPort("Next");
        }


        /* ==========================
         * > Methods
         * -------------------------- */

        #region Node Properties override

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override bool IsMovable()
        {
            return false;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override bool IsSelectable()
        {
            return false;
        }

        #endregion
    }
}