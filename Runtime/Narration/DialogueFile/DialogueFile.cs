using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Daniell.Runtime.Systems.DialogueNodes
{
    [CreateAssetMenu]
    [SupportedNodeType(typeof(DialogueLineRuntimeNode))]
    [SupportedNodeType(typeof(CallEventRuntimeNode))]
    public class DialogueFile : ScriptableObject
    {
        public IEnumerable<RuntimeNode> Nodes => _nodes;

        [SerializeField]
        private List<RuntimeNode> _nodes = new List<RuntimeNode>();

#if UNITY_EDITOR

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

                if(node.GUID == nodeGUID)
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
                if(node is StartRuntimeNode startNode)
                {
                    return startNode.GetNextNode();
                }
            }

            return null;
        }
    }
}