using System;
using System.Collections.Generic;
using UnityEngine;

namespace Daniell.Runtime.Systems.DialogueNodes
{
    [CreateAssetMenu]
    public class DialogueFile : ScriptableObject
    {
        [SerializeField]
        private List<string> _data = new List<string>();

        public string this[int i]=>_data[i];

        public int Count => _data.Count;

        public void ClearData()
        {
            _data.Clear();
        }

        public void AddData(string jsonData)
        {
            _data.Add(jsonData);
        }

        /// <summary>
        /// Get valid nodes for this file
        /// </summary>
        /// <returns>List of valid types for this file</returns>
        public virtual Dictionary<Type, string> GetValidNodeTypes()
        {
            return new Dictionary<Type, string> {
                { typeof(DialogueLineNode), "Dialogue Line" }
            };
        }
    }
}