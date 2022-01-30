using Daniell.Runtime.Systems.DialogueNodes;

namespace Daniell.Editor.Systems.DialogueNodes
{
    /// <summary>
    /// Node used to trigger a new dialogue line
    /// </summary>
    [NodeName("Dialogue Line")]
    [NodeColor(150, 70, 80)]
    [RuntimeNodeType(typeof(DialogueLineRuntimeNode))]
    public class DialogueLineNode : GenericBaseNode<DialogueLineRuntimeNode>
    {
        /* ==========================
         * > Constructors
         * -------------------------- */

        public DialogueLineNode()
        {
            // Add Ports
            _nodePortHandler.AddInputPort("Input");
            _nodePortHandler.AddOutputPort(DialogueLineRuntimeNode.NEXT_NODE_PORT);

            // Add parameter fields and link to variables
            _nodeFieldHandler.AddField(new ObjectNodeField<DialogueActor>("Actor"), "Actor Field");
            _nodeFieldHandler.AddField(new StringNodeField("Line"), "Line Field");
        }


        /* ==========================
         * > Methods
         * -------------------------- */

        #region Save & Load

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void SaveNode(DialogueLineRuntimeNode node)
        {
            node.Actor = _nodeFieldHandler.GetFieldValue<DialogueActor>("Actor Field");
            node.Line = _nodeFieldHandler.GetFieldValue<string>("Line Field");
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void LoadNode(DialogueLineRuntimeNode node)
        {
            _nodeFieldHandler.SetFieldValue("Actor Field", node.Actor);
            _nodeFieldHandler.SetFieldValue("Line Field", node.Line);
        }

        #endregion
    }
}