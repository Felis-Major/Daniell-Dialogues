using Daniell.Runtime.Systems.DialogueNodes;
using Daniell.Runtime.Systems.Events;

namespace Daniell.Editor.Systems.DialogueNodes
{
    /// <summary>
    /// Node used to call a void event
    /// </summary>
    [NodeName("Call Void Event")]
    [NodeColor(30, 150, 150)]
    [RuntimeNodeType(typeof(CallEventRuntimeNode))]
    [NodeWidth(7)]
    public class CallVoidEventNode : GenericBaseNode<CallEventRuntimeNode>
    {
        /* ==========================
         * > Constructors
         * -------------------------- */

        public CallVoidEventNode()
        {
            // Add Ports
            _nodePortHandler.AddInputPort("Input");
            _nodePortHandler.AddOutputPort(CallEventRuntimeNode.NEXT_NODE_PORT);

            // Add parameter fields and link to variables
            _nodeFieldHandler.AddField(new ObjectNodeField<VoidEvent>("Event"), "Event Field");
        }


        /* ==========================
         * > Methods
         * -------------------------- */

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void SaveNode(CallEventRuntimeNode node)
        {
            node.Event = _nodeFieldHandler.GetFieldValue<VoidEvent>("Event Field");
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void LoadNode(CallEventRuntimeNode node)
        {
            _nodeFieldHandler.SetFieldValue("Event Field", node.Event);
        }
    }
}