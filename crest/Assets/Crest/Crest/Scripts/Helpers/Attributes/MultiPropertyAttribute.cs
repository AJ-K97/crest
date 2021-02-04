// Taken from: https://forum.unity.com/threads/drawing-a-field-using-multiple-property-drawers.479377/

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public abstract class MultiPropertyAttribute : PropertyAttribute
{
    public List<object> stored = new List<object>();
    public virtual GUIContent BuildLabel(GUIContent label)
    {
        return label;
    }
    public abstract void OnGUI(Rect position, SerializedProperty property, GUIContent label, MultiPropertyDrawer drawer);

    internal virtual float? GetPropertyHeight( SerializedProperty property, GUIContent label)
    {
        return null;
    }
}