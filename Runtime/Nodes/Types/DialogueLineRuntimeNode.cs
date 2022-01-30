using UnityEngine;

namespace Daniell.Runtime.Systems.DialogueNodes
{
    /// <summary>
    /// Runtime version of a Dialogue Line Node
    /// </summary>
    public class DialogueLineRuntimeNode : RuntimeNode
    {
        /* ==========================
         * > Properties
         * -------------------------- */

        /// <summary>
        /// Actor for this dialogue node
        /// </summary>
        public DialogueActor Actor { get => _actor; set => _actor = value; }

        /// <summary>
        /// Line for this dialogue node
        /// </summary>
        public string Line { get => _text; set => _text = value; }


        /* ==========================
         * > Private fields
         * -------------------------- */

        [SerializeField]
        private DialogueActor _actor;

        [SerializeField]
        private string _text;
    }
}