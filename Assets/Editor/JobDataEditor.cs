// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JobDataEditor.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(JobData))]
public class JobDataEditor : Editor
{
    private JobData data;

    public override void OnInspectorGUI()
    {
        data = (JobData) target;

        if (GUILayout.Button("Add Job"))
        {
            data.Jobs.Add(new Job());
        }
        if (GUILayout.Button("Remove Job"))
        {
            data.Jobs.RemoveAt(data.Jobs.Count - 1);
        }
        if (GUILayout.Button("Reoder List"))
        {
            data.SortArray();
        }

        foreach (var job in data.Jobs)
        {
            EditorGUILayout.Space();
            EditorGUIUtility.labelWidth = 30f;
            job.JobName = EditorGUILayout.TextField("Job", job.JobName);
            EditorGUI.indentLevel += 1;
            EditorGUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 29f;
            EditorGUILayout.LabelField("Can have:",GUILayout.ExpandWidth(false));
            job.Female = EditorGUILayout.Toggle("F", job.Female,GUILayout.ExpandWidth(false));
            job.Male = EditorGUILayout.Toggle("M", job.Male,GUILayout.ExpandWidth(false));
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel -= 1;
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

    }



}
