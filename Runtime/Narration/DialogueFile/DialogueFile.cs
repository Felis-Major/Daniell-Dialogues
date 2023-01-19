using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FM.Runtime.Systems.DialogueNodes
{
    /// <summary>
    /// Dialogue file used to store a dialogue graph
    /// </summary>
    [CreateAssetMenu(fileName = "New Dialogue File", menuName = "FM/Dialogues/Dialogue File")]
    [SupportedNodeType(typeof(DialogueLineRuntimeNode))]
    [SupportedNodeType(typeof(DialogueBranchRuntimeNode))]
    [SupportedNodeType(typeof(CallVoidEventRuntimeNode))]
    [SupportedNodeType(typeof(CallBoolEventRuntimeNode))]
    [SupportedNodeType(typeof(CallIntEventRuntimeNode))]
    [SupportedNodeType(typeof(CallStringEventRuntimeNode))]
    [SupportedNodeType(typeof(CallFloatEventRuntimeNode))]
    public class DialogueFile : ScriptableObject
    {
        /* ==========================
         * > Properties
         * -------------------------- */

        /// <summary>
        /// Nodes of this dialogue file
        /// </summary>
        public IEnumerable<RuntimeNode> Nodes => _nodes;


        /* ==========================
         * > Private Fields
         * -------------------------- */

        [SerializeField]
        private List<RuntimeNode> _nodes = new();


        /* ==========================
         * > Methods
         * -------------------------- */

#if UNITY_EDITOR

        /// <summary>
        /// Add a new node to this file (Editor only)
        /// </summary>
        /// <typeparam name="T">Type of the node to be added</typeparam>
        /// <param name="runtimeNodeInstance">Instance of the node to add</param>
        public void AddNode<T>(T runtimeNodeInstance) where T : RuntimeNode
        {
            // Set node dialogue file
            runtimeNodeInstance.DialogueFile = this;

            // Cache nodes
            _nodes.Add(runtimeNodeInstance);

            // Add node to this file
            AssetDatabase.AddObjectToAsset(runtimeNodeInstance, this);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// Clear all the nodes on this file (Editor only)
        /// </summary>
        public void ClearNodes()
        {
            for (int i = 0; i < _nodes.Count; i++)
            {
                AssetDatabase.RemoveObjectFromAsset(_nodes[i]);
                EditorUtility.SetDirty(this);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            _nodes.Clear();
        }

#endif

        /// <summary>
        /// Find a node by its GUID
        /// </summary>
        /// <param name="nodeGUID">GUID to look for</param>
        /// <returns>Node with matching GUID</returns>
        public RuntimeNode FindNodeByGUID(string nodeGUID)
        {
            for (int i = 0; i < _nodes.Count; i++)
            {
                RuntimeNode node = _nodes[i];

                if (node.GUID == nodeGUID)
                {
                    return node;
                }
            }

            return null;
        }

        /// <summary>
        /// Get the first node of the graph
        /// </summary>
        /// <returns></returns>
        public RuntimeNode GetFirstNode()
        {
            for (int i = 0; i < _nodes.Count; i++)
            {
                RuntimeNode node = _nodes[i];
                if (node is StartRuntimeNode startNode)
                {
                    return startNode.GetNextNode();
                }
            }

            return null;
        }
    }
}