

namespace FM.Editor.Systems.DialogueNodes
{
    /// <summary>
    /// Node field used to enter a float
    /// </summary>
    public class FloatNodeField : BaseTypeNodeField<UnityEditor.UIElements.FloatField, float>
    {
        /* ==========================
         * > Constructors
         * -------------------------- */

        public FloatNodeField(string labelText) : base(labelText) { }
    }
}