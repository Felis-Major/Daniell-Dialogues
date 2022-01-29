using Daniell.Runtime.Systems.DialogueNodes;
using Daniell.Runtime.Systems.SimpleSave;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Daniell.Editor.DialogueNodes
{
    /// <summary>
    /// Editor window used to edit dialogue files
    /// </summary>
    public class DialogueGraphWindow : EditorWindow
    {
        /* ==========================
         * > Private Fields
         * -------------------------- */

        /// <summary>
        /// List of all the nodes currently in the graph
        /// </summary>
        public IEnumerable<BaseNode> Nodes => _graphView.nodes.ToList().Cast<BaseNode>();


        /* ==========================
         * > Private Fields
         * -------------------------- */

        private DialogueGraphView _graphView;
        private DialogueGraphToolbar _toolbar;
        private DialogueFile _dialogueFile;


        /* ==========================
         * > Methods
         * -------------------------- */

        [MenuItem("Daniell/Dialogue Graph")]
        public static void Open()
        {
            var window = GetWindow<DialogueGraphWindow>();
            window.titleContent = new GUIContent("Dialogue Graph");
        }

        #region Enable / Disable

        private void OnEnable()
        {
            // Create the toolbar
            CreateToolbar();
        }

        private void OnDisable()
        {
            // Remove the toolbar
            RemoveToolbar();

            // Remove the graph view
            RemoveGraphView();
        }

        #endregion

        #region Toolbar

        /// <summary>
        /// Create the toolbar
        /// </summary>
        private void CreateToolbar()
        {
            if (_toolbar == null)
            {
                _toolbar = new DialogueGraphToolbar();
                _toolbar.OnDialogueFileLoaded += OnDialogueFileValueChanged;
                _toolbar.OnSaveButtonPressed += Save;

                rootVisualElement.Add(_toolbar);
            }
        }

        /// <summary>
        /// Remove the toolbar
        /// </summary>
        private void RemoveToolbar()
        {
            if (_toolbar != null)
            {
                _toolbar.OnDialogueFileLoaded -= OnDialogueFileValueChanged;
                _toolbar.OnSaveButtonPressed -= Save;

                rootVisualElement.Remove(_toolbar);
            }
        }

        #endregion

        #region GraphView

        /// <summary>
        /// Create the GraphView
        /// </summary>
        private void CreateGraphView()
        {
            if (_graphView == null)
            {
                // Create the graph view instance
                _graphView = new DialogueGraphView("Dialogue Graph", this, _dialogueFile);

                // Stretch view to container
                _graphView.StretchToParentSize();

                // Add to the window
                rootVisualElement.Add(_graphView);

                // Send the view to the back so that the toolbar stays on top
                _graphView.SendToBack();
            }
        }

        /// <summary>
        /// Remove the GraphView
        /// </summary>
        private void RemoveGraphView()
        {
            if (_graphView != null)
            {
                // Remove from the window
                rootVisualElement.Remove(_graphView);

                // Reset reference
                _graphView = null;
            }
        }

        #endregion

        #region Event Callbacks

        private void OnDialogueFileValueChanged(DialogueFile dialogueFile)
        {
            // Cache value
            _dialogueFile = dialogueFile;

            if (dialogueFile != null)
            {
                // Create the graph
                CreateGraphView();

                Load();
            }
            else
            {
                // Remove the graph
                RemoveGraphView();
            }
        }

        #endregion

        #region Save & Load

        public void Save()
        {
            // List all the nodes in the graphview
            _dialogueFile.ClearData();

            foreach (BaseNode node in Nodes)
            {
                // Create a new save data bundle with node GUID
                SaveDataBundle dataBundle = new SaveDataBundle(node.GUID);
                node.SaveNodeData(dataBundle);

                // Save node connection infos in data bundle
                foreach (var connectedGUID in node.Ports.GetConnectedGUIDs())
                {
                    var portName = connectedGUID.Key;
                    var connectedGUIDList = connectedGUID.Value;
                    dataBundle.Set(portName, connectedGUIDList);
                }

                // Save the data bundle as json in the dialogue file
                string bundleAsJson = JsonUtility.ToJson(dataBundle, true);
                _dialogueFile.AddData(bundleAsJson);
            }
        }

        public void Load()
        {
            for (int i = 0; i < _dialogueFile.Count; i++)
            {
                var savedData = JsonUtility.FromJson<SaveDataBundle>(_dialogueFile[i]);

                // Instantiate node using reflection
                var method = typeof(DialogueGraphView).GetMethod(nameof(DialogueGraphView.CreateNode));
                var action = method.MakeGenericMethod(Type.GetType(savedData.Get<string>("Type")));
                var node = action.Invoke(_graphView, null);

                var baseNode = (BaseNode)node;
                baseNode.LoadNodeData(savedData);


















                // Load node connection infos
                var nodePortIDs = baseNode.Ports.GetPortIDs();

                // Load the connections for each port
                for (int j = 0; j < nodePortIDs.Length; i++)
                {
                    var nodePortID = nodePortIDs[i];

                    if (savedData.ContainsKey(nodePortID))
                    {
                        var portConnectedGUIDs = savedData.Get<string[]>(nodePortID);

                        // Find and connect to each node
                        for (int k = 0; k < portConnectedGUIDs.Length; k++)
                        {

                        }
                    }
                }





















            }
        }

        #endregion

        #region Helpers

        public BaseNode FindNodeByGUID(string guid)
        {
            foreach (BaseNode node in Nodes)
            {
                if (node.GUID == guid)
                {
                    return node;
                }
            }

            return null;
        }

        #endregion
    }
}