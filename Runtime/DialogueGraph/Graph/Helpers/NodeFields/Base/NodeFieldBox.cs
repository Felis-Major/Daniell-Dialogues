using UnityEngine.UIElements;

namespace Daniell.Runtime.Systems.DialogueNodes
{
    /// <summary>
    /// Box for a node field
    /// </summary>
    public class NodeFieldBox : Box
    {
        /* ==========================
         * > Constants
         * -------------------------- */

        private const int BORDER_RADIUS = 5;


        /* ==========================
         * > Constructors
         * -------------------------- */

        public NodeFieldBox()
        {
            style.borderBottomLeftRadius = BORDER_RADIUS;
            style.borderBottomRightRadius = BORDER_RADIUS;
            style.borderTopLeftRadius = BORDER_RADIUS;
            style.borderTopRightRadius = BORDER_RADIUS;
            style.marginBottom = BORDER_RADIUS;
        }
    }
}