using Daniell.Runtime.Systems.DialogueNodes;
using Daniell.Runtime.Systems.Events;

namespace Daniell.Editor.Systems.DialogueNodes
{
    /// <summary>
    /// Node used to call a void Event
    /// </summary>
    [NodeName("Call Void Event")]
    [RuntimeNodeType(typeof(CallVoidEventRuntimeNode))]
    [NodeColor(101, 42, 199)]
    public class CallVoidEventNode : CallEventNode<VoidEvent, CallVoidEventRuntimeNode>
    {

    }
}