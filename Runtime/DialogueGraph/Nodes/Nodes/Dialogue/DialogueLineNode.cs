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
         * > Constants
         * -------------------------- */

        /// <summary>
        /// Name of the speaker field
        /// </summary>
        private const string SPEAKER_FIELD_NAME = "Speaker";

        /// <summary>
        /// Name of the line field
        /// </summary>
        private const string LINE_FIELD_NAME = "Line";


        /* ==========================
         * > Constructors
         * -------------------------- */

        public DialogueLineNode()
        {
            // Add Ports
            Ports.AddInputPort("Input");
            Ports.AddOutputPort("Next");

            // Add parameter fields and link to variables
            Fields.AddField(new ObjectNodeField<Character>("Speaker"), SPEAKER_FIELD_NAME);
            Fields.AddField(new StringNodeField("Line", true), LINE_FIELD_NAME);
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

            var speaker = Fields.GetFieldValue<Character>(SPEAKER_FIELD_NAME);
            nodeSaveData.Set("Speaker", speaker);

            var line = Fields.GetFieldValue<string>(LINE_FIELD_NAME);
            nodeSaveData.Set("Line", line);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override void LoadNodeData(SaveDataBundle nodeSaveData)
        {
            base.LoadNodeData(nodeSaveData);

            var speaker = nodeSaveData.Get<Character>("Speaker");
            Fields.SetFieldValue(SPEAKER_FIELD_NAME, speaker);

            var line = nodeSaveData.Get<string>("Line");
            Fields.SetFieldValue(LINE_FIELD_NAME, line);
        }

        #endregion
    }
}