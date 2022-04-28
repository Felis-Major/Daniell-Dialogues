using UnityEngine;

namespace FM.Editor.Systems.DialogueNodes
{
    public static class NodeHelpers
    {
        public static Color GetRelativeColor(Color color, float colorOffset = 0.3f)
        {
            // Convert color to HSV
            Color.RGBToHSV(color, out float h, out float s, out float v);

            // Add offset 
            var colorAsRGB = Color.HSVToRGB(h, Mathf.Clamp01(s - colorOffset), Mathf.Clamp01(v - colorOffset));
            return colorAsRGB;
        }
    }
}