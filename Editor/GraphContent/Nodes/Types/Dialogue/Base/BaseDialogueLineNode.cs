using Daniell.Runtime.Systems.DialogueNodes;

namespace Daniell.Editor.Systems.DialogueNodes
{
    [NodeCategory("Dialogue")]
    public abstract class BaseDialogueLineNode<T> : GenericBaseNode<T> where T : RuntimeNode
    {
        public BaseDialogueLineNode()
        {
            // Add Ports
            _nodePortHandler.AddInputPort("Input");

            // Add parameter fields and link to variables
            _nodeFieldHandler.AddField(new ObjectNodeField<DialogueActor>("Actor"), "Actor Field");
            _nodeFieldHandler.AddField(new StringNodeField("Line"), "Line Field");
        }
    }
}