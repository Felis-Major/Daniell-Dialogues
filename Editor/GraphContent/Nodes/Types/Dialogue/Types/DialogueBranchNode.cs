using Daniell.Runtime.Systems.DialogueNodes;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Daniell.Editor.Systems.DialogueNodes
{
    /// <summary>
    /// Node used to trigger a new dialogue line
    /// </summary>
    [NodeName("Dialogue Branch")]
    [NodeColor(185, 111, 67)]
    [RuntimeNodeType(typeof(DialogueBranchRuntimeNode))]
    public class DialogueBranchNode : BaseDialogueLineNode<DialogueBranchRuntimeNode>
    {
        /* ==========================
         * > Properties
         * -------------------------- */

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override string DefaultEditorIcon => "d_BlendTree Icon";


        /* ==========================
         * > Data Structures
         * -------------------------- */

        /// <summary>
        /// Branch of the node
        /// </summary>
        protected class Branch
        {
            /* ==========================
             * > Properties
             * -------------------------- */

            /// <summary>
            /// Text data of the branch
            /// </summary>
            public string Text { get => _textField.value; set => _textField.value = value; }

            /// <summary>
            /// Port of the branch
            /// </summary>
            public Port Port => _port;


            /* ==========================
             * > Private fields
             * -------------------------- */

            private readonly TextField _textField;
            private readonly Port _port;
            private readonly Label _label;


            /* ==========================
             * > Constructors
             * -------------------------- */

            public Branch(Port port, TextField textField)
            {
                _port = port;
                _textField = textField;
                _label = port.contentContainer.Q<Label>("type");
                _label.style.width = 20;
            }


            /* ==========================
             * > Methods
             * -------------------------- */

            /// <summary>
            /// Set label value
            /// </summary>
            /// <param name="text">Value of the label</param>
            public void SetLabel(string text)
            {
                _label.text = text;
            }
        }

        /* ==========================
         * > Private fields
         * -------------------------- */

        private List<Branch> _branches = new List<Branch>();


        /* ==========================
         * > Constructors
         * -------------------------- */

        public DialogueBranchNode()
        {
            // Add a branch button
            Button branchButton = new Button(() => AddBranch());
            branchButton.text = "Add Branch";
            branchButton.style.marginRight = 3;
            titleContainer.Add(branchButton);

            _branches.Clear();
        }


        /* ==========================
         * > Methods
         * -------------------------- */

        /// <summary>
        /// Add a new branch to the node
        /// </summary>
        protected void AddBranch()
        {
            AddBranch(string.Empty);
        }

        /// <summary>
        /// Add a new branch to the node
        /// </summary>
        /// <param name="value">Default branch value</param>
        protected void AddBranch(string value)
        {
            var branchName = _branches.Count.ToString();

            // Add the output port
            Port port = _nodePortHandler.AddOutputPort(branchName);

            // Add Text field
            TextField textField = new TextField();
            textField.style.width = 120;
            port.contentContainer.Add(textField);

            var branch = new Branch(port, textField);
            branch.Text = value;

            // Add remove button
            Button button = new Button(() =>
            {
                RemoveBranch(branch);
            });
            button.contentContainer.Add(new Image() { image = EditorGUIUtility.IconContent("TreeEditor.Trash").image });

            port.contentContainer.Add(button);

            _branches.Add(branch);

            RebuildBranches();
        }

        /// <summary>
        /// Remove a branch
        /// </summary>
        /// <param name="branchToRemove">Branch to be removed</param>
        protected void RemoveBranch(Branch branchToRemove)
        {
            // Remove the branch port
            _nodePortHandler.RemovePort(branchToRemove.Port.portName, branchToRemove.Port.direction);

            // Remove the branch from the list
            _branches.Remove(branchToRemove);

            RebuildBranches();
        }

        /// <summary>
        /// Find branches in UI and add them as data
        /// </summary>
        protected void RebuildBranches()
        {
            for (int i = 0; i < _branches.Count; i++)
            {
                Branch branch = _branches[i];
                branch.SetLabel(i.ToString());
            }
        }

        #region Save & Load

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void SaveNode(DialogueBranchRuntimeNode node)
        {
            node.Actor = _nodeFieldHandler.GetFieldValue<DialogueActor>("Actor Field");
            node.Line = _nodeFieldHandler.GetFieldValue<string>("Line Field");

            // Save all the branches
            for (int i = 0; i < _branches.Count; i++)
            {
                Branch branch = _branches[i];
                node.AddBranch(branch.Text);
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void LoadNode(DialogueBranchRuntimeNode node)
        {
            _nodeFieldHandler.SetFieldValue("Actor Field", node.Actor);
            _nodeFieldHandler.SetFieldValue("Line Field", node.Line);

            // Create original amount of branches
            foreach (var branchText in node.Branches)
            {
                AddBranch(branchText);
            }
        }

        #endregion
    }
}