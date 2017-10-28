using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(BuildingPattern))]
public class BuildingPatternEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty data = property.FindPropertyRelative("Rows");

        EditorGUI.PrefixLabel(position, label);
        Rect newPosition = position;

        newPosition.y += 18f;
        for (int i = 0; i < data.arraySize; i++)
        {
            SerializedProperty row = data.GetArrayElementAtIndex(i).FindPropertyRelative("Collums");
            row.arraySize = data.arraySize;
            newPosition.height = 15f;
            newPosition.width = position.width / data.arraySize;
            for (int j = 0; j < row.arraySize; j++)
            {
                EditorGUI.PropertyField(newPosition, row.GetArrayElementAtIndex(j), GUIContent.none);
                newPosition.x += newPosition.width;
            }

            newPosition.x = position.x;
            newPosition.y += 15f;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 18f * 8;
    }
}
