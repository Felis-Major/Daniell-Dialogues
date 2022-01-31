using UnityEngine;

namespace Daniell.Runtime.Systems.DialogueNodes
{
    /// <summary>
    /// Default dialogue reader for base Dialogue Files
    /// </summary>
    public class DialogueReader : BaseDialogueReader
    {
        /* ==========================
         * > Properties
         * -------------------------- */

        [SerializeField]
        [Tooltip("Default Dialogue File")]
        private DialogueFile _defaultDialogueFile;

        [SerializeField]
        [Tooltip("Should this dialogue play on awake?")]
        private bool _playOnAwake = true;


        /* ==========================
         * > Methods
         * -------------------------- */

        private void Awake()
        {
            if (_playOnAwake && _defaultDialogueFile != null)
            {
                StartDialogue(_defaultDialogueFile);
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void ProcessNode(RuntimeNode node)
        {
            switch (node)
            {
                case DialogueLineRuntimeNode dialogueLineRuntimeNode:
                    Debug.Log($"{dialogueLineRuntimeNode.Actor.Name}: {dialogueLineRuntimeNode.Line}");
                    break;

                case DialogueBranchRuntimeNode dialogueBranchRuntimeNode:
                    Debug.Log($"{dialogueBranchRuntimeNode.Actor.Name}: {dialogueBranchRuntimeNode.Line}");
                    dialogueBranchRuntimeNode.SelectBranch(1);
                    break;

                case CallEventRuntimeNode callEventRuntimeNode:
                    callEventRuntimeNode.Event.Raise();

                    // Immediately process this node
                    GoToNextNode();
                    break;
            }
        }
    }
}