using FM.Runtime.Systems.DialogueNodes;
using FM.Runtime.Systems.Events;

namespace FM.Editor.Systems.DialogueNodes
{
    /// <summary>
    /// Node used to call a float Event
    /// </summary>
    [NodeName("Call Float Event")]
    [NodeColor(67, 164, 170)]
    [RuntimeNodeType(typeof(CallFloatEventRuntimeNode))]
    public class CallFloatEventNode : CallGenericEventNode<FloatEvent, float, CallFloatEventRuntimeNode, FloatNodeField>
    {

    }
}