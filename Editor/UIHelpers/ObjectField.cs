using System;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace FM.Editor.DialogueNodes
{
    /// <summary>
    /// Generic version of Object Field
    /// </summary>
    /// <typeparam name="T">Type of object to display</typeparam>
    public class ObjectField<T> : ObjectField where T : UnityEngine.Object
    {
        /* ==========================
         * > Events
         * -------------------------- */

        /// <summary>
        /// Event called when the value of the field has changed
        /// </summary>
        public event Action<T> OnValueChanged;


        /* ==========================
         * > Constructors
         * -------------------------- */

        public ObjectField()
        {
            objectType = typeof(T);
            this.RegisterValueChangedCallback(x => OnValueChanged?.Invoke((T)x.newValue));
        }
    }
}