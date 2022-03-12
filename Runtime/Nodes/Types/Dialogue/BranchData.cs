using System.Collections.Generic;
using UnityEngine;

namespace Daniell.Runtime.Systems.DialogueNodes
{
    /// <summary>
    /// Handles storing a branch choice
    /// </summary>
    [CreateAssetMenu]
    public class BranchData : ScriptableObject
    {
        /* ==========================
         * > Properties
         * -------------------------- */

        /// <summary>
        /// Branches for this node
        /// </summary>
        public IEnumerable<string> Branches => _branches;


        /* ==========================
         * > Private fields
         * -------------------------- */

        [SerializeField]
        private List<string> _branches = new List<string>();


        /* ==========================
         * > Methods
         * -------------------------- */

        /// <summary>
        /// Add a branch to data
        /// </summary>
        /// <param name="branch"></param>
        public void AddBranch(string branch)
        {
            _branches.Add(branch);
        }

        /// <summary>
        /// Remove a branch from data
        /// </summary>
        /// <param name="branch"></param>
        public void RemoveBranch(string branch)
        {
            _branches.Remove(branch);
        }

        /// <summary>
        /// Clear all branches
        /// </summary>
        public void ClearBranches()
        {
            _branches.Clear();
        }

        public void Select(int i)
        {

        }
    }
}