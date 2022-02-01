using Daniell.Runtime.Systems.DialogueNodes;
using Daniell.Runtime.Systems.Events;
using System;

namespace Daniell.Editor.Systems.DialogueNodes
{
    /// <summary>
    /// Generic event node
    /// </summary>
    /// <typeparam name="TEvent">Type of the event</typeparam>
    /// <typeparam name="TValue">Value of the event</typeparam>
    /// <typeparam name="TNode">Type of the runtime node</typeparam>
    public abstract class CallGenericEventNode<TEvent, TValue, TNode, TFieldType> : CallEventNode<TEvent, TNode>
        where TEvent : GenericScriptableEvent<TValue>
        where TNode : CallGenericEventRuntimeNode<TEvent, TValue>
        where TFieldType : GenericNodeField<TValue>
    {
        /* ==========================
         * > Constructors
         * -------------------------- */

        public CallGenericEventNode() : base()
        {
            // Add parameter fields and link to variables
            var field = (TFieldType)Activator.CreateInstance(typeof(TFieldType), "Value");
            _nodeFieldHandler.AddField(field, "Value Field");
        }


        /* ==========================
         * > Methods
         * -------------------------- */

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void SaveNode(TNode node)
        {
            base.SaveNode(node);
            node.EventValue = _nodeFieldHandler.GetFieldValue<TValue>("Value Field");
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void LoadNode(TNode node)
        {
            base.LoadNode(node);
            _nodeFieldHandler.SetFieldValue("Value Field", node.EventValue);
        }
    }
}