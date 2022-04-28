using FM.Runtime.Systems.DialogueNodes;
using System;

namespace FM.Editor.Systems.DialogueNodes
{
    /// <summary>
    /// Node used as a starting point of every dialogue graph
    /// </summary>
    [NodeName("Start")]
    [NodeColor(60, 130, 60)]
    [NodeWidth(4)]
    [SingleInstanceNode]
    [RuntimeNodeType(typeof(StartRuntimeNode))]
    public class StartNode : BaseNode
    {
        /* ==========================
         * > Properties
         * -------------------------- */

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override string DefaultEditorIcon => "NavMeshObstacle Icon";


        /* ==========================
         * > Constructors
         * -------------------------- */

        public StartNode()
        {
            _nodePortHandler.AddOutputPort(StartRuntimeNode.NEXT_NODE_PORT);
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