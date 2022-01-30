using System;

namespace Daniell.Editor.Systems.DialogueNodes
{
    /// <summary>
    /// Generic version of NodeField allowing for setting and getting field value
    /// </summary>
    /// <typeparam name="T">Type of the field value</typeparam>
    public abstract class GenericNodeField<T> : NodeField
    {
        /* ==========================
         * > Events
         * -------------------------- */

        /// <summary>
        /// Event called when the value of this field has changed
        /// </summary>
        public event Action<T> OnFieldValueChanged;


        /* ==========================
         * > Constructors
         * -------------------------- */

        protected GenericNodeField(string labelText) : base(labelText) { }


        /* ==========================
         * > Methods
         * -------------------------- */

        /// <summary>
        /// Set the value of this field
        /// </summary>
        /// <param name="value">Value to set</param>
        public virtual void SetValue(T value)
        {
            OnFieldValueChanged?.Invoke(value);
        }

        /// <summary>
        /// Get the value from this field
        /// </summary>
        /// <returns>Value as T</returns>
        public virtual T GetValue()
        {
            return default;
        }
    }
}