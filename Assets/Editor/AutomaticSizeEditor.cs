using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AutomaticSize))]
public class AutomaticSizeEditor : Editor
{
    private AutomaticSize _myScript;

    public override void OnInspectorGUI()
    {
        _myScript = (AutomaticSize)target;
        DrawDefaultInspector();

        if (GUILayout.Button("Update Size"))
        {
            _myScript.AdjustSize();
        }

    }

}
