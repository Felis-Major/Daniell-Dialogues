using Daniell.Runtime.Systems.SimpleSave;

namespace Daniell.Runtime.Systems.DialogueNodes
{
    /// <summary>
    /// Node used to trigger a new dialogue line
    /// </summary>
    [NodeName("Line")]
    [NodeColor(150, 70, 80)]
    public class DialogueLineNode : BaseNode
    {
        /* ==========================
         * > Constructors
         * -------------------------- */

        public DialogueLineNode()
        {
            // Add Ports
            _nodePortHandler.AddInputPort("Input");
            _nodePortHandler.AddOutputPort("Next");

            // Add parameter fields and link to variables
            _nodeFieldHandler.AddField(new ObjectNodeField<Character>("Speaker"), "Speaker Field");
            _nodeFieldHandler.AddField(new StringNodeField("Line"), "Line Field");
        }


        /* ==========================
         * > Methods
         * -------------------------- */

        #region Save & Load

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override void SaveNodeData(SaveDataBundle nodeSaveData)
        {
            base.SaveNodeData(nodeSaveData);

            var speaker = _nodeFieldHandler.GetFieldValue<Character>("Speaker Field");
            nodeSaveData.Set("Speaker", speaker);

            var line = _nodeFieldHandler.GetFieldValue<string>("Line Field");
            nodeSaveData.Set("Line", line);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override void LoadNodeData(SaveDataBundle nodeSaveData)
        {
            base.LoadNodeData(nodeSaveData);

            var speaker = nodeSaveData.Get<Character>("Speaker");
            _nodeFieldHandler.SetFieldValue("Speaker Field", speaker);

            var line = nodeSaveData.Get<string>("Line");
            _nodeFieldHandler.SetFieldValue("Line Field", line);
        }

        #endregion
    }
}