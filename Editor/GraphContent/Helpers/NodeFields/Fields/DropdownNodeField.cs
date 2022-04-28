using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace FM.Editor.Systems.DialogueNodes
{
    /// <summary>
    /// Node Field used to select a value from a dropdown menu
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DropdownNodeField<T> : GenericNodeField<T>
    {
        /* ==========================
         * > Private Fields
         * -------------------------- */

        private PopupField<T> _popupField;
        private List<T> _options;


        /* ==========================
         * > Constructors
         * -------------------------- */

        public DropdownNodeField(string labelText, params T[] options) : base(labelText)
        {
            _options = options.ToList();
        }


        /* ==========================
         * > Methods
         * -------------------------- */

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override T GetValue()
        {
            return _popupField.value;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override void SetValue(T fieldValue)
        {
            base.SetValue(fieldValue);
            _popupField.value = fieldValue;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override VisualElement CreateNodeField()
        {
            if (_options.Count > 0)
            {
                _popupField = new PopupField<T>(_options, 0);
            }
            return _popupField;
        }
    }
}