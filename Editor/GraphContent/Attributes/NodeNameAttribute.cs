using System;

namespace FM.Editor.Systems.DialogueNodes
{
    /// <summary>
    /// Attribute used to define the name of a node
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class NodeNameAttribute : Attribute
    {
        /* ==========================
         * > Properties
         * -------------------------- */

        /// <summary>
        /// Name of the node
        /// </summary>
        public string Name { get; }


        /* ==========================
         * > Constructors
         * -------------------------- */

        public NodeNameAttribute(string name)
        {
            Name = name;
        }
    }
}