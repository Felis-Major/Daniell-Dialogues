using FM.Runtime.Systems.Events;
using UnityEngine;

namespace FM.Runtime.Systems.DialogueNodes
{
    /// <summary>
    /// Runtime version of call event nodes that have a value to be raised
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class CallGenericEventRuntimeNode<TEvent, TValue> :
        CallEventRuntimeNode<TEvent>
        where TEvent : GenericScriptableEvent<TValue>
    {
        /* ==========================
         * > Properties
         * -------------------------- */

        /// <summary>
        /// Value of the event
        /// </summary>
        public TValue EventValue { get => _eventValue; set => _eventValue = value; }


        /* ==========================
         * > Private fields
         * -------------------------- */

        [SerializeField]
        private TValue _eventValue;
    }
}