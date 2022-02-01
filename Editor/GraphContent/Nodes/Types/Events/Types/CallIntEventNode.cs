using Daniell.Runtime.Systems.DialogueNodes;
using Daniell.Runtime.Systems.Events;

namespace Daniell.Editor.Systems.DialogueNodes
{
    /// <summary>
    /// Node used to call an int Event
    /// </summary>
    [NodeName("Call Int Event")]
    [NodeColor(47, 151, 199)]
    [RuntimeNodeType(typeof(CallIntEventRuntimeNode))]
    public class CallIntEventNode : CallGenericEventNode<IntEvent, int, CallIntEventRuntimeNode, IntNodeField>
    {

    }
}