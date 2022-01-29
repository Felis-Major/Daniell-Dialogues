using System;

namespace Daniell.Runtime.Systems.DialogueNodes
{
    /// <summary>
    /// Attribute used to determine the width of a node
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class NodeWidthAttribute : Attribute
    {
        /* ==========================
         * > Properties
         * -------------------------- */

        /// <summary>
        /// Width of the node
        /// </summary>
        public int Width { get; }


        /* ==========================
         * > Constructors
         * -------------------------- */

        public NodeWidthAttribute(int width)
        {
            Width = width;
        }
    }
}