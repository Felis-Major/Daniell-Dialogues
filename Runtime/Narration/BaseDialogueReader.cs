using Daniell.Runtime.Systems.Events;
using Daniell.Runtime.Helpers.Coroutines;
using System.Collections;
using UnityEngine;

namespace Daniell.Runtime.Systems.DialogueNodes
{
    /// <summary>
    /// Base class for reading a dialogue node graph
    /// </summary>
    public abstract class BaseDialogueReader : MonoBehaviour
    {
        /* ==========================
         * > Private Serialized Fields
         * -------------------------- */

        [SerializeField]
        [Tooltip("Event called when the dialogue has started")]
        private VoidEvent _onDialogueStartEvent;

        [SerializeField]
        [Tooltip("Event called when the dialogue has stopped")]
        private VoidEvent _onDialogueStopEvent;


        /* ==========================
         * > Private Fields
         * -------------------------- */

        private Coroutine _readCoroutine;
        private bool _isNextNodeReady;


        /* ==========================
         * > Methods
         * -------------------------- */

        /// <summary>
        /// Start the dialogue
        /// </summary>
        /// <param name="dialogueFile">Dialogue to be read</param>
        public virtual void StartDialogue(DialogueFile dialogueFile)
        {
            this.StartCoroutine(ref _readCoroutine, ReadCoroutine(dialogueFile));
        }

        /// <summary>
        /// Stop the dialogue
        /// </summary>
        public virtual void StopDialogue()
        {
            this.StopCoroutine(ref _readCoroutine);
        }

        /// <summary>
        /// Go to the next node
        /// </summary>
        public void GoToNextNode()
        {
            _isNextNodeReady = true;
        }

        /// <summary>
        /// Coroutine used to read a dialogue node graph
        /// </summary>
        /// <param name="dialogueFile">Dialogue file to be read</param>
        private IEnumerator ReadCoroutine(DialogueFile dialogueFile)
        {
            if (_onDialogueStartEvent != null)
            {
                _onDialogueStartEvent.Raise();
            }

            // Find the start node
            RuntimeNode currentNode = dialogueFile.GetFirstNode();

            _isNextNodeReady = false;

            // While there is a current node
            while (currentNode != null)
            {
                // Process the current node
                ProcessNode(currentNode);

                // Wait for the node to execute
                yield return new WaitUntil(() => _isNextNodeReady);
                _isNextNodeReady = false;

                // Find next node
                currentNode = currentNode.GetNextNode();
            }

            if (_onDialogueStopEvent != null)
            {
                _onDialogueStopEvent.Raise();
            }
        }

        /// <summary>
        /// Process a node
        /// </summary>
        /// <param name="node">Node to be processed</param>
        protected abstract void ProcessNode(RuntimeNode node);
    }
}