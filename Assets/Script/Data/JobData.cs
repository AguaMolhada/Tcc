// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JobData.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using UnityEngine;

[CreateAssetMenu()]
public class JobData : UpdatableObject
{
    public Job[] Jobs;

    [ContextMenu("Sort Array")]
    protected override void OnValidate()
    {
        if (AutoUpdate)
        {
            SortArray();
            AutoUpdate = false;
        }
    }

    private void SortArray()
    {
        System.Array.Sort(Jobs, (a, b) => string.Compare(a.JobName, b.JobName, StringComparison.Ordinal));

    }

}