using Daniell.Runtime.Systems.DialogueNodes;
using Daniell.Runtime.Systems.Events;

namespace Daniell.Editor.Systems.DialogueNodes
{
    /// <summary>
    /// Node used to call a bool Event
    /// </summary>
    [NodeName("Call Bool Event")]
    [NodeColor(161, 78, 171)]
    [RuntimeNodeType(typeof(CallBoolEventRuntimeNode))]
    public class CallBoolEventNode : CallGenericEventNode<BoolEvent, bool, CallBoolEventRuntimeNode, BoolNodeField>
    {

    }
}