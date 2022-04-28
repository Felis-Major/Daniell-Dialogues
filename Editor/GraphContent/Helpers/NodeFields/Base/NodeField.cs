using UnityEditor;
using UnityEngine.UIElements;

namespace FM.Editor.Systems.DialogueNodes
{
    /// <summary>
    /// Node field containing a custom field and a label
    /// </summary>
    public abstract class NodeField : VisualElement
    {
        /* ==========================
         * > Private Fields
         * -------------------------- */

        private readonly NodeFieldLabel _nodeFieldLabel;
        private readonly NodeFieldBox _nodeFieldBox;


        /* ==========================
         * > Constructors
         * -------------------------- */

        protected NodeField(string labelText)
        {
            // Create label and box
            _nodeFieldLabel = new NodeFieldLabel(labelText);
            _nodeFieldBox = new NodeFieldBox();

            // Add fields to the box
            _nodeFieldBox.Add(_nodeFieldLabel);
            _nodeFieldBox.Add(CreateNodeField());

            // Add the box to the root
            Add(_nodeFieldBox);
        }


        /* ==========================
         * > Methods
         * -------------------------- */

        /// <summary>
        /// Create and return a visual element to be added as field
        /// </summary>
        /// <returns>Created visual element</returns>
        protected abstract VisualElement CreateNodeField();
    }
}