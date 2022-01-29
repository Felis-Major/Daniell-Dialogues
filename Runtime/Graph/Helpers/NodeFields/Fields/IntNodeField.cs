using UnityEditor.UIElements;

namespace Daniell.Runtime.Systems.DialogueNodes
{
    /// <summary>
    /// Node field used to enter an int
    /// </summary>
    public class IntNodeField : BaseTypeNodeField<IntegerField, int>
    {
        /* ==========================
         * > Constructors
         * -------------------------- */

        public IntNodeField(string labelText) : base(labelText) { }
    }
}