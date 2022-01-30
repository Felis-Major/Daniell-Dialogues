using System;

namespace Daniell.Editor.Systems.DialogueNodes
{
    /// <summary>
    /// Attribute used to mark a node as the only one in the graph
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class SingleInstanceNodeAttribute : Attribute
    {

    }
}