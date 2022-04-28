using FM.Runtime.Systems.DialogueNodes;
using FM.Runtime.Systems.Events;

namespace FM.Editor.Systems.DialogueNodes
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