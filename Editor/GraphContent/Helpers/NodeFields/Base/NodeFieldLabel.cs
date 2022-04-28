using System;
using UnityEngine.UIElements;

namespace FM.Editor.Systems.DialogueNodes
{
    /// <summary>
    /// Label for a node field
    /// </summary>
    public class NodeFieldLabel : Label
    {
        /* ==========================
         * > Constructors
         * -------------------------- */

        public NodeFieldLabel(string text) : base($"• {text}")
        {
            style.marginLeft = 3;
        }
    }
}