using System;
using System.Collections.Generic;
using UnityEngine;

namespace FM.Runtime.Systems.DialogueNodes
{
    /// <summary>
    /// Runtime version of a Dialogue Line Node
    /// </summary>
    public class DialogueBranchRuntimeNode : RuntimeNode
    {
        /* ==========================
         * > Properties
         * -------------------------- */

        /// <summary>
        /// Branches for this node
        /// </summary>
        public IEnumerable<string> Branches => _branches;

        /// <summary>
        /// Actor for this dialogue node
        /// </summary>
        public DialogueActor Actor { get => _actor; set => _actor = value; }

        /// <summary>
        /// Line for this dialogue node
        /// </summary>
        public string Line { get => _line; set => _line = value; }

        /// <summary>
        /// Branch data for this dialogue node
        /// </summary>
        public BranchData BranchData { get => _branchData; set => _branchData = value; }


        /* ==========================
         * > Private fields
         * -------------------------- */

        [SerializeField]
        private List<string> _branches = new List<string>();

        [SerializeField]
        private DialogueActor _actor;

        [SerializeField]
        private string _line;

        [SerializeField]
        private BranchData _branchData;

        private int _selectedBranch = -1;


        /* ==========================
         * > Methods
         * -------------------------- */

        /// <summary>
        /// Select a branch
        /// </summary>
        /// <param name="i">Branch to select</param>
        public void SelectBranch(int i)
        {
            _selectedBranch = i;

            if(_branchData != null)
            {

            }
        }

        /// <summary>
        /// Add a new branch to the node
        /// </summary>
        /// <param name="branchContent">Text of the branch</param>
        public void AddBranch(string branchContent)
        {
            _branches.Add(branchContent);
        }

        /// <summary>
        /// Remove all the branches from this node
        /// </summary>
        public void ClearBranches()
        {
            _branches.Clear();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override RuntimeNode GetNextNode()
        {
            // If there is a selected branch
            if (_selectedBranch != -1)
            {
                var nodes = GetConnectedNodesToPort(_selectedBranch.ToString(), PortID.Direction.Output);
                if (nodes.Length > 0)
                {
                    return nodes[0];
                }
            }

            // Return null if no next node was found
            return null;
        }
    }
}