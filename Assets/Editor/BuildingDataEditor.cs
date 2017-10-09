// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuildingDataEditor.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BuildingData))]
public class BuildingDataEditor : Editor
{
    private BuildingData data;

    public override void OnInspectorGUI()
    {
        GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.MaxWidth(120f) , GUILayout.MinWidth(100f) };
        data = (BuildingData) target;
        
        if (GUILayout.Button("Add New Building"))
        {
            data.Buildings.Add(new Building());
        }
        if (GUILayout.Button("Remove Buildin"))
        {
            data.Buildings.RemoveAt(data.Buildings.Count-1);
        }
        if ( GUILayout.Button("Reoder Building List") ) {
            data.SortArray();
        }
        base.OnInspectorGUI();
        

    }
}
