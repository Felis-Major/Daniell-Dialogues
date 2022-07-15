

namespace FM.Editor.Systems.DialogueNodes
{
    /// <summary>
    /// Node field used to enter an int
    /// </summary>
    public class IntNodeField : BaseTypeNodeField<UnityEditor.UIElements.IntegerField, int>
    {
        /* ==========================
         * > Constructors
         * -------------------------- */

        public IntNodeField(string labelText) : base(labelText) { }
    }
}