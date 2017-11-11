// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutomaticSize.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
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
