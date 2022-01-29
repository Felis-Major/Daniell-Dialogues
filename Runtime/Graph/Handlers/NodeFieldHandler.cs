using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Daniell.Runtime.Systems.DialogueNodes
{
    /// <summary>
    /// Handles creating, deleting and setting node field values
    /// </summary>
    public class NodeFieldHandler : NodeElementHandler
    {
        /* ==========================
         * > Properties
         * -------------------------- */

        /// <summary>
        /// List of all node fields
        /// </summary>
        public Dictionary<string, NodeField>.ValueCollection NodeFields => _fields.Values;


        /* ==========================
         * > Private Fields
         * -------------------------- */

        private Dictionary<string, NodeField> _fields = new Dictionary<string, NodeField>();
        private readonly VisualElement _fieldContainer;


        /* ==========================
         * > Constructors
         * -------------------------- */

        public NodeFieldHandler(BaseNode node) : base(node)
        {
            _fieldContainer = node.extensionContainer;
        }


        /* ==========================
         * > Methods
         * -------------------------- */

        /// <summary>
        /// Try to look for a field using its name
        /// </summary>
        /// <param name="fieldName">Name of the field to look for</param>
        /// <param name="field">Field output if found</param>
        /// <returns>True if a field with a maching name was found</returns>
        public bool TryGetField(string fieldName, out NodeField field)
        {
            if (_fields.ContainsKey(fieldName))
            {
                field = _fields[fieldName];
                return true;
            }
            else
            {
                field = default;
                return false;
            }
        }

        #region Field Value

        /// <summary>
        /// Set the value of a field using its name
        /// </summary>
        /// <typeparam name="T">Type of the value to set</typeparam>
        /// <param name="fieldName">Name of the field</param>
        /// <param name="value">Value of the field</param>
        public void SetFieldValue<T>(string fieldName, T value)
        {
            // Set value if the control exists
            if (TryGetField(fieldName, out NodeField field) && field is GenericNodeField<T> genericField)
            {
                genericField.SetValue(value);
            }
        }

        /// <summary>
        /// Get the value of a field using its name
        /// </summary>
        /// <typeparam name="T">Type of the value to get</typeparam>
        /// <param name="fieldName">Name of the field</param>
        /// <returns>Value of the field as T</returns>
        public T GetFieldValue<T>(string fieldName)
        {
            // Get value if the control exists
            if (TryGetField(fieldName, out NodeField field) && field is GenericNodeField<T> genericField)
            {
                return genericField.GetValue();
            }
            else
            {
                return default;
            }
        }

        #endregion

        #region Add & Remove

        /// <summary>
        /// Add a new field to the node
        /// </summary>
        /// <param name="nodeField">Field</param>
        /// <param name="fieldName">Name of the field</param>
        public void AddField(NodeField nodeField, string fieldName)
        {
            _fieldContainer.Add(nodeField);
            _fields.Add(fieldName, nodeField);

            RefreshNodeState();
        }

        /// <summary>
        /// Remove a field from the node
        /// </summary>
        /// <param name="fieldName">Name of the field</param>
        public void RemoveField(string fieldName)
        {
            if (_fields.ContainsKey(fieldName))
            {
                // Find target field
                var targetField = _fields[fieldName];

                // Remove field from container
                var targetContainer = targetField.parent;
                targetContainer.Remove(targetField);

                _fields.Remove(fieldName);

                RefreshNodeState();
            }
        }

        #endregion
    }
}