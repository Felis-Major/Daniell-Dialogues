using Daniell.Runtime.Systems.DialogueNodes;
using Daniell.Runtime.Systems.Events;

namespace Daniell.Editor.Systems.DialogueNodes
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