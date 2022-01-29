using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Daniell.Runtime.Systems.DialogueNodes
{
    public class NodeFieldHandler : NodeElementHandler
    {
        private readonly VisualElement _fieldContainer;

        private Dictionary<string, LabeledNodeField> _fields = new Dictionary<string, LabeledNodeField>();

        public NodeFieldHandler(BaseNode node) : base(node)
        {
            _fieldContainer = node.extensionContainer;
        }

        public bool TryGetField(string fieldName, out LabeledNodeField field)
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

        public void SetFieldValue<T>(string fieldName, T value)
        {
            // Set value if the control exists
            if (TryGetField(fieldName, out LabeledNodeField field) && field is LabeledNodeField<T> genericField)
            {
                genericField.SetValue(value);
            }
        }

        public T GetFieldValue<T>(string fieldName)
        {
            // Get value if the control exists
            if (TryGetField(fieldName, out LabeledNodeField field) && field is LabeledNodeField<T> genericField)
            {
                return genericField.GetValue();
            }
            else
            {
                return default;
            }
        }

        public void AddField(LabeledNodeField labeledNodeField, string fieldName, VisualElement container = null)
        {
            if (container == null)
            {
                container = Node.extensionContainer;
            }

            labeledNodeField.Create();

            container.Add(labeledNodeField.FieldContainer);
            _fields.Add(fieldName, labeledNodeField);

            RefreshNodeState();
        }

        public void RemoveField(string fieldName)
        {
            if (_fields.ContainsKey(fieldName))
            {
                // Find target field
                var targetField = _fields[fieldName].FieldContainer;

                // Find container
                var targetContainer = targetField.parent;

                // Remove field from container
                targetContainer.Remove(targetField);

                // Remove from container list
                _fields.Remove(fieldName);

                RefreshNodeState();
            }
        }
    }
}