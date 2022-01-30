using UnityEngine.UIElements;

namespace Daniell.Editor.Systems.DialogueNodes
{
    /// <summary>
    /// Handles creating elements inside a container for a base node
    /// </summary>
    public abstract class NodeElementHandler
    {
        /* ==========================
         * > Properties
         * -------------------------- */

        /// <summary>
        /// Node used by this handler
        /// </summary>
        public BaseNode Node { get; }


        /* ==========================
         * > Constructors
         * -------------------------- */

        public NodeElementHandler(BaseNode node)
        {
            Node = node;
        }


        /* ==========================
         * > Methods
         * -------------------------- */

        /// <summary>
        /// Refresh the visual expanded state of the node
        /// </summary>
        public virtual void RefreshNodeState()
        {
            Node.RefreshExpandedState();
        }
    }
}