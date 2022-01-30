using System;

namespace Daniell.Runtime.Systems.DialogueNodes
{
    /// <summary>
    /// Attribute used to determine supported node types for this file
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class SupportedNodeTypeAttribute : Attribute
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

        public SupportedNodeTypeAttribute(Type type)
        {
            Type = type;
        }
    }
}