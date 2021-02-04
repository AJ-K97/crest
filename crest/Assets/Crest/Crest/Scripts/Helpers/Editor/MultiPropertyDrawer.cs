// Taken from: https://forum.unity.com/threads/drawing-a-field-using-multiple-property-drawers.479377/

using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(MultiPropertyAttribute), true)]
public class MultiPropertyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        MultiPropertyAttribute @Attribute = attribute as MultiPropertyAttribute;
        float height = base.GetPropertyHeight(property, label);
        foreach (object atr in @Attribute.stored)//Go through the attributes, and try to get an altered height, if no altered height return default height.
        {
            if (atr as MultiPropertyAttribute != null)
            {
                //build label here too?
                var tempheight = ((MultiPropertyAttribute)atr).GetPropertyHeight(property, label);
                if (tempheight.HasValue)
                {
                    height = tempheight.Value;
                    break;
                }
            }
        }
        return height;
    }
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        MultiPropertyAttribute @Attribute = attribute as MultiPropertyAttribute;
        // First get the attribute since it contains the range for the slider
        if (@Attribute.stored == null || @Attribute.stored.Count == 0)
        {
            @Attribute.stored = fieldInfo.GetCustomAttributes(typeof(MultiPropertyAttribute), false).OrderBy(s => ((PropertyAttribute)s).order).ToList();
        }
        var OrigColor = GUI.color;
        var Label = label;
        foreach (object atr in @Attribute.stored)
        {
            if (atr as MultiPropertyAttribute != null)
            {
                Label = ((MultiPropertyAttribute)atr).BuildLabel(Label);
                ((MultiPropertyAttribute)atr).OnGUI(position, property, Label, this);
            }
        }
        GUI.color = OrigColor;
    }
}