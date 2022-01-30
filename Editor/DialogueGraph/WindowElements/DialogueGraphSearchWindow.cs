using Daniell.Editor.Systems.DialogueNodes;
using Daniell.Runtime.Helpers.Reflection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Daniell.Editor.DialogueNodes
{
    /// <summary>
    /// Handles searching and creating nodes
    /// </summary>
    public class DialogueGraphSearchWindow : ScriptableObject, ISearchWindowProvider
    {
        /* ==========================
         * > Private Fields
         * -------------------------- */

        private EditorWindow _editorWindow;
        private DialogueGraphView _graphView;
        private List<Type> _validNodeTypes;


        /* ==========================
         * > Methods
         * -------------------------- */

        public void Initialize(EditorWindow editorWindow, DialogueGraphView dialogueGraphView, List<Type> validNodeTypes)
        {
            _editorWindow = editorWindow;
            _graphView = dialogueGraphView;
            _validNodeTypes = validNodeTypes;
        }

        private struct NodeCreationContext
        {
            public Func<BaseNode> CreateNodeAction { get; private set; }

            public NodeCreationContext(Func<BaseNode> createNodeAction)
            {
                CreateNodeAction = createNodeAction;
            }

            public void CreateNode(out BaseNode node)
            {
                node = CreateNodeAction?.Invoke();
            }
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            // Create search tree entry list
            List<SearchTreeEntry> searchTreeEntries = new List<SearchTreeEntry>();

            // Add header
            searchTreeEntries.Add(GetGroup("Create Nodes", 0));

            // Find groups
            List<string> createdGroups = new List<string>();

            foreach (Type validNodeType in _validNodeTypes)
            {
                var nodeNameAttribute = ReflectionHelpers.GetAttributeForType<NodeNameAttribute>(validNodeType);

                string groupName = nodeNameAttribute.Name;
                if (!createdGroups.Contains(groupName))
                {
                    searchTreeEntries.Add(GetGroup(groupName, 1));
                    createdGroups.Add(groupName);
                }

                // Create groups
                var method = typeof(DialogueGraphSearchWindow).GetMethod(nameof(DialogueGraphSearchWindow.GetEntry), BindingFlags.NonPublic | BindingFlags.Instance);
                var action = method.MakeGenericMethod(validNodeType);
                var entry = action.Invoke(this, new object[] { 2 });
                searchTreeEntries.Add(((SearchTreeEntry)entry));
            }

            return searchTreeEntries;
        }

        public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
        {
            NodeCreationContext userData = (NodeCreationContext)searchTreeEntry.userData;
            userData.CreateNode(out BaseNode node);

            var worldMousePosition = _editorWindow.rootVisualElement.ChangeCoordinatesTo(_editorWindow.rootVisualElement.parent, context.screenMousePosition - _editorWindow.position.position);
            var localMousePosition = _graphView.contentViewContainer.WorldToLocal(worldMousePosition);

            node.SetPosition((int)localMousePosition.x, (int)localMousePosition.y);

            return true;
        }

        #region Search Tree Builder

        private SearchTreeEntry GetEntry<T>(int level) where T : BaseNode, new()
        {
            var nodeName = Regex.Replace(typeof(T).Name, @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");
            var searchTreeEntry = new SearchTreeEntry(new GUIContent(nodeName));
            searchTreeEntry.userData = new NodeCreationContext(() =>
            {
                var node = _graphView.CreateNode<T>();
                return node;
            });

            searchTreeEntry.level = level;

            return searchTreeEntry;
        }

        private SearchTreeGroupEntry GetGroup(string groupName, int level)
        {
            return new SearchTreeGroupEntry(new GUIContent(groupName), level);
        }

        #endregion
    }
}