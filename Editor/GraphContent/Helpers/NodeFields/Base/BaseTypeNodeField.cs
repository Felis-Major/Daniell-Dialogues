using UnityEngine.UIElements;

namespace Daniell.Editor.Systems.DialogueNodes
{
    /// <summary>
    /// Generic implementation of a node field for base C# types
    /// </summary>
    /// <typeparam name="TField">Type of the field</typeparam>
    /// <typeparam name="TValue">Type of the field value</typeparam>
    public class BaseTypeNodeField<TField, TValue> : GenericNodeField<TValue> where TField : BaseField<TValue>, new()
    {
        /* ==========================
         * > Private Fields
         * -------------------------- */

        private TField _field;


        /* ==========================
         * > Constructors
         * -------------------------- */

        public BaseTypeNodeField(string labelText) : base(labelText) { }


        /* ==========================
         * > Methods
         * -------------------------- */

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override void SetValue(TValue value)
        {
            base.SetValue(value);
            _field.value = value;
            _field.MarkDirtyRepaint();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override TValue GetValue()
        {
            return _field.value;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override VisualElement CreateNodeField()
        {
            _field = new TField();
            return _field;
        }
    }
}