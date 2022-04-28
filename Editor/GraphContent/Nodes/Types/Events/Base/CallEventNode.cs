using FM.Runtime.Systems.DialogueNodes;
using FM.Runtime.Systems.Events;

namespace FM.Editor.Systems.DialogueNodes
{
    /// <summary>
    /// Generic Node used to call an event
    /// </summary>
    /// <typeparam name="TEvent">Type of the event</typeparam>
    /// <typeparam name="TNode">Type of the runtime node</typeparam>
    [NodeCategory("Call Event")]
    [NodeColor(30, 150, 150)]
    [NodeWidth(8)]
    public abstract class CallEventNode<TEvent, TNode> : GenericBaseNode<TNode>
        where TEvent : ScriptableEvent
        where TNode : CallEventRuntimeNode<TEvent>
    {
        /* ==========================
         * > Constructors
         * -------------------------- */

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override string DefaultEditorIcon => "d_EventSystem Icon";


        /* ==========================
         * > Constructors
         * -------------------------- */

        public CallEventNode()
        {
            // Add Ports
            _nodePortHandler.AddInputPort("Input");
            _nodePortHandler.AddOutputPort(CallEventRuntimeNode<TEvent>.NEXT_NODE_PORT);

            // Add parameter fields and link to variables
            _nodeFieldHandler.AddField(new ObjectNodeField<TEvent>("Event"), "Event Field");
        }


        /* ==========================
         * > Methods
         * -------------------------- */

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void SaveNode(TNode node)
        {
            node.Event = _nodeFieldHandler.GetFieldValue<TEvent>("Event Field");
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void LoadNode(TNode node)
        {
            _nodeFieldHandler.SetFieldValue("Event Field", node.Event);
        }
    }
}