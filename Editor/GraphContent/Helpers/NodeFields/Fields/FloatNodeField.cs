using UnityEditor.UIElements;

namespace FM.Editor.Systems.DialogueNodes
{
    /// <summary>
    /// Node field used to enter a float
    /// </summary>
    public class FloatNodeField : BaseTypeNodeField<FloatField, float>
    {
        /* ==========================
         * > Constructors
         * -------------------------- */

        public FloatNodeField(string labelText) : base(labelText) { }
    }
}