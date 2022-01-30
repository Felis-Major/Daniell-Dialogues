using System;

namespace Daniell.Editor.Systems.DialogueNodes
{
    /// <summary>
    /// Attribute used to determine supported node types for this file
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class RuntimeNodeTypeAttribute : Attribute
    {
        /* ==========================
         * > Properties
         * -------------------------- */

        /// <summary>
        /// Type of the node
        /// </summary>
        public Type Type { get; }


        /* ==========================
         * > Constructors
         * -------------------------- */

        public RuntimeNodeTypeAttribute(Type type)
        {
            Type = type;
        }
    }
}