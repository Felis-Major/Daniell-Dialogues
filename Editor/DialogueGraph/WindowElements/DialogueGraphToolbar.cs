using Daniell.Editor.Systems.DialogueNodes;
using Daniell.Runtime.Systems.DialogueNodes;
using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Daniell.Editor.DialogueNodes
{
    /// <summary>
    /// Toolbar used for the dialogue graph
    /// </summary>
    public class DialogueGraphToolbar : Toolbar
    {
        /* ==========================
         * > Events
         * -------------------------- */

        /// <summary>
        /// Event called when a dialogue file is loaded
        /// </summary>
        public event Action<DialogueFile> OnDialogueFileLoaded;

        /// <summary>
        /// Event called when the save button is pressed
        /// </summary>
        public event Action OnSaveButtonPressed;


        /* ==========================
         * > Private Fields
         * -------------------------- */

        private readonly ObjectField<DialogueFile> _dialogueFileField;


        /* ==========================
         * > Constructors
         * -------------------------- */

        public DialogueGraphToolbar()
        {
            // Create dialogue file loading field
            _dialogueFileField = new ObjectField<DialogueFile>();
            _dialogueFileField.OnValueChanged += x => OnDialogueFileLoaded?.Invoke(x);

            // Add field to the toolbar
            Add(_dialogueFileField);

            // Create save button
            Button saveButton = new Button(() => OnSaveButtonPressed?.Invoke());
            saveButton.contentContainer.Add(new Image() { image = EditorGUIUtility.IconContent("d_SaveAs").image });
            saveButton.style.width = 30;

            Add(saveButton);
        }

        public void Load(DialogueFile dialogueFile)
        {
            _dialogueFileField.value = dialogueFile;
        }
    }
}