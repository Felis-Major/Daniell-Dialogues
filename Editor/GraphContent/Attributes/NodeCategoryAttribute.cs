using System;

namespace FM.Editor.Systems.DialogueNodes
{
    /// <summary>
    /// Attribute used to define the category of a node
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class NodeCategoryAttribute : Attribute
    {
        /* ==========================
         * > Properties
         * -------------------------- */

        /// <summary>
        /// Category of the node
        /// </summary>
        public string Category { get; }


        /* ==========================
         * > Constructors
         * -------------------------- */

        public NodeCategoryAttribute(string category)
        {
            Category = category;
        }
    }
}