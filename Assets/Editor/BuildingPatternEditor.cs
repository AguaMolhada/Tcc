using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(BuildingPattern))]
public class BuildingPatternEditor : PropertyDrawer
{
    private int collumsNumber = 0;
    private bool ChangeRowCollums;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty data = property.FindPropertyRelative("Rows");
        SerializedProperty row = data.GetArrayElementAtIndex(0).FindPropertyRelative("Collums");
        if(collumsNumber == 0)
        {
            collumsNumber = row.arraySize;
        }

        EditorGUI.PrefixLabel(position, label);
        Rect newPosition = position;
        newPosition.x += 45;
        ChangeRowCollums = EditorGUI.Toggle(new Rect(newPosition.x, newPosition.y, 15, 15), ChangeRowCollums);
        newPosition.x += 15;
        if (collumsNumber != 0)
        {
            if (!ChangeRowCollums)
            {
                EditorGUI.LabelField(new Rect(newPosition.x, newPosition.y, 30, 18), "Row");
                newPosition.x += 30;
                data.arraySize = EditorGUI.IntSlider(new Rect(newPosition.x, newPosition.y, newPosition.width - 50f, 18), data.arraySize, 1, 8);
            }
            else
            {
                EditorGUI.LabelField(new Rect(newPosition.x, newPosition.y, 30, 18), "Coll");
                newPosition.x += 30;
                collumsNumber = EditorGUI.IntSlider(new Rect(newPosition.x, newPosition.y, newPosition.width - 50f, 18), collumsNumber, 1, 8);
            }
        }
        newPosition.x = position.x;
        newPosition.y += 18f;
        for (int i = 0; i < data.arraySize; i++)
        {
            row = data.GetArrayElementAtIndex(i).FindPropertyRelative("Collums");
            row.arraySize = collumsNumber;
            newPosition.height = 15f;
            newPosition.width = position.width / collumsNumber;
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
