using FM.Runtime.Systems.DialogueNodes;
using FM.Runtime.Systems.Events;

namespace FM.Editor.Systems.DialogueNodes
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