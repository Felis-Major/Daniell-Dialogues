using UnityEngine.UIElements;

namespace FM.Editor.Systems.DialogueNodes
{
    /// <summary>
    /// Node field used to enter a string
    /// </summary>
    public class StringNodeField : BaseTypeNodeField<TextField, string>
    {
        /* ==========================
         * > Constructors
         * -------------------------- */

        public StringNodeField(string labelText) : base(labelText)
        {

        }
    }
}