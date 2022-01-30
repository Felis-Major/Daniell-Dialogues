using Daniell.Runtime.Helpers.DataStructures;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Daniell.Runtime.Systems.DialogueNodes
{
    [CreateAssetMenu]
    public class DialogueFile : ScriptableObject
    {
        public IEnumerable<RuntimeNode> Nodes => _nodes;

        [SerializeField]
        private List<RuntimeNode> _nodes = new List<RuntimeNode>();

#if UNITY_EDITOR

        public void AddNode<T>(T runtimeNodeInstance) where T : RuntimeNode
        {
            // Cache data as dictionary
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
    }
}