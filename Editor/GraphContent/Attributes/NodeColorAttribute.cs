using System;
using UnityEngine;

namespace Daniell.Editor.Systems.DialogueNodes
{
    /// <summary>
    /// Attribute used to set a node color
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class NodeColorAttribute : Attribute
    {
        /* ==========================
         * > Properties
         * -------------------------- */

        /// <summary>
        /// R, G, B, A as Unity Color
        /// </summary>
        public Color Color => new Color32(_r, _g, _b, _a);


        /* ==========================
         * > Private Fields
         * -------------------------- */

        private readonly byte _r;
        private readonly byte _g;
        private readonly byte _b;
        private readonly byte _a;


        /* ==========================
         * > Constructors
         * -------------------------- */

        public NodeColorAttribute(byte r, byte g, byte b) : this(r, g, b, 255) { }

        public NodeColorAttribute(byte r, byte g, byte b, byte a)
        {
            _r = r;
            _g = g;
            _b = b;
            _a = a;
        }
    }
}