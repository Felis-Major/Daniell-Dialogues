using FM.Runtime.Systems.DialogueNodes;
using FM.Runtime.Systems.Events;

namespace FM.Editor.Systems.DialogueNodes
{
    /// <summary>
    /// Node used to call a string Event
    /// </summary>
    [NodeName("Call String Event")]
    [NodeColor(133, 78, 171)]
    [RuntimeNodeType(typeof(CallStringEventRuntimeNode))]
    public class CallStringEventNode : CallGenericEventNode<StringEvent, string, CallStringEventRuntimeNode, StringNodeField>
    {

    }
}