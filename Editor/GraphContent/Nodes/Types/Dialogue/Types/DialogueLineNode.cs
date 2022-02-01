using Daniell.Runtime.Systems.DialogueNodes;

namespace Daniell.Editor.Systems.DialogueNodes
{

    /// <summary>
    /// Node used to trigger a new dialogue line
    /// </summary>
    [NodeName("Dialogue Line")]
    [NodeColor(156, 59, 86)]
    [RuntimeNodeType(typeof(DialogueLineRuntimeNode))]
    public class DialogueLineNode : BaseDialogueLineNode<DialogueLineRuntimeNode>
    {
        /* ==========================
         * > Properties
         * -------------------------- */

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override string DefaultEditorIcon => "TextScriptImporter Icon";


        /* ==========================
         * > Constructors
         * -------------------------- */

        public DialogueLineNode()
        {
            // Add Ports
            _nodePortHandler.AddOutputPort(DialogueLineRuntimeNode.NEXT_NODE_PORT);
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